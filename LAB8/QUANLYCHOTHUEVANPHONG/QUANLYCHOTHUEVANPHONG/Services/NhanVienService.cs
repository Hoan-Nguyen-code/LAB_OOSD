using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class NhanVienService : CrudService
    {
        public NhanVienService()
        {
            Add(new TableSpec(
                "Nhân viên văn phòng",
                "NhanVienVanPhong", "MaNV", false,
                F.T("MaNV"),
                F.T("MaKH", "Mã công ty"),
                F.T("MaPhong"),
                F.T("HoTen"),
                F.D("NgaySinh"),
                F.T("ChucVu"),
                F.T("HinhAnh", "Đường dẫn hình ảnh"),
                F.T("DienThoai"),
                F.C("TrangThai",
                    "Đang làm việc", "Đã nghỉ")
            ));
        }

        public override void BeforeSave(
            TableSpec t,
            Dictionary<string, object> d,
            bool exists,
            SqlConnection c,
            SqlTransaction tx)
        {
            V.Require(V.S(d, "HoTen"), "Họ tên");

            if (Count(c, tx,
                @"SELECT COUNT(*)
                  FROM dbo.ChiTietHopDong ct
                  JOIN dbo.HopDongThue hd
                    ON hd.MaHopDong=ct.MaHopDong
                  WHERE ct.MaPhong=@p
                    AND hd.MaKH=@kh
                    AND hd.TrangThai<>N'Chấm dứt'",
                Db.P("@p", V.S(d, "MaPhong")),
                Db.P("@kh", V.S(d, "MaKH"))) == 0)
                throw new Exception(
                    "Phòng này chưa có hợp đồng " +
                    "thuê hợp lệ với công ty được chọn.");
        }
    }
}