
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;
using QuanLyChoThueVanPhong.Data;

namespace QUANLYCHOTHUEVANPHONG.Forms
{
    public partial class FrmGiaHan : Form
    {
        private readonly GiaHanService sv = new GiaHanService();

        private TextBox txtMaPhieu = new TextBox();
        private ComboBox cboChiTiet = new ComboBox();
        private TextBox txtMaPhong = new TextBox();
        private TextBox txtMaHD = new TextBox();
        private TextBox txtThoiHan = new TextBox();
        private TextBox txtNgayKetThuc = new TextBox();
        private DateTimePicker dtNgayLap = new DateTimePicker();
        private NumericUpDown numSoThang = new NumericUpDown();
        private DataGridView dgvPhong = new DataGridView();
        private DataGridView dgvGiaHan = new DataGridView();

        public FrmGiaHan()
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
            c.Width = 160;
            p.Controls.Add(c);
        }

        private void TaoGiaoDien()
        {
            Text = "Gia hạn hợp đồng thuê văn phòng";
            ClientSize = new Size(1150, 740);
            StartPosition = FormStartPosition.CenterScreen;

            cboChiTiet.DropDownStyle = ComboBoxStyle.DropDownList;
            txtMaPhong.ReadOnly = true;
            txtMaHD.ReadOnly = true;
            txtThoiHan.ReadOnly = true;
            txtNgayKetThuc.ReadOnly = true;
            numSoThang.Minimum = 1;
            numSoThang.Maximum = 600;
            dtNgayLap.Format = DateTimePickerFormat.Short;

            var top = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 155,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            Them(top, "Mã phiếu gia hạn", txtMaPhieu);
            Them(top, "Chi tiết thuê", cboChiTiet);
            Them(top, "Phòng", txtMaPhong);
            Them(top, "Hợp đồng", txtMaHD);
            Them(top, "Thời hạn gốc", txtThoiHan);
            Them(top, "Kết thúc cũ", txtNgayKetThuc);
            Them(top, "Ngày lập", dtNgayLap);
            Them(top, "Số tháng gia hạn", numSoThang);

            var btnGiaHan = new Button
            {
                Text = "Lập phiếu gia hạn",
                Width = 155,
                Height = 32
            };
            btnGiaHan.Click += (s, e) => GiaHan();
            top.Controls.Add(btnGiaHan);

            cboChiTiet.SelectedIndexChanged +=
                (s, e) => HienThiChiTiet();

            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 240
            };

            foreach (var d in new[] { dgvPhong, dgvGiaHan })
            {
                d.Dock = DockStyle.Fill;
                d.ReadOnly = true;
                d.AllowUserToAddRows = false;
                d.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                d.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }

            split.Panel1.Controls.Add(dgvPhong);
            split.Panel2.Controls.Add(dgvGiaHan);

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
                DataTable dt = Db.Query(
                    @"SELECT ct.MaChiTietHD, ct.MaPhong,
                             ct.MaHopDong, ct.NgayKetThuc,
                             hd.ThoiHanThue
                      FROM dbo.ChiTietHopDong ct
                      JOIN dbo.HopDongThue hd
                        ON hd.MaHopDong=ct.MaHopDong
                      WHERE hd.TrangThai=N'Có hiệu lực'");

                dgvPhong.DataSource = dt;
                cboChiTiet.DataSource = dt.Copy();
                cboChiTiet.DisplayMember = "MaChiTietHD";
                cboChiTiet.ValueMember = "MaChiTietHD";

                dgvGiaHan.DataSource = sv.List(sv.Tables[0]);
                HienThiChiTiet();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void HienThiChiTiet()
        {
            DataRowView r = cboChiTiet.SelectedItem as DataRowView;
            if (r == null) return;

            txtMaPhong.Text = Convert.ToString(r["MaPhong"]);
            txtMaHD.Text = Convert.ToString(r["MaHopDong"]);

            int han = Convert.ToInt32(r["ThoiHanThue"]);
            txtThoiHan.Text = han.ToString();

            txtNgayKetThuc.Text =
                Convert.ToDateTime(r["NgayKetThuc"])
                .ToString("dd/MM/yyyy");

            numSoThang.Maximum = Math.Max(1, han / 2);
            if (numSoThang.Value > numSoThang.Maximum)
                numSoThang.Value = numSoThang.Maximum;
        }

        private void GiaHan()
        {
            try
            {
                string kq = sv.LapPhieuGiaHan(
                    txtMaPhieu.Text.Trim(),
                    Convert.ToString(cboChiTiet.SelectedValue),
                    dtNgayLap.Value,
                    (int)numSoThang.Value);

                MessageBox.Show(kq, "Gia hạn thành công");
                Tai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Không thể gia hạn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
