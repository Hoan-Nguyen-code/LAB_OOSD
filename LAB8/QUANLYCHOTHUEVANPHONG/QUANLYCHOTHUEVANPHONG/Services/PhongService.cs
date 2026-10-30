using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyChoThueVanPhong.Services
{
    public class PhongService : CrudService
    {
        public PhongService()
        {
            Add(new TableSpec(
                "Tầng", "Tang", "MaTang", false,
                F.T("MaTang", "Mã tầng"),
                F.T("TenTang", "Tên tầng"),
                F.M("DoCao", "Độ cao (m)")
            ));

            Add(new TableSpec(
                "Phòng", "Phong", "MaPhong", false,
                F.T("MaPhong", "Mã phòng"),
                F.T("MaTang", "Mã tầng"),
                F.T("SoPhong", "Số phòng"),
                F.M("DienTich", "Diện tích (m²)"),
                F.T("HuongPhong", "Hướng phòng"),
                F.M("DonGiaThue", "Đơn giá thuê"),
                F.C("TrangThai",
                    "Sẵn sàng", "Bảo trì", "Ngừng sử dụng")
            ));
        }

        public override void BeforeSave(
            TableSpec t,
            Dictionary<string, object> d,
            bool exists,
            SqlConnection c,
            SqlTransaction tx)
        {
            if (t.Table == "Tang")
            {
                V.Require(V.S(d, "TenTang"), "Tên tầng");
            }

            if (t.Table == "Phong")
            {
                V.Require(V.S(d, "MaTang"), "Mã tầng");

                if (V.M(d, "DienTich") <= 0)
                    throw new Exception(
                        "Diện tích phòng phải lớn hơn 0.");

                if (V.M(d, "DonGiaThue") <= 0)
                    throw new Exception(
                        "Giá thuê phải lớn hơn 0.");

                V.Require(V.S(d, "HuongPhong"), "Hướng phòng");
            }
        }
    }
}