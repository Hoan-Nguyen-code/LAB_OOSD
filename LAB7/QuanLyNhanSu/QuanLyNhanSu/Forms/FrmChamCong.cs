
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmChamCong : Form
    {
        private readonly ChamCongService service =
            new ChamCongService();

        // Tab chấm công
        private TextBox txtMaBang, txtMaCTCC;
        private ComboBox cboBang, cboNVCC, cboTrangThai;
        private NumericUpDown numThang, numNam, numGioLam;
        private DateTimePicker dtpNgayLam;
        private DataGridView dgvChamCong;

        // Tab phụ trội
        private TextBox txtMaPhieu, txtMaCTPT;
        private ComboBox cboPhieu, cboNVPT, cboHinhThuc;
        private DateTimePicker dtpNgayPhuTroi;
        private NumericUpDown numGioPhieu, numGioThamGia;
        private DataGridView dgvPhieu, dgvPhuTroi;

        public FrmChamCong()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapNhanVien();
                    NapBangChamCong();
                    NapPhieuPhuTroi();
                    LamMoi();
                });
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Chấm công và phụ trội";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1250, 820);
            MinimumSize = new Size(1020, 690);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label title = new Label
            {
                Text = "QUẢN LÝ CHẤM CÔNG VÀ GIỜ PHỤ TRỘI",
                Dock = DockStyle.Top,
                Height = 62,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 66, 105)
            };

            TabControl tabs = new TabControl
            {
                Dock = DockStyle.Fill
            };

            TabPage tabCC = new TabPage("Chấm công tháng");
            TabPage tabPT = new TabPage("Phiếu phụ trội");

            TaoTabChamCong(tabCC);
            TaoTabPhuTroi(tabPT);

            tabs.TabPages.Add(tabCC);
            tabs.TabPages.Add(tabPT);

            Controls.Add(tabs);
            Controls.Add(title);
        }

        // ==========================================
        // TAB 1: CHẤM CÔNG
        // ==========================================

        private void TaoTabChamCong(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(
                105, 55, 170, 55);

            // Lập bảng tháng
            GroupBox grpBang = new GroupBox
            {
                Text = "Lập bảng chấm công tháng",
                Dock = DockStyle.Fill
            };

            FlowLayoutPanel pnlBang = TaoThanhCongCu();

            txtMaBang = new TextBox { Width = 145 };
            numThang = TaoSo(1, 12);
            numNam = TaoSo(2000, 2100);
            numThang.Width = 75;
            numNam.Width = 95;

            Button btnLapBang = TaoNut("Lập bảng", 125);

            pnlBang.Controls.Add(TaoNhan("Mã bảng:"));
            pnlBang.Controls.Add(txtMaBang);
            pnlBang.Controls.Add(TaoNhan("Tháng:"));
            pnlBang.Controls.Add(numThang);
            pnlBang.Controls.Add(TaoNhan("Năm:"));
            pnlBang.Controls.Add(numNam);
            pnlBang.Controls.Add(btnLapBang);

            grpBang.Controls.Add(pnlBang);

            // Thanh lựa chọn bảng
            FlowLayoutPanel pnlChonBang = TaoThanhCongCu();

            cboBang = TaoComboBox();
            cboBang.Width = 400;

            Button btnTaiBang = TaoNut("Tải dữ liệu", 130);

            pnlChonBang.Controls.Add(TaoNhan("Bảng chấm công:"));
            pnlChonBang.Controls.Add(cboBang);
            pnlChonBang.Controls.Add(btnTaiBang);

            // Chi tiết chấm công
            GroupBox grpChiTiet = new GroupBox
            {
                Text = "Ghi nhận chấm công nhân viên",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(3);

            txtMaCTCC = TaoTextBox();
            cboNVCC = TaoComboBox();
            cboTrangThai = TaoComboBox();

            cboTrangThai.Items.AddRange(new object[]
            {
                "Có mặt", "Vắng mặt", "Nghỉ phép"
            });
            cboTrangThai.SelectedIndex = 0;

            dtpNgayLam = TaoNgay();

            numGioLam = TaoTien(24);
            numGioLam.DecimalPlaces = 2;
            numGioLam.Increment = 0.5M;

            ThemTruong(form, 0, 0,
                "Mã chi tiết:", txtMaCTCC);
            ThemTruong(form, 2, 0,
                "Nhân viên:", cboNVCC);

            ThemTruong(form, 0, 1,
                "Ngày làm:", dtpNgayLam);
            ThemTruong(form, 2, 1,
                "Trạng thái:", cboTrangThai);

            ThemTruong(form, 0, 2,
                "Số giờ làm:", numGioLam);

            grpChiTiet.Controls.Add(form);

            FlowLayoutPanel pnlCongCu = TaoThanhCongCu();

            Button btnCham = TaoNut("Ghi chấm công", 160);
            Button btnLamMoi = TaoNut("Làm mới", 120);

            pnlCongCu.Controls.Add(btnCham);
            pnlCongCu.Controls.Add(btnLamMoi);

            dgvChamCong = TaoBang();

            layout.Controls.Add(grpBang, 0, 0);
            layout.Controls.Add(pnlChonBang, 0, 1);
            layout.Controls.Add(grpChiTiet, 0, 2);
            layout.Controls.Add(pnlCongCu, 0, 3);
            layout.Controls.Add(dgvChamCong, 0, 4);

            tab.Controls.Add(layout);

            // Sự kiện
            btnLapBang.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.LapBangChamCong(
                        txtMaBang.Text,
                        (int)numThang.Value,
                        (int)numNam.Value);

                    if (FormHelper.Bao(kq))
                    {
                        string ma = txtMaBang.Text.Trim();
                        NapBangChamCong();
                        cboBang.SelectedValue = ma;
                        txtMaBang.Clear();
                    }
                });

            cboBang.SelectedIndexChanged += (s, e) =>
                FormHelper.Chay(NapChiTietChamCong);

            btnTaiBang.Click += (s, e) =>
                FormHelper.Chay(NapBangChamCong);

            cboTrangThai.SelectedIndexChanged += (s, e) =>
            {
                if (numGioLam != null)
                {
                    bool coMat = cboTrangThai.Text == "Có mặt";
                    numGioLam.Enabled = coMat;
                    numGioLam.Value = coMat ? 8 : 0;
                }
            };

            btnCham.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.ChamCong(
                        txtMaCTCC.Text,
                        FormHelper.Gia(cboBang),
                        FormHelper.Gia(cboNVCC),
                        dtpNgayLam.Value,
                        numGioLam.Value,
                        cboTrangThai.Text);

                    if (FormHelper.Bao(kq))
                    {
                        NapChiTietChamCong();
                        txtMaCTCC.Clear();
                    }
                });

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapBangChamCong();
                    txtMaCTCC.Clear();
                    numGioLam.Value = 8;
                    cboTrangThai.SelectedIndex = 0;
                });
        }

        // ==========================================
        // TAB 2: PHIẾU PHỤ TRỘI
        // ==========================================

        private void TaoTabPhuTroi(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(
                125, 130, 55, 135, 55);

            GroupBox grpPhieu = new GroupBox
            {
                Text = "Lập phiếu phụ trội",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel formPhieu = TaoForm(2);

            txtMaPhieu = TaoTextBox();
            dtpNgayPhuTroi = TaoNgay();
            cboHinhThuc = TaoComboBox();

            cboHinhThuc.Items.AddRange(new object[]
            {
                "Tăng ca", "Ngày lễ", "Ngày nghỉ"
            });
            cboHinhThuc.SelectedIndex = 0;

            numGioPhieu = TaoTien(24);
            numGioPhieu.DecimalPlaces = 2;
            numGioPhieu.Increment = 0.5M;
            numGioPhieu.Value = 2;

            ThemTruong(formPhieu, 0, 0,
                "Mã phiếu:", txtMaPhieu);
            ThemTruong(formPhieu, 2, 0,
                "Ngày phụ trội:", dtpNgayPhuTroi);

            ThemTruong(formPhieu, 0, 1,
                "Hình thức:", cboHinhThuc);
            ThemTruong(formPhieu, 2, 1,
                "Số giờ trên phiếu:", numGioPhieu);

            grpPhieu.Controls.Add(formPhieu);

            GroupBox grpDanhSach = new GroupBox
            {
                Text = "Danh sách phiếu phụ trội",
                Dock = DockStyle.Fill
            };

            dgvPhieu = TaoBang();
            grpDanhSach.Controls.Add(dgvPhieu);

            FlowLayoutPanel pnlChon = TaoThanhCongCu();

            cboPhieu = TaoComboBox();
            cboPhieu.Width = 315;

            Button btnLapPhieu = TaoNut("Lập phiếu", 130);
            Button btnTaiPhieu = TaoNut("Tải phiếu", 120);

            pnlChon.Controls.Add(TaoNhan("Chọn phiếu:"));
            pnlChon.Controls.Add(cboPhieu);
            pnlChon.Controls.Add(btnLapPhieu);
            pnlChon.Controls.Add(btnTaiPhieu);

            GroupBox grpCT = new GroupBox
            {
                Text = "Ghi nhận nhân viên tham gia phụ trội",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel formCT = TaoForm(2);

            txtMaCTPT = TaoTextBox();
            cboNVPT = TaoComboBox();

            numGioThamGia = TaoTien(24);
            numGioThamGia.DecimalPlaces = 2;
            numGioThamGia.Increment = 0.5M;
            numGioThamGia.Value = 2;

            ThemTruong(formCT, 0, 0,
                "Mã chi tiết:", txtMaCTPT);
            ThemTruong(formCT, 2, 0,
                "Nhân viên:", cboNVPT);

            ThemTruong(formCT, 0, 1,
                "Số giờ tham gia:", numGioThamGia);

            grpCT.Controls.Add(formCT);

            FlowLayoutPanel pnlCT = TaoThanhCongCu();

            Button btnThemNV = TaoNut("Thêm nhân viên", 150);
            Button btnLamMoi = TaoNut("Làm mới", 120);

            pnlCT.Controls.Add(btnThemNV);
            pnlCT.Controls.Add(btnLamMoi);

            // Phần cuối có bảng chi tiết và thanh chọn phiếu.
            // Để đủ chỗ, dùng panel bố trí dọc.
            TableLayoutPanel duoi = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };

            duoi.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 45));
            duoi.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            dgvPhuTroi = TaoBang();

            duoi.Controls.Add(pnlCT, 0, 0);
            duoi.Controls.Add(dgvPhuTroi, 0, 1);

            // Dùng một layout riêng cho tab phụ trội.
            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 6,
                Padding = new Padding(12)
            };

            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 125));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 105));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 135));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            main.Controls.Add(grpPhieu, 0, 0);
            main.Controls.Add(pnlChon, 0, 1);
            main.Controls.Add(grpDanhSach, 0, 2);
            main.Controls.Add(grpCT, 0, 3);
            main.Controls.Add(pnlCT, 0, 4);
            main.Controls.Add(dgvPhuTroi, 0, 5);

            tab.Controls.Add(main);

            btnLapPhieu.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.LapPhieuPhuTroi(
                        txtMaPhieu.Text,
                        dtpNgayPhuTroi.Value,
                        cboHinhThuc.Text,
                        numGioPhieu.Value);

                    if (FormHelper.Bao(kq))
                    {
                        string ma = txtMaPhieu.Text.Trim();
                        NapPhieuPhuTroi();
                        cboPhieu.SelectedValue = ma;
                        txtMaPhieu.Clear();
                    }
                });

            cboPhieu.SelectedIndexChanged += (s, e) =>
                FormHelper.Chay(NapChiTietPhuTroi);

            btnTaiPhieu.Click += (s, e) =>
                FormHelper.Chay(NapPhieuPhuTroi);

            dgvPhieu.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    cboPhieu.SelectedValue =
                        Convert.ToString(
                            dgvPhieu.Rows[e.RowIndex]
                                .Cells["MaPhieuPhuTroi"].Value);
                }
            };

            btnThemNV.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.ThemNhanVienPhuTroi(
                        txtMaCTPT.Text,
                        FormHelper.Gia(cboPhieu),
                        FormHelper.Gia(cboNVPT),
                        numGioThamGia.Value);

                    if (FormHelper.Bao(kq))
                    {
                        NapChiTietPhuTroi();
                        txtMaCTPT.Clear();
                    }
                });

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapPhieuPhuTroi();
                    txtMaPhieu.Clear();
                    txtMaCTPT.Clear();
                });
        }

        // ==========================================
        // NẠP DỮ LIỆU
        // ==========================================

        private void NapNhanVien()
        {
            FormHelper.Nap(
                cboNVCC, service.LayNhanVien(),
                "HienThi", "MaNV");

            FormHelper.Nap(
                cboNVPT, service.LayNhanVien(),
                "HienThi", "MaNV");
        }

        private void NapBangChamCong()
        {
            FormHelper.Nap(
                cboBang, service.LayBangChamCong(),
                "HienThi", "MaBangChamCong");

            NapChiTietChamCong();
        }

        private void NapChiTietChamCong()
        {
            if (cboBang == null || dgvChamCong == null)
                return;

            string ma = FormHelper.Gia(cboBang);

            dgvChamCong.DataSource =
                string.IsNullOrWhiteSpace(ma)
                    ? new DataTable()
                    : service.LayChiTietChamCong(ma);

            DoiTenCot(dgvChamCong,
                "MaChiTietChamCong", "Mã chi tiết");
            DoiTenCot(dgvChamCong,
                "MaBangChamCong", "Bảng chấm công");
            DoiTenCot(dgvChamCong,
                "MaNV", "Mã nhân viên");
            DoiTenCot(dgvChamCong,
                "HoTen", "Họ và tên");
            DoiTenCot(dgvChamCong,
                "NgayLam", "Ngày làm");
            DoiTenCot(dgvChamCong,
                "SoGioLam", "Số giờ");
            DoiTenCot(dgvChamCong,
                "TrangThai", "Trạng thái");

            if (dgvChamCong.Columns.Contains("NgayLam"))
                dgvChamCong.Columns["NgayLam"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvChamCong.ClearSelection();
        }

        private void NapPhieuPhuTroi()
        {
            DataTable dt = service.LayPhieuPhuTroi();

            dgvPhieu.DataSource = dt;

            if (dgvPhieu.Columns.Contains("HienThi"))
                dgvPhieu.Columns["HienThi"].Visible = false;

            DoiTenCot(dgvPhieu,
                "MaPhieuPhuTroi", "Mã phiếu");
            DoiTenCot(dgvPhieu,
                "NgayPhuTroi", "Ngày phụ trội");
            DoiTenCot(dgvPhieu,
                "HinhThucPhuTroi", "Hình thức");
            DoiTenCot(dgvPhieu,
                "SoGioPhuTroi", "Số giờ");

            if (dgvPhieu.Columns.Contains("NgayPhuTroi"))
                dgvPhieu.Columns["NgayPhuTroi"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

            FormHelper.Nap(
                cboPhieu, service.LayPhieuPhuTroi(),
                "HienThi", "MaPhieuPhuTroi");

            NapChiTietPhuTroi();
            dgvPhieu.ClearSelection();
        }

        private void NapChiTietPhuTroi()
        {
            if (cboPhieu == null || dgvPhuTroi == null)
                return;

            string ma = FormHelper.Gia(cboPhieu);

            dgvPhuTroi.DataSource =
                string.IsNullOrWhiteSpace(ma)
                    ? new DataTable()
                    : service.LayChiTietPhuTroi(ma);

            DoiTenCot(dgvPhuTroi,
                "MaChiTietPhuTroi", "Mã chi tiết");
            DoiTenCot(dgvPhuTroi,
                "MaPhieuPhuTroi", "Mã phiếu");
            DoiTenCot(dgvPhuTroi,
                "MaNV", "Mã nhân viên");
            DoiTenCot(dgvPhuTroi,
                "HoTen", "Họ và tên");
            DoiTenCot(dgvPhuTroi,
                "SoGioThamGia", "Số giờ tham gia");

            dgvPhuTroi.ClearSelection();
        }

        private void LamMoi()
        {
            txtMaBang.Clear();
            txtMaCTCC.Clear();
            txtMaPhieu.Clear();
            txtMaCTPT.Clear();

            numThang.Value = DateTime.Today.Month;
            numNam.Value = DateTime.Today.Year;

            dtpNgayLam.Value = DateTime.Today;
            dtpNgayPhuTroi.Value = DateTime.Today;

            numGioLam.Value = 8;
            numGioPhieu.Value = 2;
            numGioThamGia.Value = 2;

            cboTrangThai.SelectedIndex = 0;
            cboHinhThuc.SelectedIndex = 0;
        }

        // ==========================================
        // HÀM TẠO GIAO DIỆN
        // ==========================================

        private TableLayoutPanel TaoLayout(params int[] doCao)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = doCao.Length + 1,
                Padding = new Padding(12)
            };

            foreach (int cao in doCao)
                table.RowStyles.Add(
                    new RowStyle(SizeType.Absolute, cao));

            table.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            return table;
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
                new ColumnStyle(SizeType.Absolute, 165));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 175));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < soDong; i++)
                form.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 100F / soDong));

            return form;
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
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
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

        private NumericUpDown TaoSo(int min, int max)
        {
            return new NumericUpDown
            {
                Minimum = min,
                Maximum = max,
                Margin = new Padding(5, 8, 12, 5)
            };
        }

        private NumericUpDown TaoTien(int max)
        {
            return new NumericUpDown
            {
                Minimum = 0,
                Maximum = max,
                DecimalPlaces = 0,
                Margin = new Padding(5, 8, 12, 5),
                Dock = DockStyle.Fill
            };
        }

        private Button TaoNut(string text, int width)
        {
            Button btn = new Button
            {
                Text = text,
                Width = width,
                Height = 38,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(35, 78, 121),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(5)
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Label TaoNhan(string text)
        {
            return new Label
            {
                Text = text,
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
            dgv.RowTemplate.Height = 30;

            return dgv;
        }

        private void DoiTenCot(
            DataGridView dgv, string cot, string tieuDe)
        {
            if (dgv.Columns.Contains(cot))
                dgv.Columns[cot].HeaderText = tieuDe;
        }
    }
}
