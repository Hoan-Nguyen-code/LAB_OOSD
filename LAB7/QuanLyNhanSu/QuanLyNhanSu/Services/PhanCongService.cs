
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class PhanCongService
    {
        // =========================
        // DANH MỤC CA LÀM VIỆC
        // =========================

        public DataTable LayCaLamViec()
        {
            return Db.Query(@"
                SELECT MaCa, TenCa, GioBatDau, GioKetThuc,
                       MaCa + N' - ' + TenCa AS HienThi
                FROM CaLamViec
                ORDER BY GioBatDau, MaCa");
        }

        public KetQuaXuLy ThemCaLamViec(
            string maCa, string tenCa,
            TimeSpan gioBatDau, TimeSpan gioKetThuc)
        {
            if (string.IsNullOrWhiteSpace(maCa) ||
                string.IsNullOrWhiteSpace(tenCa))
                return KetQuaXuLy.Fail(
                    "Mã ca và tên ca không được để trống.");

            if (gioBatDau >= gioKetThuc)
                return KetQuaXuLy.Fail(
                    "Giờ kết thúc phải sau giờ bắt đầu. " +
                    "Phiên bản này quản lý ca trong cùng một ngày.");

            try
            {
                Db.Execute(@"
                    INSERT INTO CaLamViec
                    (MaCa, TenCa, GioBatDau, GioKetThuc)
                    VALUES (@Ma, @Ten, @BatDau, @KetThuc)",
                    Db.P("@Ma", maCa),
                    Db.P("@Ten", tenCa),
                    Db.P("@BatDau", gioBatDau),
                    Db.P("@KetThuc", gioKetThuc));

                return KetQuaXuLy.Ok(
                    "Đã thêm ca làm việc thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // =========================
        // DANH MỤC PHÒNG BAN, NHÂN VIÊN
        // =========================

        public DataTable LayPhongBan()
        {
            return Db.Query(@"
                SELECT MaPhongBan, TenPhongBan,
                       MaPhongBan + N' - ' + TenPhongBan
                           AS HienThi
                FROM PhongBan
                ORDER BY TenPhongBan");
        }

        public DataTable LayNhanVienTheoPhong(string maPhongBan)
        {
            // Xác định phòng thử việc gần nhất theo
            // quyết định tuyển dụng hiện có.
            return Db.Query(@"
                SELECT nv.MaNV,
                       nv.MaNV + N' - ' + nv.HoTen AS HienThi
                FROM NhanVien nv
                CROSS APPLY
                (
                    SELECT TOP (1) td.MaPhongBan
                    FROM QuyetDinhTuyenDung td
                    WHERE td.MaNV = nv.MaNV
                    ORDER BY td.NgayBatDauThuViec DESC,
                             td.MaQDTD DESC
                ) pb
                WHERE pb.MaPhongBan = @MaPhongBan
                  AND nv.TrangThaiLamViec <> N'Nghỉ việc'
                ORDER BY nv.HoTen",
                Db.P("@MaPhongBan", maPhongBan));
        }

        // =========================
        // KẾ HOẠCH PHÂN CÔNG
        // =========================

        public DataTable LayKeHoach()
        {
            return Db.Query(@"
                SELECT kh.MaKeHoach, kh.MaPhongBan,
                       pb.TenPhongBan,
                       kh.Tuan, kh.Nam, kh.NgayLap,
                       kh.MaKeHoach + N' - ' +
                       pb.TenPhongBan + N' (Tuần ' +
                       CONVERT(NVARCHAR(5), kh.Tuan) +
                       N'/' + CONVERT(NVARCHAR(5), kh.Nam) +
                       N')' AS HienThi
                FROM KeHoachPhanCong kh
                JOIN PhongBan pb
                    ON pb.MaPhongBan = kh.MaPhongBan
                ORDER BY kh.Nam DESC, kh.Tuan DESC,
                         kh.MaKeHoach");
        }

        public KetQuaXuLy LapKeHoach(
            string maKeHoach,
            string maPhongBan,
            int tuan, int nam,
            DateTime ngayLap)
        {
            if (string.IsNullOrWhiteSpace(maKeHoach) ||
                string.IsNullOrWhiteSpace(maPhongBan))
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã kế hoạch hoặc chọn phòng ban.");

            if (tuan < 1 || tuan > 53 ||
                nam < 2000 || nam > 2100)
                return KetQuaXuLy.Fail(
                    "Tuần hoặc năm không hợp lệ.");

            try
            {
                Db.Execute(@"
                    INSERT INTO KeHoachPhanCong
                    (MaKeHoach, MaPhongBan, Tuan, Nam, NgayLap)
                    VALUES (@Ma, @Phong, @Tuan, @Nam, @Ngay)",
                    Db.P("@Ma", maKeHoach),
                    Db.P("@Phong", maPhongBan),
                    Db.P("@Tuan", tuan),
                    Db.P("@Nam", nam),
                    Db.P("@Ngay", ngayLap.Date));

                return KetQuaXuLy.Ok(
                    "Đã lập kế hoạch phân công thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // =========================
        // CHI TIẾT PHÂN CÔNG
        // =========================

        public DataTable LayChiTiet(string maKeHoach)
        {
            return Db.Query(@"
                SELECT ct.MaChiTietPhanCong,
                       ct.MaKeHoach,
                       ct.MaNV, nv.HoTen,
                       ct.MaCa, ca.TenCa,
                       ct.NgayLamViec,
                       ca.GioBatDau, ca.GioKetThuc
                FROM ChiTietPhanCong ct
                JOIN NhanVien nv ON nv.MaNV = ct.MaNV
                JOIN CaLamViec ca ON ca.MaCa = ct.MaCa
                WHERE ct.MaKeHoach = @Ma
                ORDER BY ct.NgayLamViec,
                         ca.GioBatDau, nv.HoTen",
                Db.P("@Ma", maKeHoach));
        }

        public KetQuaXuLy ThemChiTiet(
            string maChiTiet,
            string maKeHoach,
            string maNV,
            string maCa,
            DateTime ngayLamViec)
        {
            if (string.IsNullOrWhiteSpace(maChiTiet) ||
                string.IsNullOrWhiteSpace(maKeHoach) ||
                string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(maCa))
                return KetQuaXuLy.Fail(
                    "Chưa nhập đầy đủ thông tin phân công.");

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr =
                cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    int tuan, nam;
                    string maPhongBan;

                    // 1. Kiểm tra kế hoạch tồn tại
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT Tuan, Nam, MaPhongBan
                        FROM KeHoachPhanCong
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaKeHoach = @Ma", cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@Ma", maKeHoach));

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Kế hoạch không tồn tại.");
                            }

                            tuan = rd.GetInt32(0);
                            nam = rd.GetInt32(1);
                            maPhongBan = rd.GetString(2);
                        }
                    }

                    // 2. Kiểm tra ngày thuộc tuần/năm kế hoạch
                    int tuanNgay, namNgay;
                    LayTuanISO(ngayLamViec.Date,
                               out tuanNgay, out namNgay);

                    if (tuanNgay != tuan || namNgay != nam)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Ngày làm việc không thuộc tuần " +
                            tuan + "/" + nam + " của kế hoạch.");
                    }

                    // 3. Khóa nhân viên, kiểm tra còn làm việc
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TrangThaiLamViec
                        FROM NhanVien WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaNV = @NV", cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        object trangThai = cmd.ExecuteScalar();

                        if (trangThai == null ||
                            Convert.ToString(trangThai) == "Nghỉ việc")
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Nhân viên không tồn tại " +
                                "hoặc đã nghỉ việc.");
                        }
                    }

                    // 4. Kiểm tra phòng ban theo quyết định
                    // tuyển dụng mới nhất.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TOP (1) MaPhongBan
                        FROM QuyetDinhTuyenDung
                        WHERE MaNV = @NV
                        ORDER BY NgayBatDauThuViec DESC,
                                 MaQDTD DESC", cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        object phong = cmd.ExecuteScalar();

                        if (phong == null ||
                            Convert.ToString(phong) != maPhongBan)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Nhân viên không thuộc phòng ban " +
                                "của kế hoạch.");
                        }
                    }

                    // 5. Lấy giờ bắt đầu/kết thúc của ca
                    TimeSpan batDau, ketThuc;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT GioBatDau, GioKetThuc
                        FROM CaLamViec
                        WHERE MaCa = @Ca", cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@Ca", maCa));

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Ca làm việc không tồn tại.");
                            }

                            batDau = rd.GetTimeSpan(0);
                            ketThuc = rd.GetTimeSpan(1);
                        }
                    }

                    if (batDau >= ketThuc)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Ca làm việc không hợp lệ.");
                    }

                    // 6. Chặn trùng giờ với ca khác
                    // của cùng nhân viên, cùng ngày.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM ChiTietPhanCong ct
                            WITH (UPDLOCK, HOLDLOCK)
                        JOIN CaLamViec ca ON ca.MaCa = ct.MaCa
                        WHERE ct.MaNV = @NV
                          AND ct.NgayLamViec = @Ngay
                          AND @BatDau < ca.GioKetThuc
                          AND @KetThuc > ca.GioBatDau",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        cmd.Parameters.Add(Db.P(
                            "@Ngay", ngayLamViec.Date));
                        cmd.Parameters.Add(Db.P("@BatDau", batDau));
                        cmd.Parameters.Add(Db.P("@KetThuc", ketThuc));

                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Nhân viên đã được phân công " +
                                "một ca trùng giờ trong ngày.");
                        }
                    }

                    // 7. Lưu chi tiết
                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO ChiTietPhanCong
                        (
                            MaChiTietPhanCong, MaKeHoach,
                            MaNV, MaCa, NgayLamViec
                        )
                        VALUES (@Ma, @KeHoach, @NV, @Ca, @Ngay)",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@Ma", maChiTiet));
                        cmd.Parameters.Add(Db.P("@KeHoach", maKeHoach));
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        cmd.Parameters.Add(Db.P("@Ca", maCa));
                        cmd.Parameters.Add(Db.P(
                            "@Ngay", ngayLamViec.Date));

                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                    return KetQuaXuLy.Ok(
                        "Đã phân công ca làm việc thành công.");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(
                        "Không thể phân công: " + ex.Message);
                }
            }
        }

        public KetQuaXuLy XoaChiTiet(string maChiTiet)
        {
            if (string.IsNullOrWhiteSpace(maChiTiet))
                return KetQuaXuLy.Fail(
                    "Chưa chọn chi tiết phân công.");

            try
            {
                int n = Db.Execute(@"
                    DELETE FROM ChiTietPhanCong
                    WHERE MaChiTietPhanCong = @Ma",
                    Db.P("@Ma", maChiTiet));

                return n > 0
                    ? KetQuaXuLy.Ok(
                        "Đã gỡ phân công thành công.")
                    : KetQuaXuLy.Fail(
                        "Không tìm thấy chi tiết phân công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // Tuần ISO: bắt đầu từ thứ Hai.
        // Dùng được trên .NET Framework 4.7.2.
        public static void LayTuanISO(
            DateTime ngay, out int tuan, out int nam)
        {
            int thu = ((int)ngay.DayOfWeek + 6) % 7;
            DateTime thuNam = ngay.Date.AddDays(3 - thu);

            nam = thuNam.Year;

            tuan = CultureInfo.InvariantCulture.Calendar
                .GetWeekOfYear(
                    thuNam,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday);
        }
    }
}
