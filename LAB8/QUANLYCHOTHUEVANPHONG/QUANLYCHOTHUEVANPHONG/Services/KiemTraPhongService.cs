
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class KiemTraPhongService : CrudService
    {
        public KiemTraPhongService()
        {
            // 1. Quản lý kiểm tra phòng
            Add(new TableSpec(
                "Kiểm tra phòng",
                "PhieuKiemTraPhong",
                "MaPhieuKT",
                false,
                F.T("MaPhieuKT", "Mã phiếu kiểm tra"),
                F.T("MaPhong", "Mã phòng"),
                F.D("NgayKiemTra", "Ngày kiểm tra"),
                F.T("TinhTrang", "Tình trạng phòng"),
                F.T("KetQua", "Kết quả kiểm tra"),
                F.T("NguoiKiemTra", "Người kiểm tra")
            ));

            // 2. Quản lý sửa chữa
            Add(new TableSpec(
                "Sửa chữa",
                "PhieuSuaChua",
                "MaPhieuSC",
                false,
                F.T("MaPhieuSC", "Mã phiếu sửa chữa"),
                F.T("MaPhieuKT", "Mã phiếu kiểm tra"),
                F.D("NgayLap", "Ngày lập"),
                F.T("NoiDungSuaChua", "Nội dung sửa chữa"),
                F.M("ChiPhiDuKien", "Chi phí dự kiến"),
                F.C("TrangThai",
                    "Chưa thực hiện",
                    "Đang sửa",
                    "Hoàn thành")
            ));

            // 3. Quản lý xử phạt
            Add(new TableSpec(
                "Xử phạt",
                "PhieuXuPhat",
                "MaPhieuPhat",
                false,
                F.T("MaPhieuPhat", "Mã phiếu xử phạt"),
                F.T("MaPhieuKT", "Mã phiếu kiểm tra"),
                F.T("MaChiTietHD", "Mã chi tiết hợp đồng"),
                F.D("NgayLap", "Ngày lập"),
                F.T("LyDo", "Lý do xử phạt"),
                F.M("SoTienPhat", "Số tiền phạt"),
                F.C("TrangThai",
                    "Chưa thanh toán")
            ));

            // 4. Danh sách phiếu thu tiền phạt
            // Chỉ xem; muốn tạo dùng nút Thu tiền xử phạt.
            Add(new TableSpec(
                "Phiếu thu phạt",
                "PhieuThuXuPhat",
                "MaPhieuThuPhat",
                true
            ));
        }

        public override void BeforeSave(
            TableSpec t,
            Dictionary<string, object> d,
            bool exists,
            SqlConnection c,
            SqlTransaction tx)
        {
            // =====================================
            // 1. KIỂM TRA PHIẾU KIỂM TRA PHÒNG
            // =====================================
            if (t.Table == "PhieuKiemTraPhong")
            {
                string maPhong =
                    V.S(d, "MaPhong");

                string tinhTrang =
                    V.S(d, "TinhTrang");

                string ketQua =
                    V.S(d, "KetQua");

                string nguoiKiemTra =
                    V.S(d, "NguoiKiemTra");

                V.Require(maPhong, "Mã phòng");
                V.Require(tinhTrang, "Tình trạng phòng");
                V.Require(ketQua, "Kết quả kiểm tra");
                V.Require(nguoiKiemTra, "Người kiểm tra");

                DateTime ngayKiemTra =
                    V.D(d, "NgayKiemTra");

                // Kiểm tra phòng tồn tại
                int soPhong = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.Phong
                      WHERE MaPhong = @p",
                    Db.P("@p", maPhong));

                if (soPhong == 0)
                {
                    throw new Exception(
                        "Mã phòng không tồn tại.");
                }

                // Không cho sửa phiếu kiểm tra
                // đã phát sinh sửa chữa hoặc xử phạt.
                if (exists)
                {
                    string maKT =
                        V.S(d, "MaPhieuKT");

                    int soLienKet = Count(c, tx,
                        @"SELECT
                          (SELECT COUNT(*)
                           FROM dbo.PhieuSuaChua
                           WHERE MaPhieuKT = @id)
                          +
                          (SELECT COUNT(*)
                           FROM dbo.PhieuXuPhat
                           WHERE MaPhieuKT = @id)",
                        Db.P("@id", maKT));

                    if (soLienKet > 0)
                    {
                        throw new Exception(
                            "Phiếu kiểm tra đã có hồ sơ " +
                            "sửa chữa hoặc xử phạt. " +
                            "Không được chỉnh sửa trực tiếp.");
                    }
                }

                return;
            }

            // =====================================
            // 2. KIỂM TRA PHIẾU SỬA CHỮA
            // =====================================
            if (t.Table == "PhieuSuaChua")
            {
                string maKT =
                    V.S(d, "MaPhieuKT");

                string noiDung =
                    V.S(d, "NoiDungSuaChua");

                V.Require(maKT, "Mã phiếu kiểm tra");

                V.Require(
                    noiDung,
                    "Nội dung sửa chữa");

                decimal chiPhi =
                    V.M(d, "ChiPhiDuKien");

                if (chiPhi < 0)
                {
                    throw new Exception(
                        "Chi phí sửa chữa không được âm.");
                }

                DataTable dt = Db.Query(c, tx,
                    @"SELECT NgayKiemTra
                      FROM dbo.PhieuKiemTraPhong
                      WHERE MaPhieuKT = @id",
                    Db.P("@id", maKT));

                if (dt.Rows.Count == 0)
                {
                    throw new Exception(
                        "Phiếu kiểm tra không tồn tại.");
                }

                DateTime ngayKiemTra =
                    Convert.ToDateTime(
                        dt.Rows[0]["NgayKiemTra"]).Date;

                DateTime ngayLap =
                    V.D(d, "NgayLap");

                if (ngayLap < ngayKiemTra)
                {
                    throw new Exception(
                        "Ngày lập phiếu sửa chữa " +
                        "không được trước ngày kiểm tra.");
                }

                // Không chuyển phiếu đang hoàn thành
                // trở lại trạng thái đang sửa.
                if (exists)
                {
                    object oldStatus = Db.Scalar(c, tx,
                        @"SELECT TrangThai
                          FROM dbo.PhieuSuaChua
                          WHERE MaPhieuSC = @id",
                        Db.P("@id",
                            V.S(d, "MaPhieuSC")));

                    string trangThaiCu =
                        Convert.ToString(oldStatus);

                    string trangThaiMoi =
                        V.S(d, "TrangThai");

                    if (trangThaiCu == "Hoàn thành" &&
                        trangThaiMoi != "Hoàn thành")
                    {
                        throw new Exception(
                            "Phiếu sửa chữa đã hoàn thành. " +
                            "Không thể chuyển về trạng thái cũ.");
                    }
                }

                return;
            }

            // =====================================
            // 3. KIỂM TRA PHIẾU XỬ PHẠT
            // =====================================
            if (t.Table == "PhieuXuPhat")
            {
                string maPhieuPhat =
                    V.S(d, "MaPhieuPhat");

                string maKT =
                    V.S(d, "MaPhieuKT");

                string maChiTietHD =
                    V.S(d, "MaChiTietHD");

                string lyDo =
                    V.S(d, "LyDo");

                string trangThai =
                    V.S(d, "TrangThai");

                decimal soTienPhat =
                    V.M(d, "SoTienPhat");

                DateTime ngayLap =
                    V.D(d, "NgayLap");

                // 3.1. Kiểm tra dữ liệu bắt buộc
                V.Require(
                    maPhieuPhat,
                    "Mã phiếu xử phạt");

                V.Require(
                    maKT,
                    "Mã phiếu kiểm tra");

                V.Require(
                    maChiTietHD,
                    "Mã chi tiết hợp đồng");

                V.Require(
                    lyDo,
                    "Lý do xử phạt");

                if (soTienPhat <= 0)
                {
                    throw new Exception(
                        "Số tiền xử phạt phải lớn hơn 0.");
                }

                // 3.2. Không cho sửa phiếu phạt
                // đã được tạo.
                if (exists)
                {
                    throw new Exception(
                        "Phiếu xử phạt đã tồn tại. " +
                        "Không được sửa trực tiếp " +
                        "quyết định xử phạt.");
                }

                // 3.3. Phiếu mới phải chưa thanh toán
                if (trangThai != "Chưa thanh toán")
                {
                    throw new Exception(
                        "Phiếu xử phạt mới phải có " +
                        "trạng thái Chưa thanh toán.");
                }

                // 3.4. Lấy thông tin kiểm tra
                DataTable kiemTra = Db.Query(c, tx,
                    @"SELECT MaPhong,
                             NgayKiemTra,
                             TinhTrang,
                             KetQua
                      FROM dbo.PhieuKiemTraPhong
                      WHERE MaPhieuKT = @id",
                    Db.P("@id", maKT));

                if (kiemTra.Rows.Count == 0)
                {
                    throw new Exception(
                        "Phiếu kiểm tra không tồn tại.");
                }

                DataRow rowKT = kiemTra.Rows[0];

                string maPhongKiemTra =
                    Convert.ToString(
                        rowKT["MaPhong"]);

                DateTime ngayKiemTra =
                    Convert.ToDateTime(
                        rowKT["NgayKiemTra"]).Date;

                // 3.5. Ngày phạt phải từ ngày kiểm tra
                if (ngayLap < ngayKiemTra)
                {
                    throw new Exception(
                        "Ngày lập phiếu xử phạt " +
                        "không được trước ngày kiểm tra.");
                }

                // 3.6. Kiểm tra hồ sơ kiểm tra
                // có ghi nhận dấu hiệu vi phạm.
                string moTa =
                    Convert.ToString(
                        rowKT["TinhTrang"]) + " " +
                    Convert.ToString(
                        rowKT["KetQua"]);

                bool coViPham =
                    moTa.IndexOf(
                        "vi phạm",
                        StringComparison.OrdinalIgnoreCase) >= 0
                    ||
                    moTa.IndexOf(
                        "kết cấu",
                        StringComparison.OrdinalIgnoreCase) >= 0
                    ||
                    moTa.IndexOf(
                        "tự ý",
                        StringComparison.OrdinalIgnoreCase) >= 0;

                if (!coViPham)
                {
                    throw new Exception(
                        "Phiếu kiểm tra chưa ghi nhận " +
                        "vi phạm thay đổi kết cấu phòng.");
                }

                // 3.7. Kiểm tra hợp đồng và phòng thuê
                DataTable hopDong = Db.Query(c, tx,
                    @"SELECT
                          ct.MaPhong,
                          ct.NgayBatDau,
                          ct.NgayKetThuc
                      FROM dbo.ChiTietHopDong ct
                      WHERE ct.MaChiTietHD = @ct",
                    Db.P("@ct", maChiTietHD));

                if (hopDong.Rows.Count == 0)
                {
                    throw new Exception(
                        "Chi tiết hợp đồng không tồn tại.");
                }

                DataRow rowHD = hopDong.Rows[0];

                string maPhongThue =
                    Convert.ToString(
                        rowHD["MaPhong"]);

                DateTime ngayBatDauThue =
                    Convert.ToDateTime(
                        rowHD["NgayBatDau"]).Date;

                DateTime ngayKetThucThue =
                    Convert.ToDateTime(
                        rowHD["NgayKetThuc"]).Date;

                // 3.8. Phòng thuê phải là phòng kiểm tra
                if (maPhongKiemTra != maPhongThue)
                {
                    throw new Exception(
                        "Phòng trong hợp đồng không " +
                        "khớp với phiếu kiểm tra.");
                }

                // 3.9. Kiểm tra ngày vi phạm thuộc
                // khoảng thời gian thuê.
                if (ngayKiemTra < ngayBatDauThue ||
                    ngayKiemTra > ngayKetThucThue)
                {
                    throw new Exception(
                        "Ngày kiểm tra không nằm trong " +
                        "thời gian thuê của hợp đồng.");
                }

                // 3.10. Kiểm tra trùng mã phiếu
                int trungMa = Count(c, tx,
                    @"SELECT COUNT(*)
                      FROM dbo.PhieuXuPhat
                      WHERE MaPhieuPhat = @id",
                    Db.P("@id", maPhieuPhat));

                if (trungMa > 0)
                {
                    throw new Exception(
                        "Mã phiếu xử phạt đã tồn tại.");
                }

                return;
            }
        }

        // ==========================================
        // 4. THU TIỀN XỬ PHẠT
        // ==========================================
        public string ThuTienPhat(
            string maPhieuThu,
            string maPhat,
            DateTime ngayThu,
            decimal soTien,
            string hinhThuc)
        {
            return ThanhToanService.ThuTienPhat(
                maPhieuThu,
                maPhat,
                ngayThu,
                soTien,
                hinhThuc
            );
        }
    }
}
