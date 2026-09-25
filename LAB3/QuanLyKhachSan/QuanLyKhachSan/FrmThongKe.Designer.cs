namespace QuanLyKhachSan
{
    partial class FrmThongKe
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
            this.lblTieuDe = new System.Windows.Forms.Label();

            this.grpDieuKien = new System.Windows.Forms.GroupBox();
            this.lblTu = new System.Windows.Forms.Label();
            this.dtTu = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtDen = new System.Windows.Forms.DateTimePicker();
            this.btnTK = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.grpTongHop = new System.Windows.Forms.GroupBox();
            this.dgvTongHop = new System.Windows.Forms.DataGridView();

            this.grpDichVu = new System.Windows.Forms.GroupBox();
            this.dgvDV = new System.Windows.Forms.DataGridView();

            this.grpDieuKien.SuspendLayout();

            this.grpTongHop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTongHop)).BeginInit();

            this.grpDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvDV)).BeginInit();

            this.SuspendLayout();

            // =========================================================
            // lblTieuDe
            // =========================================================
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    18F,
                    System.Drawing.FontStyle.Bold);

            this.lblTieuDe.Location =
                new System.Drawing.Point(455, 20);

            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size =
                new System.Drawing.Size(190, 32);

            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "THỐNG KÊ";

            // =========================================================
            // grpDieuKien
            // =========================================================
            this.grpDieuKien.Controls.Add(this.lblTu);
            this.grpDieuKien.Controls.Add(this.dtTu);
            this.grpDieuKien.Controls.Add(this.lblDen);
            this.grpDieuKien.Controls.Add(this.dtDen);
            this.grpDieuKien.Controls.Add(this.btnTK);
            this.grpDieuKien.Controls.Add(this.btnDong);

            this.grpDieuKien.Location =
                new System.Drawing.Point(30, 70);

            this.grpDieuKien.Name = "grpDieuKien";
            this.grpDieuKien.Size =
                new System.Drawing.Size(1040, 100);

            this.grpDieuKien.TabIndex = 1;
            this.grpDieuKien.TabStop = false;
            this.grpDieuKien.Text = "Khoảng thời gian";

            // =========================================================
            // lblTu
            // =========================================================
            this.lblTu.AutoSize = true;

            this.lblTu.Location =
                new System.Drawing.Point(30, 42);

            this.lblTu.Name = "lblTu";
            this.lblTu.Size =
                new System.Drawing.Size(52, 15);

            this.lblTu.TabIndex = 0;
            this.lblTu.Text = "Từ ngày:";

            // =========================================================
            // dtTu
            // =========================================================
            this.dtTu.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtTu.Location =
                new System.Drawing.Point(95, 38);

            this.dtTu.Name = "dtTu";
            this.dtTu.Size =
                new System.Drawing.Size(150, 23);

            this.dtTu.TabIndex = 1;

            // =========================================================
            // lblDen
            // =========================================================
            this.lblDen.AutoSize = true;

            this.lblDen.Location =
                new System.Drawing.Point(285, 42);

            this.lblDen.Name = "lblDen";
            this.lblDen.Size =
                new System.Drawing.Size(59, 15);

            this.lblDen.TabIndex = 2;
            this.lblDen.Text = "Đến ngày:";

            // =========================================================
            // dtDen
            // =========================================================
            this.dtDen.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            this.dtDen.Location =
                new System.Drawing.Point(360, 38);

            this.dtDen.Name = "dtDen";
            this.dtDen.Size =
                new System.Drawing.Size(150, 23);

            this.dtDen.TabIndex = 3;

            // =========================================================
            // btnTK
            // =========================================================
            this.btnTK.Location =
                new System.Drawing.Point(570, 30);

            this.btnTK.Name = "btnTK";
            this.btnTK.Size =
                new System.Drawing.Size(160, 40);

            this.btnTK.TabIndex = 4;
            this.btnTK.Text = "Thống kê";
            this.btnTK.UseVisualStyleBackColor = true;

            // =========================================================
            // btnDong
            // =========================================================
            this.btnDong.Location =
                new System.Drawing.Point(760, 30);

            this.btnDong.Name = "btnDong";
            this.btnDong.Size =
                new System.Drawing.Size(160, 40);

            this.btnDong.TabIndex = 5;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;

            // =========================================================
            // grpTongHop
            // =========================================================
            this.grpTongHop.Controls.Add(this.dgvTongHop);

            this.grpTongHop.Location =
                new System.Drawing.Point(30, 190);

            this.grpTongHop.Name = "grpTongHop";
            this.grpTongHop.Size =
                new System.Drawing.Size(1040, 190);

            this.grpTongHop.TabIndex = 2;
            this.grpTongHop.TabStop = false;
            this.grpTongHop.Text = "Tổng hợp";

            // =========================================================
            // dgvTongHop
            // =========================================================
            this.dgvTongHop.AllowUserToAddRows = false;
            this.dgvTongHop.AllowUserToDeleteRows = false;
            this.dgvTongHop.AllowUserToResizeRows = false;

            this.dgvTongHop.AutoSizeColumnsMode =
                System.Windows.Forms
                    .DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvTongHop.ColumnHeadersHeightSizeMode =
                System.Windows.Forms
                    .DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvTongHop.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvTongHop.Location =
                new System.Drawing.Point(3, 19);

            this.dgvTongHop.MultiSelect = false;
            this.dgvTongHop.Name = "dgvTongHop";

            this.dgvTongHop.ReadOnly = true;
            this.dgvTongHop.RowHeadersWidth = 51;

            this.dgvTongHop.SelectionMode =
                System.Windows.Forms
                    .DataGridViewSelectionMode.FullRowSelect;

            this.dgvTongHop.Size =
                new System.Drawing.Size(1034, 168);

            this.dgvTongHop.TabIndex = 0;

            // =========================================================
            // grpDichVu
            // =========================================================
            this.grpDichVu.Controls.Add(this.dgvDV);

            this.grpDichVu.Location =
                new System.Drawing.Point(30, 400);

            this.grpDichVu.Name = "grpDichVu";
            this.grpDichVu.Size =
                new System.Drawing.Size(1040, 260);

            this.grpDichVu.TabIndex = 3;
            this.grpDichVu.TabStop = false;
            this.grpDichVu.Text = "Thống kê dịch vụ";

            // =========================================================
            // dgvDV
            // =========================================================
            this.dgvDV.AllowUserToAddRows = false;
            this.dgvDV.AllowUserToDeleteRows = false;
            this.dgvDV.AllowUserToResizeRows = false;

            this.dgvDV.AutoSizeColumnsMode =
                System.Windows.Forms
                    .DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvDV.ColumnHeadersHeightSizeMode =
                System.Windows.Forms
                    .DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            this.dgvDV.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.dgvDV.Location =
                new System.Drawing.Point(3, 19);

            this.dgvDV.MultiSelect = false;
            this.dgvDV.Name = "dgvDV";

            this.dgvDV.ReadOnly = true;
            this.dgvDV.RowHeadersWidth = 51;

            this.dgvDV.SelectionMode =
                System.Windows.Forms
                    .DataGridViewSelectionMode.FullRowSelect;

            this.dgvDV.Size =
                new System.Drawing.Size(1034, 238);

            this.dgvDV.TabIndex = 0;

            // =========================================================
            // FrmThongKe
            // =========================================================

            // Tắt AutoScale để tránh DataGridView bị thu nhỏ
            // trên máy có DPI / Display Scaling khác.
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.None;

            this.ClientSize =
                new System.Drawing.Size(1100, 690);

            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.grpDieuKien);
            this.Controls.Add(this.grpTongHop);
            this.Controls.Add(this.grpDichVu);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.Name = "FrmThongKe";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Thống kê";

            // =========================================================
            // Resume layout
            // =========================================================
            this.grpDieuKien.ResumeLayout(false);
            this.grpDieuKien.PerformLayout();

            this.grpTongHop.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTongHop)).EndInit();

            this.grpDichVu.ResumeLayout(false);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvDV)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;

        private System.Windows.Forms.GroupBox grpDieuKien;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtTu;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtDen;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.GroupBox grpTongHop;
        private System.Windows.Forms.DataGridView dgvTongHop;

        private System.Windows.Forms.GroupBox grpDichVu;
        private System.Windows.Forms.DataGridView dgvDV;
    }
}