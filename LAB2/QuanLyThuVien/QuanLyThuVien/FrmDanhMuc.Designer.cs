namespace QuanLyThuVien
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabDanhMuc = new System.Windows.Forms.TabControl();

            this.tabNhanVien = new System.Windows.Forms.TabPage();
            this.tabTheLoai = new System.Windows.Forms.TabPage();
            this.tabNXB = new System.Windows.Forms.TabPage();

            // NHÂN VIÊN
            this.lblMaNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblHo = new System.Windows.Forms.Label();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblPhai = new System.Windows.Forms.Label();
            this.cboPhai = new System.Windows.Forms.ComboBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.btnThemNV = new System.Windows.Forms.Button();
            this.btnSuaNV = new System.Windows.Forms.Button();
            this.btnXoaNV = new System.Windows.Forms.Button();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();

            // THỂ LOẠI
            this.lblMaTL = new System.Windows.Forms.Label();
            this.txtMaTheLoai = new System.Windows.Forms.TextBox();
            this.lblTenTL = new System.Windows.Forms.Label();
            this.txtTenTheLoai = new System.Windows.Forms.TextBox();
            this.btnThemTL = new System.Windows.Forms.Button();
            this.btnSuaTL = new System.Windows.Forms.Button();
            this.btnXoaTL = new System.Windows.Forms.Button();
            this.dgvTheLoai = new System.Windows.Forms.DataGridView();

            // NHÀ XUẤT BẢN
            this.lblMaNXB = new System.Windows.Forms.Label();
            this.txtMaNXB = new System.Windows.Forms.TextBox();
            this.lblDiaChiNXB = new System.Windows.Forms.Label();
            this.txtDiaChiNXB = new System.Windows.Forms.TextBox();
            this.lblSDTNXB = new System.Windows.Forms.Label();
            this.txtSDTNXB = new System.Windows.Forms.TextBox();
            this.btnThemNXB = new System.Windows.Forms.Button();
            this.btnSuaNXB = new System.Windows.Forms.Button();
            this.btnXoaNXB = new System.Windows.Forms.Button();
            this.dgvNXB = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheLoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNXB)).BeginInit();

            this.tabDanhMuc.SuspendLayout();
            this.tabNhanVien.SuspendLayout();
            this.tabTheLoai.SuspendLayout();
            this.tabNXB.SuspendLayout();
            this.SuspendLayout();

            // ================= TITLE =================
            this.lblTitle.Font = new System.Drawing.Font(
                "Times New Roman", 20F,
                System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(150, 20);

            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size =
                new System.Drawing.Size(700, 45);

            this.lblTitle.Text = "QUẢN LÝ DANH MỤC";
            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // ================= TAB CONTROL =================
            this.tabDanhMuc.Controls.Add(this.tabNhanVien);
            this.tabDanhMuc.Controls.Add(this.tabTheLoai);
            this.tabDanhMuc.Controls.Add(this.tabNXB);

            this.tabDanhMuc.Location =
                new System.Drawing.Point(20, 80);

            this.tabDanhMuc.Name = "tabDanhMuc";
            this.tabDanhMuc.Size =
                new System.Drawing.Size(945, 510);

            // =================================================
            // TAB NHÂN VIÊN
            // =================================================
            this.tabNhanVien.Text = "Nhân viên";
            this.tabNhanVien.UseVisualStyleBackColor = true;

            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Location = new System.Drawing.Point(20, 30);
            this.lblMaNV.Text = "Mã nhân viên:";

            this.txtMaNV.Location = new System.Drawing.Point(130, 27);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(180, 22);

            this.lblHo.AutoSize = true;
            this.lblHo.Location = new System.Drawing.Point(340, 30);
            this.lblHo.Text = "Họ:";

            this.txtHo.Location = new System.Drawing.Point(390, 27);
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(180, 22);

            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(600, 30);
            this.lblTen.Text = "Tên:";

            this.txtTen.Location = new System.Drawing.Point(650, 27);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(180, 22);

            this.lblPhai.AutoSize = true;
            this.lblPhai.Location = new System.Drawing.Point(20, 75);
            this.lblPhai.Text = "Phái:";

            this.cboPhai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboPhai.Items.AddRange(
                new object[] { "Nam", "Nữ" });

            this.cboPhai.Location =
                new System.Drawing.Point(130, 72);

            this.cboPhai.Name = "cboPhai";
            this.cboPhai.Size =
                new System.Drawing.Size(180, 24);

            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location =
                new System.Drawing.Point(340, 75);

            this.lblNgaySinh.Text = "Ngày sinh:";

            this.dtpNgaySinh.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtpNgaySinh.Location =
                new System.Drawing.Point(420, 72);

            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size =
                new System.Drawing.Size(150, 22);

            this.lblChucVu.AutoSize = true;
            this.lblChucVu.Location =
                new System.Drawing.Point(600, 75);

            this.lblChucVu.Text = "Chức vụ:";

            this.txtChucVu.Location =
                new System.Drawing.Point(670, 72);

            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size =
                new System.Drawing.Size(160, 22);

            this.lblSDT.AutoSize = true;
            this.lblSDT.Location =
                new System.Drawing.Point(20, 120);

            this.lblSDT.Text = "Số điện thoại:";

            this.txtSDT.Location =
                new System.Drawing.Point(130, 117);

            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size =
                new System.Drawing.Size(180, 22);

            this.btnThemNV.Location =
                new System.Drawing.Point(350, 110);

            this.btnThemNV.Name = "btnThemNV";
            this.btnThemNV.Size =
                new System.Drawing.Size(90, 35);

            this.btnThemNV.Text = "Thêm";

            this.btnSuaNV.Location =
                new System.Drawing.Point(450, 110);

            this.btnSuaNV.Name = "btnSuaNV";
            this.btnSuaNV.Size =
                new System.Drawing.Size(90, 35);

            this.btnSuaNV.Text = "Sửa";

            this.btnXoaNV.Location =
                new System.Drawing.Point(550, 110);

            this.btnXoaNV.Name = "btnXoaNV";
            this.btnXoaNV.Size =
                new System.Drawing.Size(90, 35);

            this.btnXoaNV.Text = "Xóa";

            this.dgvNhanVien.Location =
                new System.Drawing.Point(20, 165);

            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.Size =
                new System.Drawing.Size(880, 280);

            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabNhanVien.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblMaNV, this.txtMaNV,
                    this.lblHo, this.txtHo,
                    this.lblTen, this.txtTen,
                    this.lblPhai, this.cboPhai,
                    this.lblNgaySinh, this.dtpNgaySinh,
                    this.lblChucVu, this.txtChucVu,
                    this.lblSDT, this.txtSDT,
                    this.btnThemNV, this.btnSuaNV,
                    this.btnXoaNV, this.dgvNhanVien
                });

            // =================================================
            // TAB THỂ LOẠI
            // =================================================
            this.tabTheLoai.Text = "Thể loại";
            this.tabTheLoai.UseVisualStyleBackColor = true;

            this.lblMaTL.AutoSize = true;
            this.lblMaTL.Location =
                new System.Drawing.Point(30, 40);

            this.lblMaTL.Text = "Mã thể loại:";

            this.txtMaTheLoai.Location =
                new System.Drawing.Point(130, 37);

            this.txtMaTheLoai.Name = "txtMaTheLoai";
            this.txtMaTheLoai.Size =
                new System.Drawing.Size(200, 22);

            this.lblTenTL.AutoSize = true;
            this.lblTenTL.Location =
                new System.Drawing.Point(370, 40);

            this.lblTenTL.Text = "Tên thể loại:";

            this.txtTenTheLoai.Location =
                new System.Drawing.Point(470, 37);

            this.txtTenTheLoai.Name = "txtTenTheLoai";
            this.txtTenTheLoai.Size =
                new System.Drawing.Size(250, 22);

            this.btnThemTL.Location =
                new System.Drawing.Point(250, 90);

            this.btnThemTL.Name = "btnThemTL";
            this.btnThemTL.Size =
                new System.Drawing.Size(90, 35);

            this.btnThemTL.Text = "Thêm";

            this.btnSuaTL.Location =
                new System.Drawing.Point(350, 90);

            this.btnSuaTL.Name = "btnSuaTL";
            this.btnSuaTL.Size =
                new System.Drawing.Size(90, 35);

            this.btnSuaTL.Text = "Sửa";

            this.btnXoaTL.Location =
                new System.Drawing.Point(450, 90);

            this.btnXoaTL.Name = "btnXoaTL";
            this.btnXoaTL.Size =
                new System.Drawing.Size(90, 35);

            this.btnXoaTL.Text = "Xóa";

            this.dgvTheLoai.Location =
                new System.Drawing.Point(30, 150);

            this.dgvTheLoai.Name = "dgvTheLoai";
            this.dgvTheLoai.Size =
                new System.Drawing.Size(870, 290);

            this.dgvTheLoai.ReadOnly = true;
            this.dgvTheLoai.AllowUserToAddRows = false;
            this.dgvTheLoai.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabTheLoai.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblMaTL,
                    this.txtMaTheLoai,
                    this.lblTenTL,
                    this.txtTenTheLoai,
                    this.btnThemTL,
                    this.btnSuaTL,
                    this.btnXoaTL,
                    this.dgvTheLoai
                });

            // =================================================
            // TAB NHÀ XUẤT BẢN
            // =================================================
            this.tabNXB.Text = "Nhà xuất bản";
            this.tabNXB.UseVisualStyleBackColor = true;

            this.lblMaNXB.AutoSize = true;
            this.lblMaNXB.Location =
                new System.Drawing.Point(30, 35);

            this.lblMaNXB.Text = "Mã NXB:";

            this.txtMaNXB.Location =
                new System.Drawing.Point(120, 32);

            this.txtMaNXB.Name = "txtMaNXB";
            this.txtMaNXB.Size =
                new System.Drawing.Size(180, 22);

            this.lblDiaChiNXB.AutoSize = true;
            this.lblDiaChiNXB.Location =
                new System.Drawing.Point(330, 35);

            this.lblDiaChiNXB.Text = "Địa chỉ:";

            this.txtDiaChiNXB.Location =
                new System.Drawing.Point(400, 32);

            this.txtDiaChiNXB.Name = "txtDiaChiNXB";
            this.txtDiaChiNXB.Size =
                new System.Drawing.Size(250, 22);

            this.lblSDTNXB.AutoSize = true;
            this.lblSDTNXB.Location =
                new System.Drawing.Point(30, 80);

            this.lblSDTNXB.Text = "SĐT:";

            this.txtSDTNXB.Location =
                new System.Drawing.Point(120, 77);

            this.txtSDTNXB.Name = "txtSDTNXB";
            this.txtSDTNXB.Size =
                new System.Drawing.Size(180, 22);

            this.btnThemNXB.Location =
                new System.Drawing.Point(350, 70);

            this.btnThemNXB.Name = "btnThemNXB";
            this.btnThemNXB.Size =
                new System.Drawing.Size(90, 35);

            this.btnThemNXB.Text = "Thêm";

            this.btnSuaNXB.Location =
                new System.Drawing.Point(450, 70);

            this.btnSuaNXB.Name = "btnSuaNXB";
            this.btnSuaNXB.Size =
                new System.Drawing.Size(90, 35);

            this.btnSuaNXB.Text = "Sửa";

            this.btnXoaNXB.Location =
                new System.Drawing.Point(550, 70);

            this.btnXoaNXB.Name = "btnXoaNXB";
            this.btnXoaNXB.Size =
                new System.Drawing.Size(90, 35);

            this.btnXoaNXB.Text = "Xóa";

            this.dgvNXB.Location =
                new System.Drawing.Point(30, 140);

            this.dgvNXB.Name = "dgvNXB";
            this.dgvNXB.Size =
                new System.Drawing.Size(870, 300);

            this.dgvNXB.ReadOnly = true;
            this.dgvNXB.AllowUserToAddRows = false;
            this.dgvNXB.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabNXB.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblMaNXB,
                    this.txtMaNXB,
                    this.lblDiaChiNXB,
                    this.txtDiaChiNXB,
                    this.lblSDTNXB,
                    this.txtSDTNXB,
                    this.btnThemNXB,
                    this.btnSuaNXB,
                    this.btnXoaNXB,
                    this.dgvNXB
                });

            // ================= FORM =================
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(8F, 16F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(990, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabDanhMuc);

            this.Name = "FrmDanhMuc";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Danh mục / Nhân viên";

            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTheLoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNXB)).EndInit();

            this.tabDanhMuc.ResumeLayout(false);
            this.tabNhanVien.ResumeLayout(false);
            this.tabNhanVien.PerformLayout();
            this.tabTheLoai.ResumeLayout(false);
            this.tabTheLoai.PerformLayout();
            this.tabNXB.ResumeLayout(false);
            this.tabNXB.PerformLayout();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabDanhMuc;
        private System.Windows.Forms.TabPage tabNhanVien;
        private System.Windows.Forms.TabPage tabTheLoai;
        private System.Windows.Forms.TabPage tabNXB;

        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label lblHo;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblPhai;
        private System.Windows.Forms.ComboBox cboPhai;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblChucVu;
        private System.Windows.Forms.TextBox txtChucVu;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Button btnThemNV;
        private System.Windows.Forms.Button btnSuaNV;
        private System.Windows.Forms.Button btnXoaNV;
        private System.Windows.Forms.DataGridView dgvNhanVien;

        private System.Windows.Forms.Label lblMaTL;
        private System.Windows.Forms.TextBox txtMaTheLoai;
        private System.Windows.Forms.Label lblTenTL;
        private System.Windows.Forms.TextBox txtTenTheLoai;
        private System.Windows.Forms.Button btnThemTL;
        private System.Windows.Forms.Button btnSuaTL;
        private System.Windows.Forms.Button btnXoaTL;
        private System.Windows.Forms.DataGridView dgvTheLoai;

        private System.Windows.Forms.Label lblMaNXB;
        private System.Windows.Forms.TextBox txtMaNXB;
        private System.Windows.Forms.Label lblDiaChiNXB;
        private System.Windows.Forms.TextBox txtDiaChiNXB;
        private System.Windows.Forms.Label lblSDTNXB;
        private System.Windows.Forms.TextBox txtSDTNXB;
        private System.Windows.Forms.Button btnThemNXB;
        private System.Windows.Forms.Button btnSuaNXB;
        private System.Windows.Forms.Button btnXoaNXB;
        private System.Windows.Forms.DataGridView dgvNXB;
    }
}