using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Services
{
    public class ThongKeTongHop
    {
        public int LuotSachMuon { get; set; }
        public int SachQuaHan { get; set; }
        public int SachMat { get; set; }
        public int SachHuHong { get; set; }
        public decimal TongPhiPhat { get; set; }
    }

    public class ThongKeService
    {
        // =========================================================
        // THỐNG KÊ TỔNG HỢP
        // =========================================================
        public ThongKeTongHop LayTongHop(
            DateTime tuNgay,
            DateTime denNgay)
        {
            DateTime from = tuNgay.Date;
            DateTime to = denNgay.Date;

            // Nếu nhập ngược khoảng ngày thì tự hoán đổi
            if (to < from)
            {
                DateTime temp = from;
                from = to;
                to = temp;
            }

            ThongKeTongHop kq =
                new ThongKeTongHop();

            // -----------------------------------------------------
            // 1. SỐ LƯỢT SÁCH MƯỢN
            // -----------------------------------------------------
            kq.LuotSachMuon =
                Convert.ToInt32(
                    Db.Scalar(
                        @"SELECT COUNT(*)
                          FROM ChiTietPhieuMuon ct
                          JOIN PhieuMuon pm
                            ON pm.MaPhieuMuon =
                               ct.MaPhieuMuon
                          WHERE pm.NgayMuon
                                BETWEEN @Tu AND @Den",
                        new SqlParameter(
                            "@Tu", from),
                        new SqlParameter(
                            "@Den", to)));

            // -----------------------------------------------------
            // 2. SÁCH QUÁ HẠN
            // -----------------------------------------------------
            kq.SachQuaHan =
                Convert.ToInt32(
                    Db.Scalar(
                        @"SELECT COUNT(*)
                          FROM ChiTietPhieuMuon ct
                          JOIN PhieuMuon pm
                            ON pm.MaPhieuMuon =
                               ct.MaPhieuMuon
                          WHERE
                          (
                              ct.NgayTraThucTe IS NULL
                              AND pm.NgayHenTra <
                                  CAST(GETDATE() AS date)
                          )
                          OR
                          (
                              ct.NgayTraThucTe IS NOT NULL
                              AND ct.NgayTraThucTe >
                                  pm.NgayHenTra
                              AND ct.NgayTraThucTe
                                  BETWEEN @Tu AND @Den
                          )",
                        new SqlParameter(
                            "@Tu", from),
                        new SqlParameter(
                            "@Den", to)));

            // -----------------------------------------------------
            // 3. SÁCH MẤT
            // -----------------------------------------------------
            kq.SachMat =
                Convert.ToInt32(
                    Db.Scalar(
                        @"SELECT COUNT(*)
                          FROM ChiTietPhieuMuon
                          WHERE NgayTraThucTe
                                BETWEEN @Tu AND @Den
                            AND TinhTrangTra
                                LIKE N'%Mất%'",
                        new SqlParameter(
                            "@Tu", from),
                        new SqlParameter(
                            "@Den", to)));

            // -----------------------------------------------------
            // 4. SÁCH HƯ HỎNG
            // -----------------------------------------------------
            kq.SachHuHong =
                Convert.ToInt32(
                    Db.Scalar(
                        @"SELECT COUNT(*)
                          FROM ChiTietPhieuMuon
                          WHERE NgayTraThucTe
                                BETWEEN @Tu AND @Den
                            AND
                            (
                                TinhTrangTra LIKE N'%Rách%'
                                OR
                                TinhTrangTra LIKE N'%Hư%'
                            )",
                        new SqlParameter(
                            "@Tu", from),
                        new SqlParameter(
                            "@Den", to)));

            // -----------------------------------------------------
            // 5. TỔNG PHÍ PHẠT
            // -----------------------------------------------------
            object tong =
                Db.Scalar(
                    @"SELECT ISNULL(
                          SUM(PhiPhat), 0)
                      FROM PhieuPhat
                      WHERE NgayPhat
                            BETWEEN @Tu AND @Den",
                    new SqlParameter(
                        "@Tu", from),
                    new SqlParameter(
                        "@Den", to));

            kq.TongPhiPhat =
                Convert.ToDecimal(tong);

            return kq;
        }

        // =========================================================
        // CHI TIẾT PHIẾU PHẠT
        // =========================================================
        public DataTable LayChiTietPhat(
            DateTime tuNgay,
            DateTime denNgay)
        {
            DateTime from = tuNgay.Date;
            DateTime to = denNgay.Date;

            if (to < from)
            {
                DateTime temp = from;
                from = to;
                to = temp;
            }

            return Db.Query(
                @"SELECT
                      pp.MaPhieuPhat,
                      pp.NgayPhat,
                      pm.MaDocGia,
                      ct.MaDauSach,
                      s.TenSach,
                      pp.LyDo,
                      pp.PhiPhat,
                      pp.MaNhanVien
                  FROM PhieuPhat pp
                  JOIN ChiTietPhieuMuon ct
                    ON ct.MaChiTiet =
                       pp.MaChiTiet
                  JOIN PhieuMuon pm
                    ON pm.MaPhieuMuon =
                       ct.MaPhieuMuon
                  JOIN DauSach s
                    ON s.MaDauSach =
                       ct.MaDauSach
                  WHERE pp.NgayPhat
                        BETWEEN @Tu AND @Den
                  ORDER BY
                      pp.NgayPhat DESC,
                      pp.MaPhieuPhat",
                new SqlParameter(
                    "@Tu", from),
                new SqlParameter(
                    "@Den", to));
        }
    }
}