
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class GiaHanService : CrudService
    {
        public GiaHanService()
        {
            Add(new TableSpec(
                "Phiếu gia hạn",
                "PhieuGiaHan",
                "MaPhieuGiaHan",
                true
            ));

            Add(new TableSpec(
                "Phòng đang thuê",
                "ChiTietHopDong",
                "MaChiTietHD",
                true
            ));
        }

        public string LapPhieuGiaHan(
            string maPhieu,
            string maChiTiet,
            DateTime ngayLap,
            int soThang)
        {
            // 1. Kiểm tra dữ liệu đầu vào
            maPhieu = V.Id(maPhieu);
            maChiTiet = V.Id(maChiTiet);
            ngayLap = ngayLap.Date;

            if (soThang <= 0)
            {
                throw new Exception(
                    "Số tháng gia hạn phải lớn hơn 0.");
            }

            // 2. Toàn bộ nghiệp vụ chạy trong transaction
            return Db.Transaction<string>((c, tx) =>
            {
                // 3. Lấy thông tin phòng và hợp đồng
                DataTable dt = Db.Query(c, tx,
                    @"SELECT
                          ct.MaChiTietHD,
                          ct.MaPhong,
                          ct.NgayBatDau AS NgayBatDauPhong,
                          ct.NgayKetThuc,
                          hd.MaHopDong,
                          hd.NgayBatDau AS NgayBatDauHD,
                          hd.ThoiHanThue,
                          hd.TrangThai
                      FROM dbo.ChiTietHopDong ct
                          WITH (UPDLOCK, HOLDLOCK)
                      INNER JOIN dbo.HopDongThue hd
                          ON hd.MaHopDong = ct.MaHopDong
                      WHERE ct.MaChiTietHD = @id",
                    Db.P("@id", maChiTiet));

                if (dt.Rows.Count == 0)
                {
                    throw new Exception(
                        "Không tìm thấy chi tiết hợp đồng thuê.");
                }

                DataRow r = dt.Rows[0];

                string maHD =
                    Convert.ToString(r["MaHopDong"]);

                string maPhong =
                    Convert.ToString(r["MaPhong"]);

                string trangThai =
                    Convert.ToString(r["TrangThai"]);

                int thoiHanGoc =
                    Convert.ToInt32(r["ThoiHanThue"]);

                DateTime ngayBatDauHD =
                    Convert.ToDateTime(
                        r["NgayBatDauHD"]).Date;

                DateTime ngayBatDauPhong =
                    Convert.ToDateTime(
                        r["NgayBatDauPhong"]).Date;

                DateTime ngayKetThucCu =
                    Convert.ToDateTime(
                        r["NgayKetThuc"]).Date;

                // 4. Kiểm tra trạng thái hợp đồng
                if (trangThai != "Có hiệu lực")
                {
                    throw new Exception(
                        "Hợp đồng không ở trạng thái " +
                        "có hiệu lực, không thể gia hạn.");
                }

                if (ngayLap < ngayBatDauPhong)
                {
                    throw new Exception(
                        "Ngày lập phiếu không được " +
                        "trước ngày bắt đầu thuê phòng.");
                }

                if (ngayLap > ngayKetThucCu)
                {
                    throw new Exception(
                        "Thời hạn thuê đã kết thúc. " +
                        "Không thể gia hạn theo quy trình này.");
                }

                // 5. Kiểm tra giới hạn gia hạn
                // Quy ước: mỗi lần gia hạn tối đa
                // một nửa thời gian thuê ban đầu.
                int soThangToiDa = thoiHanGoc / 2;

                if (soThang > soThangToiDa)
                {
                    throw new Exception(
                        "Thời gian gia hạn tối đa là " +
                        soThangToiDa + " tháng.");
                }

                // 6. Xác định khoảng thời gian gia hạn
                DateTime ngayBatDauGiaHan =
                    ngayKetThucCu.AddDays(1);

                DateTime ngayKetThucMoi =
                    ngayBatDauGiaHan
                        .AddMonths(soThang)
                        .AddDays(-1);

                // 7. Khóa bản ghi phòng nhằm phối hợp
                // với giao dịch lập hợp đồng mới.
                int phongTonTai = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.Phong
                          WITH (UPDLOCK, HOLDLOCK)
                      WHERE MaPhong = @p",
                    Db.P("@p", maPhong));

                if (phongTonTai == 0)
                {
                    throw new Exception(
                        "Phòng thuê không còn tồn tại.");
                }

                // 8. Kiểm tra công nợ tiền thuê
                // Kiểm tra tất cả hóa đơn đã phát hành,
                // không chỉ những hóa đơn quá hạn.
                decimal noTienThue = Money(c, tx,
                    @"SELECT ISNULL(SUM(
                          h.TongTien -
                          ISNULL(
                              (SELECT SUM(p.SoTienThu)
                               FROM dbo.PhieuThuTienThue p
                               WHERE p.MaHoaDonThue =
                                     h.MaHoaDonThue),
                              0)
                      ), 0)
                      FROM dbo.HoaDonTienThue h
                      WHERE h.MaHopDong = @hd",
                    Db.P("@hd", maHD));

                if (noTienThue > 0)
                {
                    throw new Exception(
                        "Không thể gia hạn. " +
                        "Hợp đồng còn nợ tiền thuê: " +
                        noTienThue.ToString("N0") +
                        " đồng.");
                }

                // 9. Kiểm tra các kỳ thuê từ tháng 7
                // đã bắt đầu nhưng chưa có hóa đơn.
                // Tránh bỏ qua nghĩa vụ tiền thuê
                // chỉ vì kế toán chưa lập hóa đơn.
                for (int ky = 7; ky <= thoiHanGoc; ky++)
                {
                    DateTime ngayBatDauKy =
                        ngayBatDauHD.AddMonths(ky - 1);

                    if (ngayBatDauKy > ngayLap)
                    {
                        break;
                    }

                    int soHoaDon = Count(c, tx,
                        @"SELECT COUNT(*)
                          FROM dbo.HoaDonTienThue h
                          WHERE h.MaHopDong = @hd
                            AND h.SoKyBatDau <= @ky
                            AND h.SoKyKetThuc >= @ky",
                        Db.P("@hd", maHD),
                        Db.P("@ky", ky));

                    if (soHoaDon == 0)
                    {
                        throw new Exception(
                            "Chưa lập hóa đơn tiền thuê " +
                            "cho kỳ " + ky + ".");
                    }
                }

                // 10. Kiểm tra công nợ dịch vụ
                // Gồm hóa đơn dịch vụ của các phòng
                // thuộc cùng hợp đồng.
                decimal noDichVu = Money(c, tx,
                    @"SELECT ISNULL(SUM(
                          h.TongTien -
                          ISNULL(
                              (SELECT SUM(p.SoTienThu)
                               FROM dbo.PhieuThuDichVu p
                               WHERE p.MaHoaDonDV =
                                     h.MaHoaDonDV),
                              0)
                      ), 0)
                      FROM dbo.HoaDonDichVu h
                      INNER JOIN dbo.ChiTietHopDong ct
                          ON ct.MaChiTietHD =
                             h.MaChiTietHD
                      WHERE ct.MaHopDong = @hd",
                    Db.P("@hd", maHD));

                if (noDichVu > 0)
                {
                    throw new Exception(
                        "Không thể gia hạn. " +
                        "Hợp đồng còn nợ dịch vụ: " +
                        noDichVu.ToString("N0") +
                        " đồng.");
                }

                // 11. Kiểm tra dịch vụ đã ghi nhận
                // nhưng chưa được đưa vào hóa đơn.
                // Chỉ xét tháng không vượt quá
                // tháng lập phiếu gia hạn.
                int dichVuChuaLapHoaDon = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.SuDungDichVu sd
                      INNER JOIN dbo.ChiTietHopDong ct
                          ON ct.MaChiTietHD =
                             sd.MaChiTietHD
                      WHERE ct.MaHopDong = @hd
                        AND DATEFROMPARTS(
                            sd.Nam, sd.Thang, 1)
                            <= DATEFROMPARTS(
                                YEAR(@ngay),
                                MONTH(@ngay), 1)
                        AND NOT EXISTS
                        (
                            SELECT 1
                            FROM dbo.ChiTietHoaDonDichVu d
                            WHERE d.MaSuDung = sd.MaSuDung
                        )",
                    Db.P("@hd", maHD),
                    Db.P("@ngay", ngayLap));

                if (dichVuChuaLapHoaDon > 0)
                {
                    throw new Exception(
                        "Còn " + dichVuChuaLapHoaDon +
                        " khoản sử dụng dịch vụ " +
                        "chưa được lập hóa đơn.");
                }

                // 12. Kiểm tra trùng lịch thuê
                // Không xét chính chi tiết hợp đồng
                // đang được gia hạn.
                int trungLich = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.ChiTietHopDong ct
                          WITH (UPDLOCK, HOLDLOCK)
                      INNER JOIN dbo.HopDongThue hd
                          ON hd.MaHopDong =
                             ct.MaHopDong
                      WHERE ct.MaPhong = @p
                        AND ct.MaChiTietHD <> @ct
                        AND hd.TrangThai <> N'Chấm dứt'
                        AND ct.NgayBatDau <= @kt
                        AND ct.NgayKetThuc >= @bd",
                    Db.P("@p", maPhong),
                    Db.P("@ct", maChiTiet),
                    Db.P("@bd", ngayBatDauGiaHan),
                    Db.P("@kt", ngayKetThucMoi));

                if (trungLich > 0)
                {
                    throw new Exception(
                        "Không thể gia hạn vì phòng " +
                        maPhong +
                        " đã có lịch thuê trùng.");
                }

                // 13. Kiểm tra mã phiếu gia hạn
                int trungMa = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.PhieuGiaHan
                      WHERE MaPhieuGiaHan = @id",
                    Db.P("@id", maPhieu));

                if (trungMa > 0)
                {
                    throw new Exception(
                        "Mã phiếu gia hạn đã tồn tại.");
                }

                // 14. Lập phiếu gia hạn
                Db.Execute(c, tx,
                    @"INSERT INTO dbo.PhieuGiaHan
                      (
                          MaPhieuGiaHan,
                          MaChiTietHD,
                          NgayLap,
                          NgayBatDauGiaHan,
                          SoThangGiaHan,
                          NgayKetThucMoi,
                          GhiChu
                      )
                      VALUES
                      (
                          @id, @ct, @lap,
                          @bd, @thang, @kt, @gc
                      )",
                    Db.P("@id", maPhieu),
                    Db.P("@ct", maChiTiet),
                    Db.P("@lap", ngayLap),
                    Db.P("@bd", ngayBatDauGiaHan),
                    Db.P("@thang", soThang),
                    Db.P("@kt", ngayKetThucMoi),
                    Db.P("@gc",
                        "Gia hạn phòng " + maPhong));

                // 15. Cập nhật thời hạn thuê của phòng
                int soDong = Db.Execute(c, tx,
                    @"UPDATE dbo.ChiTietHopDong
                      SET NgayKetThuc = @kt
                      WHERE MaChiTietHD = @id",
                    Db.P("@kt", ngayKetThucMoi),
                    Db.P("@id", maChiTiet));

                if (soDong != 1)
                {
                    throw new Exception(
                        "Không thể cập nhật thời hạn thuê.");
                }

                // Transaction sẽ COMMIT nếu không có lỗi
                return
                    "Lập phiếu gia hạn thành công!" +
                    Environment.NewLine +
                    "Mã phiếu: " + maPhieu +
                    Environment.NewLine +
                    "Mã phòng: " + maPhong +
                    Environment.NewLine +
                    "Số tháng gia hạn: " + soThang +
                    Environment.NewLine +
                    "Bắt đầu gia hạn: " +
                    ngayBatDauGiaHan.ToString("dd/MM/yyyy") +
                    Environment.NewLine +
                    "Kết thúc mới: " +
                    ngayKetThucMoi.ToString("dd/MM/yyyy");
            });
        }
    }
}
