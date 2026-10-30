
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class KhamBenhService
    {
        public DataTable LayHoSo()
        {
            return Db.Query(@"
                SELECT h.MaHoSo,
                       b.HoTen + N' - ' + h.MaHoSo AS Ten
                FROM HoSoBenhAn h
                INNER JOIN BenhNhan b ON b.MaBN = h.MaBN
                ORDER BY b.HoTen");
        }

        public DataTable LayBacSi()
        {
            return Db.Query(@"
                SELECT MaBS, HoTen, MaKhoa
                FROM BacSi
                ORDER BY HoTen");
        }

        public DataTable LayKhoa()
        {
            return Db.Query(@"
                SELECT MaKhoa, TenKhoa
                FROM KhoaDieuTri
                ORDER BY TenKhoa");
        }

        public DataTable LayDanhSach()
        {
            return Db.Query(@"
                SELECT MaPhieuKham, MaHoSo, MaBS, MaKhoa,
                       NgayGioKham, LoaiKham, TrieuChung,
                       NhietDo, HuyetAp, ChanDoan, TrangThai
                FROM PhieuKham
                ORDER BY NgayGioKham DESC");
        }

        private void KiemTra(
            string maHoSo, string maBS, string maKhoa,
            string loaiKham, decimal? nhietDo)
        {
            if (string.IsNullOrWhiteSpace(maHoSo) ||
                string.IsNullOrWhiteSpace(maBS) ||
                string.IsNullOrWhiteSpace(maKhoa))
                throw new Exception("Vui lòng chọn hồ sơ, bác sĩ và khoa.");

            if (loaiKham != "THUONG" && loaiKham != "DINHKY")
                throw new Exception("Loại khám không hợp lệ.");

            if (nhietDo.HasValue &&
                (nhietDo.Value < 0 || nhietDo.Value >= 100))
                throw new Exception("Nhiệt độ không hợp lệ.");

            DataTable bacSi = Db.Query(@"
                SELECT MaBS
                FROM BacSi
                WHERE MaBS = @MaBS AND MaKhoa = @MaKhoa",
                new SqlParameter("@MaBS", maBS),
                new SqlParameter("@MaKhoa", maKhoa));

            if (bacSi.Rows.Count == 0)
                throw new Exception("Bác sĩ không thuộc khoa đã chọn.");
        }

        public string LapPhieuKham(
            string maHoSo,
            string maBS,
            string maKhoa,
            DateTime ngayGioKham,
            string loaiKham,
            string trieuChung,
            decimal? nhietDo,
            string huyetAp,
            string chanDoan)
        {
            KiemTra(maHoSo, maBS, maKhoa, loaiKham, nhietDo);

            string maPhieu = "PK" +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpper();

            const string sql = @"
                INSERT INTO PhieuKham
                    (MaPhieuKham, MaHoSo, MaBS, MaKhoa,
                     NgayGioKham, LoaiKham, TrieuChung,
                     NhietDo, HuyetAp, ChanDoan, TrangThai)
                VALUES
                    (@MaPK, @MaHS, @MaBS, @MaKhoa,
                     @Ngay, @Loai, @TrieuChung,
                     @NhietDo, @HuyetAp, @ChanDoan, N'Đang khám')";

            Db.Execute(sql,
                new SqlParameter("@MaPK", maPhieu),
                new SqlParameter("@MaHS", maHoSo),
                new SqlParameter("@MaBS", maBS),
                new SqlParameter("@MaKhoa", maKhoa),
                new SqlParameter("@Ngay", ngayGioKham),
                new SqlParameter("@Loai", loaiKham),
                new SqlParameter("@TrieuChung",
                    GiaTri(trieuChung)),
                new SqlParameter("@NhietDo",
                    nhietDo.HasValue
                        ? (object)nhietDo.Value : DBNull.Value),
                new SqlParameter("@HuyetAp",
                    GiaTri(huyetAp)),
                new SqlParameter("@ChanDoan",
                    GiaTri(chanDoan)));

            return maPhieu;
        }

        public void CapNhatPhieuKham(
            string maPhieu,
            string trieuChung,
            decimal? nhietDo,
            string huyetAp,
            string chanDoan)
        {
            if (string.IsNullOrWhiteSpace(maPhieu))
                throw new Exception("Chưa chọn phiếu khám.");

            if (nhietDo.HasValue &&
                (nhietDo.Value < 0 || nhietDo.Value >= 100))
                throw new Exception("Nhiệt độ không hợp lệ.");

            int rows = Db.Execute(@"
                UPDATE PhieuKham
                SET TrieuChung = @TrieuChung,
                    NhietDo = @NhietDo,
                    HuyetAp = @HuyetAp,
                    ChanDoan = @ChanDoan,
                    TrangThai = N'Chờ hoàn tất'
                WHERE MaPhieuKham = @MaPK
                  AND TrangThai <> N'Hoàn tất'",
                new SqlParameter("@MaPK", maPhieu),
                new SqlParameter("@TrieuChung",
                    GiaTri(trieuChung)),
                new SqlParameter("@NhietDo",
                    nhietDo.HasValue
                        ? (object)nhietDo.Value : DBNull.Value),
                new SqlParameter("@HuyetAp",
                    GiaTri(huyetAp)),
                new SqlParameter("@ChanDoan",
                    GiaTri(chanDoan)));

            if (rows == 0)
                throw new Exception(
                    "Không tìm thấy phiếu khám hoặc phiếu đã hoàn tất.");
        }

        private object GiaTri(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? (object)DBNull.Value
                : value.Trim();
        }
    }
}
