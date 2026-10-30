
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmHopDong : Form
    {
        private readonly HopDongService service =
            new HopDongService();

        private TextBox txtMaHopDong;
        private TextBox txtNoiDung;
        private ComboBox cboQuyetDinh;
        private ComboBox cboLoaiHopDong;

        private DateTimePicker dtpNgayKy;
        private DateTimePicker dtpBatDau;
        private DateTimePicker dtpKetThuc;
        private CheckBox chkCoNgayKetThuc;

        private GroupBox grpBaoHiem;
        private TextBox txtMaBaoHiem;
        private TextBox txtSoBHXH;
        private TextBox txtSoBHYT;
        private NumericUpDown numBHXH;
        private NumericUpDown numBHYT;
        private DateTimePicker dtpNgayThamGia;

        private DataGridView dgvHopDong;

        public FrmHopDong()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
            {
                FormHelper.Chay(() =>
                {
                    NapQuyetDinh();
                    NapHopDong();
                    LamMoiNhap();
                });
            };
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Hợp đồng lao động và bảo hiểm";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1250, 820);
            MinimumSize = new Size(1050, 710);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ HỢP ĐỒNG LAO ĐỘNG VÀ BẢO HIỂM",
                Dock = DockStyle.Top,
                Height = 62,
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 66, 105),
                TextAlign = ContentAlignment.MiddleCenter
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(14)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 245));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 175));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            TaoNhomHopDong(layout);
            TaoNhomBaoHiem(layout);
            TaoThanhCongCu(layout);
            TaoBangHopDong(layout);

            Controls.Add(layout);
            Controls.Add(lblTieuDe);
        }

        private void TaoNhomHopDong(TableLayoutPanel layout)
        {
            GroupBox grp = new GroupBox
            {
                Text = "Thông tin hợp đồng lao động",
                Dock = DockStyle.Fill,
                Padding = new Padding(12)
            };

            TableLayoutPanel form = TaoBangNhap(4, 4);

            txtMaHopDong = TaoTextBox();
            txtNoiDung = TaoTextBox();
            txtNoiDung.Multiline = true;
            txtNoiDung.ScrollBars = ScrollBars.Vertical;

            cboQuyetDinh = TaoComboBox();
            cboLoaiHopDong = TaoComboBox();
            cboLoaiHopDong.Items.AddRange(
                new object[] { "Ngắn hạn", "Dài hạn" });
            cboLoaiHopDong.SelectedIndex = 0;

            dtpNgayKy = TaoNgay();
            dtpBatDau = TaoNgay();
            dtpKetThuc = TaoNgay();

            chkCoNgayKetThuc = new CheckBox
            {
                Text = "Có ngày kết thúc",
                Dock = DockStyle.Fill,
                Checked = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            ThemTruong(form, 0, 0,
                "Mã hợp đồng:", txtMaHopDong);
            ThemTruong(form, 2, 0,
                "Quyết định tuyển dụng:", cboQuyetDinh);

            ThemTruong(form, 0, 1,
                "Loại hợp đồng:", cboLoaiHopDong);
            ThemTruong(form, 2, 1,
                "Ngày ký:", dtpNgayKy);

            ThemTruong(form, 0, 2,
                "Ngày bắt đầu:", dtpBatDau);
            ThemTruong(form, 2, 2,
                "Ngày kết thúc:", dtpKetThuc);

            Panel pnlNoiDung = new Panel
            {
                Dock = DockStyle.Fill
            };

            Label lblNoiDung = new Label
            {
                Text = "Nội dung hợp đồng:",
                Dock = DockStyle.Left,
                Width = 160,
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlNoiDung.Controls.Add(txtNoiDung);
            pnlNoiDung.Controls.Add(lblNoiDung);
            txtNoiDung.Dock = DockStyle.Fill;

            form.Controls.Add(pnlNoiDung, 0, 3);
            form.SetColumnSpan(pnlNoiDung, 3);

            form.Controls.Add(chkCoNgayKetThuc, 3, 3);

            cboLoaiHopDong.SelectedIndexChanged +=
                (s, e) => CapNhatLoaiHopDong();

            chkCoNgayKetThuc.CheckedChanged +=
                (s, e) => CapNhatNgayKetThuc();

            grp.Controls.Add(form);
            layout.Controls.Add(grp, 0, 0);
        }

        private void TaoNhomBaoHiem(TableLayoutPanel layout)
        {
            grpBaoHiem = new GroupBox
            {
                Text = "Hồ sơ BHYT - BHXH (hợp đồng dài hạn)",
                Dock = DockStyle.Fill,
                Padding = new Padding(12)
            };

            TableLayoutPanel form = TaoBangNhap(4, 3);

            txtMaBaoHiem = TaoTextBox();
            txtSoBHXH = TaoTextBox();
            txtSoBHYT = TaoTextBox();

            numBHXH = TaoTien();
            numBHYT = TaoTien();
            dtpNgayThamGia = TaoNgay();

            ThemTruong(form, 0, 0,
                "Mã hồ sơ bảo hiểm:", txtMaBaoHiem);
            ThemTruong(form, 2, 0,
                "Số BHXH:", txtSoBHXH);

            ThemTruong(form, 0, 1,
                "Số BHYT:", txtSoBHYT);
            ThemTruong(form, 2, 1,
                "Mức đóng BHXH:", numBHXH);

            ThemTruong(form, 0, 2,
                "Mức đóng BHYT:", numBHYT);
            ThemTruong(form, 2, 2,
                "Ngày tham gia:", dtpNgayThamGia);

            grpBaoHiem.Controls.Add(form);
            layout.Controls.Add(grpBaoHiem, 0, 1);
        }

        private void TaoThanhCongCu(TableLayoutPanel layout)
        {
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true
            };

            Button btnKy = TaoNut("Ký hợp đồng", 160);
            Button btnLamMoi = TaoNut("Làm mới", 130);
            Button btnTaiLai = TaoNut("Tải danh sách", 160);

            btnKy.Click += (s, e) =>
                FormHelper.Chay(KyHopDong);

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapQuyetDinh();
                    LamMoiNhap();
                });

            btnTaiLai.Click += (s, e) =>
                FormHelper.Chay(() =>
                {
                    NapQuyetDinh();
                    NapHopDong();
                    LamMoiNhap();
                });

            panel.Controls.Add(btnKy);
            panel.Controls.Add(btnLamMoi);
            panel.Controls.Add(btnTaiLai);

            layout.Controls.Add(panel, 0, 2);
        }

        private void TaoBangHopDong(TableLayoutPanel layout)
        {
            dgvHopDong = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.DisplayedCells,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };

            dgvHopDong.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 78, 121);
            dgvHopDong.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvHopDong.ColumnHeadersHeight = 40;
            dgvHopDong.RowTemplate.Height = 30;

            dgvHopDong.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    XemHopDongDaKy();
            };

            layout.Controls.Add(dgvHopDong, 0, 3);
        }

        private void NapQuyetDinh()
        {
            DataTable dt = service.LayQuyetDinhTuyenDung();

            FormHelper.Nap(
                cboQuyetDinh,
                dt,
                "HienThi",
                "MaQDTD");
        }

        private void NapHopDong()
        {
            dgvHopDong.DataSource = service.LayDanhSachHopDong();

            DoiTenCot("MaHopDong", "Mã hợp đồng");
            DoiTenCot("MaNV", "Mã NV");
            DoiTenCot("HoTen", "Họ tên");
            DoiTenCot("MaQDTD", "Quyết định tuyển dụng");
            DoiTenCot("LoaiHopDong", "Loại hợp đồng");
            DoiTenCot("NgayKy", "Ngày ký");
            DoiTenCot("NgayBatDau", "Bắt đầu");
            DoiTenCot("NgayKetThuc", "Kết thúc");
            DoiTenCot("NoiDungHopDong", "Nội dung");
            DoiTenCot("TrangThai", "Trạng thái");
            DoiTenCot("MaBaoHiem", "Mã hồ sơ BH");
            DoiTenCot("SoBHXH", "Số BHXH");
            DoiTenCot("SoBHYT", "Số BHYT");
            DoiTenCot("MucDongBHXH", "Mức đóng BHXH");
            DoiTenCot("MucDongBHYT", "Mức đóng BHYT");
            DoiTenCot("NgayThamGia", "Ngày tham gia");

            foreach (string cot in new[]
            {
                "NgayKy", "NgayBatDau",
                "NgayKetThuc", "NgayThamGia"
            })
            {
                if (dgvHopDong.Columns.Contains(cot))
                {
                    dgvHopDong.Columns[cot]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }

            foreach (string cot in new[]
            {
                "MucDongBHXH", "MucDongBHYT"
            })
            {
                if (dgvHopDong.Columns.Contains(cot))
                {
                    dgvHopDong.Columns[cot]
                        .DefaultCellStyle.Format = "N0";
                }
            }

            dgvHopDong.ClearSelection();
        }

        private void KyHopDong()
        {
            if (cboQuyetDinh.SelectedValue == null)
            {
                MessageBox.Show(
                    "Không còn quyết định tuyển dụng nào " +
                    "chưa ký hợp đồng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool daiHan = cboLoaiHopDong.Text == "Dài hạn";

            DateTime? ngayKetThuc =
                chkCoNgayKetThuc.Checked
                    ? (DateTime?)dtpKetThuc.Value.Date
                    : null;

            var ketQua = service.KyHopDong(
                txtMaHopDong.Text,
                FormHelper.Gia(cboQuyetDinh),
                cboLoaiHopDong.Text,
                dtpNgayKy.Value,
                dtpBatDau.Value,
                ngayKetThuc,
                txtNoiDung.Text,
                daiHan ? txtMaBaoHiem.Text : null,
                daiHan ? txtSoBHXH.Text : null,
                daiHan ? txtSoBHYT.Text : null,
                daiHan ? numBHXH.Value : 0,
                daiHan ? numBHYT.Value : 0,
                dtpNgayThamGia.Value);

            if (FormHelper.Bao(ketQua))
            {
                NapQuyetDinh();
                NapHopDong();
                LamMoiNhap();
            }
        }

        private void CapNhatLoaiHopDong()
        {
            bool daiHan = cboLoaiHopDong.Text == "Dài hạn";

            if (grpBaoHiem != null)
                grpBaoHiem.Enabled = daiHan;

            if (chkCoNgayKetThuc != null && !daiHan)
                chkCoNgayKetThuc.Checked = true;

            if (chkCoNgayKetThuc != null)
                chkCoNgayKetThuc.Enabled = daiHan;

            CapNhatNgayKetThuc();
        }

        private void CapNhatNgayKetThuc()
        {
            if (dtpKetThuc != null && chkCoNgayKetThuc != null)
                dtpKetThuc.Enabled = chkCoNgayKetThuc.Checked;
        }

        private void LamMoiNhap()
        {
            txtMaHopDong.Clear();
            txtNoiDung.Clear();

            cboLoaiHopDong.SelectedIndex = 0;
            if (cboQuyetDinh.Items.Count > 0)
                cboQuyetDinh.SelectedIndex = 0;

            dtpNgayKy.Value = DateTime.Today;
            dtpBatDau.Value = DateTime.Today;
            dtpKetThuc.Value = DateTime.Today.AddYears(1);
            chkCoNgayKetThuc.Checked = true;

            txtMaBaoHiem.Clear();
            txtSoBHXH.Clear();
            txtSoBHYT.Clear();

            numBHXH.Value = 0;
            numBHYT.Value = 0;

            dtpNgayThamGia.Value = DateTime.Today;

            CapNhatLoaiHopDong();

            dgvHopDong.ClearSelection();
        }

        private void XemHopDongDaKy()
        {
            if (dgvHopDong.CurrentRow == null ||
                dgvHopDong.CurrentRow.IsNewRow)
                return;

            DataGridViewRow row = dgvHopDong.CurrentRow;

            // Chỉ hiển thị các thông tin đã lưu.
            // Form này không sửa hợp đồng đã ký.
            string ma = LayO(row, "MaHopDong");
            string loai = LayO(row, "LoaiHopDong");
            string hoTen = LayO(row, "HoTen");
            string maBaoHiem = LayO(row, "MaBaoHiem");

            MessageBox.Show(
                "Mã hợp đồng: " + ma +
                "\nNhân viên: " + hoTen +
                "\nLoại hợp đồng: " + loai +
                "\nHồ sơ bảo hiểm: " +
                (maBaoHiem == "" ? "Không có" : maBaoHiem),
                "Thông tin hợp đồng đã ký",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void DoiTenCot(string cot, string tieuDe)
        {
            if (dgvHopDong.Columns.Contains(cot))
                dgvHopDong.Columns[cot].HeaderText = tieuDe;
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

        private TableLayoutPanel TaoBangNhap(int cot, int dong)
        {
            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = cot,
                RowCount = dong
            };

            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 165));
            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 175));
            table.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < dong; i++)
                table.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 100F / dong));

            return table;
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

        private Button TaoNut(string tieuDe, int rong)
        {
            Button btn = new Button
            {
                Text = tieuDe,
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
    }
}
