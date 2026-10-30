using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyChoThueVanPhong.Services
{
    public class KhachHangService : CrudService
    {
        public KhachHangService()
        {
            Add(new TableSpec(
                "Khách hàng", "KhachHang", "MaKH", false,
                F.T("MaKH", "Mã khách hàng"),
                F.T("TenKhachHang", "Tên khách hàng / công ty"),
                F.C("LoaiKhachHang", "Cá nhân", "Công ty"),
                F.T("MaSoThue", "Mã số thuế"),
                F.T("NguoiDaiDien", "Người đại diện"),
                F.T("DienThoai", "Điện thoại"),
                F.T("Email", "Email"),
                F.T("DiaChi", "Địa chỉ")
            ));
        }

        public override void BeforeSave(
            TableSpec t,
            Dictionary<string, object> d,
            bool exists,
            SqlConnection c,
            SqlTransaction tx)
        {
            V.Require(V.S(d, "TenKhachHang"), "Tên khách hàng");

            if (V.S(d, "LoaiKhachHang") == "Công ty")
                V.Require(V.S(d, "NguoiDaiDien"),
                    "Người đại diện công ty");
        }
    }
}