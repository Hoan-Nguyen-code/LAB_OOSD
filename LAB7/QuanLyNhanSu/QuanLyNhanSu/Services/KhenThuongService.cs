
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class KhenThuongService
    {
        public DataTable LayNhanVien()
        {
            return Db.Query(@"
                SELECT MaNV,
                       MaNV + N' - ' + HoTen AS HienThi
                FROM NhanVien
                ORDER BY HoTen");
        }

        public DataTable LayPhongBan()
        {
            return Db.Query(@"
                SELECT MaPhongBan,
                       MaPhongBan + N' - ' + TenPhongBan AS HienThi
                FROM PhongBan
                ORDER BY TenPhongBan");
        }

        // ================= QUYẾT ĐỊNH =================

        public DataTable LayQuyetDinh(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    q.MaQDKT,
                    CASE WHEN q.MaNV IS NOT NULL
                         THEN N'Cá nhân'
                         ELSE N'Phòng ban'
                    END AS DoiTuong,
                    q.MaNV,
                    nv.HoTen,
                    q.MaPhongBan,
                    pb.TenPhongBan,
                    q.NgayQuyetDinh,
                    q.LyDo,
                    q.SoTienThuong,
                    q.NguoiKy,
                    CASE WHEN p.MaPhieuChi IS NULL
                         THEN N'Chưa lập phiếu chi'
                         ELSE N'Đã lập phiếu chi'
                    END AS TinhTrangChi
                FROM QuyetDinhKhenThuong q
                LEFT JOIN NhanVien nv
                    ON q.MaNV = nv.MaNV
                LEFT JOIN PhongBan pb
                    ON q.MaPhongBan = pb.MaPhongBan
                LEFT JOIN PhieuChiKhenThuong p
                    ON p.MaQDKT = q.MaQDKT
                WHERE ISNULL(@TuKhoa, N'') = N''
                   OR q.MaQDKT LIKE N'%' + @TuKhoa + N'%'
                   OR nv.HoTen LIKE N'%' + @TuKhoa + N'%'
                   OR pb.TenPhongBan LIKE N'%' + @TuKhoa + N'%'
                   OR q.LyDo LIKE N'%' + @TuKhoa + N'%'
                   OR q.NguoiKy LIKE N'%' + @TuKhoa + N'%'
                ORDER BY q.NgayQuyetDinh DESC, q.MaQDKT",
                Db.P("@TuKhoa", tuKhoa));
        }

        public KetQuaXuLy LapQuyetDinh(
            string maQDKT,
            string loaiDoiTuong,
            string maDoiTuong,
            DateTime ngayQuyetDinh,
            string lyDo,
            decimal soTien,
            string nguoiKy)
        {
            if (string.IsNullOrWhiteSpace(maQDKT) ||
                string.IsNullOrWhiteSpace(maDoiTuong) ||
                string.IsNullOrWhiteSpace(lyDo) ||
                string.IsNullOrWhiteSpace(nguoiKy))
            {
                return KetQuaXuLy.Fail(
                    "Phải nhập đủ mã quyết định, đối tượng, " +
                    "lý do và người ký.");
            }

            if (loaiDoiTuong != "Cá nhân" &&
                loaiDoiTuong != "Phòng ban")
            {
                return KetQuaXuLy.Fail(
                    "Loại đối tượng khen thưởng không hợp lệ.");
            }

            if (soTien <= 0)
                return KetQuaXuLy.Fail(
                    "Số tiền thưởng phải lớn hơn 0.");

            try
            {
                string maNV = loaiDoiTuong == "Cá nhân"
                    ? maDoiTuong : null;

                string maPhongBan = loaiDoiTuong == "Phòng ban"
                    ? maDoiTuong : null;

                Db.Execute(@"
                    INSERT INTO QuyetDinhKhenThuong
                    (
                        MaQDKT, MaNV, MaPhongBan,
                        NgayQuyetDinh, LyDo,
                        SoTienThuong, NguoiKy
                    )
                    VALUES
                    (
                        @Ma, @NV, @Phong,
                        @Ngay, @LyDo, @Tien, @NguoiKy
                    )",
                    Db.P("@Ma", maQDKT),
                    Db.P("@NV", maNV),
                    Db.P("@Phong", maPhongBan),
                    Db.P("@Ngay", ngayQuyetDinh.Date),
                    Db.P("@LyDo", lyDo),
                    Db.P("@Tien", soTien),
                    Db.P("@NguoiKy", nguoiKy));

                return KetQuaXuLy.Ok(
                    "Lập quyết định khen thưởng thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // ================= PHIẾU CHI =================

        public DataTable LayQuyetDinhChuaChi()
        {
            return Db.Query(@"
                SELECT
                    q.MaQDKT,
                    q.MaQDKT + N' - ' +
                    CASE WHEN q.MaNV IS NOT NULL
                         THEN nv.HoTen
                         ELSE pb.TenPhongBan
                    END AS HienThi,
                    q.SoTienThuong,
                    q.NgayQuyetDinh,
                    CASE WHEN q.MaNV IS NOT NULL
                         THEN N'Cá nhân'
                         ELSE N'Phòng ban'
                    END AS DoiTuong
                FROM QuyetDinhKhenThuong q
                LEFT JOIN NhanVien nv ON nv.MaNV = q.MaNV
                LEFT JOIN PhongBan pb
                    ON pb.MaPhongBan = q.MaPhongBan
                WHERE NOT EXISTS
                (
                    SELECT 1 FROM PhieuChiKhenThuong p
                    WHERE p.MaQDKT = q.MaQDKT
                )
                ORDER BY q.NgayQuyetDinh DESC, q.MaQDKT");
        }

        public DataTable LayPhieuChi()
        {
            return Db.Query(@"
                SELECT
                    p.MaPhieuChi,
                    p.MaQDKT,
                    CASE WHEN q.MaNV IS NOT NULL
                         THEN N'Cá nhân'
                         ELSE N'Phòng ban'
                    END AS DoiTuong,
                    COALESCE(nv.HoTen, pb.TenPhongBan) AS TenDoiTuong,
                    q.LyDo,
                    q.SoTienThuong,
                    p.NgayChi,
                    p.SoTienChi,
                    p.HinhThucChi
                FROM PhieuChiKhenThuong p
                JOIN QuyetDinhKhenThuong q
                    ON p.MaQDKT = q.MaQDKT
                LEFT JOIN NhanVien nv ON nv.MaNV = q.MaNV
                LEFT JOIN PhongBan pb
                    ON pb.MaPhongBan = q.MaPhongBan
                ORDER BY p.NgayChi DESC, p.MaPhieuChi");
        }

        public KetQuaXuLy LapPhieuChi(
            string maPhieuChi,
            string maQDKT,
            DateTime ngayChi,
            decimal soTienChi,
            string hinhThucChi)
        {
            if (string.IsNullOrWhiteSpace(maPhieuChi) ||
                string.IsNullOrWhiteSpace(maQDKT))
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã phiếu chi hoặc chọn quyết định.");

            if (hinhThucChi != "Tiền mặt" &&
                hinhThucChi != "Chuyển khoản" &&
                hinhThucChi != "Qua lương")
                return KetQuaXuLy.Fail(
                    "Hình thức chi không hợp lệ.");

            if (soTienChi <= 0)
                return KetQuaXuLy.Fail(
                    "Số tiền chi phải lớn hơn 0.");

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr =
                cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    decimal tienQuyetDinh;
                    DateTime ngayQuyetDinh;
                    bool laPhongBan;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT SoTienThuong,
                               NgayQuyetDinh,
                               MaPhongBan
                        FROM QuyetDinhKhenThuong
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaQDKT = @MaQD", cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaQD", maQDKT));

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Không tìm thấy quyết định khen thưởng.");
                            }

                            tienQuyetDinh = rd.GetDecimal(0);
                            ngayQuyetDinh = rd.GetDateTime(1);
                            laPhongBan = !rd.IsDBNull(2);
                        }
                    }

                    if (ngayChi.Date < ngayQuyetDinh.Date)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Ngày chi không được trước ngày quyết định.");
                    }

                    // Mỗi quyết định thanh toán trọn một lần.
                    if (soTienChi != tienQuyetDinh)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Số tiền chi phải bằng số tiền " +
                            "trên quyết định (" +
                            tienQuyetDinh.ToString("N0") + " đồng).");
                    }

                    // Chưa có quy tắc phân bổ thưởng phòng ban
                    // tới từng nhân viên trong bảng lương.
                    if (laPhongBan && hinhThucChi == "Qua lương")
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Chưa hỗ trợ chi thưởng phòng ban " +
                            "qua lương. Hãy chọn tiền mặt hoặc chuyển khoản.");
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM PhieuChiKhenThuong WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaQDKT = @MaQD", cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaQD", maQDKT));

                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Quyết định này đã có phiếu chi.");
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO PhieuChiKhenThuong
                        (
                            MaPhieuChi, MaQDKT,
                            NgayChi, SoTienChi, HinhThucChi
                        )
                        VALUES
                        (
                            @MaPhieu, @MaQD,
                            @NgayChi, @TienChi, @HinhThuc
                        )", cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaPhieu", maPhieuChi));
                        cmd.Parameters.Add(Db.P("@MaQD", maQDKT));
                        cmd.Parameters.Add(Db.P("@NgayChi", ngayChi.Date));
                        cmd.Parameters.Add(Db.P("@TienChi", soTienChi));
                        cmd.Parameters.Add(Db.P("@HinhThuc", hinhThucChi));
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                    return KetQuaXuLy.Ok(
                        "Lập phiếu chi khen thưởng thành công.");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(
                        "Không thể lập phiếu chi: " + ex.Message);
                }
            }
        }
    }
}
