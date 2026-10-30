
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class HopDongService
    {
        public DataTable LayQuyetDinhTuyenDung()
        {
            return Db.Query(@"
                SELECT
                    td.MaQDTD,
                    td.MaNV,
                    td.MaQDTD + N' - ' + nv.HoTen
                        AS HienThi
                FROM QuyetDinhTuyenDung td
                JOIN NhanVien nv ON nv.MaNV = td.MaNV
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM HopDongLaoDong hd
                    WHERE hd.MaQDTD = td.MaQDTD
                )
                ORDER BY td.MaQDTD");
        }

        public DataTable LayDanhSachHopDong()
        {
            return Db.Query(@"
                SELECT
                    hd.MaHopDong,
                    hd.MaNV,
                    nv.HoTen,
                    hd.MaQDTD,
                    hd.LoaiHopDong,
                    hd.NgayKy,
                    hd.NgayBatDau,
                    hd.NgayKetThuc,
                    hd.NoiDungHopDong,
                    hd.TrangThai,
                    bh.MaBaoHiem,
                    bh.SoBHXH,
                    bh.SoBHYT,
                    bh.MucDongBHXH,
                    bh.MucDongBHYT,
                    bh.NgayThamGia
                FROM HopDongLaoDong hd
                JOIN NhanVien nv ON nv.MaNV = hd.MaNV
                LEFT JOIN HoSoBaoHiem bh
                    ON bh.MaHopDong = hd.MaHopDong
                ORDER BY hd.NgayKy DESC, hd.MaHopDong");
        }

        public KetQuaXuLy KyHopDong(
            string maHopDong,
            string maQDTD,
            string loaiHopDong,
            DateTime ngayKy,
            DateTime ngayBatDau,
            DateTime? ngayKetThuc,
            string noiDung,
            string maBaoHiem,
            string soBHXH,
            string soBHYT,
            decimal mucDongBHXH,
            decimal mucDongBHYT,
            DateTime ngayThamGia)
        {
            if (string.IsNullOrWhiteSpace(maHopDong) ||
                string.IsNullOrWhiteSpace(maQDTD))
            {
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã hợp đồng hoặc chọn quyết định tuyển dụng.");
            }

            if (loaiHopDong != "Ngắn hạn" &&
                loaiHopDong != "Dài hạn")
            {
                return KetQuaXuLy.Fail(
                    "Loại hợp đồng không hợp lệ.");
            }

            if (ngayBatDau.Date < ngayKy.Date)
            {
                return KetQuaXuLy.Fail(
                    "Ngày bắt đầu hợp đồng không được trước ngày ký.");
            }

            if (ngayKetThuc.HasValue &&
                ngayKetThuc.Value.Date < ngayBatDau.Date)
            {
                return KetQuaXuLy.Fail(
                    "Ngày kết thúc hợp đồng không hợp lệ.");
            }

            if (loaiHopDong == "Ngắn hạn" &&
                !ngayKetThuc.HasValue)
            {
                return KetQuaXuLy.Fail(
                    "Hợp đồng ngắn hạn phải có ngày kết thúc.");
            }

            if (loaiHopDong == "Dài hạn")
            {
                if (string.IsNullOrWhiteSpace(maBaoHiem) ||
                    string.IsNullOrWhiteSpace(soBHXH) ||
                    string.IsNullOrWhiteSpace(soBHYT))
                {
                    return KetQuaXuLy.Fail(
                        "Hợp đồng dài hạn cần nhập đầy đủ " +
                        "mã hồ sơ, số BHXH và số BHYT.");
                }

                if (mucDongBHXH < 0 || mucDongBHYT < 0)
                {
                    return KetQuaXuLy.Fail(
                        "Các khoản bảo hiểm không được âm.");
                }

                if (ngayThamGia.Date < ngayBatDau.Date)
                {
                    return KetQuaXuLy.Fail(
                        "Ngày tham gia bảo hiểm không được " +
                        "trước ngày bắt đầu hợp đồng.");
                }
            }

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr =
                cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    string maNV;
                    DateTime ngayKetThucThuViec;
                    string trangThaiNhanVien;

                    // Lấy quyết định tuyển dụng và khóa bản ghi
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT
                            td.MaNV,
                            td.NgayKetThucThuViec,
                            nv.TrangThaiLamViec
                        FROM QuyetDinhTuyenDung td WITH (UPDLOCK, HOLDLOCK)
                        JOIN NhanVien nv WITH (UPDLOCK, HOLDLOCK)
                            ON nv.MaNV = td.MaNV
                        WHERE td.MaQDTD = @MaQD",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaQD", maQDTD));

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Không tìm thấy quyết định tuyển dụng.");
                            }

                            maNV = rd.GetString(0);
                            ngayKetThucThuViec = rd.GetDateTime(1);
                            trangThaiNhanVien = rd.GetString(2);
                        }
                    }

                    if (trangThaiNhanVien == "Nghỉ việc")
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Nhân viên đã nghỉ việc.");
                    }

                    // Sau ngày kết thúc thử việc mới được ký.
                    if (ngayKy.Date <= ngayKetThucThuViec.Date)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Chưa đủ điều kiện ký hợp đồng. " +
                            "Phải kết thúc thời gian thử việc trước ngày ký.");
                    }

                    // Một quyết định tuyển dụng chỉ sinh ra một hợp đồng.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM HopDongLaoDong WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaQDTD = @MaQD",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaQD", maQDTD));

                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Quyết định tuyển dụng này đã có hợp đồng.");
                        }
                    }

                    // Không tạo hợp đồng giao thời gian với hợp đồng cũ.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM HopDongLaoDong WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaNV = @MaNV
                          AND TrangThai <> N'Chấm dứt'
                          AND @BatDau <=
                              ISNULL(NgayKetThuc, CONVERT(date,'99991231'))
                          AND ISNULL(@KetThuc, CONVERT(date,'99991231'))
                              >= NgayBatDau",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaNV", maNV));
                        cmd.Parameters.Add(Db.P("@BatDau", ngayBatDau.Date));
                        cmd.Parameters.Add(Db.P(
                            "@KetThuc",
                            ngayKetThuc.HasValue
                                ? (object)ngayKetThuc.Value.Date
                                : DBNull.Value));

                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Nhân viên đang có hợp đồng " +
                                "trùng thời gian hiệu lực.");
                        }
                    }

                    // 1. Lưu hợp đồng
                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO HopDongLaoDong
                        (
                            MaHopDong, MaNV, MaQDTD,
                            LoaiHopDong, NgayKy,
                            NgayBatDau, NgayKetThuc,
                            NoiDungHopDong, TrangThai
                        )
                        VALUES
                        (
                            @MaHD, @MaNV, @MaQD,
                            @Loai, @NgayKy,
                            @BatDau, @KetThuc,
                            @NoiDung, N'Có hiệu lực'
                        )",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaHD", maHopDong));
                        cmd.Parameters.Add(Db.P("@MaNV", maNV));
                        cmd.Parameters.Add(Db.P("@MaQD", maQDTD));
                        cmd.Parameters.Add(Db.P("@Loai", loaiHopDong));
                        cmd.Parameters.Add(Db.P("@NgayKy", ngayKy.Date));
                        cmd.Parameters.Add(Db.P("@BatDau", ngayBatDau.Date));
                        cmd.Parameters.Add(Db.P(
                            "@KetThuc",
                            ngayKetThuc.HasValue
                                ? (object)ngayKetThuc.Value.Date
                                : DBNull.Value));
                        cmd.Parameters.Add(Db.P("@NoiDung", noiDung));
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Hợp đồng dài hạn: lập hồ sơ bảo hiểm
                    if (loaiHopDong == "Dài hạn")
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO HoSoBaoHiem
                            (
                                MaBaoHiem, MaHopDong,
                                SoBHXH, SoBHYT,
                                MucDongBHXH, MucDongBHYT,
                                NgayThamGia
                            )
                            VALUES
                            (
                                @MaBH, @MaHD,
                                @BHXH, @BHYT,
                                @MucBHXH, @MucBHYT,
                                @NgayThamGia
                            )",
                            cn, tr))
                        {
                            cmd.Parameters.Add(Db.P("@MaBH", maBaoHiem));
                            cmd.Parameters.Add(Db.P("@MaHD", maHopDong));
                            cmd.Parameters.Add(Db.P("@BHXH", soBHXH));
                            cmd.Parameters.Add(Db.P("@BHYT", soBHYT));
                            cmd.Parameters.Add(Db.P("@MucBHXH", mucDongBHXH));
                            cmd.Parameters.Add(Db.P("@MucBHYT", mucDongBHYT));
                            cmd.Parameters.Add(Db.P(
                                "@NgayThamGia", ngayThamGia.Date));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 3. Chuyển trạng thái nhân viên sang chính thức
                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE NhanVien
                        SET TrangThaiLamViec = N'Chính thức'
                        WHERE MaNV = @MaNV",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaNV", maNV));
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();

                    return KetQuaXuLy.Ok(
                        loaiHopDong == "Dài hạn"
                            ? "Đã ký hợp đồng dài hạn, lập hồ sơ " +
                              "BHYT/BHXH và cập nhật nhân viên."
                            : "Đã ký hợp đồng ngắn hạn và " +
                              "cập nhật nhân viên.");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(
                        "Không thể ký hợp đồng: " + ex.Message);
                }
            }
        }
    }
}
