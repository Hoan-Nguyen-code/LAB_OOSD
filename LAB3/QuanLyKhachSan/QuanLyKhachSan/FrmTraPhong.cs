using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan
{
    public partial class FrmTraPhong : Form
    {
        private readonly TraPhongService s =
            new TraPhongService();

        private readonly DanhMucService dm =
            new DanhMucService();

        private readonly BindingList<DenBuItem> db =
            new BindingList<DenBuItem>();

        public FrmTraPhong()
        {
            InitializeComponent();

            // Nối sự kiện
            this.Load += Frm_Load;

            cboDat.SelectedIndexChanged +=
                cboDat_SelectedIndexChanged;

            dgvPhong.SelectionChanged +=
                dgvPhong_SelectionChanged;

            btnThemDB.Click +=
                btnThemDB_Click;

            btnLapDB.Click +=
                btnLapDB_Click;

            btnLapHD.Click +=
                btnLapHD_Click;

            dgvHD.SelectionChanged +=
                dgvHD_SelectionChanged;

            btnThanhToan.Click +=
                btnThanhToan_Click;

            btnTraPhong.Click +=
                btnTraPhong_Click;

            btnDong.Click +=
                btnDong_Click;
        }

        // =====================================
        // LOAD FORM
        // =====================================
        private void Frm_Load(object sender, EventArgs e)
        {
            // Phiếu đang ở
            cboDat.DataSource =
                s.LayPhieuDangO();

            cboDat.DisplayMember =
                "SoPhieuDat";

            cboDat.ValueMember =
                "SoPhieuDat";

            // Nhân viên lập đền bù
            cboNV.DataSource =
                dm.LayNhanVien();

            cboNV.DisplayMember =
                "HoTen";

            cboNV.ValueMember =
                "MaNV";

            // Nhân viên lập hóa đơn
            cboNV2.DataSource =
                dm.LayNhanVien();

            cboNV2.DisplayMember =
                "HoTen";

            cboNV2.ValueMember =
                "MaNV";

            // Hình thức thanh toán
            cboHT.Items.Clear();

            cboHT.Items.AddRange(
                new object[]
                {
                    "Tiền mặt",
                    "Chuyển khoản",
                    "Thẻ",
                    "Ví điện tử"
                });

            cboHT.SelectedIndex = 0;

            // Danh sách đền bù đang chọn
            dgvDBChon.DataSource = db;

            Tai();
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
        // TẢI DỮ LIỆU
        // =====================================
        private void Tai()
        {
            if (cboDat.SelectedValue != null)
            {
                string so = V(cboDat);

                dgvPhong.DataSource =
                    s.LayPhongTheoPhieu(so);
            }

            dgvHD.DataSource =
                s.LayHoaDon();
        }

        // =====================================
        // ĐỔI PHIẾU ĐANG Ở
        // =====================================
        private void cboDat_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cboDat.SelectedValue == null)
                return;

            dgvPhong.DataSource =
                s.LayPhongTheoPhieu(
                    V(cboDat)
                );
        }

        // =====================================
        // CHỌN PHÒNG -> NẠP TIỆN NGHI
        // =====================================
        private void dgvPhong_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvPhong.CurrentRow == null)
                return;

            if (dgvPhong.CurrentRow
                .Cells["SoPhong"].Value == null)
                return;

            txtPhong.Text =
                Convert.ToString(
                    dgvPhong.CurrentRow
                        .Cells["SoPhong"].Value
                );

            dgvTN.DataSource =
                s.LayTienNghiPhong(
                    txtPhong.Text
                );
        }

        // =====================================
        // THÊM TIỆN NGHI VÀO DANH SÁCH ĐỀN BÙ
        // =====================================
        private void btnThemDB_Click(
            object sender,
            EventArgs e)
        {
            if (dgvTN.CurrentRow == null)
            {
                MessageBox.Show(
                    "Hãy chọn tiện nghi bị hư hỏng hoặc mất.");
                return;
            }

            string ma =
                Convert.ToString(
                    dgvTN.CurrentRow
                        .Cells["MaTienNghi"].Value
                );

            string ten =
                Convert.ToString(
                    dgvTN.CurrentRow
                        .Cells["TenLoaiTN"].Value
                );

            if (string.IsNullOrWhiteSpace(
                txtMucDo.Text))
            {
                MessageBox.Show(
                    "Hãy nhập mức độ thiệt hại.");
                return;
            }

            foreach (DenBuItem x in db)
            {
                if (x.MaTienNghi == ma)
                {
                    MessageBox.Show(
                        "Tiện nghi đã có trong phiếu đền bù.");
                    return;
                }
            }

            db.Add(
                new DenBuItem
                {
                    MaTienNghi = ma,
                    TenLoaiTN = ten,
                    MucDoThietHai =
                        txtMucDo.Text.Trim(),
                    SoTien =
                        numDenBu.Value
                });
        }

        // =====================================
        // LẬP PHIẾU ĐỀN BÙ
        // =====================================
        private void btnLapDB_Click(
            object sender,
            EventArgs e)
        {
            KetQuaXuLy k =
                s.LapPhieuDenBu(
                    txtSoDB.Text.Trim(),
                    V(cboDat),
                    txtPhong.Text.Trim(),
                    DateTime.Now,
                    V(cboNV),
                    new List<DenBuItem>(db)
                );

            MessageBox.Show(
                k.ThongBao
            );

            if (k.ThanhCong)
            {
                db.Clear();
            }
        }

        // =====================================
        // LẬP HÓA ĐƠN
        // =====================================
        private void btnLapHD_Click(
            object sender,
            EventArgs e)
        {
            KetQuaXuLy k =
                s.LapHoaDon(
                    txtSoHD.Text.Trim(),
                    V(cboDat),
                    DateTime.Now,
                    V(cboNV2),
                    (int)numSoNgay.Value
                );

            MessageBox.Show(
                k.ThongBao
            );

            Tai();
        }

        // =====================================
        // CHỌN HÓA ĐƠN
        // =====================================
        private void dgvHD_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvHD.CurrentRow == null)
                return;

            if (dgvHD.CurrentRow
                .Cells["SoHoaDon"].Value == null)
                return;

            txtHDChon.Text =
                Convert.ToString(
                    dgvHD.CurrentRow
                        .Cells["SoHoaDon"].Value
                );
        }

        // =====================================
        // THANH TOÁN
        // =====================================
        private void btnThanhToan_Click(
            object sender,
            EventArgs e)
        {
            KetQuaXuLy k =
                s.ThanhToan(
                    txtMaTT.Text.Trim(),
                    txtHDChon.Text.Trim(),
                    DateTime.Now,
                    cboHT.Text,
                    numTienTT.Value
                );

            MessageBox.Show(
                k.ThongBao
            );

            Tai();
        }

        // =====================================
        // HOÀN TẤT TRẢ PHÒNG
        // =====================================
        private void btnTraPhong_Click(
            object sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Xác nhận hoàn tất trả phòng?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result != DialogResult.Yes)
                return;

            KetQuaXuLy k =
                s.TraPhong(
                    V(cboDat),
                    DateTime.Now
                );

            MessageBox.Show(
                k.ThongBao
            );

            if (k.ThanhCong)
            {
                // Nạp lại danh sách phiếu Đang ở
                cboDat.DataSource =
                    s.LayPhieuDangO();

                cboDat.DisplayMember =
                    "SoPhieuDat";

                cboDat.ValueMember =
                    "SoPhieuDat";

                Tai();
            }
        }

        // =====================================
        // ĐÓNG
        // =====================================
        private void btnDong_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}