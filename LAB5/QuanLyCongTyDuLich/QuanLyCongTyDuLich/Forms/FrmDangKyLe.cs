
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmDangKyLe : Form
    {
        private readonly DangKyLeService svc =
            new DangKyLeService();

        private readonly ChuyenLeService chuyenService =
            new ChuyenLeService();

        private readonly DanhMucService danhMucService =
            new DanhMucService();

        private TextBox txtSo;
        private TextBox txtTen;
        private TextBox txtDT;

        private ComboBox cboChuyen;
        private ComboBox cboDiemBan;

        private NumericUpDown numNguoi;
        private Label lblThanhTien;

        private DataGridView dgv;

        private Button btnDangKy;
        private Button btnDong;

        public FrmDangKyLe()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmDangKyLe_Load;
        }

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Đăng ký khách lẻ";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1150, 700);
            MinimumSize = new Size(950, 610);
            BackColor = Color.FromArgb(245, 248, 252);
            Font = new Font("Segoe UI", 10);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(15)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 180));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 50));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 65));

            Controls.Add(layout);

            Label title = new Label
            {
                Text = "ĐĂNG KÝ TOUR KHÁCH LẺ",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95),
                TextAlign = ContentAlignment.MiddleCenter
            };

            layout.Controls.Add(title, 0, 0);

            FlowLayoutPanel panelNhap =
                new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection =
                        FlowDirection.LeftToRight,
                    WrapContents = true,
                    AutoScroll = true,
                    Padding = new Padding(10),
                    Margin = new Padding(0, 0, 0, 8),
                    BackColor =
                        Color.FromArgb(240, 245, 250),
                    BorderStyle = BorderStyle.FixedSingle
                };

            layout.Controls.Add(panelNhap, 0, 1);

            txtSo = TaoText(
                panelNhap, "Số đăng ký", 155);

            cboChuyen = TaoCombo(
                panelNhap, "Chuyến đang mở", 330);

            cboDiemBan = TaoCombo(
                panelNhap, "Điểm bán vé", 230);

            txtTen = TaoText(
                panelNhap, "Người đăng ký", 220);

            txtDT = TaoText(
                panelNhap, "Điện thoại", 160);

            numNguoi = TaoSo(
                panelNhap, "Số người", 1, 11, 1);

            cboChuyen.SelectedIndexChanged += TinhTien;
            numNguoi.ValueChanged += TinhTien;

            Panel panelTien = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8)
            };

            Label lblTenTien = new Label
            {
                Text = "THÀNH TIỀN:",
                Location = new Point(15, 9),
                Size = new Size(165, 30),
                Font = new Font(
                    "Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95)
            };

            lblThanhTien = new Label
            {
                Text = "0 đ",
                Location = new Point(185, 7),
                Size = new Size(280, 34),
                Font = new Font(
                    "Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 125, 80)
            };

            panelTien.Controls.Add(lblTenTien);
            panelTien.Controls.Add(lblThanhTien);
            layout.Controls.Add(panelTien, 0, 2);

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
                    DataGridViewAutoSizeColumnsMode
                        .DisplayedCells,
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

            layout.Controls.Add(dgv, 0, 3);

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
                "Đóng", Color.Firebrick, 145);

            btnDangKy = TaoNut(
                "Đăng ký và thanh toán vé",
                Color.FromArgb(35, 95, 155),
                260);

            footer.Controls.Add(btnDong);
            footer.Controls.Add(btnDangKy);

            layout.Controls.Add(footer, 0, 4);

            btnDangKy.Click += btnDangKy_Click;
            btnDong.Click += btnDong_Click;

            ResumeLayout(true);
        }

        private Panel TaoNhom(
            FlowLayoutPanel panel,
            string ten,
            int width)
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
                Size = new Size(width, 25),
                Font = new Font("Segoe UI", 9)
            };

            group.Controls.Add(lbl);
            panel.Controls.Add(group);
            return group;
        }

        private TextBox TaoText(
            FlowLayoutPanel panel,
            string ten,
            int width)
        {
            Panel group = TaoNhom(panel, ten, width);

            TextBox txt = new TextBox
            {
                Location = new Point(0, 30),
                Width = width,
                Font = new Font("Segoe UI", 10)
            };

            group.Controls.Add(txt);
            return txt;
        }

        private ComboBox TaoCombo(
            FlowLayoutPanel panel,
            string ten,
            int width)
        {
            Panel group = TaoNhom(panel, ten, width);

            ComboBox cbo = new ComboBox
            {
                Location = new Point(0, 30),
                Width = width,
                Font = new Font("Segoe UI", 10),
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

            group.Controls.Add(cbo);
            return cbo;
        }

        private NumericUpDown TaoSo(
            FlowLayoutPanel panel,
            string ten,
            decimal min,
            decimal max,
            decimal giaTri)
        {
            Panel group = TaoNhom(panel, ten, 150);

            NumericUpDown num = new NumericUpDown
            {
                Location = new Point(0, 30),
                Width = 150,
                Minimum = min,
                Maximum = max,
                Value = giaTri,
                Font = new Font("Segoe UI", 10)
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

        private void FrmDangKyLe_Load(
            object sender, EventArgs e)
        {
            try
            {
                FormHelper.Nap(
                    cboChuyen,
                    chuyenService.LayChuyenMo(),
                    "HienThi",
                    "MaChuyen");

                FormHelper.Nap(
                    cboDiemBan,
                    danhMucService.LayDiemBan(),
                    "TenDiemBan",
                    "MaDiemBan");

                Tai();
                TinhTien(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải đăng ký khách lẻ:\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Tai()
        {
            dgv.DataSource = svc.LayDanhSach();

            if (dgv.Columns.Contains("NgayDi"))
                dgv.Columns["NgayDi"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy";

            if (dgv.Columns.Contains("NgayVe"))
                dgv.Columns["NgayVe"]
                    .DefaultCellStyle.Format =
                    "dd/MM/yyyy";

            if (dgv.Columns.Contains("ThanhTien"))
                dgv.Columns["ThanhTien"]
                    .DefaultCellStyle.Format = "N0";
        }

        private void TinhTien(
            object sender, EventArgs e)
        {
            DataRowView r =
                cboChuyen.SelectedItem as DataRowView;

            if (r == null)
            {
                lblThanhTien.Text = "0 đ";
                return;
            }

            decimal donGia =
                Convert.ToDecimal(r["DonGiaKhach"]);

            decimal thanhTien =
                donGia * numNguoi.Value;

            lblThanhTien.Text =
                thanhTien.ToString("N0") + " đ";
        }

        private void btnDangKy_Click(
            object sender, EventArgs e)
        {
            if (!(cboChuyen.SelectedValue is string))
            {
                MessageBox.Show(
                    "Vui lòng chọn chuyến đang mở.");
                return;
            }

            if (!(cboDiemBan.SelectedValue is string))
            {
                MessageBox.Show(
                    "Vui lòng chọn điểm bán vé.");
                return;
            }

            DialogResult xacNhan = MessageBox.Show(
                "Xác nhận đăng ký và đã thu tiền vé?\n" +
                "Thành tiền: " + lblThanhTien.Text,
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
                return;

            if (FormHelper.Bao(
                svc.DangKy(
                    txtSo.Text,
                    FormHelper.Gia(cboChuyen),
                    FormHelper.Gia(cboDiemBan),
                    txtTen.Text,
                    txtDT.Text,
                    (int)numNguoi.Value)))
            {
                Tai();

                txtSo.Clear();
                txtTen.Clear();
                txtDT.Clear();

                numNguoi.Value = 1;
                txtSo.Focus();
            }
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}
