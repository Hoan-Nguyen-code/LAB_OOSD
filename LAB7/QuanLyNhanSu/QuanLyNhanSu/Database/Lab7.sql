
/* ==========================================================
   LAB7 - QUAN LY NHAN SU CONG TY SON DONG A
   SQL SERVER - DATABASE: QuanLyNhanSuDongA
   ========================================================== */

IF DB_ID(N'QuanLyNhanSuDongA') IS NULL
    CREATE DATABASE QuanLyNhanSuDongA;
GO

USE QuanLyNhanSuDongA;
GO

/* ==========================================================
   1. XOA BANG CU THEO THU TU PHU THUOC
   ========================================================== */

DROP TABLE IF EXISTS PhieuThuNopPhat;
DROP TABLE IF EXISTS PhieuChiKhenThuong;
DROP TABLE IF EXISTS ChiTietBangLuong;
DROP TABLE IF EXISTS ChiTietPhuTroi;
DROP TABLE IF EXISTS ChiTietChamCong;
DROP TABLE IF EXISTS ChiTietPhanCong;

DROP TABLE IF EXISTS QuyetDinhKyLuat;
DROP TABLE IF EXISTS QuyetDinhKhenThuong;
DROP TABLE IF EXISTS BangLuong;
DROP TABLE IF EXISTS PhieuPhuTroi;
DROP TABLE IF EXISTS BangChamCong;
DROP TABLE IF EXISTS KeHoachPhanCong;
DROP TABLE IF EXISTS CaLamViec;

DROP TABLE IF EXISTS HoSoBaoHiem;
DROP TABLE IF EXISTS QuyetDinhLuong;
DROP TABLE IF EXISTS QuyetDinhChucVu;
DROP TABLE IF EXISTS ChucVu;
DROP TABLE IF EXISTS HopDongLaoDong;
DROP TABLE IF EXISTS QuyetDinhTuyenDung;
DROP TABLE IF EXISTS PhongBan;
DROP TABLE IF EXISTS NhanVien;
GO

/* ==========================================================
   2. NHOM NHAN SU - TUYEN DUNG - HOP DONG
   ========================================================== */

-- Bang 1: NhanVien
CREATE TABLE NhanVien (
    MaNV VARCHAR(20) NOT NULL PRIMARY KEY,
    HoTen NVARCHAR(120) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(20) NULL,
    DiaChi NVARCHAR(250) NULL,
    DienThoai VARCHAR(20) NULL,
    TrangThaiLamViec NVARCHAR(30) NOT NULL
        DEFAULT N'Thử việc',

    CONSTRAINT CK_NV_TrangThai
        CHECK (TrangThaiLamViec IN (
            N'Thử việc', N'Chính thức', N'Nghỉ việc'
        ))
);

-- Bang 2: PhongBan
CREATE TABLE PhongBan (
    MaPhongBan VARCHAR(20) NOT NULL PRIMARY KEY,
    TenPhongBan NVARCHAR(150) NOT NULL UNIQUE,
    MoTa NVARCHAR(500) NULL
);

-- Bang 3: QuyetDinhTuyenDung
CREATE TABLE QuyetDinhTuyenDung (
    MaQDTD VARCHAR(20) NOT NULL PRIMARY KEY,
    MaNV VARCHAR(20) NOT NULL,
    MaPhongBan VARCHAR(20) NOT NULL,
    NgayQuyetDinh DATE NOT NULL,
    NgayBatDauThuViec DATE NOT NULL,
    NgayKetThucThuViec DATE NOT NULL,
    LuongThuViec DECIMAL(18,2) NOT NULL,
    NoiDungDieuKhoan NVARCHAR(MAX) NOT NULL,

    CONSTRAINT FK_QDTD_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT FK_QDTD_PhongBan
        FOREIGN KEY (MaPhongBan)
        REFERENCES PhongBan(MaPhongBan),

    CONSTRAINT CK_QDTD_ThoiGian
        CHECK (NgayKetThucThuViec >= NgayBatDauThuViec),

    CONSTRAINT CK_QDTD_Luong
        CHECK (LuongThuViec >= 0),

    CONSTRAINT UQ_QDTD_MaNV_MaQDTD
        UNIQUE (MaNV, MaQDTD)
);

-- Bang 4: HopDongLaoDong
CREATE TABLE HopDongLaoDong (
    MaHopDong VARCHAR(20) NOT NULL PRIMARY KEY,
    MaNV VARCHAR(20) NOT NULL,
    MaQDTD VARCHAR(20) NOT NULL UNIQUE,
    LoaiHopDong NVARCHAR(20) NOT NULL,
    NgayKy DATE NOT NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NULL,
    NoiDungHopDong NVARCHAR(MAX) NULL,
    TrangThai NVARCHAR(30) NOT NULL
        DEFAULT N'Có hiệu lực',

    CONSTRAINT FK_HD_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT FK_HD_QuyetDinhTuyenDung
        FOREIGN KEY (MaNV, MaQDTD)
        REFERENCES QuyetDinhTuyenDung(MaNV, MaQDTD),

    CONSTRAINT CK_HD_Loai
        CHECK (LoaiHopDong IN (
            N'Ngắn hạn', N'Dài hạn'
        )),

    CONSTRAINT CK_HD_Ngay
        CHECK (
            NgayKetThuc IS NULL
            OR NgayKetThuc >= NgayBatDau
        ),

    CONSTRAINT CK_HD_TrangThai
        CHECK (TrangThai IN (
            N'Có hiệu lực', N'Hết hạn', N'Chấm dứt'
        ))
);
GO

/* ==========================================================
   3. NHOM CHUC VU - LUONG - BAO HIEM
   ========================================================== */

-- Bang 5: ChucVu
CREATE TABLE ChucVu (
    MaChucVu VARCHAR(20) NOT NULL PRIMARY KEY,
    TenChucVu NVARCHAR(120) NOT NULL UNIQUE,
    MoTa NVARCHAR(500) NULL
);

-- Bang 6: QuyetDinhChucVu
CREATE TABLE QuyetDinhChucVu (
    MaQDCV VARCHAR(20) NOT NULL PRIMARY KEY,
    MaNV VARCHAR(20) NOT NULL,
    MaChucVu VARCHAR(20) NOT NULL,
    NgayQuyetDinh DATE NOT NULL,
    NgayHieuLuc DATE NOT NULL,
    PhuCapChucVu DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT FK_QDCV_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT FK_QDCV_ChucVu
        FOREIGN KEY (MaChucVu) REFERENCES ChucVu(MaChucVu),

    CONSTRAINT CK_QDCV_PhuCap
        CHECK (PhuCapChucVu >= 0)
);

-- Bang 7: QuyetDinhLuong
CREATE TABLE QuyetDinhLuong (
    MaQDL VARCHAR(20) NOT NULL PRIMARY KEY,
    MaNV VARCHAR(20) NOT NULL,
    NgayQuyetDinh DATE NOT NULL,
    NgayHieuLuc DATE NOT NULL,
    LuongCoBan DECIMAL(18,2) NOT NULL,
    PhuCapLuong DECIMAL(18,2) NOT NULL DEFAULT 0,
    PhuCapDocHai DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT FK_QDL_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT CK_QDL_SoTien
        CHECK (
            LuongCoBan >= 0
            AND PhuCapLuong >= 0
            AND PhuCapDocHai >= 0
        )
);

-- Bang 8: HoSoBaoHiem
CREATE TABLE HoSoBaoHiem (
    MaBaoHiem VARCHAR(20) NOT NULL PRIMARY KEY,
    MaHopDong VARCHAR(20) NOT NULL UNIQUE,
    SoBHXH VARCHAR(30) NULL,
    SoBHYT VARCHAR(30) NULL,
    MucDongBHXH DECIMAL(18,2) NOT NULL DEFAULT 0,
    MucDongBHYT DECIMAL(18,2) NOT NULL DEFAULT 0,
    NgayThamGia DATE NOT NULL,

    CONSTRAINT FK_BH_HopDong
        FOREIGN KEY (MaHopDong)
        REFERENCES HopDongLaoDong(MaHopDong),

    CONSTRAINT CK_BH_MucDong
        CHECK (
            MucDongBHXH >= 0
            AND MucDongBHYT >= 0
        )
);
GO

/* ==========================================================
   4. NHOM PHAN CONG - CHAM CONG - PHU TROI
   ========================================================== */

-- Bang 9: CaLamViec
CREATE TABLE CaLamViec (
    MaCa VARCHAR(20) NOT NULL PRIMARY KEY,
    TenCa NVARCHAR(100) NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL
);

-- Bang 10: KeHoachPhanCong
CREATE TABLE KeHoachPhanCong (
    MaKeHoach VARCHAR(20) NOT NULL PRIMARY KEY,
    MaPhongBan VARCHAR(20) NOT NULL,
    Tuan INT NOT NULL,
    Nam INT NOT NULL,
    NgayLap DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),

    CONSTRAINT FK_KH_PhongBan
        FOREIGN KEY (MaPhongBan)
        REFERENCES PhongBan(MaPhongBan),

    CONSTRAINT CK_KH_TuanNam
        CHECK (
            Tuan BETWEEN 1 AND 53
            AND Nam BETWEEN 2000 AND 2100
        ),

    CONSTRAINT UQ_KH_PhongBan_Tuan
        UNIQUE (MaPhongBan, Tuan, Nam)
);

-- Bang 11: ChiTietPhanCong
CREATE TABLE ChiTietPhanCong (
    MaChiTietPhanCong VARCHAR(20) NOT NULL PRIMARY KEY,
    MaKeHoach VARCHAR(20) NOT NULL,
    MaNV VARCHAR(20) NOT NULL,
    MaCa VARCHAR(20) NOT NULL,
    NgayLamViec DATE NOT NULL,

    CONSTRAINT FK_CTPC_KeHoach
        FOREIGN KEY (MaKeHoach)
        REFERENCES KeHoachPhanCong(MaKeHoach),

    CONSTRAINT FK_CTPC_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT FK_CTPC_Ca
        FOREIGN KEY (MaCa) REFERENCES CaLamViec(MaCa),

    CONSTRAINT UQ_CTPC_NhanVien_Ngay_Ca
        UNIQUE (MaNV, NgayLamViec, MaCa)
);

-- Bang 12: BangChamCong
CREATE TABLE BangChamCong (
    MaBangChamCong VARCHAR(20) NOT NULL PRIMARY KEY,
    Thang INT NOT NULL,
    Nam INT NOT NULL,
    NgayLap DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),

    CONSTRAINT CK_BCC_ThangNam
        CHECK (
            Thang BETWEEN 1 AND 12
            AND Nam BETWEEN 2000 AND 2100
        ),

    CONSTRAINT UQ_BCC_ThangNam
        UNIQUE (Thang, Nam)
);

-- Bang 13: ChiTietChamCong
CREATE TABLE ChiTietChamCong (
    MaChiTietChamCong VARCHAR(20) NOT NULL PRIMARY KEY,
    MaBangChamCong VARCHAR(20) NOT NULL,
    MaNV VARCHAR(20) NOT NULL,
    NgayLam DATE NOT NULL,
    SoGioLam DECIMAL(6,2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Có mặt',

    CONSTRAINT FK_CTCC_Bang
        FOREIGN KEY (MaBangChamCong)
        REFERENCES BangChamCong(MaBangChamCong),

    CONSTRAINT FK_CTCC_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT CK_CTCC_Gio
        CHECK (SoGioLam BETWEEN 0 AND 24),

    CONSTRAINT CK_CTCC_TrangThai
        CHECK (TrangThai IN (
            N'Có mặt', N'Vắng mặt', N'Nghỉ phép'
        )),

    CONSTRAINT UQ_CTCC_NhanVien_Ngay
        UNIQUE (MaNV, NgayLam)
);

-- Bang 14: PhieuPhuTroi
CREATE TABLE PhieuPhuTroi (
    MaPhieuPhuTroi VARCHAR(20) NOT NULL PRIMARY KEY,
    NgayPhuTroi DATE NOT NULL,
    HinhThucPhuTroi NVARCHAR(30) NOT NULL,
    SoGioPhuTroi DECIMAL(6,2) NOT NULL,

    CONSTRAINT CK_PPT_HinhThuc
        CHECK (HinhThucPhuTroi IN (
            N'Tăng ca', N'Ngày lễ', N'Ngày nghỉ'
        )),

    CONSTRAINT CK_PPT_Gio
        CHECK (SoGioPhuTroi > 0 AND SoGioPhuTroi <= 24)
);

-- Bang 15: ChiTietPhuTroi
CREATE TABLE ChiTietPhuTroi (
    MaChiTietPhuTroi VARCHAR(20) NOT NULL PRIMARY KEY,
    MaPhieuPhuTroi VARCHAR(20) NOT NULL,
    MaNV VARCHAR(20) NOT NULL,
    SoGioThamGia DECIMAL(6,2) NOT NULL,

    CONSTRAINT FK_CTPT_Phieu
        FOREIGN KEY (MaPhieuPhuTroi)
        REFERENCES PhieuPhuTroi(MaPhieuPhuTroi),

    CONSTRAINT FK_CTPT_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT CK_CTPT_Gio
        CHECK (SoGioThamGia > 0 AND SoGioThamGia <= 24),

    CONSTRAINT UQ_CTPT_Phieu_NhanVien
        UNIQUE (MaPhieuPhuTroi, MaNV)
);
GO

/* ==========================================================
   5. NHOM BANG LUONG
   ========================================================== */

-- Bang 16: BangLuong
CREATE TABLE BangLuong (
    MaBangLuong VARCHAR(20) NOT NULL PRIMARY KEY,
    Thang INT NOT NULL,
    Nam INT NOT NULL,
    NgayLap DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    TongLuong DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT CK_BL_ThangNam
        CHECK (
            Thang BETWEEN 1 AND 12
            AND Nam BETWEEN 2000 AND 2100
        ),

    CONSTRAINT CK_BL_TongLuong
        CHECK (TongLuong >= 0),

    CONSTRAINT UQ_BL_ThangNam
        UNIQUE (Thang, Nam)
);

-- Bang 17: ChiTietBangLuong
CREATE TABLE ChiTietBangLuong (
    MaChiTietLuong VARCHAR(20) NOT NULL PRIMARY KEY,
    MaBangLuong VARCHAR(20) NOT NULL,
    MaNV VARCHAR(20) NOT NULL,
    LuongCoBan DECIMAL(18,2) NOT NULL DEFAULT 0,
    TongPhuCap DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienPhuTroi DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienThuong DECIMAL(18,2) NOT NULL DEFAULT 0,
    TienPhat DECIMAL(18,2) NOT NULL DEFAULT 0,
    KhoanTrichBaoHiem DECIMAL(18,2) NOT NULL DEFAULT 0,
    LuongThucLanh DECIMAL(18,2) NOT NULL DEFAULT 0,

    CONSTRAINT FK_CTBL_Bang
        FOREIGN KEY (MaBangLuong)
        REFERENCES BangLuong(MaBangLuong),

    CONSTRAINT FK_CTBL_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT UQ_CTBL_Bang_NhanVien
        UNIQUE (MaBangLuong, MaNV),

    CONSTRAINT CK_CTBL_SoTien
        CHECK (
            LuongCoBan >= 0
            AND TongPhuCap >= 0
            AND TienPhuTroi >= 0
            AND TienThuong >= 0
            AND TienPhat >= 0
            AND KhoanTrichBaoHiem >= 0
            AND LuongThucLanh >= 0
        )
);
GO

/* ==========================================================
   6. NHOM KHEN THUONG - KY LUAT
   ========================================================== */

-- Bang 18: QuyetDinhKhenThuong
CREATE TABLE QuyetDinhKhenThuong (
    MaQDKT VARCHAR(20) NOT NULL PRIMARY KEY,
    MaNV VARCHAR(20) NULL,
    MaPhongBan VARCHAR(20) NULL,
    NgayQuyetDinh DATE NOT NULL,
    LyDo NVARCHAR(1000) NOT NULL,
    SoTienThuong DECIMAL(18,2) NOT NULL,
    NguoiKy NVARCHAR(120) NOT NULL,

    CONSTRAINT FK_QDKT_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT FK_QDKT_PhongBan
        FOREIGN KEY (MaPhongBan)
        REFERENCES PhongBan(MaPhongBan),

    CONSTRAINT CK_QDKT_DoiTuong
        CHECK (
            (MaNV IS NOT NULL AND MaPhongBan IS NULL)
            OR
            (MaNV IS NULL AND MaPhongBan IS NOT NULL)
        ),

    CONSTRAINT CK_QDKT_Tien
        CHECK (SoTienThuong >= 0)
);

-- Bang 19: PhieuChiKhenThuong
CREATE TABLE PhieuChiKhenThuong (
    MaPhieuChi VARCHAR(20) NOT NULL PRIMARY KEY,
    MaQDKT VARCHAR(20) NOT NULL UNIQUE,
    NgayChi DATE NOT NULL,
    SoTienChi DECIMAL(18,2) NOT NULL,
    HinhThucChi NVARCHAR(30) NOT NULL,

    CONSTRAINT FK_PCKT_QuyetDinh
        FOREIGN KEY (MaQDKT)
        REFERENCES QuyetDinhKhenThuong(MaQDKT),

    CONSTRAINT CK_PCKT_Tien
        CHECK (SoTienChi >= 0),

    CONSTRAINT CK_PCKT_HinhThuc
        CHECK (HinhThucChi IN (
            N'Tiền mặt', N'Chuyển khoản', N'Qua lương'
        ))
);

-- Bang 20: QuyetDinhKyLuat
CREATE TABLE QuyetDinhKyLuat (
    MaQDKL VARCHAR(20) NOT NULL PRIMARY KEY,
    MaNV VARCHAR(20) NULL,
    MaPhongBan VARCHAR(20) NULL,
    NgayQuyetDinh DATE NOT NULL,
    LyDo NVARCHAR(1000) NOT NULL,
    SoTienPhat DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_QDKL_NhanVien
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),

    CONSTRAINT FK_QDKL_PhongBan
        FOREIGN KEY (MaPhongBan)
        REFERENCES PhongBan(MaPhongBan),

    CONSTRAINT CK_QDKL_DoiTuong
        CHECK (
            (MaNV IS NOT NULL AND MaPhongBan IS NULL)
            OR
            (MaNV IS NULL AND MaPhongBan IS NOT NULL)
        ),

    CONSTRAINT CK_QDKL_Tien
        CHECK (SoTienPhat >= 0)
);

-- Bang 21: PhieuThuNopPhat
CREATE TABLE PhieuThuNopPhat (
    MaPhieuThu VARCHAR(20) NOT NULL PRIMARY KEY,
    MaQDKL VARCHAR(20) NOT NULL UNIQUE,
    NgayThu DATE NOT NULL,
    SoTienThu DECIMAL(18,2) NOT NULL,
    HinhThucThu NVARCHAR(30) NOT NULL,

    CONSTRAINT FK_PTNP_QuyetDinh
        FOREIGN KEY (MaQDKL)
        REFERENCES QuyetDinhKyLuat(MaQDKL),

    CONSTRAINT CK_PTNP_Tien
        CHECK (SoTienThu >= 0),

    CONSTRAINT CK_PTNP_HinhThuc
        CHECK (HinhThucThu IN (
            N'Tiền mặt', N'Chuyển khoản', N'Trừ lương'
        ))
);
GO

/* ==========================================================
   7. INDEX PHUC VU TRA CUU
   ========================================================== */

CREATE INDEX IX_QDTD_NhanVien
    ON QuyetDinhTuyenDung(MaNV, NgayBatDauThuViec);

CREATE INDEX IX_HD_NhanVien
    ON HopDongLaoDong(MaNV, NgayBatDau);

CREATE INDEX IX_QDL_NhanVien_Ngay
    ON QuyetDinhLuong(MaNV, NgayHieuLuc DESC);

CREATE INDEX IX_QDCV_NhanVien_Ngay
    ON QuyetDinhChucVu(MaNV, NgayHieuLuc DESC);

CREATE INDEX IX_CTCC_Ngay
    ON ChiTietChamCong(NgayLam, MaNV);

CREATE INDEX IX_PPT_Ngay
    ON PhieuPhuTroi(NgayPhuTroi);
GO

/* ==========================================================
   8. DU LIEU MAU
   PHUC VU CHAY WINFORMS VA TEST CASE
   ========================================================== */

-- Phong ban
INSERT INTO PhongBan(MaPhongBan, TenPhongBan, MoTa)
VALUES
('PB01', N'Phòng tài chính kế toán',
 N'Quản lý tài chính và kế toán'),
('PB02', N'Phòng kỹ thuật',
 N'Phụ trách kỹ thuật sản xuất'),
('PB03', N'Phòng sản xuất',
 N'Tổ chức hoạt động sản xuất');

-- Chuc vu
INSERT INTO ChucVu(MaChucVu, TenChucVu, MoTa)
VALUES
('CV01', N'Nhân viên', N'Nhân viên chuyên môn'),
('CV02', N'Trưởng phòng', N'Quản lý phòng ban'),
('CV03', N'Quản đốc', N'Quản lý phân xưởng');

-- Ca lam viec
INSERT INTO CaLamViec(MaCa, TenCa, GioBatDau, GioKetThuc)
VALUES
('CA01', N'Ca sáng', '07:00', '11:00'),
('CA02', N'Ca chiều', '13:00', '17:00'),
('CA03', N'Ca tối', '18:00', '22:00');

-- Nhan vien mau
INSERT INTO NhanVien
(MaNV, HoTen, NgaySinh, GioiTinh,
 DiaChi, DienThoai, TrangThaiLamViec)
VALUES
('NV001', N'Nguyễn Văn An', '2002-05-15', N'Nam',
 N'TP.HCM', '0901234567', N'Chính thức'),
('NV002', N'Trần Thị Bình', '2001-08-20', N'Nữ',
 N'TP.HCM', '0912345678', N'Chính thức'),
('NV003', N'Lê Văn Cường', '2003-02-10', N'Nam',
 N'Bình Dương', '0987654321', N'Thử việc');

-- Quyet dinh tuyen dung
INSERT INTO QuyetDinhTuyenDung
(MaQDTD, MaNV, MaPhongBan, NgayQuyetDinh,
 NgayBatDauThuViec, NgayKetThucThuViec,
 LuongThuViec, NoiDungDieuKhoan)
VALUES
('TD001', 'NV001', 'PB02', '2026-06-01',
 '2026-06-01', '2026-07-31', 6000000,
 N'Thử việc tại phòng kỹ thuật theo điều khoản hai bên.'),
('TD002', 'NV002', 'PB01', '2026-05-01',
 '2026-05-01', '2026-06-30', 6500000,
 N'Thử việc tại phòng tài chính kế toán.'),
('TD003', 'NV003', 'PB03', '2026-10-01',
 '2026-10-01', '2026-11-30', 5500000,
 N'Thử việc tại phòng sản xuất.');

-- Hop dong chinh thuc
INSERT INTO HopDongLaoDong
(MaHopDong, MaNV, MaQDTD, LoaiHopDong,
 NgayKy, NgayBatDau, NgayKetThuc,
 NoiDungHopDong, TrangThai)
VALUES
('HD001', 'NV001', 'TD001', N'Dài hạn',
 '2026-08-01', '2026-08-01', NULL,
 N'Hợp đồng lao động dài hạn.', N'Có hiệu lực'),
('HD002', 'NV002', 'TD002', N'Ngắn hạn',
 '2026-07-01', '2026-07-01', '2027-06-30',
 N'Hợp đồng lao động ngắn hạn.', N'Có hiệu lực');

-- Quyet dinh luong
INSERT INTO QuyetDinhLuong
(MaQDL, MaNV, NgayQuyetDinh, NgayHieuLuc,
 LuongCoBan, PhuCapLuong, PhuCapDocHai)
VALUES
('QL001', 'NV001', '2026-08-01', '2026-08-01',
 10000000, 1000000, 500000),
('QL002', 'NV002', '2026-07-01', '2026-07-01',
 11000000, 1000000, 0);

-- Quyet dinh chuc vu
INSERT INTO QuyetDinhChucVu
(MaQDCV, MaNV, MaChucVu,
 NgayQuyetDinh, NgayHieuLuc, PhuCapChucVu)
VALUES
('QCV001', 'NV001', 'CV01',
 '2026-08-01', '2026-08-01', 0),
('QCV002', 'NV002', 'CV02',
 '2026-07-01', '2026-07-01', 2000000);

-- Ho so bao hiem cho hop dong dai han
INSERT INTO HoSoBaoHiem
(MaBaoHiem, MaHopDong, SoBHXH, SoBHYT,
 MucDongBHXH, MucDongBHYT, NgayThamGia)
VALUES
('BH001', 'HD001', 'BHXH001', 'BHYT001',
 800000, 150000, '2026-08-01');

-- Quyết định khen thưởng cá nhân
INSERT INTO QuyetDinhKhenThuong
(MaQDKT, MaNV, MaPhongBan, NgayQuyetDinh,
 LyDo, SoTienThuong, NguoiKy)
VALUES
('KT001', 'NV001', NULL, '2026-10-15',
 N'Hoàn thành tốt nhiệm vụ tháng.', 1000000,
 N'Giám đốc công ty');

-- Quyết định khen thưởng tập thể
INSERT INTO QuyetDinhKhenThuong
(MaQDKT, MaNV, MaPhongBan, NgayQuyetDinh,
 LyDo, SoTienThuong, NguoiKy)
VALUES
('KT002', NULL, 'PB02', '2026-10-20',
 N'Tập thể hoàn thành tốt kế hoạch sản xuất.',
 3000000, N'Giám đốc công ty');

GO

/* ==========================================================
   9. KIEM TRA SO LUONG BANG
   ========================================================== */

SELECT COUNT(*) AS SoLuongBang
FROM sys.tables
WHERE is_ms_shipped = 0;

-- Kiem tra du lieu mau
SELECT * FROM PhongBan;
SELECT * FROM NhanVien;
SELECT * FROM QuyetDinhTuyenDung;
SELECT * FROM HopDongLaoDong;
SELECT * FROM QuyetDinhLuong;
SELECT * FROM QuyetDinhChucVu;
SELECT * FROM HoSoBaoHiem;
SELECT * FROM QuyetDinhKhenThuong;


USE QuanLyNhanSuDongA;
GO

IF COL_LENGTH('dbo.BangLuong', 'GioCongChuan') IS NULL
BEGIN
    ALTER TABLE dbo.BangLuong
    ADD GioCongChuan DECIMAL(8,2) NULL;
END;
GO

