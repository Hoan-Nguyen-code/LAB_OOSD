using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmMain : Form
    {
        private Label lblTrangThai;

        public FrmMain()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Text = "LAB7 - Quản lý nhân sự Công ty sơn Đông Á";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(950, 620);
            MinimumSize = new Size(800, 540);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(245, 247, 250);

            Label lblTieuDe = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ NHÂN SỰ",
                Dock = DockStyle.Top,
                Height = 82,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 58, 95),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(lblTieuDe);

            lblTrangThai = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 46,
                Text = "Đang kiểm tra kết nối SQL Server...",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DimGray,
                Font = new Font("Segoe UI", 9F)
            };

            Controls.Add(lblTrangThai);

            TableLayoutPanel bang = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(40, 15, 40, 20),
                BackColor = Color.Transparent
            };

            bang.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            bang.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            for (int i = 0; i < 5; i++)
            {
                bang.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 20F));
            }

            ThemNut(bang, "QUẢN LÝ NHÂN VIÊN", 0, 0,
                () => new FrmNhanVien().ShowDialog(this));

            ThemNut(bang, "TUYỂN DỤNG & THỬ VIỆC", 1, 0,
                () => new FrmTuyenDung().ShowDialog(this));

            ThemNut(bang, "HỢP ĐỒNG & BẢO HIỂM", 0, 1,
                () => new FrmHopDong().ShowDialog(this));

            ThemNut(bang, "QUYẾT ĐỊNH LƯƠNG & CHỨC VỤ", 1, 1,
                () => new FrmQuyetDinh().ShowDialog(this));

            ThemNut(bang, "PHÂN CÔNG LAO ĐỘNG", 0, 2,
                () => new FrmPhanCong().ShowDialog(this));

            ThemNut(bang, "CHẤM CÔNG & PHỤ TRỘI", 1, 2,
                () => new FrmChamCong().ShowDialog(this));

            ThemNut(bang, "BẢNG LƯƠNG HẰNG THÁNG", 0, 3,
                () => new FrmBangLuong().ShowDialog(this));

            ThemNut(bang, "KHEN THƯỞNG", 1, 3,
                () => new FrmKhenThuong().ShowDialog(this));

            ThemNut(bang, "KỶ LUẬT & NỘP PHẠT", 0, 4,
                () => new FrmKyLuat().ShowDialog(this));

            ThemNut(bang, "THOÁT ỨNG DỤNG", 1, 4,
                () => Close(), true);

            Controls.Add(bang);
            bang.BringToFront();

            Load += (s, e) => KiemTraKetNoi();
        }

        private void ThemNut(
            TableLayoutPanel bang,
            string tieuDe,
            int cot,
            int dong,
            Action hanhDong,
            bool laNutThoat = false)
        {
            Button btn = new Button
            {
                Text = tieuDe,
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = laNutThoat
                    ? Color.FromArgb(170, 55, 55)
                    : Color.FromArgb(35, 78, 121),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.Click += (s, e) =>
            {
                try
                {
                    hanhDong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Lỗi mở chức năng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            };

            bang.Controls.Add(btn, cot, dong);
        }

        private void KiemTraKetNoi()
        {
            try
            {
                using (var cn = Db.OpenConnection())
                {
                    lblTrangThai.Text =
                        "Kết nối SQL Server thành công - " +
                        cn.Database;

                    lblTrangThai.ForeColor =
                        Color.FromArgb(30, 125, 65);
                }
            }
            catch (Exception ex)
            {
                lblTrangThai.Text =
                    "Không kết nối được SQL Server";

                lblTrangThai.ForeColor = Color.Red;

                MessageBox.Show(
                    "Lỗi kết nối CSDL:\n" + ex.Message,
                    "SQL Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}