using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class DichVuService
    {
        // =====================================
        // LẤY CÁC PHIẾU ĐANG Ở
        // =====================================
        public DataTable LayPhieuDangO()
        {
            return Db.Query(
                @"SELECT d.SoPhieuDat,
                         k.HoTen,
                         c.SoPhong
                  FROM PhieuDatPhong d
                  JOIN KhachHang k
                    ON d.MaKhach = k.MaKhach
                  JOIN ChiTietDatPhong c
                    ON d.SoPhieuDat = c.SoPhieuDat
                  WHERE d.TrangThai = N'Đang ở'
                  ORDER BY d.SoPhieuDat, c.SoPhong"
            );
        }

        // =====================================
        // LẤY DANH SÁCH DỊCH VỤ
        // =====================================
        public DataTable LayDichVu()
        {
            return Db.Query(
                @"SELECT *
                  FROM DichVu
                  ORDER BY MaDV"
            );
        }

        // =====================================
        // LẤY LỊCH SỬ SỬ DỤNG DỊCH VỤ
        // =====================================
        public DataTable LayLichSu(string so)
        {
            return Db.Query(
                @"SELECT p.SoPhieuSDDV,
                         p.SoPhong,
                         p.NgaySuDung,
                         d.TenDV,
                         c.SoLuong,
                         c.DonGia,
                         c.ThanhTien
                  FROM PhieuSuDungDV p
                  JOIN ChiTietPhieuSuDungDV c
                    ON p.SoPhieuSDDV = c.SoPhieuSDDV
                  JOIN DichVu d
                    ON c.MaDV = d.MaDV
                  WHERE p.SoPhieuDat = @s
                  ORDER BY p.NgaySuDung,
                           p.SoPhong,
                           d.TenDV",

                new SqlParameter("@s", so)
            );
        }

        // =====================================
        // GHI NHẬN SỬ DỤNG DỊCH VỤ
        // =====================================
        public KetQuaXuLy GhiNhan(
            string soPhieuDat,
            string soPhong,
            DateTime ngay,
            string maNV,
            string maDV,
            int soLuong)
        {
            if (string.IsNullOrWhiteSpace(soPhieuDat) ||
                string.IsNullOrWhiteSpace(soPhong) ||
                string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(maDV) ||
                soLuong <= 0)
            {
                return KetQuaXuLy.Fail(
                    "Thông tin sử dụng dịch vụ không hợp lệ."
                );
            }

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tx = cn.BeginTransaction())
            {
                try
                {
                    // =================================
                    // KIỂM TRA PHIẾU CÓ ĐANG Ở KHÔNG
                    // =================================
                    SqlCommand st = new SqlCommand(
                        @"SELECT TrangThai
                          FROM PhieuDatPhong
                          WHERE SoPhieuDat = @s",
                        cn,
                        tx
                    );

                    st.Parameters.AddWithValue(
                        "@s",
                        soPhieuDat
                    );

                    string trangThai =
                        Convert.ToString(
                            st.ExecuteScalar()
                        );

                    if (trangThai != "Đang ở")
                    {
                        return KetQuaXuLy.Fail(
                            "Chỉ ghi nhận dịch vụ cho phiếu đang lưu trú."
                        );
                    }

                    // =================================
                    // LẤY GIÁ DỊCH VỤ
                    // =================================
                    SqlCommand g = new SqlCommand(
                        @"SELECT DonGia
                          FROM DichVu
                          WHERE MaDV = @d",
                        cn,
                        tx
                    );

                    g.Parameters.AddWithValue(
                        "@d",
                        maDV
                    );

                    object og = g.ExecuteScalar();

                    if (og == null)
                    {
                        return KetQuaXuLy.Fail(
                            "Không tìm thấy dịch vụ."
                        );
                    }

                    decimal gia =
                        Convert.ToDecimal(og);

                    // =================================
                    // TÌM PHIẾU DV CỦA PHÒNG + NGÀY
                    // =================================
                    SqlCommand tim = new SqlCommand(
                        @"SELECT SoPhieuSDDV
                          FROM PhieuSuDungDV
                          WHERE SoPhieuDat = @s
                            AND SoPhong = @p
                            AND NgaySuDung = @n",
                        cn,
                        tx
                    );

                    tim.Parameters.AddWithValue(
                        "@s",
                        soPhieuDat
                    );

                    tim.Parameters.AddWithValue(
                        "@p",
                        soPhong
                    );

                    tim.Parameters.AddWithValue(
                        "@n",
                        ngay.Date
                    );

                    object oSo =
                        tim.ExecuteScalar();

                    string so;

                    // =================================
                    // CHƯA CÓ PHIẾU DV TRONG NGÀY
                    // -> TẠO MỚI
                    // =================================
                    if (oSo == null)
                    {
                        so =
                            "SD" +
                            DateTime.Now.ToString(
                                "yyyyMMddHHmmssfff"
                            );

                        SqlCommand h =
                            new SqlCommand(
                                @"INSERT INTO PhieuSuDungDV
                                  VALUES
                                  (
                                      @so,
                                      @s,
                                      @p,
                                      @n,
                                      @nv
                                  )",
                                cn,
                                tx
                            );

                        h.Parameters.AddWithValue(
                            "@so",
                            so
                        );

                        h.Parameters.AddWithValue(
                            "@s",
                            soPhieuDat
                        );

                        h.Parameters.AddWithValue(
                            "@p",
                            soPhong
                        );

                        h.Parameters.AddWithValue(
                            "@n",
                            ngay.Date
                        );

                        h.Parameters.AddWithValue(
                            "@nv",
                            maNV
                        );

                        h.ExecuteNonQuery();
                    }
                    else
                    {
                        so =
                            Convert.ToString(oSo);
                    }

                    // =================================
                    // KIỂM TRA DỊCH VỤ ĐÃ CÓ CHƯA
                    // =================================
                    SqlCommand chk =
                        new SqlCommand(
                            @"SELECT COUNT(*)
                              FROM ChiTietPhieuSuDungDV
                              WHERE SoPhieuSDDV = @so
                                AND MaDV = @d",
                            cn,
                            tx
                        );

                    chk.Parameters.AddWithValue(
                        "@so",
                        so
                    );

                    chk.Parameters.AddWithValue(
                        "@d",
                        maDV
                    );

                    int daCo =
                        Convert.ToInt32(
                            chk.ExecuteScalar()
                        );

                    // =================================
                    // ĐÃ CÓ -> CỘNG DỒN SỐ LƯỢNG
                    // =================================
                    if (daCo > 0)
                    {
                        SqlCommand u =
                            new SqlCommand(
                                @"UPDATE ChiTietPhieuSuDungDV
                                  SET SoLuong = SoLuong + @sl,
                                      DonGia = @g
                                  WHERE SoPhieuSDDV = @so
                                    AND MaDV = @d",
                                cn,
                                tx
                            );

                        u.Parameters.AddWithValue(
                            "@sl",
                            soLuong
                        );

                        u.Parameters.AddWithValue(
                            "@g",
                            gia
                        );

                        u.Parameters.AddWithValue(
                            "@so",
                            so
                        );

                        u.Parameters.AddWithValue(
                            "@d",
                            maDV
                        );

                        u.ExecuteNonQuery();
                    }

                    // =================================
                    // CHƯA CÓ -> THÊM CHI TIẾT
                    // =================================
                    else
                    {
                        SqlCommand i =
                            new SqlCommand(
                                @"INSERT INTO ChiTietPhieuSuDungDV
                                  VALUES
                                  (
                                      @so,
                                      @d,
                                      @sl,
                                      @g
                                  )",
                                cn,
                                tx
                            );

                        i.Parameters.AddWithValue(
                            "@so",
                            so
                        );

                        i.Parameters.AddWithValue(
                            "@d",
                            maDV
                        );

                        i.Parameters.AddWithValue(
                            "@sl",
                            soLuong
                        );

                        i.Parameters.AddWithValue(
                            "@g",
                            gia
                        );

                        i.ExecuteNonQuery();
                    }

                    tx.Commit();

                    return KetQuaXuLy.Ok(
                        "Đã ghi nhận dịch vụ. " +
                        "Nếu cùng dịch vụ được dùng nhiều lần trong ngày, " +
                        "số lượng được cộng dồn trong cùng phiếu."
                    );
                }
                catch (Exception ex)
                {
                    try
                    {
                        tx.Rollback();
                    }
                    catch
                    {
                    }

                    return KetQuaXuLy.Fail(
                        ex.Message
                    );
                }
            }
        }
    }
}