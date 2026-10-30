
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;
using QuanLyChoThueVanPhong.Data;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmThanhToan : Form
    {
        private readonly ThanhToanService sv =
            new ThanhToanService();

        private TextBox txtMaHoaDon = new TextBox();
        private ComboBox cboHopDong = new ComboBox();
        private NumericUpDown numKy = new NumericUpDown();
        private DataGridView dgvHoaDon = new DataGridView();

        private TextBox txtMaPhieuThu = new TextBox();
        private ComboBox cboHoaDon = new ComboBox();
        private DateTimePicker dtNgayThu = new DateTimePicker();
        private NumericUpDown numSoTien = new NumericUpDown();
        private ComboBox cboHinhThuc = new ComboBox();
        private DataGridView dgvPhieuThu = new DataGridView();

        public FrmThanhToan()
        {
            InitializeComponent();
            TaoGiaoDien();
            Load += (s, e) => Tai();
        }

        private void Them(FlowLayoutPanel p, string text, Control c)
        {
            p.Controls.Add(new Label
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(7, 9, 3, 3)
            });
            c.Width = 160;
            p.Controls.Add(c);
        }

        private void GanTab(TabPage t, DataGridView d,
            FlowLayoutPanel p)
        {
            d.Dock = DockStyle.Fill;
            d.ReadOnly = true;
            d.AllowUserToAddRows = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
            t.Controls.Add(d);
            t.Controls.Add(p);
        }

        private void TaoGiaoDien()
        {
            Text = "Thanh toán tiền thuê văn phòng";
            ClientSize = new Size(1150, 730);
            StartPosition = FormStartPosition.CenterScreen;

            numKy.Minimum = 7;
            numKy.Maximum = 600;
            numSoTien.Maximum = 999999999999m;
            numSoTien.DecimalPlaces = 2;
            numSoTien.ThousandsSeparator = true;
            dtNgayThu.Format = DateTimePickerFormat.Short;

            cboHopDong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHoaDon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHinhThuc.Items.AddRange(
                new object[] { "Tiền mặt", "Chuyển khoản" });
            cboHinhThuc.SelectedIndex = 0;

            var tabs = new TabControl { Dock = DockStyle.Fill };
            var t1 = new TabPage("Hóa đơn tiền thuê");
            var t2 = new TabPage("Phiếu thu tiền thuê");

            var p1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            Them(p1, "Mã hóa đơn", txtMaHoaDon);
            Them(p1, "Hợp đồng", cboHopDong);
            Them(p1, "Kỳ thuê", numKy);

            var btnLap = new Button
            {
                Text = "Lập hóa đơn",
                Width = 130
            };
            btnLap.Click += (s, e) => LapHoaDon();
            p1.Controls.Add(btnLap);

            var p2 = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 125,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            Them(p2, "Mã phiếu thu", txtMaPhieuThu);
            Them(p2, "Hóa đơn", cboHoaDon);
            Them(p2, "Ngày thu", dtNgayThu);
            Them(p2, "Số tiền", numSoTien);
            Them(p2, "Hình thức", cboHinhThuc);

            var btnThu = new Button
            {
                Text = "Thu tiền",
                Width = 120
            };
            btnThu.Click += (s, e) => ThuTien();
            p2.Controls.Add(btnThu);

            GanTab(t1, dgvHoaDon, p1);
            GanTab(t2, dgvPhieuThu, p2);
            tabs.TabPages.Add(t1);
            tabs.TabPages.Add(t2);

            var bottom = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                FlowDirection = FlowDirection.RightToLeft
            };
            var btnDong = new Button { Text = "Đóng" };
            var btnMoi = new Button { Text = "Làm mới" };
            btnDong.Click += (s, e) => Close();
            btnMoi.Click += (s, e) => Tai();
            bottom.Controls.Add(btnDong);
            bottom.Controls.Add(btnMoi);

            Controls.Add(tabs);
            Controls.Add(bottom);
        }

        private void Tai()
        {
            try
            {
                cboHopDong.DataSource = Db.Query(
                    "SELECT MaHopDong FROM dbo.HopDongThue");
                cboHopDong.DisplayMember = "MaHopDong";
                cboHopDong.ValueMember = "MaHopDong";

                var hd = sv.List(sv.Tables[0]);
                cboHoaDon.DataSource = hd.Copy();
                cboHoaDon.DisplayMember = "MaHoaDonThue";
                cboHoaDon.ValueMember = "MaHoaDonThue";

                dgvHoaDon.DataSource = hd;
                dgvPhieuThu.DataSource = sv.List(sv.Tables[2]);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LapHoaDon()
        {
            try
            {
                string kq = sv.LapHoaDonThang(
                    txtMaHoaDon.Text.Trim(),
                    Convert.ToString(cboHopDong.SelectedValue),
                    (int)numKy.Value);
                MessageBox.Show(kq);
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThuTien()
        {
            try
            {
                string kq = sv.ThuTienThue(
                    txtMaPhieuThu.Text.Trim(),
                    Convert.ToString(cboHoaDon.SelectedValue),
                    dtNgayThu.Value,
                    numSoTien.Value,
                    cboHinhThuc.Text);
                MessageBox.Show(kq);
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
