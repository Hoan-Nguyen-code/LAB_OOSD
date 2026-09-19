using System;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService service =
            new ThongKeService();

        public FrmThongKe()
        {
            InitializeComponent();

            this.Load += FrmThongKe_Load;

            btnThongKe.Click +=
                btnThongKe_Click;

            btnDong.Click +=
                btnDong_Click;
        }

        // =========================================================
        // LOAD FORM
        // =========================================================
        private void FrmThongKe_Load(
            object sender,
            EventArgs e)
        {
            // Ngày đầu tháng hiện tại
            dtTu.Value =
                new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    1);

            // Hôm nay
            dtDen.Value =
                DateTime.Today;

            TaiDuLieu();
        }

        // =========================================================
        // TẢI THỐNG KÊ
        // =========================================================
        private void TaiDuLieu()
        {
            try
            {
                ThongKeTongHop t =
                    service.LayTongHop(
                        dtTu.Value,
                        dtDen.Value);

                lblMuon.Text =
                    "Lượt sách mượn: "
                    + t.LuotSachMuon;

                lblQuaHan.Text =
                    "Sách quá hạn: "
                    + t.SachQuaHan;

                lblMat.Text =
                    "Sách mất: "
                    + t.SachMat;

                lblHuHong.Text =
                    "Sách hư hỏng: "
                    + t.SachHuHong;

                lblPhiPhat.Text =
                    "Tổng phí phạt: "
                    + t.TongPhiPhat.ToString("N0")
                    + " đ";

                dgvPhat.DataSource =
                    service.LayChiTietPhat(
                        dtTu.Value,
                        dtDen.Value);

                dgvPhat.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thống kê dữ liệu: "
                    + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // THỐNG KÊ
        // =========================================================
        private void btnThongKe_Click(
            object sender,
            EventArgs e)
        {
            TaiDuLieu();
        }

        // =========================================================
        // ĐÓNG
        // =========================================================
        private void btnDong_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}