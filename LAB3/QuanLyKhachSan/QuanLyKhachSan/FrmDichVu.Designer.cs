namespace QuanLyKhachSan
{
    partial class FrmDichVu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();

            this.lblLuot = new System.Windows.Forms.Label();
            this.lblPhong = new System.Windows.Forms.Label();
            this.lblDV = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblNV = new System.Windows.Forms.Label();
            this.lblSL = new System.Windows.Forms.Label();
            this.lblLichSu = new System.Windows.Forms.Label();

            this.cboLuot = new System.Windows.Forms.ComboBox();
            this.txtPhong = new System.Windows.Forms.TextBox();
            this.cboDV = new System.Windows.Forms.ComboBox();
            this.dtNgay = new System.Windows.Forms.DateTimePicker();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.numSL = new System.Windows.Forms.NumericUpDown();

            this.btnGhi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.dgvLichSu = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.numSL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();

            this.SuspendLayout();

            // =========================
            // TIÊU ĐỀ
            // =========================
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font =
                new System.Drawing.Font(
                    "Microsoft Sans Serif",
                    16F,
                    System.Drawing.FontStyle.Bold);

            this.lblTieuDe.Location =
                new System.Drawing.Point(330, 25);

            this.lblTieuDe.Text =
                "GHI NHẬN SỬ DỤNG DỊCH VỤ";

            // =========================
            // PHIẾU ĐANG Ở
            // =========================
            this.lblLuot.AutoSize = true;
            this.lblLuot.Location =
                new System.Drawing.Point(40, 95);

            this.lblLuot.Text =
                "Phiếu đang ở:";

            this.cboLuot.Location =
                new System.Drawing.Point(140, 92);

            this.cboLuot.Name = "cboLuot";

            this.cboLuot.Size =
                new System.Drawing.Size(180, 21);

            this.cboLuot.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // =========================
            // PHÒNG
            // =========================
            this.lblPhong.AutoSize = true;
            this.lblPhong.Location =
                new System.Drawing.Point(360, 95);

            this.lblPhong.Text = "Phòng:";

            this.txtPhong.Location =
                new System.Drawing.Point(420, 92);

            this.txtPhong.Name = "txtPhong";

            this.txtPhong.Size =
                new System.Drawing.Size(150, 20);

            this.txtPhong.ReadOnly = true;

            // =========================
            // DỊCH VỤ
            // =========================
            this.lblDV.AutoSize = true;
            this.lblDV.Location =
                new System.Drawing.Point(620, 95);

            this.lblDV.Text = "Dịch vụ:";

            this.cboDV.Location =
                new System.Drawing.Point(680, 92);

            this.cboDV.Name = "cboDV";

            this.cboDV.Size =
                new System.Drawing.Size(210, 21);

            this.cboDV.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // =========================
            // NGÀY SỬ DỤNG
            // =========================
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location =
                new System.Drawing.Point(40, 155);

            this.lblNgay.Text =
                "Ngày sử dụng:";

            this.dtNgay.Location =
                new System.Drawing.Point(140, 152);

            this.dtNgay.Name = "dtNgay";

            this.dtNgay.Size =
                new System.Drawing.Size(180, 20);

            this.dtNgay.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // =========================
            // NHÂN VIÊN
            // =========================
            this.lblNV.AutoSize = true;
            this.lblNV.Location =
                new System.Drawing.Point(360, 155);

            this.lblNV.Text =
                "Nhân viên:";

            this.cboNV.Location =
                new System.Drawing.Point(430, 152);

            this.cboNV.Name = "cboNV";

            this.cboNV.Size =
                new System.Drawing.Size(180, 21);

            this.cboNV.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // =========================
            // SỐ LƯỢNG
            // =========================
            this.lblSL.AutoSize = true;
            this.lblSL.Location =
                new System.Drawing.Point(650, 155);

            this.lblSL.Text =
                "Số lượng:";

            this.numSL.Location =
                new System.Drawing.Point(720, 152);

            this.numSL.Name = "numSL";

            this.numSL.Size =
                new System.Drawing.Size(100, 20);

            this.numSL.Minimum = 1;
            this.numSL.Maximum = 1000;
            this.numSL.Value = 1;

            // =========================
            // NÚT GHI
            // =========================
            this.btnGhi.Location =
                new System.Drawing.Point(330, 215);

            this.btnGhi.Name = "btnGhi";

            this.btnGhi.Size =
                new System.Drawing.Size(170, 45);

            this.btnGhi.Text =
                "Ghi nhận dịch vụ";

            this.btnGhi.UseVisualStyleBackColor = true;

            // =========================
            // NÚT ĐÓNG
            // =========================
            this.btnDong.Location =
                new System.Drawing.Point(530, 215);

            this.btnDong.Name = "btnDong";

            this.btnDong.Size =
                new System.Drawing.Size(130, 45);

            this.btnDong.Text = "Đóng";

            this.btnDong.UseVisualStyleBackColor = true;

            // =========================
            // LỊCH SỬ
            // =========================
            this.lblLichSu.AutoSize = true;
            this.lblLichSu.Location =
                new System.Drawing.Point(40, 305);

            this.lblLichSu.Font =
                new System.Drawing.Font(
                    "Microsoft Sans Serif",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblLichSu.Text =
                "Lịch sử sử dụng dịch vụ";

            this.dgvLichSu.Location =
                new System.Drawing.Point(40, 335);

            this.dgvLichSu.Name =
                "dgvLichSu";

            this.dgvLichSu.Size =
                new System.Drawing.Size(950, 300);

            this.dgvLichSu.ReadOnly = true;

            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;

            this.dgvLichSu.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvLichSu.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // =========================
            // FORM
            // =========================
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(6F, 13F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(1030, 680);

            this.Controls.Add(this.lblTieuDe);

            this.Controls.Add(this.lblLuot);
            this.Controls.Add(this.cboLuot);

            this.Controls.Add(this.lblPhong);
            this.Controls.Add(this.txtPhong);

            this.Controls.Add(this.lblDV);
            this.Controls.Add(this.cboDV);

            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.dtNgay);

            this.Controls.Add(this.lblNV);
            this.Controls.Add(this.cboNV);

            this.Controls.Add(this.lblSL);
            this.Controls.Add(this.numSL);

            this.Controls.Add(this.btnGhi);
            this.Controls.Add(this.btnDong);

            this.Controls.Add(this.lblLichSu);
            this.Controls.Add(this.dgvLichSu);

            this.Name = "FrmDichVu";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.Text =
                "Quản lý sử dụng dịch vụ";

            ((System.ComponentModel.ISupportInitialize)(this.numSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTieuDe;

        private System.Windows.Forms.Label lblLuot;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.Label lblDV;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.Label lblSL;
        private System.Windows.Forms.Label lblLichSu;

        private System.Windows.Forms.ComboBox cboLuot;
        private System.Windows.Forms.TextBox txtPhong;
        private System.Windows.Forms.ComboBox cboDV;
        private System.Windows.Forms.DateTimePicker dtNgay;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.NumericUpDown numSL;

        private System.Windows.Forms.Button btnGhi;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.DataGridView dgvLichSu;
    }
}