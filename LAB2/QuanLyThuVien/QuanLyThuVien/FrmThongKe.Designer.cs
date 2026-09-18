namespace QuanLyThuVien
{
    partial class FrmThongKe
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

            this.btnSachDangMuon =
                new System.Windows.Forms.Button();

            this.btnSachQuaHan =
                new System.Windows.Forms.Button();

            this.btnDocGiaDangMuon =
                new System.Windows.Forms.Button();

            this.btnPhieuPhat =
                new System.Windows.Forms.Button();

            this.dgvThongKe =
                new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvThongKe)).BeginInit();

            this.SuspendLayout();

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Times New Roman", 20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(150, 20);

            this.lblTitle.Size =
                new System.Drawing.Size(650, 45);

            this.lblTitle.Text = "THỐNG KÊ THƯ VIỆN";

            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            SetupButton(
                this.btnSachDangMuon,
                "Sách đang mượn",
                100, 90);

            SetupButton(
                this.btnSachQuaHan,
                "Sách quá hạn",
                300, 90);

            SetupButton(
                this.btnDocGiaDangMuon,
                "Độc giả đang mượn",
                500, 90);

            SetupButton(
                this.btnPhieuPhat,
                "Phiếu phạt",
                700, 90);

            this.dgvThongKe.Location =
                new System.Drawing.Point(40, 155);

            this.dgvThongKe.Size =
                new System.Drawing.Size(900, 400);

            this.dgvThongKe.ReadOnly = true;
            this.dgvThongKe.AllowUserToAddRows = false;

            this.dgvThongKe.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.ClientSize =
                new System.Drawing.Size(980, 590);

            this.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    lblTitle,
                    btnSachDangMuon,
                    btnSachQuaHan,
                    btnDocGiaDangMuon,
                    btnPhieuPhat,
                    dgvThongKe
                });

            this.Name = "FrmThongKe";
            this.Text = "Thống kê";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvThongKe)).EndInit();

            this.ResumeLayout(false);
        }

        private void SetupButton(
            System.Windows.Forms.Button btn,
            string text, int x, int y)
        {
            btn.Text = text;
            btn.Location =
                new System.Drawing.Point(x, y);

            btn.Size =
                new System.Drawing.Size(160, 40);
        }

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Button btnSachDangMuon;
        private System.Windows.Forms.Button btnSachQuaHan;
        private System.Windows.Forms.Button btnDocGiaDangMuon;
        private System.Windows.Forms.Button btnPhieuPhat;

        private System.Windows.Forms.DataGridView dgvThongKe;
    }
}