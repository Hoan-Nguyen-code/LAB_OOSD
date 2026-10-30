
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;
using QuanLyChoThueVanPhong.Data;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmTheXe : Form
    {
        private readonly TheXeService sv = new TheXeService();

        private TextBox txtMaXe = new TextBox();
        private ComboBox cboNhanVien = new ComboBox();
        private TextBox txtBienSo = new TextBox();
        private TextBox txtLoaiXe = new TextBox();
        private TextBox txtMauXe = new TextBox();
        private DataGridView dgvXe = new DataGridView();

        private TextBox txtMaThe = new TextBox();
        private ComboBox cboNhanVienThe = new ComboBox();
        private ComboBox cboPhuongTien = new ComboBox();
        private DateTimePicker dtNgayCap = new DateTimePicker();
        private ComboBox cboTrangThai = new ComboBox();
        private DataGridView dgvThe = new DataGridView();

        public FrmTheXe()
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

        private void GanTab(TabPage tab,
            FlowLayoutPanel top, DataGridView dgv)
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
            Text = "Quản lý phương tiện và thẻ giữ xe";
            ClientSize = new Size(1150, 720);
            StartPosition = FormStartPosition.CenterScreen;

            foreach (var c in new[]
            { cboNhanVien, cboNhanVienThe, cboPhuongTien, cboTrangThai })
                c.DropDownStyle = ComboBoxStyle.DropDownList;

            cboTrangThai.Items.AddRange(
                new object[] { "Đang sử dụng", "Đã hủy" });
            cboTrangThai.SelectedIndex = 0;
            dtNgayCap.Format = DateTimePickerFormat.Short;

            var tabs = new TabControl { Dock = DockStyle.Fill };
            var tabXe = new TabPage("Phương tiện");
            var tabThe = new TabPage("Thẻ giữ xe");

            var p1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 135,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            Them(p1, "Mã phương tiện", txtMaXe);
            Them(p1, "Nhân viên", cboNhanVien);
            Them(p1, "Biển số", txtBienSo);
            Them(p1, "Loại xe", txtLoaiXe);
            Them(p1, "Màu xe", txtMauXe);

            var btnXe = new Button
            {
                Text = "Lưu phương tiện",
                Width = 150
            };
            btnXe.Click += (s, e) => LuuXe();
            p1.Controls.Add(btnXe);

            var p2 = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 135,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            Them(p2, "Mã thẻ", txtMaThe);
            Them(p2, "Nhân viên", cboNhanVienThe);
            Them(p2, "Phương tiện", cboPhuongTien);
            Them(p2, "Ngày cấp", dtNgayCap);
            Them(p2, "Trạng thái", cboTrangThai);

            var btnThe = new Button
            {
                Text = "Lưu thẻ",
                Width = 120
            };
            btnThe.Click += (s, e) => LuuThe();
            p2.Controls.Add(btnThe);

            cboNhanVienThe.SelectedIndexChanged +=
                (s, e) => TaiPhuongTien();

            GanTab(tabXe, p1, dgvXe);
            GanTab(tabThe, p2, dgvThe);
            tabs.TabPages.Add(tabXe);
            tabs.TabPages.Add(tabThe);

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
                var nv = Db.Query(
                    @"SELECT MaNV, HoTen
                      FROM dbo.NhanVienVanPhong
                      WHERE TrangThai=N'Đang làm việc'");

                cboNhanVien.DataSource = nv;
                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "MaNV";

                cboNhanVienThe.DataSource = nv.Copy();
                cboNhanVienThe.DisplayMember = "HoTen";
                cboNhanVienThe.ValueMember = "MaNV";

                dgvXe.DataSource = sv.List(sv.Tables[0]);
                dgvThe.DataSource = sv.List(sv.Tables[1]);
                TaiPhuongTien();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void TaiPhuongTien()
        {
            try
            {
                string maNV = Convert.ToString(
                    cboNhanVienThe.SelectedValue);

                if (string.IsNullOrWhiteSpace(maNV)) return;

                cboPhuongTien.DataSource = Db.Query(
                    @"SELECT MaPhuongTien, BienSoXe
                      FROM dbo.PhuongTien
                      WHERE MaNV=@id",
                    Db.P("@id", maNV));

                cboPhuongTien.DisplayMember = "BienSoXe";
                cboPhuongTien.ValueMember = "MaPhuongTien";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuXe()
        {
            try
            {
                sv.Save(sv.Tables[0], new Dictionary<string, object>
                {
                    ["MaPhuongTien"] = txtMaXe.Text.Trim(),
                    ["MaNV"] = Convert.ToString(cboNhanVien.SelectedValue),
                    ["BienSoXe"] = txtBienSo.Text.Trim(),
                    ["LoaiXe"] = txtLoaiXe.Text.Trim(),
                    ["MauXe"] = txtMauXe.Text.Trim()
                });

                MessageBox.Show("Lưu phương tiện thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuThe()
        {
            try
            {
                sv.Save(sv.Tables[1], new Dictionary<string, object>
                {
                    ["MaThe"] = txtMaThe.Text.Trim(),
                    ["MaNV"] = Convert.ToString(cboNhanVienThe.SelectedValue),
                    ["MaPhuongTien"] =
                        Convert.ToString(cboPhuongTien.SelectedValue),
                    ["NgayCap"] = dtNgayCap.Value.Date,
                    ["TrangThai"] = cboTrangThai.Text
                });

                MessageBox.Show("Lưu thẻ giữ xe thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
