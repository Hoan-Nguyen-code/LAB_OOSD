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
            this.tabControl = new System.Windows.Forms.TabControl();

            this.tabDocGia = new System.Windows.Forms.TabPage();
            this.tabThe = new System.Windows.Forms.TabPage();

            this.txtMaDG = new System.Windows.Forms.TextBox();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboPhai = new System.Windows.Forms.ComboBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();

            this.btnThemDG = new System.Windows.Forms.Button();
            this.btnSuaDG = new System.Windows.Forms.Button();
            this.btnXoaDG = new System.Windows.Forms.Button();

            this.dgvDocGia = new System.Windows.Forms.DataGridView();

            this.txtMaThe = new System.Windows.Forms.TextBox();
            this.txtMaDGThe = new System.Windows.Forms.TextBox();
            this.dtpNgayCap = new System.Windows.Forms.DateTimePicker();
            this.dtpHanSuDung = new System.Windows.Forms.DateTimePicker();
            this.chkDongLePhi = new System.Windows.Forms.CheckBox();
            this.chkTrangThai = new System.Windows.Forms.CheckBox();

            this.btnCapThe = new System.Windows.Forms.Button();
            this.btnGiaHan = new System.Windows.Forms.Button();

            this.dgvThe = new System.Windows.Forms.DataGridView();

            this.tabControl.SuspendLayout();
            this.tabDocGia.SuspendLayout();
            this.tabThe.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThe)).BeginInit();

            this.SuspendLayout();

            // TITLE
            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Times New Roman", 20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(150, 15);

            this.lblTitle.Size =
                new System.Drawing.Size(700, 50);

            this.lblTitle.Text =
                "QUẢN LÝ ĐỘC GIẢ VÀ THẺ";

            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // TAB
            this.tabControl.Location =
                new System.Drawing.Point(20, 75);

            this.tabControl.Size =
                new System.Drawing.Size(950, 520);

            this.tabControl.Controls.Add(this.tabDocGia);
            this.tabControl.Controls.Add(this.tabThe);

            // ================= ĐỘC GIẢ =================
            this.tabDocGia.Text = "Độc giả";

            AddLabel(this.tabDocGia, "Mã độc giả:", 20, 25);
            SetupText(this.txtMaDG, 120, 22, 160);

            AddLabel(this.tabDocGia, "Họ:", 310, 25);
            SetupText(this.txtHo, 350, 22, 160);

            AddLabel(this.tabDocGia, "Tên:", 550, 25);
            SetupText(this.txtTen, 600, 22, 160);

            AddLabel(this.tabDocGia, "Ngày sinh:", 20, 70);
            this.dtpNgaySinh.Location =
                new System.Drawing.Point(120, 67);
            this.dtpNgaySinh.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            AddLabel(this.tabDocGia, "Phái:", 310, 70);
            this.cboPhai.Location =
                new System.Drawing.Point(350, 67);
            this.cboPhai.Items.AddRange(
                new object[] { "Nam", "Nữ" });

            AddLabel(this.tabDocGia, "SĐT:", 550, 70);
            SetupText(this.txtSDT, 600, 67, 160);

            AddLabel(this.tabDocGia, "Địa chỉ:", 20, 115);
            SetupText(this.txtDiaChi, 120, 112, 390);

            AddLabel(this.tabDocGia, "Email:", 550, 115);
            SetupText(this.txtEmail, 600, 112, 250);

            SetupButton(this.btnThemDG, "Thêm", 300, 155);
            SetupButton(this.btnSuaDG, "Sửa", 400, 155);
            SetupButton(this.btnXoaDG, "Xóa", 500, 155);

            this.dgvDocGia.Location =
                new System.Drawing.Point(20, 205);
            this.dgvDocGia.Size =
                new System.Drawing.Size(900, 260);
            SetupGrid(this.dgvDocGia);

            this.tabDocGia.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    txtMaDG, txtHo, txtTen,
                    dtpNgaySinh, cboPhai,
                    txtSDT, txtDiaChi, txtEmail,
                    btnThemDG, btnSuaDG, btnXoaDG,
                    dgvDocGia
                });

            // ================= THẺ =================
            this.tabThe.Text = "Thẻ độc giả";

            AddLabel(this.tabThe, "Mã thẻ:", 30, 35);
            SetupText(this.txtMaThe, 120, 32, 170);

            AddLabel(this.tabThe, "Mã độc giả:", 330, 35);
            SetupText(this.txtMaDGThe, 430, 32, 170);

            AddLabel(this.tabThe, "Ngày cấp:", 30, 80);
            this.dtpNgayCap.Location =
                new System.Drawing.Point(120, 77);
            this.dtpNgayCap.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            AddLabel(this.tabThe, "Hạn sử dụng:", 330, 80);
            this.dtpHanSuDung.Location =
                new System.Drawing.Point(430, 77);
            this.dtpHanSuDung.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.chkDongLePhi.Text = "Đã đóng lệ phí";
            this.chkDongLePhi.Location =
                new System.Drawing.Point(650, 35);
            this.chkDongLePhi.AutoSize = true;

            this.chkTrangThai.Text = "Đang hoạt động";
            this.chkTrangThai.Location =
                new System.Drawing.Point(650, 80);
            this.chkTrangThai.AutoSize = true;

            SetupButton(this.btnCapThe, "Cấp thẻ", 350, 130);
            SetupButton(this.btnGiaHan, "Gia hạn", 460, 130);

            this.dgvThe.Location =
                new System.Drawing.Point(30, 190);
            this.dgvThe.Size =
                new System.Drawing.Size(880, 270);
            SetupGrid(this.dgvThe);

            this.tabThe.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    txtMaThe, txtMaDGThe,
                    dtpNgayCap, dtpHanSuDung,
                    chkDongLePhi, chkTrangThai,
                    btnCapThe, btnGiaHan,
                    dgvThe
                });

            // FORM
            this.ClientSize =
                new System.Drawing.Size(1000, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabControl);

            this.Name = "FrmDocGia";
            this.Text = "Độc giả và thẻ";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.tabControl.ResumeLayout(false);
            this.tabDocGia.ResumeLayout(false);
            this.tabDocGia.PerformLayout();
            this.tabThe.ResumeLayout(false);
            this.tabThe.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvDocGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThe)).EndInit();

            this.ResumeLayout(false);
        }

        private void AddLabel(
            System.Windows.Forms.Control parent,
            string text, int x, int y)
        {
            System.Windows.Forms.Label lbl =
                new System.Windows.Forms.Label();

            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Location = new System.Drawing.Point(x, y);

            parent.Controls.Add(lbl);
        }

        private void SetupText(
            System.Windows.Forms.TextBox txt,
            int x, int y, int width)
        {
            txt.Location = new System.Drawing.Point(x, y);
            txt.Size = new System.Drawing.Size(width, 22);
        }

        private void SetupButton(
            System.Windows.Forms.Button btn,
            string text, int x, int y)
        {
            btn.Text = text;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(90, 35);
        }

        private void SetupGrid(
            System.Windows.Forms.DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDocGia;
        private System.Windows.Forms.TabPage tabThe;

        private System.Windows.Forms.TextBox txtMaDG;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboPhai;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnThemDG;
        private System.Windows.Forms.Button btnSuaDG;
        private System.Windows.Forms.Button btnXoaDG;
        private System.Windows.Forms.DataGridView dgvDocGia;

        private System.Windows.Forms.TextBox txtMaThe;
        private System.Windows.Forms.TextBox txtMaDGThe;
        private System.Windows.Forms.DateTimePicker dtpNgayCap;
        private System.Windows.Forms.DateTimePicker dtpHanSuDung;
        private System.Windows.Forms.CheckBox chkDongLePhi;
        private System.Windows.Forms.CheckBox chkTrangThai;
        private System.Windows.Forms.Button btnCapThe;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.DataGridView dgvThe;
    }
}