namespace QuanLyKhachSan
{
    partial class FrmPhongTienNghi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPhong = new System.Windows.Forms.TabPage();
            this.tabTienNghi = new System.Windows.Forms.TabPage();
            this.tabLapDat = new System.Windows.Forms.TabPage();

            // ===== PHÒNG =====
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.lblPhong = new System.Windows.Forms.Label();
            this.lblKhu = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblGia = new System.Windows.Forms.Label();
            this.txtPhong = new System.Windows.Forms.TextBox();
            this.cboKhu = new System.Windows.Forms.ComboBox();
            this.numMax = new System.Windows.Forms.NumericUpDown();
            this.numGia = new System.Windows.Forms.NumericUpDown();
            this.btnThemPhong = new System.Windows.Forms.Button();

            // ===== TIỆN NGHI =====
            this.dgvTN = new System.Windows.Forms.DataGridView();
            this.lblMaTN = new System.Windows.Forms.Label();
            this.lblLoai = new System.Windows.Forms.Label();
            this.lblSTT = new System.Windows.Forms.Label();
            this.lblTinhTrang = new System.Windows.Forms.Label();
            this.txtMaTN = new System.Windows.Forms.TextBox();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.numSTT = new System.Windows.Forms.NumericUpDown();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.btnThemTN = new System.Windows.Forms.Button();

            // ===== LẮP ĐẶT =====
            this.dgvLD = new System.Windows.Forms.DataGridView();
            this.lblSoLD = new System.Windows.Forms.Label();
            this.lblTN = new System.Windows.Forms.Label();
            this.lblPhongLD = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblTTLD = new System.Windows.Forms.Label();
            this.lblNV = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();

            this.txtSoLD = new System.Windows.Forms.TextBox();
            this.cboTN = new System.Windows.Forms.ComboBox();
            this.cboPhong = new System.Windows.Forms.ComboBox();
            this.dtNgay = new System.Windows.Forms.DateTimePicker();
            this.txtTTLD = new System.Windows.Forms.TextBox();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnLapDat = new System.Windows.Forms.Button();

            this.tabMain.SuspendLayout();
            this.tabPhong.SuspendLayout();
            this.tabTienNghi.SuspendLayout();
            this.tabLapDat.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSTT)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.dgvLD)).BeginInit();

            this.SuspendLayout();

            // =========================
            // TAB CONTROL
            // =========================

            this.tabMain.Controls.Add(this.tabPhong);
            this.tabMain.Controls.Add(this.tabTienNghi);
            this.tabMain.Controls.Add(this.tabLapDat);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1010, 590);

            // ==================================================
            // TAB PHÒNG
            // ==================================================

            this.tabPhong.Text = "Phòng";
            this.tabPhong.UseVisualStyleBackColor = true;

            this.dgvPhong.Location = new System.Drawing.Point(20, 20);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.Size = new System.Drawing.Size(950, 350);
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.lblPhong.AutoSize = true;
            this.lblPhong.Location = new System.Drawing.Point(30, 405);
            this.lblPhong.Text = "Số phòng:";

            this.txtPhong.Location = new System.Drawing.Point(110, 402);
            this.txtPhong.Size = new System.Drawing.Size(160, 20);

            this.lblKhu.AutoSize = true;
            this.lblKhu.Location = new System.Drawing.Point(310, 405);
            this.lblKhu.Text = "Khu vực:";

            this.cboKhu.Location = new System.Drawing.Point(380, 402);
            this.cboKhu.Size = new System.Drawing.Size(180, 21);
            this.cboKhu.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(30, 455);
            this.lblMax.Text = "Số người tối đa:";

            this.numMax.Location = new System.Drawing.Point(130, 452);
            this.numMax.Size = new System.Drawing.Size(140, 20);
            this.numMax.Minimum = 1;
            this.numMax.Maximum = 100;
            this.numMax.Value = 1;

            this.lblGia.AutoSize = true;
            this.lblGia.Location = new System.Drawing.Point(310, 455);
            this.lblGia.Text = "Đơn giá/ngày:";

            this.numGia.Location = new System.Drawing.Point(400, 452);
            this.numGia.Size = new System.Drawing.Size(160, 20);
            this.numGia.Maximum = 1000000000;
            this.numGia.ThousandsSeparator = true;

            this.btnThemPhong.Location = new System.Drawing.Point(650, 420);
            this.btnThemPhong.Name = "btnThemPhong";
            this.btnThemPhong.Size = new System.Drawing.Size(150, 45);
            this.btnThemPhong.Text = "Thêm phòng";
            this.btnThemPhong.UseVisualStyleBackColor = true;

            this.tabPhong.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.dgvPhong,
                    this.lblPhong,
                    this.txtPhong,
                    this.lblKhu,
                    this.cboKhu,
                    this.lblMax,
                    this.numMax,
                    this.lblGia,
                    this.numGia,
                    this.btnThemPhong
                });

            // ==================================================
            // TAB TIỆN NGHI
            // ==================================================

            this.tabTienNghi.Text = "Tiện nghi";
            this.tabTienNghi.UseVisualStyleBackColor = true;

            this.dgvTN.Location = new System.Drawing.Point(20, 20);
            this.dgvTN.Name = "dgvTN";
            this.dgvTN.Size = new System.Drawing.Size(950, 350);
            this.dgvTN.ReadOnly = true;
            this.dgvTN.AllowUserToAddRows = false;
            this.dgvTN.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.lblMaTN.AutoSize = true;
            this.lblMaTN.Location = new System.Drawing.Point(30, 405);
            this.lblMaTN.Text = "Mã tiện nghi:";

            this.txtMaTN.Location = new System.Drawing.Point(120, 402);
            this.txtMaTN.Size = new System.Drawing.Size(160, 20);

            this.lblLoai.AutoSize = true;
            this.lblLoai.Location = new System.Drawing.Point(320, 405);
            this.lblLoai.Text = "Loại tiện nghi:";

            this.cboLoai.Location = new System.Drawing.Point(410, 402);
            this.cboLoai.Size = new System.Drawing.Size(180, 21);
            this.cboLoai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblSTT.AutoSize = true;
            this.lblSTT.Location = new System.Drawing.Point(30, 455);
            this.lblSTT.Text = "Số thứ tự:";

            this.numSTT.Location = new System.Drawing.Point(120, 452);
            this.numSTT.Size = new System.Drawing.Size(160, 20);
            this.numSTT.Minimum = 1;
            this.numSTT.Maximum = 10000;
            this.numSTT.Value = 1;

            this.lblTinhTrang.AutoSize = true;
            this.lblTinhTrang.Location = new System.Drawing.Point(320, 455);
            this.lblTinhTrang.Text = "Tình trạng:";

            this.txtTinhTrang.Location = new System.Drawing.Point(410, 452);
            this.txtTinhTrang.Size = new System.Drawing.Size(180, 20);

            this.btnThemTN.Location = new System.Drawing.Point(660, 420);
            this.btnThemTN.Name = "btnThemTN";
            this.btnThemTN.Size = new System.Drawing.Size(150, 45);
            this.btnThemTN.Text = "Thêm tiện nghi";
            this.btnThemTN.UseVisualStyleBackColor = true;

            this.tabTienNghi.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.dgvTN,
                    this.lblMaTN,
                    this.txtMaTN,
                    this.lblLoai,
                    this.cboLoai,
                    this.lblSTT,
                    this.numSTT,
                    this.lblTinhTrang,
                    this.txtTinhTrang,
                    this.btnThemTN
                });

            // ==================================================
            // TAB LẮP ĐẶT / LUÂN CHUYỂN
            // ==================================================

            this.tabLapDat.Text = "Lắp đặt / Luân chuyển";
            this.tabLapDat.UseVisualStyleBackColor = true;

            this.dgvLD.Location = new System.Drawing.Point(20, 20);
            this.dgvLD.Name = "dgvLD";
            this.dgvLD.Size = new System.Drawing.Size(950, 310);
            this.dgvLD.ReadOnly = true;
            this.dgvLD.AllowUserToAddRows = false;
            this.dgvLD.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Số phiếu
            this.lblSoLD.AutoSize = true;
            this.lblSoLD.Location = new System.Drawing.Point(30, 365);
            this.lblSoLD.Text = "Số phiếu:";

            this.txtSoLD.Location = new System.Drawing.Point(110, 362);
            this.txtSoLD.Size = new System.Drawing.Size(160, 20);

            // Tiện nghi
            this.lblTN.AutoSize = true;
            this.lblTN.Location = new System.Drawing.Point(310, 365);
            this.lblTN.Text = "Tiện nghi:";

            this.cboTN.Location = new System.Drawing.Point(390, 362);
            this.cboTN.Size = new System.Drawing.Size(180, 21);
            this.cboTN.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Phòng
            this.lblPhongLD.AutoSize = true;
            this.lblPhongLD.Location = new System.Drawing.Point(610, 365);
            this.lblPhongLD.Text = "Phòng:";

            this.cboPhong.Location = new System.Drawing.Point(670, 362);
            this.cboPhong.Size = new System.Drawing.Size(160, 21);
            this.cboPhong.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Ngày
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(30, 410);
            this.lblNgay.Text = "Ngày lập:";

            this.dtNgay.Location = new System.Drawing.Point(110, 407);
            this.dtNgay.Size = new System.Drawing.Size(200, 20);
            this.dtNgay.Format =
                System.Windows.Forms.DateTimePickerFormat.Short;

            // Tình trạng
            this.lblTTLD.AutoSize = true;
            this.lblTTLD.Location = new System.Drawing.Point(350, 410);
            this.lblTTLD.Text = "Tình trạng:";

            this.txtTTLD.Location = new System.Drawing.Point(430, 407);
            this.txtTTLD.Size = new System.Drawing.Size(160, 20);

            // Nhân viên
            this.lblNV.AutoSize = true;
            this.lblNV.Location = new System.Drawing.Point(630, 410);
            this.lblNV.Text = "Nhân viên:";

            this.cboNV.Location = new System.Drawing.Point(700, 407);
            this.cboNV.Size = new System.Drawing.Size(180, 21);
            this.cboNV.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Ghi chú
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(30, 455);
            this.lblGhiChu.Text = "Ghi chú:";

            this.txtGhiChu.Location = new System.Drawing.Point(110, 452);
            this.txtGhiChu.Size = new System.Drawing.Size(480, 20);

            // Button lập phiếu
            this.btnLapDat.Location = new System.Drawing.Point(680, 445);
            this.btnLapDat.Name = "btnLapDat";
            this.btnLapDat.Size = new System.Drawing.Size(150, 40);
            this.btnLapDat.Text = "Lập phiếu";
            this.btnLapDat.UseVisualStyleBackColor = true;

            this.tabLapDat.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    this.dgvLD,
                    this.lblSoLD,
                    this.txtSoLD,
                    this.lblTN,
                    this.cboTN,
                    this.lblPhongLD,
                    this.cboPhong,
                    this.lblNgay,
                    this.dtNgay,
                    this.lblTTLD,
                    this.txtTTLD,
                    this.lblNV,
                    this.cboNV,
                    this.lblGhiChu,
                    this.txtGhiChu,
                    this.btnLapDat
                });

            // ==================================================
            // FORM
            // ==================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(1034, 611);

            this.Controls.Add(this.tabMain);

            this.Name = "FrmPhongTienNghi";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý phòng - tiện nghi";

            this.tabMain.ResumeLayout(false);

            this.tabPhong.ResumeLayout(false);
            this.tabPhong.PerformLayout();

            this.tabTienNghi.ResumeLayout(false);
            this.tabTienNghi.PerformLayout();

            this.tabLapDat.ResumeLayout(false);
            this.tabLapDat.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSTT)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.dgvLD)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPhong;
        private System.Windows.Forms.TabPage tabTienNghi;
        private System.Windows.Forms.TabPage tabLapDat;

        // Phòng
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.Label lblPhong;
        private System.Windows.Forms.Label lblKhu;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.TextBox txtPhong;
        private System.Windows.Forms.ComboBox cboKhu;
        private System.Windows.Forms.NumericUpDown numMax;
        private System.Windows.Forms.NumericUpDown numGia;
        private System.Windows.Forms.Button btnThemPhong;

        // Tiện nghi
        private System.Windows.Forms.DataGridView dgvTN;
        private System.Windows.Forms.Label lblMaTN;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.Label lblSTT;
        private System.Windows.Forms.Label lblTinhTrang;
        private System.Windows.Forms.TextBox txtMaTN;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.NumericUpDown numSTT;
        private System.Windows.Forms.TextBox txtTinhTrang;
        private System.Windows.Forms.Button btnThemTN;

        // Lắp đặt
        private System.Windows.Forms.DataGridView dgvLD;
        private System.Windows.Forms.Label lblSoLD;
        private System.Windows.Forms.Label lblTN;
        private System.Windows.Forms.Label lblPhongLD;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblTTLD;
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.Label lblGhiChu;

        private System.Windows.Forms.TextBox txtSoLD;
        private System.Windows.Forms.ComboBox cboTN;
        private System.Windows.Forms.ComboBox cboPhong;
        private System.Windows.Forms.DateTimePicker dtNgay;
        private System.Windows.Forms.TextBox txtTTLD;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnLapDat;
    }
}