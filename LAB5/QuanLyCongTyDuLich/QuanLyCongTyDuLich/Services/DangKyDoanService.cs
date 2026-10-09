
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class DangKyDoanService
    {
        public DataTable LayDoanKhach()
        {
            return Db.Query(
                @"SELECT MaDoan, TenCoQuanDaiDien,
                         DiaChi, DienThoai, NguoiDaiDien
                  FROM DoanKhach
                  ORDER BY TenCoQuanDaiDien");
        }

        public DataTable LayDanhSach()
        {
            return Db.Query(@"
                SELECT d.SoDKDoan,
                       k.TenCoQuanDaiDien,
                       t.TenTour,
                       d.NgayDi,
                       d.NgayKetThucDuKien,
                       d.SoNguoi,
                       d.MuaBaoHiem,
                       d.TienCoc,
                       d.TongTienDuKien,
                       d.TrangThai
                FROM DangKyDoan d
                JOIN DoanKhach k ON d.MaDoan = k.MaDoan
                JOIN Tour t ON d.MaTour = t.MaTour
                ORDER BY d.NgayDangKy DESC");
        }

        public DataTable LayThanhVien(string soDK)
        {
            return Db.Query(@"
                SELECT STT, HoTen, NgaySinh, SoGiayTo
                FROM ThanhVienDoan
                WHERE SoDKDoan = @s
                ORDER BY STT",
                Db.P("@s", soDK));
        }

        public KetQuaXuLy DangKy(
            string soDK,
            string maDoan,
            string tenCoQuan,
            string diaChi,
            string dienThoai,
            string nguoiDaiDien,
            string maTour,
            DateTime ngayDi,
            int soNguoi,
            string diaDiemDon,
            bool muaBaoHiem,
            decimal tienCoc,
            List<ThanhVienDoanItem> thanhVien)
        {
            if (string.IsNullOrWhiteSpace(soDK) ||
                string.IsNullOrWhiteSpace(maDoan) ||
                string.IsNullOrWhiteSpace(tenCoQuan) ||
                string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(dienThoai) ||
                string.IsNullOrWhiteSpace(nguoiDaiDien) ||
                string.IsNullOrWhiteSpace(maTour) ||
                string.IsNullOrWhiteSpace(diaDiemDon))
            {
                return KetQuaXuLy.Fail(
                    "Thông tin phiếu đăng ký đoàn chưa đầy đủ.");
            }

            if (soNguoi <= QuyDinh.MocKhachDoan)
            {
                return KetQuaXuLy.Fail(
                    "Khách đoàn phải trên 12 người. " +
                    "Đúng 12 người đề chưa quy định.");
            }

            if (ngayDi.Date <= DateTime.Today)
            {
                return KetQuaXuLy.Fail(
                    "Ngày đi phải sau ngày đăng ký.");
            }

            if (tienCoc <= 0)
            {
                return KetQuaXuLy.Fail(
                    "Khách đoàn phải đặt cọc trước.");
            }

            if (muaBaoHiem)
            {
                if (thanhVien == null ||
                    thanhVien.Count != soNguoi)
                {
                    return KetQuaXuLy.Fail(
                        "Đoàn mua bảo hiểm cần danh sách đủ " +
                        soNguoi + " thành viên.");
                }

                foreach (ThanhVienDoanItem x in thanhVien)
                {
                    if (x == null ||
                        string.IsNullOrWhiteSpace(x.HoTen))
                    {
                        return KetQuaXuLy.Fail(
                            "Danh sách thành viên có người thiếu họ tên.");
                    }
                }
            }

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr = cn.BeginTransaction())
            {
                try
                {
                    int soNgay;
                    decimal donGia;

                    using (SqlCommand cmd = new SqlCommand(
                        @"SELECT SoNgay, DonGiaKhach
                          FROM Tour
                          WHERE MaTour = @t
                            AND DangMoBan = 1",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@t", maTour));

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                rd.Close();
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Tour không tồn tại hoặc chưa mở bán.");
                            }

                            soNgay = Convert.ToInt32(rd["SoNgay"]);
                            donGia = Convert.ToDecimal(
                                rd["DonGiaKhach"]);
                        }
                    }

                    DateTime ketThuc = ngayDi.Date
                        .AddDays(soNgay - 1);

                    decimal tongDuKien = donGia * soNguoi;

                    if (tienCoc > tongDuKien)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Tiền cọc không được vượt tổng tiền.");
                    }

                    Exec(cn, tr, @"
                        IF EXISTS
                        (
                            SELECT 1 FROM DoanKhach
                            WHERE MaDoan = @m
                        )
                            UPDATE DoanKhach
                            SET TenCoQuanDaiDien = @t,
                                DiaChi = @d,
                                DienThoai = @p,
                                NguoiDaiDien = @dd
                            WHERE MaDoan = @m
                        ELSE
                            INSERT INTO DoanKhach
                            (
                                MaDoan, TenCoQuanDaiDien,
                                DiaChi, DienThoai, NguoiDaiDien
                            )
                            VALUES (@m, @t, @d, @p, @dd)",
                        Db.P("@m", maDoan),
                        Db.P("@t", tenCoQuan),
                        Db.P("@d", diaChi),
                        Db.P("@p", dienThoai),
                        Db.P("@dd", nguoiDaiDien));

                    Exec(cn, tr, @"
                        INSERT INTO DangKyDoan
                        (
                            SoDKDoan, MaDoan, MaTour,
                            NgayDangKy, NgayDi,
                            NgayKetThucDuKien,
                            SoNguoi, DiaDiemDon,
                            MuaBaoHiem, TienCoc,
                            DaThanhToanCoc,
                            TongTienDuKien, TrangThai
                        )
                        VALUES
                        (
                            @s, @md, @mt,
                            SYSDATETIME(), @di, @kt,
                            @n, @don, @bh, @c,
                            1, @tong, @tt
                        )",
                        Db.P("@s", soDK),
                        Db.P("@md", maDoan),
                        Db.P("@mt", maTour),
                        Db.P("@di", ngayDi.Date),
                        Db.P("@kt", ketThuc),
                        Db.P("@n", soNguoi),
                        Db.P("@don", diaDiemDon),
                        Db.P("@bh", muaBaoHiem),
                        Db.P("@c", tienCoc),
                        Db.P("@tong", tongDuKien),
                        Db.P("@tt", QuyDinh.DaDangKy));

                    if (muaBaoHiem)
                    {
                        for (int i = 0; i < thanhVien.Count; i++)
                        {
                            ThanhVienDoanItem x = thanhVien[i];

                            Exec(cn, tr, @"
                                INSERT INTO ThanhVienDoan
                                (
                                    SoDKDoan, STT, HoTen,
                                    NgaySinh, SoGiayTo
                                )
                                VALUES
                                (@s, @st, @h, @ns, @gt)",
                                Db.P("@s", soDK),
                                Db.P("@st", i + 1),
                                Db.P("@h", x.HoTen),
                                Db.P("@ns", x.NgaySinh),
                                Db.P("@gt", x.SoGiayTo));
                        }
                    }

                    tr.Commit();

                    return KetQuaXuLy.Ok(
                        "Đã lập phiếu đăng ký đoàn.\n" +
                        "Kết thúc dự kiến: " +
                        ketThuc.ToString("dd/MM/yyyy") + "\n" +
                        "Tổng tiền: " +
                        tongDuKien.ToString("N0") + " đ\n" +
                        "Tiền cọc: " +
                        tienCoc.ToString("N0") + " đ");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public KetQuaXuLy HuyDangKy(string soDK)
        {
            if (string.IsNullOrWhiteSpace(soDK))
            {
                return KetQuaXuLy.Fail(
                    "Chưa chọn phiếu đăng ký đoàn.");
            }

            try
            {
                DataTable dt = Db.Query(@"
                    SELECT NgayDi, TrangThai
                    FROM DangKyDoan
                    WHERE SoDKDoan = @s",
                    Db.P("@s", soDK));

                if (dt.Rows.Count == 0)
                {
                    return KetQuaXuLy.Fail(
                        "Không tìm thấy phiếu đăng ký đoàn.");
                }

                if (Convert.ToString(
                    dt.Rows[0]["TrangThai"]) != QuyDinh.DaDangKy)
                {
                    return KetQuaXuLy.Fail(
                        "Chỉ hủy được phiếu đang ở trạng thái Đã đăng ký.");
                }

                if (Convert.ToDateTime(
                    dt.Rows[0]["NgayDi"]).Date <= DateTime.Today)
                {
                    return KetQuaXuLy.Fail(
                        "Đoàn đã khởi hành, không thể hủy.");
                }

                using (SqlConnection cn = Db.OpenConnection())
                using (SqlTransaction tr = cn.BeginTransaction())
                {
                    try
                    {
                        Exec(cn, tr, @"
                            DELETE FROM PhanCongHDV
                            WHERE SoDKDoan = @s",
                            Db.P("@s", soDK));

                        Exec(cn, tr, @"
                            UPDATE DangKyDoan
                            SET TrangThai = @tt
                            WHERE SoDKDoan = @s",
                            Db.P("@tt", QuyDinh.HuyMatCoc),
                            Db.P("@s", soDK));

                        tr.Commit();

                        return KetQuaXuLy.Ok(
                            "Đã hủy phiếu " + soDK +
                            ". Đoàn mất tiền cọc theo quy định.");
                    }
                    catch (Exception ex)
                    {
                        try { tr.Rollback(); }
                        catch { }

                        return KetQuaXuLy.Fail(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        private static void Exec(
            SqlConnection cn,
            SqlTransaction tr,
            string sql,
            params SqlParameter[] ps)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, cn, tr))
            {
                cmd.Parameters.AddRange(ps);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
