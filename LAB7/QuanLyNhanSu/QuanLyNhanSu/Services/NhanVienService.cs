using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class NhanVienService
    {
        // ==================== NHÂN VIÊN ====================

        public DataTable LayNhanVien(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    nv.MaNV,
                    nv.HoTen,
                    nv.NgaySinh,
                    nv.GioiTinh,
                    nv.DiaChi,
                    nv.DienThoai,
                    nv.TrangThaiLamViec
                FROM NhanVien nv
                WHERE
                    ISNULL(@TuKhoa, N'') = N''
                    OR nv.MaNV LIKE N'%' + @TuKhoa + N'%'
                    OR nv.HoTen LIKE N'%' + @TuKhoa + N'%'
                ORDER BY nv.MaNV",
                Db.P("@TuKhoa", tuKhoa ?? ""));
        }

        public KetQuaXuLy ThemNhanVien(
            string maNV,
            string hoTen,
            DateTime ngaySinh,
            string gioiTinh,
            string diaChi,
            string dienThoai,
            string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(hoTen))
            {
                return KetQuaXuLy.Fail(
                    "Mã nhân viên và họ tên không được để trống.");
            }

            if (ngaySinh.Date > DateTime.Today)
            {
                return KetQuaXuLy.Fail(
                    "Ngày sinh không được lớn hơn ngày hiện tại.");
            }

            try
            {
                object tonTai = Db.Scalar(
                    "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV",
                    Db.P("@MaNV", maNV));

                if (Convert.ToInt32(tonTai) > 0)
                {
                    return KetQuaXuLy.Fail(
                        "Mã nhân viên đã tồn tại.");
                }

                Db.Execute(@"
                    INSERT INTO NhanVien
                    (
                        MaNV, HoTen, NgaySinh, GioiTinh,
                        DiaChi, DienThoai, TrangThaiLamViec
                    )
                    VALUES
                    (
                        @MaNV, @HoTen, @NgaySinh, @GioiTinh,
                        @DiaChi, @DienThoai, @TrangThai
                    )",
                    Db.P("@MaNV", maNV),
                    Db.P("@HoTen", hoTen),
                    Db.P("@NgaySinh", ngaySinh.Date),
                    Db.P("@GioiTinh", gioiTinh),
                    Db.P("@DiaChi", diaChi),
                    Db.P("@DienThoai", dienThoai),
                    Db.P("@TrangThai", trangThai));

                return KetQuaXuLy.Ok(
                    "Thêm nhân viên thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        public KetQuaXuLy CapNhatNhanVien(
            string maNV,
            string hoTen,
            DateTime ngaySinh,
            string gioiTinh,
            string diaChi,
            string dienThoai,
            string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(hoTen))
            {
                return KetQuaXuLy.Fail(
                    "Mã nhân viên và họ tên không được để trống.");
            }

            if (ngaySinh.Date > DateTime.Today)
            {
                return KetQuaXuLy.Fail(
                    "Ngày sinh không hợp lệ.");
            }

            try
            {
                int soDong = Db.Execute(@"
                    UPDATE NhanVien
                    SET
                        HoTen = @HoTen,
                        NgaySinh = @NgaySinh,
                        GioiTinh = @GioiTinh,
                        DiaChi = @DiaChi,
                        DienThoai = @DienThoai,
                        TrangThaiLamViec = @TrangThai
                    WHERE MaNV = @MaNV",
                    Db.P("@MaNV", maNV),
                    Db.P("@HoTen", hoTen),
                    Db.P("@NgaySinh", ngaySinh.Date),
                    Db.P("@GioiTinh", gioiTinh),
                    Db.P("@DiaChi", diaChi),
                    Db.P("@DienThoai", dienThoai),
                    Db.P("@TrangThai", trangThai));

                return soDong > 0
                    ? KetQuaXuLy.Ok("Cập nhật nhân viên thành công.")
                    : KetQuaXuLy.Fail("Không tìm thấy nhân viên.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // ==================== PHÒNG BAN ====================

        public DataTable LayPhongBan(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT MaPhongBan, TenPhongBan, MoTa
                FROM PhongBan
                WHERE
                    ISNULL(@TuKhoa, N'') = N''
                    OR MaPhongBan LIKE N'%' + @TuKhoa + N'%'
                    OR TenPhongBan LIKE N'%' + @TuKhoa + N'%'
                ORDER BY MaPhongBan",
                Db.P("@TuKhoa", tuKhoa ?? ""));
        }

        public KetQuaXuLy ThemPhongBan(
            string maPhongBan,
            string tenPhongBan,
            string moTa)
        {
            if (string.IsNullOrWhiteSpace(maPhongBan) ||
                string.IsNullOrWhiteSpace(tenPhongBan))
            {
                return KetQuaXuLy.Fail(
                    "Mã và tên phòng ban không được để trống.");
            }

            try
            {
                Db.Execute(@"
                    INSERT INTO PhongBan
                    (MaPhongBan, TenPhongBan, MoTa)
                    VALUES
                    (@Ma, @Ten, @MoTa)",
                    Db.P("@Ma", maPhongBan),
                    Db.P("@Ten", tenPhongBan),
                    Db.P("@MoTa", moTa));

                return KetQuaXuLy.Ok(
                    "Thêm phòng ban thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        public KetQuaXuLy CapNhatPhongBan(
            string maPhongBan,
            string tenPhongBan,
            string moTa)
        {
            if (string.IsNullOrWhiteSpace(maPhongBan) ||
                string.IsNullOrWhiteSpace(tenPhongBan))
            {
                return KetQuaXuLy.Fail(
                    "Mã và tên phòng ban không được để trống.");
            }

            try
            {
                int soDong = Db.Execute(@"
                    UPDATE PhongBan
                    SET
                        TenPhongBan = @Ten,
                        MoTa = @MoTa
                    WHERE MaPhongBan = @Ma",
                    Db.P("@Ma", maPhongBan),
                    Db.P("@Ten", tenPhongBan),
                    Db.P("@MoTa", moTa));

                return soDong > 0
                    ? KetQuaXuLy.Ok("Cập nhật phòng ban thành công.")
                    : KetQuaXuLy.Fail("Không tìm thấy phòng ban.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }
    }
}