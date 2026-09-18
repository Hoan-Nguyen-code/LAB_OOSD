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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMuon = new System.Windows.Forms.TabPage();
            this.tabTra = new System.Windows.Forms.TabPage();

            this.cboDocGia = new System.Windows.Forms.ComboBox();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.dtpNgayMuon = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayHenTra = new System.Windows.Forms.DateTimePicker();
            this.dgvSachMuon = new System.Windows.Forms.DataGridView();
            this.btnLapPhieuMuon = new System.Windows.Forms.Button();

            this.txtMaPhieuMuon = new System.Windows.Forms.TextBox();
            this.dgvSachTra = new System.Windows.Forms.DataGridView();
            this.btnTraSach = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabMuon.SuspendLayout();
            this.tabTra.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSachMuon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachTra)).BeginInit();

            this.SuspendLayout();

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

            this.lblTitle.Text = "QUẢN LÝ MƯỢN - TRẢ SÁCH";
            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.tabControl.Location =
                new System.Drawing.Point(20, 75);

            this.tabControl.Size =
                new System.Drawing.Size(950, 520);

            this.tabControl.Controls.Add(this.tabMuon);
            this.tabControl.Controls.Add(this.tabTra);

            // MƯỢN
            this.tabMuon.Text = "Mượn sách";

            AddLabel(this.tabMuon, "Độc giả:", 25, 30);
            this.cboDocGia.Location =
                new System.Drawing.Point(100, 27);
            this.cboDocGia.Size =
                new System.Drawing.Size(200, 24);

            AddLabel(this.tabMuon, "Nhân viên:", 340, 30);
            this.cboNhanVien.Location =
                new System.Drawing.Point(420, 27);
            this.cboNhanVien.Size =
                new System.Drawing.Size(200, 24);

            AddLabel(this.tabMuon, "Ngày mượn:", 25, 75);
            this.dtpNgayMuon.Location =
                new System.Drawing.Point(110, 72);
            this.dtpNgayMuon.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            AddLabel(this.tabMuon, "Ngày hẹn trả:", 340, 75);
            this.dtpNgayHenTra.Location =
                new System.Drawing.Point(440, 72);
            this.dtpNgayHenTra.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            AddLabel(
                this.tabMuon,
                "Chọn các đầu sách cần mượn:",
                25, 120);

            this.dgvSachMuon.Location =
                new System.Drawing.Point(25, 150);

            this.dgvSachMuon.Size =
                new System.Drawing.Size(880, 250);

            SetupGrid(this.dgvSachMuon);

            this.btnLapPhieuMuon.Text = "Lập phiếu mượn";
            this.btnLapPhieuMuon.Location =
                new System.Drawing.Point(390, 420);

            this.btnLapPhieuMuon.Size =
                new System.Drawing.Size(150, 38);

            this.tabMuon.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    cboDocGia, cboNhanVien,
                    dtpNgayMuon, dtpNgayHenTra,
                    dgvSachMuon, btnLapPhieuMuon
                });

            // TRẢ
            this.tabTra.Text = "Trả sách";

            AddLabel(
                this.tabTra,
                "Mã phiếu mượn:",
                30, 35);

            this.txtMaPhieuMuon.Location =
                new System.Drawing.Point(150, 32);

            this.txtMaPhieuMuon.Size =
                new System.Drawing.Size(200, 22);

            AddLabel(
                this.tabTra,
                "Danh sách sách đang mượn:",
                30, 85);

            this.dgvSachTra.Location =
                new System.Drawing.Point(30, 115);

            this.dgvSachTra.Size =
                new System.Drawing.Size(870, 280);

            SetupGrid(this.dgvSachTra);

            this.btnTraSach.Text = "Xác nhận trả sách";

            this.btnTraSach.Location =
                new System.Drawing.Point(380, 420);

            this.btnTraSach.Size =
                new System.Drawing.Size(170, 38);

            this.tabTra.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    txtMaPhieuMuon,
                    dgvSachTra,
                    btnTraSach
                });

            this.ClientSize =
                new System.Drawing.Size(1000, 620);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabControl);

            this.Name = "FrmMuonTra";
            this.Text = "Mượn - Trả sách";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.tabControl.ResumeLayout(false);
            this.tabMuon.ResumeLayout(false);
            this.tabMuon.PerformLayout();
            this.tabTra.ResumeLayout(false);
            this.tabTra.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSachMuon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachTra)).EndInit();

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
            lbl.Location =
                new System.Drawing.Point(x, y);

            parent.Controls.Add(lbl);
        }

        private void SetupGrid(
            System.Windows.Forms.DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMuon;
        private System.Windows.Forms.TabPage tabTra;

        private System.Windows.Forms.ComboBox cboDocGia;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.DateTimePicker dtpNgayMuon;
        private System.Windows.Forms.DateTimePicker dtpNgayHenTra;
        private System.Windows.Forms.DataGridView dgvSachMuon;
        private System.Windows.Forms.Button btnLapPhieuMuon;

        private System.Windows.Forms.TextBox txtMaPhieuMuon;
        private System.Windows.Forms.DataGridView dgvSachTra;
        private System.Windows.Forms.Button btnTraSach;
    }
}