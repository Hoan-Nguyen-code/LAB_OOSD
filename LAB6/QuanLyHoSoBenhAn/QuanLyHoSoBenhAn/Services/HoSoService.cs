
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class HoSoService
    {
        private string TaoMa(string tienTo)
        {
            return tienTo +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpper();
        }

        public DataTable LayBenhNhan()
        {
            return Db.Query(@"
                SELECT MaBN, HoTen, DienThoai
                FROM BenhNhan
                ORDER BY HoTen");
        }

        public DataTable LayHoSo(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT h.MaHoSo, b.MaBN, b.HoTen,
                       b.DienThoai, h.NgayCap, h.TrangThai
                FROM HoSoBenhAn h
                INNER JOIN BenhNhan b ON b.MaBN = h.MaBN
                WHERE @TuKhoa = N''
                   OR h.MaHoSo LIKE '%' + @TuKhoa + '%'
                   OR b.MaBN LIKE '%' + @TuKhoa + '%'
                   OR b.HoTen LIKE N'%' + @TuKhoa + N'%'
                ORDER BY h.NgayCap DESC",
                new SqlParameter("@TuKhoa", tuKhoa.Trim()));
        }

        public DataTable LayLichSu(string maHoSo)
        {
            return Db.Query(@"
                SELECT p.MaPhieuKham, p.NgayGioKham,
                       b.HoTen AS BacSi,
                       k.TenKhoa, p.LoaiKham,
                       p.TrieuChung, p.ChanDoan,
                       p.TrangThai
                FROM PhieuKham p
                INNER JOIN BacSi b ON b.MaBS = p.MaBS
                INNER JOIN KhoaDieuTri k ON k.MaKhoa = p.MaKhoa
                WHERE p.MaHoSo = @MaHoSo
                ORDER BY p.NgayGioKham DESC",
                new SqlParameter("@MaHoSo", maHoSo));
        }

        public string DangKyVaCapHoSo(
            string maBN, decimal soTien,
            DateTime ngayBatDau, DateTime ngayHetHan)
        {
            KiemTraLePhi(maBN, soTien, ngayBatDau, ngayHetHan);

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    // Khóa bản ghi bệnh nhân khi xử lý đăng ký.
                    var check = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM BenhNhan WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaBN = @MaBN", conn, tran);

                    check.Parameters.AddWithValue("@MaBN", maBN);
                    if ((int)check.ExecuteScalar() == 0)
                        throw new Exception("Bệnh nhân không tồn tại.");

                    var checkHoSo = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM HoSoBenhAn
                        WHERE MaBN = @MaBN", conn, tran);

                    checkHoSo.Parameters.AddWithValue("@MaBN", maBN);
                    if ((int)checkHoSo.ExecuteScalar() > 0)
                        throw new Exception(
                            "Bệnh nhân đã có hồ sơ bệnh án.");

                    string maDK = TaoMa("DK");
                    string maLePhi = TaoMa("LP");
                    string maHoSo = TaoMa("HS");

                    var dk = new SqlCommand(@"
                        INSERT INTO PhieuDangKy
                            (MaDK, MaBN, TrangThai)
                        VALUES
                            (@MaDK, @MaBN, N'Hoàn tất')",
                        conn, tran);

                    dk.Parameters.AddWithValue("@MaDK", maDK);
                    dk.Parameters.AddWithValue("@MaBN", maBN);
                    dk.ExecuteNonQuery();

                    LuuLePhi(
                        conn, tran, maLePhi, maBN,
                        soTien, ngayBatDau, ngayHetHan);

                    var hs = new SqlCommand(@"
                        INSERT INTO HoSoBenhAn
                            (MaHoSo, MaBN, TrangThai)
                        VALUES
                            (@MaHoSo, @MaBN, N'Đang sử dụng')",
                        conn, tran);

                    hs.Parameters.AddWithValue("@MaHoSo", maHoSo);
                    hs.Parameters.AddWithValue("@MaBN", maBN);
                    hs.ExecuteNonQuery();

                    tran.Commit();
                    return maHoSo;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void GiaHanLePhi(
            string maBN, decimal soTien,
            DateTime ngayBatDau, DateTime ngayHetHan)
        {
            KiemTraLePhi(maBN, soTien, ngayBatDau, ngayHetHan);

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    var check = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM BenhNhan WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaBN = @MaBN", conn, tran);

                    check.Parameters.AddWithValue("@MaBN", maBN);

                    if ((int)check.ExecuteScalar() == 0)
                        throw new Exception("Bệnh nhân không tồn tại.");

                    var hs = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM HoSoBenhAn
                        WHERE MaBN = @MaBN", conn, tran);

                    hs.Parameters.AddWithValue("@MaBN", maBN);

                    if ((int)hs.ExecuteScalar() == 0)
                        throw new Exception(
                            "Bệnh nhân chưa được cấp hồ sơ.");

                    var overlap = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM LePhiNam
                        WHERE MaBN = @MaBN
                          AND NgayBatDau < @NgayHetHan
                          AND NgayHetHan > @NgayBatDau",
                        conn, tran);

                    overlap.Parameters.AddWithValue("@MaBN", maBN);
                    overlap.Parameters.AddWithValue(
                        "@NgayBatDau", ngayBatDau.Date);
                    overlap.Parameters.AddWithValue(
                        "@NgayHetHan", ngayHetHan.Date);

                    if ((int)overlap.ExecuteScalar() > 0)
                        throw new Exception(
                            "Kỳ lệ phí này trùng với kỳ đã ghi nhận.");

                    LuuLePhi(
                        conn, tran, TaoMa("LP"), maBN,
                        soTien, ngayBatDau, ngayHetHan);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public DataTable LayLePhi(string maBN)
        {
            return Db.Query(@"
                SELECT MaLePhi, NgayDong, SoTien,
                       NgayBatDau, NgayHetHan
                FROM LePhiNam
                WHERE MaBN = @MaBN
                ORDER BY NgayBatDau DESC",
                new SqlParameter("@MaBN", maBN));
        }

        private void LuuLePhi(
            SqlConnection conn,
            SqlTransaction tran,
            string maLePhi, string maBN, decimal soTien,
            DateTime ngayBatDau, DateTime ngayHetHan)
        {
            var cmd = new SqlCommand(@"
                INSERT INTO LePhiNam
                    (MaLePhi, MaBN, NgayDong,
                     SoTien, NgayBatDau, NgayHetHan)
                VALUES
                    (@MaLePhi, @MaBN, SYSDATETIME(),
                     @SoTien, @NgayBatDau, @NgayHetHan)",
                conn, tran);

            cmd.Parameters.AddWithValue("@MaLePhi", maLePhi);
            cmd.Parameters.AddWithValue("@MaBN", maBN);
            cmd.Parameters.AddWithValue("@SoTien", soTien);
            cmd.Parameters.AddWithValue(
                "@NgayBatDau", ngayBatDau.Date);
            cmd.Parameters.AddWithValue(
                "@NgayHetHan", ngayHetHan.Date);

            cmd.ExecuteNonQuery();
        }

        private void KiemTraLePhi(
            string maBN, decimal soTien,
            DateTime ngayBatDau, DateTime ngayHetHan)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new Exception("Chưa chọn bệnh nhân.");

            if (soTien <= 0)
                throw new Exception(
                    "Số tiền lệ phí phải lớn hơn 0.");

            if (ngayHetHan.Date <= ngayBatDau.Date)
                throw new Exception(
                    "Ngày hết hạn phải sau ngày bắt đầu.");
        }
    }
}
