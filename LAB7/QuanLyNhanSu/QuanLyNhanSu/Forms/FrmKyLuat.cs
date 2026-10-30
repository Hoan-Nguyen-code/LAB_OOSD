
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmKyLuat : Form
    {
        private readonly KyLuatService service =
            new KyLuatService();

        // Tab quyết định kỷ luật
        private TextBox txtMaQD, txtLyDo, txtTim;
        private ComboBox cboDoiTuong, cboNhanVien, cboPhongBan;
        private DateTimePicker dtpNgayQD;
        private NumericUpDown numTienPhat;
        private DataGridView dgvQuyetDinh;

        // Tab phiếu thu
        private TextBox txtMaPhieuThu;
        private ComboBox cboQuyetDinh, cboHinhThuc;
        private DateTimePicker dtpNgayThu;
        private NumericUpDown numTienThu;
        private DataGridView dgvPhieuThu;

        public FrmKyLuat()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) => FormHelper.Chay(() =>
            {
                NapDanhMuc();
                NapQuyetDinh();
                NapQuyetDinhChuaThu();
                NapPhieuThu();
                LamMoiQuyetDinh();
                LamMoiPhieuThu();
            });
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Kỷ luật và nộp phạt";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1220, 790);
            MinimumSize = new Size(1000, 670);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            Label title = new Label
            {
                Text = "QUẢN LÝ KỶ LUẬT VÀ NỘP PHẠT",
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

            TabPage tabQD = new TabPage("Quyết định kỷ luật");
            TabPage tabThu = new TabPage("Phiếu thu nộp phạt");

            TaoTabQuyetDinh(tabQD);
            TaoTabPhieuThu(tabThu);

            tabs.TabPages.Add(tabQD);
            tabs.TabPages.Add(tabThu);

            Controls.Add(tabs);
            Controls.Add(title);
        }

        // ==========================================
        // TAB 1 - QUYẾT ĐỊNH KỶ LUẬT
        // ==========================================

        private void TaoTabQuyetDinh(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(220, 55);

            GroupBox grp = new GroupBox
            {
                Text = "Thông tin quyết định kỷ luật",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(3);

            txtMaQD = TaoTextBox();
            txtLyDo = TaoTextBox();

            cboDoiTuong = TaoComboBox();
            cboDoiTuong.Items.AddRange(new object[]
            {
                "Cá nhân", "Phòng ban"
            });
            cboDoiTuong.SelectedIndex = 0;

            cboNhanVien = TaoComboBox();
            cboPhongBan = TaoComboBox();
            dtpNgayQD = TaoNgay();
            numTienPhat = TaoTien();

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
                "Số tiền phạt:", numTienPhat);

            // Dòng lý do riêng.
            TableLayoutPanel khung = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };

            khung.RowStyles.Add(
                new RowStyle(SizeType.Percent, 78));
            khung.RowStyles.Add(
                new RowStyle(SizeType.Percent, 22));

            FlowLayoutPanel pnlLyDo = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false
            };

            txtLyDo.Width = 780;
            txtLyDo.Dock = DockStyle.None;

            pnlLyDo.Controls.Add(TaoNhan("Lý do kỷ luật:"));
            pnlLyDo.Controls.Add(txtLyDo);

            khung.Controls.Add(form, 0, 0);
            khung.Controls.Add(pnlLyDo, 0, 1);

            grp.Controls.Add(khung);

            FlowLayoutPanel congCu = TaoThanhCongCu();

            Button btnLap = TaoNut("Lập quyết định", 155);
            Button btnMoi = TaoNut("Làm mới", 110);
            txtTim = new TextBox
            {
                Width = 230,
                Margin = new Padding(15, 9, 5, 5)
            };
            Button btnTim = TaoNut("Tìm kiếm", 110);

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

            KetQuaXuLy kq = service.LapQuyetDinh(
                txtMaQD.Text,
                cboDoiTuong.Text,
                maDoiTuong,
                dtpNgayQD.Value,
                txtLyDo.Text,
                numTienPhat.Value);

            if (FormHelper.Bao(kq))
            {
                NapQuyetDinh();
                NapQuyetDinhChuaThu();
                LamMoiQuyetDinh();
            }
        }

        private void XemQuyetDinh(int dong)
        {
            DataGridViewRow row = dgvQuyetDinh.Rows[dong];

            txtMaQD.Text = LayO(row, "MaQDKL");
            cboDoiTuong.Text = LayO(row, "DoiTuong");
            cboNhanVien.SelectedValue = LayO(row, "MaNV");
            cboPhongBan.SelectedValue = LayO(row, "MaPhongBan");

            dtpNgayQD.Value = Convert.ToDateTime(
                row.Cells["NgayQuyetDinh"].Value);

            numTienPhat.Value = GioiHanTien(
                numTienPhat, row.Cells["SoTienPhat"].Value);

            txtLyDo.Text = LayO(row, "LyDo");
        }

        private void LamMoiQuyetDinh()
        {
            txtMaQD.Clear();
            txtLyDo.Clear();
            cboDoiTuong.SelectedIndex = 0;

            if (cboNhanVien.Items.Count > 0)
                cboNhanVien.SelectedIndex = 0;

            if (cboPhongBan.Items.Count > 0)
                cboPhongBan.SelectedIndex = 0;

            dtpNgayQD.Value = DateTime.Today;
            numTienPhat.Value = 500000;

            CapNhatDoiTuong();
            dgvQuyetDinh.ClearSelection();
        }

        // ==========================================
        // TAB 2 - PHIẾU THU NỘP PHẠT
        // ==========================================

        private void TaoTabPhieuThu(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(225, 55);

            GroupBox grp = new GroupBox
            {
                Text = "Lập phiếu thu nộp phạt",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(3);

            txtMaPhieuThu = TaoTextBox();
            cboQuyetDinh = TaoComboBox();
            dtpNgayThu = TaoNgay();
            numTienThu = TaoTien();

            cboHinhThuc = TaoComboBox();
            cboHinhThuc.Items.AddRange(new object[]
            {
                "Tiền mặt", "Chuyển khoản", "Trừ lương"
            });
            cboHinhThuc.SelectedIndex = 0;

            ThemTruong(form, 0, 0,
                "Mã phiếu thu:", txtMaPhieuThu);
            ThemTruong(form, 2, 0,
                "Quyết định:", cboQuyetDinh);

            ThemTruong(form, 0, 1,
                "Ngày thu:", dtpNgayThu);
            ThemTruong(form, 2, 1,
                "Số tiền thu:", numTienThu);

            ThemTruong(form, 0, 2,
                "Hình thức thu:", cboHinhThuc);

            grp.Controls.Add(form);

            FlowLayoutPanel congCu = TaoThanhCongCu();

            Button btnLap = TaoNut("Lập phiếu thu", 155);
            Button btnTai = TaoNut("Tải danh sách", 155);
            Button btnMoi = TaoNut("Làm mới", 110);

            congCu.Controls.Add(btnLap);
            congCu.Controls.Add(btnTai);
            congCu.Controls.Add(btnMoi);

            dgvPhieuThu = TaoBang();

            layout.Controls.Add(grp, 0, 0);
            layout.Controls.Add(congCu, 0, 1);
            layout.Controls.Add(dgvPhieuThu, 0, 2);

            tab.Controls.Add(layout);

            cboQuyetDinh.SelectedIndexChanged +=
                (s, e) => DongBoQuyetDinh();

            btnLap.Click += (s, e) =>
                FormHelper.Chay(LapPhieuThu);

            btnTai.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapQuyetDinhChuaThu();
                    NapPhieuThu();
                });

            btnMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapQuyetDinhChuaThu();
                    NapPhieuThu();
                    LamMoiPhieuThu();
                });

            dgvPhieuThu.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row =
                    dgvPhieuThu.Rows[e.RowIndex];

                MessageBox.Show(
                    "Phiếu thu: " + LayO(row, "MaPhieuThu") +
                    "\nQuyết định: " + LayO(row, "MaQDKL") +
                    "\nĐối tượng: " + LayO(row, "TenDoiTuong") +
                    "\nSố tiền: " +
                    Convert.ToDecimal(
                        row.Cells["SoTienThu"].Value)
                        .ToString("N0") + " đồng" +
                    "\nHình thức: " + LayO(row, "HinhThucThu"),
                    "Thông tin phiếu thu",
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
                numTienThu.Value = 0;
                return;
            }

            numTienThu.Value = GioiHanTien(
                numTienThu, row["SoTienPhat"]);

            DateTime ngayQD =
                Convert.ToDateTime(row["NgayQuyetDinh"]);

            if (dtpNgayThu.Value.Date < ngayQD.Date)
                dtpNgayThu.Value = ngayQD;

            bool caNhan =
                Convert.ToString(row["DoiTuong"]) == "Cá nhân";

            if (!caNhan &&
                cboHinhThuc.Text == "Trừ lương")
            {
                cboHinhThuc.SelectedIndex = 0;
            }
        }

        private void LapPhieuThu()
        {
            DataRowView row =
                cboQuyetDinh.SelectedItem as DataRowView;

            if (row == null)
            {
                MessageBox.Show(
                    "Không còn quyết định nào chưa lập phiếu thu.");
                return;
            }

            KetQuaXuLy kq = service.LapPhieuThu(
                txtMaPhieuThu.Text,
                FormHelper.Gia(cboQuyetDinh),
                dtpNgayThu.Value,
                numTienThu.Value,
                cboHinhThuc.Text);

            if (FormHelper.Bao(kq))
            {
                NapQuyetDinh();
                NapQuyetDinhChuaThu();
                NapPhieuThu();
                LamMoiPhieuThu();
            }
        }

        private void LamMoiPhieuThu()
        {
            txtMaPhieuThu.Clear();
            dtpNgayThu.Value = DateTime.Today;
            cboHinhThuc.SelectedIndex = 0;
            DongBoQuyetDinh();
            dgvPhieuThu.ClearSelection();
        }

        // ==========================================
        // NẠP DỮ LIỆU
        // ==========================================

        private void NapDanhMuc()
        {
            FormHelper.Nap(
                cboNhanVien,
                service.LayNhanVien(),
                "HienThi",
                "MaNV");

            FormHelper.Nap(
                cboPhongBan,
                service.LayPhongBan(),
                "HienThi",
                "MaPhongBan");
        }

        private void NapQuyetDinh(string tuKhoa = "")
        {
            dgvQuyetDinh.DataSource =
                service.LayQuyetDinh(tuKhoa);

            DoiTenCot(dgvQuyetDinh, "MaQDKL", "Mã quyết định");
            DoiTenCot(dgvQuyetDinh, "DoiTuong", "Đối tượng");
            DoiTenCot(dgvQuyetDinh, "MaNV", "Mã NV");
            DoiTenCot(dgvQuyetDinh, "HoTen", "Họ tên");
            DoiTenCot(dgvQuyetDinh, "MaPhongBan", "Mã phòng");
            DoiTenCot(dgvQuyetDinh, "TenPhongBan", "Phòng ban");
            DoiTenCot(dgvQuyetDinh, "NgayQuyetDinh", "Ngày QĐ");
            DoiTenCot(dgvQuyetDinh, "LyDo", "Lý do");
            DoiTenCot(dgvQuyetDinh, "SoTienPhat", "Số tiền phạt");
            DoiTenCot(dgvQuyetDinh, "TinhTrangThu", "Tình trạng");

            DinhDangNgay(dgvQuyetDinh, "NgayQuyetDinh");
            DinhDangTien(dgvQuyetDinh, "SoTienPhat");

            dgvQuyetDinh.ClearSelection();
        }

        private void NapQuyetDinhChuaThu()
        {
            FormHelper.Nap(
                cboQuyetDinh,
                service.LayQuyetDinhChuaThu(),
                "HienThi",
                "MaQDKL");

            DongBoQuyetDinh();
        }

        private void NapPhieuThu()
        {
            dgvPhieuThu.DataSource = service.LayPhieuThu();

            DoiTenCot(dgvPhieuThu, "MaPhieuThu", "Mã phiếu");
            DoiTenCot(dgvPhieuThu, "MaQDKL", "Mã quyết định");
            DoiTenCot(dgvPhieuThu, "DoiTuong", "Đối tượng");
            DoiTenCot(dgvPhieuThu, "TenDoiTuong", "Tên đối tượng");
            DoiTenCot(dgvPhieuThu, "LyDo", "Lý do");
            DoiTenCot(dgvPhieuThu, "SoTienPhat", "Tiền quyết định");
            DoiTenCot(dgvPhieuThu, "NgayThu", "Ngày thu");
            DoiTenCot(dgvPhieuThu, "SoTienThu", "Số tiền thu");
            DoiTenCot(dgvPhieuThu, "HinhThucThu", "Hình thức thu");

            DinhDangNgay(dgvPhieuThu, "NgayThu");
            DinhDangTien(dgvPhieuThu, "SoTienPhat");
            DinhDangTien(dgvPhieuThu, "SoTienThu");

            dgvPhieuThu.ClearSelection();
        }

        // ==========================================
        // HÀM GIAO DIỆN DÙNG CHUNG
        // ==========================================

        private TableLayoutPanel TaoLayout(
            int caoForm, int caoCongCu)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
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
            {
                table.RowStyles.Add(
                    new RowStyle(
                        SizeType.Percent, 100F / soDong));
            }

            return table;
        }

        private void ThemTruong(
            TableLayoutPanel table,
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

        private Label TaoNhan(string noiDung)
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
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
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
