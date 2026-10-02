using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using shopABC.Data;

namespace shopABC.Adapters
{
    public static class EmailAdapter
    {
        // =====================================================
        // MÔ PHỎNG EMAIL SERVICE
        // =====================================================
        public static bool GuiEmailXacNhan(
            string email,
            int maDonHang,
            decimal tongThanhToan,
            out string thongBao)
        {
            // Không có email thì không gửi
            if (string.IsNullOrWhiteSpace(email))
            {
                thongBao =
                    "Khách hàng không có email nên hệ thống " +
                    "không gửi thư xác nhận.";

                return false;
            }

            try
            {
                // =============================================
                // 1. THÔNG TIN ĐƠN HÀNG
                // =============================================
                DataTable dtDonHang =
                    Db.Query(
                        @"SELECT
                              DH.MaDonHang,
                              DH.ThoiDiemDat,
                              KH.HoTen AS NguoiMua,
                              NN.HoTen AS NguoiNhan,
                              NN.DiaChi,
                              NN.DienThoai,
                              LGH.TenLoai,
                              DH.TongTienHang,
                              DH.ChiPhiGiaoHang,
                              DH.PhiGiaoDich,
                              DH.TongThanhToan

                          FROM DONDATHANG DH

                          INNER JOIN KHACHHANG KH
                              ON DH.MaKhachHang =
                                 KH.MaKhachHang

                          INNER JOIN NGUOINHAN NN
                              ON DH.MaNguoiNhan =
                                 NN.MaNguoiNhan

                          INNER JOIN LOAIGIAOHANG LGH
                              ON DH.MaLoai =
                                 LGH.MaLoai

                          WHERE DH.MaDonHang =
                                @MaDonHang",

                        new SqlParameter(
                            "@MaDonHang",
                            maDonHang
                        )
                    );

                if (dtDonHang.Rows.Count == 0)
                {
                    thongBao =
                        "Không tìm thấy đơn hàng để gửi email.";

                    return false;
                }

                // =============================================
                // 2. CHI TIẾT SẢN PHẨM
                // =============================================
                DataTable dtChiTiet =
                    Db.Query(
                        @"SELECT
                              SP.TenSanPham,
                              CT.SoLuong,
                              CT.DonGia,
                              CT.SoLuong * CT.DonGia
                                  AS ThanhTien

                          FROM CHITIETDONHANG CT

                          INNER JOIN SANPHAM SP
                              ON CT.MaSanPham =
                                 SP.MaSanPham

                          WHERE CT.MaDonHang =
                                @MaDonHang

                          ORDER BY SP.TenSanPham",

                        new SqlParameter(
                            "@MaDonHang",
                            maDonHang
                        )
                    );

                DataRow donHang =
                    dtDonHang.Rows[0];

                // =============================================
                // 3. TẠO NỘI DUNG EMAIL
                // =============================================
                StringBuilder noiDung =
                    new StringBuilder();

                noiDung.AppendLine(
                    "XÁC NHẬN ĐƠN HÀNG"
                );

                noiDung.AppendLine(
                    "=============================="
                );

                noiDung.AppendLine(
                    "Mã đơn hàng: " +
                    donHang["MaDonHang"]
                );

                noiDung.AppendLine(
                    "Thời điểm đặt: " +
                    Convert.ToDateTime(
                        donHang["ThoiDiemDat"]
                    ).ToString("dd/MM/yyyy HH:mm:ss")
                );

                noiDung.AppendLine(
                    "Người mua: " +
                    donHang["NguoiMua"]
                );

                noiDung.AppendLine();

                noiDung.AppendLine(
                    "THÔNG TIN NGƯỜI NHẬN"
                );

                noiDung.AppendLine(
                    "Họ tên: " +
                    donHang["NguoiNhan"]
                );

                noiDung.AppendLine(
                    "Địa chỉ: " +
                    donHang["DiaChi"]
                );

                noiDung.AppendLine(
                    "Điện thoại: " +
                    donHang["DienThoai"]
                );

                noiDung.AppendLine();

                noiDung.AppendLine(
                    "Loại giao hàng: " +
                    donHang["TenLoai"]
                );

                noiDung.AppendLine();

                noiDung.AppendLine(
                    "SẢN PHẨM"
                );

                noiDung.AppendLine(
                    "------------------------------"
                );

                foreach (DataRow row in
                         dtChiTiet.Rows)
                {
                    decimal donGia =
                        Convert.ToDecimal(
                            row["DonGia"]
                        );

                    decimal thanhTien =
                        Convert.ToDecimal(
                            row["ThanhTien"]
                        );

                    noiDung.AppendLine(
                        row["TenSanPham"] +
                        " | SL: " +
                        row["SoLuong"] +
                        " | Đơn giá: " +
                        donGia.ToString("N0") +
                        " VNĐ" +
                        " | Thành tiền: " +
                        thanhTien.ToString("N0") +
                        " VNĐ"
                    );
                }

                noiDung.AppendLine(
                    "------------------------------"
                );

                noiDung.AppendLine(
                    "Tiền hàng: " +
                    Convert.ToDecimal(
                        donHang["TongTienHang"]
                    ).ToString("N0") +
                    " VNĐ"
                );

                noiDung.AppendLine(
                    "Phí giao hàng: " +
                    Convert.ToDecimal(
                        donHang["ChiPhiGiaoHang"]
                    ).ToString("N0") +
                    " VNĐ"
                );

                noiDung.AppendLine(
                    "Phí giao dịch: " +
                    Convert.ToDecimal(
                        donHang["PhiGiaoDich"]
                    ).ToString("N0") +
                    " VNĐ"
                );

                noiDung.AppendLine(
                    "TỔNG THANH TOÁN: " +
                    Convert.ToDecimal(
                        donHang["TongThanhToan"]
                    ).ToString("N0") +
                    " VNĐ"
                );

                // =============================================
                // TUYỆT ĐỐI KHÔNG THÊM:
                //
                // SoThe
                // CSV
                // HetHan
                // LoaiThe
                // BonSoCuoi
                //
                // Email xác nhận không chứa dữ liệu thẻ.
                // =============================================

                // =============================================
                // 4. MÔ PHỎNG EMAIL SERVICE
                // =============================================
                // LAB này chưa gửi email thật.
                // Ta mô phỏng việc Email Service nhận
                // nội dung và gửi thành công.

                thongBao =
                    "Email Service đã gửi thư xác nhận tới:\n" +
                    email +
                    "\n\n" +
                    noiDung.ToString();

                return true;
            }
            catch (Exception ex)
            {
                thongBao =
                    "Email Service gặp lỗi: " +
                    ex.Message;

                return false;
            }
        }
    }
}