using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Services
{
    public class DocGiaService
    {
        // =========================================
        // LẤY DANH SÁCH ĐỘC GIẢ
        // =========================================

        public DataTable LayDanhSach()
        {
            string sql = @"
                SELECT
                    MaDocGia,
                    Ho,
                    Ten,
                    NgaySinh,
                    Phai,
                    SoDienThoai,
                    DiaChi,
                    Email,
                    Anh3x4
                FROM DOCGIA
                ORDER BY MaDocGia";

            return Db.Query(sql);
        }

        // =========================================
        // THÊM ĐỘC GIẢ
        // =========================================

        public void ThemDocGia(
            string maDocGia,
            string ho,
            string ten,
            DateTime ngaySinh,
            string phai,
            string soDienThoai,
            string diaChi,
            string email,
            string anh3x4)
        {
            KiemTraDuLieu(
                maDocGia,
                ho,
                ten,
                email);

            string sql = @"
                INSERT INTO DOCGIA
                (
                    MaDocGia,
                    Ho,
                    Ten,
                    NgaySinh,
                    Phai,
                    SoDienThoai,
                    DiaChi,
                    Email,
                    Anh3x4
                )
                VALUES
                (
                    @MaDocGia,
                    @Ho,
                    @Ten,
                    @NgaySinh,
                    @Phai,
                    @SoDienThoai,
                    @DiaChi,
                    @Email,
                    @Anh3x4
                )";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaDocGia", maDocGia),
                new SqlParameter("@Ho", ho),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@NgaySinh", ngaySinh.Date),
                new SqlParameter("@Phai", phai),
                new SqlParameter("@SoDienThoai", soDienThoai),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@Email", email),
                new SqlParameter("@Anh3x4", anh3x4)
            };

            Db.Execute(sql, parameters);
        }

        // =========================================
        // CẬP NHẬT ĐỘC GIẢ
        // =========================================

        public void CapNhatDocGia(
            string maDocGia,
            string ho,
            string ten,
            DateTime ngaySinh,
            string phai,
            string soDienThoai,
            string diaChi,
            string email,
            string anh3x4)
        {
            KiemTraDuLieu(
                maDocGia,
                ho,
                ten,
                email);

            string sql = @"
                UPDATE DOCGIA
                SET
                    Ho = @Ho,
                    Ten = @Ten,
                    NgaySinh = @NgaySinh,
                    Phai = @Phai,
                    SoDienThoai = @SoDienThoai,
                    DiaChi = @DiaChi,
                    Email = @Email,
                    Anh3x4 = @Anh3x4
                WHERE MaDocGia = @MaDocGia";

            SqlParameter[] parameters =
            {
                new SqlParameter("@MaDocGia", maDocGia),
                new SqlParameter("@Ho", ho),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@NgaySinh", ngaySinh.Date),
                new SqlParameter("@Phai", phai),
                new SqlParameter("@SoDienThoai", soDienThoai),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@Email", email),
                new SqlParameter("@Anh3x4", anh3x4)
            };

            int soDong = Db.Execute(
                sql,
                parameters);

            if (soDong == 0)
            {
                throw new Exception(
                    "Không tìm thấy độc giả cần cập nhật.");
            }
        }

        // =========================================
        // XÓA ĐỘC GIẢ
        // =========================================

        public void XoaDocGia(string maDocGia)
        {
            string sql = @"
                DELETE FROM DOCGIA
                WHERE MaDocGia = @MaDocGia";

            int soDong = Db.Execute(
                sql,
                new SqlParameter(
                    "@MaDocGia",
                    maDocGia));

            if (soDong == 0)
            {
                throw new Exception(
                    "Không tìm thấy độc giả cần xóa.");
            }
        }

        // =========================================
        // TÌM KIẾM
        // =========================================

        public DataTable TimDocGia(string tuKhoa)
        {
            string sql = @"
                SELECT
                    MaDocGia,
                    Ho,
                    Ten,
                    NgaySinh,
                    Phai,
                    SoDienThoai,
                    DiaChi,
                    Email,
                    Anh3x4
                FROM DOCGIA
                WHERE MaDocGia LIKE @TuKhoa
                   OR Ho LIKE @TuKhoa
                   OR Ten LIKE @TuKhoa
                   OR Email LIKE @TuKhoa
                   OR SoDienThoai LIKE @TuKhoa
                ORDER BY MaDocGia";

            return Db.Query(
                sql,
                new SqlParameter(
                    "@TuKhoa",
                    "%" + tuKhoa + "%"));
        }

        // =========================================
        // LẤY THẺ CỦA ĐỘC GIẢ
        // =========================================

        public DataTable LayDanhSachThe(
            string maDocGia)
        {
            string sql = @"
                SELECT
                    MaThe,
                    MaDocGia,
                    NgayCap,
                    HanSuDung,
                    DaDongLePhi,
                    TrangThai
                FROM THEDOCGIA
                WHERE MaDocGia = @MaDocGia
                ORDER BY HanSuDung DESC";

            return Db.Query(
                sql,
                new SqlParameter(
                    "@MaDocGia",
                    maDocGia));
        }

        // =========================================
        // CẤP THẺ
        // =========================================

        public string CapThe(
            string maDocGia,
            DateTime ngayCap,
            DateTime hanSuDung,
            bool daDongLePhi)
        {
            // Hạn phải hợp lệ
            if (hanSuDung.Date < ngayCap.Date)
            {
                throw new Exception(
                    "Hạn sử dụng phải bằng hoặc sau ngày cấp.");
            }

            // Kiểm tra độc giả tồn tại
            object tonTai = Db.Scalar(
                @"SELECT COUNT(*)
                  FROM DOCGIA
                  WHERE MaDocGia = @Ma",
                new SqlParameter(
                    "@Ma",
                    maDocGia));

            if (Convert.ToInt32(tonTai) == 0)
            {
                throw new Exception(
                    "Không tìm thấy độc giả.");
            }

            using (SqlConnection cn =
                Db.OpenConnection())
            using (SqlTransaction tx =
                cn.BeginTransaction())
            {
                try
                {
                    // Kiểm tra độc giả có thẻ còn giá trị không
                    string sqlCheck = @"
                        SELECT COUNT(*)
                        FROM THEDOCGIA
                        WHERE MaDocGia = @Ma
                          AND TrangThai = 1
                          AND HanSuDung >= @NgayCap";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            sqlCheck,
                            cn,
                            tx))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma",
                            maDocGia);

                        cmd.Parameters.AddWithValue(
                            "@NgayCap",
                            ngayCap.Date);

                        int soThe =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());

                        if (soThe > 0)
                        {
                            throw new Exception(
                                "Độc giả đang có một thẻ còn giá trị sử dụng.");
                        }
                    }

                    // Tắt trạng thái các thẻ cũ
                    string sqlTatTheCu = @"
                        UPDATE THEDOCGIA
                        SET TrangThai = 0
                        WHERE MaDocGia = @Ma
                          AND TrangThai = 1";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            sqlTatTheCu,
                            cn,
                            tx))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma",
                            maDocGia);

                        cmd.ExecuteNonQuery();
                    }

                    // Tạo mã thẻ tự động
                    string maThe =
     "T" +
     DateTime.Now.ToString("yyMMddHHmmssfff");
                    string sqlInsert = @"
                        INSERT INTO THEDOCGIA
                        (
                            MaThe,
                            MaDocGia,
                            NgayCap,
                            HanSuDung,
                            DaDongLePhi,
                            TrangThai
                        )
                        VALUES
                        (
                            @MaThe,
                            @MaDocGia,
                            @NgayCap,
                            @HanSuDung,
                            @DaDongLePhi,
                            1
                        )";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            sqlInsert,
                            cn,
                            tx))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaThe",
                            maThe);

                        cmd.Parameters.AddWithValue(
                            "@MaDocGia",
                            maDocGia);

                        cmd.Parameters.AddWithValue(
                            "@NgayCap",
                            ngayCap.Date);

                        cmd.Parameters.AddWithValue(
                            "@HanSuDung",
                            hanSuDung.Date);

                        cmd.Parameters.AddWithValue(
                            "@DaDongLePhi",
                            daDongLePhi);

                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();

                    return maThe;
                }
                catch
                {
                    try
                    {
                        tx.Rollback();
                    }
                    catch
                    {
                    }

                    throw;
                }
            }
        }

        // =========================================
        // GIA HẠN THẺ
        // =========================================

        public void GiaHanThe(
            string maDocGia,
            DateTime hanMoi,
            bool daDongLePhi)
        {
            using (SqlConnection cn =
                Db.OpenConnection())
            {
                string sqlTimThe = @"
                    SELECT TOP 1
                        MaThe,
                        NgayCap,
                        HanSuDung
                    FROM THEDOCGIA
                    WHERE MaDocGia = @Ma
                      AND TrangThai = 1
                    ORDER BY HanSuDung DESC";

                string maThe;
                DateTime hanCu;

                using (SqlCommand cmd =
                    new SqlCommand(
                        sqlTimThe,
                        cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@Ma",
                        maDocGia);

                    using (SqlDataReader rd =
                        cmd.ExecuteReader())
                    {
                        if (!rd.Read())
                        {
                            throw new Exception(
                                "Độc giả chưa có thẻ đang hoạt động.");
                        }

                        maThe =
                            rd.GetString(0);

                        hanCu =
                            rd.GetDateTime(2);
                    }
                }

                if (hanMoi.Date <= hanCu.Date)
                {
                    throw new Exception(
                        "Hạn sử dụng mới phải sau hạn sử dụng hiện tại.");
                }

                string sqlUpdate = @"
                    UPDATE THEDOCGIA
                    SET
                        HanSuDung = @HanMoi,
                        DaDongLePhi = @LePhi
                    WHERE MaThe = @MaThe";

                using (SqlCommand cmd =
                    new SqlCommand(
                        sqlUpdate,
                        cn))
                {
                    cmd.Parameters.AddWithValue(
                        "@HanMoi",
                        hanMoi.Date);

                    cmd.Parameters.AddWithValue(
                        "@LePhi",
                        daDongLePhi);

                    cmd.Parameters.AddWithValue(
                        "@MaThe",
                        maThe);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================================
        // VALIDATION
        // =========================================

        private void KiemTraDuLieu(
            string maDocGia,
            string ho,
            string ten,
            string email)
        {
            if (string.IsNullOrWhiteSpace(
                    maDocGia))
            {
                throw new Exception(
                    "Mã độc giả không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(
                    ho))
            {
                throw new Exception(
                    "Họ độc giả không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(
                    ten))
            {
                throw new Exception(
                    "Tên độc giả không được để trống.");
            }

            // PDF yêu cầu kiểm tra email cơ bản
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!email.Contains("@") ||
                    !email.Contains("."))
                {
                    throw new Exception(
                        "Email không hợp lệ.");
                }
            }
        }
    }
}