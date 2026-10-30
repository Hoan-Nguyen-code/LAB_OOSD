
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmToaThuoc : Form
    {
        private readonly ToaThuocService service =
            new ToaThuocService();

        private ComboBox cboPhieuKham;
        private ComboBox cboThuoc;

        private TextBox txtSoLuong;
        private TextBox txtLieuDung;
        private TextBox txtCachDung;
        private TextBox txtLoiDan;
        private TextBox txtTimKiem;

        private DataGridView dgvChiTiet;
        private DataGridView dgvToaThuoc;

        private DataTable chiTiet;
        private string maToaDangXem = "";

        public FrmToaThuoc()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(TaiDuLieu);
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý toa thuốc";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1200, 820);
            MinimumSize = new Size(1000, 690);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            chiTiet = service.TaoBangChiTiet();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(15)
            };

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 175));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            root.Controls.Add(new Label
            {
                Text = "QUẢN LÝ TOA THUỐC",
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
                RowCount = 3,
                Padding = new Padding(8)
            };

            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 125));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 125));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 3; i++)
            {
                input.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 33.333F));
            }

            cboPhieuKham = TaoCombo();
            cboThuoc = TaoCombo();

            txtSoLuong = TaoTextBox();
            txtLieuDung = TaoTextBox();
            txtCachDung = TaoTextBox();
            txtLoiDan = TaoTextBox();

            ThemTruong(input, "Phiếu khám:",
                cboPhieuKham, 0, 0);

            ThemTruong(input, "Thuốc:",
                cboThuoc, 2, 0);

            ThemTruong(input, "Số lượng:",
                txtSoLuong, 0, 1);

            ThemTruong(input, "Liều dùng:",
                txtLieuDung, 2, 1);

            ThemTruong(input, "Cách dùng:",
                txtCachDung, 0, 2);

            ThemTruong(input, "Lời dặn:",
                txtLoiDan, 2, 2);

            root.Controls.Add(input, 0, 1);

            var thanhCongCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                AutoScroll = true
            };

            Button btnThemThuoc =
                TaoButton("THÊM THUỐC", 135);

            Button btnBoThuoc =
                TaoButton("BỎ THUỐC", 125);

            Button btnLuu =
                TaoButton("LƯU TOA THUỐC", 165);

            Button btnMoi =
                TaoButton("LÀM MỚI", 115);

            Button btnTim =
                TaoButton("TÌM TOA", 115);

            txtTimKiem = new TextBox
            {
                Width = 160,
                Margin = new Padding(12, 12, 5, 5)
            };

            thanhCongCu.Controls.Add(btnThemThuoc);
            thanhCongCu.Controls.Add(btnBoThuoc);
            thanhCongCu.Controls.Add(btnLuu);
            thanhCongCu.Controls.Add(btnMoi);
            thanhCongCu.Controls.Add(txtTimKiem);
            thanhCongCu.Controls.Add(btnTim);

            root.Controls.Add(thanhCongCu, 0, 2);

            var groupChiTiet = new GroupBox
            {
                Text = "Danh sách thuốc trong toa đang lập / chi tiết toa",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            dgvChiTiet = TaoBang();
            dgvChiTiet.DataSource = chiTiet;

            groupChiTiet.Controls.Add(dgvChiTiet);
            root.Controls.Add(groupChiTiet, 0, 3);

            var groupToa = new GroupBox
            {
                Text = "Danh sách toa thuốc đã lưu",
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            dgvToaThuoc = TaoBang();
            dgvToaThuoc.CellClick += DgvToaThuoc_CellClick;

            groupToa.Controls.Add(dgvToaThuoc);
            root.Controls.Add(groupToa, 0, 4);

            Controls.Add(root);

            btnThemThuoc.Click += (s, e) =>
                FormHelper.Chay(ThemThuoc);

            btnBoThuoc.Click += (s, e) =>
                FormHelper.Chay(BoThuoc);

            btnLuu.Click += (s, e) =>
                FormHelper.Chay(LuuToa);

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

        private TextBox TaoTextBox()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 9, 10, 9)
            };
        }

        private ComboBox TaoCombo()
        {
            return new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle =
                    ComboBoxStyle.DropDownList,
                Margin = new Padding(5, 9, 10, 9)
            };
        }

        private void ThemTruong(
            TableLayoutPanel panel,
            string ten,
            Control control,
            int cot,
            int dong)
        {
            panel.Controls.Add(new Label
            {
                Text = ten,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, cot, dong);

            panel.Controls.Add(control, cot + 1, dong);
        }

        private Button TaoButton(string text, int width)
        {
            var button = new Button
            {
                Text = text,
                Width = width,
                Height = 38,
                Margin = new Padding(5, 5, 5, 5),
                BackColor = Color.FromArgb(32, 85, 145),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private DataGridView TaoBang()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                MultiSelect = false,
                RowHeadersVisible = false
            };
        }

        private void TaiDuLieu()
        {
            TaiPhieuKham();
            TaiThuoc();
            TaiDanhSach();
        }

        private void TaiPhieuKham()
        {
            cboPhieuKham.DataSource =
                service.LayPhieuKham();

            cboPhieuKham.DisplayMember = "TenHienThi";
            cboPhieuKham.ValueMember = "MaPhieuKham";
        }

        private void TaiThuoc()
        {
            cboThuoc.DataSource = service.LayThuoc();
            cboThuoc.DisplayMember = "TenThuoc";
            cboThuoc.ValueMember = "MaThuoc";
        }

        private void TaiDanhSach()
        {
            dgvToaThuoc.DataSource =
                service.LayDanhSach(txtTimKiem.Text);

            dgvToaThuoc.ClearSelection();
        }

        private void ThemThuoc()
        {
            if (!string.IsNullOrWhiteSpace(maToaDangXem))
                throw new Exception(
                    "Đang xem toa đã lưu. " +
                    "Nhấn Làm mới để lập toa khác.");

            int soLuong;

            if (!int.TryParse(
                txtSoLuong.Text.Trim(), out soLuong))
                throw new Exception(
                    "Số lượng thuốc phải là số nguyên.");

            service.ThemThuocVaoDanhSach(
                chiTiet,
                FormHelper.Chon(cboThuoc),
                soLuong,
                txtLieuDung.Text,
                txtCachDung.Text);

            txtSoLuong.Clear();
            txtLieuDung.Clear();
            txtCachDung.Clear();
            txtSoLuong.Focus();
        }

        private void BoThuoc()
        {
            if (!string.IsNullOrWhiteSpace(maToaDangXem))
                throw new Exception(
                    "Không thể thay đổi toa đã lưu.");

            if (dgvChiTiet.CurrentRow == null ||
                dgvChiTiet.CurrentRow.DataBoundItem == null)
                throw new Exception(
                    "Vui lòng chọn thuốc cần bỏ.");

            DataRowView view =
                dgvChiTiet.CurrentRow.DataBoundItem
                as DataRowView;

            if (view != null)
                chiTiet.Rows.Remove(view.Row);
        }

        private void LuuToa()
        {
            if (!string.IsNullOrWhiteSpace(maToaDangXem))
                throw new Exception(
                    "Đang xem toa đã lưu. " +
                    "Nhấn Làm mới để lập toa khác.");

            string maToa = service.LapToaThuoc(
                FormHelper.Chon(cboPhieuKham),
                txtLoiDan.Text,
                chiTiet);

            MessageBox.Show(
                "Lập toa thuốc thành công!\n" +
                "Mã toa: " + maToa,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void DgvToaThuoc_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvToaThuoc.Rows[e.RowIndex];

            maToaDangXem = Convert.ToString(
                row.Cells["MaToa"].Value);

            txtLoiDan.Text = Convert.ToString(
                row.Cells["LoiDan"].Value);

            FormHelper.Chay(() =>
            {
                dgvChiTiet.DataSource =
                    service.LayChiTiet(maToaDangXem);
            });
        }

        private void LamMoi()
        {
            maToaDangXem = "";

            txtSoLuong.Clear();
            txtLieuDung.Clear();
            txtCachDung.Clear();
            txtLoiDan.Clear();
            txtTimKiem.Clear();

            chiTiet = service.TaoBangChiTiet();
            dgvChiTiet.DataSource = chiTiet;

            TaiDuLieu();
        }
    }
}
