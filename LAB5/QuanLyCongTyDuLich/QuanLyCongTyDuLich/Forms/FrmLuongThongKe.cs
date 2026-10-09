
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmLuongThongKe : Form
    {
        private readonly ThongKeService svc =
            new ThongKeService();

        private TabControl tabTK;

        private NumericUpDown numThang;
        private NumericUpDown numNam;

        private DateTimePicker dtTu;
        private DateTimePicker dtDen;

        private DataGridView dgvLuong;
        private DataGridView dgvTongHop;

        private Button btnLuong;
        private Button btnTongHop;
        private Button btnDong;

        public FrmLuongThongKe()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmLuongThongKe_Load;
        }

        // ========================================
        // 1. GIAO DIEN CHINH
        // ========================================

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Lương hướng dẫn viên - Thống kê";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1100, 680);
            MinimumSize = new Size(900, 580);

            Font = new Font("Segoe UI", 10);
            BackColor = Color.FromArgb(245, 248, 252);

            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(15)
            };

            main.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            main.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60));

            Controls.Add(main);

            Label title = new Label
            {
                Text = "LƯƠNG HƯỚNG DẪN VIÊN - THỐNG KÊ",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(
                    "Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95)
            };

            main.Controls.Add(title, 0, 0);

            tabTK = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(0)
            };

            main.Controls.Add(tabTK, 0, 1);

            TaoTabLuong();
            TaoTabThongKe();

            Panel footer = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

            btnDong = TaoNut(
                "Đóng", 150, Color.Firebrick);

            btnDong.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;

            btnDong.Margin = new Padding(0);

            footer.Controls.Add(btnDong);

            footer.Resize += (s, e) =>
            {
                btnDong.Location = new Point(
                    Math.Max(0,
                        footer.ClientSize.Width -
                        btnDong.Width - 8),
                    8);
            };

            btnDong.Click += btnDong_Click;

            main.Controls.Add(footer, 0, 2);

            ResumeLayout(true);
        }

        // ========================================
        // 2. CAC CONTROL DUNG CHUNG
        // ========================================

        private TableLayoutPanel TaoTab(
            string ten)
        {
            TabPage page = new TabPage(ten)
            {
                BackColor = Color.White,
                Padding = new Padding(12)
            };

            TableLayoutPanel body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };

            body.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            body.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 105));

            body.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            page.Controls.Add(body);
            tabTK.TabPages.Add(page);

            return body;
        }

        private FlowLayoutPanel TaoPanelLoc()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.FromArgb(240, 245, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private Panel TaoNhom(
            FlowLayoutPanel panel,
            string ten, int width)
        {
            Panel p = new Panel
            {
                Width = width + 12,
                Height = 70,
                Margin = new Padding(6)
            };

            Label lbl = new Label
            {
                Text = ten,
                Location = new Point(0, 0),
                Size = new Size(width, 25),
                Font = new Font("Segoe UI", 9)
            };

            p.Controls.Add(lbl);
            panel.Controls.Add(p);

            return p;
        }

        private NumericUpDown TaoSo(
            FlowLayoutPanel panel,
            string ten,
            decimal min,
            decimal max,
            decimal value,
            int width)
        {
            Panel p = TaoNhom(panel, ten, width);

            NumericUpDown num = new NumericUpDown
            {
                Location = new Point(0, 30),
                Width = width,
                Minimum = min,
                Maximum = max,
                Value = value,
                ThousandsSeparator = false,
                Font = new Font("Segoe UI", 10)
            };

            p.Controls.Add(num);
            return num;
        }

        private DateTimePicker TaoNgay(
            FlowLayoutPanel panel, string ten)
        {
            Panel p = TaoNhom(panel, ten, 180);

            DateTimePicker dt = new DateTimePicker
            {
                Location = new Point(0, 30),
                Width = 180,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };

            p.Controls.Add(dt);
            return dt;
        }

        private Button TaoNut(
            string ten, int width, Color mau)
        {
            Button btn = new Button
            {
                Text = ten,
                Size = new Size(width, 42),
                BackColor = mau,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(8, 21, 8, 5)
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
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0)
            };

            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode
                    .DisableResizing;

            dgv.RowTemplate.Height = 34;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 95, 155);

            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);

            return dgv;
        }

        // ========================================
        // 3. TAB LUONG HUONG DAN VIEN
        // ========================================

        private void TaoTabLuong()
        {
            TableLayoutPanel body =
                TaoTab("Lương hướng dẫn viên");

            FlowLayoutPanel panel = TaoPanelLoc();

            numThang = TaoSo(
                panel, "Tháng", 1, 12, 10, 135);

            numNam = TaoSo(
                panel, "Năm", 2000, 2100, 2026, 150);

            btnLuong = TaoNut(
                "Tính lương",
                185,
                Color.FromArgb(35, 95, 155));

            panel.Controls.Add(btnLuong);

            body.Controls.Add(panel, 0, 0);

            dgvLuong = TaoBang();

            body.Controls.Add(dgvLuong, 0, 1);

            btnLuong.Click += btnLuong_Click;
        }

        // ========================================
        // 4. TAB THONG KE TONG HOP
        // ========================================

        private void TaoTabThongKe()
        {
            TableLayoutPanel body =
                TaoTab("Thống kê tổng hợp");

            FlowLayoutPanel panel = TaoPanelLoc();

            dtTu = TaoNgay(panel, "Từ ngày");
            dtDen = TaoNgay(panel, "Đến ngày");

            btnTongHop = TaoNut(
                "Thống kê",
                185,
                Color.FromArgb(35, 95, 155));

            panel.Controls.Add(btnTongHop);

            body.Controls.Add(panel, 0, 0);

            dgvTongHop = TaoBang();

            body.Controls.Add(dgvTongHop, 0, 1);

            btnTongHop.Click += btnTongHop_Click;
        }

        // ========================================
        // 5. LOAD MAC DINH
        // ========================================

        private void FrmLuongThongKe_Load(
            object sender, EventArgs e)
        {
            numThang.Value = DateTime.Today.Month;
            numNam.Value = DateTime.Today.Year;

            dtTu.Value =
                new DateTime(DateTime.Today.Year, 1, 1);

            dtDen.Value = DateTime.Today;
        }

        // ========================================
        // 6. TINH LUONG HDV
        // ========================================

        private void btnLuong_Click(
            object sender, EventArgs e)
        {
            try
            {
                dgvLuong.DataSource = svc.LuongHDV(
                    (int)numThang.Value,
                    (int)numNam.Value);

                foreach (string col in new string[]
                {
                    "LuongCoBan",
                    "LuongTheoTour",
                    "TongLuong"
                })
                {
                    if (dgvLuong.Columns.Contains(col))
                    {
                        dgvLuong.Columns[col]
                            .DefaultCellStyle.Format = "N0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tính lương:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========================================
        // 7. THONG KE TONG HOP
        // ========================================

        private void btnTongHop_Click(
            object sender, EventArgs e)
        {
            if (dtDen.Value.Date < dtTu.Value.Date)
            {
                MessageBox.Show(
                    "Đến ngày không được trước từ ngày.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dgvTongHop.DataSource = svc.TongHop(
                    dtTu.Value.Date,
                    dtDen.Value.Date);

                if (dgvTongHop.Columns.Contains("GiaTri"))
                {
                    dgvTongHop.Columns["GiaTri"]
                        .DefaultCellStyle.Format = "N2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thống kê:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========================================
        // 8. DONG FORM
        // ========================================

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}
