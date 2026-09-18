using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Services
{
    public class SachService
    {
        // Lấy danh sách đầu sách
        public DataTable LayDanhSach()
        {
            string sql = @"
                SELECT 
                    ds.MaDauSach,
                    ds.TenSach,
                    ds.NamXuatBan,
                    ds.SoLuongHienCo,
                    ds.MaTheLoai,
                    tl.TenTheLoai,
                    ds.MaNhaXuatBan
                FROM DAUSACH ds
                LEFT JOIN THELOAI tl
                    ON ds.MaTheLoai = tl.MaTheLoai
                ORDER BY ds.MaDauSach";

            return Db.Query(sql);
        }

        // Load thể loại cho ComboBox
        public DataTable LayTheLoai()
        {
            return Db.Query(
                "SELECT MaTheLoai, TenTheLoai FROM THELOAI ORDER BY TenTheLoai");
        }

        // Load nhà xuất bản cho ComboBox
        public DataTable LayNhaXuatBan()
        {
            return Db.Query(
                "SELECT MaNhaXuatBan FROM NHAXUATBAN ORDER BY MaNhaXuatBan");
        }

        // Thêm đầu sách
        public void ThemSach(
            string maSach,
            string tenSach,
            int namXuatBan,
            int soLuong,
            string maTheLoai,
            string maNXB)
        {
            if (soLuong < 0)
                throw new Exception("Số lượng hiện có không được âm.");

            string sql = @"
                INSERT INTO DAUSACH
                (MaDauSach, TenSach, NamXuatBan, SoLuongHienCo,
                 MaTheLoai, MaNhaXuatBan)
                VALUES
                (@MaSach, @TenSach, @NamXB, @SoLuong,
                 @MaTheLoai, @MaNXB)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaSach", maSach),
                new SqlParameter("@TenSach", tenSach),
                new SqlParameter("@NamXB", namXuatBan),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@MaTheLoai", maTheLoai),
                new SqlParameter("@MaNXB", maNXB)
            };

            Db.Execute(sql, parameters);
        }

        // Sửa đầu sách
        public void SuaSach(
            string maSach,
            string tenSach,
            int namXuatBan,
            int soLuong,
            string maTheLoai,
            string maNXB)
        {
            if (soLuong < 0)
                throw new Exception("Số lượng hiện có không được âm.");

            string sql = @"
                UPDATE DAUSACH
                SET TenSach = @TenSach,
                    NamXuatBan = @NamXB,
                    SoLuongHienCo = @SoLuong,
                    MaTheLoai = @MaTheLoai,
                    MaNhaXuatBan = @MaNXB
                WHERE MaDauSach = @MaSach";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaSach", maSach),
                new SqlParameter("@TenSach", tenSach),
                new SqlParameter("@NamXB", namXuatBan),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@MaTheLoai", maTheLoai),
                new SqlParameter("@MaNXB", maNXB)
            };

            Db.Execute(sql, parameters);
        }

        // Xóa đầu sách
        public void XoaSach(string maSach)
        {
            string sql =
                "DELETE FROM DAUSACH WHERE MaDauSach = @MaSach";

            Db.Execute(
                sql,
                new SqlParameter("@MaSach", maSach));
        }

        // Tìm theo mã hoặc tên sách
        public DataTable TimSach(string tuKhoa)
        {
            string sql = @"
                SELECT
                    ds.MaDauSach,
                    ds.TenSach,
                    ds.NamXuatBan,
                    ds.SoLuongHienCo,
                    ds.MaTheLoai,
                    tl.TenTheLoai,
                    ds.MaNhaXuatBan
                FROM DAUSACH ds
                LEFT JOIN THELOAI tl
                    ON ds.MaTheLoai = tl.MaTheLoai
                WHERE ds.MaDauSach LIKE @TuKhoa
                   OR ds.TenSach LIKE @TuKhoa
                ORDER BY ds.MaDauSach";

            return Db.Query(
                sql,
                new SqlParameter(
                    "@TuKhoa",
                    "%" + tuKhoa + "%"));
        }
    }
}