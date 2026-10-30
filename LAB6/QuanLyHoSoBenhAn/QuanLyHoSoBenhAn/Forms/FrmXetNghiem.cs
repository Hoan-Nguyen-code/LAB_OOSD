
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmXetNghiem : Form
    {
        private readonly XetNghiemService service =
            new XetNghiemService();

        private ComboBox cboPhieuKham;
        private TextBox txtTenXN;
        private TextBox txtKetQua;
        private TextBox txtTimKiem;
        private DataGridView dgvXetNghiem;

        private string maXNDangChon = "";

        public FrmXetNghiem()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(TaiDuLieu);
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý xét nghiệm";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1150, 720);
            MinimumSize = new Size(950, 620);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(18)
            };

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 175));

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            var lblTitle = new Label
            {
                Text = "QUẢN LÝ XÉT NGHIỆM",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(
                    "Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 62, 95)
            };

            root.Controls.Add(lblTitle, 0, 0);

            var input = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(15, 8, 15, 8)
            };

            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 165));

            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < 3; i++)
            {
                input.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 33.333F));
            }

            cboPhieuKham = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle =
                    ComboBoxStyle.DropDownList,
                Margin = new Padding(5, 8, 20, 8)
            };

            txtTenXN = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 8, 20, 8),
                MaxLength = 160
            };

            txtKetQua = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Margin = new Padding(5, 3, 20, 3),
                MaxLength = 1000
            };

            ThemTruong(
                input, "Phiếu khám:", cboPhieuKham, 0);

            ThemTruong(
                input, "Tên xét nghiệm:", txtTenXN, 1);

            ThemTruong(
                input, "Kết quả:", txtKetQua, 2);

            root.Controls.Add(input, 0, 1);

            var thanhCongCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };

            var btnChiDinh = TaoButton(
                "CHỈ ĐỊNH XÉT NGHIỆM", 200);

            var btnKetQua = TaoButton(
                "GHI KẾT QUẢ", 145);

            var btnLamMoi = TaoButton(
                "LÀM MỚI", 125);

            var btnTim = TaoButton(
                "TÌM KIẾM", 125);

            txtTimKiem = new TextBox
            {
                Width = 180,
                Margin = new Padding(15, 12, 5, 5)
            };

            thanhCongCu.Controls.Add(btnChiDinh);
            thanhCongCu.Controls.Add(btnKetQua);
            thanhCongCu.Controls.Add(btnLamMoi);
            thanhCongCu.Controls.Add(txtTimKiem);
            thanhCongCu.Controls.Add(btnTim);

            root.Controls.Add(thanhCongCu, 0, 2);

            dgvXetNghiem = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false
            };

            dgvXetNghiem.CellClick +=
                DgvXetNghiem_CellClick;

            root.Controls.Add(dgvXetNghiem, 0, 3);

            Controls.Add(root);

            btnChiDinh.Click += (s, e) =>
                FormHelper.Chay(ChiDinh);

            btnKetQua.Click += (s, e) =>
                FormHelper.Chay(GhiKetQua);

            btnLamMoi.Click += (s, e) =>
                FormHelper.Chay(LamMoi);

            btnTim.Click += (s, e) =>
                FormHelper.Chay(TaiDanhSach);

            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FormHelper.Chay(TaiDanhSach);
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void ThemTruong(
            TableLayoutPanel panel,
            string ten,
            Control control,
            int row)
        {
            panel.Controls.Add(new Label
            {
                Text = ten,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, row);

            panel.Controls.Add(control, 1, row);
        }

        private Button TaoButton(string text, int width)
        {
            var button = new Button
            {
                Text = text,
                Width = width,
                Height = 40,
                Margin = new Padding(5, 5, 5, 5),
                Font = new Font(
                    "Segoe UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(32, 85, 145),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private void TaiDuLieu()
        {
            TaiPhieuKham();
            TaiDanhSach();
        }

        private void TaiPhieuKham()
        {
            cboPhieuKham.DataSource =
                service.LayPhieuKham();

            cboPhieuKham.DisplayMember = "TenHienThi";
            cboPhieuKham.ValueMember = "MaPhieuKham";
        }

        private void TaiDanhSach()
        {
            dgvXetNghiem.DataSource =
                service.LayDanhSach(txtTimKiem.Text);

            dgvXetNghiem.ClearSelection();
        }

        private void ChiDinh()
        {
            string maPhieu =
                FormHelper.Chon(cboPhieuKham);

            string maXN = service.ChiDinhXetNghiem(
                maPhieu,
                txtTenXN.Text);

            MessageBox.Show(
                "Chỉ định xét nghiệm thành công!\n" +
                "Mã xét nghiệm: " + maXN,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void GhiKetQua()
        {
            if (string.IsNullOrWhiteSpace(maXNDangChon))
                throw new Exception(
                    "Vui lòng chọn xét nghiệm trong bảng.");

            service.GhiKetQua(
                maXNDangChon,
                txtKetQua.Text);

            MessageBox.Show(
                "Ghi kết quả xét nghiệm thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void DgvXetNghiem_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvXetNghiem.Rows[e.RowIndex];

            maXNDangChon = Convert.ToString(
                row.Cells["MaXN"].Value);

            string maPhieu = Convert.ToString(
                row.Cells["MaPhieuKham"].Value);

            if (cboPhieuKham.DataSource != null)
            {
                cboPhieuKham.SelectedValue = maPhieu;
            }

            txtTenXN.Text = Convert.ToString(
                row.Cells["TenXetNghiem"].Value);

            txtKetQua.Text = Convert.ToString(
                row.Cells["KetQua"].Value);
        }

        private void LamMoi()
        {
            maXNDangChon = "";

            txtTenXN.Clear();
            txtKetQua.Clear();
            txtTimKiem.Clear();

            TaiDuLieu();
        }
    }
}
