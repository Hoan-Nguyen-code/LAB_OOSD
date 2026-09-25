namespace QuanLyKhachSan
{
    partial class FrmDatPhong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabKhach = new System.Windows.Forms.TabPage();
            this.tabDatPhong = new System.Windows.Forms.TabPage();
            this.tabNhanPhong = new System.Windows.Forms.TabPage();

            // =====================
            // TAB KHÁCH HÀNG
            // =====================
            this.dgvKhach = new System.Windows.Forms.DataGridView();

            this.lblMaKH = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.lblCMND = new System.Windows.Forms.Label();
            this.lblQT = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();

            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.txtQT = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();

            this.btnThemKhach = new System.Windows.Forms.Button();

            // =====================
            // TAB ĐẶT PHÒNG
            // =====================
            this.lblSoPhieu = new System.Windows.Forms.Label();
            this.lblKhach = new System.Windows.Forms.Label();
            this.lblNV = new System.Windows.Forms.Label();
            this.lblKenh = new System.Windows.Forms.Label();
            this.lblLap = new System.Windows.Forms.Label();
            this.lblNhan = new System.Windows.Forms.Label();
            this.lblTra = new System.Windows.Forms.Label();
            this.lblCoc = new System.Windows.Forms.Label();

            this.txtSoPhieu = new System.Windows.Forms.TextBox();
            this.cboKhach = new System.Windows.Forms.ComboBox();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.cboKenh = new System.Windows.Forms.ComboBox();
            this.dtLap = new System.Windows.Forms.DateTimePicker();
            this.dtNhan = new System.Windows.Forms.DateTimePicker();
            this.dtTra = new System.Windows.Forms.DateTimePicker();
            this.numCoc = new System.Windows.Forms.NumericUpDown();

            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.dgvChon = new System.Windows.Forms.DataGridView();
            this.dgvPhieu = new System.Windows.Forms.DataGridView();

            this.lblDSPhong = new System.Windows.Forms.Label();
            this.lblPhongChon = new System.Windows.Forms.Label();
            this.lblSoNguoi = new System.Windows.Forms.Label();
            this.lblPhieuDat = new System.Windows.Forms.Label();

            this.numSoNguoi = new System.Windows.Forms.NumericUpDown();

            this.btnThemPhong = new System.Windows.Forms.Button();
            this.btnBoPhong = new System.Windows.Forms.Button();
            this.btnLapPhieu = new System.Windows.Forms.Button();

            // =====================
            // TAB NHẬN PHÒNG
            // =====================
            this.lblPhieuChon = new System.Windows.Forms.Label();
            this.lblNguoiPhong = new System.Windows.Forms.Label();
            this.lblNguoiTen = new System.Windows.Forms.Label();
            this.lblNguoiCMND = new System.Windows.Forms.Label();
            this.lblNguoiQT = new System.Windows.Forms.Label();
            this.lblCT = new System.Windows.Forms.Label();
            this.lblNguoi = new System.Windows.Forms.Label();

            this.txtPhieuChon = new System.Windows.Forms.TextBox();
            this.txtNguoiPhong = new System.Windows.Forms.TextBox();
            this.txtNguoiTen = new System.Windows.Forms.TextBox();
            this.txtNguoiCMND = new System.Windows.Forms.TextBox();
            this.txtNguoiQT = new System.Windows.Forms.TextBox();

            this.dgvCT = new System.Windows.Forms.DataGridView();
            this.dgvNguoi = new System.Windows.Forms.DataGridView();

            this.btnThemNguoi = new System.Windows.Forms.Button();
            this.btnNhanPhong = new System.Windows.Forms.Button();
            this.btnNoShow = new System.Windows.Forms.Button();

            // =====================
            // BEGIN INIT
            // =====================
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoi)).BeginInit();

            this.tabMain.SuspendLayout();
            this.tabKhach.SuspendLayout();
            this.tabDatPhong.SuspendLayout();
            this.tabNhanPhong.SuspendLayout();

            this.SuspendLayout();

            // =====================
            // TAB CONTROL
            // =====================
            this.tabMain.Controls.Add(this.tabKhach);
            this.tabMain.Controls.Add(this.tabDatPhong);
            this.tabMain.Controls.Add(this.tabNhanPhong);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1160, 690);

            // =========================================================
            // TAB KHÁCH HÀNG
            // =========================================================
            this.tabKhach.Text = "Khách hàng";
            this.tabKhach.UseVisualStyleBackColor = true;

            this.dgvKhach.Location = new System.Drawing.Point(20, 20);
            this.dgvKhach.Name = "dgvKhach";
            this.dgvKhach.Size = new System.Drawing.Size(1100, 400);
            this.dgvKhach.ReadOnly = true;
            this.dgvKhach.AllowUserToAddRows = false;
            this.dgvKhach.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(30, 455);
            this.lblMaKH.Text = "Mã khách:";

            this.txtMaKH.Location = new System.Drawing.Point(110, 452);
            this.txtMaKH.Size = new System.Drawing.Size(160, 20);

            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(310, 455);
            this.lblTenKH.Text = "Họ tên:";

            this.txtTenKH.Location = new System.Drawing.Point(370, 452);
            this.txtTenKH.Size = new System.Drawing.Size(220, 20);

            this.lblCMND.AutoSize = true;
            this.lblCMND.Location = new System.Drawing.Point(630, 455);
            this.lblCMND.Text = "CMND/CCCD:";

            this.txtCMND.Location = new System.Drawing.Point(730, 452);
            this.txtCMND.Size = new System.Drawing.Size(180, 20);

            this.lblQT.AutoSize = true;
            this.lblQT.Location = new System.Drawing.Point(30, 505);
            this.lblQT.Text = "Quốc tịch:";

            this.txtQT.Location = new System.Drawing.Point(110, 502);
            this.txtQT.Size = new System.Drawing.Size(160, 20);

            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(310, 505);
            this.lblSDT.Text = "Điện thoại:";

            this.txtSDT.Location = new System.Drawing.Point(380, 502);
            this.txtSDT.Size = new System.Drawing.Size(210, 20);

            this.btnThemKhach.Location =
                new System.Drawing.Point(730, 495);
            this.btnThemKhach.Size =
                new System.Drawing.Size(160, 40);
            this.btnThemKhach.Name = "btnThemKhach";
            this.btnThemKhach.Text = "Thêm khách hàng";
            this.btnThemKhach.UseVisualStyleBackColor = true;

            this.tabKhach.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.dgvKhach,
                    this.lblMaKH,
                    this.txtMaKH,
                    this.lblTenKH,
                    this.txtTenKH,
                    this.lblCMND,
                    this.txtCMND,
                    this.lblQT,
                    this.txtQT,
                    this.lblSDT,
                    this.txtSDT,
                    this.btnThemKhach
                });

            // =========================================================
            // TAB ĐẶT PHÒNG
            // =========================================================
            this.tabDatPhong.Text = "Đặt phòng";
            this.tabDatPhong.UseVisualStyleBackColor = true;

            // ---- Đầu phiếu ----
            this.lblSoPhieu.AutoSize = true;
            this.lblSoPhieu.Location =
                new System.Drawing.Point(20, 25);
            this.lblSoPhieu.Text = "Số phiếu:";

            this.txtSoPhieu.Location =
                new System.Drawing.Point(90, 22);
            this.txtSoPhieu.Size =
                new System.Drawing.Size(130, 20);

            this.lblKhach.AutoSize = true;
            this.lblKhach.Location =
                new System.Drawing.Point(250, 25);
            this.lblKhach.Text = "Khách:";

            this.cboKhach.Location =
                new System.Drawing.Point(300, 22);
            this.cboKhach.Size =
                new System.Drawing.Size(180, 21);
            this.cboKhach.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblNV.AutoSize = true;
            this.lblNV.Location =
                new System.Drawing.Point(510, 25);
            this.lblNV.Text = "Lễ tân:";

            this.cboNV.Location =
                new System.Drawing.Point(565, 22);
            this.cboNV.Size =
                new System.Drawing.Size(170, 21);
            this.cboNV.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblKenh.AutoSize = true;
            this.lblKenh.Location =
                new System.Drawing.Point(770, 25);
            this.lblKenh.Text = "Kênh đặt:";

            this.cboKenh.Location =
                new System.Drawing.Point(840, 22);
            this.cboKenh.Size =
                new System.Drawing.Size(150, 21);
            this.cboKenh.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblLap.AutoSize = true;
            this.lblLap.Location =
                new System.Drawing.Point(20, 65);
            this.lblLap.Text = "Ngày lập:";

            this.dtLap.Location =
                new System.Drawing.Point(90, 62);
            this.dtLap.Size =
                new System.Drawing.Size(150, 20);
            this.dtLap.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblNhan.AutoSize = true;
            this.lblNhan.Location =
                new System.Drawing.Point(270, 65);
            this.lblNhan.Text = "Ngày nhận:";

            this.dtNhan.Location =
                new System.Drawing.Point(350, 62);
            this.dtNhan.Size =
                new System.Drawing.Size(150, 20);
            this.dtNhan.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblTra.AutoSize = true;
            this.lblTra.Location =
                new System.Drawing.Point(530, 65);
            this.lblTra.Text = "Ngày trả:";

            this.dtTra.Location =
                new System.Drawing.Point(600, 62);
            this.dtTra.Size =
                new System.Drawing.Size(150, 20);
            this.dtTra.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblCoc.AutoSize = true;
            this.lblCoc.Location =
                new System.Drawing.Point(780, 65);
            this.lblCoc.Text = "Tiền cọc:";

            this.numCoc.Location =
                new System.Drawing.Point(850, 62);
            this.numCoc.Size =
                new System.Drawing.Size(150, 20);
            this.numCoc.Maximum = 1000000000;
            this.numCoc.ThousandsSeparator = true;

            // ---- Danh sách phòng ----
            this.lblDSPhong.AutoSize = true;
            this.lblDSPhong.Location =
                new System.Drawing.Point(20, 105);
            this.lblDSPhong.Text = "Danh sách phòng";

            this.dgvPhong.Location =
                new System.Drawing.Point(20, 125);
            this.dgvPhong.Size =
                new System.Drawing.Size(500, 190);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.lblSoNguoi.AutoSize = true;
            this.lblSoNguoi.Location =
                new System.Drawing.Point(540, 150);
            this.lblSoNguoi.Text = "Số người:";

            this.numSoNguoi.Location =
                new System.Drawing.Point(610, 147);
            this.numSoNguoi.Size =
                new System.Drawing.Size(100, 20);
            this.numSoNguoi.Minimum = 1;
            this.numSoNguoi.Maximum = 100;
            this.numSoNguoi.Value = 1;

            this.btnThemPhong.Location =
                new System.Drawing.Point(550, 190);
            this.btnThemPhong.Size =
                new System.Drawing.Size(150, 40);
            this.btnThemPhong.Name = "btnThemPhong";
            this.btnThemPhong.Text = "Thêm phòng >>";
            this.btnThemPhong.UseVisualStyleBackColor = true;

            this.btnBoPhong.Location =
                new System.Drawing.Point(550, 245);
            this.btnBoPhong.Size =
                new System.Drawing.Size(150, 40);
            this.btnBoPhong.Name = "btnBoPhong";
            this.btnBoPhong.Text = "<< Bỏ phòng";
            this.btnBoPhong.UseVisualStyleBackColor = true;

            // ---- Phòng đã chọn ----
            this.lblPhongChon.AutoSize = true;
            this.lblPhongChon.Location =
                new System.Drawing.Point(730, 105);
            this.lblPhongChon.Text = "Phòng đã chọn";

            this.dgvChon.Location =
                new System.Drawing.Point(730, 125);
            this.dgvChon.Size =
                new System.Drawing.Size(390, 190);
            this.dgvChon.Name = "dgvChon";
            this.dgvChon.ReadOnly = true;
            this.dgvChon.AllowUserToAddRows = false;
            this.dgvChon.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChon.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.btnLapPhieu.Location =
                new System.Drawing.Point(480, 330);
            this.btnLapPhieu.Size =
                new System.Drawing.Size(180, 42);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Text = "Lập phiếu đặt phòng";
            this.btnLapPhieu.UseVisualStyleBackColor = true;

            // ---- Danh sách phiếu ----
            this.lblPhieuDat.AutoSize = true;
            this.lblPhieuDat.Location =
                new System.Drawing.Point(20, 395);
            this.lblPhieuDat.Text = "Danh sách phiếu đặt phòng";

            this.dgvPhieu.Location =
                new System.Drawing.Point(20, 415);
            this.dgvPhieu.Size =
                new System.Drawing.Size(1100, 220);
            this.dgvPhieu.Name = "dgvPhieu";
            this.dgvPhieu.ReadOnly = true;
            this.dgvPhieu.AllowUserToAddRows = false;
            this.dgvPhieu.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieu.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabDatPhong.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblSoPhieu,
                    this.txtSoPhieu,
                    this.lblKhach,
                    this.cboKhach,
                    this.lblNV,
                    this.cboNV,
                    this.lblKenh,
                    this.cboKenh,
                    this.lblLap,
                    this.dtLap,
                    this.lblNhan,
                    this.dtNhan,
                    this.lblTra,
                    this.dtTra,
                    this.lblCoc,
                    this.numCoc,

                    this.lblDSPhong,
                    this.dgvPhong,
                    this.lblSoNguoi,
                    this.numSoNguoi,
                    this.btnThemPhong,

                    this.btnBoPhong,
                    this.lblPhongChon,
                    this.dgvChon,

                    this.btnLapPhieu,
                    this.lblPhieuDat,
                    this.dgvPhieu
                });

            // =========================================================
            // TAB NHẬN PHÒNG
            // =========================================================
            this.tabNhanPhong.Text = "Nhận phòng";
            this.tabNhanPhong.UseVisualStyleBackColor = true;

            this.lblPhieuChon.AutoSize = true;
            this.lblPhieuChon.Location =
                new System.Drawing.Point(30, 30);
            this.lblPhieuChon.Text = "Phiếu đang chọn:";

            this.txtPhieuChon.Location =
                new System.Drawing.Point(140, 27);
            this.txtPhieuChon.Size =
                new System.Drawing.Size(180, 20);
            this.txtPhieuChon.ReadOnly = true;

            // Chi tiết phòng
            this.lblCT.AutoSize = true;
            this.lblCT.Location =
                new System.Drawing.Point(30, 70);
            this.lblCT.Text = "Chi tiết phòng đã đặt";

            this.dgvCT.Location =
                new System.Drawing.Point(30, 90);
            this.dgvCT.Size =
                new System.Drawing.Size(520, 200);
            this.dgvCT.Name = "dgvCT";
            this.dgvCT.ReadOnly = true;
            this.dgvCT.AllowUserToAddRows = false;
            this.dgvCT.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Người lưu trú
            this.lblNguoi.AutoSize = true;
            this.lblNguoi.Location =
                new System.Drawing.Point(590, 70);
            this.lblNguoi.Text = "Danh sách người lưu trú";

            this.dgvNguoi.Location =
                new System.Drawing.Point(590, 90);
            this.dgvNguoi.Size =
                new System.Drawing.Size(530, 200);
            this.dgvNguoi.Name = "dgvNguoi";
            this.dgvNguoi.ReadOnly = true;
            this.dgvNguoi.AllowUserToAddRows = false;
            this.dgvNguoi.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Nhập người lưu trú
            this.lblNguoiPhong.AutoSize = true;
            this.lblNguoiPhong.Location =
                new System.Drawing.Point(30, 330);
            this.lblNguoiPhong.Text = "Số phòng:";

            this.txtNguoiPhong.Location =
                new System.Drawing.Point(110, 327);
            this.txtNguoiPhong.Size =
                new System.Drawing.Size(150, 20);

            this.lblNguoiTen.AutoSize = true;
            this.lblNguoiTen.Location =
                new System.Drawing.Point(300, 330);
            this.lblNguoiTen.Text = "Họ tên:";

            this.txtNguoiTen.Location =
                new System.Drawing.Point(360, 327);
            this.txtNguoiTen.Size =
                new System.Drawing.Size(200, 20);

            this.lblNguoiCMND.AutoSize = true;
            this.lblNguoiCMND.Location =
                new System.Drawing.Point(600, 330);
            this.lblNguoiCMND.Text = "CMND/CCCD:";

            this.txtNguoiCMND.Location =
                new System.Drawing.Point(700, 327);
            this.txtNguoiCMND.Size =
                new System.Drawing.Size(180, 20);

            this.lblNguoiQT.AutoSize = true;
            this.lblNguoiQT.Location =
                new System.Drawing.Point(30, 380);
            this.lblNguoiQT.Text = "Quốc tịch:";

            this.txtNguoiQT.Location =
                new System.Drawing.Point(110, 377);
            this.txtNguoiQT.Size =
                new System.Drawing.Size(150, 20);

            this.btnThemNguoi.Location =
                new System.Drawing.Point(360, 370);
            this.btnThemNguoi.Size =
                new System.Drawing.Size(170, 40);
            this.btnThemNguoi.Name = "btnThemNguoi";
            this.btnThemNguoi.Text = "Thêm người lưu trú";
            this.btnThemNguoi.UseVisualStyleBackColor = true;

            this.btnNhanPhong.Location =
                new System.Drawing.Point(300, 460);
            this.btnNhanPhong.Size =
                new System.Drawing.Size(180, 45);
            this.btnNhanPhong.Name = "btnNhanPhong";
            this.btnNhanPhong.Text = "Nhận phòng";
            this.btnNhanPhong.UseVisualStyleBackColor = true;

            this.btnNoShow.Location =
                new System.Drawing.Point(520, 460);
            this.btnNoShow.Size =
                new System.Drawing.Size(180, 45);
            this.btnNoShow.Name = "btnNoShow";
            this.btnNoShow.Text = "Đánh dấu No-show";
            this.btnNoShow.UseVisualStyleBackColor = true;

            this.tabNhanPhong.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblPhieuChon,
                    this.txtPhieuChon,

                    this.lblCT,
                    this.dgvCT,

                    this.lblNguoi,
                    this.dgvNguoi,

                    this.lblNguoiPhong,
                    this.txtNguoiPhong,

                    this.lblNguoiTen,
                    this.txtNguoiTen,

                    this.lblNguoiCMND,
                    this.txtNguoiCMND,

                    this.lblNguoiQT,
                    this.txtNguoiQT,

                    this.btnThemNguoi,
                    this.btnNhanPhong,
                    this.btnNoShow
                });

            // =========================================================
            // FORM
            // =========================================================
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(1184, 711);

            this.Controls.Add(this.tabMain);

            this.Name = "FrmDatPhong";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý đặt phòng - nhận phòng";

            // =====================
            // END INIT
            // =====================
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoi)).EndInit();

            this.tabMain.ResumeLayout(false);
            this.tabKhach.ResumeLayout(false);
            this.tabKhach.PerformLayout();
            this.tabDatPhong.ResumeLayout(false);
            this.tabDatPhong.PerformLayout();
            this.tabNhanPhong.ResumeLayout(false);
            this.tabNhanPhong.PerformLayout();

            this.ResumeLayout(false);
        }

        // =====================
        // CONTROL DECLARATIONS
        // =====================

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabKhach;
        private System.Windows.Forms.TabPage tabDatPhong;
        private System.Windows.Forms.TabPage tabNhanPhong;

        // Khách hàng
        private System.Windows.Forms.DataGridView dgvKhach;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.Label lblCMND;
        private System.Windows.Forms.Label lblQT;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.TextBox txtQT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Button btnThemKhach;

        // Đặt phòng
        private System.Windows.Forms.Label lblSoPhieu;
        private System.Windows.Forms.Label lblKhach;
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.Label lblKenh;
        private System.Windows.Forms.Label lblLap;
        private System.Windows.Forms.Label lblNhan;
        private System.Windows.Forms.Label lblTra;
        private System.Windows.Forms.Label lblCoc;

        private System.Windows.Forms.TextBox txtSoPhieu;
        private System.Windows.Forms.ComboBox cboKhach;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.ComboBox cboKenh;
        private System.Windows.Forms.DateTimePicker dtLap;
        private System.Windows.Forms.DateTimePicker dtNhan;
        private System.Windows.Forms.DateTimePicker dtTra;
        private System.Windows.Forms.NumericUpDown numCoc;

        private System.Windows.Forms.Label lblDSPhong;
        private System.Windows.Forms.Label lblPhongChon;
        private System.Windows.Forms.Label lblSoNguoi;
        private System.Windows.Forms.Label lblPhieuDat;

        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.DataGridView dgvChon;
        private System.Windows.Forms.DataGridView dgvPhieu;

        private System.Windows.Forms.NumericUpDown numSoNguoi;

        private System.Windows.Forms.Button btnThemPhong;
        private System.Windows.Forms.Button btnBoPhong;
        private System.Windows.Forms.Button btnLapPhieu;

        // Nhận phòng
        private System.Windows.Forms.Label lblPhieuChon;
        private System.Windows.Forms.Label lblNguoiPhong;
        private System.Windows.Forms.Label lblNguoiTen;
        private System.Windows.Forms.Label lblNguoiCMND;
        private System.Windows.Forms.Label lblNguoiQT;
        private System.Windows.Forms.Label lblCT;
        private System.Windows.Forms.Label lblNguoi;

        private System.Windows.Forms.TextBox txtPhieuChon;
        private System.Windows.Forms.TextBox txtNguoiPhong;
        private System.Windows.Forms.TextBox txtNguoiTen;
        private System.Windows.Forms.TextBox txtNguoiCMND;
        private System.Windows.Forms.TextBox txtNguoiQT;

        private System.Windows.Forms.DataGridView dgvCT;
        private System.Windows.Forms.DataGridView dgvNguoi;

        private System.Windows.Forms.Button btnThemNguoi;
        private System.Windows.Forms.Button btnNhanPhong;
        private System.Windows.Forms.Button btnNoShow;
    }
}