using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyChoThueVanPhong.Services
{
    public class TheXeService : CrudService
    {
        public TheXeService()
        {
            Add(new TableSpec(
                "Phương tiện", "PhuongTien",
                "MaPhuongTien", false,
                F.T("MaPhuongTien"),
                F.T("MaNV"),
                F.T("BienSoXe"),
                F.T("LoaiXe"),
                F.T("MauXe")
            ));

            Add(new TableSpec(
                "Thẻ giữ xe", "TheGiuXe",
                "MaThe", false,
                F.T("MaThe"),
                F.T("MaNV"),
                F.T("MaPhuongTien"),
                F.D("NgayCap"),
                F.C("TrangThai",
                    "Đang sử dụng", "Đã hủy")
            ));
        }

        public override void BeforeSave(
            TableSpec t,
            Dictionary<string, object> d,
            bool exists,
            SqlConnection c,
            SqlTransaction tx)
        {
            if (t.Table == "PhuongTien")
            {
                V.Require(V.S(d, "BienSoXe"),
                    "Biển số xe");
                V.Require(V.S(d, "LoaiXe"),
                    "Loại xe");
            }

            if (t.Table == "TheGiuXe")
            {
                V.Require(V.S(d, "MaNV"),
                    "Mã nhân viên");
                V.Require(V.S(d, "MaPhuongTien"),
                    "Mã phương tiện");
            }
        }
    }
}