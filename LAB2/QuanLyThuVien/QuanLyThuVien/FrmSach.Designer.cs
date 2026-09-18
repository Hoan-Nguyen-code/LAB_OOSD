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

            this.dgvSach = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.numNamXB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).BeginInit();

            this.SuspendLayout();

            // TITLE
            this.lblTitle.Font = new System.Drawing.Font(
                "Times New Roman", 20F,
                System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(150, 20);

            this.lblTitle.Size =
                new System.Drawing.Size(700, 45);

            this.lblTitle.Text = "QUẢN LÝ ĐẦU SÁCH";

            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // Mã sách
            this.lblMaSach.AutoSize = true;
            this.lblMaSach.Location =
                new System.Drawing.Point(30, 90);
            this.lblMaSach.Text = "Mã đầu sách:";

            this.txtMaSach.Location =
                new System.Drawing.Point(130, 87);
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.Size =
                new System.Drawing.Size(180, 22);

            // Tên sách
            this.lblTenSach.AutoSize = true;
            this.lblTenSach.Location =
                new System.Drawing.Point(350, 90);
            this.lblTenSach.Text = "Tên sách:";

            this.txtTenSach.Location =
                new System.Drawing.Point(430, 87);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size =
                new System.Drawing.Size(300, 22);

            // Năm XB
            this.lblNamXB.AutoSize = true;
            this.lblNamXB.Location =
                new System.Drawing.Point(30, 135);
            this.lblNamXB.Text = "Năm xuất bản:";

            this.numNamXB.Location =
                new System.Drawing.Point(130, 132);
            this.numNamXB.Name = "numNamXB";
            this.numNamXB.Minimum = 1900;
            this.numNamXB.Maximum = 2100;
            this.numNamXB.Value = 2026;

            // Số lượng
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location =
                new System.Drawing.Point(350, 135);
            this.lblSoLuong.Text = "Số lượng:";

            this.numSoLuong.Location =
                new System.Drawing.Point(430, 132);
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Maximum = 10000;

            // Thể loại
            this.lblTheLoai.AutoSize = true;
            this.lblTheLoai.Location =
                new System.Drawing.Point(30, 180);
            this.lblTheLoai.Text = "Thể loại:";

            this.cboTheLoai.Location =
                new System.Drawing.Point(130, 177);
            this.cboTheLoai.Name = "cboTheLoai";
            this.cboTheLoai.Size =
                new System.Drawing.Size(180, 24);
            this.cboTheLoai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // NXB
            this.lblNXB.AutoSize = true;
            this.lblNXB.Location =
                new System.Drawing.Point(350, 180);
            this.lblNXB.Text = "Nhà xuất bản:";

            this.cboNXB.Location =
                new System.Drawing.Point(450, 177);
            this.cboNXB.Name = "cboNXB";
            this.cboNXB.Size =
                new System.Drawing.Size(200, 24);
            this.cboNXB.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Buttons
            this.btnThem.Location =
                new System.Drawing.Point(220, 225);
            this.btnThem.Size =
                new System.Drawing.Size(90, 35);
            this.btnThem.Name = "btnThem";
            this.btnThem.Text = "Thêm";

            this.btnSua.Location =
                new System.Drawing.Point(320, 225);
            this.btnSua.Size =
                new System.Drawing.Size(90, 35);
            this.btnSua.Name = "btnSua";
            this.btnSua.Text = "Sửa";

            this.btnXoa.Location =
                new System.Drawing.Point(420, 225);
            this.btnXoa.Size =
                new System.Drawing.Size(90, 35);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Text = "Xóa";

            this.btnLamMoi.Location =
                new System.Drawing.Point(520, 225);
            this.btnLamMoi.Size =
                new System.Drawing.Size(90, 35);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Text = "Làm mới";

            // Grid
            this.dgvSach.Location =
                new System.Drawing.Point(30, 285);
            this.dgvSach.Name = "dgvSach";
            this.dgvSach.Size =
                new System.Drawing.Size(920, 300);
            this.dgvSach.ReadOnly = true;
            this.dgvSach.AllowUserToAddRows = false;
            this.dgvSach.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // FORM
            this.ClientSize =
                new System.Drawing.Size(990, 620);

            this.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    lblTitle,
                    lblMaSach, txtMaSach,
                    lblTenSach, txtTenSach,
                    lblNamXB, numNamXB,
                    lblSoLuong, numSoLuong,
                    lblTheLoai, cboTheLoai,
                    lblNXB, cboNXB,
                    btnThem, btnSua,
                    btnXoa, btnLamMoi,
                    dgvSach
                });

            this.Name = "FrmSach";
            this.Text = "Quản lý đầu sách";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.numNamXB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSach)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

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

        private System.Windows.Forms.DataGridView dgvSach;
    }
}