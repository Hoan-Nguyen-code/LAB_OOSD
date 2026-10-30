
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;
using QuanLyChoThueVanPhong.Data;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmKiemTraPhong : Form
    {
        private readonly KiemTraPhongService sv =
            new KiemTraPhongService();

        private TextBox txtMaKT = new TextBox();
        private ComboBox cboPhong = new ComboBox();
        private DateTimePicker dtNgayKT = new DateTimePicker();
        private TextBox txtTinhTrang = new TextBox();
        private TextBox txtKetQua = new TextBox();
        private TextBox txtNguoiKT = new TextBox();
        private DataGridView dgvKiemTra = new DataGridView();

        private TextBox txtMaSC = new TextBox();
        private ComboBox cboPhieuKT = new ComboBox();
        private DateTimePicker dtNgaySC = new DateTimePicker();
        private TextBox txtNoiDungSC = new TextBox();
        private NumericUpDown numChiPhi = new NumericUpDown();
        private ComboBox cboTrangThaiSC = new ComboBox();
        private DataGridView dgvSuaChua = new DataGridView();

        private TextBox txtMaPhat = new TextBox();
        private ComboBox cboPhieuKTPhat = new ComboBox();
        private ComboBox cboChiTietHD = new ComboBox();
        private DateTimePicker dtNgayPhat = new DateTimePicker();
        private TextBox txtLyDo = new TextBox();
        private NumericUpDown numTienPhat = new NumericUpDown();
        private DataGridView dgvXuPhat = new DataGridView();

        private TextBox txtMaThuPhat = new TextBox();
        private ComboBox cboPhieuPhat = new ComboBox();
        private DateTimePicker dtNgayThu = new DateTimePicker();
        private NumericUpDown numTienThu = new NumericUpDown();
        private ComboBox cboHinhThuc = new ComboBox();
        private DataGridView dgvThuPhat = new DataGridView();

        public FrmKiemTraPhong()
        {
            InitializeComponent();
            TaoGiaoDien();
            Load += (s, e) => Tai();
        }

        private string Ma(ComboBox c)
        {
            return Convert.ToString(c.SelectedValue) ?? "";
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

        private FlowLayoutPanel ThanhNhap()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 145,
                Padding = new Padding(10),
                AutoScroll = true
            };
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
            Text = "Kiểm tra, sửa chữa và xử phạt văn phòng";
            ClientSize = new Size(1200, 750);
            StartPosition = FormStartPosition.CenterScreen;

            foreach (var n in new[]
            { numChiPhi, numTienPhat, numTienThu })
            {
                n.Maximum = 999999999999m;
                n.ThousandsSeparator = true;
            }

            foreach (var c in new[]
            { cboPhong, cboPhieuKT, cboPhieuKTPhat,
              cboChiTietHD, cboPhieuPhat,
              cboTrangThaiSC, cboHinhThuc })
                c.DropDownStyle = ComboBoxStyle.DropDownList;

            cboTrangThaiSC.Items.AddRange(new object[]
            {
                "Chưa thực hiện", "Đang sửa", "Hoàn thành"
            });
            cboTrangThaiSC.SelectedIndex = 0;

            cboHinhThuc.Items.AddRange(new object[]
            {
                "Tiền mặt", "Chuyển khoản"
            });
            cboHinhThuc.SelectedIndex = 0;

            foreach (var dt in new[]
            { dtNgayKT, dtNgaySC, dtNgayPhat, dtNgayThu })
                dt.Format = DateTimePickerFormat.Short;

            var tabs = new TabControl { Dock = DockStyle.Fill };

            var t1 = new TabPage("Kiểm tra phòng");
            var p1 = ThanhNhap();
            Them(p1, "Mã phiếu KT", txtMaKT);
            Them(p1, "Phòng", cboPhong);
            Them(p1, "Ngày kiểm tra", dtNgayKT);
            Them(p1, "Tình trạng", txtTinhTrang);
            Them(p1, "Kết quả", txtKetQua);
            Them(p1, "Người kiểm tra", txtNguoiKT);
            var btnKT = new Button
            {
                Text = "Lưu kiểm tra",
                Width = 135
            };
            btnKT.Click += (s, e) => LuuKiemTra();
            p1.Controls.Add(btnKT);
            GanTab(t1, p1, dgvKiemTra);

            var t2 = new TabPage("Sửa chữa phòng");
            var p2 = ThanhNhap();
            Them(p2, "Mã phiếu SC", txtMaSC);
            Them(p2, "Phiếu kiểm tra", cboPhieuKT);
            Them(p2, "Ngày lập", dtNgaySC);
            Them(p2, "Nội dung", txtNoiDungSC);
            Them(p2, "Chi phí", numChiPhi);
            Them(p2, "Trạng thái", cboTrangThaiSC);
            var btnSC = new Button
            {
                Text = "Lưu sửa chữa",
                Width = 135
            };
            btnSC.Click += (s, e) => LuuSuaChua();
            p2.Controls.Add(btnSC);
            GanTab(t2, p2, dgvSuaChua);

            var t3 = new TabPage("Xử phạt");
            var p3 = ThanhNhap();
            Them(p3, "Mã phiếu phạt", txtMaPhat);
            Them(p3, "Phiếu kiểm tra", cboPhieuKTPhat);
            Them(p3, "Chi tiết thuê", cboChiTietHD);
            Them(p3, "Ngày lập", dtNgayPhat);
            Them(p3, "Lý do", txtLyDo);
            Them(p3, "Số tiền phạt", numTienPhat);
            var btnPhat = new Button
            {
                Text = "Lập phiếu phạt",
                Width = 145
            };
            btnPhat.Click += (s, e) => LuuXuPhat();
            p3.Controls.Add(btnPhat);
            GanTab(t3, p3, dgvXuPhat);

            cboPhieuKTPhat.SelectedIndexChanged +=
                (s, e) => TaiChiTietThue();

            var t4 = new TabPage("Thu tiền phạt");
            var p4 = ThanhNhap();
            Them(p4, "Mã phiếu thu", txtMaThuPhat);
            Them(p4, "Phiếu phạt", cboPhieuPhat);
            Them(p4, "Ngày thu", dtNgayThu);
            Them(p4, "Số tiền thu", numTienThu);
            Them(p4, "Hình thức", cboHinhThuc);
            var btnThu = new Button
            {
                Text = "Thu tiền phạt",
                Width = 145
            };
            btnThu.Click += (s, e) => ThuTienPhat();
            p4.Controls.Add(btnThu);
            GanTab(t4, p4, dgvThuPhat);

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
                cboPhong.DataSource = Db.Query(
                    "SELECT MaPhong FROM dbo.Phong");
                cboPhong.DisplayMember = "MaPhong";
                cboPhong.ValueMember = "MaPhong";

                var kt = sv.List(sv.Tables[0]);
                dgvKiemTra.DataSource = kt;

                cboPhieuKT.DataSource = kt.Copy();
                cboPhieuKT.DisplayMember = "MaPhieuKT";
                cboPhieuKT.ValueMember = "MaPhieuKT";

                cboPhieuKTPhat.DataSource = kt.Copy();
                cboPhieuKTPhat.DisplayMember = "MaPhieuKT";
                cboPhieuKTPhat.ValueMember = "MaPhieuKT";

                dgvSuaChua.DataSource = sv.List(sv.Tables[1]);

                var phat = sv.List(sv.Tables[2]);
                dgvXuPhat.DataSource = phat;

                cboPhieuPhat.DataSource = phat.Copy();
                cboPhieuPhat.DisplayMember = "MaPhieuPhat";
                cboPhieuPhat.ValueMember = "MaPhieuPhat";

                dgvThuPhat.DataSource = sv.List(sv.Tables[3]);
                TaiChiTietThue();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void TaiChiTietThue()
        {
            try
            {
                string ma = Ma(cboPhieuKTPhat);
                if (string.IsNullOrWhiteSpace(ma)) return;

                cboChiTietHD.DataSource = Db.Query(
                    @"SELECT ct.MaChiTietHD
                      FROM dbo.ChiTietHopDong ct
                      JOIN dbo.PhieuKiemTraPhong k
                        ON k.MaPhong=ct.MaPhong
                      WHERE k.MaPhieuKT=@ma
                        AND k.NgayKiemTra BETWEEN
                            ct.NgayBatDau AND ct.NgayKetThuc",
                    Db.P("@ma", ma));

                cboChiTietHD.DisplayMember = "MaChiTietHD";
                cboChiTietHD.ValueMember = "MaChiTietHD";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuKiemTra()
        {
            try
            {
                sv.Save(sv.Tables[0], new Dictionary<string, object>
                {
                    ["MaPhieuKT"] = txtMaKT.Text.Trim(),
                    ["MaPhong"] = Ma(cboPhong),
                    ["NgayKiemTra"] = dtNgayKT.Value.Date,
                    ["TinhTrang"] = txtTinhTrang.Text.Trim(),
                    ["KetQua"] = txtKetQua.Text.Trim(),
                    ["NguoiKiemTra"] = txtNguoiKT.Text.Trim()
                });
                MessageBox.Show("Lưu phiếu kiểm tra thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuSuaChua()
        {
            try
            {
                sv.Save(sv.Tables[1], new Dictionary<string, object>
                {
                    ["MaPhieuSC"] = txtMaSC.Text.Trim(),
                    ["MaPhieuKT"] = Ma(cboPhieuKT),
                    ["NgayLap"] = dtNgaySC.Value.Date,
                    ["NoiDungSuaChua"] = txtNoiDungSC.Text.Trim(),
                    ["ChiPhiDuKien"] = numChiPhi.Value,
                    ["TrangThai"] = cboTrangThaiSC.Text
                });
                MessageBox.Show("Lưu sửa chữa thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LuuXuPhat()
        {
            try
            {
                sv.Save(sv.Tables[2], new Dictionary<string, object>
                {
                    ["MaPhieuPhat"] = txtMaPhat.Text.Trim(),
                    ["MaPhieuKT"] = Ma(cboPhieuKTPhat),
                    ["MaChiTietHD"] = Ma(cboChiTietHD),
                    ["NgayLap"] = dtNgayPhat.Value.Date,
                    ["LyDo"] = txtLyDo.Text.Trim(),
                    ["SoTienPhat"] = numTienPhat.Value,
                    ["TrangThai"] = "Chưa thanh toán"
                });
                MessageBox.Show("Lập phiếu xử phạt thành công.");
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ThuTienPhat()
        {
            try
            {
                string kq = sv.ThuTienPhat(
                    txtMaThuPhat.Text.Trim(),
                    Ma(cboPhieuPhat),
                    dtNgayThu.Value,
                    numTienThu.Value,
                    cboHinhThuc.Text);
                MessageBox.Show(kq);
                Tai();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
