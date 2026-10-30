
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmKhamBenh : Form
    {
        private readonly KhamBenhService service =
            new KhamBenhService();

        private ComboBox cboHoSo;
        private ComboBox cboBacSi;
        private ComboBox cboKhoa;
        private ComboBox cboLoaiKham;

        private TextBox txtTrieuChung;
        private TextBox txtNhietDo;
        private TextBox txtHuyetAp;
        private TextBox txtChanDoan;

        private DateTimePicker dtpNgayKham;
        private DataGridView dgvKhamBenh;

        private string maPhieuDangChon = "";

        public FrmKhamBenh()
        {
            InitializeComponent();
            TaoGiaoDien();

            Load += (s, e) =>
                FormHelper.Chay(TaiDuLieu);
        }

        private void TaoGiaoDien()
        {
            var panel = FormHelper.TaoGiaoDien(
                this,
                "QUẢN LÝ KHÁM BỆNH",
                out dgvKhamBenh);

            cboHoSo = FormHelper.Combo(
                panel, "Hồ sơ bệnh án:");

            cboBacSi = FormHelper.Combo(
                panel, "Bác sĩ:");

            cboKhoa = FormHelper.Combo(
                panel, "Khoa điều trị:");

            cboLoaiKham = FormHelper.Combo(
                panel, "Loại khám:");

            cboLoaiKham.Items.AddRange(
                new object[] { "THUONG", "DINHKY" });

            cboLoaiKham.SelectedIndex = 0;

            dtpNgayKham = FormHelper.Ngay(
                panel, "Ngày giờ khám:");

            txtTrieuChung = FormHelper.Text(
                panel, "Triệu chứng:");

            txtNhietDo = FormHelper.Text(
                panel, "Nhiệt độ:");

            txtHuyetAp = FormHelper.Text(
                panel, "Huyết áp:");

            txtChanDoan = FormHelper.Text(
                panel, "Chẩn đoán:");

            FormHelper.Nut(
                panel, "LẬP PHIẾU KHÁM", ThemPhieu);

            FormHelper.Nut(
                panel, "CẬP NHẬT KHÁM", CapNhat);

            FormHelper.Nut(
                panel, "LÀM MỚI", LamMoi);

            FormHelper.Nut(
                panel, "TẢI DANH SÁCH", TaiDanhSach);

            dgvKhamBenh.CellClick +=
                DgvKhamBenh_CellClick;

            cboBacSi.SelectedValueChanged += (s, e) =>
            {
                if (cboBacSi.SelectedItem is DataRowView row)
                {
                    string maKhoa =
                        Convert.ToString(row["MaKhoa"]);

                    if (!string.IsNullOrWhiteSpace(maKhoa))
                        cboKhoa.SelectedValue = maKhoa;
                }
            };
        }

        private void TaiDuLieu()
        {
            cboHoSo.DataSource = service.LayHoSo();
            cboHoSo.DisplayMember = "Ten";
            cboHoSo.ValueMember = "MaHoSo";

            cboKhoa.DataSource = service.LayKhoa();
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";

            cboBacSi.DataSource = service.LayBacSi();
            cboBacSi.DisplayMember = "HoTen";
            cboBacSi.ValueMember = "MaBS";

            TaiDanhSach();
        }

        private void TaiDanhSach()
        {
            dgvKhamBenh.DataSource =
                service.LayDanhSach();

            dgvKhamBenh.ClearSelection();
        }

        private decimal? LayNhietDo()
        {
            if (string.IsNullOrWhiteSpace(txtNhietDo.Text))
                return null;

            decimal nhietDo;

            if (!decimal.TryParse(
                txtNhietDo.Text.Trim(), out nhietDo))
            {
                throw new Exception(
                    "Nhiệt độ phải là giá trị số.");
            }

            return nhietDo;
        }

        private void ThemPhieu()
        {
            string maPhieu = service.LapPhieuKham(
                FormHelper.Chon(cboHoSo),
                FormHelper.Chon(cboBacSi),
                FormHelper.Chon(cboKhoa),
                dtpNgayKham.Value,
                cboLoaiKham.Text,
                txtTrieuChung.Text,
                LayNhietDo(),
                txtHuyetAp.Text,
                txtChanDoan.Text);

            MessageBox.Show(
                "Lập phiếu khám thành công!\n" +
                "Mã phiếu: " + maPhieu,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void CapNhat()
        {
            if (string.IsNullOrWhiteSpace(maPhieuDangChon))
            {
                throw new Exception(
                    "Vui lòng chọn phiếu khám trong bảng.");
            }

            service.CapNhatPhieuKham(
                maPhieuDangChon,
                txtTrieuChung.Text,
                LayNhietDo(),
                txtHuyetAp.Text,
                txtChanDoan.Text);

            MessageBox.Show(
                "Cập nhật phiếu khám thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LamMoi();
        }

        private void DgvKhamBenh_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvKhamBenh.Rows[e.RowIndex];

            maPhieuDangChon = Convert.ToString(
                row.Cells["MaPhieuKham"].Value);

            cboHoSo.SelectedValue = Convert.ToString(
                row.Cells["MaHoSo"].Value);

            cboBacSi.SelectedValue = Convert.ToString(
                row.Cells["MaBS"].Value);

            cboKhoa.SelectedValue = Convert.ToString(
                row.Cells["MaKhoa"].Value);

            cboLoaiKham.SelectedItem = Convert.ToString(
                row.Cells["LoaiKham"].Value);

            txtTrieuChung.Text = Convert.ToString(
                row.Cells["TrieuChung"].Value);

            txtNhietDo.Text = Convert.ToString(
                row.Cells["NhietDo"].Value);

            txtHuyetAp.Text = Convert.ToString(
                row.Cells["HuyetAp"].Value);

            txtChanDoan.Text = Convert.ToString(
                row.Cells["ChanDoan"].Value);

            object ngayKham =
                row.Cells["NgayGioKham"].Value;

            if (ngayKham != null &&
                ngayKham != DBNull.Value)
            {
                dtpNgayKham.Value =
                    Convert.ToDateTime(ngayKham);
            }
        }

        private void LamMoi()
        {
            maPhieuDangChon = "";

            txtTrieuChung.Clear();
            txtNhietDo.Clear();
            txtHuyetAp.Clear();
            txtChanDoan.Clear();

            dtpNgayKham.Value = DateTime.Now;
            cboLoaiKham.SelectedIndex = 0;

            TaiDanhSach();
        }
    }
}
