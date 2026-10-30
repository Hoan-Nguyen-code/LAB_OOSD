
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmTuyenDung : Form
    {
        private readonly TuyenDungService service =
            new TuyenDungService();

        private TextBox txtMaQD;
        private TextBox txtDieuKhoan;
        private TextBox txtTimKiem;

        private ComboBox cboNhanVien;
        private ComboBox cboPhongBan;

        private DateTimePicker dtpNgayQD;
        private DateTimePicker dtpBatDau;
        private DateTimePicker dtpKetThuc;

        private NumericUpDown numLuong;
        private DataGridView dgvQuyetDinh;

        private Button btnLap;
        private Button btnCapNhat;
        private Button btnLamMoi;
        private Button btnTim;

        public FrmTuyenDung()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    NapDanhMuc();
                    NapDanhSach();
                    LamMoiNhap();
                });
            };
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Tuyển dụng và thử việc";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1200, 780);
            MinimumSize = new Size(1000, 680);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ TUYỂN DỤNG VÀ THỬ VIỆC",
                Dock = DockStyle.Top,
                Height = 65,
                Font = new Font(
                    "Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 66, 105),
                TextAlign = ContentAlignment.MiddleCenter
            };

            TableLayoutPanel layout =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    Padding = new Padding(15)
                };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 300));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 58));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            GroupBox grpThongTin = new GroupBox
            {
                Text = "Thông tin quyết định tuyển dụng",
                Dock = DockStyle.Fill,
                Padding = new Padding(12)
            };

            TableLayoutPanel form =
                new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 4,
                    RowCount = 5
                };

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 170));

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 170));

            form.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 5; i++)
            {
                form.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 20));
            }

            txtMaQD = TaoTextBox();

            cboNhanVien = TaoComboBox();
            cboPhongBan = TaoComboBox();

            dtpNgayQD = TaoNgay();
            dtpBatDau = TaoNgay();
            dtpKetThuc = TaoNgay();

            numLuong = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                DecimalPlaces = 0,
                Minimum = 0,
                Maximum = 100000000000,
                Increment = 100000,
                ThousandsSeparator = true,
                Margin = new Padding(5, 8, 12, 5)
            };

            txtDieuKhoan = TaoTextBox();
            txtDieuKhoan.Multiline = true;
            txtDieuKhoan.ScrollBars = ScrollBars.Vertical;

            ThemTruong(form, 0, 0,
                "Mã quyết định:", txtMaQD);

            ThemTruong(form, 2, 0,
                "Nhân viên thử việc:", cboNhanVien);

            ThemTruong(form, 0, 1,
                "Phòng ban:", cboPhongBan);

            ThemTruong(form, 2, 1,
                "Ngày quyết định:", dtpNgayQD);

            ThemTruong(form, 0, 2,
                "Bắt đầu thử việc:", dtpBatDau);

            ThemTruong(form, 2, 2,
                "Kết thúc thử việc:", dtpKetThuc);

            ThemTruong(form, 0, 3,
                "Lương thử việc:", numLuong);

            Label lblDieuKhoan = new Label
            {
                Text = "Điều khoản:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(5)
            };

            form.Controls.Add(lblDieuKhoan, 0, 4);
            form.Controls.Add(txtDieuKhoan, 1, 4);
            form.SetColumnSpan(txtDieuKhoan, 3);

            grpThongTin.Controls.Add(form);

            // Thanh công cụ
            FlowLayoutPanel thanhCongCu =
                new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection =
                        FlowDirection.LeftToRight,
                    WrapContents = false,
                    AutoScroll = true
                };

            btnLap = TaoNut("Lập quyết định", 145);
            btnCapNhat = TaoNut("Cập nhật", 120);
            btnLamMoi = TaoNut("Làm mới", 110);

            txtTimKiem = new TextBox
            {
                Width = 230,
                Margin = new Padding(15, 9, 5, 5)
            };

            btnTim = TaoNut("Tìm kiếm", 110);

            thanhCongCu.Controls.Add(btnLap);
            thanhCongCu.Controls.Add(btnCapNhat);
            thanhCongCu.Controls.Add(btnLamMoi);
            thanhCongCu.Controls.Add(txtTimKiem);
            thanhCongCu.Controls.Add(btnTim);

            // Bảng dữ liệu
            dgvQuyetDinh = new DataGridView
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
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };

            dgvQuyetDinh.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 78, 121);

            dgvQuyetDinh.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvQuyetDinh.ColumnHeadersHeight = 40;
            dgvQuyetDinh.RowTemplate.Height = 32;

            dgvQuyetDinh.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    ChonQuyetDinh();
            };

            // Sự kiện
            btnLap.Click += (s, e) =>
                FormHelper.Chay(LapQuyetDinh);

            btnCapNhat.Click += (s, e) =>
                FormHelper.Chay(CapNhatQuyetDinh);

            btnLamMoi.Click += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    txtTimKiem.Clear();
                    NapDanhMuc();
                    NapDanhSach();
                    LamMoiNhap();
                });
            };

            btnTim.Click += (s, e) =>
                FormHelper.Chay(
                    () => NapDanhSach(txtTimKiem.Text));

            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTim.PerformClick();
                    e.SuppressKeyPress = true;
                }
            };

            layout.Controls.Add(grpThongTin, 0, 0);
            layout.Controls.Add(thanhCongCu, 0, 1);
            layout.Controls.Add(dgvQuyetDinh, 0, 2);

            Controls.Add(layout);
            Controls.Add(lblTieuDe);
        }

        private void NapDanhMuc()
        {
            DataTable nhanVien = service.LayNhanVien();

            FormHelper.Nap(
                cboNhanVien,
                nhanVien,
                "HienThi",
                "MaNV");

            DataTable phongBan = service.LayPhongBan();

            FormHelper.Nap(
                cboPhongBan,
                phongBan,
                "HienThi",
                "MaPhongBan");
        }

        private void NapDanhSach(string tuKhoa = "")
        {
            dgvQuyetDinh.DataSource =
                service.LayDanhSach(tuKhoa);

            DoiTenCot("MaQDTD", "Mã quyết định");
            DoiTenCot("MaNV", "Mã NV");
            DoiTenCot("HoTen", "Họ và tên");
            DoiTenCot("MaPhongBan", "Mã phòng");
            DoiTenCot("TenPhongBan", "Phòng ban");
            DoiTenCot("NgayQuyetDinh", "Ngày QĐ");
            DoiTenCot("NgayBatDauThuViec", "Bắt đầu");
            DoiTenCot("NgayKetThucThuViec", "Kết thúc");
            DoiTenCot("LuongThuViec", "Lương thử việc");
            DoiTenCot("NoiDungDieuKhoan", "Điều khoản");
            DoiTenCot("TinhTrang", "Tình trạng");

            if (dgvQuyetDinh.Columns.Contains("NgayQuyetDinh"))
            {
                dgvQuyetDinh.Columns["NgayQuyetDinh"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvQuyetDinh.Columns["NgayBatDauThuViec"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvQuyetDinh.Columns["NgayKetThucThuViec"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvQuyetDinh.Columns.Contains("LuongThuViec"))
            {
                dgvQuyetDinh.Columns["LuongThuViec"]
                    .DefaultCellStyle.Format = "N0";
            }

            dgvQuyetDinh.ClearSelection();
        }

        private void LapQuyetDinh()
        {
            var ketQua = service.LapQuyetDinh(
                txtMaQD.Text,
                FormHelper.Gia(cboNhanVien),
                FormHelper.Gia(cboPhongBan),
                dtpNgayQD.Value,
                dtpBatDau.Value,
                dtpKetThuc.Value,
                numLuong.Value,
                txtDieuKhoan.Text);

            if (FormHelper.Bao(ketQua))
            {
                NapDanhSach();
                LamMoiNhap();
            }
        }

        private void CapNhatQuyetDinh()
        {
            var ketQua = service.CapNhatQuyetDinh(
                txtMaQD.Text,
                FormHelper.Gia(cboNhanVien),
                FormHelper.Gia(cboPhongBan),
                dtpNgayQD.Value,
                dtpBatDau.Value,
                dtpKetThuc.Value,
                numLuong.Value,
                txtDieuKhoan.Text);

            if (FormHelper.Bao(ketQua))
            {
                NapDanhSach();
                LamMoiNhap();
            }
        }

        private void ChonQuyetDinh()
        {
            if (dgvQuyetDinh.CurrentRow == null ||
                dgvQuyetDinh.CurrentRow.IsNewRow)
                return;

            DataGridViewRow row =
                dgvQuyetDinh.CurrentRow;

            txtMaQD.Text = LayO(row, "MaQDTD");

            string maNV = LayO(row, "MaNV");
            string maPhongBan = LayO(row, "MaPhongBan");

            // Danh sách nhân viên để lập mới chỉ gồm
            // người đang thử việc. Khi xem quyết định cũ,
            // nếu nhân viên đã chính thức vẫn hiển thị được.
            bool coNhanVien = false;

            foreach (DataRowView item in cboNhanVien.Items)
            {
                if (Convert.ToString(item["MaNV"]) == maNV)
                {
                    coNhanVien = true;
                    break;
                }
            }

            if (!coNhanVien)
            {
                DataTable dt = service.LayNhanVien();

                DataTable tatCa = QuanLyNhanSu.Data.Db.Query(@"
                    SELECT MaNV,
                           MaNV + N' - ' + HoTen AS HienThi
                    FROM NhanVien
                    ORDER BY HoTen");

                FormHelper.Nap(
                    cboNhanVien, tatCa,
                    "HienThi", "MaNV");
            }

            cboNhanVien.SelectedValue = maNV;
            cboPhongBan.SelectedValue = maPhongBan;

            dtpNgayQD.Value =
                Convert.ToDateTime(
                    row.Cells["NgayQuyetDinh"].Value);

            dtpBatDau.Value =
                Convert.ToDateTime(
                    row.Cells["NgayBatDauThuViec"].Value);

            dtpKetThuc.Value =
                Convert.ToDateTime(
                    row.Cells["NgayKetThucThuViec"].Value);

            decimal luong = Convert.ToDecimal(
                row.Cells["LuongThuViec"].Value);

            numLuong.Value = Math.Min(
                numLuong.Maximum,
                Math.Max(numLuong.Minimum, luong));

            txtDieuKhoan.Text =
                LayO(row, "NoiDungDieuKhoan");
        }

        private void LamMoiNhap()
        {
            txtMaQD.Clear();
            txtDieuKhoan.Clear();

            dtpNgayQD.Value = DateTime.Today;
            dtpBatDau.Value = DateTime.Today;
            dtpKetThuc.Value = DateTime.Today.AddMonths(2);

            numLuong.Value = 6000000;

            if (cboNhanVien.Items.Count > 0)
                cboNhanVien.SelectedIndex = 0;

            if (cboPhongBan.Items.Count > 0)
                cboPhongBan.SelectedIndex = 0;

            dgvQuyetDinh.ClearSelection();
        }

        private void DoiTenCot(
            string tenCot,
            string tieuDe)
        {
            if (dgvQuyetDinh.Columns.Contains(tenCot))
                dgvQuyetDinh.Columns[tenCot].HeaderText =
                    tieuDe;
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

        private Button TaoNut(
            string noiDung,
            int rong)
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
                Margin = new Padding(5)
            };

            table.Controls.Add(lbl, cot, dong);
            table.Controls.Add(control, cot + 1, dong);
        }
    }
}
