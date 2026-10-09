
using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmTour : Form
    {
        private readonly TourService svc = new TourService();
        private readonly DanhMucService dm = new DanhMucService();

        private ComboBox cboTour, cboPT, cboDTQ;
        private TabControl tabTour;

        private DataGridView dgvTour;
        private DataGridView dgvDiemDung;
        private DataGridView dgvChang;
        private DataGridView dgvTQ;

        private TextBox txtMa, txtTen, txtMoTa;
        private NumericUpDown numNgay, numDem, numGia;

        private NumericUpDown numThuTu, numSao;
        private TextBox txtDiemDung, txtGhiChuDD;
        private CheckBox chkDoiPT, chkAn, chkKS;

        private NumericUpDown numChang;
        private TextBox txtGhiChuPT;

        private NumericUpDown numThuTuTQ;

        private Button btnThemTour;
        private Button btnThemDD;
        private Button btnThemChang;
        private Button btnThemTQ;
        private Button btnDong;

        public FrmTour()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmTour_Load;
        }

        // =========================================
        // 1. THIET KE GIAO DIEN CHINH
        // =========================================

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Quản lý Tour - Hành trình";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1180, 700);
            MinimumSize = new Size(950, 600);
            BackColor = Color.FromArgb(245, 248, 252);
            Font = new Font("Segoe UI", 10);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(15)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60));

            Controls.Add(layout);

            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ TOUR - HÀNH TRÌNH",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(
                    "Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95)
            };

            layout.Controls.Add(lblTitle, 0, 0);

            // Khu vuc chon tour
            Panel panelChonTour = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 5)
            };

            Label lblChonTour = new Label
            {
                Text = "Tour đang chọn:",
                Location = new Point(12, 12),
                Size = new Size(145, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            cboTour = new ComboBox
            {
                Location = new Point(160, 10),
                Width = 490,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };

            cboTour.SelectedIndexChanged +=
                cboTour_SelectedIndexChanged;

            panelChonTour.Controls.Add(lblChonTour);
            panelChonTour.Controls.Add(cboTour);

            layout.Controls.Add(panelChonTour, 0, 1);

            // TabControl
            tabTour = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(0)
            };

            layout.Controls.Add(tabTour, 0, 2);

            TaoTabTour();
            TaoTabDiemDung();
            TaoTabPhuongTien();
            TaoTabThamQuan();

            // Nút đóng
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
                    Math.Max(0, footer.ClientSize.Width
                        - btnDong.Width - 8),
                    8);
            };

            btnDong.Click += btnDong_Click;

            layout.Controls.Add(footer, 0, 3);

            ResumeLayout(true);
        }

        // =========================================
        // 2. TAO TAB CHUNG
        // =========================================

        private void TaoTab(
            string ten,
            int chieuCaoNhap,
            out DataGridView dgv,
            out FlowLayoutPanel panelNhap)
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
                new ColumnStyle(SizeType.Percent, 100));

            body.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            body.RowStyles.Add(
                new RowStyle(
                    SizeType.Absolute, chieuCaoNhap));

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
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);

            panelNhap = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10),
                Margin = new Padding(0),
                BackColor = Color.FromArgb(240, 245, 250),
                BorderStyle = BorderStyle.FixedSingle
            };

            body.Controls.Add(dgv, 0, 0);
            body.Controls.Add(panelNhap, 0, 1);

            page.Controls.Add(body);
            tabTour.TabPages.Add(page);
        }

        // =========================================
        // 3. CONTROL DUNG CHUNG
        // =========================================

        private TextBox TaoText(
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
                Size = new Size(width, 25)
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
            string nhan,
            decimal min,
            decimal max,
            decimal giaTri,
            int width = 140)
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
                Size = new Size(width, 25)
            };

            NumericUpDown num = new NumericUpDown
            {
                Width = width,
                Location = new Point(0, 30),
                Minimum = min,
                Maximum = max,
                Value = giaTri,
                DecimalPlaces = 0,
                ThousandsSeparator = true,
                Font = new Font("Segoe UI", 10)
            };

            group.Controls.Add(lbl);
            group.Controls.Add(num);
            panel.Controls.Add(group);

            return num;
        }

        private ComboBox TaoCombo(
            FlowLayoutPanel panel,
            string nhan,
            int width = 220)
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
                Size = new Size(width, 25)
            };

            ComboBox cbo = new ComboBox
            {
                Width = width,
                Location = new Point(0, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };

            group.Controls.Add(lbl);
            group.Controls.Add(cbo);
            panel.Controls.Add(group);

            return cbo;
        }

        private CheckBox TaoCheck(
            FlowLayoutPanel panel,
            string noiDung)
        {
            CheckBox chk = new CheckBox
            {
                Text = noiDung,
                AutoSize = true,
                Margin = new Padding(10, 32, 10, 8),
                Font = new Font("Segoe UI", 10)
            };

            panel.Controls.Add(chk);

            return chk;
        }

        private Button TaoNut(string text)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(185, 42),
                BackColor = Color.FromArgb(35, 95, 155),
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
        // 4. TAB TOUR
        // =========================================

        private void TaoTabTour()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Tour",
                155,
                out dgvTour,
                out panel);

            txtMa = TaoText(panel, "Mã tour", 130);
            txtTen = TaoText(panel, "Tên tour", 225);

            numNgay = TaoSo(
                panel, "Số ngày", 1, 365, 3, 110);

            numDem = TaoSo(
                panel, "Số đêm", 0, 365, 2, 110);

            numGia = TaoSo(
                panel, "Đơn giá / khách",
                0, 1000000000, 2500000, 175);

            txtMoTa = TaoText(
                panel, "Mô tả", 285);

            btnThemTour = TaoNut("Thêm tour");
            btnThemTour.Click += btnThemTour_Click;
            panel.Controls.Add(btnThemTour);
        }

        // =========================================
        // 5. TAB DIEM DUNG
        // =========================================

        private void TaoTabDiemDung()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Điểm dừng",
                165,
                out dgvDiemDung,
                out panel);

            numThuTu = TaoSo(
                panel, "Thứ tự", 1, 999, 1, 100);

            txtDiemDung = TaoText(
                panel, "Tên điểm dừng", 210);

            chkDoiPT = TaoCheck(
                panel, "Đổi phương tiện");

            chkAn = TaoCheck(
                panel, "Có nơi ăn");

            chkKS = TaoCheck(
                panel, "Có khách sạn");

            numSao = TaoSo(
                panel, "Hạng sao", 2, 5, 3, 105);

            numSao.Enabled = false;

            chkKS.CheckedChanged +=
                chkKS_CheckedChanged;

            txtGhiChuDD = TaoText(
                panel, "Ghi chú", 230);

            btnThemDD = TaoNut("Thêm điểm dừng");
            btnThemDD.Click += btnThemDD_Click;

            panel.Controls.Add(btnThemDD);
        }

        // =========================================
        // 6. TAB PHUONG TIEN THEO CHANG
        // =========================================

        private void TaoTabPhuongTien()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Phương tiện theo chặng",
                145,
                out dgvChang,
                out panel);

            numChang = TaoSo(
                panel, "Chặng thứ", 1, 999, 1, 120);

            cboPT = TaoCombo(
                panel, "Phương tiện", 270);

            txtGhiChuPT = TaoText(
                panel, "Ghi chú", 290);

            btnThemChang = TaoNut("Gắn phương tiện");
            btnThemChang.Click += btnThemChang_Click;

            panel.Controls.Add(btnThemChang);
        }

        // =========================================
        // 7. TAB DIEM THAM QUAN
        // =========================================

        private void TaoTabThamQuan()
        {
            FlowLayoutPanel panel;

            TaoTab(
                "Điểm tham quan",
                145,
                out dgvTQ,
                out panel);

            cboDTQ = TaoCombo(
                panel, "Điểm tham quan", 340);

            numThuTuTQ = TaoSo(
                panel, "Thứ tự", 1, 999, 1, 120);

            btnThemTQ = TaoNut("Gắn điểm TQ");
            btnThemTQ.Click += btnThemTQ_Click;

            panel.Controls.Add(btnThemTQ);
        }

        // =========================================
        // 8. LOAD DU LIEU
        // =========================================

        private void FrmTour_Load(
            object sender, EventArgs e)
        {
            try
            {
                FormHelper.Nap(
                    cboPT,
                    dm.LayPhuongTien(),
                    "TenPT",
                    "MaPT");

                FormHelper.Nap(
                    cboDTQ,
                    dm.LayDiemThamQuan(),
                    "TenDiemTQ",
                    "MaDiemTQ");

                TaiTour();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải tour:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TaiTour()
        {
            string maCu =
                cboTour.SelectedValue is string
                    ? cboTour.SelectedValue.ToString()
                    : "";

            dgvTour.DataSource = svc.LayTour();

            cboTour.SelectedIndexChanged -=
                cboTour_SelectedIndexChanged;

            DataTable dt = svc.LayTour();

            FormHelper.Nap(
                cboTour,
                dt,
                "TenTour",
                "MaTour");

            if (!string.IsNullOrWhiteSpace(maCu) &&
                dt.Rows.Find(maCu) != null)
            {
                cboTour.SelectedValue = maCu;
            }

            cboTour.SelectedIndexChanged +=
                cboTour_SelectedIndexChanged;

            TaiChiTiet();
        }

        private void TaiChiTiet()
        {
            if (!(cboTour.SelectedValue is string))
                return;

            string maTour =
                cboTour.SelectedValue.ToString();

            dgvDiemDung.DataSource =
                svc.LayDiemDung(maTour);

            dgvChang.DataSource =
                svc.LayChang(maTour);

            dgvTQ.DataSource =
                svc.LayDiemTQTour(maTour);
        }

        private void cboTour_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            TaiChiTiet();
        }

        private void chkKS_CheckedChanged(
            object sender, EventArgs e)
        {
            numSao.Enabled = chkKS.Checked;
        }

        // =========================================
        // 9. THEM TOUR
        // =========================================

        private void btnThemTour_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemTour(
                    txtMa.Text,
                    txtTen.Text,
                    (int)numNgay.Value,
                    (int)numDem.Value,
                    numGia.Value,
                    txtMoTa.Text)))
            {
                string maMoi = txtMa.Text.Trim();

                TaiTour();

                cboTour.SelectedValue = maMoi;

                txtMa.Clear();
                txtTen.Clear();
                txtMoTa.Clear();

                txtMa.Focus();
            }
        }

        // =========================================
        // 10. THEM DIEM DUNG
        // =========================================

        private void btnThemDD_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemDiemDung(
                    FormHelper.Gia(cboTour),
                    (int)numThuTu.Value,
                    txtDiemDung.Text,
                    chkDoiPT.Checked,
                    chkAn.Checked,
                    chkKS.Checked,
                    (int)numSao.Value,
                    txtGhiChuDD.Text)))
            {
                TaiChiTiet();

                txtDiemDung.Clear();
                txtGhiChuDD.Clear();

                chkDoiPT.Checked = false;
                chkAn.Checked = false;
                chkKS.Checked = false;
            }
        }

        // =========================================
        // 11. GAN PHUONG TIEN THEO CHANG
        // =========================================

        private void btnThemChang_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemChang(
                    FormHelper.Gia(cboTour),
                    (int)numChang.Value,
                    FormHelper.Gia(cboPT),
                    txtGhiChuPT.Text)))
            {
                TaiChiTiet();
                txtGhiChuPT.Clear();
            }
        }

        // =========================================
        // 12. GAN DIEM THAM QUAN
        // =========================================

        private void btnThemTQ_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(
                svc.ThemDiemTQTour(
                    FormHelper.Gia(cboTour),
                    FormHelper.Gia(cboDTQ),
                    (int)numThuTuTQ.Value)))
            {
                TaiChiTiet();
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
