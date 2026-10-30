
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class BenhNhanService
    {
        public DataTable LayDanhSach(string tuKhoa = "")
        {
            const string sql = @"
                SELECT MaBN, HoTen, NgaySinh,
                       DiaChi, DienThoai, Anh3x4
                FROM BenhNhan
                WHERE @TuKhoa = N''
                   OR MaBN LIKE N'%' + @TuKhoa + N'%'
                   OR HoTen LIKE N'%' + @TuKhoa + N'%'
                   OR DienThoai LIKE '%' + @TuKhoa + '%'
                ORDER BY MaBN";

            return Db.Query(sql,
                new SqlParameter("@TuKhoa", tuKhoa.Trim()));
        }

        public void Them(
            string maBN, string hoTen, DateTime ngaySinh,
            string diaChi, string dienThoai, string anh)
        {
            KiemTra(maBN, hoTen, ngaySinh, diaChi, dienThoai);

            const string sql = @"
                INSERT INTO BenhNhan
                    (MaBN, HoTen, NgaySinh, DiaChi,
                     DienThoai, Anh3x4)
                VALUES
                    (@MaBN, @HoTen, @NgaySinh, @DiaChi,
                     @DienThoai, @Anh3x4)";

            Db.Execute(sql,
                new SqlParameter("@MaBN", maBN.Trim()),
                new SqlParameter("@HoTen", hoTen.Trim()),
                new SqlParameter("@NgaySinh", ngaySinh.Date),
                new SqlParameter("@DiaChi", diaChi.Trim()),
                new SqlParameter("@DienThoai", dienThoai.Trim()),
                new SqlParameter("@Anh3x4",
                    string.IsNullOrWhiteSpace(anh)
                    ? (object)DBNull.Value : anh.Trim()));
        }

        public void Sua(
            string maBN, string hoTen, DateTime ngaySinh,
            string diaChi, string dienThoai, string anh)
        {
            KiemTra(maBN, hoTen, ngaySinh, diaChi, dienThoai);

            const string sql = @"
                UPDATE BenhNhan
                SET HoTen = @HoTen,
                    NgaySinh = @NgaySinh,
                    DiaChi = @DiaChi,
                    DienThoai = @DienThoai,
                    Anh3x4 = @Anh3x4
                WHERE MaBN = @MaBN";

            int count = Db.Execute(sql,
                new SqlParameter("@MaBN", maBN.Trim()),
                new SqlParameter("@HoTen", hoTen.Trim()),
                new SqlParameter("@NgaySinh", ngaySinh.Date),
                new SqlParameter("@DiaChi", diaChi.Trim()),
                new SqlParameter("@DienThoai", dienThoai.Trim()),
                new SqlParameter("@Anh3x4",
                    string.IsNullOrWhiteSpace(anh)
                    ? (object)DBNull.Value : anh.Trim()));

            if (count == 0)
                throw new Exception("Không tìm thấy bệnh nhân.");
        }

        private void KiemTra(
            string maBN, string hoTen, DateTime ngaySinh,
            string diaChi, string dienThoai)
        {
            if (string.IsNullOrWhiteSpace(maBN) ||
                string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(dienThoai))
                throw new Exception(
                    "Vui lòng nhập đầy đủ thông tin bắt buộc.");

            if (maBN.Length > 20 || dienThoai.Length > 20)
                throw new Exception(
                    "Mã bệnh nhân hoặc điện thoại quá 20 ký tự.");

            if (hoTen.Length > 120 || diaChi.Length > 250)
                throw new Exception("Thông tin vượt độ dài cho phép.");

            if (ngaySinh.Date > DateTime.Today)
                throw new Exception(
                    "Ngày sinh không được lớn hơn ngày hiện tại.");
        }
    }
}
