
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmMain : Form
    {
        private TableLayoutPanel menuPanel;
        private Label lblKetNoi;

        public FrmMain()
        {
            InitializeComponent();
            TaoGiaoDien();
            Load += FrmMain_Load;
        }

        private void TaoGiaoDien()
        {
            // Cấu hình Form chính
            Text = "Quản lý hồ sơ bệnh án";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1050, 680);
            MinimumSize = new Size(950, 600);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            // Tiêu đề hệ thống
            Label lblTieuDe = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ HỒ SƠ BỆNH ÁN",
                Font = new Font(
                    "Segoe UI", 21F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 62, 95),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 90
            };

            // Tiêu đề phụ
            Label lblPhu = new Label
            {
                Text = "LAB6 - PHÂN TÍCH VÀ THIẾT KẾ HƯỚNG ĐỐI TƯỢNG",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.DimGray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            // Thanh trạng thái kết nối SQL Server
            lblKetNoi = new Label
            {
                Text = "Đang kiểm tra kết nối SQL Server...",
                Dock = DockStyle.Bottom,
                Height = 45,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(228, 234, 241),
                ForeColor = Color.DimGray
            };

            // Menu 2 cột x 4 hàng
            menuPanel = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 4,
                Dock = DockStyle.Fill,
                Padding = new Padding(55, 25, 55, 25),
                BackColor = Color.Transparent
            };

            menuPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            menuPanel.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            for (int i = 0; i < 4; i++)
            {
                menuPanel.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25F));
            }

            // Các chức năng của hệ thống
            ThemNut(
                "QUẢN LÝ BỆNH NHÂN",
                0, 0, "FrmBenhNhan");

            ThemNut(
                "HỒ SƠ BỆNH ÁN",
                1, 0, "FrmHoSoBenhAn");

            ThemNut(
                "KHÁM BỆNH",
                0, 1, "FrmKhamBenh");

            ThemNut(
                "XÉT NGHIỆM",
                1, 1, "FrmXetNghiem");

            ThemNut(
                "TOA THUỐC",
                0, 2, "FrmToaThuoc");

            ThemNut(
                "LỊCH KHÁM ĐỊNH KỲ",
                1, 2, "FrmLichKhamDinhKy");

            ThemNut(
                "HOÀN TẤT ĐIỀU TRỊ",
                0, 3, "FrmHoanTatDieuTri");

            ThemNut(
                "THOÁT CHƯƠNG TRÌNH",
                1, 3, "Thoat");

            // Đưa các thành phần vào Form
            Controls.Add(menuPanel);
            Controls.Add(lblKetNoi);
            Controls.Add(lblPhu);
            Controls.Add(lblTieuDe);
        }

        private void ThemNut(
            string tenNut,
            int cot,
            int dong,
            string tenForm)
        {
            Button btn = new Button
            {
                Text = tenNut,
                Dock = DockStyle.Fill,
                Margin = new Padding(12),
                Font = new Font(
                    "Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(32, 62, 95),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };

            btn.FlatAppearance.BorderColor =
                Color.FromArgb(180, 198, 215);

            btn.FlatAppearance.BorderSize = 1;

            btn.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(222, 235, 250);

            btn.Click += (sender, e) =>
            {
                if (tenForm == "Thoat")
                {
                    XacNhanThoat();
                    return;
                }

                MoForm(tenForm);
            };

            menuPanel.Controls.Add(btn, cot, dong);
        }

        // Mở các Form nghiệp vụ
        private void MoForm(string tenForm)
        {
            try
            {
                Form form = null;

                switch (tenForm)
                {
                    case "FrmBenhNhan":
                        form = new FrmBenhNhan();
                        break;

                    case "FrmHoSoBenhAn":
                        form = new FrmHoSoBenhAn();
                        break;

                    case "FrmKhamBenh":
                        form = new FrmKhamBenh();
                        break;

                    case "FrmXetNghiem":
                        form = new FrmXetNghiem();
                        break;

                    case "FrmToaThuoc":
                        form = new FrmToaThuoc();
                        break;

                    case "FrmLichKhamDinhKy":
                        form = new FrmLichKhamDinhKy();
                        break;

                    case "FrmHoanTatDieuTri":
                        form = new FrmHoanTatDieuTri();
                        break;

                    default:
                        MessageBox.Show(
                            "Không tìm thấy chức năng: " + tenForm,
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                }

                // Mở Form con, quay lại FrmMain khi đóng
                using (form)
                {
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể mở chức năng " + tenForm +
                    ":\n" + ex.Message,
                    "Lỗi mở Form",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Xác nhận trước khi thoát
        private void XacNhanThoat()
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thoát chương trình không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        // Kiểm tra kết nối SQL Server
        private void FrmMain_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                using (var connection = Db.OpenConnection())
                {
                    lblKetNoi.Text =
                        "SQL Server: Kết nối thành công - " +
                        connection.Database;

                    lblKetNoi.ForeColor =
                        Color.FromArgb(25, 125, 65);
                }
            }
            catch (Exception ex)
            {
                lblKetNoi.Text =
                    "SQL Server: Kết nối thất bại";

                lblKetNoi.ForeColor = Color.DarkRed;

                MessageBox.Show(
                    "Không thể kết nối SQL Server:\n" +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
