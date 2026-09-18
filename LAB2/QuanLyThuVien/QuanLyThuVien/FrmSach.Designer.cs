namespace QuanLyThuVien
{
    partial class FrmSach
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

            this.lblMaSach = new System.Windows.Forms.Label();
            this.txtMaSach = new System.Windows.Forms.TextBox();

            this.lblTenSach = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();

            this.lblNamXB = new System.Windows.Forms.Label();
            this.numNamXB = new System.Windows.Forms.NumericUpDown();

            this.lblSoLuong = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();

            this.lblTheLoai = new System.Windows.Forms.Label();
            this.cboTheLoai = new System.Windows.Forms.ComboBox();

            this.lblNXB = new System.Windows.Forms.Label();
            this.cboNXB = new System.Windows.Forms.ComboBox();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();

            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();

            this.dgvSach = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.numNamXB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();

            this.SuspendLayout();

            // ========================================
            // TITLE
            // ========================================

            this.lblTitle.Font = new System.Drawing.Font(
                "Times New Roman",
                20F,
                System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(150, 20);

            this.lblTitle.Name = "lblTitle";

            this.lblTitle.Size =
                new System.Drawing.Size(700, 45);

            this.lblTitle.Text =
                "QUẢN LÝ ĐẦU SÁCH";

            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // ========================================
            // MÃ ĐẦU SÁCH
            // ========================================

            this.lblMaSach.AutoSize = true;

            this.lblMaSach.Location =
                new System.Drawing.Point(30, 90);

            this.lblMaSach.Name = "lblMaSach";

            this.lblMaSach.Text =
                "Mã đầu sách:";

            this.txtMaSach.Location =
                new System.Drawing.Point(130, 87);

            this.txtMaSach.Name =
                "txtMaSach";

            this.txtMaSach.Size =
                new System.Drawing.Size(180, 22);

            // ========================================
            // TÊN SÁCH
            // ========================================

            this.lblTenSach.AutoSize = true;

            this.lblTenSach.Location =
                new System.Drawing.Point(350, 90);

            this.lblTenSach.Name =
                "lblTenSach";

            this.lblTenSach.Text =
                "Tên sách:";

            this.txtTenSach.Location =
                new System.Drawing.Point(430, 87);

            this.txtTenSach.Name =
                "txtTenSach";

            this.txtTenSach.Size =
                new System.Drawing.Size(300, 22);

            // ========================================
            // NĂM XUẤT BẢN
            // ========================================

            this.lblNamXB.AutoSize = true;

            this.lblNamXB.Location =
                new System.Drawing.Point(30, 135);

            this.lblNamXB.Name =
                "lblNamXB";

            this.lblNamXB.Text =
                "Năm xuất bản:";

            this.numNamXB.Location =
                new System.Drawing.Point(130, 132);

            this.numNamXB.Name =
                "numNamXB";

            this.numNamXB.Minimum =
                new decimal(new int[] { 1900, 0, 0, 0 });

            this.numNamXB.Maximum =
                new decimal(new int[] { 2100, 0, 0, 0 });

            this.numNamXB.Value =
                new decimal(new int[] { 2026, 0, 0, 0 });

            this.numNamXB.Size =
                new System.Drawing.Size(180, 22);

            // ========================================
            // SỐ LƯỢNG
            // ========================================

            this.lblSoLuong.AutoSize = true;

            this.lblSoLuong.Location =
                new System.Drawing.Point(350, 135);

            this.lblSoLuong.Name =
                "lblSoLuong";

            this.lblSoLuong.Text =
                "Số lượng:";

            this.numSoLuong.Location =
                new System.Drawing.Point(430, 132);

            this.numSoLuong.Name =
                "numSoLuong";

            this.numSoLuong.Minimum =
                new decimal(new int[] { 0, 0, 0, 0 });

            this.numSoLuong.Maximum =
                new decimal(new int[] { 10000, 0, 0, 0 });

            this.numSoLuong.Size =
                new System.Drawing.Size(180, 22);

            // ========================================
            // THỂ LOẠI
            // ========================================

            this.lblTheLoai.AutoSize = true;

            this.lblTheLoai.Location =
                new System.Drawing.Point(30, 180);

            this.lblTheLoai.Name =
                "lblTheLoai";

            this.lblTheLoai.Text =
                "Thể loại:";

            this.cboTheLoai.Location =
                new System.Drawing.Point(130, 177);

            this.cboTheLoai.Name =
                "cboTheLoai";

            this.cboTheLoai.Size =
                new System.Drawing.Size(180, 24);

            this.cboTheLoai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ========================================
            // NHÀ XUẤT BẢN
            // ========================================

            this.lblNXB.AutoSize = true;

            this.lblNXB.Location =
                new System.Drawing.Point(350, 180);

            this.lblNXB.Name =
                "lblNXB";

            this.lblNXB.Text =
                "Nhà xuất bản:";

            this.cboNXB.Location =
                new System.Drawing.Point(450, 177);

            this.cboNXB.Name =
                "cboNXB";

            this.cboNXB.Size =
                new System.Drawing.Size(200, 24);

            this.cboNXB.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ========================================
            // BUTTON THÊM
            // ========================================

            this.btnThem.Location =
                new System.Drawing.Point(220, 225);

            this.btnThem.Name =
                "btnThem";

            this.btnThem.Size =
                new System.Drawing.Size(90, 35);

            this.btnThem.Text =
                "Thêm";

            this.btnThem.UseVisualStyleBackColor = true;

            // ========================================
            // BUTTON SỬA
            // ========================================

            this.btnSua.Location =
                new System.Drawing.Point(320, 225);

            this.btnSua.Name =
                "btnSua";

            this.btnSua.Size =
                new System.Drawing.Size(90, 35);

            this.btnSua.Text =
                "Sửa";

            this.btnSua.UseVisualStyleBackColor = true;

            // ========================================
            // BUTTON XÓA
            // ========================================

            this.btnXoa.Location =
                new System.Drawing.Point(420, 225);

            this.btnXoa.Name =
                "btnXoa";

            this.btnXoa.Size =
                new System.Drawing.Size(90, 35);

            this.btnXoa.Text =
                "Xóa";

            this.btnXoa.UseVisualStyleBackColor = true;

            // ========================================
            // BUTTON LÀM MỚI
            // ========================================

            this.btnLamMoi.Location =
                new System.Drawing.Point(520, 225);

            this.btnLamMoi.Name =
                "btnLamMoi";

            this.btnLamMoi.Size =
                new System.Drawing.Size(90, 35);

            this.btnLamMoi.Text =
                "Làm mới";

            this.btnLamMoi.UseVisualStyleBackColor = true;

            // ========================================
            // TÌM KIẾM
            // ========================================

            this.lblTimKiem.AutoSize = true;

            this.lblTimKiem.Location =
                new System.Drawing.Point(30, 280);

            this.lblTimKiem.Name =
                "lblTimKiem";

            this.lblTimKiem.Text =
                "Tìm kiếm:";

            this.txtTimKiem.Location =
                new System.Drawing.Point(105, 277);

            this.txtTimKiem.Name =
                "txtTimKiem";

            this.txtTimKiem.Size =
                new System.Drawing.Size(300, 22);

            this.btnTimKiem.Location =
                new System.Drawing.Point(415, 273);

            this.btnTimKiem.Name =
                "btnTimKiem";

            this.btnTimKiem.Size =
                new System.Drawing.Size(90, 30);

            this.btnTimKiem.Text =
                "Tìm";

            this.btnTimKiem.UseVisualStyleBackColor = true;

            // ========================================
            // DATAGRIDVIEW
            // ========================================

            this.dgvSach.Location =
                new System.Drawing.Point(30, 320);

            this.dgvSach.Name =
                "dgvSach";

            this.dgvSach.Size =
                new System.Drawing.Size(920, 265);

            this.dgvSach.ReadOnly = true;

            this.dgvSach.AllowUserToAddRows = false;

            this.dgvSach.AllowUserToDeleteRows = false;

            this.dgvSach.MultiSelect = false;

            this.dgvSach.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvSach.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // ========================================
            // FORM
            // ========================================

            this.ClientSize =
                new System.Drawing.Size(990, 620);

            this.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.lblTitle,

                    this.lblMaSach,
                    this.txtMaSach,

                    this.lblTenSach,
                    this.txtTenSach,

                    this.lblNamXB,
                    this.numNamXB,

                    this.lblSoLuong,
                    this.numSoLuong,

                    this.lblTheLoai,
                    this.cboTheLoai,

                    this.lblNXB,
                    this.cboNXB,

                    this.btnThem,
                    this.btnSua,
                    this.btnXoa,
                    this.btnLamMoi,

                    this.lblTimKiem,
                    this.txtTimKiem,
                    this.btnTimKiem,

                    this.dgvSach
                });

            this.Name =
                "FrmSach";

            this.Text =
                "Quản lý đầu sách";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.numNamXB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // ========================================
        // KHAI BÁO CONTROL
        // ========================================

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblMaSach;
        private System.Windows.Forms.TextBox txtMaSach;

        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.TextBox txtTenSach;

        private System.Windows.Forms.Label lblNamXB;
        private System.Windows.Forms.NumericUpDown numNamXB;

        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown numSoLuong;

        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.ComboBox cboTheLoai;

        private System.Windows.Forms.Label lblNXB;
        private System.Windows.Forms.ComboBox cboNXB;

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;

        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;

        private System.Windows.Forms.DataGridView dgvSach;
    }
}