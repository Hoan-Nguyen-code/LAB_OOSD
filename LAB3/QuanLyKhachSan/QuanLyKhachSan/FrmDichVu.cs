using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan
{
    public partial class FrmDichVu : Form
    {
        private readonly DichVuService s =
            new DichVuService();

        private readonly DanhMucService dm =
            new DanhMucService();

        public FrmDichVu()
        {
            InitializeComponent();

            this.Load += Frm_Load;

            cboLuot.SelectedIndexChanged +=
                cboLuot_SelectedIndexChanged;

            btnGhi.Click +=
                btnGhi_Click;

            btnDong.Click +=
                btnDong_Click;
        }

        // =====================================
        // LOAD FORM
        // =====================================
        private void Frm_Load(object sender, EventArgs e)
        {
            // Các phiếu đang ở
            cboLuot.DataSource =
                s.LayPhieuDangO();

            cboLuot.DisplayMember =
                "SoPhieuDat";

            cboLuot.ValueMember =
                "SoPhieuDat";


            // Danh sách dịch vụ
            cboDV.DataSource =
                s.LayDichVu();

            cboDV.DisplayMember =
                "TenDV";

            cboDV.ValueMember =
                "MaDV";


            // Danh sách nhân viên
            cboNV.DataSource =
                dm.LayNhanVien();

            cboNV.DisplayMember =
                "HoTen";

            cboNV.ValueMember =
                "MaNV";


            // Ngày mặc định
            dtNgay.Value =
                DateTime.Today;

            Tai();
        }

        // =====================================
        // TẢI LỊCH SỬ
        // =====================================
        private void Tai()
        {
            if (cboLuot.SelectedValue != null)
            {
                dgvLichSu.DataSource =
                    s.LayLichSu(
                        cboLuot.SelectedValue.ToString()
                    );
            }
        }

        // =====================================
        // LẤY VALUE COMBOBOX
        // =====================================
        private string V(ComboBox c)
        {
            return c.SelectedValue == null
                ? ""
                : c.SelectedValue.ToString();
        }

        // =====================================
        // ĐỔI PHIẾU
        // =====================================
        private void cboLuot_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cboLuot.SelectedItem is DataRowView r)
            {
                txtPhong.Text =
                    Convert.ToString(
                        r["SoPhong"]
                    );
            }

            Tai();
        }

        // =====================================
        // GHI NHẬN DỊCH VỤ
        // =====================================
        private void btnGhi_Click(
            object sender,
            EventArgs e)
        {
            KetQuaXuLy k =
                s.GhiNhan(
                    V(cboLuot),
                    txtPhong.Text.Trim(),
                    dtNgay.Value,
                    V(cboNV),
                    V(cboDV),
                    (int)numSL.Value
                );

            MessageBox.Show(
                k.ThongBao
            );

            if (k.ThanhCong)
            {
                Tai();
            }
        }

        // =====================================
        // ĐÓNG FORM
        // =====================================
        private void btnDong_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}