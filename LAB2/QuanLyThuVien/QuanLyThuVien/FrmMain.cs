using System;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            btnDanhMuc.Click += btnDanhMuc_Click;
            btnSach.Click += btnSach_Click;
            btnDocGia.Click += btnDocGia_Click;
            btnMuonTra.Click += btnMuonTra_Click;
            btnThongKe.Click += btnThongKe_Click;
            btnThoat.Click += btnThoat_Click;
        }

        private void btnDanhMuc_Click(
            object sender, EventArgs e)
        {
            using (FrmDanhMuc frm = new FrmDanhMuc())
            {
                frm.ShowDialog();
            }
        }

        private void btnSach_Click(
            object sender, EventArgs e)
        {
            using (FrmSach frm = new FrmSach())
            {
                frm.ShowDialog();
            }
        }

        private void btnDocGia_Click(
            object sender, EventArgs e)
        {
            using (FrmDocGia frm = new FrmDocGia())
            {
                frm.ShowDialog();
            }
        }

        private void btnMuonTra_Click(
            object sender, EventArgs e)
        {
            using (FrmMuonTra frm = new FrmMuonTra())
            {
                frm.ShowDialog();
            }
        }

        private void btnThongKe_Click(
            object sender, EventArgs e)
        {
            using (FrmThongKe frm = new FrmThongKe())
            {
                frm.ShowDialog();
            }
        }

        private void btnThoat_Click(
            object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}