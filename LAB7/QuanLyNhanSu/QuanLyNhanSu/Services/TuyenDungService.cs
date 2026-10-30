
using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class TuyenDungService
    {
        public DataTable LayNhanVien()
        {
            return Db.Query(@"
                SELECT MaNV,
                       MaNV + N' - ' + HoTen AS HienThi
                FROM NhanVien
                WHERE TrangThaiLamViec = N'Thử việc'
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

        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    td.MaQDTD,
                    td.MaNV,
                    nv.HoTen,
                    td.MaPhongBan,
                    pb.TenPhongBan,
                    td.NgayQuyetDinh,
                    td.NgayBatDauThuViec,
                    td.NgayKetThucThuViec,
                    td.LuongThuViec,
                    td.NoiDungDieuKhoan,
                    CASE
                        WHEN hd.MaHopDong IS NOT NULL
                            THEN N'Đã ký hợp đồng'
                        WHEN td.NgayKetThucThuViec
                             < CAST(GETDATE() AS DATE)
                            THEN N'Đã hết thời gian thử việc'
                        WHEN td.NgayBatDauThuViec
                             > CAST(GETDATE() AS DATE)
                            THEN N'Chưa bắt đầu'
                        ELSE N'Đang thử việc'
                    END AS TinhTrang
                FROM QuyetDinhTuyenDung td
                JOIN NhanVien nv ON td.MaNV = nv.MaNV
                JOIN PhongBan pb
                    ON td.MaPhongBan = pb.MaPhongBan
                LEFT JOIN HopDongLaoDong hd
                    ON td.MaQDTD = hd.MaQDTD
                WHERE
                    ISNULL(@TuKhoa, N'') = N''
                    OR td.MaQDTD LIKE N'%' + @TuKhoa + N'%'
                    OR td.MaNV LIKE N'%' + @TuKhoa + N'%'
                    OR nv.HoTen LIKE N'%' + @TuKhoa + N'%'
                    OR pb.TenPhongBan LIKE N'%' + @TuKhoa + N'%'
                ORDER BY td.NgayQuyetDinh DESC,
                         td.MaQDTD",
                Db.P("@TuKhoa", tuKhoa));
        }

        public KetQuaXuLy LapQuyetDinh(
            string maQD,
            string maNV,
            string maPhongBan,
            DateTime ngayQuyetDinh,
            DateTime ngayBatDau,
            DateTime ngayKetThuc,
            decimal luongThuViec,
            string dieuKhoan)
        {
            string loi = KiemTraThongTin(
                maQD, maNV, maPhongBan,
                ngayQuyetDinh, ngayBatDau,
                ngayKetThuc, luongThuViec, dieuKhoan);

            if (loi != null)
                return KetQuaXuLy.Fail(loi);

            try
            {
                // Chỉ tiếp nhận nhân viên đang thử việc.
                object trangThai = Db.Scalar(@"
                    SELECT TrangThaiLamViec
                    FROM NhanVien
                    WHERE MaNV = @MaNV",
                    Db.P("@MaNV", maNV));

                if (trangThai == null)
                    return KetQuaXuLy.Fail(
                        "Nhân viên không tồn tại.");

                if (Convert.ToString(trangThai) != "Thử việc")
                    return KetQuaXuLy.Fail(
                        "Chỉ lập quyết định tuyển dụng cho " +
                        "nhân viên ở trạng thái Thử việc.");

                // Tránh tạo hai quyết định thử việc đang
                // chồng lấn thời gian cho cùng nhân viên.
                int trung = Convert.ToInt32(Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM QuyetDinhTuyenDung
                    WHERE MaNV = @MaNV
                      AND @BatDau <= NgayKetThucThuViec
                      AND @KetThuc >= NgayBatDauThuViec",
                    Db.P("@MaNV", maNV),
                    Db.P("@BatDau", ngayBatDau.Date),
                    Db.P("@KetThuc", ngayKetThuc.Date)));

                if (trung > 0)
                    return KetQuaXuLy.Fail(
                        "Nhân viên đã có quyết định thử việc " +
                        "trùng thời gian.");

                Db.Execute(@"
                    INSERT INTO QuyetDinhTuyenDung
                    (
                        MaQDTD, MaNV, MaPhongBan,
                        NgayQuyetDinh,
                        NgayBatDauThuViec,
                        NgayKetThucThuViec,
                        LuongThuViec, NoiDungDieuKhoan
                    )
                    VALUES
                    (
                        @MaQD, @MaNV, @MaPhongBan,
                        @NgayQD, @BatDau, @KetThuc,
                        @Luong, @DieuKhoan
                    )",
                    Db.P("@MaQD", maQD),
                    Db.P("@MaNV", maNV),
                    Db.P("@MaPhongBan", maPhongBan),
                    Db.P("@NgayQD", ngayQuyetDinh.Date),
                    Db.P("@BatDau", ngayBatDau.Date),
                    Db.P("@KetThuc", ngayKetThuc.Date),
                    Db.P("@Luong", luongThuViec),
                    Db.P("@DieuKhoan", dieuKhoan));

                return KetQuaXuLy.Ok(
                    "Đã lập quyết định tuyển dụng " +
                    maQD + " thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        public KetQuaXuLy CapNhatQuyetDinh(
            string maQD,
            string maNV,
            string maPhongBan,
            DateTime ngayQuyetDinh,
            DateTime ngayBatDau,
            DateTime ngayKetThuc,
            decimal luongThuViec,
            string dieuKhoan)
        {
            string loi = KiemTraThongTin(
                maQD, maNV, maPhongBan,
                ngayQuyetDinh, ngayBatDau,
                ngayKetThuc, luongThuViec, dieuKhoan);

            if (loi != null)
                return KetQuaXuLy.Fail(loi);

            try
            {
                // Quyết định đã dùng để ký hợp đồng
                // không được sửa lại thông tin thử việc.
                int daKy = Convert.ToInt32(Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM HopDongLaoDong
                    WHERE MaQDTD = @MaQD",
                    Db.P("@MaQD", maQD)));

                if (daKy > 0)
                    return KetQuaXuLy.Fail(
                        "Quyết định đã có hợp đồng lao động, " +
                        "không được cập nhật.");

                int trung = Convert.ToInt32(Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM QuyetDinhTuyenDung
                    WHERE MaNV = @MaNV
                      AND MaQDTD <> @MaQD
                      AND @BatDau <= NgayKetThucThuViec
                      AND @KetThuc >= NgayBatDauThuViec",
                    Db.P("@MaNV", maNV),
                    Db.P("@MaQD", maQD),
                    Db.P("@BatDau", ngayBatDau.Date),
                    Db.P("@KetThuc", ngayKetThuc.Date)));

                if (trung > 0)
                    return KetQuaXuLy.Fail(
                        "Thời gian thử việc bị trùng " +
                        "với quyết định khác.");

                int soDong = Db.Execute(@"
                    UPDATE QuyetDinhTuyenDung
                    SET
                        MaPhongBan = @MaPhongBan,
                        NgayQuyetDinh = @NgayQD,
                        NgayBatDauThuViec = @BatDau,
                        NgayKetThucThuViec = @KetThuc,
                        LuongThuViec = @Luong,
                        NoiDungDieuKhoan = @DieuKhoan
                    WHERE MaQDTD = @MaQD
                      AND MaNV = @MaNV
                      AND NOT EXISTS
                      (
                          SELECT 1
                          FROM HopDongLaoDong
                          WHERE MaQDTD = @MaQD
                      )",
                    Db.P("@MaQD", maQD),
                    Db.P("@MaNV", maNV),
                    Db.P("@MaPhongBan", maPhongBan),
                    Db.P("@NgayQD", ngayQuyetDinh.Date),
                    Db.P("@BatDau", ngayBatDau.Date),
                    Db.P("@KetThuc", ngayKetThuc.Date),
                    Db.P("@Luong", luongThuViec),
                    Db.P("@DieuKhoan", dieuKhoan));

                return soDong > 0
                    ? KetQuaXuLy.Ok(
                        "Cập nhật quyết định thành công.")
                    : KetQuaXuLy.Fail(
                        "Không tìm thấy quyết định hoặc " +
                        "mã nhân viên không khớp.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        private string KiemTraThongTin(
            string maQD,
            string maNV,
            string maPhongBan,
            DateTime ngayQuyetDinh,
            DateTime ngayBatDau,
            DateTime ngayKetThuc,
            decimal luongThuViec,
            string dieuKhoan)
        {
            if (string.IsNullOrWhiteSpace(maQD) ||
                string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(maPhongBan))
                return "Chưa nhập đủ mã quyết định, " +
                       "nhân viên và phòng ban.";

            if (string.IsNullOrWhiteSpace(dieuKhoan))
                return "Điều khoản thử việc không được để trống.";

            if (ngayKetThuc.Date < ngayBatDau.Date)
                return "Ngày kết thúc thử việc phải " +
                       "từ ngày bắt đầu trở đi.";

            if (ngayBatDau.Date < ngayQuyetDinh.Date)
                return "Ngày bắt đầu thử việc không được " +
                       "trước ngày quyết định.";

            if (luongThuViec < 0)
                return "Lương thử việc không được âm.";

            return null;
        }
    }
}
