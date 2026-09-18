namespace QuanLyThuVien
{
    partial class FrmMain
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
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnSach = new System.Windows.Forms.Button();
            this.btnDocGia = new System.Windows.Forms.Button();
            this.btnMuonTra = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font(
                "Times New Roman",
                20F,
                System.Drawing.FontStyle.Bold
            );
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblTitle.Location = new System.Drawing.Point(100, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(600, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.btnDanhMuc.Location = new System.Drawing.Point(130, 130);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(220, 45);
            this.btnDanhMuc.TabIndex = 1;
            this.btnDanhMuc.Text = "Danh mục / Nhân viên";
            this.btnDanhMuc.UseVisualStyleBackColor = true;

            // 
            // btnSach
            // 
            this.btnSach.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.btnSach.Location = new System.Drawing.Point(450, 130);
            this.btnSach.Name = "btnSach";
            this.btnSach.Size = new System.Drawing.Size(220, 45);
            this.btnSach.TabIndex = 2;
            this.btnSach.Text = "Quản lý đầu sách";
            this.btnSach.UseVisualStyleBackColor = true;

            // 
            // btnDocGia
            // 
            this.btnDocGia.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.btnDocGia.Location = new System.Drawing.Point(130, 205);
            this.btnDocGia.Name = "btnDocGia";
            this.btnDocGia.Size = new System.Drawing.Size(220, 45);
            this.btnDocGia.TabIndex = 3;
            this.btnDocGia.Text = "Độc giả và thẻ";
            this.btnDocGia.UseVisualStyleBackColor = true;

            // 
            // btnMuonTra
            // 
            this.btnMuonTra.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.btnMuonTra.Location = new System.Drawing.Point(450, 205);
            this.btnMuonTra.Name = "btnMuonTra";
            this.btnMuonTra.Size = new System.Drawing.Size(220, 45);
            this.btnMuonTra.TabIndex = 4;
            this.btnMuonTra.Text = "Mượn - Trả sách";
            this.btnMuonTra.UseVisualStyleBackColor = true;

            // 
            // btnThongKe
            // 
            this.btnThongKe.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.btnThongKe.Location = new System.Drawing.Point(130, 280);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(220, 45);
            this.btnThongKe.TabIndex = 5;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;

            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                10F
            );
            this.btnThoat.Location = new System.Drawing.Point(450, 280);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(220, 45);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;

            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);

            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnMuonTra);
            this.Controls.Add(this.btnDocGia);
            this.Controls.Add(this.btnSach);
            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.lblTitle);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "Hệ thống quản lý thư viện";

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnSach;
        private System.Windows.Forms.Button btnDocGia;
        private System.Windows.Forms.Button btnMuonTra;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;
    }
}