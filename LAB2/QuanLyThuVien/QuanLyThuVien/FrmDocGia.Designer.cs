namespace QuanLyThuVien
{
    partial class FrmDocGia
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

            this.grpDocGia = new System.Windows.Forms.GroupBox();

            this.lblMaDocGia = new System.Windows.Forms.Label();
            this.txtMaDocGia = new System.Windows.Forms.TextBox();

            this.lblHo = new System.Windows.Forms.Label();
            this.txtHo = new System.Windows.Forms.TextBox();

            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();

            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();

            this.lblPhai = new System.Windows.Forms.Label();
            this.cboPhai = new System.Windows.Forms.ComboBox();

            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();

            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();

            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();

            this.lblAnh = new System.Windows.Forms.Label();
            this.txtAnh = new System.Windows.Forms.TextBox();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();

            this.grpThe = new System.Windows.Forms.GroupBox();

            this.lblNgayCap = new System.Windows.Forms.Label();
            this.dtpNgayCap = new System.Windows.Forms.DateTimePicker();

            this.lblHanSuDung = new System.Windows.Forms.Label();
            this.dtpHanSuDung = new System.Windows.Forms.DateTimePicker();

            this.chkLePhi = new System.Windows.Forms.CheckBox();

            this.btnCapThe = new System.Windows.Forms.Button();
            this.btnGiaHan = new System.Windows.Forms.Button();

            this.lblDanhSachThe = new System.Windows.Forms.Label();
            this.dgvThe = new System.Windows.Forms.DataGridView();

            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();

            this.dgvDocGia = new System.Windows.Forms.DataGridView();

            this.grpDocGia.SuspendLayout();
            this.grpThe.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvThe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).BeginInit();

            this.SuspendLayout();

            // =====================================
            // TITLE
            // =====================================

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Times New Roman",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(250, 15);

            this.lblTitle.Name =
                "lblTitle";

            this.lblTitle.Size =
                new System.Drawing.Size(700, 45);

            this.lblTitle.Text =
                "QUẢN LÝ ĐỘC GIẢ - THẺ THƯ VIỆN";

            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // =====================================
            // GROUP ĐỘC GIẢ
            // =====================================

            this.grpDocGia.Location =
                new System.Drawing.Point(20, 70);

            this.grpDocGia.Name =
                "grpDocGia";

            this.grpDocGia.Size =
                new System.Drawing.Size(650, 310);

            this.grpDocGia.Text =
                "Thông tin độc giả";

            // Mã độc giả
            this.lblMaDocGia.AutoSize = true;
            this.lblMaDocGia.Location =
                new System.Drawing.Point(20, 35);
            this.lblMaDocGia.Text =
                "Mã độc giả:";

            this.txtMaDocGia.Location =
                new System.Drawing.Point(120, 32);
            this.txtMaDocGia.Name =
                "txtMaDocGia";
            this.txtMaDocGia.Size =
                new System.Drawing.Size(170, 22);

            // Họ
            this.lblHo.AutoSize = true;
            this.lblHo.Location =
                new System.Drawing.Point(330, 35);
            this.lblHo.Text =
                "Họ:";

            this.txtHo.Location =
                new System.Drawing.Point(390, 32);
            this.txtHo.Name =
                "txtHo";
            this.txtHo.Size =
                new System.Drawing.Size(220, 22);

            // Tên
            this.lblTen.AutoSize = true;
            this.lblTen.Location =
                new System.Drawing.Point(20, 75);
            this.lblTen.Text =
                "Tên:";

            this.txtTen.Location =
                new System.Drawing.Point(120, 72);
            this.txtTen.Name =
                "txtTen";
            this.txtTen.Size =
                new System.Drawing.Size(170, 22);

            // Ngày sinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location =
                new System.Drawing.Point(330, 75);
            this.lblNgaySinh.Text =
                "Ngày sinh:";

            this.dtpNgaySinh.Location =
                new System.Drawing.Point(410, 72);
            this.dtpNgaySinh.Name =
                "dtpNgaySinh";
            this.dtpNgaySinh.Size =
                new System.Drawing.Size(200, 22);
            this.dtpNgaySinh.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Phái
            this.lblPhai.AutoSize = true;
            this.lblPhai.Location =
                new System.Drawing.Point(20, 115);
            this.lblPhai.Text =
                "Phái:";

            this.cboPhai.Location =
                new System.Drawing.Point(120, 112);
            this.cboPhai.Name =
                "cboPhai";
            this.cboPhai.Size =
                new System.Drawing.Size(170, 24);
            this.cboPhai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // SĐT
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location =
                new System.Drawing.Point(330, 115);
            this.lblSDT.Text =
                "Số điện thoại:";

            this.txtSDT.Location =
                new System.Drawing.Point(430, 112);
            this.txtSDT.Name =
                "txtSDT";
            this.txtSDT.Size =
                new System.Drawing.Size(180, 22);

            // Địa chỉ
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location =
                new System.Drawing.Point(20, 155);
            this.lblDiaChi.Text =
                "Địa chỉ:";

            this.txtDiaChi.Location =
                new System.Drawing.Point(120, 152);
            this.txtDiaChi.Name =
                "txtDiaChi";
            this.txtDiaChi.Size =
                new System.Drawing.Size(490, 22);

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location =
                new System.Drawing.Point(20, 195);
            this.lblEmail.Text =
                "Email:";

            this.txtEmail.Location =
                new System.Drawing.Point(120, 192);
            this.txtEmail.Name =
                "txtEmail";
            this.txtEmail.Size =
                new System.Drawing.Size(250, 22);

            // Ảnh
            this.lblAnh.AutoSize = true;
            this.lblAnh.Location =
                new System.Drawing.Point(390, 195);
            this.lblAnh.Text =
                "Ảnh 3x4:";

            this.txtAnh.Location =
                new System.Drawing.Point(455, 192);
            this.txtAnh.Name =
                "txtAnh";
            this.txtAnh.Size =
                new System.Drawing.Size(155, 22);

            // Buttons
            this.btnThem.Location =
                new System.Drawing.Point(100, 245);
            this.btnThem.Name =
                "btnThem";
            this.btnThem.Size =
                new System.Drawing.Size(90, 35);
            this.btnThem.Text =
                "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;

            this.btnCapNhat.Location =
                new System.Drawing.Point(200, 245);
            this.btnCapNhat.Name =
                "btnCapNhat";
            this.btnCapNhat.Size =
                new System.Drawing.Size(90, 35);
            this.btnCapNhat.Text =
                "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;

            this.btnXoa.Location =
                new System.Drawing.Point(300, 245);
            this.btnXoa.Name =
                "btnXoa";
            this.btnXoa.Size =
                new System.Drawing.Size(90, 35);
            this.btnXoa.Text =
                "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;

            this.btnLamMoi.Location =
                new System.Drawing.Point(400, 245);
            this.btnLamMoi.Name =
                "btnLamMoi";
            this.btnLamMoi.Size =
                new System.Drawing.Size(90, 35);
            this.btnLamMoi.Text =
                "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;

            this.grpDocGia.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblMaDocGia,
                    this.txtMaDocGia,
                    this.lblHo,
                    this.txtHo,
                    this.lblTen,
                    this.txtTen,
                    this.lblNgaySinh,
                    this.dtpNgaySinh,
                    this.lblPhai,
                    this.cboPhai,
                    this.lblSDT,
                    this.txtSDT,
                    this.lblDiaChi,
                    this.txtDiaChi,
                    this.lblEmail,
                    this.txtEmail,
                    this.lblAnh,
                    this.txtAnh,
                    this.btnThem,
                    this.btnCapNhat,
                    this.btnXoa,
                    this.btnLamMoi
                });

            // =====================================
            // GROUP THẺ THƯ VIỆN
            // =====================================

            this.grpThe.Location =
                new System.Drawing.Point(690, 70);

            this.grpThe.Name =
                "grpThe";

            this.grpThe.Size =
                new System.Drawing.Size(480, 310);

            this.grpThe.Text =
                "Thẻ thư viện";

            // Ngày cấp
            this.lblNgayCap.AutoSize = true;
            this.lblNgayCap.Location =
                new System.Drawing.Point(20, 35);
            this.lblNgayCap.Text =
                "Ngày cấp:";

            this.dtpNgayCap.Location =
                new System.Drawing.Point(120, 32);
            this.dtpNgayCap.Name =
                "dtpNgayCap";
            this.dtpNgayCap.Size =
                new System.Drawing.Size(200, 22);
            this.dtpNgayCap.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Hạn sử dụng
            this.lblHanSuDung.AutoSize = true;
            this.lblHanSuDung.Location =
                new System.Drawing.Point(20, 75);
            this.lblHanSuDung.Text =
                "Hạn sử dụng:";

            this.dtpHanSuDung.Location =
                new System.Drawing.Point(120, 72);
            this.dtpHanSuDung.Name =
                "dtpHanSuDung";
            this.dtpHanSuDung.Size =
                new System.Drawing.Size(200, 22);
            this.dtpHanSuDung.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Lệ phí
            this.chkLePhi.Location =
                new System.Drawing.Point(120, 110);
            this.chkLePhi.Name =
                "chkLePhi";
            this.chkLePhi.Size =
                new System.Drawing.Size(180, 25);
            this.chkLePhi.Text =
                "Đã đóng lệ phí";

            // Cấp thẻ
            this.btnCapThe.Location =
                new System.Drawing.Point(120, 145);
            this.btnCapThe.Name =
                "btnCapThe";
            this.btnCapThe.Size =
                new System.Drawing.Size(100, 35);
            this.btnCapThe.Text =
                "Cấp thẻ";
            this.btnCapThe.UseVisualStyleBackColor = true;

            // Gia hạn
            this.btnGiaHan.Location =
                new System.Drawing.Point(230, 145);
            this.btnGiaHan.Name =
                "btnGiaHan";
            this.btnGiaHan.Size =
                new System.Drawing.Size(100, 35);
            this.btnGiaHan.Text =
                "Gia hạn";
            this.btnGiaHan.UseVisualStyleBackColor = true;

            // Danh sách thẻ
            this.lblDanhSachThe.AutoSize = true;
            this.lblDanhSachThe.Location =
                new System.Drawing.Point(20, 195);
            this.lblDanhSachThe.Text =
                "Danh sách thẻ của độc giả:";

            this.dgvThe.Location =
                new System.Drawing.Point(20, 220);
            this.dgvThe.Name =
                "dgvThe";
            this.dgvThe.Size =
                new System.Drawing.Size(440, 75);

            this.dgvThe.ReadOnly = true;
            this.dgvThe.AllowUserToAddRows = false;
            this.dgvThe.AllowUserToDeleteRows = false;

            this.dgvThe.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvThe.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;

            this.grpThe.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblNgayCap,
                    this.dtpNgayCap,
                    this.lblHanSuDung,
                    this.dtpHanSuDung,
                    this.chkLePhi,
                    this.btnCapThe,
                    this.btnGiaHan,
                    this.lblDanhSachThe,
                    this.dgvThe
                });

            // =====================================
            // TÌM KIẾM
            // =====================================

            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location =
                new System.Drawing.Point(25, 405);
            this.lblTimKiem.Text =
                "Tìm kiếm:";

            this.txtTimKiem.Location =
                new System.Drawing.Point(100, 402);
            this.txtTimKiem.Name =
                "txtTimKiem";
            this.txtTimKiem.Size =
                new System.Drawing.Size(300, 22);

            this.btnTimKiem.Location =
                new System.Drawing.Point(410, 398);
            this.btnTimKiem.Name =
                "btnTimKiem";
            this.btnTimKiem.Size =
                new System.Drawing.Size(90, 30);
            this.btnTimKiem.Text =
                "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;

            // =====================================
            // GRID ĐỘC GIẢ
            // =====================================

            this.dgvDocGia.Location =
                new System.Drawing.Point(20, 445);

            this.dgvDocGia.Name =
                "dgvDocGia";

            this.dgvDocGia.Size =
                new System.Drawing.Size(1150, 260);

            this.dgvDocGia.ReadOnly = true;

            this.dgvDocGia.AllowUserToAddRows = false;
            this.dgvDocGia.AllowUserToDeleteRows = false;

            this.dgvDocGia.MultiSelect = false;

            this.dgvDocGia.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvDocGia.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // =====================================
            // FORM
            // =====================================

            this.ClientSize =
                new System.Drawing.Size(1190, 730);

            this.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblTitle,
                    this.grpDocGia,
                    this.grpThe,
                    this.lblTimKiem,
                    this.txtTimKiem,
                    this.btnTimKiem,
                    this.dgvDocGia
                });

            this.Name =
                "FrmDocGia";

            this.Text =
                "Quản lý độc giả";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.grpDocGia.ResumeLayout(false);
            this.grpDocGia.PerformLayout();

            this.grpThe.ResumeLayout(false);
            this.grpThe.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvThe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.GroupBox grpDocGia;

        private System.Windows.Forms.Label lblMaDocGia;
        private System.Windows.Forms.TextBox txtMaDocGia;

        private System.Windows.Forms.Label lblHo;
        private System.Windows.Forms.TextBox txtHo;

        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;

        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;

        private System.Windows.Forms.Label lblPhai;
        private System.Windows.Forms.ComboBox cboPhai;

        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;

        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;

        private System.Windows.Forms.Label lblAnh;
        private System.Windows.Forms.TextBox txtAnh;

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;

        private System.Windows.Forms.GroupBox grpThe;

        private System.Windows.Forms.Label lblNgayCap;
        private System.Windows.Forms.DateTimePicker dtpNgayCap;

        private System.Windows.Forms.Label lblHanSuDung;
        private System.Windows.Forms.DateTimePicker dtpHanSuDung;

        private System.Windows.Forms.CheckBox chkLePhi;

        private System.Windows.Forms.Button btnCapThe;
        private System.Windows.Forms.Button btnGiaHan;

        private System.Windows.Forms.Label lblDanhSachThe;
        private System.Windows.Forms.DataGridView dgvThe;

        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;

        private System.Windows.Forms.DataGridView dgvDocGia;
    }
}