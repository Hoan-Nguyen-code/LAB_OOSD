using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Services;
using QuanLyThuVien.Data;

namespace QuanLyThuVien
{
    public partial class FrmMuonTra : Form
    {
        private readonly MuonTraService service =
            new MuonTraService();

        private DataTable selectedBooks;

        public FrmMuonTra()
        {
            InitializeComponent();

            // Nối event bằng code
            this.Load += FrmMuonTra_Load;

            btnKiemTra.Click += btnKiemTra_Click;
            btnThemSach.Click += btnThemSach_Click;
            btnBoSach.Click += btnBoSach_Click;
            btnLapPhieu.Click += btnLapPhieu_Click;

            btnTaiSachMuon.Click += btnTaiSachMuon_Click;
            cboDocGiaTra.SelectedIndexChanged +=
                cboDocGiaTra_SelectedIndexChanged;

            btnTraSach.Click += btnTraSach_Click;
            btnDong.Click += btnDong_Click;
        }

        // =========================================================
        // LOAD FORM
        // =========================================================
        private void FrmMuonTra_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                TaiDocGia();
                TaiNhanVien();

                cboTinhTrang.Items.Clear();

                cboTinhTrang.Items.AddRange(
                    new object[]
                    {
                        "Bình thường",
                        "Rách/Hư hỏng",
                        "Mất"
                    });

                if (cboTinhTrang.Items.Count > 0)
                    cboTinhTrang.SelectedIndex = 0;

                dtNgayMuon.Value =
                    DateTime.Today;

                dtHenTra.Value =
                    DateTime.Today.AddDays(7);

                dtNgayTra.Value =
                    DateTime.Today;

                numPhiPhat.Value = 0;

                TaoBangChon();

                TaiSachCon();

                TaiSachDangMuon();

                lblTrangThai.Text =
                    "Chưa kiểm tra điều kiện mượn.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu: "
                    + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LOAD ĐỘC GIẢ
        // =========================================================
        private void TaiDocGia()
        {
            DataTable readers = Db.Query(
                @"SELECT
                      MaDocGia,
                      Ho,
                      Ten,
                      LTRIM(RTRIM(
                          ISNULL(Ho, '') + ' ' +
                          ISNULL(Ten, '')
                      )) AS HoTen
                  FROM DocGia
                  ORDER BY Ho, Ten");

            cboDocGia.DataSource =
                readers.Copy();

            cboDocGia.DisplayMember =
                "HoTen";

            cboDocGia.ValueMember =
                "MaDocGia";

            cboDocGiaTra.DataSource =
                readers.Copy();

            cboDocGiaTra.DisplayMember =
                "HoTen";

            cboDocGiaTra.ValueMember =
                "MaDocGia";
        }

        // =========================================================
        // LOAD NHÂN VIÊN
        // =========================================================
        private void TaiNhanVien()
        {
            DataTable staff = Db.Query(
                @"SELECT
                      MaNhanVien,
                      LTRIM(RTRIM(
                          ISNULL(Ho, '') + ' ' +
                          ISNULL(Ten, '')
                      )) AS HoTen
                  FROM NhanVien
                  ORDER BY MaNhanVien");

            cboNhanVienMuon.DataSource =
                staff.Copy();

            cboNhanVienMuon.DisplayMember =
                "MaNhanVien";

            cboNhanVienMuon.ValueMember =
                "MaNhanVien";

            cboNhanVienTra.DataSource =
                staff.Copy();

            cboNhanVienTra.DisplayMember =
                "MaNhanVien";

            cboNhanVienTra.ValueMember =
                "MaNhanVien";
        }

        // =========================================================
        // TẠO BẢNG SÁCH ĐÃ CHỌN
        // =========================================================
        private void TaoBangChon()
        {
            selectedBooks =
                new DataTable();

            selectedBooks.Columns.Add(
                "MaDauSach",
                typeof(string));

            selectedBooks.Columns.Add(
                "TenSach",
                typeof(string));

            dgvSachChon.DataSource =
                selectedBooks;
        }

        // =========================================================
        // LOAD SÁCH CÒN TRONG KHO
        // =========================================================
        private void TaiSachCon()
        {
            dgvSachCon.DataSource =
                Db.Query(
                    @"SELECT
                          MaDauSach,
                          TenSach,
                          SoLuongHienCo
                      FROM DauSach
                      WHERE SoLuongHienCo > 0
                      ORDER BY TenSach");
        }

        // =========================================================
        // MÃ ĐỘC GIẢ ĐANG CHỌN
        // =========================================================
        private string MaDocGiaMuon
        {
            get
            {
                if (cboDocGia.SelectedValue == null)
                    return "";

                return Convert.ToString(
                    cboDocGia.SelectedValue);
            }
        }

        private string MaDocGiaTra
        {
            get
            {
                if (cboDocGiaTra.SelectedValue == null)
                    return "";

                return Convert.ToString(
                    cboDocGiaTra.SelectedValue);
            }
        }

        // =========================================================
        // KIỂM TRA ĐIỀU KIỆN MƯỢN
        // =========================================================
        private void btnKiemTra_Click(
            object sender,
            EventArgs e)
        {
            int soSach =
                selectedBooks == null
                    ? 0
                    : selectedBooks.Rows.Count;

            KetQuaXuLy kq =
                service.KiemTraDieuKienMuon(
                    MaDocGiaMuon,
                    soSach);

            lblTrangThai.Text =
                kq.ThongBao;

            MessageBox.Show(
                kq.ThongBao,
                kq.ThanhCong
                    ? "Thông báo"
                    : "Không đủ điều kiện",
                MessageBoxButtons.OK,
                kq.ThanhCong
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Warning);
        }

        // =========================================================
        // THÊM SÁCH VÀO DANH SÁCH MƯỢN
        // =========================================================
        private void btnThemSach_Click(
            object sender,
            EventArgs e)
        {
            if (dgvSachCon.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn đầu sách cần mượn.");

                return;
            }

            string maSach =
                Convert.ToString(
                    dgvSachCon.CurrentRow.Cells[
                        "MaDauSach"].Value);

            string tenSach =
                Convert.ToString(
                    dgvSachCon.CurrentRow.Cells[
                        "TenSach"].Value);

            if (string.IsNullOrWhiteSpace(maSach))
                return;

            // Không cho chọn quá 3
            if (selectedBooks.Rows.Count >= 3)
            {
                MessageBox.Show(
                    "Một lần lập phiếu chỉ được chọn tối đa 3 đầu sách.");

                return;
            }

            // Không cho chọn trùng
            foreach (DataRow row
                in selectedBooks.Rows)
            {
                if (string.Equals(
                    Convert.ToString(
                        row["MaDauSach"]),
                    maSach,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "Đầu sách này đã được chọn.");

                    return;
                }
            }

            selectedBooks.Rows.Add(
                maSach,
                tenSach);

            lblTrangThai.Text =
                "Danh sách mượn đã thay đổi - hãy kiểm tra lại điều kiện.";
        }

        // =========================================================
        // BỎ SÁCH KHỎI DANH SÁCH CHỌN
        // =========================================================
        private void btnBoSach_Click(
            object sender,
            EventArgs e)
        {
            if (dgvSachChon.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sách cần bỏ.");

                return;
            }

            DataRowView rowView =
                dgvSachChon.CurrentRow
                    .DataBoundItem as DataRowView;

            if (rowView != null)
                rowView.Row.Delete();

            lblTrangThai.Text =
                "Danh sách mượn đã thay đổi - hãy kiểm tra lại điều kiện.";
        }

        // =========================================================
        // LẬP PHIẾU MƯỢN
        // =========================================================
        private void btnLapPhieu_Click(
            object sender,
            EventArgs e)
        {
            List<string> danhSachMaSach =
                new List<string>();

            foreach (DataRow row
                in selectedBooks.Rows)
            {
                if (row.RowState ==
                    DataRowState.Deleted)
                    continue;

                string ma =
                    Convert.ToString(
                        row["MaDauSach"]);

                if (!string.IsNullOrWhiteSpace(ma))
                    danhSachMaSach.Add(ma);
            }

            string maNhanVien =
                cboNhanVienMuon.SelectedValue == null
                    ? ""
                    : Convert.ToString(
                        cboNhanVienMuon.SelectedValue);

            KetQuaXuLy kq =
                service.LapPhieuMuon(
                    MaDocGiaMuon,
                    maNhanVien,
                    danhSachMaSach,
                    dtNgayMuon.Value,
                    dtHenTra.Value);

            MessageBox.Show(
                kq.ThongBao,
                kq.ThanhCong
                    ? "Thông báo"
                    : "Lỗi",
                MessageBoxButtons.OK,
                kq.ThanhCong
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Warning);

            if (kq.ThanhCong)
            {
                // Làm sạch danh sách chọn
                selectedBooks.Clear();

                // Tải lại tồn kho
                TaiSachCon();

                // Tải lại tab trả
                TaiSachDangMuon();

                lblTrangThai.Text =
                    "Lập phiếu mượn thành công.";
            }
        }

        // =========================================================
        // LOAD SÁCH ĐANG MƯỢN
        // =========================================================
        private void TaiSachDangMuon()
        {
            if (string.IsNullOrWhiteSpace(
                MaDocGiaTra))
            {
                dgvDangMuon.DataSource = null;
                return;
            }

            dgvDangMuon.DataSource =
                service.LaySachDangMuon(
                    MaDocGiaTra);
        }

        // =========================================================
        // ĐỔI ĐỘC GIẢ TAB TRẢ
        // =========================================================
        private void cboDocGiaTra_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            // Khi DataSource đang khởi tạo có thể event chạy sớm
            if (!IsHandleCreated)
                return;

            try
            {
                TaiSachDangMuon();
            }
            catch
            {
                // Người dùng vẫn có nút tải thủ công.
            }
        }

        // =========================================================
        // NÚT TẢI SÁCH ĐANG MƯỢN
        // =========================================================
        private void btnTaiSachMuon_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                TaiSachDangMuon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải sách đang mượn: "
                    + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // TRẢ SÁCH
        // =========================================================
        private void btnTraSach_Click(
            object sender,
            EventArgs e)
        {
            if (dgvDangMuon.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn sách cần trả.");

                return;
            }

            string maChiTiet =
                Convert.ToString(
                    dgvDangMuon.CurrentRow.Cells[
                        "MaChiTiet"].Value);

            string maNhanVien =
                cboNhanVienTra.SelectedValue == null
                    ? ""
                    : Convert.ToString(
                        cboNhanVienTra.SelectedValue);

            string tinhTrang =
                Convert.ToString(
                    cboTinhTrang.SelectedItem);

            decimal phiPhat =
                numPhiPhat.Value;

            DialogResult confirm =
                MessageBox.Show(
                    "Xác nhận trả đầu sách đang chọn?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            KetQuaXuLy kq =
                service.TraSach(
                    maChiTiet,
                    maNhanVien,
                    dtNgayTra.Value,
                    tinhTrang,
                    phiPhat);

            MessageBox.Show(
                kq.ThongBao,
                kq.ThanhCong
                    ? "Thông báo"
                    : "Lỗi",
                MessageBoxButtons.OK,
                kq.ThanhCong
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Warning);

            if (kq.ThanhCong)
            {
                numPhiPhat.Value = 0;

                cboTinhTrang.SelectedIndex = 0;

                TaiSachDangMuon();

                // Vì trả bình thường có thể tăng tồn
                TaiSachCon();
            }
        }

        // =========================================================
        // ĐÓNG FORM
        // =========================================================
        private void btnDong_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}