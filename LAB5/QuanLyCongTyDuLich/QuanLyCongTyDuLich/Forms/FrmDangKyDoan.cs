
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmDangKyDoan : Form
    {
        private readonly DangKyDoanService svc =
            new DangKyDoanService();

        private readonly BindingList<ThanhVienDoanItem> thanhVien =
            new BindingList<ThanhVienDoanItem>();

        private TextBox txtMaDoan, txtTenCQ;
        private TextBox txtDiaChi, txtDT, txtDaiDien;
        private TextBox txtSo, txtDon;

        private ComboBox cboTour;
        private DateTimePicker dtDi;
        private NumericUpDown numNguoi, numCoc;
        private CheckBox chkBH;

        private Label lblKetThuc, lblTong;
        private DataGridView dgvThanhVien, dgv;
        private Button btnDangKy, btnHuy, btnDong;

        public FrmDangKyDoan()
        {
            InitializeComponent();
            ThietKeGiaoDien();
            Load += FrmDangKyDoan_Load;
        }

        private void ThietKeGiaoDien()
        {
            SuspendLayout();
            Controls.Clear();

            Text = "Đăng ký tour theo đoàn";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1250, 820);
            MinimumSize = new Size(1030, 700);
            BackColor = Color.FromArgb(245, 248, 252);
            Font = new Font("Segoe UI", 10);

            TableLayoutPanel main = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(12)
            };

            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            main.RowStyles.Add(
                new RowStyle(SizeType.Percent, 60));
            main.RowStyles.Add(
                new RowStyle(SizeType.Percent, 40));
            main.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 62));

            Controls.Add(main);

            Label title = new Label
            {
                Text = "ĐĂNG KÝ TOUR THEO ĐOÀN",
                Dock = DockStyle.Fill,
                Font = new Font(
                    "Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 55, 95),
                TextAlign = ContentAlignment.MiddleCenter
            };

            main.Controls.Add(title, 0, 0);

            // Khu nhập liệu có thể cuộn dọc
            Panel vungNhap = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 8)
            };

            TableLayoutPanel noiDung = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 3
            };

            noiDung.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));
            noiDung.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 115));
            noiDung.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 195));
            noiDung.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 160));

            vungNhap.Controls.Add(noiDung);
            main.Controls.Add(vungNhap, 0, 1);

            // 1. Thông tin đoàn khách
            GroupBox grpDoan = new GroupBox
            {
                Text = "1. THÔNG TIN ĐOÀN KHÁCH",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(25, 55, 95),
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold)
            };

            FlowLayoutPanel pnDoan = TaoPanelNhap();

            txtMaDoan = TaoText(
                pnDoan, "Mã đoàn", 145);
            txtTenCQ = TaoText(
                pnDoan, "Cơ quan / gia đình", 230);
            txtDiaChi = TaoText(
                pnDoan, "Địa chỉ", 245);
            txtDT = TaoText(
                pnDoan, "Điện thoại", 155);
            txtDaiDien = TaoText(
                pnDoan, "Người đại diện", 210);

            grpDoan.Controls.Add(pnDoan);
            noiDung.Controls.Add(grpDoan, 0, 0);

            // 2. Thông tin đăng ký tour
            GroupBox grpDangKy = new GroupBox
            {
                Text = "2. THÔNG TIN ĐĂNG KÝ TOUR",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(25, 55, 95),
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold)
            };

            FlowLayoutPanel pnDangKy = TaoPanelNhap();

            txtSo = TaoText(pnDangKy, "Số phiếu", 145);
            cboTour = TaoCombo(pnDangKy, "Tour", 300);
            dtDi = TaoNgay(pnDangKy, "Ngày đi");

            numNguoi = TaoSo(
                pnDangKy, "Số người", 13, 1000, 13, 115);

            txtDon = TaoText(
                pnDangKy, "Địa điểm đón", 220);

            numCoc = TaoSo(
                pnDangKy, "Tiền cọc",
                0, 100000000000, 1000000, 190);

            chkBH = new CheckBox
            {
                Text = "Mua bảo hiểm",
                AutoSize = true,
                Margin = new Padding(12, 34, 10, 5)
            };
            pnDangKy.Controls.Add(chkBH);

            lblKetThuc = TaoNhan(
                pnDangKy, "Kết thúc dự kiến", 165);

            lblTong = TaoNhan(
                pnDangKy, "Tổng tiền dự kiến", 210);

            grpDangKy.Controls.Add(pnDangKy);
            noiDung.Controls.Add(grpDangKy, 0, 1);

            cboTour.SelectedIndexChanged += TinhTong;
            dtDi.ValueChanged += TinhTong;
            numNguoi.ValueChanged += TinhTong;
            chkBH.CheckedChanged +=
                chkBH_CheckedChanged;

            // 3. Danh sách thành viên bảo hiểm
            GroupBox grpTV = new GroupBox
            {
                Text = "3. DANH SÁCH NGƯỜI CÙNG ĐI (KHI MUA BẢO HIỂM)",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(25, 55, 95),
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold)
            };

            dgvThanhVien = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                RowHeadersVisible = true
            };

            dgvThanhVien.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Họ tên",
                    DataPropertyName = "HoTen",
                    Name = "HoTen"
                });

            dgvThanhVien.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Ngày sinh",
                    DataPropertyName = "NgaySinh",
                    Name = "NgaySinh",
                    DefaultCellStyle =
                        new DataGridViewCellStyle
                        {
                            Format = "dd/MM/yyyy"
                        }
                });

            dgvThanhVien.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    HeaderText = "Số giấy tờ",
                    DataPropertyName = "SoGiayTo",
                    Name = "SoGiayTo"
                });

            dgvThanhVien.DataError += (s, e) =>
            {
                MessageBox.Show(
                    "Ngày sinh không hợp lệ. " +
                    "Vui lòng nhập ngày theo định dạng dd/MM/yyyy.",
                    "Lỗi nhập liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                e.ThrowException = false;
            };

            grpTV.Controls.Add(dgvThanhVien);
            noiDung.Controls.Add(grpTV, 0, 2);

            // Bảng phiếu đăng ký
            GroupBox grpDanhSach = new GroupBox
            {
                Text = "DANH SÁCH PHIẾU ĐĂNG KÝ ĐOÀN",
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(25, 55, 95),
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(0)
            };

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode
                        .DisplayedCells,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            dgv.ColumnHeadersHeight = 38;
            dgv.RowTemplate.Height = 30;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(35, 95, 155);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            grpDanhSach.Controls.Add(dgv);
            main.Controls.Add(grpDanhSach, 0, 2);

            // Nút chức năng
            FlowLayoutPanel footer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };

            btnDong = TaoNut(
                "Đóng", Color.Firebrick, 140);

            btnHuy = TaoNut(
                "Hủy phiếu (mất cọc)",
                Color.FromArgb(190, 110, 30), 210);

            btnDangKy = TaoNut(
                "Lập phiếu đăng ký",
                Color.FromArgb(35, 95, 155), 210);

            footer.Controls.Add(btnDong);
            footer.Controls.Add(btnHuy);
            footer.Controls.Add(btnDangKy);

            main.Controls.Add(footer, 0, 3);

            btnDangKy.Click += btnDangKy_Click;
            btnHuy.Click += btnHuy_Click;
            btnDong.Click += btnDong_Click;

            ResumeLayout(true);
        }

        private FlowLayoutPanel TaoPanelNhap()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(8),
                BackColor = Color.FromArgb(240, 245, 250)
            };
        }

        private Panel TaoNhom(
            FlowLayoutPanel panel,
            string nhan, int width)
        {
            Panel p = new Panel
            {
                Width = width + 12,
                Height = 67,
                Margin = new Padding(6)
            };

            Label lbl = new Label
            {
                Text = nhan,
                Size = new Size(width, 23),
                Location = new Point(0, 0),
                Font = new Font("Segoe UI", 9)
            };

            p.Controls.Add(lbl);
            panel.Controls.Add(p);
            return p;
        }

        private TextBox TaoText(
            FlowLayoutPanel panel,
            string nhan, int width)
        {
            Panel p = TaoNhom(panel, nhan, width);

            TextBox txt = new TextBox
            {
                Location = new Point(0, 28),
                Width = width
            };

            p.Controls.Add(txt);
            return txt;
        }

        private ComboBox TaoCombo(
            FlowLayoutPanel panel,
            string nhan, int width)
        {
            Panel p = TaoNhom(panel, nhan, width);

            ComboBox cbo = new ComboBox
            {
                Location = new Point(0, 28),
                Width = width,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            p.Controls.Add(cbo);
            return cbo;
        }

        private DateTimePicker TaoNgay(
            FlowLayoutPanel panel, string nhan)
        {
            Panel p = TaoNhom(panel, nhan, 155);

            DateTimePicker dt = new DateTimePicker
            {
                Location = new Point(0, 28),
                Width = 155,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy"
            };

            p.Controls.Add(dt);
            return dt;
        }

        private NumericUpDown TaoSo(
            FlowLayoutPanel panel, string nhan,
            decimal min, decimal max,
            decimal value, int width)
        {
            Panel p = TaoNhom(panel, nhan, width);

            NumericUpDown num = new NumericUpDown
            {
                Location = new Point(0, 28),
                Width = width,
                Minimum = min,
                Maximum = max,
                Value = value,
                ThousandsSeparator = true
            };

            p.Controls.Add(num);
            return num;
        }

        private Label TaoNhan(
            FlowLayoutPanel panel,
            string nhan, int width)
        {
            Panel p = TaoNhom(panel, nhan, width);

            Label lbl = new Label
            {
                Text = "-",
                Location = new Point(0, 28),
                Size = new Size(width, 32),
                ForeColor = Color.FromArgb(20, 115, 80),
                Font = new Font(
                    "Segoe UI", 11, FontStyle.Bold)
            };

            p.Controls.Add(lbl);
            return lbl;
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
                Cursor = Cursors.Hand,
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(8, 3, 8, 3)
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void FrmDangKyDoan_Load(
            object sender, EventArgs e)
        {
            try
            {
                dgvThanhVien.DataSource = thanhVien;

                FormHelper.Nap(
                    cboTour,
                    new TourService().LayTourMoBan(),
                    "HienThi",
                    "MaTour");

                dtDi.Value = DateTime.Today.AddDays(14);

                Tai();
                TinhTong(sender, e);
                chkBH_CheckedChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu đoàn:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Tai()
        {
            dgv.DataSource = svc.LayDanhSach();

            foreach (string ten in new string[]
            {
                "NgayDi", "NgayKetThucDuKien"
            })
            {
                if (dgv.Columns.Contains(ten))
                    dgv.Columns[ten]
                        .DefaultCellStyle.Format =
                        "dd/MM/yyyy";
            }

            foreach (string ten in new string[]
            {
                "TienCoc", "TongTienDuKien"
            })
            {
                if (dgv.Columns.Contains(ten))
                    dgv.Columns[ten]
                        .DefaultCellStyle.Format = "N0";
            }
        }

        private void TinhTong(
            object sender, EventArgs e)
        {
            DataRowView r =
                cboTour.SelectedItem as DataRowView;

            if (r == null)
            {
                lblTong.Text = "0 đ";
                lblKetThuc.Text = "-";
                return;
            }

            decimal donGia =
                Convert.ToDecimal(r["DonGiaKhach"]);

            int soNgay = Convert.ToInt32(r["SoNgay"]);

            decimal tong = donGia * numNguoi.Value;

            lblTong.Text =
                tong.ToString("N0") + " đ";

            lblKetThuc.Text =
                dtDi.Value.Date.AddDays(soNgay - 1)
                    .ToString("dd/MM/yyyy");
        }

        private void chkBH_CheckedChanged(
            object sender, EventArgs e)
        {
            dgvThanhVien.Enabled = chkBH.Checked;
        }

        private void btnDangKy_Click(
            object sender, EventArgs e)
        {
            dgvThanhVien.EndEdit();

            CurrencyManager cm =
                BindingContext[thanhVien] as CurrencyManager;

            if (cm != null)
                cm.EndCurrentEdit();

            List<ThanhVienDoanItem> ds =
                thanhVien
                    .Where(x => x != null &&
                                !string.IsNullOrWhiteSpace(
                                    x.HoTen))
                    .ToList();

            if (MessageBox.Show(
                "Xác nhận đã nhận tiền cọc " +
                numCoc.Value.ToString("N0") + " đ?",
                "Xác nhận tiền cọc",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            KetQuaXuLy k = svc.DangKy(
                txtSo.Text,
                txtMaDoan.Text,
                txtTenCQ.Text,
                txtDiaChi.Text,
                txtDT.Text,
                txtDaiDien.Text,
                FormHelper.Gia(cboTour),
                dtDi.Value,
                (int)numNguoi.Value,
                txtDon.Text,
                chkBH.Checked,
                numCoc.Value,
                ds);

            if (FormHelper.Bao(k))
            {
                thanhVien.Clear();
                Tai();

                txtSo.Clear();
                txtMaDoan.Clear();
                txtTenCQ.Clear();
                txtDiaChi.Clear();
                txtDT.Clear();
                txtDaiDien.Clear();
                txtDon.Clear();

                chkBH.Checked = false;
                txtSo.Focus();
            }
        }

        private void btnHuy_Click(
            object sender, EventArgs e)
        {
            string so =
                FormHelper.O(dgv, "SoDKDoan");

            if (string.IsNullOrWhiteSpace(so))
            {
                MessageBox.Show(
                    "Chọn phiếu đăng ký đoàn cần hủy.");
                return;
            }

            if (MessageBox.Show(
                "Đoàn không đi sẽ mất tiền cọc.\n" +
                "Bạn muốn hủy phiếu " + so + "?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
                != DialogResult.Yes)
                return;

            if (FormHelper.Bao(svc.HuyDangKy(so)))
                Tai();
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}
