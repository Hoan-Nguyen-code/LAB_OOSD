/* ============================================================
   HỆ THỐNG QUẢN LÝ THƯ VIỆN
   Database: QuanLyThuVienDB
   SQL Server
   ============================================================ */

USE master;
GO

/* ============================================================
   1. TẠO DATABASE
   ============================================================ */

IF DB_ID(N'QuanLyThuVienDB') IS NULL
    CREATE DATABASE QuanLyThuVienDB;
GO

USE QuanLyThuVienDB;
GO


/* ============================================================
   2. BẢNG NHÂN VIÊN
   ============================================================ */

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


/* ============================================================
   3. BẢNG ĐỘC GIẢ
   ============================================================ */

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


/* ============================================================
   4. BẢNG THẺ ĐỘC GIẢ
   ============================================================ */

CREATE TABLE THEDOCGIA
(
    MaThe VARCHAR(20) PRIMARY KEY,
    MaDocGia VARCHAR(20),
    NgayCap DATE,
    HanSuDung DATE,
    DaDongLePhi BIT,

    -- 1 = đang hoạt động
    -- 0 = không hoạt động
    TrangThai BIT,

    CONSTRAINT CK_TheDocGia_HanSuDung
        CHECK (HanSuDung >= NgayCap),

    CONSTRAINT FK_TheDocGia_DocGia
        FOREIGN KEY (MaDocGia)
        REFERENCES DOCGIA(MaDocGia)
);
GO


/* Mỗi độc giả chỉ được có 1 thẻ đang hoạt động */
CREATE UNIQUE INDEX UX_TheDocGia_DocGia_Active
ON THEDOCGIA(MaDocGia)
WHERE TrangThai = 1;
GO


/* ============================================================
   5. BẢNG NHÀ XUẤT BẢN
   ============================================================ */

CREATE TABLE NHAXUATBAN
(
    MaNhaXuatBan VARCHAR(20) PRIMARY KEY,
    DiaChi NVARCHAR(200),
    SoDienThoai VARCHAR(15)
);
GO


/* ============================================================
   6. BẢNG THỂ LOẠI
   ============================================================ */

CREATE TABLE THELOAI
(
    MaTheLoai VARCHAR(20) PRIMARY KEY,
    TenTheLoai NVARCHAR(100),

    CONSTRAINT UQ_TheLoai_TenTheLoai
        UNIQUE (TenTheLoai)
);
GO


/* ============================================================
   7. BẢNG ĐẦU SÁCH
   ============================================================ */

CREATE TABLE DAUSACH
(
    MaDauSach VARCHAR(20) PRIMARY KEY,
    TenSach NVARCHAR(200),
    NamXuatBan INT,
    SoLuongHienCo INT,
    MaTheLoai VARCHAR(20),
    MaNhaXuatBan VARCHAR(20),

    CONSTRAINT CK_DauSach_SoLuong
        CHECK (SoLuongHienCo >= 0),

    CONSTRAINT FK_DauSach_TheLoai
        FOREIGN KEY (MaTheLoai)
        REFERENCES THELOAI(MaTheLoai),

    CONSTRAINT FK_DauSach_NXB
        FOREIGN KEY (MaNhaXuatBan)
        REFERENCES NHAXUATBAN(MaNhaXuatBan)
);
GO


/* ============================================================
   8. BẢNG PHIẾU MƯỢN
   ============================================================ */

CREATE TABLE PHIEUMUON
(
    MaPhieuMuon VARCHAR(20) PRIMARY KEY,
    MaDocGia VARCHAR(20),
    MaNhanVien VARCHAR(20),
    NgayMuon DATE,
    NgayHenTra DATE,

    CONSTRAINT CK_PhieuMuon_NgayHenTra
        CHECK (NgayHenTra >= NgayMuon),

    CONSTRAINT FK_PhieuMuon_DocGia
        FOREIGN KEY (MaDocGia)
        REFERENCES DOCGIA(MaDocGia),

    CONSTRAINT FK_PhieuMuon_NhanVien
        FOREIGN KEY (MaNhanVien)
        REFERENCES NHANVIEN(MaNhanVien)
);
GO


/* ============================================================
   9. BẢNG CHI TIẾT PHIẾU MƯỢN
   ============================================================ */

CREATE TABLE CHITIETPHIEUMUON
(
    MaChiTiet VARCHAR(20) PRIMARY KEY,
    MaPhieuMuon VARCHAR(20),
    MaDauSach VARCHAR(20),
    NgayTraThucTe DATE,
    TinhTrangTra NVARCHAR(100),

    /* Không được mượn trùng cùng đầu sách
       trong cùng một phiếu mượn */
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


/* ============================================================
   10. BẢNG PHIẾU PHẠT
   ============================================================ */

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


/* ============================================================
   11. DỮ LIỆU MẪU
   ============================================================ */

/* ---------- NHÂN VIÊN ---------- */

INSERT INTO NHANVIEN
(
    MaNhanVien,
    Ho,
    Ten,
    Phai,
    NgaySinh,
    ChucVu,
    SoDienThoai
)
VALUES
(
    '001',
    N'Nguyễn',
    N'An',
    N'Nam',
    '2000-01-01',
    N'Thủ thư',
    '0900000001'
);
GO


/* ---------- THỂ LOẠI ---------- */

INSERT INTO THELOAI
(
    MaTheLoai,
    TenTheLoai
)
VALUES
(
    'TL001',
    N'Công nghệ thông tin'
);
GO


/* ---------- NHÀ XUẤT BẢN ---------- */

INSERT INTO NHAXUATBAN
(
    MaNhaXuatBan,
    DiaChi,
    SoDienThoai
)
VALUES
(
    'NXB001',
    N'TP. Hồ Chí Minh',
    '0900000002'
);
GO


/* ---------- ĐẦU SÁCH ---------- */

INSERT INTO DAUSACH
(
    MaDauSach,
    TenSach,
    NamXuatBan,
    SoLuongHienCo,
    MaTheLoai,
    MaNhaXuatBan
)
VALUES
(
    'DS001',
    N'Lập trình C#',
    2025,
    5,
    'TL001',
    'NXB001'
);
GO


/* ---------- ĐỘC GIẢ ---------- */

INSERT INTO DOCGIA
(
    MaDocGia,
    Ho,
    Ten,
    NgaySinh,
    Phai,
    SoDienThoai,
    DiaChi,
    Email,
    Anh3x4
)
VALUES
(
    'DG001',
    N'Nguyễn Gia',
    N'Huy',
    '2005-09-18',
    N'Nam',
    '09849774572',
    N'Long An',
    'Huy@gmail.com',
    NULL
);
GO


/* ---------- THẺ ĐỘC GIẢ ---------- */

INSERT INTO THEDOCGIA
(
    MaThe,
    MaDocGia,
    NgayCap,
    HanSuDung,
    DaDongLePhi,
    TrangThai
)
VALUES
(
    'T260918000000001',
    'DG001',
    '2026-09-18',
    '2027-09-18',
    1,
    1
);
GO


/* ============================================================
   12. KIỂM TRA CÁC BẢNG
   ============================================================ */

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO

PRINT N'Tạo cơ sở dữ liệu QuanLyThuVienDB thành công.';
GO