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
            this.lblTitle =
                new System.Windows.Forms.Label();

            this.dtTu =
                new System.Windows.Forms.DateTimePicker();

            this.dtDen =
                new System.Windows.Forms.DateTimePicker();

            this.btnThongKe =
                new System.Windows.Forms.Button();

            this.lblMuon =
                new System.Windows.Forms.Label();

            this.lblQuaHan =
                new System.Windows.Forms.Label();

            this.lblMat =
                new System.Windows.Forms.Label();

            this.lblHuHong =
                new System.Windows.Forms.Label();

            this.lblPhiPhat =
                new System.Windows.Forms.Label();

            this.dgvPhat =
                new System.Windows.Forms.DataGridView();

            this.btnDong =
                new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvPhat)).BeginInit();

            this.SuspendLayout();

            // =====================================================
            // TITLE
            // =====================================================
            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Times New Roman",
                    20F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.SystemColors.Highlight;

            this.lblTitle.Location =
                new System.Drawing.Point(
                    185, 20);

            this.lblTitle.Size =
                new System.Drawing.Size(
                    700, 45);

            this.lblTitle.Text =
                "THỐNG KÊ THƯ VIỆN";

            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // =====================================================
            // TỪ NGÀY
            // =====================================================
            AddLabel(
                "Từ ngày:",
                45, 95);

            this.dtTu.Location =
                new System.Drawing.Point(
                    115, 92);

            this.dtTu.Size =
                new System.Drawing.Size(
                    170, 25);

            this.dtTu.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // =====================================================
            // ĐẾN NGÀY
            // =====================================================
            AddLabel(
                "Đến ngày:",
                330, 95);

            this.dtDen.Location =
                new System.Drawing.Point(
                    410, 92);

            this.dtDen.Size =
                new System.Drawing.Size(
                    170, 25);

            this.dtDen.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // =====================================================
            // BUTTON THỐNG KÊ
            // =====================================================
            this.btnThongKe.Text =
                "Thống kê";

            this.btnThongKe.Location =
                new System.Drawing.Point(
                    630, 88);

            this.btnThongKe.Size =
                new System.Drawing.Size(
                    130, 35);

            // =====================================================
            // KPI
            // =====================================================
            SetupKpiLabel(
                this.lblMuon,
                "Lượt sách mượn: 0",
                50, 155);

            SetupKpiLabel(
                this.lblQuaHan,
                "Sách quá hạn: 0",
                360, 155);

            SetupKpiLabel(
                this.lblMat,
                "Sách mất: 0",
                670, 155);

            SetupKpiLabel(
                this.lblHuHong,
                "Sách hư hỏng: 0",
                200, 210);

            SetupKpiLabel(
                this.lblPhiPhat,
                "Tổng phí phạt: 0 đ",
                540, 210);

            // =====================================================
            // TIÊU ĐỀ BẢNG
            // =====================================================
            AddLabel(
                "CHI TIẾT PHIẾU PHẠT",
                45, 285);

            // =====================================================
            // DATAGRIDVIEW
            // =====================================================
            this.dgvPhat.Location =
                new System.Drawing.Point(
                    45, 315);

            this.dgvPhat.Size =
                new System.Drawing.Size(
                    980, 285);

            this.dgvPhat.AllowUserToAddRows =
                false;

            this.dgvPhat.AllowUserToDeleteRows =
                false;

            this.dgvPhat.ReadOnly = true;

            this.dgvPhat.SelectionMode =
                System.Windows.Forms
                    .DataGridViewSelectionMode.FullRowSelect;

            this.dgvPhat.AutoSizeColumnsMode =
                System.Windows.Forms
                    .DataGridViewAutoSizeColumnsMode.Fill;

            // =====================================================
            // ĐÓNG
            // =====================================================
            this.btnDong.Text =
                "Đóng";

            this.btnDong.Location =
                new System.Drawing.Point(
                    455, 625);

            this.btnDong.Size =
                new System.Drawing.Size(
                    150, 40);

            // =====================================================
            // FORM
            // =====================================================
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(
                    7F, 15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(
                    1070, 700);

            this.Controls.Add(
                this.lblTitle);

            this.Controls.Add(
                this.dtTu);

            this.Controls.Add(
                this.dtDen);

            this.Controls.Add(
                this.btnThongKe);

            this.Controls.Add(
                this.lblMuon);

            this.Controls.Add(
                this.lblQuaHan);

            this.Controls.Add(
                this.lblMat);

            this.Controls.Add(
                this.lblHuHong);

            this.Controls.Add(
                this.lblPhiPhat);

            this.Controls.Add(
                this.dgvPhat);

            this.Controls.Add(
                this.btnDong);

            this.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.Name =
                "FrmThongKe";

            this.Text =
                "Thống kê";

            this.StartPosition =
                System.Windows.Forms
                    .FormStartPosition.CenterParent;

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvPhat)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // =========================================================
        // LABEL THƯỜNG
        // =========================================================
        private void AddLabel(
            string text,
            int x,
            int y)
        {
            System.Windows.Forms.Label lbl =
                new System.Windows.Forms.Label();

            lbl.Text = text;
            lbl.AutoSize = true;

            lbl.Location =
                new System.Drawing.Point(
                    x, y);

            this.Controls.Add(lbl);
        }

        // =========================================================
        // LABEL KPI
        // =========================================================
        private void SetupKpiLabel(
            System.Windows.Forms.Label lbl,
            string text,
            int x,
            int y)
        {
            lbl.Text = text;

            lbl.Location =
                new System.Drawing.Point(
                    x, y);

            lbl.Size =
                new System.Drawing.Size(
                    280, 35);

            lbl.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F,
                    System.Drawing.FontStyle.Bold);
        }

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.DateTimePicker dtTu;
        private System.Windows.Forms.DateTimePicker dtDen;

        private System.Windows.Forms.Button btnThongKe;

        private System.Windows.Forms.Label lblMuon;
        private System.Windows.Forms.Label lblQuaHan;
        private System.Windows.Forms.Label lblMat;
        private System.Windows.Forms.Label lblHuHong;
        private System.Windows.Forms.Label lblPhiPhat;

        private System.Windows.Forms.DataGridView dgvPhat;

        private System.Windows.Forms.Button btnDong;
    }
}