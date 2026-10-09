
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmPhanCongHDV : Form
    {
        private readonly PhanCongService svc =
            new PhanCongService();

        private TextBox txtMaPC;
        private ComboBox cboHDV, cboLoai, cboDoiTuong;
        private NumericUpDown numThuLao;
        private DataGridView dgv;
        private Button btnPhanCong, btnDong;

        public FrmPhanCongHDV()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmPhanCongHDV_Load;
        }

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Phân công hướng dẫn viên";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1120, 680);
            MinimumSize = new Size(920, 570);
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
                new RowStyle(SizeType.Absolute, 150));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            Controls.Add(layout);

            Label title = new Label
            {
                Text = "PHÂN CÔNG HƯỚNG DẪN VIÊN",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(
                    "Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95)
            };

            layout.Controls.Add(title, 0, 0);

            FlowLayoutPanel panelNhap =
                new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    WrapContents = true,
                    AutoScroll = true,
                    Padding = new Padding(12),
                    Margin = new Padding(0, 0, 0, 10),
                    BackColor =
                        Color.FromArgb(240, 245, 250),
                    BorderStyle = BorderStyle.FixedSingle
                };

            txtMaPC = TaoText(
                panelNhap, "Mã phân công", 160);

            cboHDV = TaoCombo(
                panelNhap, "Hướng dẫn viên", 260);

            cboLoai = TaoCombo(
                panelNhap, "Loại đối tượng", 170);

            cboDoiTuong = TaoCombo(
                panelNhap, "Chuyến / đoàn", 310);

            numThuLao = TaoSo(
                panelNhap, "Thù lao tour", 200);

            layout.Controls.Add(panelNhap, 0, 1);

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
                Margin = new Padding(0)
            };

            dgv.ColumnHeadersHeight = 40;
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

            layout.Controls.Add(dgv, 0, 2);

            FlowLayoutPanel footer =
                new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection =
                        FlowDirection.RightToLeft,
                    WrapContents = false,
                    Padding = new Padding(0, 8, 0, 0),
                    Margin = new Padding(0)
                };

            btnDong = TaoNut(
                "Đóng", Color.Firebrick, 150);

            btnPhanCong = TaoNut(
                "Phân công",
                Color.FromArgb(35, 95, 155),
                180);

            footer.Controls.Add(btnDong);
            footer.Controls.Add(btnPhanCong);

            layout.Controls.Add(footer, 0, 3);

            cboLoai.SelectedIndexChanged +=
                cboLoai_SelectedIndexChanged;

            btnPhanCong.Click += btnPhanCong_Click;
            btnDong.Click += btnDong_Click;

            ResumeLayout(true);
        }

        private Panel TaoNhom(
            FlowLayoutPanel panel, string ten, int width)
        {
            Panel group = new Panel
            {
                Width = width + 12,
                Height = 68,
                Margin = new Padding(6)
            };

            Label lbl = new Label
            {
                Text = ten,
                Location = new Point(0, 0),
                Size = new Size(width, 25),
                Font = new Font("Segoe UI", 9)
            };

            group.Controls.Add(lbl);
            panel.Controls.Add(group);
            return group;
        }

        private TextBox TaoText(
            FlowLayoutPanel panel, string ten, int width)
        {
            Panel group = TaoNhom(panel, ten, width);

            TextBox txt = new TextBox
            {
                Location = new Point(0, 30),
                Width = width
            };

            group.Controls.Add(txt);
            return txt;
        }

        private ComboBox TaoCombo(
            FlowLayoutPanel panel, string ten, int width)
        {
            Panel group = TaoNhom(panel, ten, width);

            ComboBox cbo = new ComboBox
            {
                Location = new Point(0, 30),
                Width = width,
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

            group.Controls.Add(cbo);
            return cbo;
        }

        private NumericUpDown TaoSo(
            FlowLayoutPanel panel, string ten, int width)
        {
            Panel group = TaoNhom(panel, ten, width);

            NumericUpDown num = new NumericUpDown
            {
                Location = new Point(0, 30),
                Width = width,
                Minimum = 0,
                Maximum = 1000000000,
                Increment = 100000,
                ThousandsSeparator = true,
                Value = 500000
            };

            group.Controls.Add(num);
            return num;
        }

        private Button TaoNut(
            string ten, Color mau, int width)
        {
            Button btn = new Button
            {
                Text = ten,
                Size = new Size(width, 42),
                BackColor = mau,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 3, 8, 3)
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void FrmPhanCongHDV_Load(
            object sender, EventArgs e)
        {
            try
            {
                FormHelper.Nap(
                    cboHDV,
                    svc.LayHDVDangLam(),
                    "HienThi",
                    "MaHDV");

                cboLoai.Items.AddRange(
                    new object[]
                    {
                        QuyDinh.Le,
                        QuyDinh.Doan
                    });

                cboLoai.SelectedIndex = 0;
                Tai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu phân công:\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Tai()
        {
            dgv.DataSource = svc.LayDanhSach();

            if (dgv.Columns.Contains("NgayBatDau"))
            {
                dgv.Columns["NgayBatDau"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy";
            }

            if (dgv.Columns.Contains("NgayKetThuc"))
            {
                dgv.Columns["NgayKetThuc"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy";
            }

            if (dgv.Columns.Contains("ThuLaoTour"))
            {
                dgv.Columns["ThuLaoTour"]
                    .DefaultCellStyle.Format = "N0";
            }
        }

        private void cboLoai_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            try
            {
                FormHelper.Nap(
                    cboDoiTuong,
                    svc.LayDoiTuong(cboLoai.Text),
                    "HienThi",
                    "Ma");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải chuyến / đoàn:\n" +
                    ex.Message);
            }
        }

        private void btnPhanCong_Click(
            object sender, EventArgs e)
        {
            if (!(cboHDV.SelectedValue is string))
            {
                MessageBox.Show(
                    "Vui lòng chọn hướng dẫn viên.");
                return;
            }

            if (!(cboDoiTuong.SelectedValue is string))
            {
                MessageBox.Show(
                    "Vui lòng chọn chuyến hoặc đoàn.");
                return;
            }

            if (FormHelper.Bao(
                svc.PhanCong(
                    txtMaPC.Text,
                    FormHelper.Gia(cboHDV),
                    cboLoai.Text,
                    FormHelper.Gia(cboDoiTuong),
                    numThuLao.Value)))
            {
                Tai();

                cboLoai_SelectedIndexChanged(
                    sender, e);

                txtMaPC.Clear();
                txtMaPC.Focus();
            }
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}
