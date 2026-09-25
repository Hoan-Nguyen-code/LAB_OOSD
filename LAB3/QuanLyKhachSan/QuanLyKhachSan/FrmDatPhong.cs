using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan
{
    public partial class FrmDatPhong : Form
    {
        private readonly DatPhongService s =
            new DatPhongService();

        private readonly DanhMucService dm =
            new DanhMucService();

        private readonly BindingList<PhongDatItem> chon =
            new BindingList<PhongDatItem>();

        public FrmDatPhong()
        {
            InitializeComponent();

            // Gắn sự kiện
            this.Load += Frm_Load;

            btnThemKhach.Click += btnThemKhach_Click;

            btnThemPhong.Click += btnThemPhong_Click;
            btnBoPhong.Click += btnBoPhong_Click;
            btnLapPhieu.Click += btnLapPhieu_Click;

            dgvPhieu.SelectionChanged +=
                dgvPhieu_SelectionChanged;

            btnThemNguoi.Click += btnThemNguoi_Click;
            btnNhanPhong.Click += btnNhanPhong_Click;
            btnNoShow.Click += btnNoShow_Click;
        }

        // =====================================
        // LOAD FORM
        // =====================================
        private void Frm_Load(
            object sender,
            EventArgs e)
        {
            // Khách hàng
            cboKhach.DataSource = s.LayKhach();
            cboKhach.DisplayMember = "HoTen";
            cboKhach.ValueMember = "MaKhach";

            // Nhân viên lễ tân
            cboNV.DataSource = dm.LayNhanVien();
            cboNV.DisplayMember = "HoTen";
            cboNV.ValueMember = "MaNV";

            // Kênh đặt phòng
            cboKenh.Items.Clear();

            cboKenh.Items.AddRange(
                new object[]
                {
                    "Điện thoại",
                    "Website",
                    "Trực tiếp"
                }
            );

            cboKenh.SelectedIndex = 0;

            // Danh sách phòng đang chọn
            dgvChon.DataSource = chon;

            Tai();
        }

        // =====================================
        // TẢI DỮ LIỆU
        // =====================================
        private void Tai()
        {
            dgvKhach.DataSource =
                s.LayKhach();

            dgvPhong.DataSource =
                s.LayPhong();

            dgvPhieu.DataSource =
                s.LayPhieuDat();

            // Refresh khách hàng trên ComboBox
            cboKhach.DataSource =
                s.LayKhach();

            cboKhach.DisplayMember =
                "HoTen";

            cboKhach.ValueMember =
                "MaKhach";
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
        // HIỂN THỊ KẾT QUẢ
        // =====================================
        private void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);

            if (k.ThanhCong)
            {
                Tai();
            }
        }

        // =====================================
        // THÊM KHÁCH HÀNG
        // =====================================
        private void btnThemKhach_Click(
            object sender,
            EventArgs e)
        {
            H(
                s.ThemKhach(
                    txtMaKH.Text.Trim(),
                    txtTenKH.Text.Trim(),
                    txtCMND.Text.Trim(),
                    txtQT.Text.Trim(),
                    txtSDT.Text.Trim()
                )
            );
        }

        // =====================================
        // THÊM PHÒNG VÀO PHIẾU
        // =====================================
        private void btnThemPhong_Click(
            object sender,
            EventArgs e)
        {
            if (dgvPhong.CurrentRow == null)
            {
                MessageBox.Show(
                    "Hãy chọn phòng."
                );

                return;
            }

            string phong = Convert.ToString(
                dgvPhong.CurrentRow
                        .Cells["SoPhong"]
                        .Value
            );

            // Không cho chọn cùng phòng 2 lần
            foreach (PhongDatItem x in chon)
            {
                if (x.SoPhong == phong)
                {
                    MessageBox.Show(
                        "Phòng đã có trong phiếu."
                    );

                    return;
                }
            }

            int soNguoi =
                (int)numSoNguoi.Value;

            decimal donGia =
                Convert.ToDecimal(
                    dgvPhong.CurrentRow
                            .Cells["DonGiaNgay"]
                            .Value
                );

            chon.Add(
                new PhongDatItem
                {
                    SoPhong = phong,
                    SoNguoi = soNguoi,
                    DonGiaNgay = donGia
                }
            );
        }

        // =====================================
        // BỎ PHÒNG KHỎI PHIẾU
        // =====================================
        private void btnBoPhong_Click(
            object sender,
            EventArgs e)
        {
            if (dgvChon.CurrentRow == null)
                return;

            int index =
                dgvChon.CurrentRow.Index;

            if (index >= 0 &&
                index < chon.Count)
            {
                chon.RemoveAt(index);
            }
        }

        // =====================================
        // LẬP PHIẾU ĐẶT PHÒNG
        // =====================================
        private void btnLapPhieu_Click(
            object sender,
            EventArgs e)
        {
            KetQuaXuLy k =
                s.TaoDatPhong(
                    txtSoPhieu.Text.Trim(),
                    V(cboKhach),
                    V(cboNV),
                    dtLap.Value,
                    dtNhan.Value,
                    dtTra.Value,
                    numCoc.Value,
                    cboKenh.Text,
                    new List<PhongDatItem>(chon)
                );

            H(k);

            // Chỉ xóa phòng đã chọn khi tạo thành công
            if (k.ThanhCong)
            {
                chon.Clear();
            }
        }

        // =====================================
        // CHỌN PHIẾU ĐẶT PHÒNG
        // =====================================
        private void dgvPhieu_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvPhieu.CurrentRow == null)
                return;

            if (dgvPhieu.CurrentRow
                        .Cells["SoPhieuDat"]
                        .Value == null)
                return;

            string so =
                Convert.ToString(
                    dgvPhieu.CurrentRow
                            .Cells["SoPhieuDat"]
                            .Value
                );

            txtPhieuChon.Text = so;

            dgvCT.DataSource =
                s.LayChiTiet(so);

            dgvNguoi.DataSource =
                s.LayNguoiLuuTru(so);
        }

        // =====================================
        // THÊM NGƯỜI LƯU TRÚ
        // =====================================
        private void btnThemNguoi_Click(
            object sender,
            EventArgs e)
        {
            KetQuaXuLy k =
                s.ThemNguoiLuuTru(
                    txtPhieuChon.Text.Trim(),
                    txtNguoiPhong.Text.Trim(),
                    txtNguoiTen.Text.Trim(),
                    txtNguoiCMND.Text.Trim(),
                    txtNguoiQT.Text.Trim()
                );

            MessageBox.Show(k.ThongBao);

            if (k.ThanhCong)
            {
                dgvNguoi.DataSource =
                    s.LayNguoiLuuTru(
                        txtPhieuChon.Text.Trim()
                    );

                Tai();
            }
        }

        // =====================================
        // NHẬN PHÒNG
        // =====================================
        private void btnNhanPhong_Click(
            object sender,
            EventArgs e)
        {
            H(
                s.NhanPhong(
                    txtPhieuChon.Text.Trim(),
                    DateTime.Now
                )
            );
        }

        // =====================================
        // NO-SHOW
        // =====================================
        private void btnNoShow_Click(
            object sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Xác nhận khách không đến nhận phòng?",
                    "Xác nhận No-show",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (result == DialogResult.Yes)
            {
                H(
                    s.DanhDauNoShow(
                        txtPhieuChon.Text.Trim()
                    )
                );
            }
        }
    }
}