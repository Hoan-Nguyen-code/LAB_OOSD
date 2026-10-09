
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmMain : Form
    {
        private Button btnDanhMuc, btnTour, btnChuyenLe,
            btnDangKyLe, btnDangKyDoan, btnPhanCong,
            btnKetThuc, btnThongKe, btnThoat;

        public FrmMain()
        {
            InitializeComponent();
            ThietKeGiaoDien();
        }

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "QUẢN LÝ CÔNG TY DU LỊCH VĂN HÓA VIỆT";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1000, 650);
            MinimumSize = new Size(900, 600);
            BackColor = Color.FromArgb(245, 248, 252);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(20)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 75));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 75));

            Controls.Add(layout);

            Label lblTieuDe = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ CÔNG TY DU LỊCH",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(lblTieuDe, 0, 0);

            Label lblCongTy = new Label
            {
                Text = "CÔNG TY DU LỊCH VĂN HÓA VIỆT",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(lblCongTy, 0, 1);

            TableLayoutPanel bang = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 4,
                Dock = DockStyle.Fill,
                Padding = new Padding(25, 15, 25, 15)
            };

            bang.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            bang.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 4; i++)
                bang.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25));

            btnDanhMuc = TaoNut("QUẢN LÝ DANH MỤC");
            btnTour = TaoNut("TOUR - HÀNH TRÌNH");
            btnChuyenLe = TaoNut("LỊCH CHUYẾN KHÁCH LẺ");
            btnDangKyLe = TaoNut("ĐĂNG KÝ KHÁCH LẺ");
            btnDangKyDoan = TaoNut("ĐĂNG KÝ THEO ĐOÀN");
            btnPhanCong = TaoNut("PHÂN CÔNG HƯỚNG DẪN VIÊN");
            btnKetThuc = TaoNut("KẾT THÚC TOUR - KHẢO SÁT");
            btnThongKe = TaoNut("LƯƠNG - THỐNG KÊ");

            bang.Controls.Add(btnDanhMuc, 0, 0);
            bang.Controls.Add(btnTour, 1, 0);
            bang.Controls.Add(btnChuyenLe, 0, 1);
            bang.Controls.Add(btnDangKyLe, 1, 1);
            bang.Controls.Add(btnDangKyDoan, 0, 2);
            bang.Controls.Add(btnPhanCong, 1, 2);
            bang.Controls.Add(btnKetThuc, 0, 3);
            bang.Controls.Add(btnThongKe, 1, 3);

            layout.Controls.Add(bang, 0, 2);

            FlowLayoutPanel panelDuoi = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(15, 10, 25, 0)
            };

            btnThoat = new Button
            {
                Text = "THOÁT",
                Size = new Size(160, 45),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            btnThoat.FlatAppearance.BorderSize = 0;

            panelDuoi.Controls.Add(btnThoat);
            layout.Controls.Add(panelDuoi, 0, 3);

            // Kết nối sẵn tất cả chức năng
            btnDanhMuc.Click += (s, e) =>
                MoForm("FrmDanhMuc");

            btnTour.Click += (s, e) =>
                MoForm("FrmTour");

            btnChuyenLe.Click += (s, e) =>
                MoForm("FrmChuyenLe");

            btnDangKyLe.Click += (s, e) =>
                MoForm("FrmDangKyLe");

            btnDangKyDoan.Click += (s, e) =>
                MoForm("FrmDangKyDoan");

            btnPhanCong.Click += (s, e) =>
                MoForm("FrmPhanCongHDV");

            btnKetThuc.Click += (s, e) =>
                MoForm("FrmKetThucKhaoSat");

            btnThongKe.Click += (s, e) =>
                MoForm("FrmLuongThongKe");

            btnThoat.Click += btnThoat_Click;

            ResumeLayout(true);
        }

        private Button TaoNut(string noiDung)
        {
            Button btn = new Button
            {
                Text = noiDung,
                Dock = DockStyle.Fill,
                Margin = new Padding(12),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(35, 95, 155),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void MoForm(string tenForm)
        {
            try
            {
                string tenDayDu =
                    "QuanLyCongTyDuLich.Forms." + tenForm;

                Type loaiForm = typeof(FrmMain)
                    .Assembly.GetType(tenDayDu);

                if (loaiForm == null)
                {
                    MessageBox.Show(
                        "Chức năng " + tenForm +
                        " chưa được xây dựng.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                if (!typeof(Form).IsAssignableFrom(loaiForm))
                {
                    MessageBox.Show(
                        tenForm + " không phải Windows Form.");
                    return;
                }

                using (Form f =
                    (Form)Activator.CreateInstance(loaiForm))
                {
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể mở " + tenForm + ":\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Bạn có thực sự muốn thoát?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
