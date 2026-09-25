namespace QuanLyKhachSan
{
    partial class FrmTraPhong
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
            this.grpPhong = new System.Windows.Forms.GroupBox();
            this.lblDat = new System.Windows.Forms.Label();
            this.cboDat = new System.Windows.Forms.ComboBox();
            this.lblPhongChon = new System.Windows.Forms.Label();
            this.txtPhong = new System.Windows.Forms.TextBox();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.dgvTN = new System.Windows.Forms.DataGridView();
            this.grpDenBu = new System.Windows.Forms.GroupBox();
            this.lblSoDB = new System.Windows.Forms.Label();
            this.txtSoDB = new System.Windows.Forms.TextBox();
            this.lblNV = new System.Windows.Forms.Label();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.lblMucDo = new System.Windows.Forms.Label();
            this.txtMucDo = new System.Windows.Forms.TextBox();
            this.lblTienDB = new System.Windows.Forms.Label();
            this.numDenBu = new System.Windows.Forms.NumericUpDown();
            this.btnThemDB = new System.Windows.Forms.Button();
            this.btnLapDB = new System.Windows.Forms.Button();
            this.dgvDBChon = new System.Windows.Forms.DataGridView();
            this.grpHoaDon = new System.Windows.Forms.GroupBox();
            this.lblSoHD = new System.Windows.Forms.Label();
            this.txtSoHD = new System.Windows.Forms.TextBox();
            this.lblSoNgay = new System.Windows.Forms.Label();
            this.numSoNgay = new System.Windows.Forms.NumericUpDown();
            this.lblNV2 = new System.Windows.Forms.Label();
            this.cboNV2 = new System.Windows.Forms.ComboBox();
            this.btnLapHD = new System.Windows.Forms.Button();
            this.dgvHD = new System.Windows.Forms.DataGridView();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.lblHDChon = new System.Windows.Forms.Label();
            this.txtHDChon = new System.Windows.Forms.TextBox();
            this.lblMaTT = new System.Windows.Forms.Label();
            this.txtMaTT = new System.Windows.Forms.TextBox();
            this.lblHT = new System.Windows.Forms.Label();
            this.cboHT = new System.Windows.Forms.ComboBox();
            this.lblTienTT = new System.Windows.Forms.Label();
            this.numTienTT = new System.Windows.Forms.NumericUpDown();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnTraPhong = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).BeginInit();
            this.grpDenBu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDenBu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDBChon)).BeginInit();
            this.grpHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).BeginInit();
            this.grpThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTienTT)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(560, 18);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(399, 31);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TRẢ PHÒNG - THANH TOÁN";
            // 
            // grpPhong
            // 
            this.grpPhong.Controls.Add(this.lblDat);
            this.grpPhong.Controls.Add(this.cboDat);
            this.grpPhong.Controls.Add(this.lblPhongChon);
            this.grpPhong.Controls.Add(this.txtPhong);
            this.grpPhong.Controls.Add(this.dgvPhong);
            this.grpPhong.Controls.Add(this.dgvTN);
            this.grpPhong.Location = new System.Drawing.Point(27, 68);
            this.grpPhong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPhong.Name = "grpPhong";
            this.grpPhong.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPhong.Size = new System.Drawing.Size(1533, 289);
            this.grpPhong.TabIndex = 1;
            this.grpPhong.TabStop = false;
            this.grpPhong.Text = "1. Phiếu lưu trú - Phòng - Tiện nghi";
            // 
            // lblDat
            // 
            this.lblDat.AutoSize = true;
            this.lblDat.Location = new System.Drawing.Point(27, 37);
            this.lblDat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDat.Name = "lblDat";
            this.lblDat.Size = new System.Drawing.Size(89, 16);
            this.lblDat.TabIndex = 0;
            this.lblDat.Text = "Phiếu đang ở:";
            // 
            // cboDat
            // 
            this.cboDat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDat.Location = new System.Drawing.Point(153, 33);
            this.cboDat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboDat.Name = "cboDat";
            this.cboDat.Size = new System.Drawing.Size(239, 24);
            this.cboDat.TabIndex = 1;
            // 
            // lblPhongChon
            // 
            this.lblPhongChon.AutoSize = true;
            this.lblPhongChon.Location = new System.Drawing.Point(433, 37);
            this.lblPhongChon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhongChon.Name = "lblPhongChon";
            this.lblPhongChon.Size = new System.Drawing.Size(81, 16);
            this.lblPhongChon.TabIndex = 2;
            this.lblPhongChon.Text = "Phòng chọn:";
            // 
            // txtPhong
            // 
            this.txtPhong.Location = new System.Drawing.Point(540, 33);
            this.txtPhong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPhong.Name = "txtPhong";
            this.txtPhong.ReadOnly = true;
            this.txtPhong.Size = new System.Drawing.Size(159, 22);
            this.txtPhong.TabIndex = 3;
            // 
            // dgvPhong
            // 
            this.dgvPhong.AllowUserToAddRows = false;
            this.dgvPhong.AllowUserToDeleteRows = false;
            this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhong.ColumnHeadersHeight = 29;
            this.dgvPhong.Location = new System.Drawing.Point(27, 80);
            this.dgvPhong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.ReadOnly = true;
            this.dgvPhong.RowHeadersWidth = 51;
            this.dgvPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhong.Size = new System.Drawing.Size(573, 185);
            this.dgvPhong.TabIndex = 4;
            // 
            // dgvTN
            // 
            this.dgvTN.AllowUserToAddRows = false;
            this.dgvTN.AllowUserToDeleteRows = false;
            this.dgvTN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTN.ColumnHeadersHeight = 29;
            this.dgvTN.Location = new System.Drawing.Point(627, 80);
            this.dgvTN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvTN.Name = "dgvTN";
            this.dgvTN.ReadOnly = true;
            this.dgvTN.RowHeadersWidth = 51;
            this.dgvTN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTN.Size = new System.Drawing.Size(880, 185);
            this.dgvTN.TabIndex = 5;
            // 
            // grpDenBu
            // 
            this.grpDenBu.Controls.Add(this.lblSoDB);
            this.grpDenBu.Controls.Add(this.txtSoDB);
            this.grpDenBu.Controls.Add(this.lblNV);
            this.grpDenBu.Controls.Add(this.cboNV);
            this.grpDenBu.Controls.Add(this.lblMucDo);
            this.grpDenBu.Controls.Add(this.txtMucDo);
            this.grpDenBu.Controls.Add(this.lblTienDB);
            this.grpDenBu.Controls.Add(this.numDenBu);
            this.grpDenBu.Controls.Add(this.btnThemDB);
            this.grpDenBu.Controls.Add(this.btnLapDB);
            this.grpDenBu.Controls.Add(this.dgvDBChon);
            this.grpDenBu.Location = new System.Drawing.Point(27, 369);
            this.grpDenBu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDenBu.Name = "grpDenBu";
            this.grpDenBu.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDenBu.Size = new System.Drawing.Size(1533, 234);
            this.grpDenBu.TabIndex = 2;
            this.grpDenBu.TabStop = false;
            this.grpDenBu.Text = "2. Đền bù tiện nghi";
            // 
            // lblSoDB
            // 
            this.lblSoDB.AutoSize = true;
            this.lblSoDB.Location = new System.Drawing.Point(27, 37);
            this.lblSoDB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoDB.Name = "lblSoDB";
            this.lblSoDB.Size = new System.Drawing.Size(63, 16);
            this.lblSoDB.TabIndex = 0;
            this.lblSoDB.Text = "Số phiếu:";
            // 
            // txtSoDB
            // 
            this.txtSoDB.Location = new System.Drawing.Point(107, 33);
            this.txtSoDB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSoDB.Name = "txtSoDB";
            this.txtSoDB.Size = new System.Drawing.Size(159, 22);
            this.txtSoDB.TabIndex = 1;
            // 
            // lblNV
            // 
            this.lblNV.AutoSize = true;
            this.lblNV.Location = new System.Drawing.Point(293, 37);
            this.lblNV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNV.Name = "lblNV";
            this.lblNV.Size = new System.Drawing.Size(70, 16);
            this.lblNV.TabIndex = 2;
            this.lblNV.Text = "Nhân viên:";
            // 
            // cboNV
            // 
            this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV.Location = new System.Drawing.Point(380, 33);
            this.cboNV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboNV.Name = "cboNV";
            this.cboNV.Size = new System.Drawing.Size(225, 24);
            this.cboNV.TabIndex = 3;
            // 
            // lblMucDo
            // 
            this.lblMucDo.AutoSize = true;
            this.lblMucDo.Location = new System.Drawing.Point(27, 86);
            this.lblMucDo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMucDo.Name = "lblMucDo";
            this.lblMucDo.Size = new System.Drawing.Size(54, 16);
            this.lblMucDo.TabIndex = 4;
            this.lblMucDo.Text = "Mức độ:";
            // 
            // txtMucDo
            // 
            this.txtMucDo.Location = new System.Drawing.Point(107, 82);
            this.txtMucDo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMucDo.Name = "txtMucDo";
            this.txtMucDo.Size = new System.Drawing.Size(199, 22);
            this.txtMucDo.TabIndex = 5;
            // 
            // lblTienDB
            // 
            this.lblTienDB.AutoSize = true;
            this.lblTienDB.Location = new System.Drawing.Point(333, 86);
            this.lblTienDB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTienDB.Name = "lblTienDB";
            this.lblTienDB.Size = new System.Drawing.Size(51, 16);
            this.lblTienDB.TabIndex = 6;
            this.lblTienDB.Text = "Số tiền:";
            // 
            // numDenBu
            // 
            this.numDenBu.Location = new System.Drawing.Point(407, 82);
            this.numDenBu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numDenBu.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numDenBu.Name = "numDenBu";
            this.numDenBu.Size = new System.Drawing.Size(200, 22);
            this.numDenBu.TabIndex = 7;
            this.numDenBu.ThousandsSeparator = true;
            // 
            // btnThemDB
            // 
            this.btnThemDB.Location = new System.Drawing.Point(107, 135);
            this.btnThemDB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThemDB.Name = "btnThemDB";
            this.btnThemDB.Size = new System.Drawing.Size(200, 43);
            this.btnThemDB.TabIndex = 8;
            this.btnThemDB.Text = "Thêm đền bù";
            this.btnThemDB.UseVisualStyleBackColor = true;
            // 
            // btnLapDB
            // 
            this.btnLapDB.Location = new System.Drawing.Point(333, 135);
            this.btnLapDB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLapDB.Name = "btnLapDB";
            this.btnLapDB.Size = new System.Drawing.Size(200, 43);
            this.btnLapDB.TabIndex = 9;
            this.btnLapDB.Text = "Lập phiếu đền bù";
            this.btnLapDB.UseVisualStyleBackColor = true;
            // 
            // dgvDBChon
            // 
            this.dgvDBChon.AllowUserToAddRows = false;
            this.dgvDBChon.AllowUserToDeleteRows = false;
            this.dgvDBChon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDBChon.ColumnHeadersHeight = 29;
            this.dgvDBChon.Location = new System.Drawing.Point(640, 31);
            this.dgvDBChon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDBChon.Name = "dgvDBChon";
            this.dgvDBChon.ReadOnly = true;
            this.dgvDBChon.RowHeadersWidth = 51;
            this.dgvDBChon.Size = new System.Drawing.Size(867, 178);
            this.dgvDBChon.TabIndex = 10;
            // 
            // grpHoaDon
            // 
            this.grpHoaDon.Controls.Add(this.lblSoHD);
            this.grpHoaDon.Controls.Add(this.txtSoHD);
            this.grpHoaDon.Controls.Add(this.lblSoNgay);
            this.grpHoaDon.Controls.Add(this.numSoNgay);
            this.grpHoaDon.Controls.Add(this.lblNV2);
            this.grpHoaDon.Controls.Add(this.cboNV2);
            this.grpHoaDon.Controls.Add(this.btnLapHD);
            this.grpHoaDon.Controls.Add(this.dgvHD);
            this.grpHoaDon.Location = new System.Drawing.Point(27, 615);
            this.grpHoaDon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpHoaDon.Name = "grpHoaDon";
            this.grpHoaDon.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpHoaDon.Size = new System.Drawing.Size(1533, 234);
            this.grpHoaDon.TabIndex = 3;
            this.grpHoaDon.TabStop = false;
            this.grpHoaDon.Text = "3. Hóa đơn";
            // 
            // lblSoHD
            // 
            this.lblSoHD.AutoSize = true;
            this.lblSoHD.Location = new System.Drawing.Point(27, 37);
            this.lblSoHD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoHD.Name = "lblSoHD";
            this.lblSoHD.Size = new System.Drawing.Size(79, 16);
            this.lblSoHD.TabIndex = 0;
            this.lblSoHD.Text = "Số hóa đơn:";
            // 
            // txtSoHD
            // 
            this.txtSoHD.Location = new System.Drawing.Point(133, 33);
            this.txtSoHD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.Size = new System.Drawing.Size(172, 22);
            this.txtSoHD.TabIndex = 1;
            // 
            // lblSoNgay
            // 
            this.lblSoNgay.AutoSize = true;
            this.lblSoNgay.Location = new System.Drawing.Point(27, 86);
            this.lblSoNgay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoNgay.Name = "lblSoNgay";
            this.lblSoNgay.Size = new System.Drawing.Size(60, 16);
            this.lblSoNgay.TabIndex = 2;
            this.lblSoNgay.Text = "Số ngày:";
            // 
            // numSoNgay
            // 
            this.numSoNgay.Location = new System.Drawing.Point(133, 82);
            this.numSoNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numSoNgay.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numSoNgay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoNgay.Name = "numSoNgay";
            this.numSoNgay.Size = new System.Drawing.Size(173, 22);
            this.numSoNgay.TabIndex = 3;
            this.numSoNgay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblNV2
            // 
            this.lblNV2.AutoSize = true;
            this.lblNV2.Location = new System.Drawing.Point(27, 135);
            this.lblNV2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNV2.Name = "lblNV2";
            this.lblNV2.Size = new System.Drawing.Size(70, 16);
            this.lblNV2.TabIndex = 4;
            this.lblNV2.Text = "Nhân viên:";
            // 
            // cboNV2
            // 
            this.cboNV2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNV2.Location = new System.Drawing.Point(133, 132);
            this.cboNV2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboNV2.Name = "cboNV2";
            this.cboNV2.Size = new System.Drawing.Size(239, 24);
            this.cboNV2.TabIndex = 5;
            // 
            // btnLapHD
            // 
            this.btnLapHD.Location = new System.Drawing.Point(407, 68);
            this.btnLapHD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLapHD.Name = "btnLapHD";
            this.btnLapHD.Size = new System.Drawing.Size(187, 55);
            this.btnLapHD.TabIndex = 6;
            this.btnLapHD.Text = "Lập hóa đơn";
            this.btnLapHD.UseVisualStyleBackColor = true;
            // 
            // dgvHD
            // 
            this.dgvHD.AllowUserToAddRows = false;
            this.dgvHD.AllowUserToDeleteRows = false;
            this.dgvHD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHD.ColumnHeadersHeight = 29;
            this.dgvHD.Location = new System.Drawing.Point(640, 31);
            this.dgvHD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvHD.Name = "dgvHD";
            this.dgvHD.ReadOnly = true;
            this.dgvHD.RowHeadersWidth = 51;
            this.dgvHD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHD.Size = new System.Drawing.Size(867, 178);
            this.dgvHD.TabIndex = 7;
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Controls.Add(this.lblHDChon);
            this.grpThanhToan.Controls.Add(this.txtHDChon);
            this.grpThanhToan.Controls.Add(this.lblMaTT);
            this.grpThanhToan.Controls.Add(this.txtMaTT);
            this.grpThanhToan.Controls.Add(this.lblHT);
            this.grpThanhToan.Controls.Add(this.cboHT);
            this.grpThanhToan.Controls.Add(this.lblTienTT);
            this.grpThanhToan.Controls.Add(this.numTienTT);
            this.grpThanhToan.Controls.Add(this.btnThanhToan);
            this.grpThanhToan.Controls.Add(this.btnTraPhong);
            this.grpThanhToan.Controls.Add(this.btnDong);
            this.grpThanhToan.Location = new System.Drawing.Point(27, 862);
            this.grpThanhToan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpThanhToan.Size = new System.Drawing.Size(1533, 166);
            this.grpThanhToan.TabIndex = 4;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "4. Thanh toán - Hoàn tất trả phòng";
            // 
            // lblHDChon
            // 
            this.lblHDChon.AutoSize = true;
            this.lblHDChon.Location = new System.Drawing.Point(27, 37);
            this.lblHDChon.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHDChon.Name = "lblHDChon";
            this.lblHDChon.Size = new System.Drawing.Size(61, 16);
            this.lblHDChon.TabIndex = 0;
            this.lblHDChon.Text = "HĐ chọn:";
            // 
            // txtHDChon
            // 
            this.txtHDChon.Location = new System.Drawing.Point(107, 33);
            this.txtHDChon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtHDChon.Name = "txtHDChon";
            this.txtHDChon.ReadOnly = true;
            this.txtHDChon.Size = new System.Drawing.Size(159, 22);
            this.txtHDChon.TabIndex = 1;
            // 
            // lblMaTT
            // 
            this.lblMaTT.AutoSize = true;
            this.lblMaTT.Location = new System.Drawing.Point(293, 37);
            this.lblMaTT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaTT.Name = "lblMaTT";
            this.lblMaTT.Size = new System.Drawing.Size(50, 16);
            this.lblMaTT.TabIndex = 2;
            this.lblMaTT.Text = "Mã TT:";
            // 
            // txtMaTT
            // 
            this.txtMaTT.Location = new System.Drawing.Point(360, 33);
            this.txtMaTT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMaTT.Name = "txtMaTT";
            this.txtMaTT.Size = new System.Drawing.Size(159, 22);
            this.txtMaTT.TabIndex = 3;
            // 
            // lblHT
            // 
            this.lblHT.AutoSize = true;
            this.lblHT.Location = new System.Drawing.Point(547, 37);
            this.lblHT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHT.Name = "lblHT";
            this.lblHT.Size = new System.Drawing.Size(64, 16);
            this.lblHT.TabIndex = 4;
            this.lblHT.Text = "Hình thức:";
            // 
            // cboHT
            // 
            this.cboHT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHT.Location = new System.Drawing.Point(640, 33);
            this.cboHT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboHT.Name = "cboHT";
            this.cboHT.Size = new System.Drawing.Size(199, 24);
            this.cboHT.TabIndex = 5;
            // 
            // lblTienTT
            // 
            this.lblTienTT.AutoSize = true;
            this.lblTienTT.Location = new System.Drawing.Point(867, 37);
            this.lblTienTT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTienTT.Name = "lblTienTT";
            this.lblTienTT.Size = new System.Drawing.Size(51, 16);
            this.lblTienTT.TabIndex = 6;
            this.lblTienTT.Text = "Số tiền:";
            // 
            // numTienTT
            // 
            this.numTienTT.Location = new System.Drawing.Point(940, 33);
            this.numTienTT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numTienTT.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numTienTT.Name = "numTienTT";
            this.numTienTT.Size = new System.Drawing.Size(200, 22);
            this.numTienTT.TabIndex = 7;
            this.numTienTT.ThousandsSeparator = true;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(267, 86);
            this.btnThanhToan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(213, 49);
            this.btnThanhToan.TabIndex = 8;
            this.btnThanhToan.Text = "Ghi thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            // 
            // btnTraPhong
            // 
            this.btnTraPhong.Location = new System.Drawing.Point(520, 86);
            this.btnTraPhong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTraPhong.Name = "btnTraPhong";
            this.btnTraPhong.Size = new System.Drawing.Size(240, 49);
            this.btnTraPhong.TabIndex = 9;
            this.btnTraPhong.Text = "Hoàn tất trả phòng";
            this.btnTraPhong.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(800, 86);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(160, 49);
            this.btnDong.TabIndex = 10;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            // 
            // FrmTraPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1200, 880);
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.grpPhong);
            this.Controls.Add(this.grpDenBu);
            this.Controls.Add(this.grpHoaDon);
            this.Controls.Add(this.grpThanhToan);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmTraPhong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trả phòng - Thanh toán";
            this.grpPhong.ResumeLayout(false);
            this.grpPhong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).EndInit();
            this.grpDenBu.ResumeLayout(false);
            this.grpDenBu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDenBu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDBChon)).EndInit();
            this.grpHoaDon.ResumeLayout(false);
            this.grpHoaDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).EndInit();
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTienTT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTieuDe;

        private System.Windows.Forms.GroupBox grpPhong;
        private System.Windows.Forms.Label lblDat;
        private System.Windows.Forms.ComboBox cboDat;
        private System.Windows.Forms.Label lblPhongChon;
        private System.Windows.Forms.TextBox txtPhong;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.DataGridView dgvTN;

        private System.Windows.Forms.GroupBox grpDenBu;
        private System.Windows.Forms.Label lblSoDB;
        private System.Windows.Forms.TextBox txtSoDB;
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Label lblMucDo;
        private System.Windows.Forms.TextBox txtMucDo;
        private System.Windows.Forms.Label lblTienDB;
        private System.Windows.Forms.NumericUpDown numDenBu;
        private System.Windows.Forms.Button btnThemDB;
        private System.Windows.Forms.Button btnLapDB;
        private System.Windows.Forms.DataGridView dgvDBChon;

        private System.Windows.Forms.GroupBox grpHoaDon;
        private System.Windows.Forms.Label lblSoHD;
        private System.Windows.Forms.TextBox txtSoHD;
        private System.Windows.Forms.Label lblSoNgay;
        private System.Windows.Forms.NumericUpDown numSoNgay;
        private System.Windows.Forms.Label lblNV2;
        private System.Windows.Forms.ComboBox cboNV2;
        private System.Windows.Forms.Button btnLapHD;
        private System.Windows.Forms.DataGridView dgvHD;

        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.Label lblHDChon;
        private System.Windows.Forms.TextBox txtHDChon;
        private System.Windows.Forms.Label lblMaTT;
        private System.Windows.Forms.TextBox txtMaTT;
        private System.Windows.Forms.Label lblHT;
        private System.Windows.Forms.ComboBox cboHT;
        private System.Windows.Forms.Label lblTienTT;
        private System.Windows.Forms.NumericUpDown numTienTT;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnTraPhong;
        private System.Windows.Forms.Button btnDong;
    }
}