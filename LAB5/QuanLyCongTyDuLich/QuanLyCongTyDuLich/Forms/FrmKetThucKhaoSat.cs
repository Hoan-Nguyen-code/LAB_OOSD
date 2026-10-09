
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmKetThucKhaoSat : Form
    {
        private readonly KetThucService svc =
            new KetThucService();

        private TabControl tabKT;
        private DataGridView dgvDoan, dgvKS;

        private TextBox txtSoTT, txtSoDK, txtGhiChu;
        private DateTimePicker dtTT;
        private NumericUpDown numTien;
        private Button btnThanhToan;

        private ComboBox cboLoaiKS, cboDangKy;
        private TextBox txtMaKS, txtKSChon, txtGopY;
        private DateTimePicker dtGui, dtPH;
        private NumericUpDown numDiem;
        private Button btnGui, btnGhiPH, btnDong;

        public FrmKetThucKhaoSat()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmKetThucKhaoSat_Load;
        }

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Kết thúc tour - Khảo sát khách hàng";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1200, 760);
            MinimumSize = new Size(980, 640);
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
                Text = "KẾT THÚC TOUR - KHẢO SÁT KHÁCH HÀNG",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(25, 55, 95),
                Font = new Font(
                    "Segoe UI", 18, FontStyle.Bold)
            };

            main.Controls.Add(title, 0, 0);

            tabKT = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(0)
            };

            main.Controls.Add(tabKT, 0, 1);

            TaoTabThanhToan();
            TaoTabKhaoSat();

            Panel footer = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

            btnDong = TaoNut("Đóng", 150);
            btnDong.BackColor = Color.Firebrick;
            btnDong.Margin = new Padding(0);
            btnDong.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;

            footer.Controls.Add(btnDong);

            footer.Resize += (s, e) =>
            {
                btnDong.Location = new Point(
                    Math.Max(0,
                        footer.ClientSize.Width -
                        btnDong.Width - 8), 8);
            };

            btnDong.Click += (s, e) => Close();
            main.Controls.Add(footer, 0, 2);

            ResumeLayout(true);
        }

        private TableLayoutPanel TaoTab(
            string ten, int soHang)
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
                RowCount = soHang
            };

            body.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            page.Controls.Add(body);
            tabKT.TabPages.Add(page);

            return body;
        }

        private FlowLayoutPanel TaoPanelNhap()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 8),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(240, 245, 250)
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

        private TextBox TaoText(
            FlowLayoutPanel panel,
            string ten, int width, bool readOnly = false)
        {
            Panel p = TaoNhom(panel, ten, width);

            TextBox txt = new TextBox
            {
                Location = new Point(0, 30),
                Width = width,
                ReadOnly = readOnly
            };

            p.Controls.Add(txt);
            return txt;
        }

        private ComboBox TaoCombo(
            FlowLayoutPanel panel, string ten, int width)
        {
            Panel p = TaoNhom(panel, ten, width);

            ComboBox cbo = new ComboBox
            {
                Location = new Point(0, 30),
                Width = width,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            p.Controls.Add(cbo);
            return cbo;
        }

        private DateTimePicker TaoNgay(
            FlowLayoutPanel panel, string ten)
        {
            Panel p = TaoNhom(panel, ten, 170);

            DateTimePicker dt = new DateTimePicker
            {
                Location = new Point(0, 30),
                Width = 170,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };

            p.Controls.Add(dt);
            return dt;
        }

        private NumericUpDown TaoSo(
            FlowLayoutPanel panel,
            string ten, decimal min, decimal max,
            decimal value, int width)
        {
            Panel p = TaoNhom(panel, ten, width);

            NumericUpDown num = new NumericUpDown
            {
                Location = new Point(0, 30),
                Width = width,
                Minimum = min,
                Maximum = max,
                Value = value,
                ThousandsSeparator = true
            };

            p.Controls.Add(num);
            return num;
        }

        private Button TaoNut(string text, int width)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(width, 42),
                BackColor = Color.FromArgb(35, 95, 155),
                ForeColor = Color.White,
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 5, 8, 5)
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
                    DataGridViewAutoSizeColumnsMode.DisplayedCells,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                Margin = new Padding(0)
            };

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 38;
            dgv.RowTemplate.Height = 30;
            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 95, 155);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            return dgv;
        }

        private void TaoTabThanhToan()
        {
            TableLayoutPanel body =
                TaoTab("Thanh toán sau tour (đoàn)", 3);

            body.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            body.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 155));
            body.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 57));

            dgvDoan = TaoBang();
            body.Controls.Add(dgvDoan, 0, 0);

            FlowLayoutPanel p = TaoPanelNhap();

            txtSoTT = TaoText(p, "Số thanh toán", 160);
            txtSoDK = TaoText(p, "Phiếu đoàn", 160, true);
            dtTT = TaoNgay(p, "Ngày thanh toán");
            numTien = TaoSo(
                p, "Số tiền thanh toán",
                0, 100000000000, 0, 200);
            txtGhiChu = TaoText(p, "Ghi chú", 250);

            body.Controls.Add(p, 0, 1);

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };

            btnThanhToan = TaoNut(
                "Ghi nhận thanh toán", 235);
            actions.Controls.Add(btnThanhToan);
            body.Controls.Add(actions, 0, 2);

            dgvDoan.SelectionChanged +=
                dgvDoan_SelectionChanged;
            btnThanhToan.Click += btnThanhToan_Click;
        }

        private void TaoTabKhaoSat()
        {
            TableLayoutPanel body =
                TaoTab("Khảo sát khách hàng", 4);

            body.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 155));
            body.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            body.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 145));
            body.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 57));

            FlowLayoutPanel pGui = TaoPanelNhap();

            cboLoaiKS = TaoCombo(
                pGui, "Loại khách", 150);
            cboDangKy = TaoCombo(
                pGui, "Đăng ký đã kết thúc", 315);
            txtMaKS = TaoText(
                pGui, "Mã khảo sát", 155);
            dtGui = TaoNgay(pGui, "Ngày gửi");

            btnGui = TaoNut("Gửi phiếu khảo sát", 205);
            btnGui.Margin = new Padding(10, 23, 8, 5);
            pGui.Controls.Add(btnGui);

            body.Controls.Add(pGui, 0, 0);

            dgvKS = TaoBang();
            body.Controls.Add(dgvKS, 0, 1);

            FlowLayoutPanel pPH = TaoPanelNhap();

            txtKSChon = TaoText(
                pPH, "Phiếu khảo sát đã chọn", 180, true);
            dtPH = TaoNgay(pPH, "Ngày phản hồi");
            numDiem = TaoSo(
                pPH, "Điểm đánh giá (1-5)",
                1, 5, 5, 165);
            txtGopY = TaoText(
                pPH, "Góp ý khách hàng", 360);

            body.Controls.Add(pPH, 0, 2);

            FlowLayoutPanel actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 5, 0, 0)
            };

            btnGhiPH = TaoNut("Ghi nhận góp ý", 200);
            actions.Controls.Add(btnGhiPH);
            body.Controls.Add(actions, 0, 3);

            cboLoaiKS.SelectedIndexChanged +=
                cboLoaiKS_SelectedIndexChanged;
            btnGui.Click += btnGui_Click;
            dgvKS.SelectionChanged +=
                dgvKS_SelectionChanged;
            btnGhiPH.Click += btnGhiPH_Click;
        }

        private void FrmKetThucKhaoSat_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboLoaiKS.Items.AddRange(
                    new object[] { QuyDinh.Le, QuyDinh.Doan });
                cboLoaiKS.SelectedIndex = 0;

                dtTT.Value = DateTime.Today;
                dtGui.Value = DateTime.Today;
                dtPH.Value = DateTime.Today;

                TaiThanhToan();
                TaiKhaoSat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TaiThanhToan()
        {
            dgvDoan.DataSource = svc.DoanCanThanhToan();

            if (dgvDoan.Columns.Contains(
                "NgayKetThucDuKien"))
                dgvDoan.Columns["NgayKetThucDuKien"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

            foreach (string col in new string[]
            {
                "TongTienDuKien", "TienCoc",
                "DaTraSauTour", "ConLai"
            })
            {
                if (dgvDoan.Columns.Contains(col))
                    dgvDoan.Columns[col]
                        .DefaultCellStyle.Format = "N0";
            }
        }

        private void TaiKhaoSat()
        {
            dgvKS.DataSource = svc.LayKhaoSat();

            foreach (string col in new string[]
            {
                "NgayGui", "NgayPhanHoi"
            })
            {
                if (dgvKS.Columns.Contains(col))
                    dgvKS.Columns[col]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void dgvDoan_SelectionChanged(
            object sender, EventArgs e)
        {
            txtSoDK.Text = FormHelper.O(
                dgvDoan, "SoDKDoan");

            string con = FormHelper.O(dgvDoan, "ConLai");
            decimal soTien;

            if (decimal.TryParse(con, out soTien))
            {
                numTien.Value = Math.Max(
                    numTien.Minimum,
                    Math.Min(numTien.Maximum, soTien));
            }
            else
            {
                numTien.Value = 0;
            }
        }

        private void btnThanhToan_Click(
            object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoDK.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu đoàn.");
                return;
            }

            if (MessageBox.Show(
                "Ghi nhận đã nhận " +
                numTien.Value.ToString("N0") +
                " đ cho phiếu " + txtSoDK.Text + "?",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            if (FormHelper.Bao(svc.ThanhToanDoan(
                txtSoTT.Text,
                txtSoDK.Text,
                dtTT.Value,
                numTien.Value,
                txtGhiChu.Text)))
            {
                TaiThanhToan();
                txtSoTT.Clear();
                txtGhiChu.Clear();
            }
        }

        private void cboLoaiKS_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            try
            {
                FormHelper.Nap(
                    cboDangKy,
                    svc.LayDangKyChoKhaoSat(
                        cboLoaiKS.Text),
                    "HienThi",
                    "Ma");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải đăng ký đủ điều kiện:\n" +
                    ex.Message);
            }
        }

        private void btnGui_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.GuiKhaoSat(
                txtMaKS.Text,
                cboLoaiKS.Text,
                FormHelper.Gia(cboDangKy),
                dtGui.Value)))
            {
                TaiKhaoSat();
                cboLoaiKS_SelectedIndexChanged(sender, e);
                txtMaKS.Clear();
            }
        }

        private void dgvKS_SelectionChanged(
            object sender, EventArgs e)
        {
            txtKSChon.Text = FormHelper.O(
                dgvKS, "MaKhaoSat");
        }

        private void btnGhiPH_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.GhiPhanHoi(
                txtKSChon.Text,
                dtPH.Value,
                (int)numDiem.Value,
                txtGopY.Text)))
            {
                TaiKhaoSat();
                txtGopY.Clear();
            }
        }
    }
}
