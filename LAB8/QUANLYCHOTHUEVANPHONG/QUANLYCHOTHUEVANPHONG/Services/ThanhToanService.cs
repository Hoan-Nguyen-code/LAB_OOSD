
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class ThanhToanService : CrudService
    {
        public ThanhToanService()
        {
            // Danh sách hóa đơn tiền thuê
            Add(new TableSpec(
                "Hóa đơn thuê",
                "HoaDonTienThue",
                "MaHoaDonThue",
                true
            ));

            // Chi tiết hóa đơn tiền thuê
            Add(new TableSpec(
                "Chi tiết hóa đơn thuê",
                "ChiTietHoaDonThue",
                "MaCTHDThue",
                true
            ));

            // Danh sách phiếu thu tiền thuê
            Add(new TableSpec(
                "Phiếu thu thuê",
                "PhieuThuTienThue",
                "MaPhieuThu",
                true
            ));
        }

        // ==========================================
        // 1. LẬP HÓA ĐƠN TIỀN THUÊ TỪ THÁNG THỨ 7
        // ==========================================
        public string LapHoaDonThang(
            string maHoaDon,
            string maHD,
            int ky)
        {
            maHoaDon = V.Id(maHoaDon);
            maHD = V.Id(maHD);

            if (ky < 7)
            {
                throw new Exception(
                    "Kỳ 1 đến kỳ 6 đã được lập " +
                    "hóa đơn trả trước. " +
                    "Chỉ lập hóa đơn riêng từ kỳ thứ 7.");
            }

            if (ky > 600)
            {
                throw new Exception(
                    "Kỳ thuê không hợp lệ.");
            }

            return Db.Transaction<string>((c, tx) =>
            {
                // 1.1. Kiểm tra hợp đồng
                DataTable hd = Db.Query(c, tx,
                    @"SELECT
                          MaHopDong,
                          NgayBatDau,
                          ThoiHanThue,
                          TrangThai
                      FROM dbo.HopDongThue
                          WITH (UPDLOCK, HOLDLOCK)
                      WHERE MaHopDong = @id",
                    Db.P("@id", maHD));

                if (hd.Rows.Count == 0)
                {
                    throw new Exception(
                        "Không tìm thấy hợp đồng thuê.");
                }

                DataRow rowHD = hd.Rows[0];

                string trangThai =
                    Convert.ToString(rowHD["TrangThai"]);

                if (trangThai == "Chấm dứt" ||
                    trangThai == "Hết hạn")
                {
                    throw new Exception(
                        "Hợp đồng đã kết thúc hoặc " +
                        "chấm dứt, không thể lập hóa đơn.");
                }

                DateTime ngayBatDauHD =
                    Convert.ToDateTime(
                        rowHD["NgayBatDau"]).Date;

                int thoiHanGoc =
                    Convert.ToInt32(
                        rowHD["ThoiHanThue"]);

                DateTime ngayBatDauKy =
                    ngayBatDauHD.AddMonths(ky - 1);

                DateTime ngayKetThucKy =
                    ngayBatDauHD
                        .AddMonths(ky)
                        .AddDays(-1);

                // 1.2. Kiểm tra kỳ thuê có hợp lệ
                // với thời hạn ban đầu hoặc gia hạn.
                DataTable rooms = Db.Query(c, tx,
                    @"SELECT
                          MaChiTietHD,
                          MaPhong,
                          DonGiaThue,
                          NgayBatDau,
                          NgayKetThuc
                      FROM dbo.ChiTietHopDong
                      WHERE MaHopDong = @hd
                        AND NgayBatDau <= @bd
                        AND NgayKetThuc >= @kt",
                    Db.P("@hd", maHD),
                    Db.P("@bd", ngayBatDauKy),
                    Db.P("@kt", ngayKetThucKy));

                if (rooms.Rows.Count == 0)
                {
                    throw new Exception(
                        "Không có phòng thuê hợp lệ " +
                        "cho toàn bộ kỳ " + ky + ".");
                }

                // 1.3. Không lập trùng mã hóa đơn
                int trungMa = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.HoaDonTienThue
                          WITH (UPDLOCK, HOLDLOCK)
                      WHERE MaHoaDonThue = @id",
                    Db.P("@id", maHoaDon));

                if (trungMa > 0)
                {
                    throw new Exception(
                        "Mã hóa đơn đã tồn tại.");
                }

                // 1.4. Không lập trùng kỳ thuê
                int trungKy = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.HoaDonTienThue
                          WITH (UPDLOCK, HOLDLOCK)
                      WHERE MaHopDong = @hd
                        AND SoKyBatDau <= @ky
                        AND SoKyKetThuc >= @ky",
                    Db.P("@hd", maHD),
                    Db.P("@ky", ky));

                if (trungKy > 0)
                {
                    throw new Exception(
                        "Kỳ thuê thứ " + ky +
                        " đã có hóa đơn.");
                }

                // 1.5. Tính tổng tiền thuê
                decimal tongTien = rooms.AsEnumerable()
                    .Sum(r => Convert.ToDecimal(
                        r["DonGiaThue"]));

                if (tongTien <= 0)
                {
                    throw new Exception(
                        "Tổng tiền thuê phải lớn hơn 0.");
                }

                // 1.6. Lập hóa đơn tiền thuê
                Db.Execute(c, tx,
                    @"INSERT INTO dbo.HoaDonTienThue
                      (
                          MaHoaDonThue,
                          MaHopDong,
                          SoKyBatDau,
                          SoKyKetThuc,
                          NgayLap,
                          HanThanhToan,
                          TongTien,
                          TrangThai
                      )
                      VALUES
                      (
                          @id, @hd, @ky, @ky,
                          @lap, @han, @tien,
                          N'Chưa thanh toán'
                      )",
                    Db.P("@id", maHoaDon),
                    Db.P("@hd", maHD),
                    Db.P("@ky", ky),
                    Db.P("@lap", DateTime.Today),
                    Db.P("@han", ngayBatDauKy),
                    Db.P("@tien", tongTien));

                // 1.7. Lập chi tiết hóa đơn cho
                // từng phòng trong hợp đồng.
                int i = 0;

                foreach (DataRow r in rooms.Rows)
                {
                    i++;

                    string maChiTietHoaDon =
                        V.Id("CT" + maHoaDon + "_" + i);

                    Db.Execute(c, tx,
                        @"INSERT INTO dbo.ChiTietHoaDonThue
                          (
                              MaCTHDThue,
                              MaHoaDonThue,
                              MaChiTietHD,
                              ThanhTien
                          )
                          VALUES
                          (
                              @id, @hd, @ct, @tien
                          )",
                        Db.P("@id", maChiTietHoaDon),
                        Db.P("@hd", maHoaDon),
                        Db.P("@ct", r["MaChiTietHD"]),
                        Db.P("@tien", r["DonGiaThue"]));
                }

                return
                    "Lập hóa đơn thành công!" +
                    Environment.NewLine +
                    "Mã hóa đơn: " + maHoaDon +
                    Environment.NewLine +
                    "Mã hợp đồng: " + maHD +
                    Environment.NewLine +
                    "Kỳ thuê: " + ky +
                    Environment.NewLine +
                    "Tổng tiền: " +
                    tongTien.ToString("N0") + " đồng." +
                    Environment.NewLine +
                    "Hạn thanh toán: " +
                    ngayBatDauKy.ToString("dd/MM/yyyy");
            });
        }

        // ==========================================
        // 2. HÀM THU TIỀN DÙNG CHUNG
        // ==========================================
        private static string Collect(
            string maPhieu,
            string maHoaDon,
            DateTime ngayThu,
            decimal soTien,
            string hinhThuc,
            string invoiceTable,
            string invoiceKey,
            string amountColumn,
            string receiptTable,
            string receiptKey,
            string receiptInvoiceKey)
        {
            // 2.1. Kiểm tra dữ liệu đầu vào
            maPhieu = V.Id(maPhieu);
            maHoaDon = V.Id(maHoaDon);
            ngayThu = ngayThu.Date;

            if (soTien <= 0)
            {
                throw new Exception(
                    "Số tiền thu phải lớn hơn 0.");
            }

            if (hinhThuc != "Tiền mặt" &&
                hinhThuc != "Chuyển khoản")
            {
                throw new Exception(
                    "Hình thức thanh toán không hợp lệ.");
            }

            return Db.Transaction<string>((c, tx) =>
            {
                // 2.2. Khóa chứng từ để ngăn
                // hai giao dịch thu vượt tiền.
                string sql =
                    "SELECT [" + amountColumn + "] " +
                    "FROM dbo.[" + invoiceTable + "] " +
                    "WITH (UPDLOCK, HOLDLOCK) " +
                    "WHERE [" + invoiceKey + "] = @id";

                object obj = Db.Scalar(c, tx,
                    sql,
                    Db.P("@id", maHoaDon));

                if (obj == null ||
                    obj == DBNull.Value)
                {
                    throw new Exception(
                        "Không tìm thấy hóa đơn " +
                        "hoặc phiếu xử phạt.");
                }

                decimal tongTien =
                    Convert.ToDecimal(obj);

                // 2.3. Kiểm tra trùng mã phiếu thu
                int trungMa = Count(c, tx,
                    "SELECT COUNT(*) FROM dbo.[" +
                    receiptTable + "] " +
                    "WHERE [" + receiptKey + "] = @id",
                    Db.P("@id", maPhieu));

                if (trungMa > 0)
                {
                    throw new Exception(
                        "Mã phiếu thu đã tồn tại.");
                }

                // 2.4. Tính tổng tiền đã thu
                decimal daThu = Money(c, tx,
                    "SELECT ISNULL(SUM(SoTienThu), 0) " +
                    "FROM dbo.[" + receiptTable + "] " +
                    "WHERE [" + receiptInvoiceKey +
                    "] = @id",
                    Db.P("@id", maHoaDon));

                decimal conNo = tongTien - daThu;

                if (conNo <= 0)
                {
                    throw new Exception(
                        "Chứng từ đã được thanh toán đủ.");
                }

                if (soTien > conNo)
                {
                    throw new Exception(
                        "Số tiền thu vượt số tiền " +
                        "còn phải thanh toán." +
                        Environment.NewLine +
                        "Còn nợ: " +
                        conNo.ToString("N0") +
                        " đồng.");
                }

                // 2.5. Lưu phiếu thu
                string insertSql =
                    "INSERT INTO dbo.[" + receiptTable + "] " +
                    "([" + receiptKey + "], " +
                    "[" + receiptInvoiceKey + "], " +
                    "NgayThu, SoTienThu, HinhThucThu) " +
                    "VALUES (@p, @h, @ngay, @tien, @ht)";

                Db.Execute(c, tx,
                    insertSql,
                    Db.P("@p", maPhieu),
                    Db.P("@h", maHoaDon),
                    Db.P("@ngay", ngayThu),
                    Db.P("@tien", soTien),
                    Db.P("@ht", hinhThuc));

                // 2.6. Cập nhật công nợ
                decimal conLai = conNo - soTien;

                string trangThai =
                    conLai == 0m
                    ? "Đã thanh toán"
                    : "Thanh toán một phần";

                // 2.7. Cập nhật trạng thái hóa đơn
                string updateSql =
                    "UPDATE dbo.[" + invoiceTable + "] " +
                    "SET TrangThai = @st " +
                    "WHERE [" + invoiceKey + "] = @id";

                int soDong = Db.Execute(c, tx,
                    updateSql,
                    Db.P("@st", trangThai),
                    Db.P("@id", maHoaDon));

                if (soDong != 1)
                {
                    throw new Exception(
                        "Không thể cập nhật trạng thái " +
                        "thanh toán.");
                }

                // Transaction tự COMMIT khi không lỗi
                return
                    "Thu tiền thành công!" +
                    Environment.NewLine +
                    "Mã phiếu thu: " + maPhieu +
                    Environment.NewLine +
                    "Mã chứng từ: " + maHoaDon +
                    Environment.NewLine +
                    "Số tiền vừa thu: " +
                    soTien.ToString("N0") + " đồng" +
                    Environment.NewLine +
                    "Số tiền còn nợ: " +
                    conLai.ToString("N0") + " đồng" +
                    Environment.NewLine +
                    "Trạng thái: " + trangThai;
            });
        }

        // ==========================================
        // 3. THU TIỀN THUÊ VĂN PHÒNG
        // ==========================================
        public string ThuTienThue(
            string maPhieu,
            string maHoaDon,
            DateTime ngayThu,
            decimal soTien,
            string hinhThuc)
        {
            return Collect(
                maPhieu,
                maHoaDon,
                ngayThu,
                soTien,
                hinhThuc,
                "HoaDonTienThue",
                "MaHoaDonThue",
                "TongTien",
                "PhieuThuTienThue",
                "MaPhieuThu",
                "MaHoaDonThue"
            );
        }

        // ==========================================
        // 4. THU TIỀN DỊCH VỤ
        // ==========================================
        public static string ThuTienDichVu(
            string maPhieu,
            string maHoaDon,
            DateTime ngayThu,
            decimal soTien,
            string hinhThuc)
        {
            return Collect(
                maPhieu,
                maHoaDon,
                ngayThu,
                soTien,
                hinhThuc,
                "HoaDonDichVu",
                "MaHoaDonDV",
                "TongTien",
                "PhieuThuDichVu",
                "MaPhieuThuDV",
                "MaHoaDonDV"
            );
        }

        // ==========================================
        // 5. THU TIỀN XỬ PHẠT
        // ==========================================
        public static string ThuTienPhat(
            string maPhieu,
            string maPhat,
            DateTime ngayThu,
            decimal soTien,
            string hinhThuc)
        {
            return Collect(
                maPhieu,
                maPhat,
                ngayThu,
                soTien,
                hinhThuc,
                "PhieuXuPhat",
                "MaPhieuPhat",
                "SoTienPhat",
                "PhieuThuXuPhat",
                "MaPhieuThuPhat",
                "MaPhieuPhat"
            );
        }
    }
}
