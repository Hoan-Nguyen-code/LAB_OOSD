
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmPhanCong : Form
    {
        private readonly PhanCongService service =
            new PhanCongService();

        // Tab ca làm việc
        private TextBox txtMaCa, txtTenCa;
        private DateTimePicker dtpGioBatDau, dtpGioKetThuc;
        private DataGridView dgvCa;

        // Tab kế hoạch
        private TextBox txtMaKeHoach;
        private ComboBox cboPhongBan;
        private NumericUpDown numTuan, numNam;
        private DateTimePicker dtpNgayLap;
        private DataGridView dgvKeHoach;

        // Chi tiết phân công
        private ComboBox cboKeHoach, cboNhanVien, cboCa;
        private TextBox txtMaChiTiet;
        private DateTimePicker dtpNgayLam;
        private DataGridView dgvChiTiet;

        public FrmPhanCong()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapCa();
                    NapPhongBan();
                    NapKeHoach();
                    LamMoiNhap();
                });
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Phân công lao động";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1200, 820);
            MinimumSize = new Size(1000, 700);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label lbl = new Label
            {
                Text = "QUẢN LÝ PHÂN CÔNG LAO ĐỘNG",
                Dock = DockStyle.Top,
                Height = 65,
                Font = new Font(
                    "Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 66, 105),
                TextAlign = ContentAlignment.MiddleCenter
            };

            TabControl tabs = new TabControl
            {
                Dock = DockStyle.Fill
            };

            TabPage tabCa = new TabPage("Ca làm việc");
            TabPage tabKeHoach =
                new TabPage("Kế hoạch phân công");

            TaoTabCa(tabCa);
            TaoTabKeHoach(tabKeHoach);

            tabs.TabPages.Add(tabCa);
            tabs.TabPages.Add(tabKeHoach);

            Controls.Add(tabs);
            Controls.Add(lbl);
        }

        // =========================================
        // TAB 1 - CA LÀM VIỆC
        // =========================================

        private void TaoTabCa(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(3);
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 165));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            GroupBox grp = new GroupBox
            {
                Text = "Thông tin ca làm việc",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel form = TaoForm(2);

            txtMaCa = TaoTextBox();
            txtTenCa = TaoTextBox();
            dtpGioBatDau = TaoGio();
            dtpGioKetThuc = TaoGio();

            ThemTruong(form, 0, 0, "Mã ca:", txtMaCa);
            ThemTruong(form, 2, 0, "Tên ca:", txtTenCa);
            ThemTruong(form, 0, 1, "Giờ bắt đầu:", dtpGioBatDau);
            ThemTruong(form, 2, 1, "Giờ kết thúc:", dtpGioKetThuc);

            grp.Controls.Add(form);

            FlowLayoutPanel thanhCongCu = TaoThanhCongCu();

            Button btnThem = TaoNut("Thêm ca làm việc", 160);
            Button btnLamMoi = TaoNut("Làm mới", 120);

            thanhCongCu.Controls.Add(btnThem);
            thanhCongCu.Controls.Add(btnLamMoi);

            dgvCa = TaoBang();

            layout.Controls.Add(grp, 0, 0);
            layout.Controls.Add(thanhCongCu, 0, 1);
            layout.Controls.Add(dgvCa, 0, 2);
            tab.Controls.Add(layout);

            btnThem.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.ThemCaLamViec(
                        txtMaCa.Text,
                        txtTenCa.Text,
                        dtpGioBatDau.Value.TimeOfDay,
                        dtpGioKetThuc.Value.TimeOfDay);

                    if (FormHelper.Bao(kq))
                    {
                        NapCa();
                        XoaNhapCa();
                    }
                });

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapCa();
                    XoaNhapCa();
                });

            dgvCa.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dgvCa.Rows[e.RowIndex];

                txtMaCa.Text = LayO(row, "MaCa");
                txtTenCa.Text = LayO(row, "TenCa");

                dtpGioBatDau.Value = DateTime.Today.Add(
                    (TimeSpan)row.Cells["GioBatDau"].Value);

                dtpGioKetThuc.Value = DateTime.Today.Add(
                    (TimeSpan)row.Cells["GioKetThuc"].Value);
            };
        }

        // =========================================
        // TAB 2 - KẾ HOẠCH PHÂN CÔNG
        // =========================================

        private void TaoTabKeHoach(TabPage tab)
        {
            TableLayoutPanel layout = TaoLayout(5);

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 165));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 195));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            GroupBox grpKeHoach = new GroupBox
            {
                Text = "Lập kế hoạch phân công theo tuần",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel formKH = TaoForm(2);

            txtMaKeHoach = TaoTextBox();
            cboPhongBan = TaoComboBox();

            numTuan = TaoSo(1, 53);
            numNam = TaoSo(2000, 2100);
            dtpNgayLap = TaoNgay();

            ThemTruong(formKH, 0, 0,
                "Mã kế hoạch:", txtMaKeHoach);
            ThemTruong(formKH, 2, 0,
                "Phòng ban:", cboPhongBan);
            ThemTruong(formKH, 0, 1,
                "Tuần:", numTuan);
            ThemTruong(formKH, 2, 1,
                "Năm:", numNam);

            grpKeHoach.Controls.Add(formKH);

            FlowLayoutPanel congCuKH = TaoThanhCongCu();
            Button btnLapKH = TaoNut("Lập kế hoạch", 150);
            Button btnTaiKH = TaoNut("Tải kế hoạch", 150);

            congCuKH.Controls.Add(btnLapKH);
            congCuKH.Controls.Add(btnTaiKH);

            GroupBox grpChiTiet = new GroupBox
            {
                Text = "Phân công nhân viên theo ca",
                Dock = DockStyle.Fill
            };

            TableLayoutPanel formCT = TaoForm(3);

            cboKeHoach = TaoComboBox();
            cboNhanVien = TaoComboBox();
            cboCa = TaoComboBox();
            txtMaChiTiet = TaoTextBox();
            dtpNgayLam = TaoNgay();

            ThemTruong(formCT, 0, 0,
                "Kế hoạch:", cboKeHoach);
            ThemTruong(formCT, 2, 0,
                "Mã chi tiết:", txtMaChiTiet);
            ThemTruong(formCT, 0, 1,
                "Nhân viên:", cboNhanVien);
            ThemTruong(formCT, 2, 1,
                "Ca làm việc:", cboCa);
            ThemTruong(formCT, 0, 2,
                "Ngày làm việc:", dtpNgayLam);

            grpChiTiet.Controls.Add(formCT);

            FlowLayoutPanel congCuCT = TaoThanhCongCu();
            Button btnPhanCong = TaoNut("Phân công", 145);
            Button btnGo = TaoNut("Gỡ phân công", 150);
            Button btnLamMoi = TaoNut("Làm mới", 115);

            congCuCT.Controls.Add(btnPhanCong);
            congCuCT.Controls.Add(btnGo);
            congCuCT.Controls.Add(btnLamMoi);

            dgvChiTiet = TaoBang();

            layout.Controls.Add(grpKeHoach, 0, 0);
            layout.Controls.Add(congCuKH, 0, 1);
            layout.Controls.Add(grpChiTiet, 0, 2);
            layout.Controls.Add(congCuCT, 0, 3);
            layout.Controls.Add(dgvChiTiet, 0, 4);

            tab.Controls.Add(layout);

            // Khi chọn kế hoạch, tải nhân viên
            // thuộc phòng ban và các phân công đã có.
            cboKeHoach.SelectedIndexChanged += (s, e) =>
                FormHelper.Chay(NapTheoKeHoach);

            btnLapKH.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.LapKeHoach(
                        txtMaKeHoach.Text,
                        FormHelper.Gia(cboPhongBan),
                        (int)numTuan.Value,
                        (int)numNam.Value,
                        dtpNgayLap.Value);

                    if (FormHelper.Bao(kq))
                    {
                        string maMoi = txtMaKeHoach.Text.Trim();

                        NapKeHoach();
                        cboKeHoach.SelectedValue = maMoi;
                        txtMaKeHoach.Clear();
                    }
                });

            btnTaiKH.Click += (s, e) =>
                FormHelper.Chay(NapKeHoach);

            btnPhanCong.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    var kq = service.ThemChiTiet(
                        txtMaChiTiet.Text,
                        FormHelper.Gia(cboKeHoach),
                        FormHelper.Gia(cboNhanVien),
                        FormHelper.Gia(cboCa),
                        dtpNgayLam.Value);

                    if (FormHelper.Bao(kq))
                    {
                        NapChiTiet();
                        txtMaChiTiet.Clear();
                    }
                });

            btnGo.Click += (s, e) =>
                FormHelper.Chay(GoPhanCong);

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapKeHoach();
                    txtMaKeHoach.Clear();
                    txtMaChiTiet.Clear();
                });

            dgvChiTiet.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row =
                    dgvChiTiet.Rows[e.RowIndex];

                txtMaChiTiet.Text =
                    LayO(row, "MaChiTietPhanCong");

                cboNhanVien.SelectedValue =
                    LayO(row, "MaNV");

                cboCa.SelectedValue = LayO(row, "MaCa");

                dtpNgayLam.Value = Convert.ToDateTime(
                    row.Cells["NgayLamViec"].Value);
            };
        }

        // =========================================
        // NẠP DỮ LIỆU
        // =========================================

        private void NapCa()
        {
            dgvCa.DataSource = service.LayCaLamViec();

            DoiTenCot(dgvCa, "MaCa", "Mã ca");
            DoiTenCot(dgvCa, "TenCa", "Tên ca");
            DoiTenCot(dgvCa, "GioBatDau", "Bắt đầu");
            DoiTenCot(dgvCa, "GioKetThuc", "Kết thúc");

            if (dgvCa.Columns.Contains("HienThi"))
                dgvCa.Columns["HienThi"].Visible = false;

            FormHelper.Nap(
                cboCa,
                service.LayCaLamViec(),
                "HienThi", "MaCa");

            dgvCa.ClearSelection();
        }

        private void NapPhongBan()
        {
            FormHelper.Nap(
                cboPhongBan,
                service.LayPhongBan(),
                "HienThi", "MaPhongBan");
        }

        private void NapKeHoach()
        {
            DataTable dt = service.LayKeHoach();

            // Gắn lại DataSource sẽ tự kích hoạt
            // SelectedIndexChanged.
            FormHelper.Nap(
                cboKeHoach, dt,
                "HienThi", "MaKeHoach");

            NapTheoKeHoach();
        }

        private void NapTheoKeHoach()
        {
            if (cboKeHoach == null ||
                cboNhanVien == null ||
                dgvChiTiet == null)
                return;

            DataRowView row =
                cboKeHoach.SelectedItem as DataRowView;

            if (row == null)
            {
                cboNhanVien.DataSource = null;
                dgvChiTiet.DataSource = null;
                return;
            }

            string maPhongBan =
                Convert.ToString(row["MaPhongBan"]);

            FormHelper.Nap(
                cboNhanVien,
                service.LayNhanVienTheoPhong(maPhongBan),
                "HienThi", "MaNV");

            NapChiTiet();
        }

        private void NapChiTiet()
        {
            if (cboKeHoach == null ||
                dgvChiTiet == null)
                return;

            string maKeHoach = FormHelper.Gia(cboKeHoach);

            if (string.IsNullOrEmpty(maKeHoach))
            {
                dgvChiTiet.DataSource = null;
                return;
            }

            dgvChiTiet.DataSource =
                service.LayChiTiet(maKeHoach);

            DoiTenCot(dgvChiTiet,
                "MaChiTietPhanCong", "Mã chi tiết");
            DoiTenCot(dgvChiTiet,
                "MaKeHoach", "Mã kế hoạch");
            DoiTenCot(dgvChiTiet,
                "MaNV", "Mã NV");
            DoiTenCot(dgvChiTiet,
                "HoTen", "Họ tên");
            DoiTenCot(dgvChiTiet,
                "MaCa", "Mã ca");
            DoiTenCot(dgvChiTiet,
                "TenCa", "Ca làm việc");
            DoiTenCot(dgvChiTiet,
                "NgayLamViec", "Ngày làm việc");
            DoiTenCot(dgvChiTiet,
                "GioBatDau", "Giờ bắt đầu");
            DoiTenCot(dgvChiTiet,
                "GioKetThuc", "Giờ kết thúc");

            if (dgvChiTiet.Columns.Contains("NgayLamViec"))
                dgvChiTiet.Columns["NgayLamViec"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvChiTiet.ClearSelection();
        }

        private void GoPhanCong()
        {
            if (dgvChiTiet.CurrentRow == null ||
                dgvChiTiet.CurrentRow.IsNewRow ||
                dgvChiTiet.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Hãy chọn dòng phân công cần gỡ.");
                return;
            }

            string ma = LayO(
                dgvChiTiet.CurrentRow,
                "MaChiTietPhanCong");

            if (MessageBox.Show(
                "Gỡ phân công " + ma + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            var kq = service.XoaChiTiet(ma);

            if (FormHelper.Bao(kq))
            {
                NapChiTiet();
                txtMaChiTiet.Clear();
            }
        }

        private void LamMoiNhap()
        {
            XoaNhapCa();
            txtMaKeHoach.Clear();
            txtMaChiTiet.Clear();

            int tuan, nam;
            PhanCongService.LayTuanISO(
                DateTime.Today, out tuan, out nam);

            numTuan.Value = tuan;
            numNam.Value = nam;

            dtpNgayLap.Value = DateTime.Today;
            dtpNgayLam.Value = DateTime.Today;

            if (cboPhongBan.Items.Count > 0)
                cboPhongBan.SelectedIndex = 0;

            NapTheoKeHoach();
        }

        private void XoaNhapCa()
        {
            txtMaCa.Clear();
            txtTenCa.Clear();

            dtpGioBatDau.Value =
                DateTime.Today.AddHours(7);

            dtpGioKetThuc.Value =
                DateTime.Today.AddHours(11);

            dgvCa.ClearSelection();
        }

        // =========================================
        // CÁC HÀM GIAO DIỆN CHUNG
        // =========================================

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
                new ColumnStyle(SizeType.Absolute, 160));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 160));
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

        private DateTimePicker TaoGio()
        {
            return new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "HH:mm",
                ShowUpDown = true,
                Margin = new Padding(5, 8, 12, 5)
            };
        }

        private NumericUpDown TaoSo(int min, int max)
        {
            return new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = min,
                Maximum = max,
                Margin = new Padding(5, 8, 12, 5)
            };
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
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(35, 78, 121),
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
            dgv.RowTemplate.Height = 30;

            return dgv;
        }

        private void DoiTenCot(
            DataGridView dgv, string cot, string tieuDe)
        {
            if (dgv.Columns.Contains(cot))
                dgv.Columns[cot].HeaderText = tieuDe;
        }

        private string LayO(
            DataGridViewRow row, string tenCot)
        {
            object o = row.Cells[tenCot].Value;

            return o == null || o == DBNull.Value
                ? ""
                : Convert.ToString(o);
        }
    }
}
