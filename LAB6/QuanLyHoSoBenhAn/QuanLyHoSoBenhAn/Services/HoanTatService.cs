
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class HoanTatService
    {
        public DataTable LayPhieuKham()
        {
            return Db.Query(@"
                SELECT p.MaPhieuKham,
                       p.MaPhieuKham + N' - ' + b.HoTen
                           AS TenHienThi
                FROM PhieuKham p
                INNER JOIN HoSoBenhAn h
                    ON p.MaHoSo = h.MaHoSo
                INNER JOIN BenhNhan b
                    ON h.MaBN = b.MaBN
                ORDER BY p.NgayGioKham DESC");
        }

        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT p.MaPhieuKham,
                       p.MaHoSo,
                       b.HoTen AS TenBenhNhan,
                       p.NgayGioKham,
                       p.ChanDoan,
                       p.TrangThai,
                       t.MaThanhToan,
                       t.SoTien,
                       t.NgayThanhToan,
                       t.TrangThai AS TrangThaiThanhToan
                FROM PhieuKham p
                INNER JOIN HoSoBenhAn h
                    ON p.MaHoSo = h.MaHoSo
                INNER JOIN BenhNhan b
                    ON h.MaBN = b.MaBN
                LEFT JOIN ThanhToanVienPhi t
                    ON p.MaPhieuKham = t.MaPhieuKham
                WHERE @TuKhoa = N''
                   OR p.MaPhieuKham LIKE '%' + @TuKhoa + '%'
                   OR p.MaHoSo LIKE '%' + @TuKhoa + '%'
                   OR b.HoTen LIKE N'%' + @TuKhoa + N'%'
                ORDER BY p.NgayGioKham DESC",
                new SqlParameter("@TuKhoa", tuKhoa.Trim()));
        }

        public void GhiNhanThanhToan(
            string maPhieuKham,
            decimal soTien)
        {
            KiemTraThongTin(maPhieuKham, soTien);

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    KiemTraPhieuKham(conn, tran, maPhieuKham);
                    KiemTraChuaThanhToan(conn, tran, maPhieuKham);
                    LuuThanhToan(conn, tran, maPhieuKham, soTien);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void HoanTatDieuTri(
            string maPhieuKham,
            bool daHoanTatThuTuc)
        {
            if (string.IsNullOrWhiteSpace(maPhieuKham))
                throw new Exception(
                    "Vui lòng chọn phiếu khám.");

            if (!daHoanTatThuTuc)
                throw new Exception(
                    "Chưa xác nhận hoàn tất thủ tục.");

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    KiemTraPhieuKham(conn, tran, maPhieuKham);
                    KiemTraDaThanhToan(conn, tran, maPhieuKham);
                    CapNhatTrangThai(conn, tran, maPhieuKham);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void ThanhToanVaHoanTat(
            string maPhieuKham,
            decimal soTien,
            bool daHoanTatThuTuc)
        {
            KiemTraThongTin(maPhieuKham, soTien);

            if (!daHoanTatThuTuc)
                throw new Exception(
                    "Chưa xác nhận hoàn tất thủ tục.");

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    KiemTraPhieuKham(conn, tran, maPhieuKham);
                    KiemTraChuaThanhToan(conn, tran, maPhieuKham);

                    // Hai thao tác cùng một transaction.
                    LuuThanhToan(conn, tran, maPhieuKham, soTien);
                    CapNhatTrangThai(conn, tran, maPhieuKham);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private void KiemTraThongTin(
            string maPhieuKham,
            decimal soTien)
        {
            if (string.IsNullOrWhiteSpace(maPhieuKham))
                throw new Exception(
                    "Vui lòng chọn phiếu khám.");

            if (soTien < 0)
                throw new Exception(
                    "Số tiền viện phí không được âm.");
        }

        private void KiemTraPhieuKham(
            SqlConnection conn,
            SqlTransaction tran,
            string maPhieuKham)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT TrangThai, ChanDoan
                FROM PhieuKham WITH (UPDLOCK, HOLDLOCK)
                WHERE MaPhieuKham = @MaPK",
                conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@MaPK", maPhieuKham);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        throw new Exception(
                            "Không tìm thấy phiếu khám.");

                    string trangThai =
                        reader["TrangThai"].ToString();

                    if (trangThai == "Hoàn tất")
                        throw new Exception(
                            "Phiếu khám đã hoàn tất trước đó.");

                    if (trangThai != "Chờ hoàn tất")
                        throw new Exception(
                            "Phiếu khám chưa hoàn thành khám bệnh. " +
                            "Vui lòng cập nhật chẩn đoán " +
                            "và trạng thái khám trước.");

                    if (reader["ChanDoan"] == DBNull.Value ||
                        string.IsNullOrWhiteSpace(
                            reader["ChanDoan"].ToString()))
                        throw new Exception(
                            "Phiếu khám chưa có chẩn đoán.");
                }
            }
        }

        private void KiemTraChuaThanhToan(
            SqlConnection conn,
            SqlTransaction tran,
            string maPhieuKham)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM ThanhToanVienPhi WITH (UPDLOCK, HOLDLOCK)
                WHERE MaPhieuKham = @MaPK",
                conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@MaPK", maPhieuKham);

                if ((int)cmd.ExecuteScalar() > 0)
                    throw new Exception(
                        "Phiếu khám đã có thanh toán. " +
                        "Không thể thanh toán lần thứ hai.");
            }
        }

        private void KiemTraDaThanhToan(
            SqlConnection conn,
            SqlTransaction tran,
            string maPhieuKham)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM ThanhToanVienPhi WITH (UPDLOCK, HOLDLOCK)
                WHERE MaPhieuKham = @MaPK
                  AND TrangThai = N'Đã thanh toán'",
                conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@MaPK", maPhieuKham);

                if ((int)cmd.ExecuteScalar() == 0)
                    throw new Exception(
                        "Chưa ghi nhận thanh toán viện phí.");
            }
        }

        private void LuuThanhToan(
            SqlConnection conn,
            SqlTransaction tran,
            string maPhieuKham,
            decimal soTien)
        {
            string maThanhToan = "TT" +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpper();

            using (SqlCommand cmd = new SqlCommand(@"
                INSERT INTO ThanhToanVienPhi
                    (MaThanhToan, MaPhieuKham,
                     NgayThanhToan, SoTien, TrangThai)
                VALUES
                    (@MaTT, @MaPK,
                     SYSDATETIME(), @SoTien, N'Đã thanh toán')",
                conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@MaTT", maThanhToan);

                cmd.Parameters.AddWithValue(
                    "@MaPK", maPhieuKham);

                cmd.Parameters.AddWithValue(
                    "@SoTien", soTien);

                cmd.ExecuteNonQuery();
            }
        }

        private void CapNhatTrangThai(
            SqlConnection conn,
            SqlTransaction tran,
            string maPhieuKham)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                UPDATE PhieuKham
                SET TrangThai = N'Hoàn tất'
                WHERE MaPhieuKham = @MaPK
                  AND TrangThai = N'Chờ hoàn tất'",
                conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@MaPK", maPhieuKham);

                if (cmd.ExecuteNonQuery() != 1)
                    throw new Exception(
                        "Không thể cập nhật trạng thái điều trị.");
            }
        }
    }
}
