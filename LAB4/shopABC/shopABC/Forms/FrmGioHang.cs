using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using shopABC.Data;
using shopABC.Helpers;

namespace shopABC.Forms
{
    public partial class FrmGioHang : Form
    {
        private DataGridView dgvGioHang;
        private NumericUpDown nudSoLuong;
        private Label lblTongTien;
        private Button btnCapNhat;
        private Button btnXoa;
        private Button btnCheckout;
        private Button btnDong;

        private int maGioHang = 0;

        public FrmGioHang()
        {
            InitializeComponent();
            TaoGiaoDien();
            LoadGioHang();
        }

        private void TaoGiaoDien()
        {
            Text = "Giỏ hàng";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 620);

            // ===== TIÊU ĐỀ =====
            Label lblTitle = new Label();
            lblTitle.Text = "GIỎ HÀNG";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(410, 25);

            // ===== DATAGRIDVIEW =====
            dgvGioHang = new DataGridView();
            dgvGioHang.Location = new Point(40, 90);
            dgvGioHang.Size = new Size(900, 330);
            dgvGioHang.ReadOnly = true;
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.AllowUserToDeleteRows = false;
            dgvGioHang.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvGioHang.MultiSelect = false;
            dgvGioHang.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvGioHang.SelectionChanged +=
                DgvGioHang_SelectionChanged;

            // ===== SỐ LƯỢNG =====
            Label lblSoLuong = new Label();
            lblSoLuong.Text = "Số lượng:";
            lblSoLuong.Font = new Font("Segoe UI", 10);
            lblSoLuong.AutoSize = true;
            lblSoLuong.Location = new Point(40, 455);

            nudSoLuong = new NumericUpDown();
            nudSoLuong.Location = new Point(120, 450);
            nudSoLuong.Size = new Size(100, 30);
            nudSoLuong.Minimum = 1;
            nudSoLuong.Maximum = 100;

            // ===== CẬP NHẬT =====
            btnCapNhat = new Button();
            btnCapNhat.Text = "CẬP NHẬT";
            btnCapNhat.Location = new Point(250, 445);
            btnCapNhat.Size = new Size(130, 40);
            btnCapNhat.Click += BtnCapNhat_Click;

            // ===== XÓA =====
            btnXoa = new Button();
            btnXoa.Text = "XÓA SẢN PHẨM";
            btnXoa.Location = new Point(400, 445);
            btnXoa.Size = new Size(150, 40);
            btnXoa.Click += BtnXoa_Click;

            // ===== TỔNG TIỀN =====
            lblTongTien = new Label();
            lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            lblTongTien.Font =
                new Font("Segoe UI", 13, FontStyle.Bold);
            lblTongTien.AutoSize = true;
            lblTongTien.Location = new Point(40, 520);

            // ===== CHECKOUT =====
            btnCheckout = new Button();
            btnCheckout.Text = "CHECKOUT";
            btnCheckout.Location = new Point(620, 510);
            btnCheckout.Size = new Size(150, 45);

            //  làm checkout ở bước này
            btnCheckout.Click += (s, e) =>
            {
                if (dgvGioHang.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Giỏ hàng đang trống.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                FrmCheckout frm = new FrmCheckout();
                frm.ShowDialog();

                LoadGioHang();
            };

            // ===== ĐÓNG =====
            btnDong = new Button();
            btnDong.Text = "ĐÓNG";
            btnDong.Location = new Point(790, 510);
            btnDong.Size = new Size(150, 45);
            btnDong.Click += (s, e) => Close();

            Controls.Add(lblTitle);
            Controls.Add(dgvGioHang);
            Controls.Add(lblSoLuong);
            Controls.Add(nudSoLuong);
            Controls.Add(btnCapNhat);
            Controls.Add(btnXoa);
            Controls.Add(lblTongTien);
            Controls.Add(btnCheckout);
            Controls.Add(btnDong);
        }

        // =====================================
        // LOAD GIỎ HÀNG
        // =====================================
        private void LoadGioHang()
        {
            if (!Session.DaDangNhap)
            {
                MessageBox.Show(
                    "Vui lòng đăng nhập để xem giỏ hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                Close();
                return;
            }

            object result = Db.Scalar(
                @"SELECT MaGioHang
                  FROM GIOHANG
                  WHERE MaKhachHang = @MaKhachHang",

                new SqlParameter(
                    "@MaKhachHang",
                    Session.MaKhachHang
                )
            );

            // Khách chưa có giỏ hàng
            if (result == null || result == DBNull.Value)
            {
                maGioHang = 0;
                dgvGioHang.DataSource = null;
                lblTongTien.Text = "Tổng tiền: 0 VNĐ";
                return;
            }

            maGioHang = Convert.ToInt32(result);

            string sql = @"
                SELECT
                    CT.MaSanPham,
                    SP.TenSanPham,
                    CT.DonGia,
                    CT.SoLuong,
                    CT.DonGia * CT.SoLuong AS ThanhTien
                FROM CHITIETGIOHANG CT
                INNER JOIN SANPHAM SP
                    ON CT.MaSanPham = SP.MaSanPham
                WHERE CT.MaGioHang = @MaGioHang
                ORDER BY CT.MaSanPham";

            DataTable dt = Db.Query(
                sql,
                new SqlParameter(
                    "@MaGioHang",
                    maGioHang
                )
            );

            dgvGioHang.DataSource = dt;

            DinhDangBang();
            TinhTongTien();
        }

        // =====================================
        // KHI CHỌN SẢN PHẨM
        // =====================================
        private void DgvGioHang_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null)
                return;

            object value =
                dgvGioHang.CurrentRow.Cells["SoLuong"].Value;

            if (value != null &&
                value != DBNull.Value)
            {
                int soLuong = Convert.ToInt32(value);

                if (soLuong >= nudSoLuong.Minimum &&
                    soLuong <= nudSoLuong.Maximum)
                {
                    nudSoLuong.Value = soLuong;
                }
            }
        }

        // =====================================
        // CẬP NHẬT SỐ LƯỢNG
        // =====================================
        private void BtnCapNhat_Click(
            object sender,
            EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần cập nhật.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string maSanPham =
                dgvGioHang.CurrentRow
                    .Cells["MaSanPham"]
                    .Value.ToString();

            int soLuong = Convert.ToInt32(
                nudSoLuong.Value
            );

            try
            {
                Db.Execute(
                    @"UPDATE CHITIETGIOHANG
                      SET SoLuong = @SoLuong
                      WHERE MaGioHang = @MaGioHang
                        AND MaSanPham = @MaSanPham",

                    new SqlParameter(
                        "@SoLuong",
                        soLuong
                    ),

                    new SqlParameter(
                        "@MaGioHang",
                        maGioHang
                    ),

                    new SqlParameter(
                        "@MaSanPham",
                        maSanPham
                    )
                );

                MessageBox.Show(
                    "Cập nhật giỏ hàng thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadGioHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cập nhật thất bại!\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================
        // XÓA SẢN PHẨM
        // =====================================
        private void BtnXoa_Click(
            object sender,
            EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần xóa.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string maSanPham =
                dgvGioHang.CurrentRow
                    .Cells["MaSanPham"]
                    .Value.ToString();

            string tenSanPham =
                dgvGioHang.CurrentRow
                    .Cells["TenSanPham"]
                    .Value.ToString();

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa \"" +
                tenSanPham +
                "\" khỏi giỏ hàng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                Db.Execute(
                    @"DELETE FROM CHITIETGIOHANG
                      WHERE MaGioHang = @MaGioHang
                        AND MaSanPham = @MaSanPham",

                    new SqlParameter(
                        "@MaGioHang",
                        maGioHang
                    ),

                    new SqlParameter(
                        "@MaSanPham",
                        maSanPham
                    )
                );

                MessageBox.Show(
                    "Đã xóa sản phẩm khỏi giỏ hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadGioHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Xóa sản phẩm thất bại!\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================
        // TÍNH TỔNG TIỀN
        // =====================================
        private void TinhTongTien()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row
                in dgvGioHang.Rows)
            {
                if (row.Cells["ThanhTien"].Value != null &&
                    row.Cells["ThanhTien"].Value != DBNull.Value)
                {
                    tongTien += Convert.ToDecimal(
                        row.Cells["ThanhTien"].Value
                    );
                }
            }

            lblTongTien.Text =
                "Tổng tiền: " +
                tongTien.ToString("N0") +
                " VNĐ";
        }

        // =====================================
        // FORMAT BẢNG
        // =====================================
        private void DinhDangBang()
        {
            if (dgvGioHang.Columns.Count == 0)
                return;

            dgvGioHang.Columns["MaSanPham"]
                .HeaderText = "Mã SP";

            dgvGioHang.Columns["TenSanPham"]
                .HeaderText = "Tên sản phẩm";

            dgvGioHang.Columns["DonGia"]
                .HeaderText = "Đơn giá";

            dgvGioHang.Columns["SoLuong"]
                .HeaderText = "Số lượng";

            dgvGioHang.Columns["ThanhTien"]
                .HeaderText = "Thành tiền";

            dgvGioHang.Columns["DonGia"]
                .DefaultCellStyle.Format = "N0";

            dgvGioHang.Columns["ThanhTien"]
                .DefaultCellStyle.Format = "N0";
        }
    }
}