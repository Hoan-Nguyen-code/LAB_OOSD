using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class DichVuService : CrudService
    {
        public DichVuService()
        {
            Add(new TableSpec(
                "Danh mục dịch vụ", "DichVu",
                "MaDichVu", false,
                F.T("MaDichVu"),
                F.T("TenDichVu"),
                F.T("DonViTinh"),
                F.M("DonGia")
            ));

            Add(new TableSpec(
                "Sử dụng dịch vụ", "SuDungDichVu",
                "MaSuDung", false,
                F.T("MaSuDung"),
                F.T("MaChiTietHD"),
                F.T("MaDichVu"),
                F.I("Thang"),
                F.I("Nam"),
                F.M("SoLuong"),
                F.M("DonGia")
            ));

            Add(new TableSpec(
                "Hóa đơn dịch vụ", "HoaDonDichVu",
                "MaHoaDonDV", true));

            Add(new TableSpec(
                "Chi tiết hóa đơn DV", "ChiTietHoaDonDichVu",
                "MaCTHDDV", true));

            Add(new TableSpec(
                "Phiếu thu dịch vụ", "PhieuThuDichVu",
                "MaPhieuThuDV", true));
        }

        public override void BeforeSave(
            TableSpec t,
            Dictionary<string, object> d,
            bool exists,
            SqlConnection c,
            SqlTransaction tx)
        {
            if (t.Table == "DichVu")
            {
                V.Require(V.S(d, "TenDichVu"),
                    "Tên dịch vụ");
                V.Require(V.S(d, "DonViTinh"),
                    "Đơn vị tính");
            }

            if (t.Table == "SuDungDichVu")
            {
                int thang = V.I(d, "Thang");
                int nam = V.I(d, "Nam");

                if (thang < 1 || thang > 12 ||
                    nam < 2000 || nam > 2100)
                    throw new Exception(
                        "Tháng hoặc năm không hợp lệ.");

                if (V.M(d, "SoLuong") <= 0)
                    throw new Exception(
                        "Số lượng phải lớn hơn 0.");

                if (Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.ChiTietHoaDonDichVu ct
                      WHERE ct.MaSuDung=@id",
                    Db.P("@id", V.S(d, "MaSuDung"))) > 0)
                    throw new Exception(
                        "Dịch vụ đã được lập hóa đơn, " +
                        "không được sửa.");

                DataTable thue = Db.Query(c, tx,
                    @"SELECT NgayBatDau,NgayKetThuc
                      FROM dbo.ChiTietHopDong
                      WHERE MaChiTietHD=@ct",
                    Db.P("@ct", V.S(d, "MaChiTietHD")));

                if (thue.Rows.Count == 0)
                    throw new Exception(
                        "Chi tiết thuê không tồn tại.");

                DateTime dau = new DateTime(nam, thang, 1);
                DateTime cuoi =
                    dau.AddMonths(1).AddDays(-1);

                DateTime bd = Convert.ToDateTime(
                    thue.Rows[0]["NgayBatDau"]);
                DateTime kt = Convert.ToDateTime(
                    thue.Rows[0]["NgayKetThuc"]);

                if (cuoi < bd || dau > kt)
                    throw new Exception(
                        "Tháng dịch vụ không thuộc thời gian thuê.");
            }
        }

        public string LapHoaDonDichVu(
            string maHoaDon, string maChiTietHD,
            int thang, int nam, DateTime ngayLap)
        {
            maHoaDon = V.Id(maHoaDon);

            if (thang < 1 || thang > 12 ||
                nam < 2000 || nam > 2100)
                throw new Exception(
                    "Tháng hoặc năm không hợp lệ.");

            DateTime cuoiThang =
                new DateTime(nam, thang, 1)
                .AddMonths(1).AddDays(-1);

            if (ngayLap.Date < cuoiThang)
                throw new Exception(
                    "Hóa đơn dịch vụ chỉ được lập cuối tháng.");

            return Db.Transaction<string>((c, tx) =>
            {
                if (Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.HoaDonDichVu WITH (UPDLOCK,HOLDLOCK)
                      WHERE MaChiTietHD=@ct
                        AND Thang=@t AND Nam=@n",
                    Db.P("@ct", maChiTietHD),
                    Db.P("@t", thang),
                    Db.P("@n", nam)) > 0)
                    throw new Exception(
                        "Đã có hóa đơn dịch vụ cho tháng này.");

                DataTable lines = Db.Query(c, tx,
                    @"SELECT sd.MaSuDung,sd.ThanhTien
                      FROM dbo.SuDungDichVu sd
                      WHERE sd.MaChiTietHD=@ct
                        AND sd.Thang=@t AND sd.Nam=@n
                        AND NOT EXISTS
                        (SELECT 1
                         FROM dbo.ChiTietHoaDonDichVu x
                         WHERE x.MaSuDung=sd.MaSuDung)",
                    Db.P("@ct", maChiTietHD),
                    Db.P("@t", thang),
                    Db.P("@n", nam));

                if (lines.Rows.Count == 0)
                    throw new Exception(
                        "Không có dịch vụ chưa lập hóa đơn.");

                decimal tong = lines.AsEnumerable()
                    .Sum(x => Convert.ToDecimal(
                        x["ThanhTien"]));

                Db.Execute(c, tx,
                    @"INSERT INTO dbo.HoaDonDichVu
                      (MaHoaDonDV,MaChiTietHD,Thang,Nam,
                       NgayLap,HanThanhToan,TongTien,TrangThai)
                      VALUES (@id,@ct,@t,@n,@lap,@han,
                              @tien,N'Chưa thanh toán')",
                    Db.P("@id", maHoaDon),
                    Db.P("@ct", maChiTietHD),
                    Db.P("@t", thang),
                    Db.P("@n", nam),
                    Db.P("@lap", ngayLap.Date),
                    Db.P("@han", cuoiThang),
                    Db.P("@tien", tong));

                int i = 0;
                foreach (DataRow r in lines.Rows)
                {
                    i++;

                    Db.Execute(c, tx,
                        @"INSERT INTO dbo.ChiTietHoaDonDichVu
                          (MaCTHDDV,MaHoaDonDV,
                           MaSuDung,ThanhTien)
                          VALUES (@id,@hd,@sd,@tien)",
                        Db.P("@id", V.Id(
                            "DV" + maHoaDon + "_" + i)),
                        Db.P("@hd", maHoaDon),
                        Db.P("@sd", r["MaSuDung"]),
                        Db.P("@tien", r["ThanhTien"]));
                }

                return "Lập hóa đơn dịch vụ thành công. " +
                    "Tổng tiền: " +
                    tong.ToString("N0") + " đồng.";
            });
        }

        public string ThuTien(
            string maPhieu, string maHoaDon,
            DateTime ngayThu, decimal soTien,
            string hinhThuc)
        {
            return ThanhToanService.ThuTienDichVu(
                maPhieu, maHoaDon,
                ngayThu, soTien, hinhThuc);
        }
    }
}