using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using shopABC.Adapters;
using shopABC.Data;
using shopABC.Helpers;

namespace shopABC.Forms
{
    public partial class FrmSanPham : Form
    {
        private DataGridView dgvSanPham;
        private ComboBox cboNhomSanPham;
        private TextBox txtTimKiem;

        private Button btnTimKiem;
        private Button btnLamMoi;
        private Button btnXemChiTiet;
        private Button btnThemGioHang;
        private Button btnDong;

        public FrmSanPham()
        {
            InitializeComponent();
            TaoGiaoDien();
            LoadNhomSanPham();
            LoadSanPham();
        }

        private void TaoGiaoDien()
        {
            Text = "Danh sách sản phẩm";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 650);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // ==============================
            // TIÊU ĐỀ
            // ==============================
            Label lblTitle = new Label();
            lblTitle.Text = "DANH SÁCH SẢN PHẨM";
            lblTitle.Font =
                new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(360, 25);

            // ==============================
            // NHÓM SẢN PHẨM
            // ==============================
            Label lblNhom = new Label();
            lblNhom.Text = "Nhóm sản phẩm:";
            lblNhom.Font = new Font("Segoe UI", 10);
            lblNhom.AutoSize = true;
            lblNhom.Location = new Point(40, 90);

            cboNhomSanPham = new ComboBox();
            cboNhomSanPham.Location = new Point(165, 86);
            cboNhomSanPham.Size = new Size(230, 30);
            cboNhomSanPham.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboNhomSanPham.SelectedIndexChanged +=
                CboNhomSanPham_SelectedIndexChanged;

            // ==============================
            // TÌM KIẾM
            // ==============================
            Label lblTim = new Label();
            lblTim.Text = "Tìm kiếm:";
            lblTim.Font = new Font("Segoe UI", 10);
            lblTim.AutoSize = true;
            lblTim.Location = new Point(425, 90);

            txtTimKiem = new TextBox();
            txtTimKiem.Location = new Point(505, 86);
            txtTimKiem.Size = new Size(230, 30);

            btnTimKiem = new Button();
            btnTimKiem.Text = "TÌM";
            btnTimKiem.Location = new Point(750, 83);
            btnTimKiem.Size = new Size(100, 35);
            btnTimKiem.Click += BtnTimKiem_Click;

            btnLamMoi = new Button();
            btnLamMoi.Text = "LÀM MỚI";
            btnLamMoi.Location = new Point(865, 83);
            btnLamMoi.Size = new Size(120, 35);
            btnLamMoi.Click += BtnLamMoi_Click;

            // ==============================
            // DATAGRIDVIEW
            // ==============================
            dgvSanPham = new DataGridView();
            dgvSanPham.Location = new Point(40, 145);
            dgvSanPham.Size = new Size(1000, 360);
            dgvSanPham.ReadOnly = true;
            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.AllowUserToDeleteRows = false;
            dgvSanPham.MultiSelect = false;
            dgvSanPham.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvSanPham.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // Double click cũng xem chi tiết
            dgvSanPham.CellDoubleClick +=
                DgvSanPham_CellDoubleClick;

            // ==============================
            // XEM CHI TIẾT
            // ==============================
            btnXemChiTiet = new Button();
            btnXemChiTiet.Text = "XEM CHI TIẾT";
            btnXemChiTiet.Location = new Point(480, 535);
            btnXemChiTiet.Size = new Size(160, 45);
            btnXemChiTiet.Click +=
                BtnXemChiTiet_Click;

            // ==============================
            // THÊM GIỎ
            // ==============================
            btnThemGioHang = new Button();
            btnThemGioHang.Text = "THÊM VÀO GIỎ";
            btnThemGioHang.Location = new Point(660, 535);
            btnThemGioHang.Size = new Size(170, 45);
            btnThemGioHang.Click +=
                BtnThemGioHang_Click;

            // ==============================
            // ĐÓNG
            // ==============================
            btnDong = new Button();
            btnDong.Text = "ĐÓNG";
            btnDong.Location = new Point(850, 535);
            btnDong.Size = new Size(160, 45);
            btnDong.Click += (s, e) => Close();

            Controls.Add(lblTitle);
            Controls.Add(lblNhom);
            Controls.Add(cboNhomSanPham);
            Controls.Add(lblTim);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(btnLamMoi);
            Controls.Add(dgvSanPham);
            Controls.Add(btnXemChiTiet);
            Controls.Add(btnThemGioHang);
            Controls.Add(btnDong);
        }

        // =====================================
        // LOAD NHÓM
        // =====================================
        private void LoadNhomSanPham()
        {
            DataTable dt =
                ProductSystemAdapter.LayDanhSachNhom();

            // Thêm lựa chọn "Tất cả"
            DataRow row = dt.NewRow();
            row["MaNhom"] = "";
            row["TenNhom"] = "Tất cả sản phẩm";

            dt.Rows.InsertAt(row, 0);

            cboNhomSanPham.DataSource = dt;
            cboNhomSanPham.DisplayMember = "TenNhom";
            cboNhomSanPham.ValueMember = "MaNhom";

            cboNhomSanPham.SelectedIndex = 0;
        }

        // =====================================
        // LOAD SẢN PHẨM
        // =====================================
        private void LoadSanPham()
        {
            dgvSanPham.DataSource =
                ProductSystemAdapter.LayDanhSachSanPham();

            DinhDangBang();
        }

        // =====================================
        // LỌC THEO NHÓM
        // =====================================
        private void CboNhomSanPham_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cboNhomSanPham.SelectedValue == null)
                return;

            string maNhom =
                cboNhomSanPham.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(maNhom))
            {
                LoadSanPham();
            }
            else
            {
                dgvSanPham.DataSource =
                    ProductSystemAdapter
                        .LaySanPhamTheoNhom(maNhom);

                DinhDangBang();
            }
        }

        // =====================================
        // TÌM KIẾM
        // =====================================
        private void BtnTimKiem_Click(
            object sender,
            EventArgs e)
        {
            string tuKhoa =
                txtTimKiem.Text.Trim();

            if (tuKhoa == "")
            {
                LoadSanPham();
                return;
            }

            dgvSanPham.DataSource =
                ProductSystemAdapter
                    .TimKiemSanPham(tuKhoa);

            DinhDangBang();
        }

        // =====================================
        // LÀM MỚI
        // =====================================
        private void BtnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            txtTimKiem.Clear();
            cboNhomSanPham.SelectedIndex = 0;
            LoadSanPham();
        }

        // =====================================
        // XEM CHI TIẾT
        // =====================================
        private void BtnXemChiTiet_Click(
            object sender,
            EventArgs e)
        {
            MoChiTietSanPham();
        }

        private void DgvSanPham_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MoChiTietSanPham();
            }
        }

        private void MoChiTietSanPham()
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string maSanPham =
                dgvSanPham.CurrentRow
                    .Cells["MaSanPham"]
                    .Value.ToString();

            // Form này tạo ở bước kế tiếp
            FrmChiTietSanPham frm =
                new FrmChiTietSanPham(maSanPham);

            frm.ShowDialog();
        }

        // =====================================
        // THÊM VÀO GIỎ
        // =====================================
        private void BtnThemGioHang_Click(
            object sender,
            EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string maSanPham =
                dgvSanPham.CurrentRow
                    .Cells["MaSanPham"]
                    .Value.ToString();

            decimal donGia =
                Convert.ToDecimal(
                    dgvSanPham.CurrentRow
                        .Cells["GiaBan"]
                        .Value
                );

            // Kiểm tra tình trạng sản phẩm
            if (!ProductSystemAdapter
                .KiemTraConHang(maSanPham))
            {
                MessageBox.Show(
                    "Sản phẩm hiện đã hết hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Chưa đăng nhập → yêu cầu đăng nhập
            if (!Session.DaDangNhap)
            {
                MessageBox.Show(
                    "Vui lòng đăng nhập trước khi thêm sản phẩm vào giỏ hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                using (FrmDangNhap frm =
                    new FrmDangNhap())
                {
                    frm.ShowDialog();
                }

                if (!Session.DaDangNhap)
                    return;
            }

            try
            {
                // =========================
                // TÌM GIỎ HÀNG
                // =========================
                object result = Db.Scalar(
                    @"SELECT MaGioHang
                      FROM GIOHANG
                      WHERE MaKhachHang =
                            @MaKhachHang",

                    new SqlParameter(
                        "@MaKhachHang",
                        Session.MaKhachHang
                    )
                );

                int maGioHang;

                // =========================
                // CHƯA CÓ GIỎ → TẠO
                // =========================
                if (result == null ||
                    result == DBNull.Value)
                {
                    object newId = Db.Scalar(
                        @"INSERT INTO GIOHANG
                            (MaKhachHang, NgayTao)
                          OUTPUT INSERTED.MaGioHang
                          VALUES
                            (@MaKhachHang, GETDATE())",

                        new SqlParameter(
                            "@MaKhachHang",
                            Session.MaKhachHang
                        )
                    );

                    maGioHang =
                        Convert.ToInt32(newId);
                }
                else
                {
                    maGioHang =
                        Convert.ToInt32(result);
                }

                // =========================
                // SP ĐÃ CÓ TRONG GIỎ?
                // =========================
                int tonTai =
                    Convert.ToInt32(
                        Db.Scalar(
                            @"SELECT COUNT(*)
                              FROM CHITIETGIOHANG
                              WHERE MaGioHang =
                                    @MaGioHang
                                AND MaSanPham =
                                    @MaSanPham",

                            new SqlParameter(
                                "@MaGioHang",
                                maGioHang
                            ),

                            new SqlParameter(
                                "@MaSanPham",
                                maSanPham
                            )
                        )
                    );

                if (tonTai > 0)
                {
                    Db.Execute(
                        @"UPDATE CHITIETGIOHANG
                          SET SoLuong = SoLuong + 1
                          WHERE MaGioHang =
                                @MaGioHang
                            AND MaSanPham =
                                @MaSanPham",

                        new SqlParameter(
                            "@MaGioHang",
                            maGioHang
                        ),

                        new SqlParameter(
                            "@MaSanPham",
                            maSanPham
                        )
                    );
                }
                else
                {
                    Db.Execute(
                        @"INSERT INTO CHITIETGIOHANG
                            (
                                MaGioHang,
                                MaSanPham,
                                SoLuong,
                                DonGia
                            )
                          VALUES
                            (
                                @MaGioHang,
                                @MaSanPham,
                                1,
                                @DonGia
                            )",

                        new SqlParameter(
                            "@MaGioHang",
                            maGioHang
                        ),

                        new SqlParameter(
                            "@MaSanPham",
                            maSanPham
                        ),

                        new SqlParameter(
                            "@DonGia",
                            donGia
                        )
                    );
                }

                MessageBox.Show(
                    "Đã thêm sản phẩm vào giỏ hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm sản phẩm vào giỏ hàng!\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================
        // FORMAT
        // =====================================
        private void DinhDangBang()
        {
            if (dgvSanPham.Columns.Count == 0)
                return;

            dgvSanPham.Columns["MaSanPham"]
                .HeaderText = "Mã SP";

            dgvSanPham.Columns["TenSanPham"]
                .HeaderText = "Tên sản phẩm";

            dgvSanPham.Columns["NhaSanXuat"]
                .HeaderText = "Nhà sản xuất";

            dgvSanPham.Columns["GiaBan"]
                .HeaderText = "Giá bán";

            dgvSanPham.Columns["TinhTrang"]
                .HeaderText = "Tình trạng";

            dgvSanPham.Columns["TenNhom"]
                .HeaderText = "Nhóm sản phẩm";

            dgvSanPham.Columns["GiaBan"]
                .DefaultCellStyle.Format = "N0";

            // Không cần hiện mã nhóm cho người dùng
            dgvSanPham.Columns["MaNhom"].Visible = false;
        }
    }
}