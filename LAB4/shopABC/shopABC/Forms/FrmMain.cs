using System;
using System.Drawing;
using System.Windows.Forms;
using shopABC.Helpers;

namespace shopABC.Forms
{
    public partial class FrmMain : Form
    {
        private Label lblNguoiDung;

        private Button btnSanPham;
        private Button btnGioHang;
        private Button btnDangNhap;
        private Button btnCheckout;
        private Button btnDangXuat;
        private Button btnThoat;

        public FrmMain()
        {
            InitializeComponent();
            TaoGiaoDien();
            CapNhatTrangThai();
        }

        private void TaoGiaoDien()
        {
            // ===== FORM =====
            Text = "e-SHOPPING ABC";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(650, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label();
            lblTitle.Text = "HỆ THỐNG e-SHOPPING ABC";
            lblTitle.Font =
                new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(125, 40);

            // ===== NGƯỜI DÙNG =====
            lblNguoiDung = new Label();
            lblNguoiDung.Font =
                new Font("Segoe UI", 10, FontStyle.Italic);
            lblNguoiDung.AutoSize = true;
            lblNguoiDung.Location = new Point(190, 95);

            // ===== SẢN PHẨM =====
            btnSanPham = TaoButton("SẢN PHẨM", 140);

            btnSanPham.Click += (s, e) =>
            {
                using (FrmSanPham frm = new FrmSanPham())
                {
                    frm.ShowDialog();
                }

                CapNhatTrangThai();
            };

            // ===== GIỎ HÀNG =====
            btnGioHang = TaoButton("GIỎ HÀNG", 200);

            btnGioHang.Click += (s, e) =>
            {
                if (!KiemTraDangNhap())
                    return;

                using (FrmGioHang frm = new FrmGioHang())
                {
                    frm.ShowDialog();
                }

                CapNhatTrangThai();
            };

            // ===== ĐĂNG NHẬP =====
            btnDangNhap = TaoButton("ĐĂNG NHẬP", 260);

            btnDangNhap.Click += (s, e) =>
            {
                using (FrmDangNhap frm = new FrmDangNhap())
                {
                    frm.ShowDialog();
                }

                CapNhatTrangThai();
            };

            // ===== CHECKOUT =====
            btnCheckout = TaoButton("CHECKOUT", 320);

            btnCheckout.Click += (s, e) =>
            {
                if (!KiemTraDangNhap())
                    return;

                if (!CoSanPhamTrongGio())
                {
                    MessageBox.Show(
                        "Giỏ hàng đang trống.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                using (FrmCheckout frm = new FrmCheckout())
                {
                    frm.ShowDialog();
                }

                CapNhatTrangThai();
            };

            // ===== ĐĂNG XUẤT =====
            btnDangXuat = TaoButton("ĐĂNG XUẤT", 380);

            btnDangXuat.Click += (s, e) =>
            {
                if (!Session.DaDangNhap)
                    return;

                DialogResult result =
                    MessageBox.Show(
                        "Bạn có muốn đăng xuất không?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                if (result == DialogResult.Yes)
                {
                    Session.DangXuat();

                    MessageBox.Show(
                        "Đăng xuất thành công.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    CapNhatTrangThai();
                }
            };

            // ===== THOÁT =====
            btnThoat = TaoButton("THOÁT", 440);

            btnThoat.Click += (s, e) =>
            {
                Application.Exit();
            };

            // ===== ADD =====
            Controls.Add(lblTitle);
            Controls.Add(lblNguoiDung);
            Controls.Add(btnSanPham);
            Controls.Add(btnGioHang);
            Controls.Add(btnDangNhap);
            Controls.Add(btnCheckout);
            Controls.Add(btnDangXuat);
            Controls.Add(btnThoat);
        }

        private Button TaoButton(string text, int y)
        {
            Button button = new Button();

            button.Text = text;
            button.Size = new Size(250, 45);
            button.Location = new Point(190, y);
            button.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            return button;
        }

        // =====================================
        // CẬP NHẬT TRẠNG THÁI MAIN
        // =====================================
        private void CapNhatTrangThai()
        {
            if (Session.DaDangNhap)
            {
                lblNguoiDung.Text =
                    "Xin chào, " + Session.HoTen;

                btnDangNhap.Enabled = false;
                btnDangXuat.Enabled = true;
            }
            else
            {
                lblNguoiDung.Text =
                    "Bạn chưa đăng nhập";

                btnDangNhap.Enabled = true;
                btnDangXuat.Enabled = false;
            }
        }

        // =====================================
        // YÊU CẦU ĐĂNG NHẬP
        // =====================================
        private bool KiemTraDangNhap()
        {
            if (Session.DaDangNhap)
                return true;

            MessageBox.Show(
                "Vui lòng đăng nhập trước.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            using (FrmDangNhap frm = new FrmDangNhap())
            {
                frm.ShowDialog();
            }

            CapNhatTrangThai();

            return Session.DaDangNhap;
        }

        // =====================================
        // KIỂM TRA GIỎ HÀNG
        // =====================================
        private bool CoSanPhamTrongGio()
        {
            object result =
                shopABC.Data.Db.Scalar(
                    @"SELECT COUNT(*)
                      FROM CHITIETGIOHANG CT
                      INNER JOIN GIOHANG GH
                          ON CT.MaGioHang = GH.MaGioHang
                      WHERE GH.MaKhachHang = @MaKhachHang",

                    new System.Data.SqlClient.SqlParameter(
                        "@MaKhachHang",
                        Session.MaKhachHang
                    )
                );

            return Convert.ToInt32(result) > 0;
        }
    }
}