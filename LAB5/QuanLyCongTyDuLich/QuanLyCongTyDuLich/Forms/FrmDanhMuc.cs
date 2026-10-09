
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService svc =
            new DanhMucService();

        private TabControl tabDanhMuc;

        private DataGridView dgvPT;
        private DataGridView dgvDB;
        private DataGridView dgvHDV;
        private DataGridView dgvDTQ;

        private TextBox txtPTMa;
        private TextBox txtPTTen;
        private TextBox txtPTGhiChu;

        private TextBox txtDBMa;
        private TextBox txtDBTen;
        private TextBox txtDBDiaChi;
        private TextBox txtDBDT;

        private TextBox txtHDVMa;
        private TextBox txtHDVTen;
        private TextBox txtHDVDT;
        private NumericUpDown numLuong;

        private TextBox txtDTQMa;
        private TextBox txtDTQTen;
        private TextBox txtDTQDiaDiem;
        private TextBox txtDTQNoiDung;
        private TextBox txtDTQYNghia;

        private Button btnThemPT;
        private Button btnThemDB;
        private Button btnThemHDV;
        private Button btnThemDTQ;
        private Button btnDong;

        public FrmDanhMuc()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmDanhMuc_Load;
        }

        // =========================================
        // 1. THIET KE GIAO DIEN CHINH
        // =========================================

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Quản lý danh mục";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1100, 650);
            MinimumSize = new Size(900, 560);
            BackColor = Color.FromArgb(245, 248, 252);
            Font = new Font("Segoe UI", 10);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(15)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60));

            Controls.Add(layout);

            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ DANH MỤC",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(
                    "Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95)
            };

            layout.Controls.Add(lblTitle, 0, 0);

            tabDanhMuc = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(0)
            };

            layout.Controls.Add(tabDanhMuc, 0, 1);

            TaoTabPhuongTien();
            TaoTabDiemBan();
            TaoTabHDV();
            TaoTabDiemThamQuan();

            // Khu vuc nut Dong
            Panel footer = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

            btnDong = TaoNut("Đóng");
            btnDong.Size = new Size(150, 42);
            btnDong.BackColor = Color.Firebrick;
            btnDong.Margin = new Padding(0);
            btnDong.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;

            footer.Controls.Add(btnDong);

            footer.Resize += (s, e) =>
            {
                btnDong.Location = new Point(
                    footer.ClientSize.Width -
                        btnDong.Width - 8,
                    8);
            };

            btnDong.Click += btnDong_Click;

            layout.Controls.Add(footer, 0, 2);

            ResumeLayout(true);
        }

        // =========================================
        // 2. TAO TAB VA DATAGRIDVIEW
        // =========================================

        private void TaoTab(
            string ten,
            out DataGridView dgv,
            out FlowLayoutPanel khuNhap)
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
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            body.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent, 100));

            body.RowStyles.Add(
                new RowStyle(
                    SizeType.Percent, 100));

            // Khu nhap lieu gon hon
            body.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute, 155));

            dgv = new DataGridView
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
                Margin = new Padding(0, 0, 0, 10)
            };

            dgv.ColumnHeadersHeight = 38;
            dgv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode
                    .DisableResizing;

            dgv.RowTemplate.Height = 32;

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

            khuNhap = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection =
                    FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10),
                Margin = new Padding(0),
                BackColor =
                    Color.FromArgb(240, 245, 250),
                BorderStyle = BorderStyle.FixedSingle
            };

            body.Controls.Add(dgv, 0, 0);
            body.Controls.Add(khuNhap, 0, 1);

            page.Controls.Add(body);
            tabDanhMuc.TabPages.Add(page);
        }

        // =========================================
        // 3. CAC CONTROL DUNG CHUNG
        // =========================================

        private TextBox TaoO(
            FlowLayoutPanel panel,
            string nhan,
            int width = 180)
        {
            Panel group = new Panel
            {
                Width = width + 12,
                Height = 68,
                Margin = new Padding(6)
            };

            Label lbl = new Label
            {
                Text = nhan,
                Location = new Point(0, 0),
                Size = new Size(width, 25),
                Font = new Font("Segoe UI", 9)
            };

            TextBox txt = new TextBox
            {
                Width = width,
                Location = new Point(0, 30),
                Font = new Font("Segoe UI", 10)
            };

            group.Controls.Add(lbl);
            group.Controls.Add(txt);

            panel.Controls.Add(group);

            return txt;
        }

        private NumericUpDown TaoSo(
            FlowLayoutPanel panel,
            string nhan)
        {
            Panel group = new Panel
            {
                Width = 195,
                Height = 68,
                Margin = new Padding(6)
            };

            Label lbl = new Label
            {
                Text = nhan,
                Location = new Point(0, 0),
                Size = new Size(185, 25)
            };

            NumericUpDown num = new NumericUpDown
            {
                Width = 180,
                Location = new Point(0, 30),
                Minimum = 0,
                Maximum = 1000000000,
                Increment = 500000,
                DecimalPlaces = 0,
                ThousandsSeparator = true,
                Font = new Font("Segoe UI", 10)
            };

            group.Controls.Add(lbl);
            group.Controls.Add(num);

            panel.Controls.Add(group);

            return num;
        }

        private Button TaoNut(string text)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(185, 42),
                BackColor =
                    Color.FromArgb(35, 95, 155),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 18, 8, 8),
                UseVisualStyleBackColor = false
            };

            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        // =========================================
        // 4. TAB PHUONG TIEN
        // =========================================

        private void TaoTabPhuongTien()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Phương tiện",
                out dgvPT,
                out panel);

            txtPTMa = TaoO(
                panel, "Mã phương tiện", 170);

            txtPTTen = TaoO(
                panel, "Tên phương tiện", 220);

            txtPTGhiChu = TaoO(
                panel, "Ghi chú", 280);

            btnThemPT = TaoNut("Thêm phương tiện");

            btnThemPT.Click += btnThemPT_Click;

            panel.Controls.Add(btnThemPT);
        }

        // =========================================
        // 5. TAB DIEM BAN VE
        // =========================================

        private void TaoTabDiemBan()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Điểm bán vé",
                out dgvDB,
                out panel);

            txtDBMa = TaoO(
                panel, "Mã điểm bán", 145);

            txtDBTen = TaoO(
                panel, "Tên điểm bán", 190);

            txtDBDiaChi = TaoO(
                panel, "Địa chỉ", 250);

            txtDBDT = TaoO(
                panel, "Điện thoại", 145);

            btnThemDB = TaoNut("Thêm điểm bán");

            btnThemDB.Click += btnThemDB_Click;

            panel.Controls.Add(btnThemDB);
        }

        // =========================================
        // 6. TAB HUONG DAN VIEN
        // =========================================

        private void TaoTabHDV()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Hướng dẫn viên",
                out dgvHDV,
                out panel);

            txtHDVMa = TaoO(
                panel, "Mã HDV", 135);

            txtHDVTen = TaoO(
                panel, "Họ tên", 210);

            txtHDVDT = TaoO(
                panel, "Điện thoại", 145);

            numLuong = TaoSo(
                panel, "Lương căn bản");

            btnThemHDV = TaoNut("Thêm HDV");

            btnThemHDV.Click += btnThemHDV_Click;

            panel.Controls.Add(btnThemHDV);
        }

        // =========================================
        // 7. TAB DIEM THAM QUAN
        // =========================================

        private void TaoTabDiemThamQuan()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Điểm tham quan",
                out dgvDTQ,
                out panel);

            txtDTQMa = TaoO(
                panel, "Mã điểm tham quan", 155);

            txtDTQTen = TaoO(
                panel, "Tên điểm tham quan", 185);

            txtDTQDiaDiem = TaoO(
                panel, "Địa điểm", 175);

            txtDTQNoiDung = TaoO(
                panel, "Nội dung", 215);

            txtDTQYNghia = TaoO(
                panel, "Ý nghĩa", 215);

            btnThemDTQ = TaoNut("Thêm điểm TQ");

            btnThemDTQ.Click += btnThemDTQ_Click;

            panel.Controls.Add(btnThemDTQ);
        }

        // =========================================
        // 8. TAI DU LIEU SQL SERVER
        // =========================================

        private void FrmDanhMuc_Load(
            object sender, EventArgs e)
        {
            try
            {
                Tai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu danh mục:\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Tai()
        {
            dgvPT.DataSource =
                svc.LayPhuongTien();

            dgvDB.DataSource =
                svc.LayDiemBan();

            dgvHDV.DataSource =
                svc.LayHDV();

            dgvDTQ.DataSource =
                svc.LayDiemThamQuan();
        }

        // =========================================
        // 9. THEM PHUONG TIEN
        // =========================================

        private void btnThemPT_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemPhuongTien(
                    txtPTMa.Text,
                    txtPTTen.Text,
                    txtPTGhiChu.Text)))
            {
                Tai();

                txtPTMa.Clear();
                txtPTTen.Clear();
                txtPTGhiChu.Clear();

                txtPTMa.Focus();
            }
        }

        // =========================================
        // 10. THEM DIEM BAN VE
        // =========================================

        private void btnThemDB_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemDiemBan(
                    txtDBMa.Text,
                    txtDBTen.Text,
                    txtDBDiaChi.Text,
                    txtDBDT.Text)))
            {
                Tai();

                txtDBMa.Clear();
                txtDBTen.Clear();
                txtDBDiaChi.Clear();
                txtDBDT.Clear();

                txtDBMa.Focus();
            }
        }

        // =========================================
        // 11. THEM HUONG DAN VIEN
        // =========================================

        private void btnThemHDV_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemHDV(
                    txtHDVMa.Text,
                    txtHDVTen.Text,
                    txtHDVDT.Text,
                    numLuong.Value)))
            {
                Tai();

                txtHDVMa.Clear();
                txtHDVTen.Clear();
                txtHDVDT.Clear();
                numLuong.Value = 0;

                txtHDVMa.Focus();
            }
        }

        // =========================================
        // 12. THEM DIEM THAM QUAN
        // =========================================

        private void btnThemDTQ_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemDiemThamQuan(
                    txtDTQMa.Text,
                    txtDTQTen.Text,
                    txtDTQDiaDiem.Text,
                    txtDTQNoiDung.Text,
                    txtDTQYNghia.Text)))
            {
                Tai();

                txtDTQMa.Clear();
                txtDTQTen.Clear();
                txtDTQDiaDiem.Clear();
                txtDTQNoiDung.Clear();
                txtDTQYNghia.Clear();

                txtDTQMa.Focus();
            }
        }

        // =========================================
        // 13. DONG FORM
        // =========================================

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}
