
/* ============================================================
   LAB5 - QUAN LY CONG TY DU LICH VAN HOA VIET
   Database: QuanLyCongTyDuLich
   SQL Server
   ============================================================ */

USE master;
GO

IF DB_ID(N'QuanLyCongTyDuLich') IS NULL
    CREATE DATABASE QuanLyCongTyDuLich;
GO

USE QuanLyCongTyDuLich;
GO

/* ============================================================
   1. XOA CAC BANG CU (THEO THU TU KHOA NGOAI)
   ============================================================ */

IF OBJECT_ID('KhaoSat', 'U') IS NOT NULL DROP TABLE KhaoSat;
IF OBJECT_ID('ThanhToanDoan', 'U') IS NOT NULL DROP TABLE ThanhToanDoan;
IF OBJECT_ID('PhanCongHDV', 'U') IS NOT NULL DROP TABLE PhanCongHDV;
IF OBJECT_ID('DangKyLe', 'U') IS NOT NULL DROP TABLE DangKyLe;
IF OBJECT_ID('ThanhVienDoan', 'U') IS NOT NULL DROP TABLE ThanhVienDoan;
IF OBJECT_ID('DangKyDoan', 'U') IS NOT NULL DROP TABLE DangKyDoan;
IF OBJECT_ID('DoanKhach', 'U') IS NOT NULL DROP TABLE DoanKhach;
IF OBJECT_ID('ChuyenLe', 'U') IS NOT NULL DROP TABLE ChuyenLe;
IF OBJECT_ID('TourDiemThamQuan', 'U') IS NOT NULL DROP TABLE TourDiemThamQuan;
IF OBJECT_ID('TourPhuongTien', 'U') IS NOT NULL DROP TABLE TourPhuongTien;
IF OBJECT_ID('TourDiemDung', 'U') IS NOT NULL DROP TABLE TourDiemDung;
IF OBJECT_ID('HuongDanVien', 'U') IS NOT NULL DROP TABLE HuongDanVien;
IF OBJECT_ID('DiemBanVe', 'U') IS NOT NULL DROP TABLE DiemBanVe;
IF OBJECT_ID('DiemThamQuan', 'U') IS NOT NULL DROP TABLE DiemThamQuan;
IF OBJECT_ID('PhuongTien', 'U') IS NOT NULL DROP TABLE PhuongTien;
IF OBJECT_ID('Tour', 'U') IS NOT NULL DROP TABLE Tour;
GO

/* ============================================================
   2. TAO CAC BANG
   ============================================================ */

-- 1. TOUR
CREATE TABLE Tour
(
    MaTour VARCHAR(20) NOT NULL PRIMARY KEY,
    TenTour NVARCHAR(180) NOT NULL,
    SoNgay INT NOT NULL CHECK (SoNgay > 0),
    SoDem INT NOT NULL CHECK (SoDem >= 0),
    DonGiaKhach DECIMAL(18,2) NOT NULL CHECK (DonGiaKhach >= 0),
    MoTa NVARCHAR(1000) NULL,
    DangMoBan BIT NOT NULL DEFAULT 1
);
GO

-- 2. PHUONG TIEN
CREATE TABLE PhuongTien
(
    MaPT VARCHAR(20) NOT NULL PRIMARY KEY,
    TenPT NVARCHAR(120) NOT NULL UNIQUE,
    GhiChu NVARCHAR(300) NULL
);
GO

-- 3. DIEM THAM QUAN
CREATE TABLE DiemThamQuan
(
    MaDiemTQ VARCHAR(20) NOT NULL PRIMARY KEY,
    TenDiemTQ NVARCHAR(180) NOT NULL,
    DiaDiem NVARCHAR(250) NOT NULL,
    NoiDung NVARCHAR(1000) NULL,
    YNghia NVARCHAR(1000) NULL
);
GO

-- 4. DIEM BAN VE
CREATE TABLE DiemBanVe
(
    MaDiemBan VARCHAR(20) NOT NULL PRIMARY KEY,
    TenDiemBan NVARCHAR(150) NOT NULL,
    DiaChi NVARCHAR(250) NOT NULL,
    DienThoai VARCHAR(20) NULL
);
GO

-- 5. HUONG DAN VIEN
CREATE TABLE HuongDanVien
(
    MaHDV VARCHAR(20) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(120) NOT NULL,
    DienThoai VARCHAR(20) NULL,
    LuongCoBan DECIMAL(18,2) NOT NULL CHECK (LuongCoBan >= 0),
    DangLamViec BIT NOT NULL DEFAULT 1
);
GO

-- 6. TOUR DIEM DUNG
CREATE TABLE TourDiemDung
(
    MaTour VARCHAR(20) NOT NULL,
    ThuTu INT NOT NULL CHECK (ThuTu > 0),
    TenDiemDung NVARCHAR(180) NOT NULL,
    DoiPhuongTien BIT NOT NULL DEFAULT 0,
    CoNoiAn BIT NOT NULL DEFAULT 0,
    CoKhachSan BIT NOT NULL DEFAULT 0,
    HangSaoKhachSan INT NULL
        CHECK (HangSaoKhachSan BETWEEN 2 AND 5),
    GhiChu NVARCHAR(500) NULL,

    PRIMARY KEY (MaTour, ThuTu),

    CONSTRAINT FK_TDD_Tour
        FOREIGN KEY (MaTour) REFERENCES Tour(MaTour),

    CONSTRAINT CK_TDD_KhachSan
        CHECK (
            (CoKhachSan = 0 AND HangSaoKhachSan IS NULL)
            OR
            (CoKhachSan = 1 AND HangSaoKhachSan BETWEEN 2 AND 5)
        )
);
GO

-- 7. TOUR PHUONG TIEN
CREATE TABLE TourPhuongTien
(
    MaTour VARCHAR(20) NOT NULL,
    ThuTuChang INT NOT NULL CHECK (ThuTuChang > 0),
    MaPT VARCHAR(20) NOT NULL,
    GhiChu NVARCHAR(300) NULL,

    PRIMARY KEY (MaTour, ThuTuChang, MaPT),

    CONSTRAINT FK_TPT_Tour
        FOREIGN KEY (MaTour) REFERENCES Tour(MaTour),

    CONSTRAINT FK_TPT_PT
        FOREIGN KEY (MaPT) REFERENCES PhuongTien(MaPT)
);
GO

-- 8. TOUR DIEM THAM QUAN
CREATE TABLE TourDiemThamQuan
(
    MaTour VARCHAR(20) NOT NULL,
    MaDiemTQ VARCHAR(20) NOT NULL,
    ThuTu INT NOT NULL CHECK (ThuTu > 0),

    PRIMARY KEY (MaTour, MaDiemTQ),

    CONSTRAINT UQ_TDTQ UNIQUE (MaTour, ThuTu),

    CONSTRAINT FK_TDTQ_Tour
        FOREIGN KEY (MaTour) REFERENCES Tour(MaTour),

    CONSTRAINT FK_TDTQ_Diem
        FOREIGN KEY (MaDiemTQ) REFERENCES DiemThamQuan(MaDiemTQ)
);
GO

-- 9. CHUYEN KHACH LE
CREATE TABLE ChuyenLe
(
    MaChuyen VARCHAR(20) NOT NULL PRIMARY KEY,
    MaTour VARCHAR(20) NOT NULL,
    NgayDi DATE NOT NULL,
    NgayVe DATE NOT NULL,
    DiaDiemDon NVARCHAR(250) NOT NULL,
    TrangThai NVARCHAR(40) NOT NULL DEFAULT N'Mở đăng ký',

    CONSTRAINT CK_Chuyen_TrangThai
        CHECK (
            TrangThai IN (N'Mở đăng ký', N'Đóng đăng ký')
        ),

    CONSTRAINT FK_Chuyen_Tour
        FOREIGN KEY (MaTour) REFERENCES Tour(MaTour),

    CONSTRAINT CK_Chuyen_Ngay
        CHECK (NgayVe >= NgayDi)
);
GO

-- 10. DOAN KHACH
CREATE TABLE DoanKhach
(
    MaDoan VARCHAR(20) NOT NULL PRIMARY KEY,
    TenCoQuanDaiDien NVARCHAR(180) NOT NULL,
    DiaChi NVARCHAR(250) NOT NULL,
    DienThoai VARCHAR(20) NOT NULL,
    NguoiDaiDien NVARCHAR(120) NOT NULL
);
GO

-- 11. DANG KY DOAN
CREATE TABLE DangKyDoan
(
    SoDKDoan VARCHAR(20) NOT NULL PRIMARY KEY,
    MaDoan VARCHAR(20) NOT NULL,
    MaTour VARCHAR(20) NOT NULL,
    NgayDangKy DATETIME2 NOT NULL,
    NgayDi DATE NOT NULL,
    NgayKetThucDuKien DATE NOT NULL,
    SoNguoi INT NOT NULL CHECK (SoNguoi > 12),
    DiaDiemDon NVARCHAR(250) NOT NULL,
    MuaBaoHiem BIT NOT NULL DEFAULT 0,
    TienCoc DECIMAL(18,2) NOT NULL CHECK (TienCoc > 0),
    DaThanhToanCoc BIT NOT NULL,
    TongTienDuKien DECIMAL(18,2) NOT NULL
        CHECK (TongTienDuKien >= 0),
    TrangThai NVARCHAR(40) NOT NULL DEFAULT N'Đã đăng ký',

    CONSTRAINT CK_DKDoan_TrangThai
        CHECK (
            TrangThai IN (
                N'Đã đăng ký',
                N'Hủy - mất cọc',
                N'Đã hoàn tất thanh toán'
            )
        ),

    CONSTRAINT CK_DKDoan_Coc
        CHECK (TienCoc <= TongTienDuKien),

    CONSTRAINT FK_DKDoan_Doan
        FOREIGN KEY (MaDoan) REFERENCES DoanKhach(MaDoan),

    CONSTRAINT FK_DKDoan_Tour
        FOREIGN KEY (MaTour) REFERENCES Tour(MaTour),

    CONSTRAINT CK_DKDoan_Ngay
        CHECK (NgayKetThucDuKien >= NgayDi)
);
GO

-- 12. THANH VIEN DOAN
CREATE TABLE ThanhVienDoan
(
    SoDKDoan VARCHAR(20) NOT NULL,
    STT INT NOT NULL CHECK (STT > 0),
    HoTen NVARCHAR(120) NOT NULL,
    NgaySinh DATE NULL,
    SoGiayTo NVARCHAR(40) NULL,

    PRIMARY KEY (SoDKDoan, STT),

    CONSTRAINT FK_TVDoan_DK
        FOREIGN KEY (SoDKDoan)
        REFERENCES DangKyDoan(SoDKDoan)
);
GO

-- 13. DANG KY KHACH LE
CREATE TABLE DangKyLe
(
    SoDKLe VARCHAR(20) NOT NULL PRIMARY KEY,
    MaChuyen VARCHAR(20) NOT NULL,
    MaDiemBan VARCHAR(20) NOT NULL,
    NgayDangKy DATETIME2 NOT NULL,
    TenNguoiDangKy NVARCHAR(120) NOT NULL,
    DienThoai VARCHAR(20) NOT NULL,
    SoNguoi INT NOT NULL
        CHECK (SoNguoi BETWEEN 1 AND 11),
    ThanhTien DECIMAL(18,2) NOT NULL CHECK (ThanhTien >= 0),
    DaThanhToan BIT NOT NULL DEFAULT 1
        CHECK (DaThanhToan = 1),
    TrangThai NVARCHAR(40) NOT NULL DEFAULT N'Đã đăng ký',

    CONSTRAINT FK_DKLe_Chuyen
        FOREIGN KEY (MaChuyen) REFERENCES ChuyenLe(MaChuyen),

    CONSTRAINT FK_DKLe_DiemBan
        FOREIGN KEY (MaDiemBan) REFERENCES DiemBanVe(MaDiemBan)
);
GO

-- 14. PHAN CONG HUONG DAN VIEN
CREATE TABLE PhanCongHDV
(
    MaPC VARCHAR(20) NOT NULL PRIMARY KEY,
    MaHDV VARCHAR(20) NOT NULL,
    LoaiDoiTuong VARCHAR(10) NOT NULL
        CHECK (LoaiDoiTuong IN ('LE', 'DOAN')),
    MaChuyen VARCHAR(20) NULL,
    SoDKDoan VARCHAR(20) NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    ThuLaoTour DECIMAL(18,2) NOT NULL
        CHECK (ThuLaoTour >= 0),

    CONSTRAINT FK_PC_HDV
        FOREIGN KEY (MaHDV) REFERENCES HuongDanVien(MaHDV),

    CONSTRAINT FK_PC_Chuyen
        FOREIGN KEY (MaChuyen) REFERENCES ChuyenLe(MaChuyen),

    CONSTRAINT FK_PC_Doan
        FOREIGN KEY (SoDKDoan) REFERENCES DangKyDoan(SoDKDoan),

    CONSTRAINT CK_PC_Target
        CHECK (
            (
                LoaiDoiTuong = 'LE'
                AND MaChuyen IS NOT NULL
                AND SoDKDoan IS NULL
            )
            OR
            (
                LoaiDoiTuong = 'DOAN'
                AND SoDKDoan IS NOT NULL
                AND MaChuyen IS NULL
            )
        ),

    CONSTRAINT CK_PC_Ngay
        CHECK (NgayKetThuc >= NgayBatDau)
);
GO

-- Moi chuyen khach le toi da mot HDV
CREATE UNIQUE INDEX UX_PC_ChuyenLe
ON PhanCongHDV(MaChuyen)
WHERE MaChuyen IS NOT NULL;
GO

-- 15. THANH TOAN DOAN
CREATE TABLE ThanhToanDoan
(
    SoTT VARCHAR(20) NOT NULL PRIMARY KEY,
    SoDKDoan VARCHAR(20) NOT NULL,
    NgayThanhToan DATETIME2 NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL CHECK (SoTien > 0),
    GhiChu NVARCHAR(300) NULL,

    CONSTRAINT FK_TTDoan_DK
        FOREIGN KEY (SoDKDoan) REFERENCES DangKyDoan(SoDKDoan)
);
GO

-- 16. KHAO SAT
CREATE TABLE KhaoSat
(
    MaKhaoSat VARCHAR(20) NOT NULL PRIMARY KEY,
    LoaiKhach VARCHAR(10) NOT NULL
        CHECK (LoaiKhach IN ('LE', 'DOAN')),
    SoDKLe VARCHAR(20) NULL,
    SoDKDoan VARCHAR(20) NULL,
    NgayGui DATE NOT NULL,
    NgayPhanHoi DATE NULL,
    DiemDanhGia INT NULL
        CHECK (DiemDanhGia BETWEEN 1 AND 5),
    GopY NVARCHAR(1500) NULL,

    CONSTRAINT FK_KS_Le
        FOREIGN KEY (SoDKLe) REFERENCES DangKyLe(SoDKLe),

    CONSTRAINT FK_KS_Doan
        FOREIGN KEY (SoDKDoan) REFERENCES DangKyDoan(SoDKDoan),

    CONSTRAINT CK_KS_PhanHoi
        CHECK (
            NgayPhanHoi IS NULL
            OR NgayPhanHoi >= NgayGui
        ),

    CONSTRAINT CK_KS_Target
        CHECK (
            (
                LoaiKhach = 'LE'
                AND SoDKLe IS NOT NULL
                AND SoDKDoan IS NULL
            )
            OR
            (
                LoaiKhach = 'DOAN'
                AND SoDKDoan IS NOT NULL
                AND SoDKLe IS NULL
            )
        )
);
GO

CREATE UNIQUE INDEX UX_KS_Le
ON KhaoSat(SoDKLe)
WHERE SoDKLe IS NOT NULL;
GO

CREATE UNIQUE INDEX UX_KS_Doan
ON KhaoSat(SoDKDoan)
WHERE SoDKDoan IS NOT NULL;
GO

CREATE INDEX IX_PC_HDV_Ngay
ON PhanCongHDV(MaHDV, NgayBatDau, NgayKetThuc);
GO

/* ============================================================
   3. THEM DU LIEU MAU
   ============================================================ */

-- TOUR
INSERT INTO Tour
(MaTour, TenTour, SoNgay, SoDem, DonGiaKhach, MoTa, DangMoBan)
VALUES
(
    'T001', N'Miền Tây 3 ngày 2 đêm',
    3, 2, 2500000,
    N'TP.HCM - Mỹ Tho - Cần Thơ - TP.HCM', 1
),
(
    'T002', N'Đà Lạt 4 ngày 3 đêm',
    4, 3, 3200000,
    N'TP.HCM - Đà Lạt - TP.HCM', 1
),
(
    'T003', N'Hà Nội - Hạ Long 5 ngày 4 đêm',
    5, 4, 8900000,
    N'TP.HCM - Hà Nội - Hạ Long - TP.HCM', 1
);

-- PHUONG TIEN
INSERT INTO PhuongTien
(MaPT, TenPT, GhiChu)
VALUES
('PT01', N'Xe du lịch', NULL),
('PT02', N'Máy bay', NULL),
('PT03', N'Tàu hỏa', NULL),
('PT04', N'Tàu thủy', NULL);

-- DIEM BAN VE
INSERT INTO DiemBanVe
(MaDiemBan, TenDiemBan, DiaChi, DienThoai)
VALUES
(
    'DB01',
    N'Điểm bán Quận 1',
    N'12 Lê Lợi, Quận 1, TP.HCM',
    '0281000001'
),
(
    'DB02',
    N'Điểm bán Thủ Đức',
    N'5 Võ Văn Ngân, TP. Thủ Đức',
    '0281000002'
);

-- HUONG DAN VIEN
INSERT INTO HuongDanVien
(MaHDV, HoTen, DienThoai, LuongCoBan, DangLamViec)
VALUES
('HDV01', N'Nguyễn Minh Anh', '0903000001', 9000000, 1),
('HDV02', N'Trần Quốc Bình', '0903000002', 9500000, 1),
('HDV03', N'Lê Thu Cúc', '0903000003', 8500000, 1);

-- DIEM THAM QUAN
INSERT INTO DiemThamQuan
(MaDiemTQ, TenDiemTQ, DiaDiem, NoiDung, YNghia)
VALUES
(
    'DTQ01',
    N'Chợ nổi Cái Răng',
    N'Cần Thơ',
    N'Tham quan chợ trên sông',
    N'Nét văn hóa sông nước miền Tây'
),
(
    'DTQ02',
    N'Chùa Vĩnh Tràng',
    N'Mỹ Tho, Tiền Giang',
    N'Tham quan kiến trúc chùa',
    N'Di tích kiến trúc nghệ thuật cấp quốc gia'
),
(
    'DTQ03',
    N'Hồ Xuân Hương',
    N'Đà Lạt',
    N'Dạo quanh hồ trung tâm',
    N'Biểu tượng thành phố Đà Lạt'
),
(
    'DTQ04',
    N'Vịnh Hạ Long',
    N'Quảng Ninh',
    N'Du thuyền tham quan vịnh',
    N'Di sản thiên nhiên thế giới'
),
(
    'DTQ05',
    N'Văn Miếu - Quốc Tử Giám',
    N'Hà Nội',
    N'Tham quan di tích',
    N'Trường đại học đầu tiên của Việt Nam'
);

-- DIEM DUNG CUA TOUR
INSERT INTO TourDiemDung
(MaTour, ThuTu, TenDiemDung, DoiPhuongTien,
 CoNoiAn, CoKhachSan, HangSaoKhachSan, GhiChu)
VALUES
('T001', 1, N'Mỹ Tho', 0, 1, 0, NULL, NULL),
('T001', 2, N'Cần Thơ', 0, 1, 1, 3, NULL),
('T001', 3, N'TP.HCM', 0, 0, 0, NULL, N'Kết thúc tour'),
('T003', 1, N'Hà Nội', 1, 1, 1, 4, N'Đổi sang xe du lịch'),
('T003', 2, N'Hạ Long', 1, 1, 1, 5, N'Đi tàu thủy trên vịnh'),
('T003', 3, N'TP.HCM', 0, 0, 0, NULL, N'Kết thúc tour');

-- PHUONG TIEN THEO CHANG
INSERT INTO TourPhuongTien
(MaTour, ThuTuChang, MaPT, GhiChu)
VALUES
('T001', 1, 'PT01', NULL),
('T001', 2, 'PT01', NULL),
('T001', 3, 'PT01', NULL),
('T003', 1, 'PT02', N'TP.HCM - Hà Nội'),
('T003', 2, 'PT01', N'Hà Nội - Hạ Long'),
('T003', 2, 'PT04', N'Tham quan vịnh'),
('T003', 3, 'PT02', N'Hà Nội - TP.HCM');

-- DIEM THAM QUAN CUA TOUR
INSERT INTO TourDiemThamQuan
(MaTour, MaDiemTQ, ThuTu)
VALUES
('T001', 'DTQ02', 1),
('T001', 'DTQ01', 2),
('T002', 'DTQ03', 1),
('T003', 'DTQ05', 1),
('T003', 'DTQ04', 2);

-- CHUYEN KHACH LE
INSERT INTO ChuyenLe
(MaChuyen, MaTour, NgayDi, NgayVe, DiaDiemDon, TrangThai)
VALUES
(
    'CL001', 'T001',
    '20260905', '20260907',
    N'Nhà Văn hóa Thanh Niên, Quận 1',
    N'Đóng đăng ký'
),
(
    'CL002', 'T001',
    '20261115', '20261117',
    N'Nhà Văn hóa Thanh Niên, Quận 1',
    N'Mở đăng ký'
),
(
    'CL003', 'T002',
    '20261120', '20261123',
    N'Công viên 23/9, Quận 1',
    N'Mở đăng ký'
);

-- DANG KY KHACH LE
INSERT INTO DangKyLe
(SoDKLe, MaChuyen, MaDiemBan, NgayDangKy,
 TenNguoiDangKy, DienThoai, SoNguoi,
 ThanhTien, DaThanhToan, TrangThai)
VALUES
(
    'DKL001', 'CL001', 'DB01',
    '20260820 09:00',
    N'Phạm Văn Long', '0912000001',
    2, 5000000, 1, N'Đã đăng ký'
),
(
    'DKL002', 'CL002', 'DB02',
    '20261001 10:00',
    N'Võ Thị Mai', '0912000002',
    3, 7500000, 1, N'Đã đăng ký'
);

-- DOAN KHACH
INSERT INTO DoanKhach
(MaDoan, TenCoQuanDaiDien, DiaChi, DienThoai, NguoiDaiDien)
VALUES
(
    'DK01',
    N'Công ty CP Phần mềm Sao Việt',
    N'25 Nguyễn Thị Minh Khai, Quận 3, TP.HCM',
    '0283900001',
    N'Lê Văn Hải'
),
(
    'DK02',
    N'Gia đình ông Trần Văn Nam',
    N'8 Phan Xích Long, Phú Nhuận, TP.HCM',
    '0909111222',
    N'Trần Văn Nam'
);

-- DANG KY DOAN
INSERT INTO DangKyDoan
(
    SoDKDoan, MaDoan, MaTour, NgayDangKy,
    NgayDi, NgayKetThucDuKien, SoNguoi,
    DiaDiemDon, MuaBaoHiem, TienCoc,
    DaThanhToanCoc, TongTienDuKien, TrangThai
)
VALUES
(
    'DD001', 'DK01', 'T001',
    '20260801 08:30',
    '20260910', '20260912',
    20,
    N'25 Nguyễn Thị Minh Khai, Quận 3',
    0, 10000000, 1, 50000000,
    N'Đã đăng ký'
),
(
    'DD002', 'DK02', 'T002',
    '20260925 14:00',
    '20261210', '20261213',
    15,
    N'8 Phan Xích Long, Phú Nhuận',
    0, 12000000, 1, 48000000,
    N'Đã đăng ký'
);

-- PHAN CONG HUONG DAN VIEN
INSERT INTO PhanCongHDV
(
    MaPC, MaHDV, LoaiDoiTuong, MaChuyen,
    SoDKDoan, NgayBatDau, NgayKetThuc, ThuLaoTour
)
VALUES
(
    'PC001', 'HDV01', 'LE', 'CL001', NULL,
    '20260905', '20260907', 1500000
),
(
    'PC002', 'HDV02', 'DOAN', NULL, 'DD001',
    '20260910', '20260912', 2000000
),
(
    'PC003', 'HDV03', 'DOAN', NULL, 'DD001',
    '20260910', '20260912', 2000000
);

-- KHAO SAT
INSERT INTO KhaoSat
(
    MaKhaoSat, LoaiKhach, SoDKLe, SoDKDoan,
    NgayGui, NgayPhanHoi, DiemDanhGia, GopY
)
VALUES
(
    'KS001', 'LE', 'DKL001', NULL,
    '20260908', '20260910',
    5, N'Hướng dẫn viên nhiệt tình'
);
GO

PRINT N'Da tao CSDL QuanLyCongTyDuLich va them du lieu mau.';
GO

/* ============================================================
   4. KIEM TRA KET QUA
   ============================================================ */

SELECT COUNT(*) AS SoBang
FROM sys.tables;

SELECT * FROM Tour;
SELECT * FROM PhuongTien;
SELECT * FROM HuongDanVien;
SELECT * FROM ChuyenLe;
SELECT * FROM DangKyLe;
SELECT * FROM DangKyDoan;
SELECT * FROM PhanCongHDV;
SELECT * FROM KhaoSat;
GO

USE QuanLyCongTyDuLich;
GO

SELECT *
FROM PhuongTien
WHERE MaPT = 'PT05';

USE QuanLyCongTyDuLich;
GO

SELECT *
FROM ChuyenLe
WHERE MaChuyen = 'CL004';

USE QuanLyCongTyDuLich;
GO

SELECT *
FROM DangKyLe
WHERE SoDKLe = 'DKL003';

USE QuanLyCongTyDuLich;
GO

SELECT *
FROM DoanKhach
WHERE MaDoan = 'D004';

SELECT *
FROM DangKyDoan
WHERE SoDKDoan = 'DKD004';

SELECT *
FROM ThanhVienDoan
WHERE SoDKDoan = 'DKD004';

USE QuanLyCongTyDuLich;
GO

SELECT * FROM ThanhToanDoan;
SELECT * FROM KhaoSat;

USE QuanLyCongTyDuLich;
GO

SELECT
    h.MaHDV,
    h.HoTen,
    h.LuongCoBan,
    COUNT(p.MaPC) AS SoTour,
    ISNULL(SUM(p.ThuLaoTour), 0) AS LuongTheoTour,
    h.LuongCoBan + ISNULL(SUM(p.ThuLaoTour), 0) AS TongLuong
FROM HuongDanVien h
LEFT JOIN PhanCongHDV p
    ON h.MaHDV = p.MaHDV
    AND MONTH(p.NgayKetThuc) = 10
    AND YEAR(p.NgayKetThuc) = 2026
WHERE h.DangLamViec = 1
GROUP BY h.MaHDV, h.HoTen, h.LuongCoBan;