using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService s = new DanhMucService();

        public FrmDanhMuc()
        {
            InitializeComponent();

            // Gắn các sự kiện
            this.Load += FrmDanhMuc_Load;

            btnThemKhu.Click += btnThemKhu_Click;
            btnThemNV.Click += btnThemNV_Click;
            btnThemLoaiTN.Click += btnThemLoaiTN_Click;
            btnThemDV.Click += btnThemDV_Click;
            btnThemQD.Click += btnThemQD_Click;
        }

        // =========================
        // LOAD FORM
        // =========================

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            Tai();
        }

        // =========================
        // TẢI DỮ LIỆU
        // =========================

        private void Tai()
        {
            dgvKhu.DataSource = s.LayKhuVuc();

            dgvNV.DataSource = s.LayNhanVien();

            dgvLoaiTN.DataSource = s.LayLoaiTienNghi();

            dgvDV.DataSource = s.LayDichVu();

            dgvQD.DataSource = s.LayQuyDinhDenBu();

            // ComboBox loại tiện nghi
            cboQDLoai.DataSource = s.LayLoaiTienNghi();
            cboQDLoai.DisplayMember = "TenLoaiTN";
            cboQDLoai.ValueMember = "MaLoaiTN";
        }

        // =========================
        // HIỂN THỊ KẾT QUẢ
        // =========================

        private void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);

            if (k.ThanhCong)
            {
                Tai();
            }
        }

        // =========================
        // THÊM KHU VỰC
        // =========================

        private void btnThemKhu_Click(object sender, EventArgs e)
        {
            H(
                s.ThemKhu(
                    txtKhuMa.Text.Trim(),
                    txtKhuTen.Text.Trim()
                )
            );
        }

        // =========================
        // THÊM NHÂN VIÊN
        // =========================

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            H(
                s.ThemNhanVien(
                    txtNVMa.Text.Trim(),
                    txtNVTen.Text.Trim(),
                    txtNVVaiTro.Text.Trim(),
                    txtNVSDT.Text.Trim()
                )
            );
        }

        // =========================
        // THÊM LOẠI TIỆN NGHI
        // =========================

        private void btnThemLoaiTN_Click(object sender, EventArgs e)
        {
            H(
                s.ThemLoaiTN(
                    txtLoaiMa.Text.Trim(),
                    txtLoaiTen.Text.Trim()
                )
            );
        }

        // =========================
        // THÊM DỊCH VỤ
        // =========================

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            H(
                s.ThemDichVu(
                    txtDVMa.Text.Trim(),
                    txtDVTen.Text.Trim(),
                    txtDVDVT.Text.Trim(),
                    numDVGia.Value
                )
            );
        }

        // =========================
        // THÊM QUY ĐỊNH ĐỀN BÙ
        // =========================

        private void btnThemQD_Click(object sender, EventArgs e)
        {
            string maLoai = "";

            if (cboQDLoai.SelectedValue != null)
            {
                maLoai = cboQDLoai.SelectedValue.ToString();
            }

            H(
                s.ThemQuyDinh(
                    txtQDMa.Text.Trim(),
                    maLoai,
                    txtQDMucDo.Text.Trim(),
                    numQDTien.Value
                )
            );
        }
    }
}