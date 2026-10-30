
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmKhachHang : Form
    {
        private readonly KhachHangService sv = new KhachHangService();

        private TextBox txtMa = new TextBox();
        private TextBox txtTen = new TextBox();
        private ComboBox cboLoai = new ComboBox();
        private TextBox txtMST = new TextBox();
        private TextBox txtDaiDien = new TextBox();
        private TextBox txtSDT = new TextBox();
        private TextBox txtEmail = new TextBox();
        private TextBox txtDiaChi = new TextBox();
        private DataGridView dgv = new DataGridView();

        public FrmKhachHang()
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
                Margin = new Padding(8, 9, 3, 3)
            });
            c.Width = 170;
            p.Controls.Add(c);
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý khách hàng";
            ClientSize = new Size(1150, 700);
            StartPosition = FormStartPosition.CenterScreen;

            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.AddRange(new object[] { "Cá nhân", "Công ty" });
            cboLoai.SelectedIndex = 1;

            var top = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 170,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            Them(top, "Mã KH", txtMa);
            Them(top, "Tên khách hàng", txtTen);
            Them(top, "Loại KH", cboLoai);
            Them(top, "Mã số thuế", txtMST);
            Them(top, "Người đại diện", txtDaiDien);
            Them(top, "Điện thoại", txtSDT);
            Them(top, "Email", txtEmail);
            Them(top, "Địa chỉ", txtDiaChi);

            var btnLuu = new Button
            {
                Text = "Lưu / Cập nhật",
                Width = 145,
                Height = 32
            };
            btnLuu.Click += (s, e) => Luu();
            top.Controls.Add(btnLuu);

            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgv.SelectionChanged += (s, e) =>
            {
                if (dgv.CurrentRow == null) return;
                var r = dgv.CurrentRow;
                txtMa.Text = Convert.ToString(r.Cells["MaKH"].Value);
                txtTen.Text = Convert.ToString(r.Cells["TenKhachHang"].Value);
                cboLoai.Text = Convert.ToString(r.Cells["LoaiKhachHang"].Value);
                txtMST.Text = Convert.ToString(r.Cells["MaSoThue"].Value);
                txtDaiDien.Text = Convert.ToString(r.Cells["NguoiDaiDien"].Value);
                txtSDT.Text = Convert.ToString(r.Cells["DienThoai"].Value);
                txtEmail.Text = Convert.ToString(r.Cells["Email"].Value);
                txtDiaChi.Text = Convert.ToString(r.Cells["DiaChi"].Value);
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

            Controls.Add(dgv);
            Controls.Add(top);
            Controls.Add(bottom);
        }

        private void Tai()
        {
            try { dgv.DataSource = sv.List(sv.Tables[0]); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Luu()
        {
            try
            {
                sv.Save(sv.Tables[0], new Dictionary<string, object>
                {
                    ["MaKH"] = txtMa.Text.Trim(),
                    ["TenKhachHang"] = txtTen.Text.Trim(),
                    ["LoaiKhachHang"] = cboLoai.Text,
                    ["MaSoThue"] = txtMST.Text.Trim(),
                    ["NguoiDaiDien"] = txtDaiDien.Text.Trim(),
                    ["DienThoai"] = txtSDT.Text.Trim(),
                    ["Email"] = txtEmail.Text.Trim(),
                    ["DiaChi"] = txtDiaChi.Text.Trim()
                });
                MessageBox.Show("Lưu khách hàng thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
