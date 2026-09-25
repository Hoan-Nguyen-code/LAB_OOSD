using System;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (FrmThongKe f = new FrmThongKe())
            {
                f.ShowDialog(this);
            }
        }
        private void btnTraPhong_Click(object sender, EventArgs e)
        {
            using (FrmTraPhong f = new FrmTraPhong())
            {
                f.ShowDialog(this);
            }
        }
        private void btnDichVu_Click(object sender, EventArgs e)
        {
            using (FrmDichVu f = new FrmDichVu())
            {
                f.ShowDialog(this);
            }
        }
        private void btnDatPhong_Click(
    object sender,
    EventArgs e)
        {
            using (FrmDatPhong f = new FrmDatPhong())
            {
                f.ShowDialog(this);
            }
        }
        private void btnPhongTienNghi_Click(object sender, EventArgs e)
        {
            using (FrmPhongTienNghi f = new FrmPhongTienNghi())
            {
                f.ShowDialog(this);
            }
        }
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            using (FrmDanhMuc f = new FrmDanhMuc())
            {
                f.ShowDialog(this);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn thoát chương trình?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}