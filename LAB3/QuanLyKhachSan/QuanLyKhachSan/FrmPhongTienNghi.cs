using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan
{
    public partial class FrmPhongTienNghi : Form
    {
        private readonly PhongTienNghiService s =
            new PhongTienNghiService();

        private readonly DanhMucService dm =
            new DanhMucService();

        public FrmPhongTienNghi()
        {
            InitializeComponent();

            // Gắn sự kiện
            this.Load += Frm_Load;

            btnThemPhong.Click += btnThemPhong_Click;
            btnThemTN.Click += btnThemTN_Click;
            btnLapDat.Click += btnLapDat_Click;
        }

        // =========================
        // LOAD FORM
        // =========================

        private void Frm_Load(object sender, EventArgs e)
        {
            // Khu vực
            cboKhu.DataSource = dm.LayKhuVuc();
            cboKhu.DisplayMember = "TenKhuVuc";
            cboKhu.ValueMember = "MaKhuVuc";

            // Loại tiện nghi
            cboLoai.DataSource = dm.LayLoaiTienNghi();
            cboLoai.DisplayMember = "TenLoaiTN";
            cboLoai.ValueMember = "MaLoaiTN";

            // Tiện nghi
            cboTN.DataSource = s.LayTienNghi();
            cboTN.DisplayMember = "MaTienNghi";
            cboTN.ValueMember = "MaTienNghi";

            // Phòng
            cboPhong.DataSource = s.LayPhong();
            cboPhong.DisplayMember = "SoPhong";
            cboPhong.ValueMember = "SoPhong";

            // Nhân viên
            cboNV.DataSource = dm.LayNhanVien();
            cboNV.DisplayMember = "HoTen";
            cboNV.ValueMember = "MaNV";

            Tai();
        }

        // =========================
        // TẢI DỮ LIỆU
        // =========================

        private void Tai()
        {
            dgvPhong.DataSource = s.LayPhong();
            dgvTN.DataSource = s.LayTienNghi();
            dgvLD.DataSource = s.LayLapDat();

            // Refresh lại ComboBox vì có thể
            // vừa thêm phòng hoặc tiện nghi mới
            cboTN.DataSource = s.LayTienNghi();
            cboTN.DisplayMember = "MaTienNghi";
            cboTN.ValueMember = "MaTienNghi";

            cboPhong.DataSource = s.LayPhong();
            cboPhong.DisplayMember = "SoPhong";
            cboPhong.ValueMember = "SoPhong";
        }

        // =========================
        // LẤY VALUE COMBOBOX
        // =========================

        private string V(ComboBox c)
        {
            return c.SelectedValue == null
                ? ""
                : c.SelectedValue.ToString();
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
        // THÊM PHÒNG
        // =========================

        private void btnThemPhong_Click(
            object sender,
            EventArgs e)
        {
            H(
                s.ThemPhong(
                    txtPhong.Text.Trim(),
                    V(cboKhu),
                    (int)numMax.Value,
                    numGia.Value
                )
            );
        }

        // =========================
        // THÊM TIỆN NGHI
        // =========================

        private void btnThemTN_Click(
            object sender,
            EventArgs e)
        {
            H(
                s.ThemTienNghi(
                    txtMaTN.Text.Trim(),
                    V(cboLoai),
                    (int)numSTT.Value,
                    txtTinhTrang.Text.Trim()
                )
            );
        }

        // =========================
        // LẬP PHIẾU LẮP ĐẶT
        // =========================

        private void btnLapDat_Click(
            object sender,
            EventArgs e)
        {
            H(
                s.LapDat(
                    txtSoLD.Text.Trim(),
                    V(cboTN),
                    V(cboPhong),
                    dtNgay.Value,
                    txtTTLD.Text.Trim(),
                    V(cboNV),
                    txtGhiChu.Text.Trim()
                )
            );
        }
    }
}