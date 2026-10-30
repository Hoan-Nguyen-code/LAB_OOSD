
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Text = "QUẢN LÝ CHO THUÊ VĂN PHÒNG";
            ClientSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);

            var title = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ CHO THUÊ VĂN PHÒNG",
                Dock = DockStyle.Top,
                Height = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 100)
            };
            Controls.Add(title);

            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 4,
                Padding = new Padding(25)
            };

            for (int i = 0; i < 3; i++)
                grid.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 33.33f));

            for (int i = 0; i < 4; i++)
                grid.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25f));

            ThemNut(grid, "QUẢN LÝ TẦNG / PHÒNG",
                () => new FrmPhong(), 0, 0);
            ThemNut(grid, "QUẢN LÝ KHÁCH HÀNG",
                () => new FrmKhachHang(), 1, 0);
            ThemNut(grid, "QUẢN LÝ HỢP ĐỒNG",
                () => new FrmHopDong(), 2, 0);
            ThemNut(grid, "THANH TOÁN TIỀN THUÊ",
                () => new FrmThanhToan(), 0, 1);
            ThemNut(grid, "QUẢN LÝ DỊCH VỤ",
                () => new FrmDichVu(), 1, 1);
            ThemNut(grid, "GIA HẠN HỢP ĐỒNG",
                () => new FrmGiaHan(), 2, 1);
            ThemNut(grid, "QUẢN LÝ NHÂN VIÊN",
                () => new FrmNhanVien(), 0, 2);
            ThemNut(grid, "THẺ GIỮ XE",
                () => new FrmTheXe(), 1, 2);
            ThemNut(grid, "KIỂM TRA VÀ SỬA CHỮA",
                () => new FrmKiemTraPhong(), 2, 2);

            var thoat = new Button
            {
                Text = "THOÁT",
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            thoat.Click += (s, e) => Close();
            grid.Controls.Add(thoat, 1, 3);
            Controls.Add(grid);
            grid.BringToFront();
        }

        private void ThemNut(
            TableLayoutPanel grid, string ten,
            Func<Form> taoForm, int cot, int hang)
        {
            var btn = new Button
            {
                Text = ten,
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                BackColor = Color.FromArgb(39, 79, 120),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            btn.Click += (s, e) =>
            {
                using (Form form = taoForm())
                    form.ShowDialog(this);
            };

            grid.Controls.Add(btn, cot, hang);
        }
    }
}
