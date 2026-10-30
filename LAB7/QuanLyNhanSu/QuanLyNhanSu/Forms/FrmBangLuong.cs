
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmBangLuong : Form
    {
        private readonly BangLuongService service =
            new BangLuongService();

        private NumericUpDown numThang;
        private NumericUpDown numNam;
        private NumericUpDown numGioChuan;

        private DataGridView dgvDuKien;
        private DataGridView dgvBangLuong;
        private DataGridView dgvChiTiet;

        private Label lblTongDuKien;
        private Label lblTongDaLuu;

        public FrmBangLuong()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
            {
                FormHelper.Chay(NapLichSu);
            };
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Bảng lương nhân viên";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1250, 800);
            MinimumSize = new Size(1020, 650);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            Label title = new Label
            {
                Text = "QUẢN LÝ VÀ TÍNH LƯƠNG HẰNG THÁNG",
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

            TabPage tabTinh = new TabPage("Tính lương tháng");
            TabPage tabLichSu = new TabPage("Lịch sử bảng lương");

            TaoTabTinhLuong(tabTinh);
            TaoTabLichSu(tabLichSu);

            tabs.TabPages.Add(tabTinh);
            tabs.TabPages.Add(tabLichSu);

            Controls.Add(tabs);
            Controls.Add(title);
        }

        // ==========================================
        // TAB TÍNH LƯƠNG THÁNG
        // ==========================================

        private void TaoTabTinhLuong(TabPage tab)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(15)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 125));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 58));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60));

            GroupBox grp = new GroupBox
            {
                Text = "Chọn kỳ lương và giờ công chuẩn",
                Dock = DockStyle.Fill
            };

            FlowLayoutPanel pnlNhap = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 17, 5, 5),
                WrapContents = true
            };

            numThang = TaoSo(1, 12, DateTime.Today.Month, 80);
            numNam = TaoSo(2000, 2100, DateTime.Today.Year, 95);

            numGioChuan = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 744,
                Value = 176,
                DecimalPlaces = 2,
                Increment = 8,
                Width = 110,
                ThousandsSeparator = true
            };

            pnlNhap.Controls.Add(TaoNhan("Tháng:"));
            pnlNhap.Controls.Add(numThang);
            pnlNhap.Controls.Add(TaoNhan("Năm:"));
            pnlNhap.Controls.Add(numNam);
            pnlNhap.Controls.Add(TaoNhan("Giờ công chuẩn:"));
            pnlNhap.Controls.Add(numGioChuan);

            grp.Controls.Add(pnlNhap);

            FlowLayoutPanel congCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                AutoScroll = true
            };

            Button btnTinh = TaoNut("Tính dự kiến", 150);
            Button btnLuu = TaoNut("Lập bảng lương", 160);
            Button btnLamMoi = TaoNut("Làm mới", 120);

            congCu.Controls.Add(btnTinh);
            congCu.Controls.Add(btnLuu);
            congCu.Controls.Add(btnLamMoi);

            dgvDuKien = TaoBang();

            lblTongDuKien = new Label
            {
                Text = "Tổng lương dự kiến: 0 đồng",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(26, 111, 60),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(15)
            };

            layout.Controls.Add(grp, 0, 0);
            layout.Controls.Add(congCu, 0, 1);
            layout.Controls.Add(dgvDuKien, 0, 2);
            layout.Controls.Add(lblTongDuKien, 0, 3);

            tab.Controls.Add(layout);

            btnTinh.Click += (s, e) =>
                FormHelper.Chay(TinhDuKien);

            btnLuu.Click += (s, e) =>
                FormHelper.Chay(LapBangLuong);

            btnLamMoi.Click += (s, e) =>
            {
                dgvDuKien.DataSource = null;
                lblTongDuKien.Text =
                    "Tổng lương dự kiến: 0 đồng";

                numThang.Value = DateTime.Today.Month;
                numNam.Value = DateTime.Today.Year;
                numGioChuan.Value = 176;
            };
        }

        // ==========================================
        // TAB LỊCH SỬ BẢNG LƯƠNG
        // ==========================================

        private void TaoTabLichSu(TabPage tab)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(15)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 50));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 35));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 65));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));

            FlowLayoutPanel congCu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill
            };

            Button btnTai = TaoNut("Tải lịch sử", 140);
            congCu.Controls.Add(btnTai);

            dgvBangLuong = TaoBang();

            Label lblChiTiet = new Label
            {
                Text = "CHI TIẾT BẢNG LƯƠNG ĐÃ CHỌN",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            dgvChiTiet = TaoBang();

            lblTongDaLuu = new Label
            {
                Text = "Tổng lương đã lưu: 0 đồng",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(26, 111, 60),
                TextAlign = ContentAlignment.MiddleRight
            };

            layout.Controls.Add(congCu, 0, 0);
            layout.Controls.Add(dgvBangLuong, 0, 1);
            layout.Controls.Add(lblChiTiet, 0, 2);
            layout.Controls.Add(dgvChiTiet, 0, 3);
            layout.Controls.Add(lblTongDaLuu, 0, 4);

            tab.Controls.Add(layout);

            btnTai.Click += (s, e) =>
                FormHelper.Chay(NapLichSu);

            dgvBangLuong.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    FormHelper.Chay(NapChiTiet);
            };
        }

        // ==========================================
        // NGHIỆP VỤ FORM
        // ==========================================

        private void TinhDuKien()
        {
            DataTable dt = service.TinhDuKien(
                (int)numThang.Value,
                (int)numNam.Value,
                numGioChuan.Value);

            dgvDuKien.DataSource = dt;
            DinhDangBang(dgvDuKien);

            decimal tong = 0;

            foreach (DataRow row in dt.Rows)
                tong += Convert.ToDecimal(
                    row["LuongThucLanh"]);

            lblTongDuKien.Text =
                "Tổng lương dự kiến: " +
                tong.ToString("N0") + " đồng";

            if (dt.Rows.Count == 0)
                MessageBox.Show(
                    "Chưa có dữ liệu chấm công trong tháng này.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            dgvDuKien.ClearSelection();
        }

        private void LapBangLuong()
        {
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            DialogResult xacNhan = MessageBox.Show(
                "Lập và lưu bảng lương tháng " +
                thang + "/" + nam + "?\n" +
                "Sau khi lưu, hệ thống không cho " +
                "lập trùng bảng lương tháng này.",
                "Xác nhận lập bảng lương",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
                return;

            var kq = service.LapBangLuong(
                thang, nam, numGioChuan.Value);

            if (FormHelper.Bao(kq))
            {
                TinhDuKien();
                NapLichSu();
            }
        }

        private void NapLichSu()
        {
            dgvBangLuong.DataSource =
                service.LayBangLuong();

            DoiTenCot(dgvBangLuong,
                "MaBangLuong", "Mã bảng lương");
            DoiTenCot(dgvBangLuong,
                "Thang", "Tháng");
            DoiTenCot(dgvBangLuong,
                "Nam", "Năm");
            DoiTenCot(dgvBangLuong,
                "NgayLap", "Ngày lập");
            DoiTenCot(dgvBangLuong,
                "GioCongChuan", "Giờ công chuẩn");
            DoiTenCot(dgvBangLuong,
                "TongLuong", "Tổng thực lãnh");

            DinhDangNgay(dgvBangLuong, "NgayLap");
            DinhDangTien(dgvBangLuong, "TongLuong");

            dgvBangLuong.ClearSelection();
            dgvChiTiet.DataSource = null;
            lblTongDaLuu.Text =
                "Tổng lương đã lưu: 0 đồng";
        }

        private void NapChiTiet()
        {
            if (dgvBangLuong.CurrentRow == null ||
                dgvBangLuong.CurrentRow.IsNewRow)
                return;

            DataGridViewRow row = dgvBangLuong.CurrentRow;

            string maBang = Convert.ToString(
                row.Cells["MaBangLuong"].Value);

            dgvChiTiet.DataSource =
                service.LayChiTiet(maBang);

            DinhDangBang(dgvChiTiet);

            decimal tong = Convert.ToDecimal(
                row.Cells["TongLuong"].Value);

            lblTongDaLuu.Text =
                "Bảng " + maBang +
                " - Tổng thực lãnh: " +
                tong.ToString("N0") + " đồng";

            dgvChiTiet.ClearSelection();
        }

        // ==========================================
        // GIAO DIỆN CHUNG
        // ==========================================

        private NumericUpDown TaoSo(
            int min, int max, int giaTri, int rong)
        {
            return new NumericUpDown
            {
                Minimum = min,
                Maximum = max,
                Value = giaTri,
                Width = rong
            };
        }

        private Label TaoNhan(string noiDung)
        {
            return new Label
            {
                Text = noiDung,
                AutoSize = true,
                Margin = new Padding(8, 4, 8, 5)
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
                    DataGridViewAutoSizeColumnsMode.DisplayedCells,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 78, 121);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 32;

            return dgv;
        }

        private void DoiTenCot(
            DataGridView dgv,
            string cot, string tieuDe)
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

        private void DinhDangBang(DataGridView dgv)
        {
            DoiTenCot(dgv, "MaNV", "Mã NV");
            DoiTenCot(dgv, "HoTen", "Họ tên");
            DoiTenCot(dgv, "NguonLuong", "Loại lương");
            DoiTenCot(dgv, "GioLam", "Giờ làm");
            DoiTenCot(dgv, "GioPhuTroi", "Giờ phụ trội");
            DoiTenCot(dgv, "LuongCoBan", "Lương cơ bản");
            DoiTenCot(dgv, "LuongTheoCong", "Lương theo công");
            DoiTenCot(dgv, "TongPhuCap", "Tổng phụ cấp");
            DoiTenCot(dgv, "TienPhuTroi", "Tiền phụ trội");
            DoiTenCot(dgv, "TienThuong", "Tiền thưởng");
            DoiTenCot(dgv, "TienPhat", "Tiền phạt");
            DoiTenCot(dgv,
                "KhoanTrichBaoHiem", "Trích bảo hiểm");
            DoiTenCot(dgv,
                "LuongThucLanh", "Lương thực lãnh");

            foreach (string cot in new[]
            {
                "LuongCoBan", "LuongTheoCong",
                "TongPhuCap", "TienPhuTroi",
                "TienThuong", "TienPhat",
                "KhoanTrichBaoHiem", "LuongThucLanh"
            })
            {
                DinhDangTien(dgv, cot);
            }
        }
    }
}
