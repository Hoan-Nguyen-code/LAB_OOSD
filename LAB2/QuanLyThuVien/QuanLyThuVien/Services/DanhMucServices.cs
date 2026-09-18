using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Services
{
    public class DanhMucService
    {
        // =========================
        // NHÂN VIÊN
        // =========================

        public DataTable LayDanhSachNhanVien()
        {
            return Db.Query("SELECT * FROM NHANVIEN");
        }

        public void ThemNhanVien(
            string maNV,
            string ho,
            string ten,
            string phai,
            DateTime ngaySinh,
            string chucVu,
            string soDienThoai)
        {
            string sql = @"
                INSERT INTO NHANVIEN
                (MaNhanVien, Ho, Ten, Phai, NgaySinh, ChucVu, SoDienThoai)
                VALUES
                (@MaNV, @Ho, @Ten, @Phai, @NgaySinh, @ChucVu, @SDT)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@Ho", ho),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@Phai", phai),
                new SqlParameter("@NgaySinh", ngaySinh),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@SDT", soDienThoai)
            };

            Db.Execute(sql, parameters);
        }

        public void SuaNhanVien(
            string maNV,
            string ho,
            string ten,
            string phai,
            DateTime ngaySinh,
            string chucVu,
            string soDienThoai)
        {
            string sql = @"
                UPDATE NHANVIEN
                SET Ho = @Ho,
                    Ten = @Ten,
                    Phai = @Phai,
                    NgaySinh = @NgaySinh,
                    ChucVu = @ChucVu,
                    SoDienThoai = @SDT
                WHERE MaNhanVien = @MaNV";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNV", maNV),
                new SqlParameter("@Ho", ho),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@Phai", phai),
                new SqlParameter("@NgaySinh", ngaySinh),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@SDT", soDienThoai)
            };

            Db.Execute(sql, parameters);
        }

        public void XoaNhanVien(string maNV)
        {
            string sql =
                "DELETE FROM NHANVIEN WHERE MaNhanVien = @MaNV";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNV", maNV)
            };

            Db.Execute(sql, parameters);
        }


        // =========================
        // THỂ LOẠI
        // =========================

        public DataTable LayDanhSachTheLoai()
        {
            return Db.Query("SELECT * FROM THELOAI");
        }

        public void ThemTheLoai(string maTheLoai, string tenTheLoai)
        {
            string sql = @"
                INSERT INTO THELOAI(MaTheLoai, TenTheLoai)
                VALUES (@MaTheLoai, @TenTheLoai)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTheLoai", maTheLoai),
                new SqlParameter("@TenTheLoai", tenTheLoai)
            };

            Db.Execute(sql, parameters);
        }

        public void SuaTheLoai(string maTheLoai, string tenTheLoai)
        {
            string sql = @"
                UPDATE THELOAI
                SET TenTheLoai = @TenTheLoai
                WHERE MaTheLoai = @MaTheLoai";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTheLoai", maTheLoai),
                new SqlParameter("@TenTheLoai", tenTheLoai)
            };

            Db.Execute(sql, parameters);
        }

        public void XoaTheLoai(string maTheLoai)
        {
            string sql =
                "DELETE FROM THELOAI WHERE MaTheLoai = @MaTheLoai";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTheLoai", maTheLoai)
            };

            Db.Execute(sql, parameters);
        }


        // =========================
        // NHÀ XUẤT BẢN
        // =========================

        public DataTable LayDanhSachNhaXuatBan()
        {
            return Db.Query("SELECT * FROM NHAXUATBAN");
        }

        public void ThemNhaXuatBan(
            string maNXB,
            string diaChi,
            string soDienThoai)
        {
            string sql = @"
                INSERT INTO NHAXUATBAN
                (MaNhaXuatBan, DiaChi, SoDienThoai)
                VALUES (@MaNXB, @DiaChi, @SDT)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNXB", maNXB),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", soDienThoai)
            };

            Db.Execute(sql, parameters);
        }

        public void SuaNhaXuatBan(
            string maNXB,
            string diaChi,
            string soDienThoai)
        {
            string sql = @"
                UPDATE NHAXUATBAN
                SET DiaChi = @DiaChi,
                    SoDienThoai = @SDT
                WHERE MaNhaXuatBan = @MaNXB";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNXB", maNXB),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", soDienThoai)
            };

            Db.Execute(sql, parameters);
        }

        public void XoaNhaXuatBan(string maNXB)
        {
            string sql =
                "DELETE FROM NHAXUATBAN WHERE MaNhaXuatBan = @MaNXB";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaNXB", maNXB)
            };

            Db.Execute(sql, parameters);
        }
    }
}