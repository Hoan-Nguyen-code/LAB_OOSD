
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmPhong : Form
    {
        private readonly PhongService sv = new PhongService();

        private TextBox txtMaTang = new TextBox();
        private TextBox txtTenTang = new TextBox();
        private NumericUpDown numDoCao = new NumericUpDown();
        private DataGridView dgvTang = new DataGridView();

        private TextBox txtMaPhong = new TextBox();
        private ComboBox cboTang = new ComboBox();
        private TextBox txtSoPhong = new TextBox();
        private NumericUpDown numDienTich = new NumericUpDown();
        private TextBox txtHuong = new TextBox();
        private NumericUpDown numGia = new NumericUpDown();
        private ComboBox cboTrangThai = new ComboBox();
        private DataGridView dgvPhong = new DataGridView();

        public FrmPhong()
        {
            InitializeComponent();
            TaoGiaoDien();
            Load += (s, e) => Tai();
        }

        private void Them(FlowLayoutPanel p, string nhan, Control c)
        {
            p.Controls.Add(new Label
            {
                Text = nhan,
                AutoSize = true,
                Margin = new Padding(8, 9, 3, 3)
            });
            c.Width = 165;
            p.Controls.Add(c);
        }

        private FlowLayoutPanel ThanhNhap()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 130,
                AutoScroll = true,
                Padding = new Padding(10)
            };
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý tầng và văn phòng";
            ClientSize = new Size(1150, 720);
            StartPosition = FormStartPosition.CenterScreen;

            numDoCao.Maximum = 100000;
            numDoCao.DecimalPlaces = 2;
            numDienTich.Maximum = 1000000;
            numDienTich.DecimalPlaces = 2;
            numGia.Maximum = 999999999999m;
            numGia.ThousandsSeparator = true;

            cboTang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Items.AddRange(new object[]
            {
                "Sẵn sàng", "Bảo trì", "Ngừng sử dụng"
            });
            cboTrangThai.SelectedIndex = 0;

            var tabs = new TabControl { Dock = DockStyle.Fill };
            var t1 = new TabPage("Quản lý tầng");
            var t2 = new TabPage("Quản lý phòng");

            var p1 = ThanhNhap();
            Them(p1, "Mã tầng", txtMaTang);
            Them(p1, "Tên tầng", txtTenTang);
            Them(p1, "Độ cao", numDoCao);

            var btnLuuTang = new Button
            {
                Text = "Lưu tầng",
                Width = 120,
                Height = 32
            };
            btnLuuTang.Click += (s, e) => LuuTang();
            p1.Controls.Add(btnLuuTang);

            var p2 = ThanhNhap();
            Them(p2, "Mã phòng", txtMaPhong);
            Them(p2, "Tầng", cboTang);
            Them(p2, "Số phòng", txtSoPhong);
            Them(p2, "Diện tích", numDienTich);
            Them(p2, "Hướng", txtHuong);
            Them(p2, "Giá thuê", numGia);
            Them(p2, "Trạng thái", cboTrangThai);

            var btnLuuPhong = new Button
            {
                Text = "Lưu phòng",
                Width = 120,
                Height = 32
            };
            btnLuuPhong.Click += (s, e) => LuuPhong();
            p2.Controls.Add(btnLuuPhong);

            ChuanGrid(dgvTang);
            ChuanGrid(dgvPhong);

            t1.Controls.Add(dgvTang);
            t1.Controls.Add(p1);
            t2.Controls.Add(dgvPhong);
            t2.Controls.Add(p2);

            tabs.TabPages.Add(t1);
            tabs.TabPages.Add(t2);

            dgvTang.SelectionChanged += (s, e) =>
            {
                if (dgvTang.CurrentRow == null) return;
                var r = dgvTang.CurrentRow;
                txtMaTang.Text = Convert.ToString(r.Cells["MaTang"].Value);
                txtTenTang.Text = Convert.ToString(r.Cells["TenTang"].Value);
                decimal v;
                if (decimal.TryParse(Convert.ToString(
                    r.Cells["DoCao"].Value), out v) &&
                    v >= numDoCao.Minimum && v <= numDoCao.Maximum)
                    numDoCao.Value = v;
            };

            dgvPhong.SelectionChanged += (s, e) =>
            {
                if (dgvPhong.CurrentRow == null) return;
                var r = dgvPhong.CurrentRow;
                txtMaPhong.Text = Convert.ToString(r.Cells["MaPhong"].Value);
                cboTang.SelectedValue = Convert.ToString(r.Cells["MaTang"].Value);
                txtSoPhong.Text = Convert.ToString(r.Cells["SoPhong"].Value);
                txtHuong.Text = Convert.ToString(r.Cells["HuongPhong"].Value);
                cboTrangThai.Text = Convert.ToString(r.Cells["TrangThai"].Value);

                decimal v;
                if (decimal.TryParse(Convert.ToString(
                    r.Cells["DienTich"].Value), out v) &&
                    v >= numDienTich.Minimum && v <= numDienTich.Maximum)
                    numDienTich.Value = v;

                if (decimal.TryParse(Convert.ToString(
                    r.Cells["DonGiaThue"].Value), out v) &&
                    v >= numGia.Minimum && v <= numGia.Maximum)
                    numGia.Value = v;
            };

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

        private void ChuanGrid(DataGridView d)
        {
            d.Dock = DockStyle.Fill;
            d.ReadOnly = true;
            d.AllowUserToAddRows = false;
            d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            d.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void Tai()
        {
            try
            {
                var tang = sv.List(sv.Tables[0]);
                dgvTang.DataSource = tang;
                dgvPhong.DataSource = sv.List(sv.Tables[1]);

                cboTang.DataSource = tang.Copy();
                cboTang.DisplayMember = "TenTang";
                cboTang.ValueMember = "MaTang";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuTang()
        {
            try
            {
                sv.Save(sv.Tables[0], new Dictionary<string, object>
                {
                    ["MaTang"] = txtMaTang.Text.Trim(),
                    ["TenTang"] = txtTenTang.Text.Trim(),
                    ["DoCao"] = numDoCao.Value
                });
                MessageBox.Show("Lưu tầng thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuPhong()
        {
            try
            {
                sv.Save(sv.Tables[1], new Dictionary<string, object>
                {
                    ["MaPhong"] = txtMaPhong.Text.Trim(),
                    ["MaTang"] = Convert.ToString(cboTang.SelectedValue),
                    ["SoPhong"] = txtSoPhong.Text.Trim(),
                    ["DienTich"] = numDienTich.Value,
                    ["HuongPhong"] = txtHuong.Text.Trim(),
                    ["DonGiaThue"] = numGia.Value,
                    ["TrangThai"] = cboTrangThai.Text
                });
                MessageBox.Show("Lưu phòng thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
