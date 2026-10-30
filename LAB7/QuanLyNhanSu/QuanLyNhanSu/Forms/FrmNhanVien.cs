using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmNhanVien : Form
    {
        private readonly NhanVienService service =
            new NhanVienService();

        // Nhân viên
        private DataGridView dgvNhanVien;
        private TextBox txtMaNV;
        private TextBox txtHoTen;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;
        private TextBox txtTimNhanVien;
        private ComboBox cboGioiTinh;
        private ComboBox cboTrangThai;
        private DateTimePicker dtpNgaySinh;

        // Phòng ban
        private DataGridView dgvPhongBan;
        private TextBox txtMaPhongBan;
        private TextBox txtTenPhongBan;
        private TextBox txtMoTa;
        private TextBox txtTimPhongBan;

        public FrmNhanVien()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    NapNhanVien();
                    NapPhongBan();
                });
            };
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Quản lý nhân viên và phòng ban";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1150, 750);
            MinimumSize = new Size(950, 650);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ NHÂN VIÊN VÀ PHÒNG BAN",
                Dock = DockStyle.Top,
                Height = 65,
                Font = new Font(
                    "Segoe UI", 17F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(32, 66, 105)
            };

            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                Padding = new Point(15, 8)
            };

            TabPage tabNhanVien = new TabPage("Nhân viên");
            TabPage tabPhongBan = new TabPage("Phòng ban");

            TaoTabNhanVien(tabNhanVien);
            TaoTabPhongBan(tabPhongBan);

            tabControl.TabPages.Add(tabNhanVien);
            tabControl.TabPages.Add(tabPhongBan);

            Controls.Add(tabControl);
            Controls.Add(lblTieuDe);
        }

        // =================================================
        // TAB NHÂN VIÊN
        // =================================================

        private void TaoTabNhanVien(TabPage tab)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 220));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 4,
                Padding = new Padding(8)
            };

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 145));

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 145));

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 4; i++)
            {
                form.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25));
            }

            txtMaNV = TaoTextBox();
            txtHoTen = TaoTextBox();
            txtDiaChi = TaoTextBox();
            txtDienThoai = TaoTextBox();

            cboGioiTinh = TaoComboBox(
                "Nam", "Nữ", "Khác");

            cboTrangThai = TaoComboBox(
                "Thử việc", "Chính thức", "Nghỉ việc");

            dtpNgaySinh = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Dock = DockStyle.Fill,
                Value = new DateTime(2002, 1, 1)
            };

            ThemTruong(form, 0, 0,
                "Mã nhân viên:", txtMaNV);

            ThemTruong(form, 2, 0,
                "Họ và tên:", txtHoTen);

            ThemTruong(form, 0, 1,
                "Ngày sinh:", dtpNgaySinh);

            ThemTruong(form, 2, 1,
                "Giới tính:", cboGioiTinh);

            ThemTruong(form, 0, 2,
                "Điện thoại:", txtDienThoai);

            ThemTruong(form, 2, 2,
                "Trạng thái:", cboTrangThai);

            ThemTruong(form, 0, 3,
                "Địa chỉ:", txtDiaChi);

            FlowLayoutPanel thanhCongCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };

            Button btnThem = TaoNut("Thêm nhân viên");
            Button btnCapNhat = TaoNut("Cập nhật");
            Button btnLamMoi = TaoNut("Làm mới");

            txtTimNhanVien = new TextBox
            {
                Width = 200,
                Margin = new Padding(15, 9, 5, 5)
            };

            Button btnTim = TaoNut("Tìm kiếm");

            thanhCongCu.Controls.Add(btnThem);
            thanhCongCu.Controls.Add(btnCapNhat);
            thanhCongCu.Controls.Add(btnLamMoi);
            thanhCongCu.Controls.Add(txtTimNhanVien);
            thanhCongCu.Controls.Add(btnTim);

            dgvNhanVien = TaoBang();

            dgvNhanVien.SelectionChanged +=
                (s, e) => ChonNhanVien();

            btnThem.Click += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    var ketQua = service.ThemNhanVien(
                        txtMaNV.Text,
                        txtHoTen.Text,
                        dtpNgaySinh.Value,
                        cboGioiTinh.Text,
                        txtDiaChi.Text,
                        txtDienThoai.Text,
                        cboTrangThai.Text);

                    if (FormHelper.Bao(ketQua))
                    {
                        NapNhanVien();
                        XoaNhapNhanVien();
                    }
                });
            };

            btnCapNhat.Click += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    var ketQua = service.CapNhatNhanVien(
                        txtMaNV.Text,
                        txtHoTen.Text,
                        dtpNgaySinh.Value,
                        cboGioiTinh.Text,
                        txtDiaChi.Text,
                        txtDienThoai.Text,
                        cboTrangThai.Text);

                    if (FormHelper.Bao(ketQua))
                        NapNhanVien();
                });
            };

            btnTim.Click += (s, e) =>
                FormHelper.Chay(
                    () => NapNhanVien(txtTimNhanVien.Text));

            txtTimNhanVien.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTim.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            btnLamMoi.Click += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    txtTimNhanVien.Clear();
                    NapNhanVien();
                    XoaNhapNhanVien();
                });
            };

            layout.Controls.Add(form, 0, 0);
            layout.Controls.Add(thanhCongCu, 0, 1);
            layout.Controls.Add(dgvNhanVien, 0, 2);

            tab.Controls.Add(layout);
        }

        private void NapNhanVien(string tuKhoa = "")
        {
            dgvNhanVien.DataSource =
                service.LayNhanVien(tuKhoa);

            if (dgvNhanVien.Columns.Contains("MaNV"))
                dgvNhanVien.Columns["MaNV"].HeaderText =
                    "Mã nhân viên";

            if (dgvNhanVien.Columns.Contains("HoTen"))
                dgvNhanVien.Columns["HoTen"].HeaderText =
                    "Họ và tên";

            if (dgvNhanVien.Columns.Contains("NgaySinh"))
            {
                dgvNhanVien.Columns["NgaySinh"].HeaderText =
                    "Ngày sinh";

                dgvNhanVien.Columns["NgaySinh"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvNhanVien.Columns.Contains("GioiTinh"))
                dgvNhanVien.Columns["GioiTinh"].HeaderText =
                    "Giới tính";

            if (dgvNhanVien.Columns.Contains("DiaChi"))
                dgvNhanVien.Columns["DiaChi"].HeaderText =
                    "Địa chỉ";

            if (dgvNhanVien.Columns.Contains("DienThoai"))
                dgvNhanVien.Columns["DienThoai"].HeaderText =
                    "Điện thoại";

            if (dgvNhanVien.Columns.Contains("TrangThaiLamViec"))
                dgvNhanVien.Columns["TrangThaiLamViec"].HeaderText =
                    "Trạng thái làm việc";

            dgvNhanVien.ClearSelection();
        }

        private void ChonNhanVien()
        {
            if (dgvNhanVien.CurrentRow == null ||
                dgvNhanVien.CurrentRow.IsNewRow)
                return;

            DataGridViewRow row = dgvNhanVien.CurrentRow;

            txtMaNV.Text = LayO(row, "MaNV");
            txtHoTen.Text = LayO(row, "HoTen");
            txtDiaChi.Text = LayO(row, "DiaChi");
            txtDienThoai.Text = LayO(row, "DienThoai");
            cboGioiTinh.Text = LayO(row, "GioiTinh");
            cboTrangThai.Text = LayO(row, "TrangThaiLamViec");

            object ngaySinh = row.Cells["NgaySinh"].Value;

            if (ngaySinh != null &&
                ngaySinh != DBNull.Value)
            {
                dtpNgaySinh.Value =
                    Convert.ToDateTime(ngaySinh);
            }
        }

        private void XoaNhapNhanVien()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();

            dtpNgaySinh.Value = new DateTime(2002, 1, 1);
            cboGioiTinh.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;

            dgvNhanVien.ClearSelection();
        }

        // =================================================
        // TAB PHÒNG BAN
        // =================================================

        private void TaoTabPhongBan(TabPage tab)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 165));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(8)
            };

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 155));

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < 3; i++)
            {
                form.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 33.33F));
            }

            txtMaPhongBan = TaoTextBox();
            txtTenPhongBan = TaoTextBox();
            txtMoTa = TaoTextBox();

            ThemTruong(form, 0, 0,
                "Mã phòng ban:", txtMaPhongBan);

            ThemTruong(form, 0, 1,
                "Tên phòng ban:", txtTenPhongBan);

            ThemTruong(form, 0, 2,
                "Mô tả:", txtMoTa);

            FlowLayoutPanel thanhCongCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };

            Button btnThem = TaoNut("Thêm phòng ban");
            Button btnCapNhat = TaoNut("Cập nhật");
            Button btnLamMoi = TaoNut("Làm mới");

            txtTimPhongBan = new TextBox
            {
                Width = 200,
                Margin = new Padding(15, 9, 5, 5)
            };

            Button btnTim = TaoNut("Tìm kiếm");

            thanhCongCu.Controls.Add(btnThem);
            thanhCongCu.Controls.Add(btnCapNhat);
            thanhCongCu.Controls.Add(btnLamMoi);
            thanhCongCu.Controls.Add(txtTimPhongBan);
            thanhCongCu.Controls.Add(btnTim);

            dgvPhongBan = TaoBang();

            dgvPhongBan.SelectionChanged +=
                (s, e) => ChonPhongBan();

            btnThem.Click += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    var ketQua = service.ThemPhongBan(
                        txtMaPhongBan.Text,
                        txtTenPhongBan.Text,
                        txtMoTa.Text);

                    if (FormHelper.Bao(ketQua))
                    {
                        NapPhongBan();
                        XoaNhapPhongBan();
                    }
                });
            };

            btnCapNhat.Click += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    var ketQua = service.CapNhatPhongBan(
                        txtMaPhongBan.Text,
                        txtTenPhongBan.Text,
                        txtMoTa.Text);

                    if (FormHelper.Bao(ketQua))
                        NapPhongBan();
                });
            };

            btnTim.Click += (s, e) =>
                FormHelper.Chay(
                    () => NapPhongBan(txtTimPhongBan.Text));

            txtTimPhongBan.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTim.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            btnLamMoi.Click += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    txtTimPhongBan.Clear();
                    NapPhongBan();
                    XoaNhapPhongBan();
                });
            };

            layout.Controls.Add(form, 0, 0);
            layout.Controls.Add(thanhCongCu, 0, 1);
            layout.Controls.Add(dgvPhongBan, 0, 2);

            tab.Controls.Add(layout);
        }

        private void NapPhongBan(string tuKhoa = "")
        {
            dgvPhongBan.DataSource =
                service.LayPhongBan(tuKhoa);

            if (dgvPhongBan.Columns.Contains("MaPhongBan"))
                dgvPhongBan.Columns["MaPhongBan"].HeaderText =
                    "Mã phòng ban";

            if (dgvPhongBan.Columns.Contains("TenPhongBan"))
                dgvPhongBan.Columns["TenPhongBan"].HeaderText =
                    "Tên phòng ban";

            if (dgvPhongBan.Columns.Contains("MoTa"))
                dgvPhongBan.Columns["MoTa"].HeaderText =
                    "Mô tả";

            dgvPhongBan.ClearSelection();
        }

        private void ChonPhongBan()
        {
            if (dgvPhongBan.CurrentRow == null ||
                dgvPhongBan.CurrentRow.IsNewRow)
                return;

            DataGridViewRow row = dgvPhongBan.CurrentRow;

            txtMaPhongBan.Text = LayO(row, "MaPhongBan");
            txtTenPhongBan.Text = LayO(row, "TenPhongBan");
            txtMoTa.Text = LayO(row, "MoTa");
        }

        private void XoaNhapPhongBan()
        {
            txtMaPhongBan.Clear();
            txtTenPhongBan.Clear();
            txtMoTa.Clear();

            dgvPhongBan.ClearSelection();
        }

        // =================================================
        // CÁC HÀM TẠO GIAO DIỆN DÙNG CHUNG
        // =================================================

        private TextBox TaoTextBox()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 8, 12, 5)
            };
        }

        private ComboBox TaoComboBox(params string[] ds)
        {
            ComboBox cbo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(5, 8, 12, 5)
            };

            cbo.Items.AddRange(ds);

            if (cbo.Items.Count > 0)
                cbo.SelectedIndex = 0;

            return cbo;
        }

        private Button TaoNut(string noiDung)
        {
            Button btn = new Button
            {
                Text = noiDung,
                Width = 135,
                Height = 38,
                BackColor = Color.FromArgb(35, 78, 121),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(5),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        private DataGridView TaoBang()
        {
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 78, 121);

            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 38;
            dgv.RowTemplate.Height = 32;

            return dgv;
        }

        private void ThemTruong(
            TableLayoutPanel table,
            int cot,
            int dong,
            string nhan,
            Control control)
        {
            Label lbl = new Label
            {
                Text = nhan,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false,
                Margin = new Padding(5)
            };

            table.Controls.Add(lbl, cot, dong);
            table.Controls.Add(control, cot + 1, dong);
        }

        private string LayO(
            DataGridViewRow row,
            string tenCot)
        {
            object value = row.Cells[tenCot].Value;

            return value == null || value == DBNull.Value
                ? ""
                : Convert.ToString(value);
        }
    }
}