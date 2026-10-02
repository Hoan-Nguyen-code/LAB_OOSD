using System.Data;
using System.Data.SqlClient;
using shopABC.Data;

namespace shopABC.Adapters
{
    /*
     * Adapter mô phỏng việc kết nối tới
     * Hệ thống quản lý sản phẩm bên ngoài.
     *
     * Trong LAB4, bảng SANPHAM và NHOMSANPHAM
     * đóng vai trò dữ liệu mô phỏng của hệ thống ngoài.
     */
    public static class ProductSystemAdapter
    {
        // =====================================
        // LẤY DANH SÁCH NHÓM SẢN PHẨM
        // =====================================
        public static DataTable LayDanhSachNhom()
        {
            string sql = @"
                SELECT
                    MaNhom,
                    TenNhom
                FROM NHOMSANPHAM
                ORDER BY TenNhom";

            return Db.Query(sql);
        }

        // =====================================
        // LẤY TẤT CẢ SẢN PHẨM
        // =====================================
        public static DataTable LayDanhSachSanPham()
        {
            string sql = @"
                SELECT
                    SP.MaSanPham,
                    SP.TenSanPham,
                    SP.NhaSanXuat,
                    SP.GiaBan,
                    SP.TinhTrang,
                    SP.MaNhom,
                    NSP.TenNhom
                FROM SANPHAM SP
                LEFT JOIN NHOMSANPHAM NSP
                    ON SP.MaNhom = NSP.MaNhom
                ORDER BY SP.MaSanPham";

            return Db.Query(sql);
        }

        // =====================================
        // LỌC SẢN PHẨM THEO NHÓM
        // =====================================
        public static DataTable LaySanPhamTheoNhom(
            string maNhom)
        {
            string sql = @"
                SELECT
                    SP.MaSanPham,
                    SP.TenSanPham,
                    SP.NhaSanXuat,
                    SP.GiaBan,
                    SP.TinhTrang,
                    SP.MaNhom,
                    NSP.TenNhom
                FROM SANPHAM SP
                LEFT JOIN NHOMSANPHAM NSP
                    ON SP.MaNhom = NSP.MaNhom
                WHERE SP.MaNhom = @MaNhom
                ORDER BY SP.MaSanPham";

            return Db.Query(
                sql,
                new SqlParameter(
                    "@MaNhom",
                    maNhom
                )
            );
        }

        // =====================================
        // TÌM KIẾM SẢN PHẨM
        // =====================================
        public static DataTable TimKiemSanPham(
            string tuKhoa)
        {
            string sql = @"
                SELECT
                    SP.MaSanPham,
                    SP.TenSanPham,
                    SP.NhaSanXuat,
                    SP.GiaBan,
                    SP.TinhTrang,
                    SP.MaNhom,
                    NSP.TenNhom
                FROM SANPHAM SP
                LEFT JOIN NHOMSANPHAM NSP
                    ON SP.MaNhom = NSP.MaNhom
                WHERE SP.MaSanPham LIKE @TuKhoa
                   OR SP.TenSanPham LIKE @TuKhoa
                   OR SP.NhaSanXuat LIKE @TuKhoa
                ORDER BY SP.MaSanPham";

            return Db.Query(
                sql,
                new SqlParameter(
                    "@TuKhoa",
                    "%" + tuKhoa + "%"
                )
            );
        }

        // =====================================
        // LẤY CHI TIẾT MỘT SẢN PHẨM
        // =====================================
        public static DataTable LayChiTietSanPham(
            string maSanPham)
        {
            string sql = @"
                SELECT
                    SP.MaSanPham,
                    SP.TenSanPham,
                    SP.NhaSanXuat,
                    SP.MoTa,
                    SP.ThongSoKyThuat,
                    SP.GiaBan,
                    SP.TinhTrang,
                    SP.HinhAnh,
                    SP.MaNhom,
                    NSP.TenNhom
                FROM SANPHAM SP
                LEFT JOIN NHOMSANPHAM NSP
                    ON SP.MaNhom = NSP.MaNhom
                WHERE SP.MaSanPham = @MaSanPham";

            return Db.Query(
                sql,
                new SqlParameter(
                    "@MaSanPham",
                    maSanPham
                )
            );
        }

        // =====================================
        // KIỂM TRA CÒN HÀNG
        // =====================================
        public static bool KiemTraConHang(
            string maSanPham)
        {
            object result = Db.Scalar(
                @"SELECT COUNT(*)
                  FROM SANPHAM
                  WHERE MaSanPham = @MaSanPham
                    AND TinhTrang = N'Còn hàng'",

                new SqlParameter(
                    "@MaSanPham",
                    maSanPham
                )
            );

            return
                result != null &&
                result != System.DBNull.Value &&
                System.Convert.ToInt32(result) > 0;
        }
    }
}