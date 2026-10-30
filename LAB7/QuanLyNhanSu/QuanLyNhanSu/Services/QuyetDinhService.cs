
using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class QuyetDinhService
    {
        // ==========================================
        // DANH MỤC NHÂN VIÊN VÀ CHỨC VỤ
        // ==========================================

        public DataTable LayNhanVien()
        {
            return Db.Query(@"
                SELECT
                    MaNV,
                    MaNV + N' - ' + HoTen AS HienThi
                FROM NhanVien
                WHERE TrangThaiLamViec = N'Chính thức'
                ORDER BY HoTen");
        }

        public DataTable LayChucVu()
        {
            return Db.Query(@"
                SELECT
                    MaChucVu,
                    TenChucVu,
                    MoTa,
                    MaChucVu + N' - ' + TenChucVu AS HienThi
                FROM ChucVu
                ORDER BY TenChucVu");
        }

        public KetQuaXuLy ThemChucVu(
            string maChucVu,
            string tenChucVu,
            string moTa)
        {
            if (string.IsNullOrWhiteSpace(maChucVu) ||
                string.IsNullOrWhiteSpace(tenChucVu))
            {
                return KetQuaXuLy.Fail(
                    "Mã và tên chức vụ không được để trống.");
            }

            try
            {
                Db.Execute(@"
                    INSERT INTO ChucVu
                    (MaChucVu, TenChucVu, MoTa)
                    VALUES (@Ma, @Ten, @MoTa)",
                    Db.P("@Ma", maChucVu),
                    Db.P("@Ten", tenChucVu),
                    Db.P("@MoTa", moTa));

                return KetQuaXuLy.Ok(
                    "Đã thêm chức vụ thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // ==========================================
        // QUYẾT ĐỊNH CHỨC VỤ
        // ==========================================

        public DataTable LayQuyetDinhChucVu(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    q.MaQDCV,
                    q.MaNV,
                    nv.HoTen,
                    q.MaChucVu,
                    cv.TenChucVu,
                    q.NgayQuyetDinh,
                    q.NgayHieuLuc,
                    q.PhuCapChucVu
                FROM QuyetDinhChucVu q
                JOIN NhanVien nv ON q.MaNV = nv.MaNV
                JOIN ChucVu cv ON q.MaChucVu = cv.MaChucVu
                WHERE
                    ISNULL(@TuKhoa, N'') = N''
                    OR q.MaQDCV LIKE N'%' + @TuKhoa + N'%'
                    OR q.MaNV LIKE N'%' + @TuKhoa + N'%'
                    OR nv.HoTen LIKE N'%' + @TuKhoa + N'%'
                    OR cv.TenChucVu LIKE N'%' + @TuKhoa + N'%'
                ORDER BY q.NgayHieuLuc DESC, q.MaQDCV DESC",
                Db.P("@TuKhoa", tuKhoa));
        }

        public KetQuaXuLy LapQuyetDinhChucVu(
            string maQDCV,
            string maNV,
            string maChucVu,
            DateTime ngayQuyetDinh,
            DateTime ngayHieuLuc,
            decimal phuCapChucVu)
        {
            if (string.IsNullOrWhiteSpace(maQDCV) ||
                string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(maChucVu))
            {
                return KetQuaXuLy.Fail(
                    "Chưa nhập đầy đủ mã quyết định, " +
                    "nhân viên và chức vụ.");
            }

            if (ngayHieuLuc.Date < ngayQuyetDinh.Date)
            {
                return KetQuaXuLy.Fail(
                    "Ngày hiệu lực không được trước ngày quyết định.");
            }

            if (phuCapChucVu < 0)
            {
                return KetQuaXuLy.Fail(
                    "Phụ cấp chức vụ không được âm.");
            }

            try
            {
                if (!CoHopDongHopLe(maNV, ngayHieuLuc))
                {
                    return KetQuaXuLy.Fail(
                        "Nhân viên chưa có hợp đồng chính thức " +
                        "có hiệu lực tại ngày được bổ nhiệm.");
                }

                int trungNgay = Convert.ToInt32(Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM QuyetDinhChucVu
                    WHERE MaNV = @MaNV
                      AND NgayHieuLuc = @NgayHieuLuc",
                    Db.P("@MaNV", maNV),
                    Db.P("@NgayHieuLuc", ngayHieuLuc.Date)));

                if (trungNgay > 0)
                {
                    return KetQuaXuLy.Fail(
                        "Nhân viên đã có quyết định chức vụ " +
                        "cùng ngày hiệu lực.");
                }

                Db.Execute(@"
                    INSERT INTO QuyetDinhChucVu
                    (
                        MaQDCV, MaNV, MaChucVu,
                        NgayQuyetDinh, NgayHieuLuc,
                        PhuCapChucVu
                    )
                    VALUES
                    (
                        @MaQD, @MaNV, @MaChucVu,
                        @NgayQD, @NgayHieuLuc,
                        @PhuCap
                    )",
                    Db.P("@MaQD", maQDCV),
                    Db.P("@MaNV", maNV),
                    Db.P("@MaChucVu", maChucVu),
                    Db.P("@NgayQD", ngayQuyetDinh.Date),
                    Db.P("@NgayHieuLuc", ngayHieuLuc.Date),
                    Db.P("@PhuCap", phuCapChucVu));

                return KetQuaXuLy.Ok(
                    "Đã lập quyết định chức vụ thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // ==========================================
        // QUYẾT ĐỊNH LƯƠNG
        // ==========================================

        public DataTable LayQuyetDinhLuong(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    q.MaQDL,
                    q.MaNV,
                    nv.HoTen,
                    q.NgayQuyetDinh,
                    q.NgayHieuLuc,
                    q.LuongCoBan,
                    q.PhuCapLuong,
                    q.PhuCapDocHai
                FROM QuyetDinhLuong q
                JOIN NhanVien nv ON q.MaNV = nv.MaNV
                WHERE
                    ISNULL(@TuKhoa, N'') = N''
                    OR q.MaQDL LIKE N'%' + @TuKhoa + N'%'
                    OR q.MaNV LIKE N'%' + @TuKhoa + N'%'
                    OR nv.HoTen LIKE N'%' + @TuKhoa + N'%'
                ORDER BY q.NgayHieuLuc DESC, q.MaQDL DESC",
                Db.P("@TuKhoa", tuKhoa));
        }

        public KetQuaXuLy LapQuyetDinhLuong(
            string maQDL,
            string maNV,
            DateTime ngayQuyetDinh,
            DateTime ngayHieuLuc,
            decimal luongCoBan,
            decimal phuCapLuong,
            decimal phuCapDocHai)
        {
            if (string.IsNullOrWhiteSpace(maQDL) ||
                string.IsNullOrWhiteSpace(maNV))
            {
                return KetQuaXuLy.Fail(
                    "Chưa nhập mã quyết định hoặc chọn nhân viên.");
            }

            if (ngayHieuLuc.Date < ngayQuyetDinh.Date)
            {
                return KetQuaXuLy.Fail(
                    "Ngày hiệu lực không được trước ngày quyết định.");
            }

            if (luongCoBan < 0 ||
                phuCapLuong < 0 ||
                phuCapDocHai < 0)
            {
                return KetQuaXuLy.Fail(
                    "Các khoản lương và phụ cấp không được âm.");
            }

            if (luongCoBan == 0)
            {
                return KetQuaXuLy.Fail(
                    "Lương cơ bản phải lớn hơn 0.");
            }

            try
            {
                if (!CoHopDongHopLe(maNV, ngayHieuLuc))
                {
                    return KetQuaXuLy.Fail(
                        "Nhân viên chưa có hợp đồng chính thức " +
                        "có hiệu lực tại ngày quyết định lương.");
                }

                int trungNgay = Convert.ToInt32(Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM QuyetDinhLuong
                    WHERE MaNV = @MaNV
                      AND NgayHieuLuc = @NgayHieuLuc",
                    Db.P("@MaNV", maNV),
                    Db.P("@NgayHieuLuc", ngayHieuLuc.Date)));

                if (trungNgay > 0)
                {
                    return KetQuaXuLy.Fail(
                        "Nhân viên đã có quyết định lương " +
                        "cùng ngày hiệu lực.");
                }

                Db.Execute(@"
                    INSERT INTO QuyetDinhLuong
                    (
                        MaQDL, MaNV,
                        NgayQuyetDinh, NgayHieuLuc,
                        LuongCoBan, PhuCapLuong, PhuCapDocHai
                    )
                    VALUES
                    (
                        @MaQD, @MaNV,
                        @NgayQD, @NgayHieuLuc,
                        @Luong, @PhuCap, @DocHai
                    )",
                    Db.P("@MaQD", maQDL),
                    Db.P("@MaNV", maNV),
                    Db.P("@NgayQD", ngayQuyetDinh.Date),
                    Db.P("@NgayHieuLuc", ngayHieuLuc.Date),
                    Db.P("@Luong", luongCoBan),
                    Db.P("@PhuCap", phuCapLuong),
                    Db.P("@DocHai", phuCapDocHai));

                return KetQuaXuLy.Ok(
                    "Đã lập quyết định lương thành công.");
            }
            catch (Exception ex)
            {
                return KetQuaXuLy.Fail(ex.Message);
            }
        }

        // ==========================================
        // KIỂM TRA HỢP ĐỒNG CÓ HIỆU LỰC
        // ==========================================

        private bool CoHopDongHopLe(
            string maNV,
            DateTime ngayHieuLuc)
        {
            int soLuong = Convert.ToInt32(Db.Scalar(@"
                SELECT COUNT(*)
                FROM HopDongLaoDong
                WHERE MaNV = @MaNV
                  AND TrangThai = N'Có hiệu lực'
                  AND NgayBatDau <= @Ngay
                  AND
                  (
                      NgayKetThuc IS NULL
                      OR NgayKetThuc >= @Ngay
                  )",
                Db.P("@MaNV", maNV),
                Db.P("@Ngay", ngayHieuLuc.Date)));

            return soLuong > 0;
        }
    }
}
