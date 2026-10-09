
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmChuyenLe : Form
    {
        private readonly ChuyenLeService svc =
            new ChuyenLeService();

        private readonly TourService tour =
            new TourService();

        private TextBox txtMa, txtDon;
        private ComboBox cboTour;
        private DateTimePicker dtDi;
        private Label lblNgayVe;
        private DataGridView dgv;
        private Button btnThem, btnDongDK, btnDong;

        public FrmChuyenLe()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmChuyenLe_Load;
        }

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Lịch chuyến khách lẻ";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1120, 680);
            MinimumSize = new Size(950, 580);
            BackColor = Color.FromArgb(245, 248, 252);
            Font = new Font("Segoe UI", 10);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(15)
            };

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 180));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            Controls.Add(layout);

            Label title = new Label
            {
                Text = "QUẢN LÝ LỊCH CHUYẾN KHÁCH LẺ",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95),
                TextAlign = ContentAlignment.MiddleCenter
            };
            layout.Controls.Add(title, 0, 0);

            FlowLayoutPanel panelNhap = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(12),
                Margin = new Padding(0, 0, 0, 10),
                BackColor = Color.FromArgb(240, 245, 250),
                BorderStyle = BorderStyle.FixedSingle
            };
            layout.Controls.Add(panelNhap, 0, 1);

            txtMa = TaoText(panelNhap, "Mã chuyến", 150);
            cboTour = TaoCombo(panelNhap, "Tour", 310);
            dtDi = TaoNgay(panelNhap, "Ngày đi");
            lblNgayVe = TaoNhan(panelNhap, "Ngày về", 155);
            txtDon = TaoText(panelNhap, "Địa điểm đón", 300);

            cboTour.SelectedIndexChanged += TinhNgayVe;
            dtDi.ValueChanged += TinhNgayVe;

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
                Margin = new Padding(0)
            };

            dgv.ColumnHeadersHeight = 40;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 95, 155);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            dgv.RowTemplate.Height = 32;
            layout.Controls.Add(dgv, 0, 2);

            FlowLayoutPanel footer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 8, 0, 0)
            };

            btnDong = TaoNut("Đóng", Color.Firebrick);
            btnDongDK = TaoNut(
                "Đóng đăng ký", Color.FromArgb(190, 110, 30));
            btnThem = TaoNut(
                "Tạo chuyến", Color.FromArgb(35, 95, 155));

            footer.Controls.Add(btnDong);
            footer.Controls.Add(btnDongDK);
            footer.Controls.Add(btnThem);
            layout.Controls.Add(footer, 0, 3);

            btnThem.Click += btnThem_Click;
            btnDongDK.Click += btnDongDK_Click;
            btnDong.Click += btnDong_Click;

            ResumeLayout(true);
        }

        private Panel TaoNhom(
            FlowLayoutPanel panel, string ten, int width)
        {
            Panel group = new Panel
            {
                Width = width + 12,
                Height = 72,
                Margin = new Padding(6)
            };

            Label lbl = new Label
            {
                Text = ten,
                Location = new Point(0, 0),
                Size = new Size(width, 25)
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
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            group.Controls.Add(cbo);
            return cbo;
        }

        private DateTimePicker TaoNgay(
            FlowLayoutPanel panel, string ten)
        {
            Panel group = TaoNhom(panel, ten, 170);

            DateTimePicker dt = new DateTimePicker
            {
                Location = new Point(0, 30),
                Width = 170,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };

            group.Controls.Add(dt);
            return dt;
        }

        private Label TaoNhan(
            FlowLayoutPanel panel, string ten, int width)
        {
            Panel group = TaoNhom(panel, ten, width);

            Label lbl = new Label
            {
                Location = new Point(0, 30),
                Size = new Size(width, 30),
                Text = "-",
                ForeColor = Color.FromArgb(25, 80, 140),
                Font = new Font(
                    "Segoe UI", 11, FontStyle.Bold)
            };

            group.Controls.Add(lbl);
            return lbl;
        }

        private Button TaoNut(string ten, Color mau)
        {
            Button btn = new Button
            {
                Text = ten,
                Size = new Size(165, 42),
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

        private void FrmChuyenLe_Load(
            object sender, EventArgs e)
        {
            try
            {
                FormHelper.Nap(
                    cboTour,
                    tour.LayTourMoBan(),
                    "HienThi",
                    "MaTour");

                dtDi.Value = DateTime.Today.AddDays(7);

                Tai();
                TinhNgayVe(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải chuyến:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Tai()
        {
            dgv.DataSource = svc.LayChuyen();

            if (dgv.Columns.Contains("NgayDi"))
                dgv.Columns["NgayDi"].DefaultCellStyle
                    .Format = "dd/MM/yyyy";

            if (dgv.Columns.Contains("NgayVe"))
                dgv.Columns["NgayVe"].DefaultCellStyle
                    .Format = "dd/MM/yyyy";
        }

        private void TinhNgayVe(
            object sender, EventArgs e)
        {
            DataRowView r =
                cboTour.SelectedItem as DataRowView;

            if (r == null)
            {
                lblNgayVe.Text = "-";
                return;
            }

            int soNgay = Convert.ToInt32(r["SoNgay"]);

            DateTime ngayVe = dtDi.Value.Date
                .AddDays(soNgay - 1);

            lblNgayVe.Text =
                ngayVe.ToString("dd/MM/yyyy");
        }

        private void btnThem_Click(
            object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemChuyen(
                txtMa.Text,
                FormHelper.Gia(cboTour),
                dtDi.Value,
                txtDon.Text)))
            {
                Tai();
                txtMa.Clear();
                txtDon.Clear();
                txtMa.Focus();
            }
        }

        private void btnDongDK_Click(
            object sender, EventArgs e)
        {
            string ma = FormHelper.O(dgv, "MaChuyen");

            if (string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Vui lòng chọn một chuyến.");
                return;
            }

            if (MessageBox.Show(
                "Đóng đăng ký chuyến " + ma + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            if (FormHelper.Bao(svc.DongDangKy(ma)))
                Tai();
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}
