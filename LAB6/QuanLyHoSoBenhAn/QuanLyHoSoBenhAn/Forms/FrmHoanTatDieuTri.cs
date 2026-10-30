
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmHoanTatDieuTri : Form
    {
        private readonly HoanTatService service =
            new HoanTatService();

        private ComboBox cboPhieuKham;
        private TextBox txtSoTien;
        private TextBox txtTimKiem;
        private CheckBox chkThuTuc;
        private DataGridView dgvThanhToan;
        private Label lblTrangThai;

        public FrmHoanTatDieuTri()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(TaiDuLieu);
        }

        private void TaoGiaoDien()
        {
            Text = "Thanh toán và hoàn tất điều trị";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1180, 740);
            MinimumSize = new Size(980, 620);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(18)
            };

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 135));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 45));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            root.Controls.Add(new Label
            {
                Text = "THANH TOÁN VÀ HOÀN TẤT ĐIỀU TRỊ",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(
                    "Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 62, 95)
            }, 0, 0);

            var input = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2,
                Padding = new Padding(12, 8, 12, 8)
            };

            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 145));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 145));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            input.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50));
            input.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50));

            cboPhieuKham = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle =
                    ComboBoxStyle.DropDownList,
                Margin = new Padding(5, 12, 15, 10)
            };

            txtSoTien = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 12, 15, 10)
            };

            chkThuTuc = new CheckBox
            {
                Text = "Đã hoàn tất thủ tục cần thiết",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(32, 85, 145),
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold)
            };

            lblTrangThai = new Label
            {
                Text = "Chọn phiếu khám để xem trạng thái.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DimGray
            };

            input.Controls.Add(
                TaoNhan("Phiếu khám:"), 0, 0);

            input.Controls.Add(
                cboPhieuKham, 1, 0);

            input.Controls.Add(
                TaoNhan("Số tiền viện phí:"), 2, 0);

            input.Controls.Add(
                txtSoTien, 3, 0);

            input.Controls.Add(
                chkThuTuc, 0, 1);

            input.SetColumnSpan(chkThuTuc, 2);

            input.Controls.Add(
                lblTrangThai, 2, 1);

            input.SetColumnSpan(lblTrangThai, 2);

            root.Controls.Add(input, 0, 1);

            var thanhCongCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 5, 0, 0)
            };

            Button btnThanhToan = TaoButton(
                "GHI NHẬN THANH TOÁN", 205);

            Button btnHoanTat = TaoButton(
                "HOÀN TẤT ĐIỀU TRỊ", 185);

            Button btnCaHai = TaoButton(
                "THANH TOÁN + HOÀN TẤT", 220);

            Button btnMoi = TaoButton(
                "LÀM MỚI", 115);

            Button btnTim = TaoButton(
                "TÌM KIẾM", 115);

            txtTimKiem = new TextBox
            {
                Width = 170,
                Margin = new Padding(12, 12, 5, 5)
            };

            thanhCongCu.Controls.Add(btnThanhToan);
            thanhCongCu.Controls.Add(btnHoanTat);
            thanhCongCu.Controls.Add(btnCaHai);
            thanhCongCu.Controls.Add(btnMoi);
            thanhCongCu.Controls.Add(txtTimKiem);
            thanhCongCu.Controls.Add(btnTim);

            root.Controls.Add(thanhCongCu, 0, 2);

            root.Controls.Add(new Label
            {
                Text =
                    "Chỉ hoàn tất điều trị khi phiếu khám ở trạng thái " +
                    "'Chờ hoàn tất', đã thanh toán và hoàn thành thủ tục.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DimGray
            }, 0, 3);

            dgvThanhToan = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            dgvThanhToan.CellClick +=
                DgvThanhToan_CellClick;

            root.Controls.Add(dgvThanhToan, 0, 4);

            Controls.Add(root);

            btnThanhToan.Click += (s, e) =>
                FormHelper.Chay(GhiNhanThanhToan);

            btnHoanTat.Click += (s, e) =>
                FormHelper.Chay(HoanTat);

            btnCaHai.Click += (s, e) =>
                FormHelper.Chay(ThanhToanVaHoanTat);

            btnMoi.Click += (s, e) =>
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

        private Label TaoNhan(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Button TaoButton(string text, int width)
        {
            var button = new Button
            {
                Text = text,
                Width = width,
                Height = 40,
                Margin = new Padding(5),
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
            cboPhieuKham.DataSource =
                service.LayPhieuKham();

            cboPhieuKham.DisplayMember = "TenHienThi";
            cboPhieuKham.ValueMember = "MaPhieuKham";

            TaiDanhSach();
        }

        private void TaiDanhSach()
        {
            dgvThanhToan.DataSource =
                service.LayDanhSach(txtTimKiem.Text);

            dgvThanhToan.ClearSelection();
        }

        private decimal LaySoTien()
        {
            decimal soTien;

            if (!decimal.TryParse(
                txtSoTien.Text.Trim(), out soTien))
            {
                throw new Exception(
                    "Số tiền viện phí phải là số hợp lệ.");
            }

            if (soTien < 0)
                throw new Exception(
                    "Số tiền viện phí không được âm.");

            return soTien;
        }

        private void GhiNhanThanhToan()
        {
            service.GhiNhanThanhToan(
                FormHelper.Chon(cboPhieuKham),
                LaySoTien());

            MessageBox.Show(
                "Ghi nhận thanh toán viện phí thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void HoanTat()
        {
            service.HoanTatDieuTri(
                FormHelper.Chon(cboPhieuKham),
                chkThuTuc.Checked);

            MessageBox.Show(
                "Hoàn tất điều trị thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void ThanhToanVaHoanTat()
        {
            service.ThanhToanVaHoanTat(
                FormHelper.Chon(cboPhieuKham),
                LaySoTien(),
                chkThuTuc.Checked);

            MessageBox.Show(
                "Thanh toán và hoàn tất điều trị thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void DgvThanhToan_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvThanhToan.Rows[e.RowIndex];

            string maPhieu = Convert.ToString(
                row.Cells["MaPhieuKham"].Value);

            if (cboPhieuKham.DataSource != null)
                cboPhieuKham.SelectedValue = maPhieu;

            object soTien =
                row.Cells["SoTien"].Value;

            txtSoTien.Text =
                soTien == null || soTien == DBNull.Value
                    ? ""
                    : Convert.ToString(soTien);

            string trangThai = Convert.ToString(
                row.Cells["TrangThai"].Value);

            string trangThaiThanhToan = Convert.ToString(
                row.Cells["TrangThaiThanhToan"].Value);

            lblTrangThai.Text =
                "Khám: " + trangThai +
                " | Viện phí: " +
                (string.IsNullOrWhiteSpace(trangThaiThanhToan)
                    ? "Chưa thanh toán"
                    : trangThaiThanhToan);

            chkThuTuc.Checked = false;
        }

        private void LamMoi()
        {
            txtSoTien.Clear();
            txtTimKiem.Clear();

            chkThuTuc.Checked = false;

            lblTrangThai.Text =
                "Chọn phiếu khám để xem trạng thái.";

            TaiDuLieu();
        }
    }
}
