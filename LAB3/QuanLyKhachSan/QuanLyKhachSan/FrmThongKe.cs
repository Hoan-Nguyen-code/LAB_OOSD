using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService s =
            new ThongKeService();

        public FrmThongKe()
        {
            InitializeComponent();

            btnTK.Click += btnTK_Click;
            btnDong.Click += btnDong_Click;

            dtTu.Value = DateTime.Today;
            dtDen.Value = DateTime.Today;
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if (dtDen.Value.Date < dtTu.Value.Date)
            {
                MessageBox.Show(
                    "Đến ngày không được trước từ ngày.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            dgvTongHop.DataSource =
                s.TongHop(dtTu.Value, dtDen.Value);

            dgvDV.DataSource =
                s.DichVu(dtTu.Value, dtDen.Value);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}