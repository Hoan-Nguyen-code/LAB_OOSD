
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmKhenThuong : Form
    {
        private readonly KhenThuongService service =
            new KhenThuongService();

        // Quyết định
        private TextBox txtMaQD, txtLyDo, txtNguoiKy, txtTim;
        private ComboBox cboDoiTuong, cboNhanVien, cboPhongBan;
        private DateTimePicker dtpNgayQD;
        private NumericUpDown numTienThuong;
        private DataGridView dgvQuyetDinh;

        // Phiếu chi
        private TextBox txtMaPhieuChi;
        private ComboBox cboQuyetDinh, cboHinhThuc;
        private DateTimePicker dtpNgayChi;
        private NumericUpDown numTienChi;
        private DataGridView dgvPhieuChi;

        public FrmKhenThuong()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapDanhMuc();
                    NapQuyetDinh();
                    NapQuyetDinhChuaChi();
                    NapPhieuChi();
                    LamMoiQuyetDinh();
                    LamMoiPhieuChi();
                });
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Quản lý khen thưởng";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1220, 790);
            MinimumSize = new Size(1000, 670);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label title = new Label
            {
                Text = "QUẢN LÝ KHEN THƯỞNG NHÂN SỰ",
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

            TabPage tabQuyetDinh =
                new TabPage("Quyết định khen thưởng");

            TabPage tabPhieuChi =
                new TabPage("Phiếu chi khen thưởng");

            TaoTabQuyetDinh(tabQuyetDinh);
            TaoTabPhieuChi(tabPhieuChi);

            tabs.TabPages.Add(tabQuyetDinh);
            tabs.TabPages.Add(tabPhieuChi);

            Controls.Add(tabs);
            Controls.Add(title);
        }

        // ==========================================
        // TAB QUYẾT ĐỊNH
        // ==========================================

        private void TaoTabQuyetDinh(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(3, 245, 58);

            GroupBox grp = new GroupBox
            {
                Text = "Lập quyết định khen thưởng",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(4);

            txtMaQD = TaoTextBox();
            txtLyDo = TaoTextBox();
            txtNguoiKy = TaoTextBox();

            cboDoiTuong = TaoComboBox();
            cboDoiTuong.Items.AddRange(
                new object[] { "Cá nhân", "Phòng ban" });
            cboDoiTuong.SelectedIndex = 0;

            cboNhanVien = TaoComboBox();
            cboPhongBan = TaoComboBox();

            dtpNgayQD = TaoNgay();
            numTienThuong = TaoTien();
            numTienThuong.Value = 1000000;

            ThemTruong(form, 0, 0,
                "Mã quyết định:", txtMaQD);
            ThemTruong(form, 2, 0,
                "Đối tượng:", cboDoiTuong);

            ThemTruong(form, 0, 1,
                "Nhân viên:", cboNhanVien);
            ThemTruong(form, 2, 1,
                "Phòng ban:", cboPhongBan);

            ThemTruong(form, 0, 2,
                "Ngày quyết định:", dtpNgayQD);
            ThemTruong(form, 2, 2,
                "Số tiền thưởng:", numTienThuong);

            ThemTruong(form, 0, 3,
                "Người ký:", txtNguoiKy);
            ThemTruong(form, 2, 3,
                "Lý do:", txtLyDo);

            grp.Controls.Add(form);

            FlowLayoutPanel congCu = TaoThanhCongCu();

            Button btnLap = TaoNut("Lập quyết định", 155);
            Button btnMoi = TaoNut("Làm mới", 105);
            txtTim = new TextBox
            {
                Width = 230,
                Margin = new Padding(15, 9, 5, 5)
            };
            Button btnTim = TaoNut("Tìm kiếm", 105);

            congCu.Controls.Add(btnLap);
            congCu.Controls.Add(btnMoi);
            congCu.Controls.Add(txtTim);
            congCu.Controls.Add(btnTim);

            dgvQuyetDinh = TaoBang();

            layout.Controls.Add(grp, 0, 0);
            layout.Controls.Add(congCu, 0, 1);
            layout.Controls.Add(dgvQuyetDinh, 0, 2);

            tab.Controls.Add(layout);

            cboDoiTuong.SelectedIndexChanged += (s, e) =>
                CapNhatDoiTuong();

            btnLap.Click += (s, e) =>
                FormHelper.Chay(LapQuyetDinh);

            btnMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    txtTim.Clear();
                    NapDanhMuc();
                    NapQuyetDinh();
                    LamMoiQuyetDinh();
                });

            btnTim.Click += (s, e) =>
                FormHelper.Chay(
                    () => NapQuyetDinh(txtTim.Text));

            txtTim.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTim.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            dgvQuyetDinh.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    XemQuyetDinh(e.RowIndex);
            };

            CapNhatDoiTuong();
        }

        private void CapNhatDoiTuong()
        {
            bool caNhan = cboDoiTuong.Text == "Cá nhân";

            cboNhanVien.Enabled = caNhan;
            cboPhongBan.Enabled = !caNhan;
        }

        private void LapQuyetDinh()
        {
            string maDoiTuong =
                cboDoiTuong.Text == "Cá nhân"
                    ? FormHelper.Gia(cboNhanVien)
                    : FormHelper.Gia(cboPhongBan);

            var kq = service.LapQuyetDinh(
                txtMaQD.Text,
                cboDoiTuong.Text,
                maDoiTuong,
                dtpNgayQD.Value,
                txtLyDo.Text,
                numTienThuong.Value,
                txtNguoiKy.Text);

            if (FormHelper.Bao(kq))
            {
                NapQuyetDinh();
                NapQuyetDinhChuaChi();
                LamMoiQuyetDinh();
            }
        }

        private void XemQuyetDinh(int rowIndex)
        {
            DataGridViewRow row =
                dgvQuyetDinh.Rows[rowIndex];

            txtMaQD.Text = LayO(row, "MaQDKT");
            cboDoiTuong.Text = LayO(row, "DoiTuong");
            cboNhanVien.SelectedValue = LayO(row, "MaNV");
            cboPhongBan.SelectedValue =
                LayO(row, "MaPhongBan");

            dtpNgayQD.Value = Convert.ToDateTime(
                row.Cells["NgayQuyetDinh"].Value);

            numTienThuong.Value = GioiHanTien(
                numTienThuong,
                row.Cells["SoTienThuong"].Value);

            txtLyDo.Text = LayO(row, "LyDo");
            txtNguoiKy.Text = LayO(row, "NguoiKy");
        }

        private void LamMoiQuyetDinh()
        {
            txtMaQD.Clear();
            txtLyDo.Clear();
            txtNguoiKy.Clear();

            cboDoiTuong.SelectedIndex = 0;

            if (cboNhanVien.Items.Count > 0)
                cboNhanVien.SelectedIndex = 0;

            if (cboPhongBan.Items.Count > 0)
                cboPhongBan.SelectedIndex = 0;

            dtpNgayQD.Value = DateTime.Today;
            numTienThuong.Value = 1000000;

            CapNhatDoiTuong();
            dgvQuyetDinh.ClearSelection();
        }

        // ==========================================
        // TAB PHIẾU CHI
        // ==========================================

        private void TaoTabPhieuChi(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(3, 225, 58);

            GroupBox grp = new GroupBox
            {
                Text = "Lập phiếu chi khen thưởng",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(3);

            txtMaPhieuChi = TaoTextBox();
            cboQuyetDinh = TaoComboBox();
            cboHinhThuc = TaoComboBox();

            cboHinhThuc.Items.AddRange(new object[]
            {
                "Tiền mặt", "Chuyển khoản", "Qua lương"
            });
            cboHinhThuc.SelectedIndex = 0;

            dtpNgayChi = TaoNgay();
            numTienChi = TaoTien();

            ThemTruong(form, 0, 0,
                "Mã phiếu chi:", txtMaPhieuChi);
            ThemTruong(form, 2, 0,
                "Quyết định:", cboQuyetDinh);

            ThemTruong(form, 0, 1,
                "Ngày chi:", dtpNgayChi);
            ThemTruong(form, 2, 1,
                "Số tiền chi:", numTienChi);

            ThemTruong(form, 0, 2,
                "Hình thức chi:", cboHinhThuc);

            grp.Controls.Add(form);

            FlowLayoutPanel congCu = TaoThanhCongCu();

            Button btnLap = TaoNut("Lập phiếu chi", 150);
            Button btnTai = TaoNut("Tải danh sách", 155);
            Button btnMoi = TaoNut("Làm mới", 110);

            congCu.Controls.Add(btnLap);
            congCu.Controls.Add(btnTai);
            congCu.Controls.Add(btnMoi);

            dgvPhieuChi = TaoBang();

            layout.Controls.Add(grp, 0, 0);
            layout.Controls.Add(congCu, 0, 1);
            layout.Controls.Add(dgvPhieuChi, 0, 2);

            tab.Controls.Add(layout);

            cboQuyetDinh.SelectedIndexChanged +=
                (s, e) => DongBoQuyetDinh();

            btnLap.Click += (s, e) =>
                FormHelper.Chay(LapPhieuChi);

            btnTai.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapQuyetDinhChuaChi();
                    NapPhieuChi();
                });

            btnMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapQuyetDinhChuaChi();
                    NapPhieuChi();
                    LamMoiPhieuChi();
                });

            dgvPhieuChi.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row =
                    dgvPhieuChi.Rows[e.RowIndex];

                MessageBox.Show(
                    "Phiếu chi: " + LayO(row, "MaPhieuChi") +
                    "\nQuyết định: " + LayO(row, "MaQDKT") +
                    "\nĐối tượng: " + LayO(row, "TenDoiTuong") +
                    "\nSố tiền: " +
                    Convert.ToDecimal(
                        row.Cells["SoTienChi"].Value)
                        .ToString("N0") + " đồng" +
                    "\nHình thức: " + LayO(row, "HinhThucChi"),
                    "Thông tin phiếu chi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };
        }

        private void DongBoQuyetDinh()
        {
            DataRowView row =
                cboQuyetDinh.SelectedItem as DataRowView;

            if (row == null)
            {
                numTienChi.Value = 0;
                return;
            }

            numTienChi.Value = GioiHanTien(
                numTienChi, row["SoTienThuong"]);

            DateTime ngayQD =
                Convert.ToDateTime(row["NgayQuyetDinh"]);

            if (dtpNgayChi.Value.Date < ngayQD.Date)
                dtpNgayChi.Value = ngayQD;

            bool caNhan =
                Convert.ToString(row["DoiTuong"]) == "Cá nhân";

            // Chưa hỗ trợ phân bổ thưởng phòng ban qua lương.
            if (!caNhan &&
                cboHinhThuc.Text == "Qua lương")
                cboHinhThuc.SelectedIndex = 0;

            
        }

        private void LapPhieuChi()
        {
            DataRowView row =
                cboQuyetDinh.SelectedItem as DataRowView;

            if (row == null)
            {
                MessageBox.Show(
                    "Không còn quyết định nào chưa lập phiếu chi.");
                return;
            }

            var kq = service.LapPhieuChi(
                txtMaPhieuChi.Text,
                FormHelper.Gia(cboQuyetDinh),
                dtpNgayChi.Value,
                numTienChi.Value,
                cboHinhThuc.Text);

            if (FormHelper.Bao(kq))
            {
                NapQuyetDinh();
                NapQuyetDinhChuaChi();
                NapPhieuChi();
                LamMoiPhieuChi();
            }
        }

        private void LamMoiPhieuChi()
        {
            txtMaPhieuChi.Clear();
            dtpNgayChi.Value = DateTime.Today;
            cboHinhThuc.SelectedIndex = 0;
            DongBoQuyetDinh();
            dgvPhieuChi.ClearSelection();
        }

        // ==========================================
        // NẠP DANH MỤC VÀ DANH SÁCH
        // ==========================================

        private void NapDanhMuc()
        {
            FormHelper.Nap(
                cboNhanVien, service.LayNhanVien(),
                "HienThi", "MaNV");

            FormHelper.Nap(
                cboPhongBan, service.LayPhongBan(),
                "HienThi", "MaPhongBan");
        }

        private void NapQuyetDinh(string tuKhoa = "")
        {
            dgvQuyetDinh.DataSource =
                service.LayQuyetDinh(tuKhoa);

            DoiTenCot(dgvQuyetDinh, "MaQDKT", "Mã QĐ");
            DoiTenCot(dgvQuyetDinh, "DoiTuong", "Đối tượng");
            DoiTenCot(dgvQuyetDinh, "MaNV", "Mã NV");
            DoiTenCot(dgvQuyetDinh, "HoTen", "Họ tên");
            DoiTenCot(dgvQuyetDinh, "MaPhongBan", "Mã phòng");
            DoiTenCot(dgvQuyetDinh, "TenPhongBan", "Phòng ban");
            DoiTenCot(dgvQuyetDinh, "NgayQuyetDinh", "Ngày QĐ");
            DoiTenCot(dgvQuyetDinh, "LyDo", "Lý do");
            DoiTenCot(dgvQuyetDinh, "SoTienThuong", "Số tiền");
            DoiTenCot(dgvQuyetDinh, "NguoiKy", "Người ký");
            DoiTenCot(dgvQuyetDinh, "TinhTrangChi", "Tình trạng");

            DinhDangNgay(dgvQuyetDinh, "NgayQuyetDinh");
            DinhDangTien(dgvQuyetDinh, "SoTienThuong");
            dgvQuyetDinh.ClearSelection();
        }

        private void NapQuyetDinhChuaChi()
        {
            FormHelper.Nap(
                cboQuyetDinh,
                service.LayQuyetDinhChuaChi(),
                "HienThi", "MaQDKT");
            DongBoQuyetDinh();
        }

        private void NapPhieuChi()
        {
            dgvPhieuChi.DataSource =
                service.LayPhieuChi();

            DoiTenCot(dgvPhieuChi, "MaPhieuChi", "Mã phiếu");
            DoiTenCot(dgvPhieuChi, "MaQDKT", "Mã QĐ");
            DoiTenCot(dgvPhieuChi, "DoiTuong", "Đối tượng");
            DoiTenCot(dgvPhieuChi, "TenDoiTuong", "Tên đối tượng");
            DoiTenCot(dgvPhieuChi, "LyDo", "Lý do");
            DoiTenCot(dgvPhieuChi, "SoTienThuong", "Tiền QĐ");
            DoiTenCot(dgvPhieuChi, "NgayChi", "Ngày chi");
            DoiTenCot(dgvPhieuChi, "SoTienChi", "Tiền chi");
            DoiTenCot(dgvPhieuChi, "HinhThucChi", "Hình thức");

            DinhDangNgay(dgvPhieuChi, "NgayChi");
            DinhDangTien(dgvPhieuChi, "SoTienThuong");
            DinhDangTien(dgvPhieuChi, "SoTienChi");

            dgvPhieuChi.ClearSelection();
        }

        // ==========================================
        // HÀM GIAO DIỆN DÙNG CHUNG
        // ==========================================

        private TableLayoutPanel TaoLayout(
            int soDong, int caoForm, int caoCongCu)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = soDong,
                Padding = new Padding(12)
            };

            table.RowStyles.Add(
                new RowStyle(SizeType.Absolute, caoForm));
            table.RowStyles.Add(
                new RowStyle(SizeType.Absolute, caoCongCu));
            table.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            return table;
        }

        private TableLayoutPanel TaoForm(int soDong)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = soDong,
                Padding = new Padding(8)
            };

            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 160));
            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 165));
            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < soDong; i++)
                table.RowStyles.Add(
                    new RowStyle(
                        SizeType.Percent, 100F / soDong));

            return table;
        }

        private void ThemTruong(
            TableLayoutPanel table, int cot, int dong,
            string nhan, Control control)
        {
            Label lbl = new Label
            {
                Text = nhan,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(5)
            };

            table.Controls.Add(lbl, cot, dong);
            table.Controls.Add(control, cot + 1, dong);
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
                DropDownStyle = ComboBoxStyle.DropDownList,
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

        private decimal GioiHanTien(
            NumericUpDown num, object value)
        {
            decimal tien = Convert.ToDecimal(value);

            return Math.Max(
                num.Minimum,
                Math.Min(num.Maximum, tien));
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

        private Button TaoNut(string noiDung, int rong)
        {
            Button btn = new Button
            {
                Text = noiDung,
                Width = rong,
                Height = 38,
                BackColor = Color.FromArgb(35, 78, 121),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(5)
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
            dgv.RowTemplate.Height = 31;

            return dgv;
        }

        private void DoiTenCot(
            DataGridView dgv, string cot, string tieuDe)
        {
            if (dgv.Columns.Contains(cot))
                dgv.Columns[cot].HeaderText = tieuDe;
        }

        private void DinhDangNgay(
            DataGridView dgv, string cot)
        {
            if (dgv.Columns.Contains(cot))
                dgv.Columns[cot]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void DinhDangTien(
            DataGridView dgv, string cot)
        {
            if (dgv.Columns.Contains(cot))
                dgv.Columns[cot]
                    .DefaultCellStyle.Format = "N0";
        }

        private string LayO(
            DataGridViewRow row, string cot)
        {
            object value = row.Cells[cot].Value;

            return value == null || value == DBNull.Value
                ? ""
                : Convert.ToString(value);
        }
    }
}
