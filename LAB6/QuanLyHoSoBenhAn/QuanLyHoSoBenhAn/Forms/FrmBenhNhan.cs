
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmBenhNhan : Form
    {
        private readonly BenhNhanService service =
            new BenhNhanService();

        private TextBox txtMaBN;
        private TextBox txtHoTen;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;
        private TextBox txtAnh;
        private TextBox txtTimKiem;
        private DateTimePicker dtpNgaySinh;
        private DataGridView dgvBenhNhan;

        public FrmBenhNhan()
        {
            InitializeComponent();
            TaoGiaoDien();
            Load += (s, e) => TaiDanhSach();
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý bệnh nhân";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1120, 720);
            MinimumSize = new Size(940, 620);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(20),
            };

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 62));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 215));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 58));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            var lblTitle = new Label
            {
                Text = "QUẢN LÝ BỆNH NHÂN",
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 62, 95),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            root.Controls.Add(lblTitle, 0, 0);

            var input = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = 3,
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 130));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 130));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 3; i++)
                input.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 33.333F));

            txtMaBN = TaoTextBox();
            txtHoTen = TaoTextBox();
            txtDiaChi = TaoTextBox();
            txtDienThoai = TaoTextBox();
            txtAnh = TaoTextBox();

            dtpNgaySinh = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 12, 10, 12)
            };

            ThemTruong(input, "Mã bệnh nhân:", txtMaBN, 0, 0);
            ThemTruong(input, "Họ tên:", txtHoTen, 2, 0);
            ThemTruong(input, "Ngày sinh:", dtpNgaySinh, 0, 1);
            ThemTruong(input, "Điện thoại:", txtDienThoai, 2, 1);
            ThemTruong(input, "Địa chỉ:", txtDiaChi, 0, 2);
            ThemTruong(input, "Ảnh 3×4:", txtAnh, 2, 2);

            root.Controls.Add(input, 0, 1);

            var bar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };

            Button btnThem = TaoButton("THÊM");
            Button btnSua = TaoButton("CẬP NHẬT");
            Button btnMoi = TaoButton("LÀM MỚI");
            Button btnTim = TaoButton("TÌM KIẾM");

            txtTimKiem = new TextBox
            {
                Width = 210,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(15, 12, 5, 5)
            };

            bar.Controls.Add(btnThem);
            bar.Controls.Add(btnSua);
            bar.Controls.Add(btnMoi);
            bar.Controls.Add(txtTimKiem);
            bar.Controls.Add(btnTim);

            root.Controls.Add(bar, 0, 2);

            dgvBenhNhan = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            dgvBenhNhan.CellClick += DgvBenhNhan_CellClick;
            root.Controls.Add(dgvBenhNhan, 0, 3);
            Controls.Add(root);

            btnThem.Click += (s, e) => ThucHien(() =>
            {
                service.Them(
                    txtMaBN.Text,
                    txtHoTen.Text,
                    dtpNgaySinh.Value,
                    txtDiaChi.Text,
                    txtDienThoai.Text,
                    txtAnh.Text);

                MessageBox.Show("Thêm bệnh nhân thành công!");
                TaiDanhSach();
                LamMoi();
            });

            btnSua.Click += (s, e) => ThucHien(() =>
            {
                service.Sua(
                    txtMaBN.Text,
                    txtHoTen.Text,
                    dtpNgaySinh.Value,
                    txtDiaChi.Text,
                    txtDienThoai.Text,
                    txtAnh.Text);

                MessageBox.Show("Cập nhật bệnh nhân thành công!");
                TaiDanhSach();
            });

            btnMoi.Click += (s, e) =>
            {
                LamMoi();
                TaiDanhSach();
            };

            btnTim.Click += (s, e) => TaiDanhSach();
            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    TaiDanhSach();
                    e.SuppressKeyPress = true;
                }
            };
        }

        private TextBox TaoTextBox()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 12, 10, 12)
            };
        }

        private void ThemTruong(
            TableLayoutPanel panel,
            string ten,
            Control control,
            int col,
            int row)
        {
            panel.Controls.Add(new Label
            {
                Text = ten,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, col, row);

            panel.Controls.Add(control, col + 1, row);
        }

        private Button TaoButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Width = 125,
                Height = 38,
                Margin = new Padding(5, 7, 5, 5),
                BackColor = Color.FromArgb(32, 85, 145),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void TaiDanhSach()
        {
            ThucHien(() =>
            {
                dgvBenhNhan.DataSource =
                    service.LayDanhSach(
                        txtTimKiem == null ? "" : txtTimKiem.Text);
            });
        }

        private void DgvBenhNhan_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvBenhNhan.Rows[e.RowIndex];

            txtMaBN.Text =
                Convert.ToString(row.Cells["MaBN"].Value);

            txtHoTen.Text =
                Convert.ToString(row.Cells["HoTen"].Value);

            txtDiaChi.Text =
                Convert.ToString(row.Cells["DiaChi"].Value);

            txtDienThoai.Text =
                Convert.ToString(row.Cells["DienThoai"].Value);

            txtAnh.Text =
                Convert.ToString(row.Cells["Anh3x4"].Value);

            object ngaySinh = row.Cells["NgaySinh"].Value;
            if (ngaySinh != null &&
                ngaySinh != DBNull.Value)
                dtpNgaySinh.Value = Convert.ToDateTime(ngaySinh);

            txtMaBN.ReadOnly = true;
        }

        private void LamMoi()
        {
            txtMaBN.ReadOnly = false;
            txtMaBN.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtAnh.Clear();
            txtTimKiem.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            dgvBenhNhan.ClearSelection();
            txtMaBN.Focus();
        }

        private void ThucHien(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
