
IF DB_ID(N'QuanLyChoThueVanPhong') IS NULL
    CREATE DATABASE QuanLyChoThueVanPhong;
GO

USE QuanLyChoThueVanPhong;
GO

CREATE TABLE dbo.Tang (
    MaTang VARCHAR(20) PRIMARY KEY,
    TenTang NVARCHAR(100) NOT NULL UNIQUE,
    DoCao DECIMAL(8,2) NULL CHECK (DoCao >= 0)
);

CREATE TABLE dbo.Phong (
    MaPhong VARCHAR(20) PRIMARY KEY,
    MaTang VARCHAR(20) NOT NULL,
    SoPhong NVARCHAR(30) NOT NULL,
    DienTich DECIMAL(12,2) NOT NULL CHECK (DienTich > 0),
    HuongPhong NVARCHAR(50) NOT NULL,
    DonGiaThue DECIMAL(18,2) NOT NULL CHECK (DonGiaThue > 0),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Sẵn sàng',
    CONSTRAINT UQ_Phong_TangSo UNIQUE (MaTang, SoPhong),
    CONSTRAINT FK_Phong_Tang FOREIGN KEY (MaTang)
        REFERENCES dbo.Tang(MaTang),
    CONSTRAINT CK_Phong_TrangThai CHECK
        (TrangThai IN (N'Sẵn sàng', N'Bảo trì', N'Ngừng sử dụng'))
);

CREATE TABLE dbo.KhachHang (
    MaKH VARCHAR(20) PRIMARY KEY,
    TenKhachHang NVARCHAR(200) NOT NULL,
    LoaiKhachHang NVARCHAR(30) NOT NULL,
    MaSoThue VARCHAR(30) NULL,
    NguoiDaiDien NVARCHAR(100) NULL,
    DienThoai VARCHAR(20) NULL,
    Email VARCHAR(150) NULL,
    DiaChi NVARCHAR(255) NULL,
    CONSTRAINT CK_KH_Loai CHECK
        (LoaiKhachHang IN (N'Cá nhân', N'Công ty'))
);
CREATE UNIQUE INDEX UX_KH_MST ON dbo.KhachHang(MaSoThue)
WHERE MaSoThue IS NOT NULL;

CREATE TABLE dbo.HopDongThue (
    MaHopDong VARCHAR(20) PRIMARY KEY,
    MaKH VARCHAR(20) NOT NULL,
    NgayKy DATE NOT NULL,
    NgayBatDau DATE NOT NULL,
    ThoiHanThue INT NOT NULL CHECK (ThoiHanThue >= 6),
    NgayKetThuc DATE NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Có hiệu lực',
    NoiDung NVARCHAR(MAX) NULL,
    CONSTRAINT FK_HD_KH FOREIGN KEY (MaKH)
        REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT CK_HD_TrangThai CHECK
        (TrangThai IN (N'Có hiệu lực', N'Hết hạn', N'Chấm dứt')),
    CONSTRAINT CK_HD_Ngay CHECK (NgayKetThuc >= NgayBatDau)
);

CREATE TABLE dbo.ChiTietHopDong (
    MaChiTietHD VARCHAR(20) PRIMARY KEY,
    MaHopDong VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    DonGiaThue DECIMAL(18,2) NOT NULL CHECK (DonGiaThue > 0),
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    CONSTRAINT UQ_CTHD_Phong UNIQUE (MaHopDong, MaPhong),
    CONSTRAINT UQ_CTHD_MaPhong UNIQUE (MaChiTietHD, MaPhong),
    CONSTRAINT FK_CTHD_HD FOREIGN KEY (MaHopDong)
        REFERENCES dbo.HopDongThue(MaHopDong),
    CONSTRAINT FK_CTHD_Phong FOREIGN KEY (MaPhong)
        REFERENCES dbo.Phong(MaPhong),
    CONSTRAINT CK_CTHD_Ngay CHECK (NgayKetThuc >= NgayBatDau)
);

CREATE TABLE dbo.PhieuGiaHan (
    MaPhieuGiaHan VARCHAR(20) PRIMARY KEY,
    MaChiTietHD VARCHAR(20) NOT NULL,
    NgayLap DATE NOT NULL,
    NgayBatDauGiaHan DATE NOT NULL,
    SoThangGiaHan INT NOT NULL CHECK (SoThangGiaHan > 0),
    NgayKetThucMoi DATE NOT NULL,
    GhiChu NVARCHAR(500) NULL,
    CONSTRAINT FK_GH_CTHD FOREIGN KEY (MaChiTietHD)
        REFERENCES dbo.ChiTietHopDong(MaChiTietHD),
    CONSTRAINT CK_GH_Ngay CHECK (NgayKetThucMoi >= NgayBatDauGiaHan)
);

CREATE TABLE dbo.HoaDonTienThue (
    MaHoaDonThue VARCHAR(20) PRIMARY KEY,
    MaHopDong VARCHAR(20) NOT NULL,
    SoKyBatDau INT NOT NULL CHECK (SoKyBatDau >= 1),
    SoKyKetThuc INT NOT NULL,
    NgayLap DATE NOT NULL,
    HanThanhToan DATE NOT NULL,
    TongTien DECIMAL(18,2) NOT NULL CHECK (TongTien > 0),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chưa thanh toán',
    CONSTRAINT UQ_HDThue_Ky UNIQUE
        (MaHopDong, SoKyBatDau, SoKyKetThuc),
    CONSTRAINT FK_HDThue_HD FOREIGN KEY (MaHopDong)
        REFERENCES dbo.HopDongThue(MaHopDong),
    CONSTRAINT CK_HDThue_Ky CHECK (SoKyKetThuc >= SoKyBatDau),
    CONSTRAINT CK_HDThue_TrangThai CHECK
        (TrangThai IN (N'Chưa thanh toán', N'Thanh toán một phần', N'Đã thanh toán'))
);

CREATE TABLE dbo.ChiTietHoaDonThue (
    MaCTHDThue VARCHAR(20) PRIMARY KEY,
    MaHoaDonThue VARCHAR(20) NOT NULL,
    MaChiTietHD VARCHAR(20) NOT NULL,
    ThanhTien DECIMAL(18,2) NOT NULL CHECK (ThanhTien > 0),
    CONSTRAINT UQ_CTHDThue UNIQUE (MaHoaDonThue, MaChiTietHD),
    CONSTRAINT FK_CTHDThue_HD FOREIGN KEY (MaHoaDonThue)
        REFERENCES dbo.HoaDonTienThue(MaHoaDonThue),
    CONSTRAINT FK_CTHDThue_CTHD FOREIGN KEY (MaChiTietHD)
        REFERENCES dbo.ChiTietHopDong(MaChiTietHD)
);

CREATE TABLE dbo.PhieuThuTienThue (
    MaPhieuThu VARCHAR(20) PRIMARY KEY,
    MaHoaDonThue VARCHAR(20) NOT NULL,
    NgayThu DATE NOT NULL,
    SoTienThu DECIMAL(18,2) NOT NULL CHECK (SoTienThu > 0),
    HinhThucThu NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_PTT_HD FOREIGN KEY (MaHoaDonThue)
        REFERENCES dbo.HoaDonTienThue(MaHoaDonThue),
    CONSTRAINT CK_PTT_HinhThuc CHECK
        (HinhThucThu IN (N'Tiền mặt', N'Chuyển khoản'))
);

CREATE TABLE dbo.DichVu (
    MaDichVu VARCHAR(20) PRIMARY KEY,
    TenDichVu NVARCHAR(100) NOT NULL UNIQUE,
    DonViTinh NVARCHAR(30) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0)
);

CREATE TABLE dbo.SuDungDichVu (
    MaSuDung VARCHAR(20) PRIMARY KEY,
    MaChiTietHD VARCHAR(20) NOT NULL,
    MaDichVu VARCHAR(20) NOT NULL,
    Thang INT NOT NULL CHECK (Thang BETWEEN 1 AND 12),
    Nam INT NOT NULL CHECK (Nam BETWEEN 2000 AND 2100),
    SoLuong DECIMAL(18,2) NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18,2) NOT NULL CHECK (DonGia >= 0),
    ThanhTien AS CAST(SoLuong * DonGia AS DECIMAL(18,2)) PERSISTED,
    CONSTRAINT UQ_SDDV_Ky UNIQUE (MaChiTietHD, MaDichVu, Thang, Nam),
    CONSTRAINT FK_SDDV_CTHD FOREIGN KEY (MaChiTietHD)
        REFERENCES dbo.ChiTietHopDong(MaChiTietHD),
    CONSTRAINT FK_SDDV_DV FOREIGN KEY (MaDichVu)
        REFERENCES dbo.DichVu(MaDichVu)
);

CREATE TABLE dbo.HoaDonDichVu (
    MaHoaDonDV VARCHAR(20) PRIMARY KEY,
    MaChiTietHD VARCHAR(20) NOT NULL,
    Thang INT NOT NULL CHECK (Thang BETWEEN 1 AND 12),
    Nam INT NOT NULL CHECK (Nam BETWEEN 2000 AND 2100),
    NgayLap DATE NOT NULL,
    HanThanhToan DATE NOT NULL,
    TongTien DECIMAL(18,2) NOT NULL CHECK (TongTien >= 0),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chưa thanh toán',
    CONSTRAINT UQ_HDDV_Ky UNIQUE (MaChiTietHD, Thang, Nam),
    CONSTRAINT FK_HDDV_CTHD FOREIGN KEY (MaChiTietHD)
        REFERENCES dbo.ChiTietHopDong(MaChiTietHD),
    CONSTRAINT CK_HDDV_TrangThai CHECK
        (TrangThai IN (N'Chưa thanh toán', N'Thanh toán một phần', N'Đã thanh toán'))
);

CREATE TABLE dbo.ChiTietHoaDonDichVu (
    MaCTHDDV VARCHAR(20) PRIMARY KEY,
    MaHoaDonDV VARCHAR(20) NOT NULL,
    MaSuDung VARCHAR(20) NOT NULL UNIQUE,
    ThanhTien DECIMAL(18,2) NOT NULL CHECK (ThanhTien >= 0),
    CONSTRAINT FK_CTHDDV_HD FOREIGN KEY (MaHoaDonDV)
        REFERENCES dbo.HoaDonDichVu(MaHoaDonDV),
    CONSTRAINT FK_CTHDDV_SDDV FOREIGN KEY (MaSuDung)
        REFERENCES dbo.SuDungDichVu(MaSuDung)
);

CREATE TABLE dbo.PhieuThuDichVu (
    MaPhieuThuDV VARCHAR(20) PRIMARY KEY,
    MaHoaDonDV VARCHAR(20) NOT NULL,
    NgayThu DATE NOT NULL,
    SoTienThu DECIMAL(18,2) NOT NULL CHECK (SoTienThu > 0),
    HinhThucThu NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_PTDV_HD FOREIGN KEY (MaHoaDonDV)
        REFERENCES dbo.HoaDonDichVu(MaHoaDonDV),
    CONSTRAINT CK_PTDV_HinhThuc CHECK
        (HinhThucThu IN (N'Tiền mặt', N'Chuyển khoản'))
);

CREATE TABLE dbo.NhanVienVanPhong (
    MaNV VARCHAR(20) PRIMARY KEY,
    MaKH VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    ChucVu NVARCHAR(100) NULL,
    HinhAnh NVARCHAR(255) NULL,
    DienThoai VARCHAR(20) NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Đang làm việc',
    CONSTRAINT FK_NV_KH FOREIGN KEY (MaKH)
        REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT FK_NV_Phong FOREIGN KEY (MaPhong)
        REFERENCES dbo.Phong(MaPhong),
    CONSTRAINT CK_NV_TrangThai CHECK
        (TrangThai IN (N'Đang làm việc', N'Đã nghỉ'))
);

CREATE TABLE dbo.PhuongTien (
    MaPhuongTien VARCHAR(20) PRIMARY KEY,
    MaNV VARCHAR(20) NOT NULL,
    BienSoXe VARCHAR(30) NOT NULL,
    LoaiXe NVARCHAR(50) NOT NULL,
    MauXe NVARCHAR(50) NULL,
    CONSTRAINT UQ_PT_BienSo UNIQUE (BienSoXe),
    CONSTRAINT UQ_PT_MaNV UNIQUE (MaPhuongTien, MaNV),
    CONSTRAINT FK_PT_NV FOREIGN KEY (MaNV)
        REFERENCES dbo.NhanVienVanPhong(MaNV)
);

CREATE TABLE dbo.TheGiuXe (
    MaThe VARCHAR(20) PRIMARY KEY,
    MaNV VARCHAR(20) NOT NULL,
    MaPhuongTien VARCHAR(20) NOT NULL,
    NgayCap DATE NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Đang sử dụng',
    CONSTRAINT FK_The_NV FOREIGN KEY (MaNV)
        REFERENCES dbo.NhanVienVanPhong(MaNV),
    CONSTRAINT FK_The_PT_NV FOREIGN KEY (MaPhuongTien, MaNV)
        REFERENCES dbo.PhuongTien(MaPhuongTien, MaNV),
    CONSTRAINT CK_The_TrangThai CHECK
        (TrangThai IN (N'Đang sử dụng', N'Đã hủy'))
);
CREATE UNIQUE INDEX UX_TheXe_DangDung
ON dbo.TheGiuXe(MaPhuongTien)
WHERE TrangThai = N'Đang sử dụng';

CREATE TABLE dbo.PhieuKiemTraPhong (
    MaPhieuKT VARCHAR(20) PRIMARY KEY,
    MaPhong VARCHAR(20) NOT NULL,
    NgayKiemTra DATE NOT NULL,
    TinhTrang NVARCHAR(500) NOT NULL,
    KetQua NVARCHAR(500) NOT NULL,
    NguoiKiemTra NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_KT_Phong FOREIGN KEY (MaPhong)
        REFERENCES dbo.Phong(MaPhong)
);

CREATE TABLE dbo.PhieuSuaChua (
    MaPhieuSC VARCHAR(20) PRIMARY KEY,
    MaPhieuKT VARCHAR(20) NOT NULL,
    NgayLap DATE NOT NULL,
    NoiDungSuaChua NVARCHAR(500) NOT NULL,
    ChiPhiDuKien DECIMAL(18,2) NOT NULL CHECK (ChiPhiDuKien >= 0),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chưa thực hiện',
    CONSTRAINT FK_SC_KT FOREIGN KEY (MaPhieuKT)
        REFERENCES dbo.PhieuKiemTraPhong(MaPhieuKT),
    CONSTRAINT CK_SC_TrangThai CHECK
        (TrangThai IN (N'Chưa thực hiện', N'Đang sửa', N'Hoàn thành'))
);

CREATE TABLE dbo.PhieuXuPhat (
    MaPhieuPhat VARCHAR(20) PRIMARY KEY,
    MaPhieuKT VARCHAR(20) NOT NULL,
    MaChiTietHD VARCHAR(20) NOT NULL,
    NgayLap DATE NOT NULL,
    LyDo NVARCHAR(500) NOT NULL,
    SoTienPhat DECIMAL(18,2) NOT NULL CHECK (SoTienPhat > 0),
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chưa thanh toán',
    CONSTRAINT FK_XP_KT FOREIGN KEY (MaPhieuKT)
        REFERENCES dbo.PhieuKiemTraPhong(MaPhieuKT),
    CONSTRAINT FK_XP_CTHD FOREIGN KEY (MaChiTietHD)
        REFERENCES dbo.ChiTietHopDong(MaChiTietHD),
    CONSTRAINT CK_XP_TrangThai CHECK
        (TrangThai IN (N'Chưa thanh toán', N'Thanh toán một phần', N'Đã thanh toán'))
);

CREATE TABLE dbo.PhieuThuXuPhat (
    MaPhieuThuPhat VARCHAR(20) PRIMARY KEY,
    MaPhieuPhat VARCHAR(20) NOT NULL,
    NgayThu DATE NOT NULL,
    SoTienThu DECIMAL(18,2) NOT NULL CHECK (SoTienThu > 0),
    HinhThucThu NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_PTXPhat_XP FOREIGN KEY (MaPhieuPhat)
        REFERENCES dbo.PhieuXuPhat(MaPhieuPhat),
    CONSTRAINT CK_PTXPhat_HT CHECK
        (HinhThucThu IN (N'Tiền mặt', N'Chuyển khoản'))
);
GO


USE QuanLyChoThueVanPhong;
GO

INSERT INTO dbo.Tang (MaTang, TenTang, DoCao) VALUES
('T01', N'Tầng 1', 4.00),
('T02', N'Tầng 2', 8.00);

INSERT INTO dbo.Phong
(MaPhong, MaTang, SoPhong, DienTich, HuongPhong, DonGiaThue, TrangThai)
VALUES
('P101', 'T01', N'101', 60, N'Đông', 10000000, N'Sẵn sàng'),
('P102', 'T01', N'102', 70, N'Tây', 12000000, N'Sẵn sàng'),
('P201', 'T02', N'201', 90, N'Nam', 16000000, N'Sẵn sàng');

INSERT INTO dbo.KhachHang
(MaKH, TenKhachHang, LoaiKhachHang, MaSoThue,
 NguoiDaiDien, DienThoai, Email, DiaChi)
VALUES
('KH001', N'Công ty An Phát', N'Công ty', '0312345678',
 N'Nguyễn Văn An', '0901234567', 'anphat@example.com', N'TP.HCM'),
('KH002', N'Công ty Bình Minh', N'Công ty', '0312345679',
 N'Trần Thị Bình', '0901234568', 'binhminh@example.com', N'TP.HCM');

INSERT INTO dbo.DichVu (MaDichVu, TenDichVu, DonViTinh, DonGia)
VALUES
('DV01', N'Điện', N'kWh', 3000),
('DV02', N'Nước', N'm3', 15000),
('DV03', N'Internet', N'Tháng', 500000);

INSERT INTO dbo.HopDongThue
(MaHopDong, MaKH, NgayKy, NgayBatDau, ThoiHanThue,
 NgayKetThuc, TrangThai, NoiDung)
VALUES
('HD001', 'KH001', '2026-10-30', '2027-01-01',
 12, '2027-12-31', N'Có hiệu lực',
 N'Thuê P101 và P102 trong 12 tháng');

INSERT INTO dbo.ChiTietHopDong
(MaChiTietHD, MaHopDong, MaPhong, DonGiaThue, NgayBatDau, NgayKetThuc)
VALUES
('CTHD001', 'HD001', 'P101', 10000000, '2027-01-01', '2027-12-31'),
('CTHD002', 'HD001', 'P102', 12000000, '2027-01-01', '2027-12-31');

INSERT INTO dbo.HoaDonTienThue
(MaHoaDonThue, MaHopDong, SoKyBatDau, SoKyKetThuc,
 NgayLap, HanThanhToan, TongTien, TrangThai)
VALUES
('HDT001', 'HD001', 1, 6,
 '2026-10-30', '2027-01-01', 132000000, N'Chưa thanh toán');

INSERT INTO dbo.ChiTietHoaDonThue
(MaCTHDThue, MaHoaDonThue, MaChiTietHD, ThanhTien)
VALUES
('CTT001', 'HDT001', 'CTHD001', 60000000),
('CTT002', 'HDT001', 'CTHD002', 72000000);
GO


USE QuanLyChoThueVanPhong;
GO

-- 1. Kiểm tra danh sách phòng
SELECT p.MaPhong, t.TenTang, p.DienTich,
       p.HuongPhong, p.DonGiaThue
FROM dbo.Phong p
JOIN dbo.Tang t ON t.MaTang = p.MaTang;

-- 2. Kiểm tra tổng tiền thuê tháng của HD001
SELECT MaHopDong, SUM(DonGiaThue) AS TongThueThang
FROM dbo.ChiTietHopDong
GROUP BY MaHopDong;

-- 3. Kiểm tra tiền thuê 6 tháng đầu
SELECT h.MaHoaDonThue, h.TongTien,
       ISNULL(SUM(p.SoTienThu), 0) AS DaThu,
       h.TongTien - ISNULL(SUM(p.SoTienThu), 0) AS ConNo
FROM dbo.HoaDonTienThue h
LEFT JOIN dbo.PhieuThuTienThue p
    ON p.MaHoaDonThue = h.MaHoaDonThue
GROUP BY h.MaHoaDonThue, h.TongTien;

-- 4. Kiểm tra số bảng đã tạo
SELECT COUNT(*) AS SoBang
FROM sys.tables
WHERE schema_id = SCHEMA_ID('dbo');
GO


USE QuanLyChoThueVanPhong;
GO

SELECT DB_NAME() AS DatabaseHienTai;

SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;