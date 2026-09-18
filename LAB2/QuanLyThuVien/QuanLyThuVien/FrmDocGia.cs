using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmDocGia : Form
    {
        private readonly DocGiaService service =
            new DocGiaService();

        public FrmDocGia()
        {
            InitializeComponent();

            Load += FrmDocGia_Load;

            btnThem.Click += BtnThem_Click;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            btnCapThe.Click += BtnCapThe_Click;
            btnGiaHan.Click += BtnGiaHan_Click;

            btnTimKiem.Click += BtnTimKiem_Click;
            txtTimKiem.KeyDown += TxtTimKiem_KeyDown;

            dgvDocGia.SelectionChanged +=
                DgvDocGia_SelectionChanged;
        }

        // =========================================
        // LOAD FORM
        // =========================================

        private void FrmDocGia_Load(
            object sender,
            EventArgs e)
        {
            cboPhai.Items.Clear();

            cboPhai.Items.AddRange(
                new object[]
                {
                    "Nam",
                    "Nữ",
                    "Khác"
                });

            dtpNgayCap.Value =
                DateTime.Today;

            dtpHanSuDung.Value =
                DateTime.Today.AddYears(1);

            TaiDuLieu();

            LamMoi();
        }

        // =========================================
        // LOAD DANH SÁCH ĐỘC GIẢ
        // =========================================

        private void TaiDuLieu()
        {
            try
            {
                dgvDocGia.DataSource =
                    service.LayDanhSach();

                dgvDocGia.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách độc giả.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================
        // THÊM ĐỘC GIẢ
        // =========================================

        private void BtnThem_Click(
            object sender,
            EventArgs e)
        {
            if (!KiemTraNhapLieu())
                return;

            try
            {
                service.ThemDocGia(
                    txtMaDocGia.Text.Trim(),
                    txtHo.Text.Trim(),
                    txtTen.Text.Trim(),
                    dtpNgaySinh.Value,
                    cboPhai.Text,
                    txtSDT.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtAnh.Text.Trim());

                MessageBox.Show(
                    "Thêm độc giả thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDuLieu();
                LamMoi();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Không thể thêm độc giả.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================================
        // CẬP NHẬT ĐỘC GIẢ
        // =========================================

        private void BtnCapNhat_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtMaDocGia.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn độc giả cần cập nhật.");
                return;
            }

            if (!KiemTraNhapLieu())
                return;

            try
            {
                service.CapNhatDocGia(
                    txtMaDocGia.Text.Trim(),
                    txtHo.Text.Trim(),
                    txtTen.Text.Trim(),
                    dtpNgaySinh.Value,
                    cboPhai.Text,
                    txtSDT.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtAnh.Text.Trim());

                MessageBox.Show(
                    "Cập nhật độc giả thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDuLieu();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================
        // XÓA ĐỘC GIẢ
        // =========================================

        private void BtnXoa_Click(
            object sender,
            EventArgs e)
        {
            string maDocGia =
                txtMaDocGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                    maDocGia))
            {
                MessageBox.Show(
                    "Vui lòng chọn độc giả cần xóa.");
                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc muốn xóa độc giả " +
                    maDocGia + "?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                service.XoaDocGia(
                    maDocGia);

                MessageBox.Show(
                    "Xóa độc giả thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDuLieu();
                LamMoi();
            }
            catch (SqlException)
            {
                MessageBox.Show(
                    "Không thể xóa độc giả vì đang có thẻ hoặc dữ liệu mượn/trả liên quan.",
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================
        // CẤP THẺ
        // =========================================

        private void BtnCapThe_Click(
            object sender,
            EventArgs e)
        {
            string maDocGia =
                txtMaDocGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                    maDocGia))
            {
                MessageBox.Show(
                    "Vui lòng chọn độc giả trước khi cấp thẻ.");
                return;
            }

            if (dtpHanSuDung.Value.Date <
                dtpNgayCap.Value.Date)
            {
                MessageBox.Show(
                    "Hạn sử dụng không được trước ngày cấp.");
                return;
            }

            try
            {
                string maThe =
                    service.CapThe(
                        maDocGia,
                        dtpNgayCap.Value,
                        dtpHanSuDung.Value,
                        chkLePhi.Checked);

                MessageBox.Show(
                    "Cấp thẻ thành công.\n" +
                    "Mã thẻ: " + maThe,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadTheCuaDocGia(
                    maDocGia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Không thể cấp thẻ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================================
        // GIA HẠN THẺ
        // =========================================

        private void BtnGiaHan_Click(
            object sender,
            EventArgs e)
        {
            string maDocGia =
                txtMaDocGia.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                    maDocGia))
            {
                MessageBox.Show(
                    "Vui lòng chọn độc giả trước khi gia hạn.");
                return;
            }

            try
            {
                service.GiaHanThe(
                    maDocGia,
                    dtpHanSuDung.Value,
                    chkLePhi.Checked);

                MessageBox.Show(
                    "Gia hạn thẻ thành công đến " +
                    dtpHanSuDung.Value.ToString(
                        "dd/MM/yyyy") +
                    ".",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadTheCuaDocGia(
                    maDocGia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Không thể gia hạn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =========================================
        // TÌM KIẾM
        // =========================================

        private void BtnTimKiem_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                string tuKhoa =
                    txtTimKiem.Text.Trim();

                if (string.IsNullOrWhiteSpace(
                        tuKhoa))
                {
                    TaiDuLieu();
                    return;
                }

                dgvDocGia.DataSource =
                    service.TimDocGia(
                        tuKhoa);

                dgvDocGia.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tìm kiếm.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void TxtTimKiem_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnTimKiem_Click(
                    sender,
                    EventArgs.Empty);

                e.SuppressKeyPress = true;
            }
        }

        // =========================================
        // CHỌN ĐỘC GIẢ TRÊN GRID
        // =========================================

        private void DgvDocGia_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvDocGia.CurrentRow == null)
                return;

            if (dgvDocGia.SelectedRows.Count == 0)
                return;

            DataGridViewRow row =
                dgvDocGia.CurrentRow;

            if (row.Cells["MaDocGia"].Value == null)
                return;

            txtMaDocGia.Text =
                Convert.ToString(
                    row.Cells["MaDocGia"].Value);

            txtHo.Text =
                Convert.ToString(
                    row.Cells["Ho"].Value);

            txtTen.Text =
                Convert.ToString(
                    row.Cells["Ten"].Value);

            txtSDT.Text =
                Convert.ToString(
                    row.Cells["SoDienThoai"].Value);

            txtDiaChi.Text =
                Convert.ToString(
                    row.Cells["DiaChi"].Value);

            txtEmail.Text =
                Convert.ToString(
                    row.Cells["Email"].Value);

            txtAnh.Text =
                Convert.ToString(
                    row.Cells["Anh3x4"].Value);

            // Ngày sinh
            if (row.Cells["NgaySinh"].Value !=
                DBNull.Value)
            {
                dtpNgaySinh.Value =
                    Convert.ToDateTime(
                        row.Cells["NgaySinh"].Value);
            }

            // Phái
            string phai =
                Convert.ToString(
                    row.Cells["Phai"].Value);

            if (!string.IsNullOrWhiteSpace(phai))
            {
                int index =
                    cboPhai.Items.IndexOf(phai);

                if (index >= 0)
                    cboPhai.SelectedIndex = index;
            }

            // Khi chọn độc giả thì load thẻ
            LoadTheCuaDocGia(
                txtMaDocGia.Text.Trim());
        }

        // =========================================
        // LOAD THẺ CỦA ĐỘC GIẢ
        // =========================================

        private void LoadTheCuaDocGia(
            string maDocGia)
        {
            if (string.IsNullOrWhiteSpace(
                    maDocGia))
            {
                dgvThe.DataSource = null;
                return;
            }

            try
            {
                DataTable table =
                    service.LayDanhSachThe(
                        maDocGia);

                dgvThe.DataSource =
                    table;

                dgvThe.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải thông tin thẻ.\n" +
                    ex.Message);
            }
        }

        // =========================================
        // LÀM MỚI
        // =========================================

        private void BtnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            LamMoi();
            TaiDuLieu();
        }

        private void LamMoi()
        {
            txtMaDocGia.Clear();
            txtHo.Clear();
            txtTen.Clear();

            dtpNgaySinh.Value =
                DateTime.Today;

            if (cboPhai.Items.Count > 0)
                cboPhai.SelectedIndex = 0;

            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtAnh.Clear();

            dtpNgayCap.Value =
                DateTime.Today;

            dtpHanSuDung.Value =
                DateTime.Today.AddYears(1);

            chkLePhi.Checked = false;

            txtTimKiem.Clear();

            dgvThe.DataSource = null;

            dgvDocGia.ClearSelection();

            txtMaDocGia.Enabled = true;
            txtMaDocGia.Focus();
        }

        // =========================================
        // VALIDATION UI
        // =========================================

        private bool KiemTraNhapLieu()
        {
            if (string.IsNullOrWhiteSpace(
                    txtMaDocGia.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã độc giả.");

                txtMaDocGia.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                    txtHo.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập họ độc giả.");

                txtHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                    txtTen.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên độc giả.");

                txtTen.Focus();
                return false;
            }

            if (cboPhai.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn phái.");

                cboPhai.Focus();
                return false;
            }

            string email =
                txtEmail.Text.Trim();

            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!email.Contains("@") ||
                    !email.Contains("."))
                {
                    MessageBox.Show(
                        "Email không hợp lệ.");

                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}