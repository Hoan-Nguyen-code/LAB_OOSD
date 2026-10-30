
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmHoSoBenhAn : Form
    {
        private readonly HoSoService service = new HoSoService();

        private ComboBox cboBenhNhan;
        private TextBox txtSoTien, txtTimKiem;
        private DateTimePicker dtpBatDau, dtpHetHan;
        private DataGridView dgvHoSo, dgvLichSu, dgvLePhi;

        public FrmHoSoBenhAn()
        {
            InitializeComponent();
            TaoGiaoDien();
            Load += (s, e) => TaiDuLieu();
        }

        private Label TaoNhan(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Button TaoNut(string text)
        {
            var btn = new Button
            {
                Text = text,
                Width = 165,
                Height = 38,
                Margin = new Padding(5),
                BackColor = Color.FromArgb(32, 85, 145),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private DataGridView TaoBang()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý hồ sơ bệnh án";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1200, 800);
            MinimumSize = new Size(1000, 680);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(245, 247, 250);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(15),
                ColumnCount = 1,
                RowCount = 4
            };

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 150));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 55));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            root.Controls.Add(new Label
            {
                Text = "LỆ PHÍ NĂM VÀ HỒ SƠ BỆNH ÁN",
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 62, 95),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            }, 0, 0);

            var input = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2
            };

            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 140));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Absolute, 140));
            input.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            input.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            input.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            cboBenhNhan = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new Padding(5, 15, 15, 10)
            };

            txtSoTien = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 15, 15, 10)
            };

            dtpBatDau = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 15, 15, 10)
            };

            dtpHetHan = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill,
                Margin = new Padding(5, 15, 15, 10)
            };

            input.Controls.Add(TaoNhan("Bệnh nhân:"), 0, 0);
            input.Controls.Add(cboBenhNhan, 1, 0);
            input.Controls.Add(TaoNhan("Số tiền lệ phí:"), 2, 0);
            input.Controls.Add(txtSoTien, 3, 0);
            input.Controls.Add(TaoNhan("Ngày bắt đầu:"), 0, 1);
            input.Controls.Add(dtpBatDau, 1, 1);
            input.Controls.Add(TaoNhan("Ngày hết hạn:"), 2, 1);
            input.Controls.Add(dtpHetHan, 3, 1);

            root.Controls.Add(input, 0, 1);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false
            };

            Button btnCap = TaoNut("ĐĂNG KÝ + CẤP HỒ SƠ");
            btnCap.Width = 205;

            Button btnGiaHan = TaoNut("THU LỆ PHÍ NĂM");
            Button btnTim = TaoNut("TÌM HỒ SƠ");
            Button btnLamMoi = TaoNut("LÀM MỚI");

            txtTimKiem = new TextBox
            {
                Width = 180,
                Margin = new Padding(10, 12, 5, 5)
            };

            buttons.Controls.Add(btnCap);
            buttons.Controls.Add(btnGiaHan);
            buttons.Controls.Add(txtTimKiem);
            buttons.Controls.Add(btnTim);
            buttons.Controls.Add(btnLamMoi);

            root.Controls.Add(buttons, 0, 2);

            var tabs = new TabControl
            {
                Dock = DockStyle.Fill
            };

            var tabHoSo = new TabPage("Danh sách hồ sơ");
            var tabLichSu = new TabPage("Lịch sử khám");
            var tabLePhi = new TabPage("Lịch sử lệ phí năm");

            dgvHoSo = TaoBang();
            dgvLichSu = TaoBang();
            dgvLePhi = TaoBang();

            tabHoSo.Controls.Add(dgvHoSo);
            tabLichSu.Controls.Add(dgvLichSu);
            tabLePhi.Controls.Add(dgvLePhi);

            tabs.TabPages.Add(tabHoSo);
            tabs.TabPages.Add(tabLichSu);
            tabs.TabPages.Add(tabLePhi);

            root.Controls.Add(tabs, 0, 3);
            Controls.Add(root);

            dtpBatDau.Value = DateTime.Today;
            dtpHetHan.Value = DateTime.Today.AddYears(1);

            btnCap.Click += (s, e) => ThucHien(() =>
            {
                string maHoSo = service.DangKyVaCapHoSo(
                    LayMaBenhNhan(),
                    LaySoTien(),
                    dtpBatDau.Value,
                    dtpHetHan.Value);

                MessageBox.Show(
                    "Đăng ký, thu lệ phí và cấp hồ sơ thành công!\n" +
                    "Mã hồ sơ: " + maHoSo);

                TaiHoSo();
            });

            btnGiaHan.Click += (s, e) => ThucHien(() =>
            {
                service.GiaHanLePhi(
                    LayMaBenhNhan(),
                    LaySoTien(),
                    dtpBatDau.Value,
                    dtpHetHan.Value);

                MessageBox.Show(
                    "Ghi nhận lệ phí năm thành công!");

                TaiLePhi();
            });

            btnTim.Click += (s, e) => TaiHoSo();

            btnLamMoi.Click += (s, e) =>
            {
                txtTimKiem.Clear();
                txtSoTien.Clear();
                dtpBatDau.Value = DateTime.Today;
                dtpHetHan.Value = DateTime.Today.AddYears(1);
                TaiDuLieu();
            };

            cboBenhNhan.SelectedIndexChanged +=
                (s, e) => TaiLePhi();

            dgvHoSo.CellClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;

                string maHoSo = Convert.ToString(
                    dgvHoSo.Rows[e.RowIndex]
                        .Cells["MaHoSo"].Value);

                ThucHien(() =>
                {
                    dgvLichSu.DataSource =
                        service.LayLichSu(maHoSo);

                    tabs.SelectedTab = tabLichSu;
                });
            };
        }

        private string LayMaBenhNhan()
        {
            if (cboBenhNhan.SelectedValue == null ||
                cboBenhNhan.SelectedValue is DataRowView)
                throw new Exception("Vui lòng chọn bệnh nhân.");

            return cboBenhNhan.SelectedValue.ToString();
        }

        private decimal LaySoTien()
        {
            decimal soTien;

            if (!decimal.TryParse(txtSoTien.Text.Trim(), out soTien)
                || soTien <= 0)
                throw new Exception(
                    "Số tiền phải là một số lớn hơn 0.");

            return soTien;
        }

        private void TaiDuLieu()
        {
            ThucHien(() =>
            {
                DataTable dsBenhNhan = service.LayBenhNhan();
                cboBenhNhan.DataSource = dsBenhNhan;
                cboBenhNhan.DisplayMember = "HoTen";
                cboBenhNhan.ValueMember = "MaBN";

                TaiHoSo();
                TaiLePhi();
            });
        }

        private void TaiHoSo()
        {
            ThucHien(() =>
            {
                dgvHoSo.DataSource =
                    service.LayHoSo(txtTimKiem.Text);
            });
        }

        private void TaiLePhi()
        {
            if (cboBenhNhan.SelectedValue == null ||
                cboBenhNhan.SelectedValue is DataRowView)
                return;

            ThucHien(() =>
            {
                dgvLePhi.DataSource = service.LayLePhi(
                    cboBenhNhan.SelectedValue.ToString());
            });
        }

        private void ThucHien(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
