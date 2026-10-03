CREATE DATABASE ShopABCDB;
GO

USE ShopABCDB;
GO

CREATE TABLE KHACHHANG
(
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    CMNDPassport VARCHAR(20) NULL,
    DiaChi NVARCHAR(200) NOT NULL,
    DienThoai VARCHAR(15) NOT NULL,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NULL
);
GO

CREATE TABLE SANPHAM
(
    MaSanPham VARCHAR(20) PRIMARY KEY,
    TenSanPham NVARCHAR(150) NOT NULL,
    NhaSanXuat NVARCHAR(100) NULL,
    MoTa NVARCHAR(500) NULL,
    ThongSoKyThuat NVARCHAR(500) NULL,
    GiaBan DECIMAL(18,2) NOT NULL,
    TinhTrang NVARCHAR(50) NOT NULL,

    CONSTRAINT CK_SANPHAM_GiaBan
        CHECK (GiaBan >= 0)
);
GO

CREATE TABLE GIOHANG
(
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT NOT NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_GIOHANG_KHACHHANG
        FOREIGN KEY (MaKhachHang)
        REFERENCES KHACHHANG(MaKhachHang),

    CONSTRAINT UQ_GIOHANG_KHACHHANG
        UNIQUE (MaKhachHang)
);
GO

CREATE TABLE CHITIETGIOHANG
(
    MaGioHang INT NOT NULL,
    MaSanPham VARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,

    CONSTRAINT PK_CHITIETGIOHANG
        PRIMARY KEY (MaGioHang, MaSanPham),

    CONSTRAINT FK_CTGH_GIOHANG
        FOREIGN KEY (MaGioHang)
        REFERENCES GIOHANG(MaGioHang),

    CONSTRAINT FK_CTGH_SANPHAM
        FOREIGN KEY (MaSanPham)
        REFERENCES SANPHAM(MaSanPham),

    CONSTRAINT CK_CTGH_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTGH_DonGia
        CHECK (DonGia >= 0)
);
GO

CREATE TABLE LOAIGIAOHANG
(
    MaLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL UNIQUE,
    PhiGiaoHang DECIMAL(18,2) NOT NULL,
    ThoiGianXuLy NVARCHAR(100) NULL,

    CONSTRAINT CK_LOAIGIAOHANG_Phi
        CHECK (PhiGiaoHang >= 0)
);
GO
CREATE TABLE NGUOINHAN
(
    MaNguoiNhan INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200) NOT NULL,
    DienThoai VARCHAR(15) NOT NULL
);
GO
CREATE TABLE DONDATHANG
(
    MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT NOT NULL,
    MaNguoiNhan INT NOT NULL,
    MaLoai INT NOT NULL,
    ThoiDiemDat DATETIME NOT NULL DEFAULT GETDATE(),
    TongTienHang DECIMAL(18,2) NOT NULL,
    ChiPhiGiaoHang DECIMAL(18,2) NOT NULL,
    TongThanhToan DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_DONDATHANG_KHACHHANG
        FOREIGN KEY (MaKhachHang)
        REFERENCES KHACHHANG(MaKhachHang),

    CONSTRAINT FK_DONDATHANG_NGUOINHAN
        FOREIGN KEY (MaNguoiNhan)
        REFERENCES NGUOINHAN(MaNguoiNhan),

    CONSTRAINT FK_DONDATHANG_LOAIGIAOHANG
        FOREIGN KEY (MaLoai)
        REFERENCES LOAIGIAOHANG(MaLoai),

    CONSTRAINT CK_DONDATHANG_TongTienHang
        CHECK (TongTienHang >= 0),

    CONSTRAINT CK_DONDATHANG_ChiPhiGiaoHang
        CHECK (ChiPhiGiaoHang >= 0),

    CONSTRAINT CK_DONDATHANG_TongThanhToan
        CHECK (TongThanhToan >= 0)
);
GO

CREATE TABLE CHITIETDONHANG
(
    MaDonHang INT NOT NULL,
    MaSanPham VARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,

    CONSTRAINT PK_CHITIETDONHANG
        PRIMARY KEY (MaDonHang, MaSanPham),

    CONSTRAINT FK_CTDH_DONDATHANG
        FOREIGN KEY (MaDonHang)
        REFERENCES DONDATHANG(MaDonHang),

    CONSTRAINT FK_CTDH_SANPHAM
        FOREIGN KEY (MaSanPham)
        REFERENCES SANPHAM(MaSanPham),

    CONSTRAINT CK_CTDH_SoLuong
        CHECK (SoLuong > 0),

    CONSTRAINT CK_CTDH_DonGia
        CHECK (DonGia >= 0)
);
GO


/* =========================================
   1. DỮ LIỆU KHÁCH HÀNG MẪU
   ========================================= */
INSERT INTO KHACHHANG
    (HoTen, NgaySinh, CMNDPassport, DiaChi, DienThoai,
     TenDangNhap, MatKhau, Email)
VALUES
    (N'Nguyễn Văn An', '2003-05-15', '079203001234',
     N'TP. Hồ Chí Minh', '0901234567',
     'nguyenvanan', '123456', 'an@gmail.com'),

    (N'Trần Thị Bình', '2002-10-20', '079202005678',
     N'TP. Hồ Chí Minh', '0912345678',
     'tranthibinh', '123456', 'binh@gmail.com');
GO


/* =========================================
   2. DỮ LIỆU SẢN PHẨM MẪU
   Đây là dữ liệu mô phỏng Product Management System
   ========================================= */
INSERT INTO SANPHAM
    (MaSanPham, TenSanPham, NhaSanXuat, MoTa,
     ThongSoKyThuat, GiaBan, TinhTrang)
VALUES
    ('SP001', N'Laptop Dell Inspiron 15', N'Dell',
     N'Laptop phục vụ học tập và làm việc',
     N'Intel Core i5, RAM 16GB, SSD 512GB',
     15990000, N'Còn hàng'),

    ('SP002', N'Chuột Logitech M331', N'Logitech',
     N'Chuột không dây',
     N'Wireless, kết nối USB',
     350000, N'Còn hàng'),

    ('SP003', N'Bàn phím Logitech K120', N'Logitech',
     N'Bàn phím có dây',
     N'USB, Full Size',
     250000, N'Còn hàng'),

    ('SP004', N'Tai nghe Sony WH-CH520', N'Sony',
     N'Tai nghe Bluetooth',
     N'Bluetooth, pin lên đến 50 giờ',
     1290000, N'Còn hàng'),

    ('SP005', N'Màn hình Samsung 24 inch', N'Samsung',
     N'Màn hình máy tính Full HD',
     N'24 inch, Full HD, 75Hz',
     2890000, N'Còn hàng');
GO


/* =========================================
   3. CÁC LOẠI GIAO HÀNG
   ========================================= */
INSERT INTO LOAIGIAOHANG
    (TenLoai, PhiGiaoHang, ThoiGianXuLy)
VALUES
    (N'Giao hàng thông thường', 30000, N'3 - 5 ngày'),
    (N'Giao hàng nhanh',        50000, N'1 - 2 ngày'),
    (N'Giao hàng trong ngày',  80000, N'Trong ngày');
GO

SELECT * FROM KHACHHANG;
SELECT * FROM SANPHAM;
SELECT * FROM LOAIGIAOHANG;

SELECT * FROM GIOHANG;

SELECT * FROM CHITIETGIOHANG;


SELECT * FROM NGUOINHAN;

SELECT * FROM DONDATHANG;

SELECT * FROM CHITIETDONHANG;

SELECT * FROM CHITIETGIOHANG;



-- =============================================
-- 1. NHÓM SẢN PHẨM
-- =============================================
IF OBJECT_ID('NHOMSANPHAM', 'U') IS NULL
BEGIN
    CREATE TABLE NHOMSANPHAM
    (
        MaNhom VARCHAR(20) PRIMARY KEY,
        TenNhom NVARCHAR(100) NOT NULL UNIQUE
    );
END
GO


-- =============================================
-- 2. THÊM NHÓM VÀ HÌNH ẢNH VÀO SANPHAM
-- =============================================
IF COL_LENGTH('SANPHAM', 'MaNhom') IS NULL
BEGIN
    ALTER TABLE SANPHAM
    ADD MaNhom VARCHAR(20) NULL;
END
GO

IF COL_LENGTH('SANPHAM', 'HinhAnh') IS NULL
BEGIN
    ALTER TABLE SANPHAM
    ADD HinhAnh NVARCHAR(255) NULL;
END
GO


-- =============================================
-- 3. DỮ LIỆU NHÓM SẢN PHẨM
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM NHOMSANPHAM
    WHERE MaNhom = 'MT'
)
INSERT INTO NHOMSANPHAM
VALUES ('MT', N'Thiết bị máy tính');

IF NOT EXISTS (
    SELECT 1 FROM NHOMSANPHAM
    WHERE MaNhom = 'DT'
)
INSERT INTO NHOMSANPHAM
VALUES ('DT', N'Điện thoại');

IF NOT EXISTS (
    SELECT 1 FROM NHOMSANPHAM
    WHERE MaNhom = 'GD'
)
INSERT INTO NHOMSANPHAM
VALUES ('GD', N'Thiết bị điện gia dụng');

IF NOT EXISTS (
    SELECT 1 FROM NHOMSANPHAM
    WHERE MaNhom = 'PK'
)
INSERT INTO NHOMSANPHAM
VALUES ('PK', N'Phụ kiện');
GO


-- =============================================
-- 4. GÁN NHÓM CHO DỮ LIỆU MẪU
-- =============================================

-- Tạm gán toàn bộ về thiết bị máy tính
UPDATE SANPHAM
SET MaNhom = 'MT'
WHERE MaNhom IS NULL;
GO


-- =============================================
-- 5. TẠO FK
-- =============================================
IF NOT EXISTS
(
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'FK_SANPHAM_NHOMSANPHAM'
)
BEGIN
    ALTER TABLE SANPHAM
    ADD CONSTRAINT FK_SANPHAM_NHOMSANPHAM
    FOREIGN KEY (MaNhom)
    REFERENCES NHOMSANPHAM(MaNhom);
END
GO


-- =============================================
-- 6. KIỂM TRA
-- =============================================
SELECT * FROM NHOMSANPHAM;

SELECT
    MaSanPham,
    TenSanPham,
    NhaSanXuat,
    MoTa,
    ThongSoKyThuat,
    GiaBan,
    TinhTrang,
    MaNhom,
    HinhAnh
FROM SANPHAM;
GO



-- =============================================
-- KHU VỰC GIAO HÀNG
-- =============================================
IF OBJECT_ID('KHUVUCGIAOHANG', 'U') IS NULL
BEGIN
    CREATE TABLE KHUVUCGIAOHANG
    (
        MaKhuVuc VARCHAR(20) PRIMARY KEY,
        TenKhuVuc NVARCHAR(100) NOT NULL UNIQUE,
        PhuPhi DECIMAL(18,2) NOT NULL DEFAULT 0,

        CONSTRAINT CK_KHUVUCGIAOHANG_PhuPhi
        CHECK (PhuPhi >= 0)
    );
END
GO


-- =============================================
-- DỮ LIỆU DEMO
-- Lưu ý: mức phí này là dữ liệu mô phỏng của LAB,
-- file gốc không quy định con số cụ thể.
-- =============================================

IF NOT EXISTS
(
    SELECT 1
    FROM KHUVUCGIAOHANG
    WHERE MaKhuVuc = 'NOITHANH'
)
BEGIN
    INSERT INTO KHUVUCGIAOHANG
        (MaKhuVuc, TenKhuVuc, PhuPhi)
    VALUES
        ('NOITHANH', N'Nội thành', 0);
END


IF NOT EXISTS
(
    SELECT 1
    FROM KHUVUCGIAOHANG
    WHERE MaKhuVuc = 'NGOAITP'
)
BEGIN
    INSERT INTO KHUVUCGIAOHANG
        (MaKhuVuc, TenKhuVuc, PhuPhi)
    VALUES
        ('NGOAITP', N'Ngoại thành', 20000);
END


IF NOT EXISTS
(
    SELECT 1
    FROM KHUVUCGIAOHANG
    WHERE MaKhuVuc = 'TINHKHAC'
)
BEGIN
    INSERT INTO KHUVUCGIAOHANG
        (MaKhuVuc, TenKhuVuc, PhuPhi)
    VALUES
        ('TINHKHAC', N'Tỉnh / Thành phố khác', 40000);
END
GO


SELECT * FROM KHUVUCGIAOHANG;
GO


-- Thêm loại thẻ đã sử dụng
IF COL_LENGTH('DONDATHANG', 'LoaiThe') IS NULL
BEGIN
    ALTER TABLE DONDATHANG
    ADD LoaiThe NVARCHAR(30) NULL;
END
GO

-- Chỉ lưu 4 số cuối của thẻ
IF COL_LENGTH('DONDATHANG', 'BonSoCuoi') IS NULL
BEGIN
    ALTER TABLE DONDATHANG
    ADD BonSoCuoi VARCHAR(4) NULL;
END
GO

-- Phí giao dịch thanh toán
IF COL_LENGTH('DONDATHANG', 'PhiGiaoDich') IS NULL
BEGIN
    ALTER TABLE DONDATHANG
    ADD PhiGiaoDich DECIMAL(18,2) NOT NULL
        CONSTRAINT DF_DONDATHANG_PhiGiaoDich
        DEFAULT 0;
END
GO

SELECT TOP 10 *
FROM DONDATHANG
ORDER BY MaDonHang DESC;

SELECT
    MaDonHang,
    TongTienHang,
    ChiPhiGiaoHang,
    PhiGiaoDich,
    TongThanhToan,
    LoaiThe,
    BonSoCuoi
FROM DONDATHANG
ORDER BY MaDonHang DESC;


USE ShopABCDB;
GO

UPDATE SANPHAM
SET HinhAnh = 'Images\SP001.jpg'
WHERE MaSanPham = 'SP001';

UPDATE SANPHAM
SET HinhAnh = 'Images\SP002.jpg'
WHERE MaSanPham = 'SP002';

UPDATE SANPHAM
SET HinhAnh = 'Images\SP003.jpg'
WHERE MaSanPham = 'SP003';

UPDATE SANPHAM
SET HinhAnh = 'Images\SP004.jpg'
WHERE MaSanPham = 'SP004';

UPDATE SANPHAM
SET HinhAnh = 'Images\SP005.jpg'
WHERE MaSanPham = 'SP005';

SELECT MaSanPham, TenSanPham, HinhAnh
FROM SANPHAM
ORDER BY MaSanPham;

-- Thêm khu vực giao hàng vào đơn đặt hàng
IF COL_LENGTH('DONDATHANG', 'MaKhuVuc') IS NULL
BEGIN
    ALTER TABLE DONDATHANG
    ADD MaKhuVuc VARCHAR(20) NULL;
END
GO

-- Tạo khóa ngoại tới KHUVUCGIAOHANG
IF NOT EXISTS
(
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = 'FK_DONDATHANG_KHUVUCGIAOHANG'
)
BEGIN
    ALTER TABLE DONDATHANG
    ADD CONSTRAINT FK_DONDATHANG_KHUVUCGIAOHANG
        FOREIGN KEY (MaKhuVuc)
        REFERENCES KHUVUCGIAOHANG(MaKhuVuc);
END
GO

-- Kiểm tra
SELECT
    MaDonHang,
    MaKhachHang,
    MaNguoiNhan,
    MaLoai,
    MaKhuVuc,
    TongTienHang,
    ChiPhiGiaoHang,
    PhiGiaoDich,
    TongThanhToan,
    LoaiThe,
    BonSoCuoi,
    ThoiDiemDat
FROM DONDATHANG
ORDER BY MaDonHang;


SELECT
    MaDonHang,
    MaLoai,
    MaKhuVuc,
    TongTienHang,
    ChiPhiGiaoHang,
    PhiGiaoDich,
    TongThanhToan,
    LoaiThe,
    BonSoCuoi
FROM DONDATHANG
WHERE MaDonHang = 5;


USE ShopABCDB;
GO

SELECT TOP 5
    MaDonHang,
    MaKhachHang,
    MaNguoiNhan,
    MaLoai,
    MaKhuVuc,
    ThoiDiemDat,
    TongTienHang,
    ChiPhiGiaoHang,
    PhiGiaoDich,
    TongThanhToan,
    LoaiThe,
    BonSoCuoi
FROM DONDATHANG
ORDER BY MaDonHang DESC;



SELECT
    ct.MaDonHang,
    ct.MaSanPham,
    sp.TenSanPham,
    ct.SoLuong,
    ct.DonGia,
    ct.SoLuong * ct.DonGia AS ThanhTien
FROM CHITIETDONHANG ct
JOIN SANPHAM sp
    ON ct.MaSanPham = sp.MaSanPham
WHERE ct.MaDonHang = (
    SELECT MAX(MaDonHang)
    FROM DONDATHANG
);