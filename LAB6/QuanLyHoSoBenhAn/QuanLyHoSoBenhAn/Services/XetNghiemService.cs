
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class XetNghiemService
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
                WHERE p.TrangThai <> N'Hoàn tất'
                ORDER BY p.NgayGioKham DESC");
        }

        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT x.MaXN,
                       x.MaPhieuKham,
                       b.HoTen AS TenBenhNhan,
                       x.TenXetNghiem,
                       x.NgayChiDinh,
                       x.KetQua,
                       x.NgayCoKetQua
                FROM XetNghiem x
                INNER JOIN PhieuKham p
                    ON x.MaPhieuKham = p.MaPhieuKham
                INNER JOIN HoSoBenhAn h
                    ON p.MaHoSo = h.MaHoSo
                INNER JOIN BenhNhan b
                    ON h.MaBN = b.MaBN
                WHERE @TuKhoa = N''
                   OR x.MaXN LIKE '%' + @TuKhoa + '%'
                   OR x.MaPhieuKham LIKE '%' + @TuKhoa + '%'
                   OR b.HoTen LIKE N'%' + @TuKhoa + N'%'
                   OR x.TenXetNghiem LIKE
                        N'%' + @TuKhoa + N'%'
                ORDER BY x.NgayChiDinh DESC",
                new SqlParameter(
                    "@TuKhoa", tuKhoa.Trim()));
        }

        public string ChiDinhXetNghiem(
            string maPhieuKham,
            string tenXetNghiem)
        {
            if (string.IsNullOrWhiteSpace(maPhieuKham))
                throw new Exception(
                    "Vui lòng chọn phiếu khám.");

            if (string.IsNullOrWhiteSpace(tenXetNghiem))
                throw new Exception(
                    "Vui lòng nhập tên xét nghiệm.");

            if (tenXetNghiem.Trim().Length > 160)
                throw new Exception(
                    "Tên xét nghiệm không vượt quá 160 ký tự.");

            string maXN = "XN" +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpper();

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    KiemTraPhieuKham(
                        conn, tran, maPhieuKham);

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO XetNghiem
                            (MaXN, MaPhieuKham,
                             TenXetNghiem, NgayChiDinh)
                        VALUES
                            (@MaXN, @MaPK,
                             @TenXN, SYSDATETIME())",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaXN", maXN);

                        cmd.Parameters.AddWithValue(
                            "@MaPK", maPhieuKham);

                        cmd.Parameters.AddWithValue(
                            "@TenXN", tenXetNghiem.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                    return maXN;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void GhiKetQua(
            string maXN,
            string ketQua)
        {
            if (string.IsNullOrWhiteSpace(maXN))
                throw new Exception(
                    "Vui lòng chọn xét nghiệm cần ghi kết quả.");

            if (string.IsNullOrWhiteSpace(ketQua))
                throw new Exception(
                    "Vui lòng nhập kết quả xét nghiệm.");

            if (ketQua.Trim().Length > 1000)
                throw new Exception(
                    "Kết quả không vượt quá 1000 ký tự.");

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    string maPhieuKham;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT MaPhieuKham
                        FROM XetNghiem WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaXN = @MaXN",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaXN", maXN);

                        object result = cmd.ExecuteScalar();

                        if (result == null)
                            throw new Exception(
                                "Không tìm thấy xét nghiệm.");

                        maPhieuKham = result.ToString();
                    }

                    KiemTraPhieuKham(
                        conn, tran, maPhieuKham);

                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE XetNghiem
                        SET KetQua = @KetQua,
                            NgayCoKetQua = SYSDATETIME()
                        WHERE MaXN = @MaXN",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaXN", maXN);

                        cmd.Parameters.AddWithValue(
                            "@KetQua", ketQua.Trim());

                        if (cmd.ExecuteNonQuery() != 1)
                            throw new Exception(
                                "Không thể cập nhật kết quả.");
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

        private void KiemTraPhieuKham(
            SqlConnection conn,
            SqlTransaction tran,
            string maPhieuKham)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT TrangThai
                FROM PhieuKham WITH (UPDLOCK, HOLDLOCK)
                WHERE MaPhieuKham = @MaPK",
                conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@MaPK", maPhieuKham);

                object result = cmd.ExecuteScalar();

                if (result == null)
                    throw new Exception(
                        "Phiếu khám không tồn tại.");

                if (result.ToString() == "Hoàn tất")
                    throw new Exception(
                        "Phiếu khám đã hoàn tất, " +
                        "không được thay đổi xét nghiệm.");
            }
        }
    }
}
