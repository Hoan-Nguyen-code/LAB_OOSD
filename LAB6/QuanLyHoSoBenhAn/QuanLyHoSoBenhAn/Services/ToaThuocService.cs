
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class ToaThuocService
    {
        // Danh sách các phiếu khám có thể lập toa
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
                  AND NOT EXISTS (
                      SELECT 1
                      FROM ToaThuoc t
                      WHERE t.MaPhieuKham = p.MaPhieuKham
                  )
                ORDER BY p.NgayGioKham DESC");
        }

        // Danh mục thuốc
        public DataTable LayThuoc()
        {
            return Db.Query(@"
                SELECT MaThuoc, TenThuoc, DonViTinh,
                       HamLuong
                FROM Thuoc
                ORDER BY TenThuoc");
        }

        // Danh sách toa thuốc đã lập
        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT t.MaToa,
                       t.MaPhieuKham,
                       b.HoTen AS TenBenhNhan,
                       t.NgayKe,
                       t.LoiDan
                FROM ToaThuoc t
                INNER JOIN PhieuKham p
                    ON t.MaPhieuKham = p.MaPhieuKham
                INNER JOIN HoSoBenhAn h
                    ON p.MaHoSo = h.MaHoSo
                INNER JOIN BenhNhan b
                    ON h.MaBN = b.MaBN
                WHERE @TuKhoa = N''
                   OR t.MaToa LIKE '%' + @TuKhoa + '%'
                   OR t.MaPhieuKham LIKE '%' + @TuKhoa + '%'
                   OR b.HoTen LIKE N'%' + @TuKhoa + N'%'
                ORDER BY t.NgayKe DESC",
                new SqlParameter("@TuKhoa", tuKhoa.Trim()));
        }

        // Chi tiết một toa thuốc
        public DataTable LayChiTiet(string maToa)
        {
            return Db.Query(@"
                SELECT ct.MaThuoc,
                       th.TenThuoc,
                       th.HamLuong,
                       th.DonViTinh,
                       ct.SoLuong,
                       ct.LieuDung,
                       ct.CachDung
                FROM ChiTietToaThuoc ct
                INNER JOIN Thuoc th
                    ON ct.MaThuoc = th.MaThuoc
                WHERE ct.MaToa = @MaToa
                ORDER BY th.TenThuoc",
                new SqlParameter("@MaToa", maToa));
        }

        // Tạo DataTable chứa thuốc của toa đang lập
        public DataTable TaoBangChiTiet()
        {
            DataTable table = new DataTable();

            table.Columns.Add("MaThuoc", typeof(string));
            table.Columns.Add("TenThuoc", typeof(string));
            table.Columns.Add("SoLuong", typeof(int));
            table.Columns.Add("LieuDung", typeof(string));
            table.Columns.Add("CachDung", typeof(string));

            return table;
        }

        // Kiểm tra và thêm thuốc vào danh sách tạm
        public void ThemThuocVaoDanhSach(
            DataTable chiTiet,
            string maThuoc,
            int soLuong,
            string lieuDung,
            string cachDung)
        {
            if (chiTiet == null)
                throw new Exception(
                    "Danh sách thuốc chưa được khởi tạo.");

            if (string.IsNullOrWhiteSpace(maThuoc))
                throw new Exception("Vui lòng chọn thuốc.");

            if (soLuong <= 0)
                throw new Exception(
                    "Số lượng phải là số nguyên dương.");

            if (string.IsNullOrWhiteSpace(lieuDung) ||
                string.IsNullOrWhiteSpace(cachDung))
                throw new Exception(
                    "Vui lòng nhập liều dùng và cách dùng.");

            if (lieuDung.Trim().Length > 250 ||
                cachDung.Trim().Length > 250)
                throw new Exception(
                    "Liều dùng và cách dùng tối đa 250 ký tự.");

            foreach (DataRow row in chiTiet.Rows)
            {
                if (row["MaThuoc"].ToString() == maThuoc)
                    throw new Exception(
                        "Thuốc đã có trong danh sách toa.");
            }

            DataTable thuoc = Db.Query(@"
                SELECT TenThuoc
                FROM Thuoc
                WHERE MaThuoc = @MaThuoc",
                new SqlParameter("@MaThuoc", maThuoc));

            if (thuoc.Rows.Count == 0)
                throw new Exception("Thuốc không tồn tại.");

            string tenThuoc =
                thuoc.Rows[0]["TenThuoc"].ToString();

            chiTiet.Rows.Add(
                maThuoc,
                tenThuoc,
                soLuong,
                lieuDung.Trim(),
                cachDung.Trim());
        }

        // Lập toa và toàn bộ chi tiết trong một transaction
        public string LapToaThuoc(
            string maPhieuKham,
            string loiDan,
            DataTable chiTiet)
        {
            if (string.IsNullOrWhiteSpace(maPhieuKham))
                throw new Exception(
                    "Vui lòng chọn phiếu khám.");

            if (chiTiet == null || chiTiet.Rows.Count == 0)
                throw new Exception(
                    "Toa thuốc phải có ít nhất một thuốc.");

            if (loiDan != null && loiDan.Trim().Length > 1000)
                throw new Exception(
                    "Lời dặn không vượt quá 1000 ký tự.");

            string maToa = "TOA" +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpper();

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    // Kiểm tra trạng thái và chẩn đoán
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TrangThai, ChanDoan
                        FROM PhieuKham WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaPhieuKham = @MaPK",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaPK", maPhieuKham);

                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception(
                                    "Không tìm thấy phiếu khám.");

                            string trangThai =
                                reader["TrangThai"].ToString();

                            if (trangThai == "Hoàn tất")
                                throw new Exception(
                                    "Phiếu khám đã hoàn tất.");

                            if (reader["ChanDoan"] ==
                                DBNull.Value ||
                                string.IsNullOrWhiteSpace(
                                    reader["ChanDoan"].ToString()))
                                throw new Exception(
                                    "Bác sĩ chưa ghi chẩn đoán.");
                        }
                    }

                    // Kiểm tra phiếu khám chưa có toa
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM ToaThuoc WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaPhieuKham = @MaPK",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaPK", maPhieuKham);

                        if ((int)cmd.ExecuteScalar() > 0)
                            throw new Exception(
                                "Phiếu khám đã có toa thuốc.");
                    }

                    // Lưu toa thuốc
                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO ToaThuoc
                            (MaToa, MaPhieuKham, NgayKe, LoiDan)
                        VALUES
                            (@MaToa, @MaPK,
                             SYSDATETIME(), @LoiDan)",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaToa", maToa);

                        cmd.Parameters.AddWithValue(
                            "@MaPK", maPhieuKham);

                        cmd.Parameters.AddWithValue(
                            "@LoiDan",
                            string.IsNullOrWhiteSpace(loiDan)
                                ? (object)DBNull.Value
                                : loiDan.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    // Lưu từng dòng chi tiết
                    foreach (DataRow row in chiTiet.Rows)
                    {
                        string maThuoc =
                            row["MaThuoc"].ToString();

                        int soLuong =
                            Convert.ToInt32(row["SoLuong"]);

                        string lieuDung =
                            row["LieuDung"].ToString();

                        string cachDung =
                            row["CachDung"].ToString();

                        if (soLuong <= 0 ||
                            string.IsNullOrWhiteSpace(lieuDung) ||
                            string.IsNullOrWhiteSpace(cachDung))
                        {
                            throw new Exception(
                                "Chi tiết toa thuốc không hợp lệ.");
                        }

                        using (SqlCommand cmd =
                            new SqlCommand(@"
                                INSERT INTO ChiTietToaThuoc
                                    (MaToa, MaThuoc,
                                     SoLuong, LieuDung, CachDung)
                                VALUES
                                    (@MaToa, @MaThuoc,
                                     @SoLuong, @LieuDung, @CachDung)",
                                conn, tran))
                        {
                            cmd.Parameters.AddWithValue(
                                "@MaToa", maToa);

                            cmd.Parameters.AddWithValue(
                                "@MaThuoc", maThuoc);

                            cmd.Parameters.AddWithValue(
                                "@SoLuong", soLuong);

                            cmd.Parameters.AddWithValue(
                                "@LieuDung", lieuDung);

                            cmd.Parameters.AddWithValue(
                                "@CachDung", cachDung);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return maToa;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
