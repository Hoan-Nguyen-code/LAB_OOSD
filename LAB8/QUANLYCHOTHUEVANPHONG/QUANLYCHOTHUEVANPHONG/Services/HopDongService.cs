using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class HopDongService : CrudService
    {
        public HopDongService()
        {
            Add(new TableSpec(
                "Hợp đồng", "HopDongThue",
                "MaHopDong", true));

            Add(new TableSpec(
                "Chi tiết phòng thuê", "ChiTietHopDong",
                "MaChiTietHD", true));

            Add(new TableSpec(
                "Hóa đơn trả trước", "HoaDonTienThue",
                "MaHoaDonThue", true));
        }

        public string LapHopDong(
            string maHD, string maKH,
            DateTime ngayKy, DateTime ngayBatDau,
            int soThang, string danhSachPhong)
        {
            maHD = V.Id(maHD);
            V.Require(maKH, "Mã khách hàng");

            if (soThang < 6)
                throw new Exception(
                    "Thời hạn thuê tối thiểu là 6 tháng.");

            string[] rooms = danhSachPhong
                .Split(new[] { ',', ';' },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();

            if (rooms.Length == 0)
                throw new Exception("Phải chọn ít nhất một phòng.");

            if (rooms.Distinct(StringComparer.OrdinalIgnoreCase)
                     .Count() != rooms.Length)
                throw new Exception(
                    "Danh sách có mã phòng bị trùng.");

            DateTime bd = ngayBatDau.Date;
            DateTime kt = bd.AddMonths(soThang).AddDays(-1);

            return Db.Transaction<string>((c, tx) =>
            {
                if (Count(c, tx,
                    "SELECT COUNT(*) FROM dbo.KhachHang " +
                    "WHERE MaKH=@id",
                    Db.P("@id", maKH)) == 0)
                    throw new Exception(
                        "Khách hàng không tồn tại.");

                if (Count(c, tx,
                    "SELECT COUNT(*) FROM dbo.HopDongThue " +
                    "WHERE MaHopDong=@id",
                    Db.P("@id", maHD)) > 0)
                    throw new Exception(
                        "Mã hợp đồng đã tồn tại.");

                var prices = new List<decimal>();

                foreach (string room in rooms)
                {
                    DataTable dt = Db.Query(c, tx,
                        "SELECT DonGiaThue, TrangThai " +
                        "FROM dbo.Phong WITH (UPDLOCK,HOLDLOCK) " +
                        "WHERE MaPhong=@p",
                        Db.P("@p", room));

                    if (dt.Rows.Count == 0)
                        throw new Exception(
                            "Không tìm thấy phòng " + room);

                    if (Convert.ToString(
                        dt.Rows[0]["TrangThai"]) != "Sẵn sàng")
                        throw new Exception(
                            "Phòng " + room + " đang bảo trì " +
                            "hoặc ngừng sử dụng.");

                    int overlap = Count(c, tx,
                        @"SELECT COUNT(*)
                          FROM dbo.ChiTietHopDong ct WITH (UPDLOCK,HOLDLOCK)
                          JOIN dbo.HopDongThue hd
                            ON hd.MaHopDong=ct.MaHopDong
                          WHERE ct.MaPhong=@p
                            AND hd.TrangThai<>N'Chấm dứt'
                            AND ct.NgayBatDau<=@kt
                            AND ct.NgayKetThuc>=@bd",
                        Db.P("@p", room),
                        Db.P("@bd", bd),
                        Db.P("@kt", kt));

                    if (overlap > 0)
                        throw new Exception(
                            "Phòng " + room +
                            " đã được thuê trong khoảng này.");

                    prices.Add(Convert.ToDecimal(
                        dt.Rows[0]["DonGiaThue"]));
                }

                Db.Execute(c, tx,
                    @"INSERT INTO dbo.HopDongThue
                      (MaHopDong,MaKH,NgayKy,NgayBatDau,
                       ThoiHanThue,NgayKetThuc,TrangThai,NoiDung)
                      VALUES
                      (@id,@kh,@ky,@bd,@thang,@kt,
                       N'Có hiệu lực',@nd)",
                    Db.P("@id", maHD),
                    Db.P("@kh", maKH),
                    Db.P("@ky", ngayKy.Date),
                    Db.P("@bd", bd),
                    Db.P("@thang", soThang),
                    Db.P("@kt", kt),
                    Db.P("@nd", "Thuê " +
                        string.Join(", ", rooms)));

                decimal tongThang = prices.Sum();
                decimal traTruoc = tongThang * 6m;

                string maHoaDon = V.Id("HDT" + maHD);

                Db.Execute(c, tx,
                    @"INSERT INTO dbo.HoaDonTienThue
                      (MaHoaDonThue,MaHopDong,SoKyBatDau,
                       SoKyKetThuc,NgayLap,HanThanhToan,
                       TongTien,TrangThai)
                      VALUES
                      (@id,@hd,1,6,@lap,@han,@tien,
                       N'Chưa thanh toán')",
                    Db.P("@id", maHoaDon),
                    Db.P("@hd", maHD),
                    Db.P("@lap", ngayKy.Date),
                    Db.P("@han", bd),
                    Db.P("@tien", traTruoc));

                for (int i = 0; i < rooms.Length; i++)
                {
                    string maCT = V.Id(
                        "CT" + maHD + "_" + (i + 1));

                    string maCTHoaDon = V.Id(
                        "TT" + maHD + "_" + (i + 1));

                    Db.Execute(c, tx,
                        @"INSERT INTO dbo.ChiTietHopDong
                          (MaChiTietHD,MaHopDong,MaPhong,
                           DonGiaThue,NgayBatDau,NgayKetThuc)
                          VALUES (@ct,@hd,@p,@gia,@bd,@kt)",
                        Db.P("@ct", maCT),
                        Db.P("@hd", maHD),
                        Db.P("@p", rooms[i]),
                        Db.P("@gia", prices[i]),
                        Db.P("@bd", bd),
                        Db.P("@kt", kt));

                    Db.Execute(c, tx,
                        @"INSERT INTO dbo.ChiTietHoaDonThue
                          (MaCTHDThue,MaHoaDonThue,
                           MaChiTietHD,ThanhTien)
                          VALUES (@ct,@hdt,@cthd,@tien)",
                        Db.P("@ct", maCTHoaDon),
                        Db.P("@hdt", maHoaDon),
                        Db.P("@cthd", maCT),
                        Db.P("@tien", prices[i] * 6m));
                }

                return "Lập hợp đồng " + maHD +
                    " thành công.\nTiền thuê mỗi tháng: " +
                    tongThang.ToString("N0") +
                    " đồng\nPhải trả trước 6 tháng: " +
                    traTruoc.ToString("N0") + " đồng.";
            });
        }
    }
}