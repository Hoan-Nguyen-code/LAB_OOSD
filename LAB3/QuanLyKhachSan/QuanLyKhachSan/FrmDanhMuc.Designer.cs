namespace QuanLyKhachSan
{
    partial class FrmDanhMuc
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
            this.tabKhuVuc = new System.Windows.Forms.TabPage();
            this.tabNhanVien = new System.Windows.Forms.TabPage();
            this.tabLoaiTN = new System.Windows.Forms.TabPage();
            this.tabDichVu = new System.Windows.Forms.TabPage();
            this.tabQuyDinh = new System.Windows.Forms.TabPage();

            // Khu vực
            this.dgvKhu = new System.Windows.Forms.DataGridView();
            this.lblKhuMa = new System.Windows.Forms.Label();
            this.lblKhuTen = new System.Windows.Forms.Label();
            this.txtKhuMa = new System.Windows.Forms.TextBox();
            this.txtKhuTen = new System.Windows.Forms.TextBox();
            this.btnThemKhu = new System.Windows.Forms.Button();

            // Nhân viên
            this.dgvNV = new System.Windows.Forms.DataGridView();
            this.lblNVMa = new System.Windows.Forms.Label();
            this.lblNVTen = new System.Windows.Forms.Label();
            this.lblNVVaiTro = new System.Windows.Forms.Label();
            this.lblNVSDT = new System.Windows.Forms.Label();
            this.txtNVMa = new System.Windows.Forms.TextBox();
            this.txtNVTen = new System.Windows.Forms.TextBox();
            this.txtNVVaiTro = new System.Windows.Forms.TextBox();
            this.txtNVSDT = new System.Windows.Forms.TextBox();
            this.btnThemNV = new System.Windows.Forms.Button();

            // Loại tiện nghi
            this.dgvLoaiTN = new System.Windows.Forms.DataGridView();
            this.lblLoaiMa = new System.Windows.Forms.Label();
            this.lblLoaiTen = new System.Windows.Forms.Label();
            this.txtLoaiMa = new System.Windows.Forms.TextBox();
            this.txtLoaiTen = new System.Windows.Forms.TextBox();
            this.btnThemLoaiTN = new System.Windows.Forms.Button();

            // Dịch vụ
            this.dgvDV = new System.Windows.Forms.DataGridView();
            this.lblDVMa = new System.Windows.Forms.Label();
            this.lblDVTen = new System.Windows.Forms.Label();
            this.lblDVDVT = new System.Windows.Forms.Label();
            this.lblDVGia = new System.Windows.Forms.Label();
            this.txtDVMa = new System.Windows.Forms.TextBox();
            this.txtDVTen = new System.Windows.Forms.TextBox();
            this.txtDVDVT = new System.Windows.Forms.TextBox();
            this.numDVGia = new System.Windows.Forms.NumericUpDown();
            this.btnThemDV = new System.Windows.Forms.Button();

            // Quy định
            this.dgvQD = new System.Windows.Forms.DataGridView();
            this.lblQDMa = new System.Windows.Forms.Label();
            this.lblQDLoai = new System.Windows.Forms.Label();
            this.lblQDMucDo = new System.Windows.Forms.Label();
            this.lblQDTien = new System.Windows.Forms.Label();
            this.txtQDMa = new System.Windows.Forms.TextBox();
            this.cboQDLoai = new System.Windows.Forms.ComboBox();
            this.txtQDMucDo = new System.Windows.Forms.TextBox();
            this.numQDTien = new System.Windows.Forms.NumericUpDown();
            this.btnThemQD = new System.Windows.Forms.Button();

            this.tabMain.SuspendLayout();
            this.tabKhuVuc.SuspendLayout();
            this.tabNhanVien.SuspendLayout();
            this.tabLoaiTN.SuspendLayout();
            this.tabDichVu.SuspendLayout();
            this.tabQuyDinh.SuspendLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKhu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQDTien)).BeginInit();

            this.SuspendLayout();

            // ================= TAB CONTROL =================

            this.tabMain.Controls.Add(this.tabKhuVuc);
            this.tabMain.Controls.Add(this.tabNhanVien);
            this.tabMain.Controls.Add(this.tabLoaiTN);
            this.tabMain.Controls.Add(this.tabDichVu);
            this.tabMain.Controls.Add(this.tabQuyDinh);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(960, 540);

            // ================= KHU VỰC =================

            this.tabKhuVuc.Text = "Khu vực";
            this.tabKhuVuc.UseVisualStyleBackColor = true;

            this.dgvKhu.Location = new System.Drawing.Point(20, 20);
            this.dgvKhu.Name = "dgvKhu";
            this.dgvKhu.Size = new System.Drawing.Size(900, 320);
            this.dgvKhu.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhu.ReadOnly = true;
            this.dgvKhu.AllowUserToAddRows = false;

            this.lblKhuMa.AutoSize = true;
            this.lblKhuMa.Location = new System.Drawing.Point(30, 380);
            this.lblKhuMa.Text = "Mã khu vực:";

            this.txtKhuMa.Location = new System.Drawing.Point(120, 377);
            this.txtKhuMa.Size = new System.Drawing.Size(180, 20);

            this.lblKhuTen.AutoSize = true;
            this.lblKhuTen.Location = new System.Drawing.Point(330, 380);
            this.lblKhuTen.Text = "Tên khu vực:";

            this.txtKhuTen.Location = new System.Drawing.Point(420, 377);
            this.txtKhuTen.Size = new System.Drawing.Size(220, 20);

            this.btnThemKhu.Location = new System.Drawing.Point(680, 370);
            this.btnThemKhu.Size = new System.Drawing.Size(130, 35);
            this.btnThemKhu.Text = "Thêm khu vực";
            this.btnThemKhu.Name = "btnThemKhu";

            this.tabKhuVuc.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.dgvKhu, this.lblKhuMa, this.txtKhuMa,
                this.lblKhuTen, this.txtKhuTen, this.btnThemKhu
            });

            // ================= NHÂN VIÊN =================

            this.tabNhanVien.Text = "Nhân viên";
            this.tabNhanVien.UseVisualStyleBackColor = true;

            this.dgvNV.Location = new System.Drawing.Point(20, 20);
            this.dgvNV.Name = "dgvNV";
            this.dgvNV.Size = new System.Drawing.Size(900, 310);
            this.dgvNV.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNV.ReadOnly = true;
            this.dgvNV.AllowUserToAddRows = false;

            this.lblNVMa.AutoSize = true;
            this.lblNVMa.Location = new System.Drawing.Point(30, 365);
            this.lblNVMa.Text = "Mã NV:";

            this.txtNVMa.Location = new System.Drawing.Point(100, 362);
            this.txtNVMa.Size = new System.Drawing.Size(150, 20);

            this.lblNVTen.AutoSize = true;
            this.lblNVTen.Location = new System.Drawing.Point(280, 365);
            this.lblNVTen.Text = "Họ tên:";

            this.txtNVTen.Location = new System.Drawing.Point(340, 362);
            this.txtNVTen.Size = new System.Drawing.Size(220, 20);

            this.lblNVVaiTro.AutoSize = true;
            this.lblNVVaiTro.Location = new System.Drawing.Point(30, 415);
            this.lblNVVaiTro.Text = "Vai trò:";

            this.txtNVVaiTro.Location = new System.Drawing.Point(100, 412);
            this.txtNVVaiTro.Size = new System.Drawing.Size(150, 20);

            this.lblNVSDT.AutoSize = true;
            this.lblNVSDT.Location = new System.Drawing.Point(280, 415);
            this.lblNVSDT.Text = "SĐT:";

            this.txtNVSDT.Location = new System.Drawing.Point(340, 412);
            this.txtNVSDT.Size = new System.Drawing.Size(220, 20);

            this.btnThemNV.Location = new System.Drawing.Point(650, 385);
            this.btnThemNV.Size = new System.Drawing.Size(140, 40);
            this.btnThemNV.Text = "Thêm nhân viên";
            this.btnThemNV.Name = "btnThemNV";

            this.tabNhanVien.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.dgvNV,
                this.lblNVMa, this.txtNVMa,
                this.lblNVTen, this.txtNVTen,
                this.lblNVVaiTro, this.txtNVVaiTro,
                this.lblNVSDT, this.txtNVSDT,
                this.btnThemNV
            });

            // ================= LOẠI TIỆN NGHI =================

            this.tabLoaiTN.Text = "Loại tiện nghi";
            this.tabLoaiTN.UseVisualStyleBackColor = true;

            this.dgvLoaiTN.Location = new System.Drawing.Point(20, 20);
            this.dgvLoaiTN.Name = "dgvLoaiTN";
            this.dgvLoaiTN.Size = new System.Drawing.Size(900, 320);
            this.dgvLoaiTN.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoaiTN.ReadOnly = true;
            this.dgvLoaiTN.AllowUserToAddRows = false;

            this.lblLoaiMa.AutoSize = true;
            this.lblLoaiMa.Location = new System.Drawing.Point(30, 380);
            this.lblLoaiMa.Text = "Mã loại:";

            this.txtLoaiMa.Location = new System.Drawing.Point(100, 377);
            this.txtLoaiMa.Size = new System.Drawing.Size(180, 20);

            this.lblLoaiTen.AutoSize = true;
            this.lblLoaiTen.Location = new System.Drawing.Point(320, 380);
            this.lblLoaiTen.Text = "Tên loại:";

            this.txtLoaiTen.Location = new System.Drawing.Point(390, 377);
            this.txtLoaiTen.Size = new System.Drawing.Size(220, 20);

            this.btnThemLoaiTN.Location = new System.Drawing.Point(650, 370);
            this.btnThemLoaiTN.Size = new System.Drawing.Size(150, 35);
            this.btnThemLoaiTN.Text = "Thêm loại tiện nghi";
            this.btnThemLoaiTN.Name = "btnThemLoaiTN";

            this.tabLoaiTN.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.dgvLoaiTN,
                this.lblLoaiMa, this.txtLoaiMa,
                this.lblLoaiTen, this.txtLoaiTen,
                this.btnThemLoaiTN
            });

            // ================= DỊCH VỤ =================

            this.tabDichVu.Text = "Dịch vụ";
            this.tabDichVu.UseVisualStyleBackColor = true;

            this.dgvDV.Location = new System.Drawing.Point(20, 20);
            this.dgvDV.Name = "dgvDV";
            this.dgvDV.Size = new System.Drawing.Size(900, 310);
            this.dgvDV.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDV.ReadOnly = true;
            this.dgvDV.AllowUserToAddRows = false;

            this.lblDVMa.AutoSize = true;
            this.lblDVMa.Location = new System.Drawing.Point(30, 365);
            this.lblDVMa.Text = "Mã DV:";

            this.txtDVMa.Location = new System.Drawing.Point(100, 362);
            this.txtDVMa.Size = new System.Drawing.Size(150, 20);

            this.lblDVTen.AutoSize = true;
            this.lblDVTen.Location = new System.Drawing.Point(280, 365);
            this.lblDVTen.Text = "Tên DV:";

            this.txtDVTen.Location = new System.Drawing.Point(350, 362);
            this.txtDVTen.Size = new System.Drawing.Size(220, 20);

            this.lblDVDVT.AutoSize = true;
            this.lblDVDVT.Location = new System.Drawing.Point(30, 415);
            this.lblDVDVT.Text = "Đơn vị tính:";

            this.txtDVDVT.Location = new System.Drawing.Point(100, 412);
            this.txtDVDVT.Size = new System.Drawing.Size(150, 20);

            this.lblDVGia.AutoSize = true;
            this.lblDVGia.Location = new System.Drawing.Point(280, 415);
            this.lblDVGia.Text = "Đơn giá:";

            this.numDVGia.Location = new System.Drawing.Point(350, 412);
            this.numDVGia.Size = new System.Drawing.Size(220, 20);
            this.numDVGia.Maximum = 1000000000;
            this.numDVGia.ThousandsSeparator = true;

            this.btnThemDV.Location = new System.Drawing.Point(650, 385);
            this.btnThemDV.Size = new System.Drawing.Size(140, 40);
            this.btnThemDV.Text = "Thêm dịch vụ";
            this.btnThemDV.Name = "btnThemDV";

            this.tabDichVu.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.dgvDV,
                this.lblDVMa, this.txtDVMa,
                this.lblDVTen, this.txtDVTen,
                this.lblDVDVT, this.txtDVDVT,
                this.lblDVGia, this.numDVGia,
                this.btnThemDV
            });

            // ================= QUY ĐỊNH ĐỀN BÙ =================

            this.tabQuyDinh.Text = "Quy định đền bù";
            this.tabQuyDinh.UseVisualStyleBackColor = true;

            this.dgvQD.Location = new System.Drawing.Point(20, 20);
            this.dgvQD.Name = "dgvQD";
            this.dgvQD.Size = new System.Drawing.Size(900, 310);
            this.dgvQD.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQD.ReadOnly = true;
            this.dgvQD.AllowUserToAddRows = false;

            this.lblQDMa.AutoSize = true;
            this.lblQDMa.Location = new System.Drawing.Point(30, 365);
            this.lblQDMa.Text = "Mã quy định:";

            this.txtQDMa.Location = new System.Drawing.Point(120, 362);
            this.txtQDMa.Size = new System.Drawing.Size(150, 20);

            this.lblQDLoai.AutoSize = true;
            this.lblQDLoai.Location = new System.Drawing.Point(300, 365);
            this.lblQDLoai.Text = "Loại tiện nghi:";

            this.cboQDLoai.Location = new System.Drawing.Point(390, 362);
            this.cboQDLoai.Size = new System.Drawing.Size(200, 21);
            this.cboQDLoai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblQDMucDo.AutoSize = true;
            this.lblQDMucDo.Location = new System.Drawing.Point(30, 415);
            this.lblQDMucDo.Text = "Mức độ:";

            this.txtQDMucDo.Location = new System.Drawing.Point(120, 412);
            this.txtQDMucDo.Size = new System.Drawing.Size(150, 20);

            this.lblQDTien.AutoSize = true;
            this.lblQDTien.Location = new System.Drawing.Point(300, 415);
            this.lblQDTien.Text = "Mức đền bù:";

            this.numQDTien.Location = new System.Drawing.Point(390, 412);
            this.numQDTien.Size = new System.Drawing.Size(200, 20);
            this.numQDTien.Maximum = 1000000000;
            this.numQDTien.ThousandsSeparator = true;

            this.btnThemQD.Location = new System.Drawing.Point(650, 385);
            this.btnThemQD.Size = new System.Drawing.Size(150, 40);
            this.btnThemQD.Text = "Thêm quy định";
            this.btnThemQD.Name = "btnThemQD";

            this.tabQuyDinh.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.dgvQD,
                this.lblQDMa, this.txtQDMa,
                this.lblQDLoai, this.cboQDLoai,
                this.lblQDMucDo, this.txtQDMucDo,
                this.lblQDTien, this.numQDTien,
                this.btnThemQD
            });

            // ================= FORM =================

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabMain);
            this.Name = "FrmDanhMuc";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý danh mục";

            this.tabMain.ResumeLayout(false);
            this.tabKhuVuc.ResumeLayout(false);
            this.tabKhuVuc.PerformLayout();
            this.tabNhanVien.ResumeLayout(false);
            this.tabNhanVien.PerformLayout();
            this.tabLoaiTN.ResumeLayout(false);
            this.tabLoaiTN.PerformLayout();
            this.tabDichVu.ResumeLayout(false);
            this.tabDichVu.PerformLayout();
            this.tabQuyDinh.ResumeLayout(false);
            this.tabQuyDinh.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKhu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQDTien)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabKhuVuc;
        private System.Windows.Forms.TabPage tabNhanVien;
        private System.Windows.Forms.TabPage tabLoaiTN;
        private System.Windows.Forms.TabPage tabDichVu;
        private System.Windows.Forms.TabPage tabQuyDinh;

        private System.Windows.Forms.DataGridView dgvKhu;
        private System.Windows.Forms.Label lblKhuMa;
        private System.Windows.Forms.Label lblKhuTen;
        private System.Windows.Forms.TextBox txtKhuMa;
        private System.Windows.Forms.TextBox txtKhuTen;
        private System.Windows.Forms.Button btnThemKhu;

        private System.Windows.Forms.DataGridView dgvNV;
        private System.Windows.Forms.Label lblNVMa;
        private System.Windows.Forms.Label lblNVTen;
        private System.Windows.Forms.Label lblNVVaiTro;
        private System.Windows.Forms.Label lblNVSDT;
        private System.Windows.Forms.TextBox txtNVMa;
        private System.Windows.Forms.TextBox txtNVTen;
        private System.Windows.Forms.TextBox txtNVVaiTro;
        private System.Windows.Forms.TextBox txtNVSDT;
        private System.Windows.Forms.Button btnThemNV;

        private System.Windows.Forms.DataGridView dgvLoaiTN;
        private System.Windows.Forms.Label lblLoaiMa;
        private System.Windows.Forms.Label lblLoaiTen;
        private System.Windows.Forms.TextBox txtLoaiMa;
        private System.Windows.Forms.TextBox txtLoaiTen;
        private System.Windows.Forms.Button btnThemLoaiTN;

        private System.Windows.Forms.DataGridView dgvDV;
        private System.Windows.Forms.Label lblDVMa;
        private System.Windows.Forms.Label lblDVTen;
        private System.Windows.Forms.Label lblDVDVT;
        private System.Windows.Forms.Label lblDVGia;
        private System.Windows.Forms.TextBox txtDVMa;
        private System.Windows.Forms.TextBox txtDVTen;
        private System.Windows.Forms.TextBox txtDVDVT;
        private System.Windows.Forms.NumericUpDown numDVGia;
        private System.Windows.Forms.Button btnThemDV;

        private System.Windows.Forms.DataGridView dgvQD;
        private System.Windows.Forms.Label lblQDMa;
        private System.Windows.Forms.Label lblQDLoai;
        private System.Windows.Forms.Label lblQDMucDo;
        private System.Windows.Forms.Label lblQDTien;
        private System.Windows.Forms.TextBox txtQDMa;
        private System.Windows.Forms.ComboBox cboQDLoai;
        private System.Windows.Forms.TextBox txtQDMucDo;
        private System.Windows.Forms.NumericUpDown numQDTien;
        private System.Windows.Forms.Button btnThemQD;
    }
}