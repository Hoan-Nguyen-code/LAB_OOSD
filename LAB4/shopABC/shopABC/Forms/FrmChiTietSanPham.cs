using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using shopABC.Adapters;
using shopABC.Data;
using shopABC.Helpers;

namespace shopABC.Forms
{
    public partial class FrmChiTietSanPham : Form
    {
        private readonly string maSanPham;

        private Label lblMa;
        private Label lblTen;
        private Label lblNhom;
        private Label lblNhaSanXuat;
        private Label lblGia;
        private Label lblTinhTrang;

        private TextBox txtMoTa;
        private TextBox txtThongSo;

        private PictureBox picSanPham;

        private Button btnThemGioHang;
        private Button btnDong;

        private decimal giaBan;

        public FrmChiTietSanPham(string maSanPham)
        {
            InitializeComponent();

            this.maSanPham = maSanPham;

            TaoGiaoDien();
            LoadChiTiet();
        }

        private void TaoGiaoDien()
        {
            Text = "Chi tiết sản phẩm";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(850, 650);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label lblTitle = new Label();
            lblTitle.Text = "CHI TIẾT SẢN PHẨM";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(270, 25);

            Controls.Add(lblTitle);

            // ===== HÌNH ẢNH =====
            picSanPham = new PictureBox();
            picSanPham.Location = new Point(50, 100);
            picSanPham.Size = new Size(250, 220);
            picSanPham.BorderStyle = BorderStyle.FixedSingle;
            picSanPham.SizeMode = PictureBoxSizeMode.Zoom;

            Controls.Add(picSanPham);

            // ===== THÔNG TIN =====
            lblMa = TaoLabel("", 340, 100);
            lblTen = TaoLabel("", 340, 140);
            lblNhom = TaoLabel("", 340, 180);
            lblNhaSanXuat = TaoLabel("", 340, 220);
            lblGia = TaoLabel("", 340, 260);
            lblTinhTrang = TaoLabel("", 340, 300);

            lblTen.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblGia.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            Controls.Add(lblMa);
            Controls.Add(lblTen);
            Controls.Add(lblNhom);
            Controls.Add(lblNhaSanXuat);
            Controls.Add(lblGia);
            Controls.Add(lblTinhTrang);

            // ===== MÔ TẢ =====
            Label lblMoTa = TaoLabel(
                "Mô tả sản phẩm:",
                50,
                350
            );

            lblMoTa.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            Controls.Add(lblMoTa);

            txtMoTa = new TextBox();
            txtMoTa.Location = new Point(50, 380);
            txtMoTa.Size = new Size(350, 120);
            txtMoTa.Multiline = true;
            txtMoTa.ReadOnly = true;
            txtMoTa.ScrollBars = ScrollBars.Vertical;

            Controls.Add(txtMoTa);

            // ===== THÔNG SỐ =====
            Label lblThongSo = TaoLabel(
                "Thông số kỹ thuật:",
                430,
                350
            );

            lblThongSo.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            Controls.Add(lblThongSo);

            txtThongSo = new TextBox();
            txtThongSo.Location = new Point(430, 380);
            txtThongSo.Size = new Size(350, 120);
            txtThongSo.Multiline = true;
            txtThongSo.ReadOnly = true;
            txtThongSo.ScrollBars = ScrollBars.Vertical;

            Controls.Add(txtThongSo);

            // ===== BUTTON =====
            btnThemGioHang = new Button();
            btnThemGioHang.Text = "THÊM VÀO GIỎ";
            btnThemGioHang.Location = new Point(450, 540);
            btnThemGioHang.Size = new Size(160, 45);
            btnThemGioHang.Click += BtnThemGioHang_Click;

            btnDong = new Button();
            btnDong.Text = "ĐÓNG";
            btnDong.Location = new Point(630, 540);
            btnDong.Size = new Size(150, 45);
            btnDong.Click += (s, e) => Close();

            Controls.Add(btnThemGioHang);
            Controls.Add(btnDong);
        }

        private Label TaoLabel(string text, int x, int y)
        {
            Label lbl = new Label();

            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10);
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);

            return lbl;
        }

        // =====================================
        // LOAD CHI TIẾT
        // =====================================
        private void LoadChiTiet()
        {
            DataTable dt =
                ProductSystemAdapter.LayChiTietSanPham(maSanPham);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Không tìm thấy sản phẩm.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                Close();
                return;
            }

            DataRow row = dt.Rows[0];

            lblMa.Text =
                "Mã sản phẩm: " +
                row["MaSanPham"];

            lblTen.Text =
                "Tên sản phẩm: " +
                row["TenSanPham"];

            lblNhom.Text =
                "Nhóm: " +
                row["TenNhom"];

            lblNhaSanXuat.Text =
                "Nhà sản xuất: " +
                row["NhaSanXuat"];

            giaBan =
                Convert.ToDecimal(
                    row["GiaBan"]
                );

            lblGia.Text =
                "Giá bán: " +
                giaBan.ToString("N0") +
                " VNĐ";

            lblTinhTrang.Text =
                "Tình trạng: " +
                row["TinhTrang"];

            txtMoTa.Text =
                row["MoTa"] == DBNull.Value
                    ? ""
                    : row["MoTa"].ToString();

            txtThongSo.Text =
                row["ThongSoKyThuat"] == DBNull.Value
                    ? ""
                    : row["ThongSoKyThuat"].ToString();

            // ===== LOAD HÌNH ẢNH =====
            LoadHinhAnh(row);

            // Hết hàng → không cho thêm
            btnThemGioHang.Enabled =
                ProductSystemAdapter.KiemTraConHang(maSanPham);
        }

        // =====================================
        // LOAD ẢNH SẢN PHẨM
        // =====================================
        private void LoadHinhAnh(DataRow row)
        {
            picSanPham.Image = null;

            if (!row.Table.Columns.Contains("HinhAnh") ||
                row["HinhAnh"] == DBNull.Value)
            {
                return;
            }

            string hinhAnh = row["HinhAnh"].ToString();

            if (string.IsNullOrWhiteSpace(hinhAnh))
            {
                return;
            }

            try
            {
                string duongDan;

                // Nếu DB đã chứa đường dẫn tuyệt đối
                if (Path.IsPathRooted(hinhAnh))
                {
                    duongDan = hinhAnh;
                }
                else
                {
                    // Ví dụ DB lưu:
                    // Images\SP001.jpg
                    duongDan = Path.Combine(
                        Application.StartupPath,
                        hinhAnh
                    );
                }

                if (!File.Exists(duongDan))
                {
                    return;
                }

                // Dùng stream để tránh khóa file ảnh
                using (FileStream stream =
                    new FileStream(
                        duongDan,
                        FileMode.Open,
                        FileAccess.Read))
                {
                    using (Image temp = Image.FromStream(stream))
                    {
                        picSanPham.Image =
                            new Bitmap(temp);
                    }
                }
            }
            catch
            {
                picSanPham.Image = null;
            }
        }

        // =====================================
        // THÊM VÀO GIỎ TỪ CHI TIẾT
        // =====================================
        private void BtnThemGioHang_Click(
            object sender,
            EventArgs e)
        {
            if (!ProductSystemAdapter
                .KiemTraConHang(maSanPham))
            {
                MessageBox.Show(
                    "Sản phẩm hiện đã hết hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!Session.DaDangNhap)
            {
                MessageBox.Show(
                    "Vui lòng đăng nhập trước khi thêm sản phẩm vào giỏ hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                using (FrmDangNhap frm =
                    new FrmDangNhap())
                {
                    frm.ShowDialog();
                }

                if (!Session.DaDangNhap)
                    return;
            }

            try
            {
                object result = Db.Scalar(
                    @"SELECT MaGioHang
                      FROM GIOHANG
                      WHERE MaKhachHang = @MaKhachHang",

                    new SqlParameter(
                        "@MaKhachHang",
                        Session.MaKhachHang
                    )
                );

                int maGioHang;

                if (result == null ||
                    result == DBNull.Value)
                {
                    object newId = Db.Scalar(
                        @"INSERT INTO GIOHANG
                            (MaKhachHang, NgayTao)
                          OUTPUT INSERTED.MaGioHang
                          VALUES
                            (@MaKhachHang, GETDATE())",

                        new SqlParameter(
                            "@MaKhachHang",
                            Session.MaKhachHang
                        )
                    );

                    maGioHang =
                        Convert.ToInt32(newId);
                }
                else
                {
                    maGioHang =
                        Convert.ToInt32(result);
                }

                int tonTai =
                    Convert.ToInt32(
                        Db.Scalar(
                            @"SELECT COUNT(*)
                              FROM CHITIETGIOHANG
                              WHERE MaGioHang = @MaGioHang
                                AND MaSanPham = @MaSanPham",

                            new SqlParameter(
                                "@MaGioHang",
                                maGioHang
                            ),

                            new SqlParameter(
                                "@MaSanPham",
                                maSanPham
                            )
                        )
                    );

                if (tonTai > 0)
                {
                    Db.Execute(
                        @"UPDATE CHITIETGIOHANG
                          SET SoLuong = SoLuong + 1
                          WHERE MaGioHang = @MaGioHang
                            AND MaSanPham = @MaSanPham",

                        new SqlParameter(
                            "@MaGioHang",
                            maGioHang
                        ),

                        new SqlParameter(
                            "@MaSanPham",
                            maSanPham
                        )
                    );
                }
                else
                {
                    Db.Execute(
                        @"INSERT INTO CHITIETGIOHANG
                        (
                            MaGioHang,
                            MaSanPham,
                            SoLuong,
                            DonGia
                        )
                        VALUES
                        (
                            @MaGioHang,
                            @MaSanPham,
                            1,
                            @DonGia
                        )",

                        new SqlParameter(
                            "@MaGioHang",
                            maGioHang
                        ),

                        new SqlParameter(
                            "@MaSanPham",
                            maSanPham
                        ),

                        new SqlParameter(
                            "@DonGia",
                            giaBan
                        )
                    );
                }

                MessageBox.Show(
                    "Đã thêm sản phẩm vào giỏ hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm vào giỏ hàng!\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}