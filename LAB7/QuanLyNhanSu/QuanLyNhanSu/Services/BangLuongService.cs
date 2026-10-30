
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class BangLuongService
    {
        // Dữ liệu nguồn của bảng lương.
        // Phần thưởng/phạt chỉ lấy chứng từ qua lương.
        private const string SqlNguon = @"
            SELECT
                nv.MaNV,
                nv.HoTen,
                ISNULL(cc.GioLam, 0) AS GioLam,
                ISNULL(pt.GioPhuTroi, 0) AS GioPhuTroi,
                CASE
                    WHEN qdl.MaQDL IS NOT NULL
                        THEN qdl.LuongCoBan
                    ELSE ISNULL(td.LuongThuViec, 0)
                END AS LuongCoBan,
                CASE
                    WHEN qdl.MaQDL IS NOT NULL
                        THEN ISNULL(qdl.PhuCapLuong, 0)
                           + ISNULL(qdl.PhuCapDocHai, 0)
                           + ISNULL(cv.PhuCapChucVu, 0)
                    ELSE 0
                END AS TongPhuCap,
                CASE
                    WHEN qdl.MaQDL IS NOT NULL
                        THEN ISNULL(bh.TongBaoHiem, 0)
                    ELSE 0
                END AS KhoanTrichBaoHiem,
                ISNULL(thuong.TienThuong, 0) AS TienThuong,
                ISNULL(phat.TienPhat, 0) AS TienPhat,
                CASE
                    WHEN qdl.MaQDL IS NOT NULL
                        THEN N'Chính thức'
                    WHEN td.MaQDTD IS NOT NULL
                        THEN N'Thử việc'
                    ELSE N'Chưa có quyết định lương'
                END AS NguonLuong
            FROM
            (
                SELECT MaNV, SUM(SoGioLam) AS GioLam
                FROM ChiTietChamCong
                WHERE NgayLam BETWEEN @TuNgay AND @DenNgay
                GROUP BY MaNV
            ) cc
            JOIN NhanVien nv ON nv.MaNV = cc.MaNV

            OUTER APPLY
            (
                SELECT TOP (1)
                    q.MaQDL, q.LuongCoBan,
                    q.PhuCapLuong, q.PhuCapDocHai
                FROM QuyetDinhLuong q
                WHERE q.MaNV = nv.MaNV
                  AND q.NgayHieuLuc <= @DenNgay
                ORDER BY q.NgayHieuLuc DESC, q.MaQDL DESC
            ) qdl

            OUTER APPLY
            (
                SELECT TOP (1)
                    q.MaQDTD, q.LuongThuViec
                FROM QuyetDinhTuyenDung q
                WHERE q.MaNV = nv.MaNV
                  AND q.NgayBatDauThuViec <= @DenNgay
                  AND q.NgayKetThucThuViec >= @TuNgay
                ORDER BY q.NgayBatDauThuViec DESC,
                         q.MaQDTD DESC
            ) td

            OUTER APPLY
            (
                SELECT TOP (1) q.PhuCapChucVu
                FROM QuyetDinhChucVu q
                WHERE q.MaNV = nv.MaNV
                  AND q.NgayHieuLuc <= @DenNgay
                ORDER BY q.NgayHieuLuc DESC, q.MaQDCV DESC
            ) cv

            OUTER APPLY
            (
                SELECT SUM(ct.SoGioThamGia) AS GioPhuTroi
                FROM ChiTietPhuTroi ct
                JOIN PhieuPhuTroi p
                    ON p.MaPhieuPhuTroi = ct.MaPhieuPhuTroi
                WHERE ct.MaNV = nv.MaNV
                  AND p.NgayPhuTroi
                      BETWEEN @TuNgay AND @DenNgay
            ) pt

            OUTER APPLY
            (
                SELECT TOP (1)
                    b.MucDongBHXH + b.MucDongBHYT
                        AS TongBaoHiem
                FROM HoSoBaoHiem b
                JOIN HopDongLaoDong hd
                    ON hd.MaHopDong = b.MaHopDong
                WHERE hd.MaNV = nv.MaNV
                  AND hd.LoaiHopDong = N'Dài hạn'
                  AND hd.TrangThai <> N'Chấm dứt'
                  AND hd.NgayBatDau <= @DenNgay
                  AND
                  (
                      hd.NgayKetThuc IS NULL
                      OR hd.NgayKetThuc >= @TuNgay
                  )
                  AND b.NgayThamGia <= @DenNgay
                ORDER BY b.NgayThamGia DESC, b.MaBaoHiem DESC
            ) bh

            OUTER APPLY
            (
                SELECT SUM(q.SoTienThuong) AS TienThuong
                FROM QuyetDinhKhenThuong q
                JOIN PhieuChiKhenThuong p
                    ON p.MaQDKT = q.MaQDKT
                WHERE q.MaNV = nv.MaNV
                  AND p.HinhThucChi = N'Qua lương'
                  AND p.NgayChi BETWEEN @TuNgay AND @DenNgay
            ) thuong

            OUTER APPLY
            (
                SELECT SUM(q.SoTienPhat) AS TienPhat
                FROM QuyetDinhKyLuat q
                JOIN PhieuThuNopPhat p
                    ON p.MaQDKL = q.MaQDKL
                WHERE q.MaNV = nv.MaNV
                  AND p.HinhThucThu = N'Trừ lương'
                  AND p.NgayThu BETWEEN @TuNgay AND @DenNgay
            ) phat

            ORDER BY nv.MaNV";

        private static DateTime DauThang(int thang, int nam)
        {
            return new DateTime(nam, thang, 1);
        }

        private static DateTime CuoiThang(int thang, int nam)
        {
            return DauThang(thang, nam).AddMonths(1).AddDays(-1);
        }

        private static decimal LamTron(decimal value)
        {
            return Math.Round(value, 2,
                MidpointRounding.AwayFromZero);
        }

        // ==========================================
        // TÍNH DỰ KIẾN
        // ==========================================

        public DataTable TinhDuKien(
            int thang, int nam, decimal gioCongChuan)
        {
            KiemTraKyLuong(thang, nam, gioCongChuan);

            DateTime tuNgay = DauThang(thang, nam);
            DateTime denNgay = CuoiThang(thang, nam);

            DataTable nguon = Db.Query(
                SqlNguon,
                Db.P("@TuNgay", tuNgay),
                Db.P("@DenNgay", denNgay));

            return TaoBangTinh(nguon, gioCongChuan);
        }

        // ==========================================
        // DANH SÁCH BẢNG LƯƠNG ĐÃ LƯU
        // ==========================================

        public DataTable LayBangLuong()
        {
            return Db.Query(@"
                SELECT
                    MaBangLuong, Thang, Nam,
                    NgayLap, GioCongChuan, TongLuong
                FROM BangLuong
                ORDER BY Nam DESC, Thang DESC");
        }

        public DataTable LayChiTiet(string maBangLuong)
        {
            return Db.Query(@"
                SELECT
                    ct.MaChiTietLuong,
                    ct.MaNV,
                    nv.HoTen,
                    ct.LuongCoBan,
                    ct.TongPhuCap,
                    ct.TienPhuTroi,
                    ct.TienThuong,
                    ct.TienPhat,
                    ct.KhoanTrichBaoHiem,
                    ct.LuongThucLanh
                FROM ChiTietBangLuong ct
                JOIN NhanVien nv ON nv.MaNV = ct.MaNV
                WHERE ct.MaBangLuong = @Ma
                ORDER BY ct.MaNV",
                Db.P("@Ma", maBangLuong));
        }

        // ==========================================
        // LƯU BẢNG LƯƠNG - TRANSACTION
        // ==========================================

        public KetQuaXuLy LapBangLuong(
            int thang, int nam, decimal gioCongChuan)
        {
            try
            {
                KiemTraKyLuong(thang, nam, gioCongChuan);
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }

            string maBang = "BL" +
                nam.ToString("0000") +
                thang.ToString("00");

            DateTime tuNgay = DauThang(thang, nam);
            DateTime denNgay = CuoiThang(thang, nam);

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr =
                cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    // Chặn lập trùng bảng lương tháng.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM BangLuong WITH (UPDLOCK, HOLDLOCK)
                        WHERE Thang = @Thang AND Nam = @Nam",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@Thang", thang));
                        cmd.Parameters.Add(Db.P("@Nam", nam));

                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Bảng lương tháng này đã được lập.");
                        }
                    }

                    DataTable nguon = new DataTable();

                    using (SqlCommand cmd =
                        new SqlCommand(SqlNguon, cn, tr))
                    using (SqlDataAdapter da =
                        new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add(
                            Db.P("@TuNgay", tuNgay));
                        cmd.Parameters.Add(
                            Db.P("@DenNgay", denNgay));

                        da.Fill(nguon);
                    }

                    DataTable ketQua =
                        TaoBangTinh(nguon, gioCongChuan);

                    if (ketQua.Rows.Count == 0)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Chưa có dữ liệu chấm công tháng này.");
                    }

                    decimal tongLuong = 0;

                    // Lưu bảng lương tổng.
                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO BangLuong
                        (
                            MaBangLuong, Thang, Nam,
                            NgayLap, GioCongChuan, TongLuong
                        )
                        VALUES
                        (
                            @Ma, @Thang, @Nam,
                            CAST(GETDATE() AS DATE),
                            @GioChuan, 0
                        )",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@Ma", maBang));
                        cmd.Parameters.Add(Db.P("@Thang", thang));
                        cmd.Parameters.Add(Db.P("@Nam", nam));
                        cmd.Parameters.Add(
                            Db.P("@GioChuan", gioCongChuan));
                        cmd.ExecuteNonQuery();
                    }

                    // Lưu chi tiết từng nhân viên.
                    foreach (DataRow row in ketQua.Rows)
                    {
                        string maNV =
                            Convert.ToString(row["MaNV"]);

                        string maChiTiet = maBang + "_" + maNV;

                        if (maChiTiet.Length > 20)
                            throw new InvalidOperationException(
                                "Mã chi tiết lương vượt quá 20 ký tự. " +
                                "Hãy rút ngắn mã nhân viên.");

                        decimal thucLanh =
                            Convert.ToDecimal(row["LuongThucLanh"]);

                        using (SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO ChiTietBangLuong
                            (
                                MaChiTietLuong, MaBangLuong,
                                MaNV, LuongCoBan, TongPhuCap,
                                TienPhuTroi, TienThuong,
                                TienPhat, KhoanTrichBaoHiem,
                                LuongThucLanh
                            )
                            VALUES
                            (
                                @CT, @Bang, @NV, @Luong,
                                @PhuCap, @PhuTroi, @Thuong,
                                @Phat, @BaoHiem, @ThucLanh
                            )",
                            cn, tr))
                        {
                            cmd.Parameters.Add(
                                Db.P("@CT", maChiTiet));
                            cmd.Parameters.Add(
                                Db.P("@Bang", maBang));
                            cmd.Parameters.Add(
                                Db.P("@NV", maNV));
                            cmd.Parameters.Add(Db.P(
                                "@Luong", row["LuongCoBan"]));
                            cmd.Parameters.Add(Db.P(
                                "@PhuCap", row["TongPhuCap"]));
                            cmd.Parameters.Add(Db.P(
                                "@PhuTroi", row["TienPhuTroi"]));
                            cmd.Parameters.Add(Db.P(
                                "@Thuong", row["TienThuong"]));
                            cmd.Parameters.Add(Db.P(
                                "@Phat", row["TienPhat"]));
                            cmd.Parameters.Add(Db.P(
                                "@BaoHiem", row["KhoanTrichBaoHiem"]));
                            cmd.Parameters.Add(Db.P(
                                "@ThucLanh", thucLanh));

                            cmd.ExecuteNonQuery();
                        }

                        tongLuong += thucLanh;
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE BangLuong
                        SET TongLuong = @Tong
                        WHERE MaBangLuong = @Ma",
                        cn, tr))
                    {
                        cmd.Parameters.Add(
                            Db.P("@Tong", tongLuong));
                        cmd.Parameters.Add(Db.P("@Ma", maBang));
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();

                    return KetQuaXuLy.Ok(
                        "Đã lập bảng lương " + maBang +
                        ". Tổng thực lãnh: " +
                        tongLuong.ToString("N0") + " đồng.");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(
                        "Không thể lập bảng lương: " + ex.Message);
                }
            }
        }

        // ==========================================
        // TÍNH TOÁN TRÊN DỮ LIỆU ĐÃ TỔNG HỢP
        // ==========================================

        private DataTable TaoBangTinh(
            DataTable nguon, decimal gioCongChuan)
        {
            DataTable result = new DataTable();

            result.Columns.Add("MaNV", typeof(string));
            result.Columns.Add("HoTen", typeof(string));
            result.Columns.Add("NguonLuong", typeof(string));
            result.Columns.Add("GioLam", typeof(decimal));
            result.Columns.Add("GioPhuTroi", typeof(decimal));
            result.Columns.Add("LuongCoBan", typeof(decimal));
            result.Columns.Add("LuongTheoCong", typeof(decimal));
            result.Columns.Add("TongPhuCap", typeof(decimal));
            result.Columns.Add("TienPhuTroi", typeof(decimal));
            result.Columns.Add("TienThuong", typeof(decimal));
            result.Columns.Add("TienPhat", typeof(decimal));
            result.Columns.Add("KhoanTrichBaoHiem", typeof(decimal));
            result.Columns.Add("LuongThucLanh", typeof(decimal));

            foreach (DataRow r in nguon.Rows)
            {
                string maNV = Convert.ToString(r["MaNV"]);
                string hoTen = Convert.ToString(r["HoTen"]);
                string nguonLuong =
                    Convert.ToString(r["NguonLuong"]);

                if (nguonLuong == "Chưa có quyết định lương")
                    throw new InvalidOperationException(
                        "Nhân viên " + maNV +
                        " chưa có quyết định lương hoặc thử việc " +
                        "phù hợp với kỳ tính.");

                decimal gioLam =
                    Convert.ToDecimal(r["GioLam"]);

                decimal gioPhuTroi =
                    Convert.ToDecimal(r["GioPhuTroi"]);

                decimal luongCoBan =
                    Convert.ToDecimal(r["LuongCoBan"]);

                decimal tongPhuCap =
                    Convert.ToDecimal(r["TongPhuCap"]);

                decimal baoHiem =
                    Convert.ToDecimal(r["KhoanTrichBaoHiem"]);

                decimal thuong =
                    Convert.ToDecimal(r["TienThuong"]);

                decimal phat =
                    Convert.ToDecimal(r["TienPhat"]);

                decimal luongGio =
                    luongCoBan / gioCongChuan;

                decimal luongTheoCong =
                    LamTron(luongGio * gioLam);

                decimal tienPhuTroi =
                    LamTron(luongGio * gioPhuTroi * 1.5M);

                decimal thucLanh = LamTron(
                    luongTheoCong + tongPhuCap +
                    tienPhuTroi + thuong - phat - baoHiem);

                if (thucLanh < 0)
                    throw new InvalidOperationException(
                        "Lương thực lãnh của " + maNV +
                        " âm. Cần kiểm tra khoản khấu trừ.");

                result.Rows.Add(
                    maNV, hoTen, nguonLuong,
                    gioLam, gioPhuTroi,
                    luongCoBan, luongTheoCong,
                    tongPhuCap, tienPhuTroi,
                    thuong, phat, baoHiem, thucLanh);
            }

            return result;
        }

        private static void KiemTraKyLuong(
            int thang, int nam, decimal gioCongChuan)
        {
            if (thang < 1 || thang > 12 ||
                nam < 2000 || nam > 2100)
                throw new ArgumentException(
                    "Tháng hoặc năm không hợp lệ.");

            if (gioCongChuan <= 0 || gioCongChuan > 744)
                throw new ArgumentException(
                    "Giờ công chuẩn phải lớn hơn 0 " +
                    "và không quá 744 giờ.");
        }
    }
}
