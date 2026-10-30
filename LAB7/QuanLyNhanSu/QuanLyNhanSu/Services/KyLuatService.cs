
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class KyLuatService
    {
        // ====================================
        // DANH MỤC
        // ====================================

        public DataTable LayNhanVien()
        {
            return Db.Query(@"
                SELECT
                    MaNV,
                    MaNV + N' - ' + HoTen AS HienThi
                FROM NhanVien
                ORDER BY HoTen");
        }

        public DataTable LayPhongBan()
        {
            return Db.Query(@"
                SELECT
                    MaPhongBan,
                    MaPhongBan + N' - ' + TenPhongBan AS HienThi
                FROM PhongBan
                ORDER BY TenPhongBan");
        }

        // ====================================
        // QUYẾT ĐỊNH KỶ LUẬT
        // ====================================

        public DataTable LayQuyetDinh(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    q.MaQDKL,
                    CASE
                        WHEN q.MaNV IS NOT NULL
                            THEN N'Cá nhân'
                        ELSE N'Phòng ban'
                    END AS DoiTuong,
                    q.MaNV,
                    nv.HoTen,
                    q.MaPhongBan,
                    pb.TenPhongBan,
                    q.NgayQuyetDinh,
                    q.LyDo,
                    q.SoTienPhat,
                    CASE
                        WHEN p.MaPhieuThu IS NULL
                            THEN N'Chưa lập phiếu thu'
                        ELSE N'Đã lập phiếu thu'
                    END AS TinhTrangThu
                FROM QuyetDinhKyLuat q
                LEFT JOIN NhanVien nv
                    ON nv.MaNV = q.MaNV
                LEFT JOIN PhongBan pb
                    ON pb.MaPhongBan = q.MaPhongBan
                LEFT JOIN PhieuThuNopPhat p
                    ON p.MaQDKL = q.MaQDKL
                WHERE ISNULL(@TuKhoa, N'') = N''
                   OR q.MaQDKL LIKE N'%' + @TuKhoa + N'%'
                   OR nv.HoTen LIKE N'%' + @TuKhoa + N'%'
                   OR pb.TenPhongBan LIKE N'%' + @TuKhoa + N'%'
                   OR q.LyDo LIKE N'%' + @TuKhoa + N'%'
                ORDER BY q.NgayQuyetDinh DESC, q.MaQDKL",
                Db.P("@TuKhoa", tuKhoa));
        }

        public KetQuaXuLy LapQuyetDinh(
            string maQDKL,
            string loaiDoiTuong,
            string maDoiTuong,
            DateTime ngayQuyetDinh,
            string lyDo,
            decimal soTienPhat)
        {
            if (string.IsNullOrWhiteSpace(maQDKL) ||
                string.IsNullOrWhiteSpace(maDoiTuong) ||
                string.IsNullOrWhiteSpace(lyDo))
            {
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã quyết định, " +
                    "đối tượng hoặc lý do kỷ luật.");
            }

            if (loaiDoiTuong != "Cá nhân" &&
                loaiDoiTuong != "Phòng ban")
            {
                return KetQuaXuLy.Fail(
                    "Loại đối tượng không hợp lệ.");
            }

            if (soTienPhat < 0)
            {
                return KetQuaXuLy.Fail(
                    "Số tiền phạt không được âm.");
            }

            string maNV = loaiDoiTuong == "Cá nhân"
                ? maDoiTuong : null;

            string maPhongBan = loaiDoiTuong == "Phòng ban"
                ? maDoiTuong : null;

            try
            {
                Db.Execute(@"
                    INSERT INTO QuyetDinhKyLuat
                    (
                        MaQDKL,
                        MaNV,
                        MaPhongBan,
                        NgayQuyetDinh,
                        LyDo,
                        SoTienPhat
                    )
                    VALUES
                    (
                        @Ma,
                        @NV,
                        @Phong,
                        @Ngay,
                        @LyDo,
                        @Tien
                    )",
                    Db.P("@Ma", maQDKL),
                    Db.P("@NV", maNV),
                    Db.P("@Phong", maPhongBan),
                    Db.P("@Ngay", ngayQuyetDinh.Date),
                    Db.P("@LyDo", lyDo),
                    Db.P("@Tien", soTienPhat));

                return KetQuaXuLy.Ok(
                    "Đã lập quyết định kỷ luật thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(
                    "Không thể lập quyết định: " + ex.Message);
            }
        }

        // ====================================
        // PHIẾU THU NỘP PHẠT
        // ====================================

        public DataTable LayQuyetDinhChuaThu()
        {
            return Db.Query(@"
                SELECT
                    q.MaQDKL,
                    q.MaQDKL + N' - ' +
                    CASE
                        WHEN q.MaNV IS NOT NULL
                            THEN nv.HoTen
                        ELSE pb.TenPhongBan
                    END AS HienThi,
                    q.SoTienPhat,
                    q.NgayQuyetDinh,
                    CASE
                        WHEN q.MaNV IS NOT NULL
                            THEN N'Cá nhân'
                        ELSE N'Phòng ban'
                    END AS DoiTuong
                FROM QuyetDinhKyLuat q
                LEFT JOIN NhanVien nv
                    ON nv.MaNV = q.MaNV
                LEFT JOIN PhongBan pb
                    ON pb.MaPhongBan = q.MaPhongBan
                WHERE q.SoTienPhat > 0
                  AND NOT EXISTS
                  (
                      SELECT 1
                      FROM PhieuThuNopPhat p
                      WHERE p.MaQDKL = q.MaQDKL
                  )
                ORDER BY q.NgayQuyetDinh DESC, q.MaQDKL");
        }

        public DataTable LayPhieuThu()
        {
            return Db.Query(@"
                SELECT
                    p.MaPhieuThu,
                    p.MaQDKL,
                    CASE
                        WHEN q.MaNV IS NOT NULL
                            THEN N'Cá nhân'
                        ELSE N'Phòng ban'
                    END AS DoiTuong,
                    COALESCE(nv.HoTen, pb.TenPhongBan)
                        AS TenDoiTuong,
                    q.LyDo,
                    q.SoTienPhat,
                    p.NgayThu,
                    p.SoTienThu,
                    p.HinhThucThu
                FROM PhieuThuNopPhat p
                JOIN QuyetDinhKyLuat q
                    ON q.MaQDKL = p.MaQDKL
                LEFT JOIN NhanVien nv
                    ON nv.MaNV = q.MaNV
                LEFT JOIN PhongBan pb
                    ON pb.MaPhongBan = q.MaPhongBan
                ORDER BY p.NgayThu DESC, p.MaPhieuThu");
        }

        public KetQuaXuLy LapPhieuThu(
            string maPhieuThu,
            string maQDKL,
            DateTime ngayThu,
            decimal soTienThu,
            string hinhThucThu)
        {
            if (string.IsNullOrWhiteSpace(maPhieuThu) ||
                string.IsNullOrWhiteSpace(maQDKL))
            {
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã phiếu thu hoặc chọn quyết định.");
            }

            if (hinhThucThu != "Tiền mặt" &&
                hinhThucThu != "Chuyển khoản" &&
                hinhThucThu != "Trừ lương")
            {
                return KetQuaXuLy.Fail(
                    "Hình thức thu không hợp lệ.");
            }

            if (soTienThu <= 0)
            {
                return KetQuaXuLy.Fail(
                    "Số tiền thu phải lớn hơn 0.");
            }

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr =
                cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    decimal tienQuyetDinh;
                    DateTime ngayQuyetDinh;
                    bool laPhongBan;
                    string maNV;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT
                            SoTienPhat,
                            NgayQuyetDinh,
                            MaPhongBan,
                            MaNV
                        FROM QuyetDinhKyLuat
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaQDKL = @MaQD",
                        cn, tr))
                    {
                        cmd.Parameters.Add(
                            Db.P("@MaQD", maQDKL));

                        using (SqlDataReader rd =
                            cmd.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Không tìm thấy quyết định kỷ luật.");
                            }

                            tienQuyetDinh = rd.GetDecimal(0);
                            ngayQuyetDinh = rd.GetDateTime(1);
                            laPhongBan = !rd.IsDBNull(2);
                            maNV = rd.IsDBNull(3)
                                ? null : rd.GetString(3);
                        }
                    }

                    if (ngayThu.Date < ngayQuyetDinh.Date)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Ngày thu không được trước ngày quyết định.");
                    }

                    // Một phiếu thu thanh toán trọn số tiền.
                    if (soTienThu != tienQuyetDinh)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Số tiền thu phải bằng số tiền " +
                            "trên quyết định: " +
                            tienQuyetDinh.ToString("N0") + " đồng.");
                    }

                    // Chưa có quy tắc phân bổ tiền phạt
                    // phòng ban cho từng nhân viên.
                    if (laPhongBan && hinhThucThu == "Trừ lương")
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Chưa hỗ trợ trừ lương " +
                            "đối với quyết định kỷ luật phòng ban.");
                    }

                    // Không cho ghi khoản trừ lương mới
                    // vào kỳ lương đã được lập.
                    if (hinhThucThu == "Trừ lương")
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*)
                            FROM BangLuong WITH (UPDLOCK, HOLDLOCK)
                            WHERE Thang = @Thang
                              AND Nam = @Nam",
                            cn, tr))
                        {
                            cmd.Parameters.Add(
                                Db.P("@Thang", ngayThu.Month));
                            cmd.Parameters.Add(
                                Db.P("@Nam", ngayThu.Year));

                            if (Convert.ToInt32(
                                cmd.ExecuteScalar()) > 0)
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Bảng lương tháng này đã được lập. " +
                                    "Không thể bổ sung phiếu trừ lương.");
                            }
                        }

                        // Phải có nguồn lương trong kỳ.
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*)
                            FROM NhanVien
                            WHERE MaNV = @NV
                              AND TrangThaiLamViec <> N'Nghỉ việc'",
                            cn, tr))
                        {
                            cmd.Parameters.Add(Db.P("@NV", maNV));

                            if (Convert.ToInt32(
                                cmd.ExecuteScalar()) == 0)
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Nhân viên không còn làm việc. " +
                                    "Không thể chọn hình thức trừ lương.");
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM PhieuThuNopPhat
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaQDKL = @MaQD",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaQD", maQDKL));

                        if (Convert.ToInt32(
                            cmd.ExecuteScalar()) > 0)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Quyết định đã có phiếu thu.");
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO PhieuThuNopPhat
                        (
                            MaPhieuThu,
                            MaQDKL,
                            NgayThu,
                            SoTienThu,
                            HinhThucThu
                        )
                        VALUES
                        (
                            @MaPhieu,
                            @MaQD,
                            @Ngay,
                            @Tien,
                            @HinhThuc
                        )",
                        cn, tr))
                    {
                        cmd.Parameters.Add(
                            Db.P("@MaPhieu", maPhieuThu));
                        cmd.Parameters.Add(
                            Db.P("@MaQD", maQDKL));
                        cmd.Parameters.Add(
                            Db.P("@Ngay", ngayThu.Date));
                        cmd.Parameters.Add(
                            Db.P("@Tien", soTienThu));
                        cmd.Parameters.Add(
                            Db.P("@HinhThuc", hinhThucThu));

                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();

                    return KetQuaXuLy.Ok(
                        hinhThucThu == "Trừ lương"
                            ? "Đã lập phiếu thu. Khoản phạt " +
                              "sẽ được tính vào kỳ lương tương ứng."
                            : "Đã lập phiếu thu nộp phạt thành công.");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(
                        "Không thể lập phiếu thu: " + ex.Message);
                }
            }
        }
    }
}
