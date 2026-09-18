using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmSach : Form
    {
        private readonly SachService service =
            new SachService();

        public FrmSach()
        {
            InitializeComponent();

            Load += FrmSach_Load;

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            btnTimKiem.Click += BtnTimKiem_Click;
            txtTimKiem.KeyDown += TxtTimKiem_KeyDown;

            dgvSach.CellClick += DgvSach_CellClick;
        }

        // =========================================
        // LOAD FORM
        // =========================================

        private void FrmSach_Load(
            object sender,
            EventArgs e)
        {
            LoadTheLoai();
            LoadNhaXuatBan();
            LoadSach();
        }

        private void LoadSach()
        {
            try
            {
                dgvSach.DataSource =
                    service.LayDanhSach();

                dgvSach.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvSach.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvSach.MultiSelect = false;
                dgvSach.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách sách.\n" +
                    ex.Message);
            }
        }

        // =========================================
        // LOAD COMBOBOX THỂ LOẠI
        // =========================================

        private void LoadTheLoai()
        {
            try
            {
                cboTheLoai.DataSource =
                    service.LayTheLoai();

                cboTheLoai.DisplayMember =
                    "TenTheLoai";

                cboTheLoai.ValueMember =
                    "MaTheLoai";

                cboTheLoai.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải thể loại.\n" +
                    ex.Message);
            }
        }

        // =========================================
        // LOAD COMBOBOX NXB
        // =========================================

        private void LoadNhaXuatBan()
        {
            try
            {
                cboNXB.DataSource =
                    service.LayNhaXuatBan();

                cboNXB.DisplayMember =
                    "MaNhaXuatBan";

                cboNXB.ValueMember =
                    "MaNhaXuatBan";

                cboNXB.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải nhà xuất bản.\n" +
                    ex.Message);
            }
        }

        // =========================================
        // THÊM SÁCH
        // =========================================

        private void BtnThem_Click(
            object sender,
            EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            try
            {
                service.ThemSach(
                    txtMaSach.Text.Trim(),
                    txtTenSach.Text.Trim(),
                    Convert.ToInt32(numNamXB.Value),
                    Convert.ToInt32(numSoLuong.Value),
                    cboTheLoai.SelectedValue.ToString(),
                    cboNXB.SelectedValue.ToString());

                MessageBox.Show(
                    "Thêm đầu sách thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadSach();
                XoaTrang();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Không thể thêm đầu sách.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
        // SỬA SÁCH
        // =========================================

        private void BtnSua_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtMaSach.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn đầu sách cần sửa.");
                return;
            }

            if (!KiemTraDuLieu())
                return;

            try
            {
                service.SuaSach(
                    txtMaSach.Text.Trim(),
                    txtTenSach.Text.Trim(),
                    Convert.ToInt32(numNamXB.Value),
                    Convert.ToInt32(numSoLuong.Value),
                    cboTheLoai.SelectedValue.ToString(),
                    cboNXB.SelectedValue.ToString());

                MessageBox.Show(
                    "Cập nhật đầu sách thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật đầu sách.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================
        // XÓA SÁCH
        // =========================================

        private void BtnXoa_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtMaSach.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn đầu sách cần xóa.");
                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc muốn xóa đầu sách này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                service.XoaSach(
                    txtMaSach.Text.Trim());

                MessageBox.Show(
                    "Xóa đầu sách thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadSach();
                XoaTrang();
            }
            catch (SqlException)
            {
                MessageBox.Show(
                    "Không thể xóa đầu sách vì đang có dữ liệu mượn/trả liên quan.",
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

                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    LoadSach();
                    return;
                }

                dgvSach.DataSource =
                    service.TimSach(tuKhoa);
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

        // Nhấn Enter cũng tìm được
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
        // LÀM MỚI
        // =========================================

        private void BtnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            XoaTrang();
            LoadSach();
        }

        // =========================================
        // CLICK DÒNG TRONG DATAGRIDVIEW
        // =========================================

        private void DgvSach_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvSach.Rows[e.RowIndex];

            txtMaSach.Text =
                Convert.ToString(
                    row.Cells["MaDauSach"].Value);

            txtTenSach.Text =
                Convert.ToString(
                    row.Cells["TenSach"].Value);

            // Năm xuất bản
            if (row.Cells["NamXuatBan"].Value
                != DBNull.Value)
            {
                decimal nam =
                    Convert.ToDecimal(
                        row.Cells["NamXuatBan"].Value);

                if (nam >= numNamXB.Minimum &&
                    nam <= numNamXB.Maximum)
                {
                    numNamXB.Value = nam;
                }
            }

            // Số lượng
            if (row.Cells["SoLuongHienCo"].Value
                != DBNull.Value)
            {
                decimal soLuong =
                    Convert.ToDecimal(
                        row.Cells["SoLuongHienCo"].Value);

                if (soLuong >= numSoLuong.Minimum &&
                    soLuong <= numSoLuong.Maximum)
                {
                    numSoLuong.Value = soLuong;
                }
            }

            // Thể loại
            if (row.Cells["MaTheLoai"].Value
                != DBNull.Value)
            {
                cboTheLoai.SelectedValue =
                    Convert.ToString(
                        row.Cells["MaTheLoai"].Value);
            }
            else
            {
                cboTheLoai.SelectedIndex = -1;
            }

            // Nhà xuất bản
            if (row.Cells["MaNhaXuatBan"].Value
                != DBNull.Value)
            {
                cboNXB.SelectedValue =
                    Convert.ToString(
                        row.Cells["MaNhaXuatBan"].Value);
            }
            else
            {
                cboNXB.SelectedIndex = -1;
            }
        }

        // =========================================
        // KIỂM TRA DỮ LIỆU
        // =========================================

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(
                    txtMaSach.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã đầu sách.");

                txtMaSach.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                    txtTenSach.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sách.");

                txtTenSach.Focus();
                return false;
            }

            if (cboTheLoai.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn thể loại.");

                cboTheLoai.Focus();
                return false;
            }

            if (cboNXB.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhà xuất bản.");

                cboNXB.Focus();
                return false;
            }

            if (numSoLuong.Value < 0)
            {
                MessageBox.Show(
                    "Số lượng hiện có không được âm.");

                numSoLuong.Focus();
                return false;
            }

            return true;
        }

        // =========================================
        // XÓA TRẮNG
        // =========================================

        private void XoaTrang()
        {
            txtMaSach.Clear();
            txtTenSach.Clear();

            if (DateTime.Now.Year >= numNamXB.Minimum &&
                DateTime.Now.Year <= numNamXB.Maximum)
            {
                numNamXB.Value =
                    DateTime.Now.Year;
            }

            numSoLuong.Value = 0;

            cboTheLoai.SelectedIndex = -1;
            cboNXB.SelectedIndex = -1;

            txtTimKiem.Clear();

            dgvSach.ClearSelection();

            txtMaSach.Focus();
        }
    }
}