
IF DB_ID(N'QuanLyHoSoBenhAn') IS NULL
    CREATE DATABASE QuanLyHoSoBenhAn;
GO

USE QuanLyHoSoBenhAn;
GO

-- 1. Bệnh nhân
CREATE TABLE BenhNhan (
    MaBN VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(120) NOT NULL,
    DiaChi NVARCHAR(250) NOT NULL,
    NgaySinh DATE NOT NULL,
    DienThoai VARCHAR(20) NOT NULL,
    Anh3x4 NVARCHAR(500) NULL
);

-- 2. Phiếu đăng ký lần đầu
CREATE TABLE PhieuDangKy (
    MaDK VARCHAR(20) PRIMARY KEY,
    MaBN VARCHAR(20) NOT NULL UNIQUE,
    NgayDangKy DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Chờ hoàn tất',
    CONSTRAINT FK_DK_BN FOREIGN KEY (MaBN)
        REFERENCES BenhNhan(MaBN),
    CONSTRAINT CK_DK_TT CHECK
        (TrangThai IN (N'Chờ hoàn tất', N'Hoàn tất'))
);

-- 3. Lệ phí hằng năm
CREATE TABLE LePhiNam (
    MaLePhi VARCHAR(20) PRIMARY KEY,
    MaBN VARCHAR(20) NOT NULL,
    NgayDong DATETIME2 NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL CHECK (SoTien >= 0),
    NgayBatDau DATE NOT NULL,
    NgayHetHan DATE NOT NULL,
    CONSTRAINT FK_LP_BN FOREIGN KEY (MaBN)
        REFERENCES BenhNhan(MaBN),
    CONSTRAINT CK_LP_Ngay CHECK (NgayHetHan > NgayBatDau),
    CONSTRAINT UQ_LP_Ky UNIQUE (MaBN, NgayBatDau)
);

-- 4. Hồ sơ bệnh án
CREATE TABLE HoSoBenhAn (
    MaHoSo VARCHAR(20) PRIMARY KEY,
    MaBN VARCHAR(20) NOT NULL UNIQUE,
    NgayCap DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Đang sử dụng',
    CONSTRAINT FK_HS_BN FOREIGN KEY (MaBN)
        REFERENCES BenhNhan(MaBN)
);

-- 5. Khoa điều trị
CREATE TABLE KhoaDieuTri (
    MaKhoa VARCHAR(20) PRIMARY KEY,
    TenKhoa NVARCHAR(150) NOT NULL UNIQUE
);

-- 6. Bác sĩ
CREATE TABLE BacSi (
    MaBS VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(120) NOT NULL,
    ChuyenMon NVARCHAR(150) NULL,
    MaKhoa VARCHAR(20) NOT NULL,
    CONSTRAINT FK_BS_Khoa FOREIGN KEY (MaKhoa)
        REFERENCES KhoaDieuTri(MaKhoa)
);

-- 7. Phiếu khám
CREATE TABLE PhieuKham (
    MaPhieuKham VARCHAR(20) PRIMARY KEY,
    MaHoSo VARCHAR(20) NOT NULL,
    MaBS VARCHAR(20) NOT NULL,
    MaKhoa VARCHAR(20) NOT NULL,
    NgayGioKham DATETIME2 NOT NULL,
    TrieuChung NVARCHAR(1000) NULL,
    NhietDo DECIMAL(4,1) NULL,
    HuyetAp NVARCHAR(30) NULL,
    ChanDoan NVARCHAR(1000) NULL,
    PhanUngThuoc NVARCHAR(1000) NULL,
    HieuQuaDieuTri NVARCHAR(1000) NULL,
    LoaiKham VARCHAR(20) NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Đang khám',
    CONSTRAINT FK_PK_HS FOREIGN KEY (MaHoSo)
        REFERENCES HoSoBenhAn(MaHoSo),
    CONSTRAINT FK_PK_BS FOREIGN KEY (MaBS)
        REFERENCES BacSi(MaBS),
    CONSTRAINT FK_PK_Khoa FOREIGN KEY (MaKhoa)
        REFERENCES KhoaDieuTri(MaKhoa),
    CONSTRAINT CK_PK_Loai CHECK
        (LoaiKham IN ('THUONG', 'DINHKY')),
    CONSTRAINT CK_PK_TrangThai CHECK
        (TrangThai IN (
            N'Đang khám',
            N'Chờ hoàn tất',
            N'Hoàn tất'
        ))
);

-- 8. Xét nghiệm
CREATE TABLE XetNghiem (
    MaXN VARCHAR(20) PRIMARY KEY,
    MaPhieuKham VARCHAR(20) NOT NULL,
    TenXetNghiem NVARCHAR(160) NOT NULL,
    NgayChiDinh DATETIME2 NOT NULL
        DEFAULT SYSDATETIME(),
    KetQua NVARCHAR(1000) NULL,
    NgayCoKetQua DATETIME2 NULL,
    CONSTRAINT FK_XN_PK FOREIGN KEY (MaPhieuKham)
        REFERENCES PhieuKham(MaPhieuKham)
);

-- 9. Thuốc
CREATE TABLE Thuoc (
    MaThuoc VARCHAR(20) PRIMARY KEY,
    TenThuoc NVARCHAR(160) NOT NULL,
    DonViTinh NVARCHAR(50) NOT NULL,
    HamLuong NVARCHAR(100) NULL
);

-- 10. Toa thuốc
CREATE TABLE ToaThuoc (
    MaToa VARCHAR(20) PRIMARY KEY,
    MaPhieuKham VARCHAR(20) NOT NULL UNIQUE,
    NgayKe DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    LoiDan NVARCHAR(1000) NULL,
    CONSTRAINT FK_Toa_PK FOREIGN KEY (MaPhieuKham)
        REFERENCES PhieuKham(MaPhieuKham)
);

-- 11. Chi tiết toa thuốc
CREATE TABLE ChiTietToaThuoc (
    MaToa VARCHAR(20) NOT NULL,
    MaThuoc VARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    LieuDung NVARCHAR(250) NOT NULL,
    CachDung NVARCHAR(250) NOT NULL,
    PRIMARY KEY (MaToa, MaThuoc),
    CONSTRAINT FK_CT_Toa FOREIGN KEY (MaToa)
        REFERENCES ToaThuoc(MaToa),
    CONSTRAINT FK_CT_Thuoc FOREIGN KEY (MaThuoc)
        REFERENCES Thuoc(MaThuoc)
);

-- 12. Lịch khám định kỳ
CREATE TABLE LichKhamDinhKy (
    MaLich VARCHAR(20) PRIMARY KEY,
    MaHoSo VARCHAR(20) NOT NULL,
    NgayHen DATETIME2 NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Đã đặt',
    CONSTRAINT FK_Lich_HS FOREIGN KEY (MaHoSo)
        REFERENCES HoSoBenhAn(MaHoSo),
    CONSTRAINT CK_Lich_TT CHECK
        (TrangThai IN (N'Đã đặt', N'Đã khám', N'Đã hủy'))
);

-- 13. Thanh toán viện phí
CREATE TABLE ThanhToanVienPhi (
    MaThanhToan VARCHAR(20) PRIMARY KEY,
    MaPhieuKham VARCHAR(20) NOT NULL UNIQUE,
    NgayThanhToan DATETIME2 NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL CHECK (SoTien >= 0),
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Đã thanh toán',
    CONSTRAINT FK_TT_PK FOREIGN KEY (MaPhieuKham)
        REFERENCES PhieuKham(MaPhieuKham),
    CONSTRAINT CK_TT_TrangThai CHECK
        (TrangThai = N'Đã thanh toán')
);
GO

CREATE INDEX IX_PhieuKham_HoSo
    ON PhieuKham(MaHoSo, NgayGioKham);

CREATE INDEX IX_XetNghiem_PhieuKham
    ON XetNghiem(MaPhieuKham);

CREATE INDEX IX_LichKham_HoSo
    ON LichKhamDinhKy(MaHoSo, NgayHen);
GO

PRINT N'Da tao xong 13 bang QuanLyHoSoBenhAn';

USE QuanLyHoSoBenhAn;
GO

SELECT
    TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;



USE QuanLyHoSoBenhAn;
GO

IF NOT EXISTS (SELECT 1 FROM KhoaDieuTri WHERE MaKhoa = 'K001')
INSERT INTO KhoaDieuTri (MaKhoa, TenKhoa)
VALUES ('K001', N'Khoa Nội tổng quát');

IF NOT EXISTS (SELECT 1 FROM BacSi WHERE MaBS = 'BS001')
INSERT INTO BacSi (MaBS, HoTen, ChuyenMon, MaKhoa)
VALUES ('BS001', N'Bác sĩ Nguyễn An',
        N'Nội tổng quát', 'K001');
GO



USE QuanLyHoSoBenhAn;
GO

IF NOT EXISTS (SELECT 1 FROM Thuoc WHERE MaThuoc = 'TH001')
INSERT INTO Thuoc(MaThuoc, TenThuoc, DonViTinh, HamLuong)
VALUES ('TH001', N'Thuốc mẫu A', N'Viên', N'Mẫu');

IF NOT EXISTS (SELECT 1 FROM Thuoc WHERE MaThuoc = 'TH002')
INSERT INTO Thuoc(MaThuoc, TenThuoc, DonViTinh, HamLuong)
VALUES ('TH002', N'Thuốc mẫu B', N'Viên', N'Mẫu');
GO


USE QuanLyHoSoBenhAn;
GO

SELECT MaBN, HoTen, NgaySinh, DiaChi, DienThoai, Anh3x4
FROM BenhNhan
WHERE MaBN = 'BN001';



USE QuanLyHoSoBenhAn;
GO

SELECT
    bn.MaBN,
    bn.HoTen,
    dk.MaDK,
    lp.MaLePhi,
    lp.SoTien,
    lp.NgayBatDau,
    lp.NgayHetHan,
    hs.MaHoSo,
    hs.TrangThai
FROM BenhNhan bn
LEFT JOIN PhieuDangKy dk
    ON bn.MaBN = dk.MaBN
LEFT JOIN LePhiNam lp
    ON bn.MaBN = lp.MaBN
LEFT JOIN HoSoBenhAn hs
    ON bn.MaBN = hs.MaBN
WHERE bn.MaBN = 'BN001';



USE QuanLyHoSoBenhAn;
GO

SELECT
    (SELECT COUNT(*) FROM PhieuDangKy
     WHERE MaBN = 'BN001') AS SoPhieuDangKy,

    (SELECT COUNT(*) FROM LePhiNam
     WHERE MaBN = 'BN001') AS SoLePhi,

    (SELECT COUNT(*) FROM HoSoBenhAn
     WHERE MaBN = 'BN001') AS SoHoSo;



USE QuanLyHoSoBenhAn;
GO

SELECT
    bn.MaBN,
    bn.HoTen AS TenBenhNhan,
    hs.MaHoSo,
    pk.MaPhieuKham,
    pk.NgayGioKham,
    bs.HoTen AS TenBacSi,
    k.TenKhoa,
    pk.TrieuChung,
    pk.NhietDo,
    pk.HuyetAp,
    pk.ChanDoan,
    pk.TrangThai
FROM BenhNhan bn
INNER JOIN HoSoBenhAn hs
    ON bn.MaBN = hs.MaBN
INNER JOIN PhieuKham pk
    ON hs.MaHoSo = pk.MaHoSo
INNER JOIN BacSi bs
    ON pk.MaBS = bs.MaBS
INNER JOIN KhoaDieuTri k
    ON pk.MaKhoa = k.MaKhoa
WHERE bn.MaBN = 'BN001'
ORDER BY pk.NgayGioKham DESC;



USE QuanLyHoSoBenhAn;
GO

SELECT
    bn.MaBN,
    bn.HoTen,
    pk.MaPhieuKham,
    t.MaToa,
    t.NgayKe,
    th.MaThuoc,
    th.TenThuoc,
    ct.SoLuong,
    ct.LieuDung,
    ct.CachDung
FROM ToaThuoc t
INNER JOIN PhieuKham pk
    ON t.MaPhieuKham = pk.MaPhieuKham
INNER JOIN HoSoBenhAn hs
    ON pk.MaHoSo = hs.MaHoSo
INNER JOIN BenhNhan bn
    ON hs.MaBN = bn.MaBN
INNER JOIN ChiTietToaThuoc ct
    ON t.MaToa = ct.MaToa
INNER JOIN Thuoc th
    ON ct.MaThuoc = th.MaThuoc
WHERE bn.MaBN = 'BN001'
ORDER BY t.NgayKe DESC, th.MaThuoc;



USE QuanLyHoSoBenhAn;
GO

SELECT
    l.MaLich,
    bn.MaBN,
    bn.HoTen,
    l.NgayHen,
    DATEPART(QUARTER, l.NgayHen) AS Quy,
    YEAR(l.NgayHen) AS Nam,
    l.TrangThai
FROM LichKhamDinhKy l
JOIN HoSoBenhAn hs ON l.MaHoSo = hs.MaHoSo
JOIN BenhNhan bn ON hs.MaBN = bn.MaBN
WHERE bn.MaBN = 'BN001'
ORDER BY l.NgayHen DESC;



USE QuanLyHoSoBenhAn;
GO

SELECT
    p.MaPhieuKham,
    p.TrangThai AS TrangThaiKham,
    t.MaThanhToan,
    t.SoTien,
    t.TrangThai AS TrangThaiThanhToan
FROM PhieuKham p
LEFT JOIN ThanhToanVienPhi t
    ON p.MaPhieuKham = t.MaPhieuKham
WHERE p.MaHoSo IN (
    SELECT MaHoSo
    FROM HoSoBenhAn
    WHERE MaBN = 'BN001'
);




USE QuanLyHoSoBenhAn;
GO

SELECT
    bn.MaBN,
    bn.HoTen,
    pk.MaPhieuKham,
    pk.TrangThai AS TrangThaiKham,
    tt.MaThanhToan,
    tt.SoTien,
    tt.NgayThanhToan,
    tt.TrangThai AS TrangThaiThanhToan
FROM PhieuKham pk
INNER JOIN HoSoBenhAn hs
    ON pk.MaHoSo = hs.MaHoSo
INNER JOIN BenhNhan bn
    ON hs.MaBN = bn.MaBN
LEFT JOIN ThanhToanVienPhi tt
    ON pk.MaPhieuKham = tt.MaPhieuKham
WHERE bn.MaBN = 'BN001'
ORDER BY pk.NgayGioKham DESC;

/*Kiem thu rollback*/

USE QuanLyHoSoBenhAn;
GO

DECLARE @MaPhieuKham VARCHAR(20);
DECLARE @MaToa VARCHAR(20) = 'TOA_ROLLBACK_TEST';

-- Lấy một phiếu khám đã tồn tại
SELECT TOP 1
    @MaPhieuKham = MaPhieuKham
FROM PhieuKham
ORDER BY NgayGioKham DESC;

IF @MaPhieuKham IS NULL
BEGIN
    PRINT N'Không có phiếu khám để kiểm thử.';
    RETURN;
END;

-- Đảm bảo không dùng mã toa đã tồn tại
IF EXISTS (
    SELECT 1
    FROM ToaThuoc
    WHERE MaToa = @MaToa
)
BEGIN
    PRINT N'Mã toa kiểm thử đã tồn tại. Không thực hiện.';
    RETURN;
END;

BEGIN TRY
    BEGIN TRANSACTION;

    -- 1. Thêm toa thuốc hợp lệ
    INSERT INTO ToaThuoc
        (MaToa, MaPhieuKham, NgayKe, LoiDan)
    VALUES
        (@MaToa, @MaPhieuKham,
         SYSDATETIME(), N'Kiểm thử rollback');

    PRINT N'Đã thêm toa thuốc tạm thời.';

    -- 2. Cố ý sử dụng mã thuốc không tồn tại
    INSERT INTO ChiTietToaThuoc
        (MaToa, MaThuoc, SoLuong, LieuDung, CachDung)
    VALUES
        (@MaToa, 'TH_KHONG_TON_TAI',
         1, N'Dữ liệu kiểm thử',
         N'Dữ liệu kiểm thử');

    COMMIT TRANSACTION;
    PRINT N'Transaction thành công.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    PRINT N'Đã phát hiện lỗi: ' + ERROR_MESSAGE();
    PRINT N'ROLLBACK được thực hiện thành công.';
END CATCH;

-- 3. Kiểm tra toa thuốc có bị lưu dở dang không
SELECT *
FROM ToaThuoc
WHERE MaToa = @MaToa;

SELECT *
FROM ChiTietToaThuoc
WHERE MaToa = @MaToa;
