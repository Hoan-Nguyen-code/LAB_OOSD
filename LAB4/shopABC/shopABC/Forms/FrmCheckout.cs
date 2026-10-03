using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using shopABC.Data;
using shopABC.Helpers;
using shopABC.Adapters;

namespace shopABC.Forms
{
    public partial class FrmCheckout : Form
    {
        private TextBox txtHoTen;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;

        private ComboBox cboLoaiGiaoHang;
        private ComboBox cboKhuVucGiaoHang;

        private ComboBox cboLoaiThe;
        private TextBox txtSoThe;
        private TextBox txtChuThe;
        private TextBox txtHetHan;
        private TextBox txtCSV;

        private Label lblTienHang;
        private Label lblPhiGiaoHang;
        private Label lblPhiGiaoDich;
        private Label lblTongThanhToan;

        private Button btnDatHang;
        private Button btnDong;

        private int maGioHang;

        private decimal tongTienHang;
        private decimal phiGiaoHang;
        private decimal phiGiaoDich;
        private decimal tongThanhToan;

        public FrmCheckout()
        {
            InitializeComponent();

            TaoGiaoDien();

            LoadThongTinKhachHang();
            LoadLoaiGiaoHang();
            LoadKhuVucGiaoHang();
            LoadTongTienHang();

            TinhTongThanhToan();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================
        private void TaoGiaoDien()
        {
            Text = "Checkout";
            StartPosition = FormStartPosition.CenterScreen;

            Size = new Size(800, 830);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // =================================================
            // TIÊU ĐỀ
            // =================================================
            Label lblTitle = new Label();

            lblTitle.Text = "CHECKOUT";

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    20,
                    FontStyle.Bold
                );

            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(320, 20);

            Controls.Add(lblTitle);

            // =================================================
            // NGƯỜI NHẬN
            // =================================================
            Label lblNguoiNhan = new Label();

            lblNguoiNhan.Text =
                "THÔNG TIN NGƯỜI NHẬN";

            lblNguoiNhan.Font =
                new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                );

            lblNguoiNhan.AutoSize = true;
            lblNguoiNhan.Location =
                new Point(50, 80);

            Controls.Add(lblNguoiNhan);

            Controls.Add(
                TaoLabel("Họ tên:", 50, 125)
            );

            txtHoTen =
                TaoTextBox(200, 120);

            Controls.Add(txtHoTen);


            Controls.Add(
                TaoLabel("Địa chỉ:", 50, 170)
            );

            txtDiaChi =
                TaoTextBox(200, 165);

            Controls.Add(txtDiaChi);


            Controls.Add(
                TaoLabel("Điện thoại:", 50, 215)
            );

            txtDienThoai =
                TaoTextBox(200, 210);

            Controls.Add(txtDienThoai);

            // =================================================
            // LOẠI GIAO HÀNG
            // =================================================
            Controls.Add(
                TaoLabel(
                    "Loại giao hàng:",
                    50,
                    265
                )
            );

            cboLoaiGiaoHang =
                new ComboBox();

            cboLoaiGiaoHang.Location =
                new Point(200, 260);

            cboLoaiGiaoHang.Size =
                new Size(300, 30);

            cboLoaiGiaoHang.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboLoaiGiaoHang.SelectedIndexChanged +=
                CboLoaiGiaoHang_SelectedIndexChanged;

            Controls.Add(cboLoaiGiaoHang);

            // =================================================
            // KHU VỰC
            // =================================================
            Controls.Add(
                TaoLabel(
                    "Khu vực:",
                    50,
                    310
                )
            );

            cboKhuVucGiaoHang =
                new ComboBox();

            cboKhuVucGiaoHang.Location =
                new Point(200, 305);

            cboKhuVucGiaoHang.Size =
                new Size(300, 30);

            cboKhuVucGiaoHang.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboKhuVucGiaoHang.SelectedIndexChanged +=
                CboKhuVucGiaoHang_SelectedIndexChanged;

            Controls.Add(cboKhuVucGiaoHang);

            // =================================================
            // THANH TOÁN
            // =================================================
            Label lblThanhToan = new Label();

            lblThanhToan.Text =
                "THÔNG TIN THANH TOÁN";

            lblThanhToan.Font =
                new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                );

            lblThanhToan.AutoSize = true;
            lblThanhToan.Location =
                new Point(50, 365);

            Controls.Add(lblThanhToan);

            // Loại thẻ
            Controls.Add(
                TaoLabel(
                    "Loại thẻ:",
                    50,
                    410
                )
            );

            cboLoaiThe =
                new ComboBox();

            cboLoaiThe.Location =
                new Point(200, 405);

            cboLoaiThe.Size =
                new Size(200, 30);

            cboLoaiThe.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboLoaiThe.Items.Add("VISA");
            cboLoaiThe.Items.Add("MasterCard");
            cboLoaiThe.Items.Add("Discover");
            cboLoaiThe.Items.Add("American Express");

            cboLoaiThe.SelectedIndex = 0;

            cboLoaiThe.SelectedIndexChanged +=
                CboLoaiThe_SelectedIndexChanged;

            Controls.Add(cboLoaiThe);

            // Số thẻ
            Controls.Add(
                TaoLabel(
                    "Số thẻ:",
                    50,
                    455
                )
            );

            txtSoThe =
                TaoTextBox(200, 450);

            Controls.Add(txtSoThe);

            // Chủ thẻ
            Controls.Add(
                TaoLabel(
                    "Chủ thẻ:",
                    50,
                    500
                )
            );

            txtChuThe =
                TaoTextBox(200, 495);

            Controls.Add(txtChuThe);

            // Hết hạn
            Controls.Add(
                TaoLabel(
                    "Hết hạn (MM/YY):",
                    50,
                    545
                )
            );

            txtHetHan =
                TaoTextBox(200, 540);

            txtHetHan.Size =
                new Size(120, 30);

            Controls.Add(txtHetHan);

            // CSV
            Controls.Add(
                TaoLabel(
                    "CSV:",
                    350,
                    545
                )
            );

            txtCSV =
                TaoTextBox(400, 540);

            txtCSV.Size =
                new Size(100, 30);

            txtCSV.UseSystemPasswordChar =
                true;

            Controls.Add(txtCSV);

            // =================================================
            // TIỀN
            // =================================================
            lblTienHang =
                new Label();

            lblTienHang.Font =
                new Font("Segoe UI", 10);

            lblTienHang.AutoSize = true;

            lblTienHang.Location =
                new Point(50, 595);

            Controls.Add(lblTienHang);


            lblPhiGiaoHang =
                new Label();

            lblPhiGiaoHang.Font =
                new Font("Segoe UI", 10);

            lblPhiGiaoHang.AutoSize = true;

            lblPhiGiaoHang.Location =
                new Point(50, 625);

            Controls.Add(lblPhiGiaoHang);


            lblPhiGiaoDich =
                new Label();

            lblPhiGiaoDich.Font =
                new Font("Segoe UI", 10);

            lblPhiGiaoDich.AutoSize = true;

            lblPhiGiaoDich.Location =
                new Point(50, 655);

            Controls.Add(lblPhiGiaoDich);


            lblTongThanhToan =
                new Label();

            lblTongThanhToan.Font =
                new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                );

            lblTongThanhToan.AutoSize = true;

            lblTongThanhToan.Location =
                new Point(50, 690);

            Controls.Add(lblTongThanhToan);

            // =================================================
            // BUTTON ĐẶT HÀNG
            // =================================================
            btnDatHang =
                new Button();

            btnDatHang.Text =
                "XÁC NHẬN ĐẶT HÀNG";

            btnDatHang.Size =
                new Size(190, 45);

            btnDatHang.Location =
                new Point(470, 635);

            btnDatHang.Click +=
                BtnDatHang_Click;

            Controls.Add(btnDatHang);

            // =================================================
            // BUTTON ĐÓNG
            // =================================================
            btnDong =
                new Button();

            btnDong.Text = "ĐÓNG";

            btnDong.Size =
                new Size(120, 45);

            btnDong.Location =
                new Point(540, 690);

            btnDong.Click +=
                (s, e) => Close();

            Controls.Add(btnDong);
        }

        // =====================================================
        // HELPER UI
        // =====================================================
        private Label TaoLabel(
            string text,
            int x,
            int y)
        {
            Label label =
                new Label();

            label.Text = text;

            label.Font =
                new Font("Segoe UI", 10);

            label.AutoSize = true;

            label.Location =
                new Point(x, y);

            return label;
        }

        private TextBox TaoTextBox(
            int x,
            int y)
        {
            TextBox txt =
                new TextBox();

            txt.Location =
                new Point(x, y);

            txt.Size =
                new Size(300, 30);

            return txt;
        }

        // =====================================================
        // LOAD KHÁCH HÀNG
        // =====================================================
        private void LoadThongTinKhachHang()
        {
            DataTable dt =
                Db.Query(
                    @"SELECT HoTen,
                             DiaChi,
                             DienThoai
                      FROM KHACHHANG
                      WHERE MaKhachHang =
                            @MaKhachHang",

                    new SqlParameter(
                        "@MaKhachHang",
                        Session.MaKhachHang
                    )
                );

            if (dt.Rows.Count > 0)
            {
                txtHoTen.Text =
                    dt.Rows[0]["HoTen"]
                        .ToString();

                txtDiaChi.Text =
                    dt.Rows[0]["DiaChi"]
                        .ToString();

                txtDienThoai.Text =
                    dt.Rows[0]["DienThoai"]
                        .ToString();
            }
        }

        // =====================================================
        // LOAD LOẠI GIAO HÀNG
        // =====================================================
        private void LoadLoaiGiaoHang()
        {
            DataTable dt =
                Db.Query(
                    @"SELECT MaLoai,
                             TenLoai,
                             PhiGiaoHang
                      FROM LOAIGIAOHANG
                      ORDER BY MaLoai"
                );

            cboLoaiGiaoHang.DataSource = dt;
            cboLoaiGiaoHang.DisplayMember = "TenLoai";
            cboLoaiGiaoHang.ValueMember = "MaLoai";
        }

        // =====================================================
        // LOAD KHU VỰC
        // =====================================================
        private void LoadKhuVucGiaoHang()
        {
            DataTable dt =
                Db.Query(
                    @"SELECT MaKhuVuc,
                             TenKhuVuc,
                             PhuPhi
                      FROM KHUVUCGIAOHANG
                      ORDER BY
                          CASE MaKhuVuc
                              WHEN 'NOITHANH' THEN 1
                              WHEN 'NGOAITP' THEN 2
                              WHEN 'TINHKHAC' THEN 3
                              ELSE 4
                          END"
                );

            cboKhuVucGiaoHang.DataSource = dt;
            cboKhuVucGiaoHang.DisplayMember = "TenKhuVuc";
            cboKhuVucGiaoHang.ValueMember = "MaKhuVuc";
        }

        // =====================================================
        // LOAD GIỎ HÀNG
        // =====================================================
        private void LoadTongTienHang()
        {
            object gio =
                Db.Scalar(
                    @"SELECT MaGioHang
                      FROM GIOHANG
                      WHERE MaKhachHang =
                            @MaKhachHang",

                    new SqlParameter(
                        "@MaKhachHang",
                        Session.MaKhachHang
                    )
                );

            if (gio == null ||
                gio == DBNull.Value)
            {
                maGioHang = 0;
                tongTienHang = 0;

                return;
            }

            maGioHang =
                Convert.ToInt32(gio);

            object tong =
                Db.Scalar(
                    @"SELECT ISNULL(
                            SUM(SoLuong * DonGia),
                            0
                      )
                      FROM CHITIETGIOHANG
                      WHERE MaGioHang =
                            @MaGioHang",

                    new SqlParameter(
                        "@MaGioHang",
                        maGioHang
                    )
                );

            tongTienHang =
                Convert.ToDecimal(tong);
        }

        // =====================================================
        // EVENTS
        // =====================================================
        private void CboLoaiGiaoHang_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            TinhTongThanhToan();
        }

        private void CboKhuVucGiaoHang_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            TinhTongThanhToan();
        }

        private void CboLoaiThe_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            TinhTongThanhToan();
        }

        // =====================================================
        // TÍNH TIỀN
        // =====================================================
        private void TinhTongThanhToan()
        {
            decimal phiLoaiGiaoHang = 0;
            decimal phuPhiKhuVuc = 0;

            int maLoai = 0;

            // Loại giao hàng
            if (cboLoaiGiaoHang != null &&
                cboLoaiGiaoHang.SelectedItem
                    is DataRowView rowLoai)
            {
                maLoai =
                    Convert.ToInt32(
                        rowLoai["MaLoai"]
                    );

                phiLoaiGiaoHang =
                    Convert.ToDecimal(
                        rowLoai["PhiGiaoHang"]
                    );
            }

            // Khu vực
            if (cboKhuVucGiaoHang != null &&
                cboKhuVucGiaoHang.SelectedItem
                    is DataRowView rowKhuVuc)
            {
                phuPhiKhuVuc =
                    Convert.ToDecimal(
                        rowKhuVuc["PhuPhi"]
                    );
            }

            phiGiaoHang =
                phiLoaiGiaoHang +
                phuPhiKhuVuc;

            // Express miễn phí >= 1 triệu
            if (maLoai == 2 &&
                tongTienHang >= 1000000)
            {
                phiGiaoHang = 0;
            }

            // Same-day miễn phí >= 5 triệu
            if (maLoai == 3 &&
                tongTienHang >= 5000000)
            {
                phiGiaoHang = 0;
            }

            // Phí giao dịch tính trên
            // tiền hàng + phí giao hàng
            decimal tienTruocPhiThe =
                tongTienHang +
                phiGiaoHang;

            phiGiaoDich =
                cboLoaiThe == null
                    ? 0
                    : PaymentAdapter
                        .TinhPhiGiaoDich(
                            cboLoaiThe.Text,
                            tienTruocPhiThe
                        );

            tongThanhToan =
                tienTruocPhiThe +
                phiGiaoDich;

            if (lblTienHang == null ||
                lblPhiGiaoHang == null ||
                lblPhiGiaoDich == null ||
                lblTongThanhToan == null)
            {
                return;
            }

            lblTienHang.Text =
                "Tiền hàng: " +
                tongTienHang.ToString("N0") +
                " VNĐ";

            lblPhiGiaoHang.Text =
                "Phí giao hàng: " +
                phiGiaoHang.ToString("N0") +
                " VNĐ";

            lblPhiGiaoDich.Text =
                "Phí giao dịch thẻ: " +
                phiGiaoDich.ToString("N0") +
                " VNĐ";

            lblTongThanhToan.Text =
                "TỔNG THANH TOÁN: " +
                tongThanhToan.ToString("N0") +
                " VNĐ";
        }

        // =====================================================
        // ĐẶT HÀNG
        // =====================================================
        private void BtnDatHang_Click(
            object sender,
            EventArgs e)
        {
            // Người nhận
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin người nhận.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Giỏ hàng
            if (maGioHang == 0 ||
                tongTienHang <= 0)
            {
                MessageBox.Show(
                    "Giỏ hàng không có sản phẩm.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cboLoaiGiaoHang.SelectedValue == null ||
                cboKhuVucGiaoHang.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn đầy đủ thông tin giao hàng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Tính lại lần cuối
            TinhTongThanhToan();

            // =================================================
            // ONLINE PAYMENT SERVICE
            // =================================================
            string thongBaoThanhToan;

            bool thanhToanThanhCong =
                PaymentAdapter.ThanhToan(
                    cboLoaiThe.Text,
                    txtSoThe.Text.Trim(),
                    txtChuThe.Text.Trim(),
                    txtHetHan.Text.Trim(),
                    txtCSV.Text.Trim(),
                    tongThanhToan,
                    out thongBaoThanhToan
                );

            if (!thanhToanThanhCong)
            {
                MessageBox.Show(
                    thongBaoThanhToan,
                    "Thanh toán thất bại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                // =============================================
                // 1. NGƯỜI NHẬN
                // =============================================
                object nguoiNhanId =
                    Db.Scalar(
                        @"INSERT INTO NGUOINHAN
                            (
                                HoTen,
                                DiaChi,
                                DienThoai
                            )
                          OUTPUT INSERTED.MaNguoiNhan
                          VALUES
                            (
                                @HoTen,
                                @DiaChi,
                                @DienThoai
                            )",

                        new SqlParameter(
                            "@HoTen",
                            txtHoTen.Text.Trim()
                        ),

                        new SqlParameter(
                            "@DiaChi",
                            txtDiaChi.Text.Trim()
                        ),

                        new SqlParameter(
                            "@DienThoai",
                            txtDienThoai.Text.Trim()
                        )
                    );

                int maNguoiNhan =
                    Convert.ToInt32(
                        nguoiNhanId
                    );

                // =============================================
                // 2. GIAO HÀNG
                // =============================================
                int maLoai =
                    Convert.ToInt32(
                        cboLoaiGiaoHang.SelectedValue
                    );

                string maKhuVuc =
                    cboKhuVucGiaoHang.SelectedValue
                        .ToString();

                // =============================================
                // 3. THẺ ĐÃ DÙNG
                // =============================================
                string soThe =
                    txtSoThe.Text.Trim();

                string bonSoCuoi =
                    soThe.Substring(
                        soThe.Length - 4
                    );

                string loaiThe =
                    cboLoaiThe.Text;

                // =============================================
                // 4. TẠO ĐƠN
                // =============================================
                object donHangId =
                    Db.Scalar(
                        @"INSERT INTO DONDATHANG
        (
            MaKhachHang,
            MaNguoiNhan,
            MaLoai,
            MaKhuVuc,
            ThoiDiemDat,
            TongTienHang,
            ChiPhiGiaoHang,
            PhiGiaoDich,
            TongThanhToan,
            LoaiThe,
            BonSoCuoi
        )
        OUTPUT INSERTED.MaDonHang
        VALUES
        (
            @MaKhachHang,
            @MaNguoiNhan,
            @MaLoai,
            @MaKhuVuc,
            GETDATE(),
            @TongTienHang,
            @ChiPhiGiaoHang,
            @PhiGiaoDich,
            @TongThanhToan,
            @LoaiThe,
            @BonSoCuoi
        )",

                        new SqlParameter(
                            "@MaKhachHang",
                            Session.MaKhachHang
                        ),

                        new SqlParameter(
                            "@MaNguoiNhan",
                            maNguoiNhan
                        ),

                        new SqlParameter(
                            "@MaLoai",
                            maLoai
                        ),

                        new SqlParameter(
                            "@MaKhuVuc",
                            maKhuVuc
                        ),

                        new SqlParameter(
                            "@TongTienHang",
                            tongTienHang
                        ),

                        new SqlParameter(
                            "@ChiPhiGiaoHang",
                            phiGiaoHang
                        ),

                        new SqlParameter(
                            "@PhiGiaoDich",
                            phiGiaoDich
                        ),

                        new SqlParameter(
                            "@TongThanhToan",
                            tongThanhToan
                        ),

                        new SqlParameter(
                            "@LoaiThe",
                            loaiThe
                        ),

                        new SqlParameter(
                            "@BonSoCuoi",
                            bonSoCuoi
                        )
                    );

                int maDonHang =
                    Convert.ToInt32(
                        donHangId
                    );

                // =============================================
                // 5. COPY GIỎ → CHI TIẾT ĐƠN
                // =============================================
                Db.Execute(
                    @"INSERT INTO CHITIETDONHANG
                        (
                            MaDonHang,
                            MaSanPham,
                            SoLuong,
                            DonGia
                        )

                      SELECT
                            @MaDonHang,
                            MaSanPham,
                            SoLuong,
                            DonGia

                      FROM CHITIETGIOHANG

                      WHERE MaGioHang =
                            @MaGioHang",

                    new SqlParameter(
                        "@MaDonHang",
                        maDonHang
                    ),

                    new SqlParameter(
                        "@MaGioHang",
                        maGioHang
                    )
                );

                // =============================================
                // 6. EMAIL KHÁCH HÀNG
                // =============================================
                object emailObj =
                    Db.Scalar(
                        @"SELECT Email
                          FROM KHACHHANG
                          WHERE MaKhachHang =
                                @MaKhachHang",

                        new SqlParameter(
                            "@MaKhachHang",
                            Session.MaKhachHang
                        )
                    );

                string email =
                    emailObj == null ||
                    emailObj == DBNull.Value
                        ? ""
                        : emailObj.ToString();

                string thongBaoEmail;

                EmailAdapter.GuiEmailXacNhan(
                    email,
                    maDonHang,
                    tongThanhToan,
                    out thongBaoEmail
                );

                // =============================================
                // 7. XÓA GIỎ
                // =============================================
                Db.Execute(
                    @"DELETE FROM CHITIETGIOHANG
                      WHERE MaGioHang =
                            @MaGioHang",

                    new SqlParameter(
                        "@MaGioHang",
                        maGioHang
                    )
                );

                // =============================================
                // 8. THÀNH CÔNG
                // =============================================
                MessageBox.Show(
                    "ĐẶT HÀNG THÀNH CÔNG!\n\n" +

                    "Mã đơn hàng: " +
                    maDonHang +

                    "\nKhu vực: " +
                    cboKhuVucGiaoHang.Text +

                    "\nLoại giao hàng: " +
                    cboLoaiGiaoHang.Text +

                    "\nPhí giao hàng: " +
                    phiGiaoHang.ToString("N0") +
                    " VNĐ" +

                    "\nPhí giao dịch: " +
                    phiGiaoDich.ToString("N0") +
                    " VNĐ" +

                    "\nTổng thanh toán: " +
                    tongThanhToan.ToString("N0") +
                    " VNĐ\n\n" +

                    thongBaoEmail,

                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể ghi nhận đơn hàng!\n" +
                    ex.Message,

                    "Lỗi",

                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}