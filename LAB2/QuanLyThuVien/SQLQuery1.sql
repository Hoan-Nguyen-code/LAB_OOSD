IF DB_ID(N'QuanLyThuVienDB') IS NULL
    CREATE DATABASE QuanLyThuVienDB;
GO

USE QuanLyThuVienDB;
GO

-- =============================================
-- 1. BẢNG NHÂN VIÊN
-- =============================================

CREATE TABLE NHANVIEN
(
    MaNhanVien VARCHAR(20) PRIMARY KEY,
    Ho NVARCHAR(50),
    Ten NVARCHAR(50),
    Phai NVARCHAR(10),
    NgaySinh DATE,
    ChucVu NVARCHAR(50),
    SoDienThoai VARCHAR(15)
);
GO


-- =============================================
-- 2. BẢNG ĐỘC GIẢ
-- =============================================

CREATE TABLE DOCGIA
(
    MaDocGia VARCHAR(20) PRIMARY KEY,
    Ho NVARCHAR(50),
    Ten NVARCHAR(50),
    NgaySinh DATE,
    Phai NVARCHAR(10),
    SoDienThoai VARCHAR(15),
    DiaChi NVARCHAR(200),
    Email VARCHAR(100),
    Anh3x4 VARCHAR(255)
);
GO


-- =============================================
-- 3. BẢNG THẺ ĐỘC GIẢ
-- =============================================

CREATE TABLE THEDOCGIA
(
    MaThe VARCHAR(20) PRIMARY KEY,
    MaDocGia VARCHAR(20),
    NgayCap DATE,
    HanSuDung DATE,
    DaDongLePhi BIT,
    TrangThai NVARCHAR(50),

    CONSTRAINT FK_TheDocGia_DocGia
        FOREIGN KEY (MaDocGia)
        REFERENCES DOCGIA(MaDocGia)
);
GO


-- =============================================
-- 4. BẢNG NHÀ XUẤT BẢN
-- =============================================

CREATE TABLE NHAXUATBAN
(
    MaNhaXuatBan VARCHAR(20) PRIMARY KEY,
    DiaChi NVARCHAR(200),
    SoDienThoai VARCHAR(15)
);
GO


-- =============================================
-- 5. BẢNG THỂ LOẠI
-- =============================================

CREATE TABLE THELOAI
(
    MaTheLoai VARCHAR(20) PRIMARY KEY,
    TenTheLoai NVARCHAR(100)
);
GO


-- =============================================
-- 6. BẢNG ĐẦU SÁCH
-- =============================================

CREATE TABLE DAUSACH
(
    MaDauSach VARCHAR(20) PRIMARY KEY,
    TenSach NVARCHAR(200),
    NamXuatBan INT,
    SoLuongHienCo INT,
    MaTheLoai VARCHAR(20),
    MaNhaXuatBan VARCHAR(20),

    CONSTRAINT FK_DauSach_TheLoai
        FOREIGN KEY (MaTheLoai)
        REFERENCES THELOAI(MaTheLoai),

    CONSTRAINT FK_DauSach_NXB
        FOREIGN KEY (MaNhaXuatBan)
        REFERENCES NHAXUATBAN(MaNhaXuatBan)
);
GO


-- =============================================
-- 7. BẢNG PHIẾU MƯỢN
-- =============================================

CREATE TABLE PHIEUMUON
(
    MaPhieuMuon VARCHAR(20) PRIMARY KEY,
    MaDocGia VARCHAR(20),
    MaNhanVien VARCHAR(20),
    NgayMuon DATE,
    NgayHenTra DATE,

    CONSTRAINT FK_PhieuMuon_DocGia
        FOREIGN KEY (MaDocGia)
        REFERENCES DOCGIA(MaDocGia),

    CONSTRAINT FK_PhieuMuon_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES NHANVIEN(MaNhanVien)
);
GO


-- =============================================
-- 8. BẢNG CHI TIẾT PHIẾU MƯỢN
-- =============================================

CREATE TABLE CHITIETPHIEUMUON
(
    MaChiTiet VARCHAR(20) PRIMARY KEY,
    MaPhieuMuon VARCHAR(20),
    MaDauSach VARCHAR(20),
    NgayTraThucTe DATE,
    TinhTrangTra NVARCHAR(100),

    CONSTRAINT UQ_PhieuMuon_DauSach
        UNIQUE (MaPhieuMuon, MaDauSach),

    CONSTRAINT FK_CTPM_PhieuMuon
        FOREIGN KEY (MaPhieuMuon)
        REFERENCES PHIEUMUON(MaPhieuMuon),

    CONSTRAINT FK_CTPM_DauSach
        FOREIGN KEY (MaDauSach)
        REFERENCES DAUSACH(MaDauSach)
);
GO


-- =============================================
-- 9. BẢNG PHIẾU PHẠT
-- =============================================

CREATE TABLE PHIEUPHAT
(
    MaPhieuPhat VARCHAR(20) PRIMARY KEY,
    MaChiTiet VARCHAR(20),
    MaNhanVien VARCHAR(20),
    NgayPhat DATE,
    LyDo NVARCHAR(200),
    PhiPhat DECIMAL(18,2),

    CONSTRAINT FK_PhieuPhat_CTPM
        FOREIGN KEY (MaChiTiet)
        REFERENCES CHITIETPHIEUMUON(MaChiTiet),

    CONSTRAINT FK_PhieuPhat_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES NHANVIEN(MaNhanVien)
);
GO


-- =============================================
-- KIỂM TRA CÁC BẢNG ĐÃ TẠO
-- =============================================

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO

USE QuanLyThuVienDB;
GO

-- 1. TrangThai của thẻ: đổi sang BIT
-- 1 = đang hoạt động
-- 0 = không hoạt động
ALTER TABLE THEDOCGIA
ALTER COLUMN TrangThai BIT NULL;
GO

-- 2. Số lượng sách không được âm
ALTER TABLE DAUSACH
ADD CONSTRAINT CK_DauSach_SoLuong
CHECK (SoLuongHienCo >= 0);
GO

-- 3. Hạn sử dụng thẻ phải >= ngày cấp
ALTER TABLE THEDOCGIA
ADD CONSTRAINT CK_TheDocGia_HanSuDung
CHECK (HanSuDung >= NgayCap);
GO

-- 4. Ngày hẹn trả phải >= ngày mượn
ALTER TABLE PHIEUMUON
ADD CONSTRAINT CK_PhieuMuon_NgayHenTra
CHECK (NgayHenTra >= NgayMuon);
GO

-- 5. Tên thể loại không được trùng
ALTER TABLE THELOAI
ADD CONSTRAINT UQ_TheLoai_TenTheLoai
UNIQUE (TenTheLoai);
GO

-- 6. Mỗi độc giả chỉ có 1 thẻ đang hoạt động
CREATE UNIQUE INDEX UX_TheDocGia_DocGia_Active
ON THEDOCGIA(MaDocGia)
WHERE TrangThai = 1;
GO