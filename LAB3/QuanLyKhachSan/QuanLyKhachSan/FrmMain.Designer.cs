namespace QuanLyKhachSan
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnPhongTienNghi = new System.Windows.Forms.Button();
            this.btnDatPhong = new System.Windows.Forms.Button();
            this.btnDichVu = new System.Windows.Forms.Button();
            this.btnTraPhong = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblTieuDe
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                18F,
                System.Drawing.FontStyle.Bold
            );
            this.lblTieuDe.Location = new System.Drawing.Point(185, 35);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(380, 29);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN";

            // btnDanhMuc
            this.btnDanhMuc.Location = new System.Drawing.Point(80, 110);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(180, 60);
            this.btnDanhMuc.TabIndex = 1;
            this.btnDanhMuc.Text = "Quản lý danh mục";
            this.btnDanhMuc.UseVisualStyleBackColor = true;
            this.btnDanhMuc.Click +=
                new System.EventHandler(this.btnDanhMuc_Click);

            // btnPhongTienNghi
            this.btnPhongTienNghi.Location = new System.Drawing.Point(290, 110);
            this.btnPhongTienNghi.Name = "btnPhongTienNghi";
            this.btnPhongTienNghi.Size = new System.Drawing.Size(180, 60);
            this.btnPhongTienNghi.TabIndex = 2;
            this.btnPhongTienNghi.Text = "Phòng - Tiện nghi";
            this.btnPhongTienNghi.UseVisualStyleBackColor = true;
            this.btnPhongTienNghi.Click +=
                new System.EventHandler(this.btnPhongTienNghi_Click);

            // btnDatPhong
            this.btnDatPhong.Location = new System.Drawing.Point(500, 110);
            this.btnDatPhong.Name = "btnDatPhong";
            this.btnDatPhong.Size = new System.Drawing.Size(180, 60);
            this.btnDatPhong.TabIndex = 3;
            this.btnDatPhong.Text = "Đặt / Nhận phòng";
            this.btnDatPhong.UseVisualStyleBackColor = true;
            this.btnDatPhong.Click +=
    new System.EventHandler(this.btnDatPhong_Click);

            // btnDichVu
            this.btnDichVu.Location = new System.Drawing.Point(80, 200);
            this.btnDichVu.Name = "btnDichVu";
            this.btnDichVu.Size = new System.Drawing.Size(180, 60);
            this.btnDichVu.TabIndex = 4;
            this.btnDichVu.Text = "Dịch vụ";
            this.btnDichVu.UseVisualStyleBackColor = true;
            this.btnDichVu.Click +=
    new System.EventHandler(this.btnDichVu_Click);

            // btnTraPhong
            this.btnTraPhong.Location = new System.Drawing.Point(290, 200);
            this.btnTraPhong.Name = "btnTraPhong";
            this.btnTraPhong.Size = new System.Drawing.Size(180, 60);
            this.btnTraPhong.TabIndex = 5;
            this.btnTraPhong.Text = "Trả phòng - Thanh toán";
            this.btnTraPhong.UseVisualStyleBackColor = true;
            this.btnTraPhong.Click +=
    new System.EventHandler(this.btnTraPhong_Click);

            // btnThongKe
            this.btnThongKe.Location = new System.Drawing.Point(500, 200);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(180, 60);
            this.btnThongKe.TabIndex = 6;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;

            // btnThoat
            this.btnThoat.Location = new System.Drawing.Point(290, 300);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(180, 50);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;

            // FrmMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 400);

            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnTraPhong);
            this.Controls.Add(this.btnDichVu);
            this.Controls.Add(this.btnDatPhong);
            this.Controls.Add(this.btnPhongTienNghi);
            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.lblTieuDe);

            this.Name = "FrmMain";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý khách sạn";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnPhongTienNghi;
        private System.Windows.Forms.Button btnDatPhong;
        private System.Windows.Forms.Button btnDichVu;
        private System.Windows.Forms.Button btnTraPhong;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;
    }
}