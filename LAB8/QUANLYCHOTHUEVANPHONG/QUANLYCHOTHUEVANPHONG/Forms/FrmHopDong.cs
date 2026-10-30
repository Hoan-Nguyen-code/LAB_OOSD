
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;
using QuanLyChoThueVanPhong.Data;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmHopDong : Form
    {
        private readonly HopDongService sv = new HopDongService();

        private TextBox txtMaHD = new TextBox();
        private ComboBox cboKhachHang = new ComboBox();
        private DateTimePicker dtNgayKy = new DateTimePicker();
        private DateTimePicker dtBatDau = new DateTimePicker();
        private NumericUpDown numThoiHan = new NumericUpDown();

        private DataGridView dgvPhong = new DataGridView();
        private DataGridView dgvDaChon = new DataGridView();
        private DataGridView dgvHopDong = new DataGridView();
        private DataGridView dgvChiTiet = new DataGridView();
        private Label lblTien = new Label();

        private BindingList<PhongChon> dsChon =
            new BindingList<PhongChon>();

        private class PhongChon
        {
            public string MaPhong { get; set; }
            public decimal DonGia { get; set; }
        }

        public FrmHopDong()
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
            c.Width = 155;
            p.Controls.Add(c);
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

        private void TaoGiaoDien()
        {
            Text = "Quản lý hợp đồng thuê";
            ClientSize = new Size(1220, 800);
            StartPosition = FormStartPosition.CenterScreen;

            numThoiHan.Minimum = 6;
            numThoiHan.Maximum = 600;
            numThoiHan.Value = 12;
            cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
            dtNgayKy.Format = dtBatDau.Format =
                DateTimePickerFormat.Short;

            var top = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            Them(top, "Mã hợp đồng", txtMaHD);
            Them(top, "Khách hàng", cboKhachHang);
            Them(top, "Ngày ký", dtNgayKy);
            Them(top, "Ngày bắt đầu", dtBatDau);
            Them(top, "Số tháng", numThoiHan);

            var tabs = new TabControl { Dock = DockStyle.Fill };
            var tabLap = new TabPage("Lập hợp đồng");
            var tabDS = new TabPage("Danh sách hợp đồng");

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 280
            };

            ChuanGrid(dgvPhong);
            ChuanGrid(dgvDaChon);
            ChuanGrid(dgvHopDong);
            ChuanGrid(dgvChiTiet);

            dgvDaChon.DataSource = dsChon;
            split.Panel1.Controls.Add(dgvPhong);
            split.Panel2.Controls.Add(dgvDaChon);

            var tools = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 75,
                Padding = new Padding(8)
            };

            var btnThem = new Button
            {
                Text = "Chọn phòng",
                Width = 125
            };
            var btnBo = new Button
            {
                Text = "Bỏ phòng",
                Width = 110
            };
            var btnLap = new Button
            {
                Text = "Lập hợp đồng",
                Width = 145
            };

            lblTien.AutoSize = true;
            lblTien.Text = "Tiền thuê: 0 đồng";
            lblTien.Margin = new Padding(15, 8, 3, 3);

            btnThem.Click += (s, e) =>
            {
                if (dgvPhong.CurrentRow == null) return;

                var r = dgvPhong.CurrentRow;
                string ma = Convert.ToString(
                    r.Cells["MaPhong"].Value);

                if (dsChon.Any(x => x.MaPhong == ma))
                {
                    MessageBox.Show("Phòng đã được chọn.");
                    return;
                }

                dsChon.Add(new PhongChon
                {
                    MaPhong = ma,
                    DonGia = Convert.ToDecimal(
                        r.Cells["DonGiaThue"].Value)
                });
                TinhTien();
            };

            btnBo.Click += (s, e) =>
            {
                if (dgvDaChon.CurrentRow == null) return;
                int i = dgvDaChon.CurrentRow.Index;
                if (i >= 0 && i < dsChon.Count)
                    dsChon.RemoveAt(i);
                TinhTien();
            };

            btnLap.Click += (s, e) => LapHopDong();

            tools.Controls.AddRange(new Control[]
            {
                btnThem, btnBo, btnLap, lblTien
            });

            tabLap.Controls.Add(split);
            tabLap.Controls.Add(tools);

            var lichSu = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 300
            };
            lichSu.Panel1.Controls.Add(dgvHopDong);
            lichSu.Panel2.Controls.Add(dgvChiTiet);
            tabDS.Controls.Add(lichSu);

            dgvHopDong.SelectionChanged += (s, e) => TaiChiTiet();

            tabs.TabPages.Add(tabLap);
            tabs.TabPages.Add(tabDS);

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
            Controls.Add(top);
            Controls.Add(bottom);
        }

        private void TinhTien()
        {
            decimal tong = dsChon.Sum(x => x.DonGia);
            lblTien.Text = "Tiền/tháng: " + tong.ToString("N0") +
                " đ | Trả trước 6 tháng: " +
                (tong * 6).ToString("N0") + " đ";
        }

        private void Tai()
        {
            try
            {
                var kh = Db.Query(
                    "SELECT MaKH, TenKhachHang FROM dbo.KhachHang");
                cboKhachHang.DataSource = kh;
                cboKhachHang.DisplayMember = "TenKhachHang";
                cboKhachHang.ValueMember = "MaKH";

                dgvPhong.DataSource = Db.Query(
                    @"SELECT MaPhong, SoPhong, DienTich,
                             HuongPhong, DonGiaThue
                      FROM dbo.Phong
                      WHERE TrangThai=N'Sẵn sàng'");

                dgvHopDong.DataSource = sv.List(sv.Tables[0]);
                TaiChiTiet();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void TaiChiTiet()
        {
            if (dgvHopDong.CurrentRow == null) return;
            try
            {
                string ma = Convert.ToString(
                    dgvHopDong.CurrentRow.Cells["MaHopDong"].Value);

                dgvChiTiet.DataSource = Db.Query(
                    @"SELECT MaChiTietHD, MaPhong, DonGiaThue,
                             NgayBatDau, NgayKetThuc
                      FROM dbo.ChiTietHopDong
                      WHERE MaHopDong=@ma",
                    Db.P("@ma", ma));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LapHopDong()
        {
            try
            {
                if (dsChon.Count == 0)
                    throw new Exception("Chưa chọn phòng thuê.");

                string ds = string.Join(",",
                    dsChon.Select(x => x.MaPhong));

                string kq = sv.LapHopDong(
                    txtMaHD.Text.Trim(),
                    Convert.ToString(cboKhachHang.SelectedValue),
                    dtNgayKy.Value,
                    dtBatDau.Value,
                    (int)numThoiHan.Value,
                    ds);

                MessageBox.Show(kq);
                dsChon.Clear();
                TinhTien();
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
