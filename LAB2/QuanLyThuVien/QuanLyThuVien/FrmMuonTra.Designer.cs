namespace QuanLyThuVien
{
    partial class FrmMuonTra
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabMuon = new System.Windows.Forms.TabPage();
            this.tabTra = new System.Windows.Forms.TabPage();

            // TAB MƯỢN
            this.cboDocGia = new System.Windows.Forms.ComboBox();
            this.btnKiemTra = new System.Windows.Forms.Button();
            this.lblTrangThai = new System.Windows.Forms.Label();

            this.cboNhanVienMuon = new System.Windows.Forms.ComboBox();
            this.dtNgayMuon = new System.Windows.Forms.DateTimePicker();
            this.dtHenTra = new System.Windows.Forms.DateTimePicker();

            this.dgvSachCon = new System.Windows.Forms.DataGridView();
            this.dgvSachChon = new System.Windows.Forms.DataGridView();

            this.btnThemSach = new System.Windows.Forms.Button();
            this.btnBoSach = new System.Windows.Forms.Button();
            this.btnLapPhieu = new System.Windows.Forms.Button();

            // TAB TRẢ
            this.cboDocGiaTra = new System.Windows.Forms.ComboBox();
            this.btnTaiSachMuon = new System.Windows.Forms.Button();
            this.cboNhanVienTra = new System.Windows.Forms.ComboBox();

            this.dgvDangMuon = new System.Windows.Forms.DataGridView();

            this.dtNgayTra = new System.Windows.Forms.DateTimePicker();
            this.cboTinhTrang = new System.Windows.Forms.ComboBox();
            this.numPhiPhat = new System.Windows.Forms.NumericUpDown();

            this.btnTraSach = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.tabs.SuspendLayout();
            this.tabMuon.SuspendLayout();
            this.tabTra.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSachCon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachChon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhiPhat)).BeginInit();

            this.SuspendLayout();

            // =========================
            // TITLE
            // =========================
            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Times New Roman",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(200, 10);

            this.lblTitle.Size =
                new System.Drawing.Size(800, 50);

            this.lblTitle.Text =
                "QUẢN LÝ MƯỢN - TRẢ SÁCH";

            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // =========================
            // TAB CONTROL
            // =========================
            this.tabs.Location =
                new System.Drawing.Point(20, 70);

            this.tabs.Size =
                new System.Drawing.Size(1170, 660);

            this.tabs.Controls.Add(this.tabMuon);
            this.tabs.Controls.Add(this.tabTra);

            // =====================================================
            // TAB MƯỢN
            // =====================================================
            this.tabMuon.Text = "Mượn sách";
            this.tabMuon.Padding =
                new System.Windows.Forms.Padding(10);

            // Độc giả
            AddLabel(
                this.tabMuon,
                "Độc giả:",
                25, 30);

            this.cboDocGia.Location =
                new System.Drawing.Point(100, 27);

            this.cboDocGia.Size =
                new System.Drawing.Size(220, 25);

            this.cboDocGia.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Kiểm tra
            this.btnKiemTra.Text =
                "Kiểm tra điều kiện";

            this.btnKiemTra.Location =
                new System.Drawing.Point(340, 25);

            this.btnKiemTra.Size =
                new System.Drawing.Size(145, 30);

            // Trạng thái
            this.lblTrangThai.Text =
                "Chưa kiểm tra điều kiện mượn.";

            this.lblTrangThai.Location =
                new System.Drawing.Point(510, 30);

            this.lblTrangThai.Size =
                new System.Drawing.Size(600, 30);

            this.lblTrangThai.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            // Nhân viên
            AddLabel(
                this.tabMuon,
                "Nhân viên:",
                25, 80);

            this.cboNhanVienMuon.Location =
                new System.Drawing.Point(100, 77);

            this.cboNhanVienMuon.Size =
                new System.Drawing.Size(220, 25);

            this.cboNhanVienMuon.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Ngày mượn
            AddLabel(
                this.tabMuon,
                "Ngày mượn:",
                355, 80);

            this.dtNgayMuon.Location =
                new System.Drawing.Point(440, 77);

            this.dtNgayMuon.Size =
                new System.Drawing.Size(150, 25);

            this.dtNgayMuon.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Ngày hẹn trả
            AddLabel(
                this.tabMuon,
                "Ngày hẹn trả:",
                630, 80);

            this.dtHenTra.Location =
                new System.Drawing.Point(735, 77);

            this.dtHenTra.Size =
                new System.Drawing.Size(150, 25);

            this.dtHenTra.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Sách còn
            AddLabel(
                this.tabMuon,
                "SÁCH CÒN TRONG KHO",
                25, 135);

            this.dgvSachCon.Location =
                new System.Drawing.Point(25, 165);

            this.dgvSachCon.Size =
                new System.Drawing.Size(470, 350);

            SetupGrid(this.dgvSachCon);

            // Sách chọn
            AddLabel(
                this.tabMuon,
                "SÁCH ĐÃ CHỌN MƯỢN",
                665, 135);

            this.dgvSachChon.Location =
                new System.Drawing.Point(665, 165);

            this.dgvSachChon.Size =
                new System.Drawing.Size(470, 350);

            SetupGrid(this.dgvSachChon);

            // Thêm sách
            this.btnThemSach.Text = "Thêm >>";

            this.btnThemSach.Location =
                new System.Drawing.Point(520, 250);

            this.btnThemSach.Size =
                new System.Drawing.Size(115, 38);

            // Bỏ sách
            this.btnBoSach.Text = "<< Bỏ";

            this.btnBoSach.Location =
                new System.Drawing.Point(520, 305);

            this.btnBoSach.Size =
                new System.Drawing.Size(115, 38);

            // Lập phiếu
            this.btnLapPhieu.Text =
                "LẬP PHIẾU MƯỢN";

            this.btnLapPhieu.Location =
                new System.Drawing.Point(475, 545);

            this.btnLapPhieu.Size =
                new System.Drawing.Size(200, 45);

            this.tabMuon.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.cboDocGia,
                    this.btnKiemTra,
                    this.lblTrangThai,

                    this.cboNhanVienMuon,
                    this.dtNgayMuon,
                    this.dtHenTra,

                    this.dgvSachCon,
                    this.dgvSachChon,

                    this.btnThemSach,
                    this.btnBoSach,
                    this.btnLapPhieu
                });

            // =====================================================
            // TAB TRẢ
            // =====================================================
            this.tabTra.Text = "Trả sách";
            this.tabTra.Padding =
                new System.Windows.Forms.Padding(10);

            // Độc giả trả
            AddLabel(
                this.tabTra,
                "Độc giả:",
                25, 30);

            this.cboDocGiaTra.Location =
                new System.Drawing.Point(100, 27);

            this.cboDocGiaTra.Size =
                new System.Drawing.Size(220, 25);

            this.cboDocGiaTra.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Tải sách
            this.btnTaiSachMuon.Text =
                "Tải sách đang mượn";

            this.btnTaiSachMuon.Location =
                new System.Drawing.Point(340, 25);

            this.btnTaiSachMuon.Size =
                new System.Drawing.Size(160, 30);

            // Nhân viên nhận trả
            AddLabel(
                this.tabTra,
                "Nhân viên nhận trả:",
                540, 30);

            this.cboNhanVienTra.Location =
                new System.Drawing.Point(680, 27);

            this.cboNhanVienTra.Size =
                new System.Drawing.Size(200, 25);

            this.cboNhanVienTra.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Danh sách đang mượn
            AddLabel(
                this.tabTra,
                "DANH SÁCH SÁCH ĐANG MƯỢN",
                25, 90);

            this.dgvDangMuon.Location =
                new System.Drawing.Point(25, 120);

            this.dgvDangMuon.Size =
                new System.Drawing.Size(1110, 300);

            SetupGrid(this.dgvDangMuon);

            // Ngày trả
            AddLabel(
                this.tabTra,
                "Ngày trả:",
                25, 460);

            this.dtNgayTra.Location =
                new System.Drawing.Point(100, 457);

            this.dtNgayTra.Size =
                new System.Drawing.Size(150, 25);

            this.dtNgayTra.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Tình trạng
            AddLabel(
                this.tabTra,
                "Tình trạng:",
                300, 460);

            this.cboTinhTrang.Location =
                new System.Drawing.Point(385, 457);

            this.cboTinhTrang.Size =
                new System.Drawing.Size(200, 25);

            this.cboTinhTrang.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Phí phạt
            AddLabel(
                this.tabTra,
                "Phí phạt:",
                640, 460);

            this.numPhiPhat.Location =
                new System.Drawing.Point(710, 457);

            this.numPhiPhat.Size =
                new System.Drawing.Size(180, 25);

            this.numPhiPhat.Maximum =
                1000000000M;

            this.numPhiPhat.ThousandsSeparator = true;

            // Trả sách
            this.btnTraSach.Text =
                "XÁC NHẬN TRẢ SÁCH";

            this.btnTraSach.Location =
                new System.Drawing.Point(360, 535);

            this.btnTraSach.Size =
                new System.Drawing.Size(200, 45);

            // Đóng
            this.btnDong.Text = "Đóng";

            this.btnDong.Location =
                new System.Drawing.Point(590, 535);

            this.btnDong.Size =
                new System.Drawing.Size(130, 45);

            this.tabTra.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.cboDocGiaTra,
                    this.btnTaiSachMuon,
                    this.cboNhanVienTra,

                    this.dgvDangMuon,

                    this.dtNgayTra,
                    this.cboTinhTrang,
                    this.numPhiPhat,

                    this.btnTraSach,
                    this.btnDong
                });

            // =====================================================
            // FORM
            // =====================================================
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(1210, 765);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabs);

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.Name = "FrmMuonTra";

            this.Text = "Mượn - Trả sách";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            // =========================
            // RESUME
            // =========================
            this.tabs.ResumeLayout(false);

            this.tabMuon.ResumeLayout(false);
            this.tabMuon.PerformLayout();

            this.tabTra.ResumeLayout(false);
            this.tabTra.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSachCon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachChon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPhiPhat)).EndInit();

            this.ResumeLayout(false);
        }

        // =========================================================
        // HÀM TẠO LABEL
        // =========================================================
        private void AddLabel(
            System.Windows.Forms.Control parent,
            string text,
            int x,
            int y)
        {
            System.Windows.Forms.Label lbl =
                new System.Windows.Forms.Label();

            lbl.Text = text;
            lbl.AutoSize = true;

            lbl.Location =
                new System.Drawing.Point(x, y);

            parent.Controls.Add(lbl);
        }

        // =========================================================
        // CẤU HÌNH DATAGRIDVIEW
        // =========================================================
        private void SetupGrid(
            System.Windows.Forms.DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;

            dgv.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;

            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =========================================================
        // CONTROL
        // =========================================================

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabMuon;
        private System.Windows.Forms.TabPage tabTra;

        // MƯỢN
        private System.Windows.Forms.ComboBox cboDocGia;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.Label lblTrangThai;

        private System.Windows.Forms.ComboBox cboNhanVienMuon;

        private System.Windows.Forms.DateTimePicker dtNgayMuon;
        private System.Windows.Forms.DateTimePicker dtHenTra;

        private System.Windows.Forms.DataGridView dgvSachCon;
        private System.Windows.Forms.DataGridView dgvSachChon;

        private System.Windows.Forms.Button btnThemSach;
        private System.Windows.Forms.Button btnBoSach;
        private System.Windows.Forms.Button btnLapPhieu;

        // TRẢ
        private System.Windows.Forms.ComboBox cboDocGiaTra;
        private System.Windows.Forms.Button btnTaiSachMuon;

        private System.Windows.Forms.ComboBox cboNhanVienTra;

        private System.Windows.Forms.DataGridView dgvDangMuon;

        private System.Windows.Forms.DateTimePicker dtNgayTra;
        private System.Windows.Forms.ComboBox cboTinhTrang;

        private System.Windows.Forms.NumericUpDown numPhiPhat;

        private System.Windows.Forms.Button btnTraSach;
        private System.Windows.Forms.Button btnDong;
    }
}