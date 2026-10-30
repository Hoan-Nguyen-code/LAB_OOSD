
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class LichKhamService
    {
        public DataTable LayHoSo()
        {
            return Db.Query(@"
                SELECT h.MaHoSo,
                       b.HoTen + N' - ' + h.MaHoSo
                           AS TenHienThi
                FROM HoSoBenhAn h
                INNER JOIN BenhNhan b
                    ON h.MaBN = b.MaBN
                ORDER BY b.HoTen");
        }

        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT l.MaLich,
                       l.MaHoSo,
                       b.HoTen AS TenBenhNhan,
                       l.NgayHen,
                       YEAR(l.NgayHen) AS Nam,
                       DATEPART(QUARTER, l.NgayHen) AS Quy,
                       l.TrangThai
                FROM LichKhamDinhKy l
                INNER JOIN HoSoBenhAn h
                    ON l.MaHoSo = h.MaHoSo
                INNER JOIN BenhNhan b
                    ON h.MaBN = b.MaBN
                WHERE @TuKhoa = N''
                   OR l.MaLich LIKE '%' + @TuKhoa + '%'
                   OR l.MaHoSo LIKE '%' + @TuKhoa + '%'
                   OR b.HoTen LIKE N'%' + @TuKhoa + N'%'
                ORDER BY l.NgayHen DESC",
                new SqlParameter("@TuKhoa", tuKhoa.Trim()));
        }

        public string DatLich(string maHoSo, DateTime ngayHen)
        {
            if (string.IsNullOrWhiteSpace(maHoSo))
                throw new Exception(
                    "Vui lòng chọn hồ sơ bệnh án.");

            if (ngayHen <= DateTime.Now)
                throw new Exception(
                    "Ngày giờ hẹn phải lớn hơn hiện tại.");

            string maLich = "LICH" +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpper();

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    // Khóa hồ sơ để kiểm tra quyền lợi.
                    string maBN;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT MaBN
                        FROM HoSoBenhAn WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaHoSo = @MaHoSo
                          AND TrangThai = N'Đang sử dụng'",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaHoSo", maHoSo);

                        object result = cmd.ExecuteScalar();

                        if (result == null)
                            throw new Exception(
                                "Hồ sơ không tồn tại hoặc không còn sử dụng.");

                        maBN = result.ToString();
                    }

                    // Kiểm tra lệ phí năm còn hiệu lực vào ngày hẹn.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM LePhiNam WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaBN = @MaBN
                          AND NgayBatDau <= @NgayHen
                          AND NgayHetHan > @NgayHen",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaBN", maBN);

                        cmd.Parameters.AddWithValue(
                            "@NgayHen", ngayHen.Date);

                        if ((int)cmd.ExecuteScalar() == 0)
                        {
                            throw new Exception(
                                "Lệ phí năm không còn hiệu lực " +
                                "tại ngày hẹn khám.");
                        }
                    }

                    // Mỗi hồ sơ chỉ có một lịch đang hoạt động
                    // trong cùng quý.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM LichKhamDinhKy
                             WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaHoSo = @MaHoSo
                          AND YEAR(NgayHen) = @Nam
                          AND DATEPART(QUARTER, NgayHen) = @Quy
                          AND TrangThai IN
                              (N'Đã đặt', N'Đã khám')",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaHoSo", maHoSo);

                        cmd.Parameters.AddWithValue(
                            "@Nam", ngayHen.Year);

                        cmd.Parameters.AddWithValue(
                            "@Quy", (ngayHen.Month - 1) / 3 + 1);

                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                            throw new Exception(
                                "Hồ sơ đã có lịch khám định kỳ " +
                                "trong quý này.");
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO LichKhamDinhKy
                            (MaLich, MaHoSo, NgayHen, TrangThai)
                        VALUES
                            (@MaLich, @MaHoSo,
                             @NgayHen, N'Đã đặt')",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaLich", maLich);

                        cmd.Parameters.AddWithValue(
                            "@MaHoSo", maHoSo);

                        cmd.Parameters.AddWithValue(
                            "@NgayHen", ngayHen);

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    return maLich;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void XacNhanDaKham(string maLich)
        {
            if (string.IsNullOrWhiteSpace(maLich))
                throw new Exception(
                    "Vui lòng chọn lịch khám.");

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    // Do lược đồ hiện tại chưa có MaLich
                    // trong PhieuKham nên đối chiếu
                    // hồ sơ, loại khám và ngày khám.
                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE LichKhamDinhKy
                        SET TrangThai = N'Đã khám'
                        WHERE MaLich = @MaLich
                          AND TrangThai = N'Đã đặt'
                          AND EXISTS (
                              SELECT 1
                              FROM PhieuKham p
                              WHERE p.MaHoSo =
                                    LichKhamDinhKy.MaHoSo
                                AND p.LoaiKham = 'DINHKY'
                                AND CAST(p.NgayGioKham AS DATE) =
                                    CAST(
                                      LichKhamDinhKy.NgayHen AS DATE)
                          )",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaLich", maLich);

                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            throw new Exception(
                                "Chưa có phiếu khám định kỳ " +
                                "đúng ngày hẹn, hoặc lịch " +
                                "không ở trạng thái Đã đặt.");
                        }
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void HuyLich(string maLich)
        {
            if (string.IsNullOrWhiteSpace(maLich))
                throw new Exception(
                    "Vui lòng chọn lịch khám cần hủy.");

            int rows = Db.Execute(@"
                UPDATE LichKhamDinhKy
                SET TrangThai = N'Đã hủy'
                WHERE MaLich = @MaLich
                  AND TrangThai = N'Đã đặt'",
                new SqlParameter("@MaLich", maLich));

            if (rows == 0)
            {
                throw new Exception(
                    "Không tìm thấy lịch hoặc lịch " +
                    "không còn ở trạng thái Đã đặt.");
            }
        }
    }
}
