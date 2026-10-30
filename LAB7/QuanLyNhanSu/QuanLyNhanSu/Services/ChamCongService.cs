
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class ChamCongService
    {
        // =====================================
        // DANH MỤC NHÂN VIÊN
        // =====================================

        public DataTable LayNhanVien()
        {
            return Db.Query(@"
                SELECT MaNV,
                       MaNV + N' - ' + HoTen AS HienThi
                FROM NhanVien
                WHERE TrangThaiLamViec <> N'Nghỉ việc'
                ORDER BY HoTen");
        }

        // =====================================
        // BẢNG CHẤM CÔNG THEO THÁNG
        // =====================================

        public DataTable LayBangChamCong()
        {
            return Db.Query(@"
                SELECT
                    MaBangChamCong,
                    Thang,
                    Nam,
                    NgayLap,
                    MaBangChamCong + N' - Tháng '
                    + CONVERT(NVARCHAR(2), Thang)
                    + N'/' + CONVERT(NVARCHAR(4), Nam)
                    AS HienThi
                FROM BangChamCong
                ORDER BY Nam DESC, Thang DESC");
        }

        public KetQuaXuLy LapBangChamCong(
            string maBang, int thang, int nam)
        {
            if (string.IsNullOrWhiteSpace(maBang))
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã bảng chấm công.");

            if (thang < 1 || thang > 12 ||
                nam < 2000 || nam > 2100)
                return KetQuaXuLy.Fail(
                    "Tháng hoặc năm không hợp lệ.");

            try
            {
                Db.Execute(@"
                    INSERT INTO BangChamCong
                    (MaBangChamCong, Thang, Nam, NgayLap)
                    VALUES (@Ma, @Thang, @Nam,
                            CAST(GETDATE() AS DATE))",
                    Db.P("@Ma", maBang),
                    Db.P("@Thang", thang),
                    Db.P("@Nam", nam));

                return KetQuaXuLy.Ok(
                    "Đã lập bảng chấm công tháng " +
                    thang + "/" + nam + ".");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        public DataTable LayChiTietChamCong(string maBang)
        {
            return Db.Query(@"
                SELECT
                    ct.MaChiTietChamCong,
                    ct.MaBangChamCong,
                    ct.MaNV,
                    nv.HoTen,
                    ct.NgayLam,
                    ct.SoGioLam,
                    ct.TrangThai
                FROM ChiTietChamCong ct
                JOIN NhanVien nv ON nv.MaNV = ct.MaNV
                WHERE ct.MaBangChamCong = @Ma
                ORDER BY ct.NgayLam, nv.HoTen",
                Db.P("@Ma", maBang));
        }

        public KetQuaXuLy ChamCong(
            string maChiTiet,
            string maBang,
            string maNV,
            DateTime ngayLam,
            decimal soGioLam,
            string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maChiTiet) ||
                string.IsNullOrWhiteSpace(maBang) ||
                string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail(
                    "Chưa nhập đủ thông tin chấm công.");

            if (trangThai != "Có mặt" &&
                trangThai != "Vắng mặt" &&
                trangThai != "Nghỉ phép")
                return KetQuaXuLy.Fail(
                    "Trạng thái chấm công không hợp lệ.");

            if (soGioLam < 0 || soGioLam > 24)
                return KetQuaXuLy.Fail(
                    "Số giờ làm phải từ 0 đến 24.");

            if (trangThai != "Có mặt" && soGioLam != 0)
                return KetQuaXuLy.Fail(
                    "Vắng mặt hoặc nghỉ phép phải có 0 giờ làm.");

            if (trangThai == "Có mặt" && soGioLam <= 0)
                return KetQuaXuLy.Fail(
                    "Có mặt phải có số giờ làm lớn hơn 0.");

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr =
                cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    int thang, nam;

                    // Kiểm tra bảng chấm công.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT Thang, Nam
                        FROM BangChamCong WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaBangChamCong = @Ma",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@Ma", maBang));

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read())
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Bảng chấm công không tồn tại.");
                            }

                            thang = rd.GetInt32(0);
                            nam = rd.GetInt32(1);
                        }
                    }

                    if (ngayLam.Month != thang ||
                        ngayLam.Year != nam)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Ngày làm việc không thuộc tháng/năm " +
                            "của bảng chấm công.");
                    }

                    // Kiểm tra nhân viên.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TrangThaiLamViec
                        FROM NhanVien WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaNV = @NV",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        object kq = cmd.ExecuteScalar();

                        if (kq == null ||
                            Convert.ToString(kq) == "Nghỉ việc")
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Nhân viên không tồn tại hoặc đã nghỉ việc.");
                        }
                    }

                    // Có mặt: phải được phân công làm ngày đó.
                    if (trangThai == "Có mặt")
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            SELECT COUNT(*)
                            FROM ChiTietPhanCong
                            WHERE MaNV = @NV
                              AND NgayLamViec = @Ngay",
                            cn, tr))
                        {
                            cmd.Parameters.Add(Db.P("@NV", maNV));
                            cmd.Parameters.Add(Db.P(
                                "@Ngay", ngayLam.Date));

                            if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                            {
                                tr.Rollback();
                                return KetQuaXuLy.Fail(
                                    "Nhân viên chưa có phân công " +
                                    "trong ngày này.");
                            }
                        }
                    }

                    // Không chấm công trùng ngày.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM ChiTietChamCong
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaNV = @NV AND NgayLam = @Ngay",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        cmd.Parameters.Add(Db.P(
                            "@Ngay", ngayLam.Date));

                        if (Convert.ToInt32(cmd.ExecuteScalar()) > 0)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Nhân viên đã được chấm công ngày này.");
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO ChiTietChamCong
                        (
                            MaChiTietChamCong,
                            MaBangChamCong,
                            MaNV, NgayLam,
                            SoGioLam, TrangThai
                        )
                        VALUES
                        (
                            @MaCT, @MaBang,
                            @NV, @Ngay,
                            @Gio, @TrangThai
                        )",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@MaCT", maChiTiet));
                        cmd.Parameters.Add(Db.P("@MaBang", maBang));
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        cmd.Parameters.Add(Db.P("@Ngay", ngayLam.Date));
                        cmd.Parameters.Add(Db.P("@Gio", soGioLam));
                        cmd.Parameters.Add(Db.P("@TrangThai", trangThai));
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();

                    return KetQuaXuLy.Ok(
                        "Đã ghi nhận chấm công thành công.");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(
                        "Không thể chấm công: " + ex.Message);
                }
            }
        }

        // =====================================
        // PHIẾU PHỤ TRỘI
        // =====================================

        public DataTable LayPhieuPhuTroi()
        {
            return Db.Query(@"
                SELECT
                    MaPhieuPhuTroi,
                    NgayPhuTroi,
                    HinhThucPhuTroi,
                    SoGioPhuTroi,
                    MaPhieuPhuTroi + N' - ' +
                    CONVERT(NVARCHAR(10), NgayPhuTroi, 103)
                    AS HienThi
                FROM PhieuPhuTroi
                ORDER BY NgayPhuTroi DESC, MaPhieuPhuTroi");
        }

        public KetQuaXuLy LapPhieuPhuTroi(
            string maPhieu,
            DateTime ngayPhuTroi,
            string hinhThuc,
            decimal soGio)
        {
            if (string.IsNullOrWhiteSpace(maPhieu))
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã phiếu phụ trội.");

            if (hinhThuc != "Tăng ca" &&
                hinhThuc != "Ngày lễ" &&
                hinhThuc != "Ngày nghỉ")
                return KetQuaXuLy.Fail(
                    "Hình thức phụ trội không hợp lệ.");

            if (soGio <= 0 || soGio > 24)
                return KetQuaXuLy.Fail(
                    "Số giờ phụ trội phải lớn hơn 0 và không quá 24.");

            try
            {
                Db.Execute(@"
                    INSERT INTO PhieuPhuTroi
                    (
                        MaPhieuPhuTroi,
                        NgayPhuTroi,
                        HinhThucPhuTroi,
                        SoGioPhuTroi
                    )
                    VALUES (@Ma, @Ngay, @HinhThuc, @Gio)",
                    Db.P("@Ma", maPhieu),
                    Db.P("@Ngay", ngayPhuTroi.Date),
                    Db.P("@HinhThuc", hinhThuc),
                    Db.P("@Gio", soGio));

                return KetQuaXuLy.Ok(
                    "Đã lập phiếu phụ trội thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        public DataTable LayChiTietPhuTroi(string maPhieu)
        {
            return Db.Query(@"
                SELECT
                    ct.MaChiTietPhuTroi,
                    ct.MaPhieuPhuTroi,
                    ct.MaNV,
                    nv.HoTen,
                    ct.SoGioThamGia
                FROM ChiTietPhuTroi ct
                JOIN NhanVien nv ON nv.MaNV = ct.MaNV
                WHERE ct.MaPhieuPhuTroi = @Ma
                ORDER BY nv.HoTen",
                Db.P("@Ma", maPhieu));
        }

        public KetQuaXuLy ThemNhanVienPhuTroi(
            string maChiTiet,
            string maPhieu,
            string maNV,
            decimal soGioThamGia)
        {
            if (string.IsNullOrWhiteSpace(maChiTiet) ||
                string.IsNullOrWhiteSpace(maPhieu) ||
                string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail(
                    "Chưa nhập đủ thông tin phụ trội.");

            if (soGioThamGia <= 0 || soGioThamGia > 24)
                return KetQuaXuLy.Fail(
                    "Số giờ tham gia không hợp lệ.");

            using (SqlConnection cn = Db.OpenConnection())
            using (SqlTransaction tr =
                cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    decimal gioPhieu;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT SoGioPhuTroi
                        FROM PhieuPhuTroi WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaPhieuPhuTroi = @Ma",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@Ma", maPhieu));
                        object o = cmd.ExecuteScalar();

                        if (o == null)
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Phiếu phụ trội không tồn tại.");
                        }

                        gioPhieu = Convert.ToDecimal(o);
                    }

                    if (soGioThamGia > gioPhieu)
                    {
                        tr.Rollback();
                        return KetQuaXuLy.Fail(
                            "Số giờ tham gia không được vượt " +
                            "số giờ của phiếu phụ trội.");
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TrangThaiLamViec
                        FROM NhanVien WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaNV = @NV",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        object o = cmd.ExecuteScalar();

                        if (o == null ||
                            Convert.ToString(o) == "Nghỉ việc")
                        {
                            tr.Rollback();
                            return KetQuaXuLy.Fail(
                                "Nhân viên không tồn tại hoặc đã nghỉ việc.");
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO ChiTietPhuTroi
                        (
                            MaChiTietPhuTroi,
                            MaPhieuPhuTroi,
                            MaNV,
                            SoGioThamGia
                        )
                        VALUES (@CT, @Phieu, @NV, @Gio)",
                        cn, tr))
                    {
                        cmd.Parameters.Add(Db.P("@CT", maChiTiet));
                        cmd.Parameters.Add(Db.P("@Phieu", maPhieu));
                        cmd.Parameters.Add(Db.P("@NV", maNV));
                        cmd.Parameters.Add(Db.P("@Gio", soGioThamGia));

                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit();
                    return KetQuaXuLy.Ok(
                        "Đã ghi nhận nhân viên phụ trội.");
                }
                catch (Exception ex)
                {
                    try { tr.Rollback(); }
                    catch { }

                    return KetQuaXuLy.Fail(
                        "Không thể thêm phụ trội: " + ex.Message);
                }
            }
        }
    }
}
