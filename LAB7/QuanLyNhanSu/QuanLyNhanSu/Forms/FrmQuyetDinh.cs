
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmQuyetDinh : Form
    {
        private readonly QuyetDinhService service =
            new QuyetDinhService();

        // Quyết định chức vụ
        private TextBox txtMaQDCV;
        private ComboBox cboNVCV, cboChucVu;
        private DateTimePicker dtpNgayQDCV, dtpHieuLucCV;
        private NumericUpDown numPhuCapCV;
        private TextBox txtTimCV;
        private DataGridView dgvCV;

        // Danh mục chức vụ
        private TextBox txtMaChucVu;
        private TextBox txtTenChucVu;
        private TextBox txtMoTaChucVu;

        // Quyết định lương
        private TextBox txtMaQDL;
        private ComboBox cboNVLuong;
        private DateTimePicker dtpNgayQDL, dtpHieuLucLuong;
        private NumericUpDown numLuongCB;
        private NumericUpDown numPhuCapLuong;
        private NumericUpDown numPhuCapDocHai;
        private TextBox txtTimLuong;
        private DataGridView dgvLuong;

        public FrmQuyetDinh()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    NapDanhMuc();
                    NapQuyetDinhChucVu();
                    NapQuyetDinhLuong();
                    LamMoiCV();
                    LamMoiLuong();
                });
            };
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Quyết định chức vụ và lương";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1250, 800);
            MinimumSize = new Size(1050, 680);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ QUYẾT ĐỊNH CHỨC VỤ VÀ LƯƠNG",
                Dock = DockStyle.Top,
                Height = 65,
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 66, 105),
                TextAlign = ContentAlignment.MiddleCenter
            };

            TabControl tabs = new TabControl
            {
                Dock = DockStyle.Fill
            };

            TabPage tabCV =
                new TabPage("Quyết định chức vụ");

            TabPage tabLuong =
                new TabPage("Quyết định lương");

            TaoTabChucVu(tabCV);
            TaoTabLuong(tabLuong);

            tabs.TabPages.Add(tabCV);
            tabs.TabPages.Add(tabLuong);

            Controls.Add(tabs);
            Controls.Add(lblTitle);
        }

        // ==========================================
        // TAB QUYẾT ĐỊNH CHỨC VỤ
        // ==========================================

        private void TaoTabChucVu(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(4);
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 165));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 105));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            GroupBox grpQD = new GroupBox
            {
                Text = "Lập quyết định chức vụ",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(3);

            txtMaQDCV = TaoTextBox();
            cboNVCV = TaoComboBox();
            cboChucVu = TaoComboBox();
            dtpNgayQDCV = TaoNgay();
            dtpHieuLucCV = TaoNgay();
            numPhuCapCV = TaoTien();

            ThemTruong(form, 0, 0,
                "Mã quyết định:", txtMaQDCV);
            ThemTruong(form, 2, 0,
                "Nhân viên:", cboNVCV);

            ThemTruong(form, 0, 1,
                "Chức vụ:", cboChucVu);
            ThemTruong(form, 2, 1,
                "Ngày quyết định:", dtpNgayQDCV);

            ThemTruong(form, 0, 2,
                "Ngày hiệu lực:", dtpHieuLucCV);
            ThemTruong(form, 2, 2,
                "Phụ cấp chức vụ:", numPhuCapCV);

            grpQD.Controls.Add(form);

            GroupBox grpDanhMuc = new GroupBox
            {
                Text = "Thêm danh mục chức vụ",
                Dock = DockStyle.Fill
            };

            FlowLayoutPanel pnlDanhMuc = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(6, 13, 6, 4)
            };

            txtMaChucVu = new TextBox { Width = 110 };
            txtTenChucVu = new TextBox { Width = 180 };
            txtMoTaChucVu = new TextBox { Width = 250 };

            Button btnThemDM = TaoNut("Thêm chức vụ", 145);

            pnlDanhMuc.Controls.Add(TaoLabel("Mã:"));
            pnlDanhMuc.Controls.Add(txtMaChucVu);
            pnlDanhMuc.Controls.Add(TaoLabel("Tên:"));
            pnlDanhMuc.Controls.Add(txtTenChucVu);
            pnlDanhMuc.Controls.Add(TaoLabel("Mô tả:"));
            pnlDanhMuc.Controls.Add(txtMoTaChucVu);
            pnlDanhMuc.Controls.Add(btnThemDM);

            grpDanhMuc.Controls.Add(pnlDanhMuc);

            FlowLayoutPanel thanhCongCu = TaoThanhCongCu();

            Button btnLap = TaoNut("Lập quyết định", 150);
            Button btnLamMoi = TaoNut("Làm mới", 105);

            txtTimCV = new TextBox
            {
                Width = 230,
                Margin = new Padding(15, 9, 5, 5)
            };

            Button btnTim = TaoNut("Tìm kiếm", 105);

            thanhCongCu.Controls.Add(btnLap);
            thanhCongCu.Controls.Add(btnLamMoi);
            thanhCongCu.Controls.Add(txtTimCV);
            thanhCongCu.Controls.Add(btnTim);

            dgvCV = TaoBang();

            layout.Controls.Add(grpQD, 0, 0);
            layout.Controls.Add(grpDanhMuc, 0, 1);
            layout.Controls.Add(thanhCongCu, 0, 2);
            layout.Controls.Add(dgvCV, 0, 3);

            tab.Controls.Add(layout);

            // Các sự kiện
            btnThemDM.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.ThemChucVu(
                        txtMaChucVu.Text,
                        txtTenChucVu.Text,
                        txtMoTaChucVu.Text);

                    if (FormHelper.Bao(kq))
                    {
                        NapChucVu();
                        txtMaChucVu.Clear();
                        txtTenChucVu.Clear();
                        txtMoTaChucVu.Clear();
                    }
                });

            btnLap.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.LapQuyetDinhChucVu(
                        txtMaQDCV.Text,
                        FormHelper.Gia(cboNVCV),
                        FormHelper.Gia(cboChucVu),
                        dtpNgayQDCV.Value,
                        dtpHieuLucCV.Value,
                        numPhuCapCV.Value);

                    if (FormHelper.Bao(kq))
                    {
                        NapQuyetDinhChucVu();
                        LamMoiCV();
                    }
                });

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    txtTimCV.Clear();
                    NapDanhMuc();
                    NapQuyetDinhChucVu();
                    LamMoiCV();
                });

            btnTim.Click += (s, e) =>
                FormHelper.Chay(() =>
                    NapQuyetDinhChucVu(txtTimCV.Text));

            txtTimCV.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTim.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            dgvCV.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    ChonQuyetDinhCV();
            };
        }

        // ==========================================
        // TAB QUYẾT ĐỊNH LƯƠNG
        // ==========================================

        private void TaoTabLuong(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(3);

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 220));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            GroupBox grp = new GroupBox
            {
                Text = "Lập quyết định lương",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(4);

            txtMaQDL = TaoTextBox();
            cboNVLuong = TaoComboBox();
            dtpNgayQDL = TaoNgay();
            dtpHieuLucLuong = TaoNgay();

            numLuongCB = TaoTien();
            numPhuCapLuong = TaoTien();
            numPhuCapDocHai = TaoTien();

            ThemTruong(form, 0, 0,
                "Mã quyết định:", txtMaQDL);
            ThemTruong(form, 2, 0,
                "Nhân viên:", cboNVLuong);

            ThemTruong(form, 0, 1,
                "Ngày quyết định:", dtpNgayQDL);
            ThemTruong(form, 2, 1,
                "Ngày hiệu lực:", dtpHieuLucLuong);

            ThemTruong(form, 0, 2,
                "Lương cơ bản:", numLuongCB);
            ThemTruong(form, 2, 2,
                "Phụ cấp lương:", numPhuCapLuong);

            ThemTruong(form, 0, 3,
                "Phụ cấp độc hại:", numPhuCapDocHai);

            grp.Controls.Add(form);

            FlowLayoutPanel thanhCongCu = TaoThanhCongCu();

            Button btnLap = TaoNut("Lập quyết định", 150);
            Button btnLamMoi = TaoNut("Làm mới", 105);

            txtTimLuong = new TextBox
            {
                Width = 230,
                Margin = new Padding(15, 9, 5, 5)
            };

            Button btnTim = TaoNut("Tìm kiếm", 105);

            thanhCongCu.Controls.Add(btnLap);
            thanhCongCu.Controls.Add(btnLamMoi);
            thanhCongCu.Controls.Add(txtTimLuong);
            thanhCongCu.Controls.Add(btnTim);

            dgvLuong = TaoBang();

            layout.Controls.Add(grp, 0, 0);
            layout.Controls.Add(thanhCongCu, 0, 1);
            layout.Controls.Add(dgvLuong, 0, 2);

            tab.Controls.Add(layout);

            btnLap.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.LapQuyetDinhLuong(
                        txtMaQDL.Text,
                        FormHelper.Gia(cboNVLuong),
                        dtpNgayQDL.Value,
                        dtpHieuLucLuong.Value,
                        numLuongCB.Value,
                        numPhuCapLuong.Value,
                        numPhuCapDocHai.Value);

                    if (FormHelper.Bao(kq))
                    {
                        NapQuyetDinhLuong();
                        LamMoiLuong();
                    }
                });

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    txtTimLuong.Clear();
                    NapDanhMuc();
                    NapQuyetDinhLuong();
                    LamMoiLuong();
                });

            btnTim.Click += (s, e) =>
                FormHelper.Chay(() =>
                    NapQuyetDinhLuong(txtTimLuong.Text));

            txtTimLuong.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTim.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            dgvLuong.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    ChonQuyetDinhLuong();
            };
        }

        // ==========================================
        // NẠP DỮ LIỆU
        // ==========================================

        private void NapDanhMuc()
        {
            DataTable nv = service.LayNhanVien();

            FormHelper.Nap(
                cboNVCV, nv, "HienThi", "MaNV");

            FormHelper.Nap(
                cboNVLuong,
                service.LayNhanVien(),
                "HienThi", "MaNV");

            NapChucVu();
        }

        private void NapChucVu()
        {
            FormHelper.Nap(
                cboChucVu,
                service.LayChucVu(),
                "HienThi", "MaChucVu");
        }

        private void NapQuyetDinhChucVu(string tuKhoa = "")
        {
            dgvCV.DataSource =
                service.LayQuyetDinhChucVu(tuKhoa);

            DoiTenCot(dgvCV, "MaQDCV", "Mã quyết định");
            DoiTenCot(dgvCV, "MaNV", "Mã nhân viên");
            DoiTenCot(dgvCV, "HoTen", "Họ tên");
            DoiTenCot(dgvCV, "MaChucVu", "Mã chức vụ");
            DoiTenCot(dgvCV, "TenChucVu", "Chức vụ");
            DoiTenCot(dgvCV, "NgayQuyetDinh", "Ngày QĐ");
            DoiTenCot(dgvCV, "NgayHieuLuc", "Hiệu lực");
            DoiTenCot(dgvCV, "PhuCapChucVu", "Phụ cấp");

            DinhDangNgay(dgvCV, "NgayQuyetDinh");
            DinhDangNgay(dgvCV, "NgayHieuLuc");
            DinhDangTien(dgvCV, "PhuCapChucVu");

            dgvCV.ClearSelection();
        }

        private void NapQuyetDinhLuong(string tuKhoa = "")
        {
            dgvLuong.DataSource =
                service.LayQuyetDinhLuong(tuKhoa);

            DoiTenCot(dgvLuong, "MaQDL", "Mã quyết định");
            DoiTenCot(dgvLuong, "MaNV", "Mã nhân viên");
            DoiTenCot(dgvLuong, "HoTen", "Họ tên");
            DoiTenCot(dgvLuong, "NgayQuyetDinh", "Ngày QĐ");
            DoiTenCot(dgvLuong, "NgayHieuLuc", "Hiệu lực");
            DoiTenCot(dgvLuong, "LuongCoBan", "Lương cơ bản");
            DoiTenCot(dgvLuong, "PhuCapLuong", "Phụ cấp lương");
            DoiTenCot(dgvLuong, "PhuCapDocHai", "Phụ cấp độc hại");

            DinhDangNgay(dgvLuong, "NgayQuyetDinh");
            DinhDangNgay(dgvLuong, "NgayHieuLuc");

            DinhDangTien(dgvLuong, "LuongCoBan");
            DinhDangTien(dgvLuong, "PhuCapLuong");
            DinhDangTien(dgvLuong, "PhuCapDocHai");

            dgvLuong.ClearSelection();
        }

        // ==========================================
        // CHỌN DÒNG ĐỂ XEM THÔNG TIN
        // ==========================================

        private void ChonQuyetDinhCV()
        {
            if (dgvCV.CurrentRow == null ||
                dgvCV.CurrentRow.IsNewRow)
                return;

            DataGridViewRow row = dgvCV.CurrentRow;

            txtMaQDCV.Text = LayO(row, "MaQDCV");
            cboNVCV.SelectedValue = LayO(row, "MaNV");
            cboChucVu.SelectedValue = LayO(row, "MaChucVu");

            dtpNgayQDCV.Value =
                Convert.ToDateTime(
                    row.Cells["NgayQuyetDinh"].Value);

            dtpHieuLucCV.Value =
                Convert.ToDateTime(
                    row.Cells["NgayHieuLuc"].Value);

            GanTien(numPhuCapCV,
                row.Cells["PhuCapChucVu"].Value);
        }

        private void ChonQuyetDinhLuong()
        {
            if (dgvLuong.CurrentRow == null ||
                dgvLuong.CurrentRow.IsNewRow)
                return;

            DataGridViewRow row = dgvLuong.CurrentRow;

            txtMaQDL.Text = LayO(row, "MaQDL");
            cboNVLuong.SelectedValue = LayO(row, "MaNV");

            dtpNgayQDL.Value =
                Convert.ToDateTime(
                    row.Cells["NgayQuyetDinh"].Value);

            dtpHieuLucLuong.Value =
                Convert.ToDateTime(
                    row.Cells["NgayHieuLuc"].Value);

            GanTien(numLuongCB,
                row.Cells["LuongCoBan"].Value);

            GanTien(numPhuCapLuong,
                row.Cells["PhuCapLuong"].Value);

            GanTien(numPhuCapDocHai,
                row.Cells["PhuCapDocHai"].Value);
        }

        private void LamMoiCV()
        {
            txtMaQDCV.Clear();
            dtpNgayQDCV.Value = DateTime.Today;
            dtpHieuLucCV.Value = DateTime.Today;
            numPhuCapCV.Value = 0;

            if (cboNVCV.Items.Count > 0)
                cboNVCV.SelectedIndex = 0;

            if (cboChucVu.Items.Count > 0)
                cboChucVu.SelectedIndex = 0;

            dgvCV.ClearSelection();
        }

        private void LamMoiLuong()
        {
            txtMaQDL.Clear();
            dtpNgayQDL.Value = DateTime.Today;
            dtpHieuLucLuong.Value = DateTime.Today;

            numLuongCB.Value = 10000000;
            numPhuCapLuong.Value = 0;
            numPhuCapDocHai.Value = 0;

            if (cboNVLuong.Items.Count > 0)
                cboNVLuong.SelectedIndex = 0;

            dgvLuong.ClearSelection();
        }

        // ==========================================
        // HÀM GIAO DIỆN DÙNG CHUNG
        // ==========================================

        private TableLayoutPanel TaoLayout(int soDong)
        {
            return new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = soDong,
                Padding = new Padding(12)
            };
        }

        private TableLayoutPanel TaoForm(int soDong)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = soDong,
                Padding = new Padding(8)
            };

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 155));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 165));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < soDong; i++)
                form.RowStyles.Add(
                    new RowStyle(
                        SizeType.Percent, 100F / soDong));

            return form;
        }

        private void ThemTruong(
            TableLayoutPanel form,
            int cot, int dong,
            string nhan, Control control)
        {
            Label lbl = new Label
            {
                Text = nhan,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(5)
            };

            form.Controls.Add(lbl, cot, dong);
            form.Controls.Add(control, cot + 1, dong);
        }

        private FlowLayoutPanel TaoThanhCongCu()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };
        }

        private TextBox TaoTextBox()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 8, 12, 5)
            };
        }

        private ComboBox TaoComboBox()
        {
            return new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle =
                    ComboBoxStyle.DropDownList,
                Margin = new Padding(5, 8, 12, 5)
            };
        }

        private DateTimePicker TaoNgay()
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Margin = new Padding(5, 8, 12, 5)
            };
        }

        private NumericUpDown TaoTien()
        {
            return new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 100000000000,
                DecimalPlaces = 0,
                Increment = 100000,
                ThousandsSeparator = true,
                Margin = new Padding(5, 8, 12, 5)
            };
        }

        private Button TaoNut(string noiDung, int rong)
        {
            Button btn = new Button
            {
                Text = noiDung,
                Width = rong,
                Height = 38,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(35, 78, 121),
                Cursor = Cursors.Hand,
                Margin = new Padding(5)
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Label TaoLabel(string noiDung)
        {
            return new Label
            {
                Text = noiDung,
                AutoSize = true,
                Margin = new Padding(8, 12, 5, 5)
            };
        }

        private DataGridView TaoBang()
        {
            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 78, 121);

            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgv.ColumnHeadersHeight = 38;
            dgv.RowTemplate.Height = 30;

            return dgv;
        }

        private void DoiTenCot(
            DataGridView dgv,
            string tenCot,
            string tieuDe)
        {
            if (dgv.Columns.Contains(tenCot))
                dgv.Columns[tenCot].HeaderText = tieuDe;
        }

        private void DinhDangNgay(
            DataGridView dgv, string tenCot)
        {
            if (dgv.Columns.Contains(tenCot))
                dgv.Columns[tenCot]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void DinhDangTien(
            DataGridView dgv, string tenCot)
        {
            if (dgv.Columns.Contains(tenCot))
                dgv.Columns[tenCot]
                    .DefaultCellStyle.Format = "N0";
        }

        private void GanTien(
            NumericUpDown num, object value)
        {
            decimal tien = Convert.ToDecimal(value);

            num.Value = Math.Min(
                num.Maximum,
                Math.Max(num.Minimum, tien));
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
