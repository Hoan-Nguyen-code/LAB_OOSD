
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;
using QuanLyChoThueVanPhong.Data;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmDichVu : Form
    {
        private readonly DichVuService sv = new DichVuService();

        private TextBox txtMaDV = new TextBox();
        private TextBox txtTenDV = new TextBox();
        private TextBox txtDonVi = new TextBox();
        private NumericUpDown numDonGia = new NumericUpDown();
        private DataGridView dgvDichVu = new DataGridView();

        private TextBox txtMaSuDung = new TextBox();
        private ComboBox cboChiTiet = new ComboBox();
        private ComboBox cboDichVu = new ComboBox();
        private NumericUpDown numThang = new NumericUpDown();
        private NumericUpDown numNam = new NumericUpDown();
        private NumericUpDown numSoLuong = new NumericUpDown();
        private NumericUpDown numGiaSuDung = new NumericUpDown();
        private DataGridView dgvSuDung = new DataGridView();

        private TextBox txtMaHoaDon = new TextBox();
        private ComboBox cboChiTietHD = new ComboBox();
        private NumericUpDown numThangHD = new NumericUpDown();
        private NumericUpDown numNamHD = new NumericUpDown();
        private DateTimePicker dtNgayLap = new DateTimePicker();
        private DataGridView dgvHoaDon = new DataGridView();

        private TextBox txtMaPhieuThu = new TextBox();
        private ComboBox cboHoaDon = new ComboBox();
        private DateTimePicker dtNgayThu = new DateTimePicker();
        private NumericUpDown numSoTien = new NumericUpDown();
        private ComboBox cboHinhThuc = new ComboBox();
        private DataGridView dgvPhieuThu = new DataGridView();

        public FrmDichVu()
        {
            InitializeComponent();
            TaoGiaoDien();
            Load += (s, e) => Tai();
        }

        private string Ma(ComboBox c)
        {
            return Convert.ToString(c.SelectedValue) ?? "";
        }

        private FlowLayoutPanel ThanhNhap()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 145,
                AutoScroll = true,
                Padding = new Padding(10)
            };
        }

        private void Them(FlowLayoutPanel p, string text, Control c)
        {
            p.Controls.Add(new Label
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(7, 9, 3, 3)
            });
            c.Width = 150;
            p.Controls.Add(c);
        }

        private void GanTab(TabPage tab, DataGridView dgv,
            FlowLayoutPanel top)
        {
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
            tab.Controls.Add(dgv);
            tab.Controls.Add(top);
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý dịch vụ";
            ClientSize = new Size(1200, 760);
            StartPosition = FormStartPosition.CenterScreen;

            foreach (var num in new[]
            { numDonGia, numSoLuong, numGiaSuDung, numSoTien })
            {
                num.Maximum = 999999999999m;
                num.DecimalPlaces = 2;
                num.ThousandsSeparator = true;
            }

            foreach (var num in new[] { numThang, numThangHD })
            {
                num.Minimum = 1;
                num.Maximum = 12;
                num.Value = DateTime.Today.Month;
            }

            foreach (var num in new[] { numNam, numNamHD })
            {
                num.Minimum = 2000;
                num.Maximum = 2100;
                num.Value = DateTime.Today.Year;
            }

            foreach (var c in new[]
            { cboChiTiet, cboDichVu, cboChiTietHD,
              cboHoaDon, cboHinhThuc })
                c.DropDownStyle = ComboBoxStyle.DropDownList;

            cboHinhThuc.Items.AddRange(
                new object[] { "Tiền mặt", "Chuyển khoản" });
            cboHinhThuc.SelectedIndex = 0;

            dtNgayLap.Format = DateTimePickerFormat.Short;
            dtNgayThu.Format = DateTimePickerFormat.Short;

            var tabs = new TabControl { Dock = DockStyle.Fill };

            var t1 = new TabPage("Danh mục dịch vụ");
            var p1 = ThanhNhap();
            Them(p1, "Mã dịch vụ", txtMaDV);
            Them(p1, "Tên dịch vụ", txtTenDV);
            Them(p1, "Đơn vị", txtDonVi);
            Them(p1, "Đơn giá", numDonGia);
            var btnLuu = new Button { Text = "Lưu dịch vụ", Width = 130 };
            btnLuu.Click += (s, e) => LuuDichVu();
            p1.Controls.Add(btnLuu);
            GanTab(t1, dgvDichVu, p1);

            var t2 = new TabPage("Sử dụng dịch vụ");
            var p2 = ThanhNhap();
            Them(p2, "Mã sử dụng", txtMaSuDung);
            Them(p2, "Phòng thuê", cboChiTiet);
            Them(p2, "Dịch vụ", cboDichVu);
            Them(p2, "Tháng", numThang);
            Them(p2, "Năm", numNam);
            Them(p2, "Số lượng", numSoLuong);
            Them(p2, "Đơn giá", numGiaSuDung);
            var btnGhi = new Button { Text = "Ghi nhận", Width = 125 };
            btnGhi.Click += (s, e) => GhiNhan();
            p2.Controls.Add(btnGhi);
            GanTab(t2, dgvSuDung, p2);

            var t3 = new TabPage("Hóa đơn dịch vụ");
            var p3 = ThanhNhap();
            Them(p3, "Mã hóa đơn", txtMaHoaDon);
            Them(p3, "Phòng thuê", cboChiTietHD);
            Them(p3, "Tháng", numThangHD);
            Them(p3, "Năm", numNamHD);
            Them(p3, "Ngày lập", dtNgayLap);
            var btnLap = new Button { Text = "Lập hóa đơn", Width = 135 };
            btnLap.Click += (s, e) => LapHoaDon();
            p3.Controls.Add(btnLap);
            GanTab(t3, dgvHoaDon, p3);

            var t4 = new TabPage("Thanh toán dịch vụ");
            var p4 = ThanhNhap();
            Them(p4, "Mã phiếu thu", txtMaPhieuThu);
            Them(p4, "Hóa đơn", cboHoaDon);
            Them(p4, "Ngày thu", dtNgayThu);
            Them(p4, "Số tiền", numSoTien);
            Them(p4, "Hình thức", cboHinhThuc);
            var btnThu = new Button { Text = "Thu tiền", Width = 115 };
            btnThu.Click += (s, e) => ThuTien();
            p4.Controls.Add(btnThu);
            GanTab(t4, dgvPhieuThu, p4);

            tabs.TabPages.AddRange(new[] { t1, t2, t3, t4 });

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
                var dv = sv.List(sv.Tables[0]);
                dgvDichVu.DataSource = dv;

                cboDichVu.DataSource = dv.Copy();
                cboDichVu.DisplayMember = "TenDichVu";
                cboDichVu.ValueMember = "MaDichVu";

                var ct = Db.Query(
                    "SELECT MaChiTietHD FROM dbo.ChiTietHopDong");

                cboChiTiet.DataSource = ct;
                cboChiTiet.DisplayMember = "MaChiTietHD";
                cboChiTiet.ValueMember = "MaChiTietHD";

                cboChiTietHD.DataSource = ct.Copy();
                cboChiTietHD.DisplayMember = "MaChiTietHD";
                cboChiTietHD.ValueMember = "MaChiTietHD";

                dgvSuDung.DataSource = sv.List(sv.Tables[1]);

                var hd = sv.List(sv.Tables[2]);
                dgvHoaDon.DataSource = hd;

                cboHoaDon.DataSource = hd.Copy();
                cboHoaDon.DisplayMember = "MaHoaDonDV";
                cboHoaDon.ValueMember = "MaHoaDonDV";

                dgvPhieuThu.DataSource = sv.List(sv.Tables[4]);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuDichVu()
        {
            try
            {
                sv.Save(sv.Tables[0], new Dictionary<string, object>
                {
                    ["MaDichVu"] = txtMaDV.Text.Trim(),
                    ["TenDichVu"] = txtTenDV.Text.Trim(),
                    ["DonViTinh"] = txtDonVi.Text.Trim(),
                    ["DonGia"] = numDonGia.Value
                });
                MessageBox.Show("Lưu dịch vụ thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void GhiNhan()
        {
            try
            {
                sv.Save(sv.Tables[1], new Dictionary<string, object>
                {
                    ["MaSuDung"] = txtMaSuDung.Text.Trim(),
                    ["MaChiTietHD"] = Ma(cboChiTiet),
                    ["MaDichVu"] = Ma(cboDichVu),
                    ["Thang"] = (int)numThang.Value,
                    ["Nam"] = (int)numNam.Value,
                    ["SoLuong"] = numSoLuong.Value,
                    ["DonGia"] = numGiaSuDung.Value
                });
                MessageBox.Show("Ghi nhận dịch vụ thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LapHoaDon()
        {
            try
            {
                string kq = sv.LapHoaDonDichVu(
                    txtMaHoaDon.Text.Trim(),
                    Ma(cboChiTietHD),
                    (int)numThangHD.Value,
                    (int)numNamHD.Value,
                    dtNgayLap.Value);
                MessageBox.Show(kq);
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThuTien()
        {
            try
            {
                string kq = sv.ThuTien(
                    txtMaPhieuThu.Text.Trim(),
                    Ma(cboHoaDon),
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
