using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using shopABC.Data;
using shopABC.Helpers;

namespace shopABC.Forms
{
    public partial class FrmDangNhap : Form
    {
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
        private Button btnDangKy;

        public FrmDangNhap()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            // ===== FORM =====
            Text = "Đăng nhập";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(500, 380);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label();
            lblTitle.Text = "ĐĂNG NHẬP";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(155, 40);

            // ===== TÊN ĐĂNG NHẬP =====
            Label lblTenDangNhap = new Label();
            lblTenDangNhap.Text = "Tên đăng nhập:";
            lblTenDangNhap.Font = new Font("Segoe UI", 10);
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.Location = new Point(60, 120);

            txtTenDangNhap = new TextBox();
            txtTenDangNhap.Size = new Size(250, 30);
            txtTenDangNhap.Location = new Point(180, 115);

            // ===== MẬT KHẨU =====
            Label lblMatKhau = new Label();
            lblMatKhau.Text = "Mật khẩu:";
            lblMatKhau.Font = new Font("Segoe UI", 10);
            lblMatKhau.AutoSize = true;
            lblMatKhau.Location = new Point(60, 170);

            txtMatKhau = new TextBox();
            txtMatKhau.Size = new Size(250, 30);
            txtMatKhau.Location = new Point(180, 165);
            txtMatKhau.UseSystemPasswordChar = true;

            // ===== BUTTON ĐĂNG NHẬP =====
            btnDangNhap = new Button();
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.Size = new Size(150, 40);
            btnDangNhap.Location = new Point(80, 230);
            btnDangNhap.Click += BtnDangNhap_Click;

            // ===== BUTTON ĐĂNG KÝ =====
            btnDangKy = new Button();
            btnDangKy.Text = "ĐĂNG KÝ";
            btnDangKy.Size = new Size(150, 40);
            btnDangKy.Location = new Point(260, 230);

            btnDangKy.Click += (s, e) =>
            {
                FrmDangKy frm = new FrmDangKy();
                frm.ShowDialog();
            };

            // ===== ADD CONTROLS =====
            Controls.Add(lblTitle);
            Controls.Add(lblTenDangNhap);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblMatKhau);
            Controls.Add(txtMatKhau);
            Controls.Add(btnDangNhap);
            Controls.Add(btnDangKy);

            // Nhấn Enter để đăng nhập
            AcceptButton = btnDangNhap;
        }

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // ===== KIỂM TRA RỖNG =====
            if (tenDangNhap == "" || matKhau == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // ===== KIỂM TRA TÀI KHOẢN TRONG DATABASE =====
            string sql = @"
                SELECT MaKhachHang, HoTen
                FROM KHACHHANG
                WHERE TenDangNhap = @TenDangNhap
                  AND MatKhau = @MatKhau";

            try
            {
                DataTable dt = Db.Query(
                    sql,
                    new SqlParameter("@TenDangNhap", tenDangNhap),
                    new SqlParameter("@MatKhau", matKhau)
                );

                if (dt.Rows.Count > 0)
                {
                    int maKhachHang =
                        Convert.ToInt32(dt.Rows[0]["MaKhachHang"]);

                    string hoTen =
                        dt.Rows[0]["HoTen"].ToString();

                    // ===== LƯU SESSION =====
                    Session.MaKhachHang = maKhachHang;
                    Session.HoTen = hoTen;

                    MessageBox.Show(
                        "Đăng nhập thành công!\nXin chào " + hoTen,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Tên đăng nhập hoặc mật khẩu không đúng.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi đăng nhập!\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}