using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using shopABC.Data;

namespace shopABC.Forms
{
    public partial class FrmDangKy : Form
    {
        private TextBox txtHoTen;
        private DateTimePicker dtpNgaySinh;
        private TextBox txtCMND;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private TextBox txtEmail;
        private Button btnDangKy;
        private Button btnHuy;

        public FrmDangKy()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Text = "Đăng ký tài khoản";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(620, 620);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label lblTitle = new Label();
            lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(145, 30);
            Controls.Add(lblTitle);

            int labelX = 70;
            int inputX = 230;
            int y = 100;
            int khoangCach = 50;

            // Họ tên
            Controls.Add(TaoLabel("Họ tên:", labelX, y));
            txtHoTen = TaoTextBox(inputX, y);
            Controls.Add(txtHoTen);

            // Ngày sinh
            y += khoangCach;
            Controls.Add(TaoLabel("Ngày sinh:", labelX, y));

            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Location = new Point(inputX, y);
            dtpNgaySinh.Size = new Size(280, 30);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            Controls.Add(dtpNgaySinh);

            // CMND / Passport
            y += khoangCach;
            Controls.Add(TaoLabel("CMND/Passport:", labelX, y));
            txtCMND = TaoTextBox(inputX, y);
            Controls.Add(txtCMND);

            // Địa chỉ
            y += khoangCach;
            Controls.Add(TaoLabel("Địa chỉ:", labelX, y));
            txtDiaChi = TaoTextBox(inputX, y);
            Controls.Add(txtDiaChi);

            // Điện thoại
            y += khoangCach;
            Controls.Add(TaoLabel("Điện thoại:", labelX, y));
            txtDienThoai = TaoTextBox(inputX, y);
            Controls.Add(txtDienThoai);

            // Tên đăng nhập
            y += khoangCach;
            Controls.Add(TaoLabel("Tên đăng nhập:", labelX, y));
            txtTenDangNhap = TaoTextBox(inputX, y);
            Controls.Add(txtTenDangNhap);

            // Mật khẩu
            y += khoangCach;
            Controls.Add(TaoLabel("Mật khẩu:", labelX, y));
            txtMatKhau = TaoTextBox(inputX, y);
            txtMatKhau.UseSystemPasswordChar = true;
            Controls.Add(txtMatKhau);

            // Email
            y += khoangCach;
            Controls.Add(TaoLabel("Email:", labelX, y));
            txtEmail = TaoTextBox(inputX, y);
            Controls.Add(txtEmail);

            // Button đăng ký
            btnDangKy = new Button();
            btnDangKy.Text = "ĐĂNG KÝ";
            btnDangKy.Size = new Size(150, 40);
            btnDangKy.Location = new Point(135, 510);
            btnDangKy.Click += BtnDangKy_Click;
            Controls.Add(btnDangKy);

            // Button hủy
            btnHuy = new Button();
            btnHuy.Text = "HỦY";
            btnHuy.Size = new Size(150, 40);
            btnHuy.Location = new Point(320, 510);
            btnHuy.Click += (s, e) => Close();
            Controls.Add(btnHuy);
        }

        private Label TaoLabel(string text, int x, int y)
        {
            Label label = new Label();
            label.Text = text;
            label.Location = new Point(x, y + 3);
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 10);

            return label;
        }

        private TextBox TaoTextBox(int x, int y)
        {
            TextBox textBox = new TextBox();
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(280, 30);

            return textBox;
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text.Trim() == "" ||
                txtDiaChi.Text.Trim() == "" ||
                txtDienThoai.Text.Trim() == "" ||
                txtTenDangNhap.Text.Trim() == "" ||
                txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ các thông tin bắt buộc.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Kiểm tra username đã tồn tại chưa
            string sqlKiemTra =
                "SELECT COUNT(*) FROM KHACHHANG WHERE TenDangNhap = @TenDangNhap";

            int soLuong = Convert.ToInt32(
                Db.Scalar(
                    sqlKiemTra,
                    new SqlParameter("@TenDangNhap",
                        txtTenDangNhap.Text.Trim())
                )
            );

            if (soLuong > 0)
            {
                MessageBox.Show(
                    "Tên đăng nhập đã tồn tại.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string sql = @"
                INSERT INTO KHACHHANG
                (
                    HoTen,
                    NgaySinh,
                    CMNDPassport,
                    DiaChi,
                    DienThoai,
                    TenDangNhap,
                    MatKhau,
                    Email
                )
                VALUES
                (
                    @HoTen,
                    @NgaySinh,
                    @CMNDPassport,
                    @DiaChi,
                    @DienThoai,
                    @TenDangNhap,
                    @MatKhau,
                    @Email
                )";

            try
            {
                Db.Execute(
                    sql,
                    new SqlParameter("@HoTen", txtHoTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value.Date),
                    new SqlParameter("@CMNDPassport", txtCMND.Text.Trim()),
                    new SqlParameter("@DiaChi", txtDiaChi.Text.Trim()),
                    new SqlParameter("@DienThoai", txtDienThoai.Text.Trim()),
                    new SqlParameter("@TenDangNhap", txtTenDangNhap.Text.Trim()),
                    new SqlParameter("@MatKhau", txtMatKhau.Text.Trim()),
                    new SqlParameter("@Email", txtEmail.Text.Trim())
                );

                MessageBox.Show(
                    "Đăng ký tài khoản thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đăng ký thất bại!\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}