using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService service =
            new DanhMucService();

        public FrmDanhMuc()
        {
            InitializeComponent();

            Load += FrmDanhMuc_Load;

            // Nhân viên
            btnThemNV.Click += BtnThemNV_Click;
            btnSuaNV.Click += BtnSuaNV_Click;
            btnXoaNV.Click += BtnXoaNV_Click;
            dgvNhanVien.CellClick += DgvNhanVien_CellClick;

            // Thể loại
            btnThemTL.Click += BtnThemTL_Click;
            btnSuaTL.Click += BtnSuaTL_Click;
            btnXoaTL.Click += BtnXoaTL_Click;
            dgvTheLoai.CellClick += DgvTheLoai_CellClick;

            // Nhà xuất bản
            btnThemNXB.Click += BtnThemNXB_Click;
            btnSuaNXB.Click += BtnSuaNXB_Click;
            btnXoaNXB.Click += BtnXoaNXB_Click;
            dgvNXB.CellClick += DgvNXB_CellClick;
        }

        // =========================================
        // LOAD DỮ LIỆU
        // =========================================

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadTheLoai();
            LoadNhaXuatBan();
        }

        private void LoadNhanVien()
        {
            dgvNhanVien.DataSource =
                service.LayDanhSachNhanVien();
        }

        private void LoadTheLoai()
        {
            dgvTheLoai.DataSource =
                service.LayDanhSachTheLoai();
        }

        private void LoadNhaXuatBan()
        {
            dgvNXB.DataSource =
                service.LayDanhSachNhaXuatBan();
        }

        // =========================================
        // NHÂN VIÊN
        // =========================================

        private void BtnThemNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã và tên nhân viên.");
                return;
            }

            try
            {
                service.ThemNhanVien(
                    txtMaNV.Text.Trim(),
                    txtHo.Text.Trim(),
                    txtTen.Text.Trim(),
                    cboPhai.Text,
                    dtpNgaySinh.Value.Date,
                    txtChucVu.Text.Trim(),
                    txtSDT.Text.Trim());

                MessageBox.Show("Thêm nhân viên thành công.");

                LoadNhanVien();
                XoaTrangNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm nhân viên.\n" +
                    ex.Message);
            }
        }

        private void BtnSuaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên cần sửa.");
                return;
            }

            try
            {
                service.SuaNhanVien(
                    txtMaNV.Text.Trim(),
                    txtHo.Text.Trim(),
                    txtTen.Text.Trim(),
                    cboPhai.Text,
                    dtpNgaySinh.Value.Date,
                    txtChucVu.Text.Trim(),
                    txtSDT.Text.Trim());

                MessageBox.Show("Cập nhật nhân viên thành công.");

                LoadNhanVien();
                XoaTrangNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật nhân viên.\n" +
                    ex.Message);
            }
        }

        private void BtnXoaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                service.XoaNhanVien(txtMaNV.Text.Trim());

                MessageBox.Show("Xóa nhân viên thành công.");

                LoadNhanVien();
                XoaTrangNhanVien();
            }
            catch (SqlException)
            {
                MessageBox.Show(
                    "Không thể xóa nhân viên vì đang có dữ liệu liên quan.");
            }
        }

        private void DgvNhanVien_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvNhanVien.Rows[e.RowIndex];

            txtMaNV.Text =
                Convert.ToString(row.Cells["MaNhanVien"].Value);

            txtHo.Text =
                Convert.ToString(row.Cells["Ho"].Value);

            txtTen.Text =
                Convert.ToString(row.Cells["Ten"].Value);

            cboPhai.Text =
                Convert.ToString(row.Cells["Phai"].Value);

            if (row.Cells["NgaySinh"].Value != DBNull.Value)
            {
                dtpNgaySinh.Value =
                    Convert.ToDateTime(
                        row.Cells["NgaySinh"].Value);
            }

            txtChucVu.Text =
                Convert.ToString(row.Cells["ChucVu"].Value);

            txtSDT.Text =
                Convert.ToString(
                    row.Cells["SoDienThoai"].Value);
        }

        private void XoaTrangNhanVien()
        {
            txtMaNV.Clear();
            txtHo.Clear();
            txtTen.Clear();
            cboPhai.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Today;
            txtChucVu.Clear();
            txtSDT.Clear();
        }

        // =========================================
        // THỂ LOẠI
        // =========================================

        private void BtnThemTL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTheLoai.Text) ||
                string.IsNullOrWhiteSpace(txtTenTheLoai.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin thể loại.");
                return;
            }

            try
            {
                service.ThemTheLoai(
                    txtMaTheLoai.Text.Trim(),
                    txtTenTheLoai.Text.Trim());

                MessageBox.Show("Thêm thể loại thành công.");

                LoadTheLoai();
                XoaTrangTheLoai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm thể loại.\n" +
                    ex.Message);
            }
        }

        private void BtnSuaTL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTheLoai.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn thể loại cần sửa.");
                return;
            }

            try
            {
                service.SuaTheLoai(
                    txtMaTheLoai.Text.Trim(),
                    txtTenTheLoai.Text.Trim());

                MessageBox.Show("Cập nhật thể loại thành công.");

                LoadTheLoai();
                XoaTrangTheLoai();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật thể loại.\n" +
                    ex.Message);
            }
        }

        private void BtnXoaTL_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTheLoai.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn thể loại cần xóa.");
                return;
            }

            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa thể loại này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                service.XoaTheLoai(
                    txtMaTheLoai.Text.Trim());

                MessageBox.Show("Xóa thể loại thành công.");

                LoadTheLoai();
                XoaTrangTheLoai();
            }
            catch (SqlException)
            {
                MessageBox.Show(
                    "Không thể xóa thể loại vì đang có đầu sách thuộc thể loại này.");
            }
        }

        private void DgvTheLoai_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvTheLoai.Rows[e.RowIndex];

            txtMaTheLoai.Text =
                Convert.ToString(
                    row.Cells["MaTheLoai"].Value);

            txtTenTheLoai.Text =
                Convert.ToString(
                    row.Cells["TenTheLoai"].Value);
        }

        private void XoaTrangTheLoai()
        {
            txtMaTheLoai.Clear();
            txtTenTheLoai.Clear();
        }

        // =========================================
        // NHÀ XUẤT BẢN
        // =========================================

        private void BtnThemNXB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNXB.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã nhà xuất bản.");
                return;
            }

            try
            {
                service.ThemNhaXuatBan(
                    txtMaNXB.Text.Trim(),
                    txtDiaChiNXB.Text.Trim(),
                    txtSDTNXB.Text.Trim());

                MessageBox.Show(
                    "Thêm nhà xuất bản thành công.");

                LoadNhaXuatBan();
                XoaTrangNXB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm nhà xuất bản.\n" +
                    ex.Message);
            }
        }

        private void BtnSuaNXB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNXB.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhà xuất bản cần sửa.");
                return;
            }

            try
            {
                service.SuaNhaXuatBan(
                    txtMaNXB.Text.Trim(),
                    txtDiaChiNXB.Text.Trim(),
                    txtSDTNXB.Text.Trim());

                MessageBox.Show(
                    "Cập nhật nhà xuất bản thành công.");

                LoadNhaXuatBan();
                XoaTrangNXB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật nhà xuất bản.\n" +
                    ex.Message);
            }
        }

        private void BtnXoaNXB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNXB.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhà xuất bản cần xóa.");
                return;
            }

            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa nhà xuất bản này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                service.XoaNhaXuatBan(
                    txtMaNXB.Text.Trim());

                MessageBox.Show(
                    "Xóa nhà xuất bản thành công.");

                LoadNhaXuatBan();
                XoaTrangNXB();
            }
            catch (SqlException)
            {
                MessageBox.Show(
                    "Không thể xóa nhà xuất bản vì đang có đầu sách thuộc nhà xuất bản này.");
            }
        }

        private void DgvNXB_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvNXB.Rows[e.RowIndex];

            txtMaNXB.Text =
                Convert.ToString(
                    row.Cells["MaNhaXuatBan"].Value);

            txtDiaChiNXB.Text =
                Convert.ToString(
                    row.Cells["DiaChi"].Value);

            txtSDTNXB.Text =
                Convert.ToString(
                    row.Cells["SoDienThoai"].Value);
        }

        private void XoaTrangNXB()
        {
            txtMaNXB.Clear();
            txtDiaChiNXB.Clear();
            txtSDTNXB.Clear();
        }
    }
}