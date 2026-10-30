
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;
using QuanLyChoThueVanPhong.Data;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmNhanVien : Form
    {
        private readonly NhanVienService sv = new NhanVienService();

        private TextBox txtMaNV = new TextBox();
        private TextBox txtHoTen = new TextBox();
        private ComboBox cboCongTy = new ComboBox();
        private ComboBox cboPhong = new ComboBox();
        private DateTimePicker dtNgaySinh = new DateTimePicker();
        private TextBox txtChucVu = new TextBox();
        private TextBox txtSDT = new TextBox();
        private TextBox txtAnh = new TextBox();
        private ComboBox cboTrangThai = new ComboBox();
        private PictureBox picAnh = new PictureBox();
        private DataGridView dgv = new DataGridView();

        public FrmNhanVien()
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

        private void TaoGiaoDien()
        {
            Text = "Quản lý nhân viên văn phòng";
            ClientSize = new Size(1200, 750);
            StartPosition = FormStartPosition.CenterScreen;

            cboCongTy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Items.AddRange(
                new object[] { "Đang làm việc", "Đã nghỉ" });
            cboTrangThai.SelectedIndex = 0;
            dtNgaySinh.Format = DateTimePickerFormat.Short;

            var top = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 185,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            Them(top, "Mã NV", txtMaNV);
            Them(top, "Họ tên", txtHoTen);
            Them(top, "Công ty", cboCongTy);
            Them(top, "Phòng", cboPhong);
            Them(top, "Ngày sinh", dtNgaySinh);
            Them(top, "Chức vụ", txtChucVu);
            Them(top, "Điện thoại", txtSDT);
            Them(top, "Trạng thái", cboTrangThai);
            Them(top, "Đường dẫn ảnh", txtAnh);

            var btnAnh = new Button
            {
                Text = "Chọn ảnh",
                Width = 110
            };
            btnAnh.Click += (s, e) => ChonAnh();

            var btnLuu = new Button
            {
                Text = "Lưu nhân viên",
                Width = 140
            };
            btnLuu.Click += (s, e) => Luu();

            top.Controls.Add(btnAnh);
            top.Controls.Add(btnLuu);

            cboCongTy.SelectedIndexChanged +=
                (s, e) => TaiPhong();

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 960
            };

            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
            split.Panel1.Controls.Add(dgv);

            picAnh.Dock = DockStyle.Top;
            picAnh.Height = 230;
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
            picAnh.BorderStyle = BorderStyle.FixedSingle;
            split.Panel2.Controls.Add(picAnh);

            dgv.SelectionChanged += (s, e) => HienThiNhanVien();

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

            Controls.Add(split);
            Controls.Add(top);
            Controls.Add(bottom);
        }

        private void Tai()
        {
            try
            {
                cboCongTy.DataSource = Db.Query(
                    @"SELECT MaKH, TenKhachHang
                      FROM dbo.KhachHang
                      ORDER BY MaKH");
                cboCongTy.DisplayMember = "TenKhachHang";
                cboCongTy.ValueMember = "MaKH";

                TaiPhong();
                dgv.DataSource = sv.List(sv.Tables[0]);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void TaiPhong()
        {
            try
            {
                string maKH = Convert.ToString(cboCongTy.SelectedValue);
                if (string.IsNullOrEmpty(maKH)) return;

                cboPhong.DataSource = Db.Query(
                    @"SELECT DISTINCT ct.MaPhong
                      FROM dbo.ChiTietHopDong ct
                      JOIN dbo.HopDongThue hd
                        ON hd.MaHopDong=ct.MaHopDong
                      WHERE hd.MaKH=@kh
                        AND hd.TrangThai=N'Có hiệu lực'",
                    Db.P("@kh", maKH));

                cboPhong.DisplayMember = "MaPhong";
                cboPhong.ValueMember = "MaPhong";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ChonAnh()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Hình ảnh|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialog.ShowDialog() != DialogResult.OK) return;
                txtAnh.Text = dialog.FileName;
                HienThiAnh(dialog.FileName);
            }
        }

        private void HienThiAnh(string path)
        {
            Image cu = picAnh.Image;
            picAnh.Image = null;
            if (cu != null) cu.Dispose();

            if (string.IsNullOrWhiteSpace(path) ||
                !File.Exists(path)) return;

            try
            {
                using (var fs = File.OpenRead(path))
                using (var img = Image.FromStream(fs))
                    picAnh.Image = new Bitmap(img);
            }
            catch { picAnh.Image = null; }
        }

        private void HienThiNhanVien()
        {
            if (dgv.CurrentRow == null) return;
            var r = dgv.CurrentRow;

            txtMaNV.Text = Convert.ToString(r.Cells["MaNV"].Value);
            txtHoTen.Text = Convert.ToString(r.Cells["HoTen"].Value);
            cboCongTy.SelectedValue = Convert.ToString(r.Cells["MaKH"].Value);
            cboPhong.SelectedValue = Convert.ToString(r.Cells["MaPhong"].Value);
            txtChucVu.Text = Convert.ToString(r.Cells["ChucVu"].Value);
            txtSDT.Text = Convert.ToString(r.Cells["DienThoai"].Value);
            txtAnh.Text = Convert.ToString(r.Cells["HinhAnh"].Value);
            cboTrangThai.Text = Convert.ToString(r.Cells["TrangThai"].Value);

            if (r.Cells["NgaySinh"].Value != DBNull.Value)
                dtNgaySinh.Value =
                    Convert.ToDateTime(r.Cells["NgaySinh"].Value);

            HienThiAnh(txtAnh.Text);
        }

        private void Luu()
        {
            try
            {
                sv.Save(sv.Tables[0], new Dictionary<string, object>
                {
                    ["MaNV"] = txtMaNV.Text.Trim(),
                    ["MaKH"] = Convert.ToString(cboCongTy.SelectedValue),
                    ["MaPhong"] = Convert.ToString(cboPhong.SelectedValue),
                    ["HoTen"] = txtHoTen.Text.Trim(),
                    ["NgaySinh"] = dtNgaySinh.Value.Date,
                    ["ChucVu"] = txtChucVu.Text.Trim(),
                    ["HinhAnh"] = txtAnh.Text.Trim(),
                    ["DienThoai"] = txtSDT.Text.Trim(),
                    ["TrangThai"] = cboTrangThai.Text
                });

                MessageBox.Show("Lưu nhân viên thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
