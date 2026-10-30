
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmLichKhamDinhKy : Form
    {
        private readonly LichKhamService service =
            new LichKhamService();

        private ComboBox cboHoSo;
        private DateTimePicker dtpNgayHen;
        private TextBox txtTimKiem;
        private DataGridView dgvLichKham;
        private Label lblThongTin;

        private string maLichDangChon = "";

        public FrmLichKhamDinhKy()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(TaiDuLieu);
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý lịch khám định kỳ";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1150, 720);
            MinimumSize = new Size(950, 600);
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
                new RowStyle(SizeType.Absolute, 115));

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 40));

            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            root.Controls.Add(new Label
            {
                Text = "QUẢN LÝ LỊCH KHÁM ĐỊNH KỲ",
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
                Padding = new Padding(12, 5, 12, 5)
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

            cboHoSo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle =
                    ComboBoxStyle.DropDownList,
                Margin = new Padding(5, 10, 15, 10)
            };

            dtpNgayHen = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Margin = new Padding(5, 10, 15, 10)
            };

            input.Controls.Add(TaoNhan("Hồ sơ bệnh án:"), 0, 0);
            input.Controls.Add(cboHoSo, 1, 0);

            input.Controls.Add(TaoNhan("Ngày giờ hẹn:"), 2, 0);
            input.Controls.Add(dtpNgayHen, 3, 0);

            input.Controls.Add(
                TaoNhan("Quý khám dự kiến:"), 0, 1);

            lblThongTin = new Label
            {
                Text = "",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(32, 85, 145),
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold)
            };

            input.Controls.Add(lblThongTin, 1, 1);
            input.SetColumnSpan(lblThongTin, 3);

            root.Controls.Add(input, 0, 1);

            var thanhCongCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };

            Button btnDatLich =
                TaoButton("ĐẶT LỊCH KHÁM", 165);

            Button btnDaKham =
                TaoButton("XÁC NHẬN ĐÃ KHÁM", 190);

            Button btnHuy =
                TaoButton("HỦY LỊCH", 125);

            Button btnMoi =
                TaoButton("LÀM MỚI", 115);

            Button btnTim =
                TaoButton("TÌM KIẾM", 120);

            txtTimKiem = new TextBox
            {
                Width = 170,
                Margin = new Padding(12, 12, 5, 5)
            };

            thanhCongCu.Controls.Add(btnDatLich);
            thanhCongCu.Controls.Add(btnDaKham);
            thanhCongCu.Controls.Add(btnHuy);
            thanhCongCu.Controls.Add(btnMoi);
            thanhCongCu.Controls.Add(txtTimKiem);
            thanhCongCu.Controls.Add(btnTim);

            root.Controls.Add(thanhCongCu, 0, 2);

            root.Controls.Add(new Label
            {
                Text =
                    "Chọn một lịch trong bảng để xác nhận đã khám hoặc hủy lịch.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.DimGray
            }, 0, 3);

            dgvLichKham = new DataGridView
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

            dgvLichKham.CellClick +=
                DgvLichKham_CellClick;

            root.Controls.Add(dgvLichKham, 0, 4);
            Controls.Add(root);

            dtpNgayHen.ValueChanged +=
                (s, e) => CapNhatThongTinQuy();

            btnDatLich.Click += (s, e) =>
                FormHelper.Chay(DatLich);

            btnDaKham.Click += (s, e) =>
                FormHelper.Chay(XacNhanDaKham);

            btnHuy.Click += (s, e) =>
                FormHelper.Chay(HuyLich);

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

            dtpNgayHen.Value = DateTime.Now.AddDays(1);
            CapNhatThongTinQuy();
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

        private void CapNhatThongTinQuy()
        {
            DateTime ngay = dtpNgayHen.Value;
            int quy = (ngay.Month - 1) / 3 + 1;

            lblThongTin.Text =
                "Quý " + quy + " / Năm " + ngay.Year;
        }

        private void TaiDuLieu()
        {
            cboHoSo.DataSource = service.LayHoSo();
            cboHoSo.DisplayMember = "TenHienThi";
            cboHoSo.ValueMember = "MaHoSo";

            TaiDanhSach();
        }

        private void TaiDanhSach()
        {
            dgvLichKham.DataSource =
                service.LayDanhSach(txtTimKiem.Text);

            dgvLichKham.ClearSelection();
        }

        private void DatLich()
        {
            string maLich = service.DatLich(
                FormHelper.Chon(cboHoSo),
                dtpNgayHen.Value);

            MessageBox.Show(
                "Đặt lịch khám định kỳ thành công!\n" +
                "Mã lịch: " + maLich,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void XacNhanDaKham()
        {
            if (string.IsNullOrWhiteSpace(maLichDangChon))
                throw new Exception(
                    "Vui lòng chọn lịch khám trong bảng.");

            service.XacNhanDaKham(maLichDangChon);

            MessageBox.Show(
                "Đã xác nhận bệnh nhân thực hiện khám định kỳ.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void HuyLich()
        {
            if (string.IsNullOrWhiteSpace(maLichDangChon))
                throw new Exception(
                    "Vui lòng chọn lịch khám cần hủy.");

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy lịch khám này?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            service.HuyLich(maLichDangChon);

            MessageBox.Show(
                "Hủy lịch khám thành công.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void DgvLichKham_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvLichKham.Rows[e.RowIndex];

            maLichDangChon = Convert.ToString(
                row.Cells["MaLich"].Value);

            string maHoSo = Convert.ToString(
                row.Cells["MaHoSo"].Value);

            if (cboHoSo.DataSource != null)
                cboHoSo.SelectedValue = maHoSo;

            object ngayHen =
                row.Cells["NgayHen"].Value;

            if (ngayHen != null &&
                ngayHen != DBNull.Value)
                dtpNgayHen.Value =
                    Convert.ToDateTime(ngayHen);
        }

        private void LamMoi()
        {
            maLichDangChon = "";

            txtTimKiem.Clear();
            dtpNgayHen.Value = DateTime.Now.AddDays(1);

            TaiDuLieu();
        }
    }
}
