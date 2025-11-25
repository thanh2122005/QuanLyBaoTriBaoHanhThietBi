

IF DB_ID(N'qlbttb') IS NOT NULL
    DROP DATABASE qlbttb1;
GO
CREATE DATABASE qlbttb1;
GO
USE qlbttb1;
GO

/* ===================== 1) NHÂN VIÊN ===================== */
CREATE TABLE dbo.NhanVien
(
    MaNV        INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(200) NOT NULL,
    SoDienThoai NVARCHAR(20)  NULL,
    Email       NVARCHAR(100) NULL UNIQUE,
    TrangThai   NVARCHAR(20)  NOT NULL
        CONSTRAINT DF_NhanVien_TrangThai DEFAULT (N'Hoạt động'),
    CONSTRAINT CK_NhanVien_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Nghỉ việc'))
);
GO

/* ===================== 2) TÀI KHOẢN ===================== */
CREATE TABLE dbo.TaiKhoan
(
    MaTaiKhoan  INT IDENTITY(1,1) PRIMARY KEY,             
    TenDangNhap NVARCHAR(100) NOT NULL UNIQUE,             
    MatKhauHash NVARCHAR(500) NOT NULL,                    
    Role        NVARCHAR(20)  NOT NULL                     
        CONSTRAINT CK_TaiKhoan_Role CHECK (Role IN (N'QuanLy', N'NhanVien')),
    FullName    NVARCHAR(100) NULL,                        
    Email       NVARCHAR(100) NULL UNIQUE,                 
    Phone       NVARCHAR(20)  NULL,                        
    TrangThai   NVARCHAR(20) NOT NULL DEFAULT N'Hoạt động'
        CONSTRAINT CK_TaiKhoan_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Khóa')),
    NgayTao     DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

/* ===================== 3) KHÁCH HÀNG & BẢO HÀNH ===================== */
CREATE TABLE dbo.KhachHang
(
    MaKH      INT IDENTITY(1,1) PRIMARY KEY,
    TenKH     NVARCHAR(200) NOT NULL,
    Email     NVARCHAR(200) NULL,
    DienThoai NVARCHAR(50)  NULL,
    DiaChi    NVARCHAR(255) NULL
);
GO

CREATE TABLE dbo.BaoHanh
(
    MaBaoHanh   INT IDENTITY(1,1) PRIMARY KEY,
    NhaCungCap  NVARCHAR(200) NOT NULL,
    NgayBatDau  DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    DieuKhoan   NVARCHAR(MAX) NULL,
    MaTaiSan    INT NULL,
    CONSTRAINT CK_BaoHanh_Ngay CHECK (NgayKetThuc >= NgayBatDau)
);
GO

/* ===================== 4) TÀI SẢN ===================== */
CREATE TABLE dbo.TaiSan
(
    MaTaiSan  INT IDENTITY(1,1) PRIMARY KEY,
    TenTaiSan NVARCHAR(200) NOT NULL,
    ViTri     NVARCHAR(200) NULL,
    NgayMua   DATE NULL,
    MaBaoHanh INT NULL,
    MaKH      INT NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang hoạt động',
    GhiChu    NVARCHAR(MAX) NULL,
    FOREIGN KEY (MaBaoHanh) REFERENCES dbo.BaoHanh(MaBaoHanh) ON DELETE SET NULL,
    FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH) ON DELETE SET NULL,
    CONSTRAINT CK_TaiSan_TrangThai CHECK (TrangThai IN (N'Đang hoạt động', N'Hỏng', N'Bảo trì'))
);
GO

ALTER TABLE dbo.BaoHanh
ADD CONSTRAINT FK_BaoHanh_TaiSan FOREIGN KEY (MaTaiSan)
REFERENCES dbo.TaiSan(MaTaiSan)
ON DELETE SET NULL;
GO

/* ===================== 5) LỊCH BẢO TRÌ ===================== */
CREATE TABLE dbo.LichBaoTri
(
    MaLich           INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiSan         INT NOT NULL,
    MaNV             INT NULL,
    TanSuat          NVARCHAR(50) NOT NULL,
    SoNgayLapLai     INT NULL,
    NgayKeTiep       DATE NOT NULL,
    ChecklistMacDinh NVARCHAR(MAX) NULL,
    HieuLuc          BIT NOT NULL DEFAULT (1),
    FOREIGN KEY (MaTaiSan) REFERENCES dbo.TaiSan(MaTaiSan) ON DELETE CASCADE,
    FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE SET NULL
);
GO

/* ===================== 6) PHIẾU CÔNG VIỆC ===================== */
CREATE TABLE dbo.PhieuCongViec
(
    MaPhieuCV     INT IDENTITY(1,1) PRIMARY KEY,
    Loai          NVARCHAR(10) NOT NULL,
    MaLich        INT NULL,
    MaTaiSan      INT NOT NULL,
    TieuDe        NVARCHAR(300) NOT NULL,
    MucUuTien     NVARCHAR(20) NOT NULL DEFAULT N'Trung bình',
    SLA_Gio       INT NULL,
    MoTa          NVARCHAR(MAX) NULL,
    MaNV_PhanCong INT NULL,
    NgayTao       DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    TrangThai     NVARCHAR(30) NOT NULL DEFAULT N'Mới',
    NgayBatDau    DATETIME2 NULL,
    NgayHoanThanh DATETIME2 NULL,
    GhiChu        NVARCHAR(MAX) NULL,
    CONSTRAINT CK_PCV_Loai CHECK (Loai IN (N'PM', N'CM')),
    CONSTRAINT CK_PCV_UuTien CHECK (MucUuTien IN (N'Thấp', N'Trung bình', N'Cao', N'Khẩn')),
    CONSTRAINT CK_PCV_TrangThai CHECK (TrangThai IN (N'Mới', N'Đang làm', N'Tạm dừng', N'Hoàn thành', N'Huỷ')),
    FOREIGN KEY (MaLich) REFERENCES dbo.LichBaoTri(MaLich),
    FOREIGN KEY (MaTaiSan) REFERENCES dbo.TaiSan(MaTaiSan),
    FOREIGN KEY (MaNV_PhanCong) REFERENCES dbo.NhanVien(MaNV)
);
GO

/* ===================== 7) PHIẾU SỰ CỐ (ĐƠN GIẢN HÓA) ===================== */
CREATE TABLE dbo.PhieuSuCo
(
    MaSuCo       INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiSan     INT NOT NULL,
    MoTa         NVARCHAR(MAX) NULL,
    MucDo        NVARCHAR(20) NOT NULL
        CONSTRAINT CK_PhieuSuCo_MucDo CHECK (MucDo IN (N'Thấp', N'Trung bình', N'Cao', N'Khẩn')),
    NgayBaoCao   DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    TrangThai    NVARCHAR(30) NOT NULL DEFAULT N'Mới'
        CONSTRAINT CK_PhieuSuCo_TrangThai CHECK (TrangThai IN (N'Mới', N'Đang xử lý', N'Đã giải quyết', N'Đóng')),
    FOREIGN KEY (MaTaiSan) REFERENCES dbo.TaiSan(MaTaiSan) ON DELETE CASCADE
);
GO

    /* ===================== 8) LINH KIỆN & PHIẾU KHO ===================== */
    CREATE TABLE dbo.LinhKien
    (
        MaLinhKien  INT IDENTITY(1,1) PRIMARY KEY,
        TenLinhKien NVARCHAR(200) NOT NULL,
        MaSo        NVARCHAR(100) NULL,
        TonKho      INT NOT NULL DEFAULT(0)
    );
    GO

    CREATE TABLE dbo.PhieuKho
    (
        MaPhieuKho  INT IDENTITY(1,1) PRIMARY KEY,
        Loai        NVARCHAR(10) NOT NULL CHECK (Loai IN (N'Nhap', N'Xuat')),
        NgayLap     DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        MaNV        INT NULL,
        TenNhanVien NVARCHAR(200) NULL,
        GhiChu      NVARCHAR(500) NULL,
        FOREIGN KEY (MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE SET NULL
    );
    GO

    CREATE TABLE dbo.PhieuKho_ChiTiet
    (
        MaCT       INT IDENTITY(1,1) PRIMARY KEY,
        MaPhieuKho INT NOT NULL,
        MaLinhKien INT NOT NULL,
        SoLuong    INT NOT NULL CHECK (SoLuong > 0),
        DonGia     DECIMAL(18,2) NULL,
        FOREIGN KEY (MaPhieuKho) REFERENCES dbo.PhieuKho(MaPhieuKho) ON DELETE CASCADE,
        FOREIGN KEY (MaLinhKien) REFERENCES dbo.LinhKien(MaLinhKien)
    );
    GO

/* ===================== 9) CHECKLIST ===================== */
CREATE TABLE dbo.Checklist
(
    ChecklistID INT IDENTITY(1,1) PRIMARY KEY,
    Ten         NVARCHAR(200) NOT NULL,
    MoTa        NVARCHAR(500) NULL
);
GO

CREATE TABLE dbo.ChecklistItem
(
    ItemID      INT IDENTITY(1,1) PRIMARY KEY,
    ChecklistID INT NOT NULL,
    NoiDung     NVARCHAR(500) NOT NULL,
    FOREIGN KEY (ChecklistID) REFERENCES dbo.Checklist(ChecklistID) ON DELETE CASCADE
);
GO

CREATE TABLE dbo.PCV_Checklist
(
    ID           INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieuCV    INT NOT NULL,
    ItemID       INT NOT NULL,
    DaHoanThanh  BIT NOT NULL DEFAULT(0),
    FOREIGN KEY (MaPhieuCV) REFERENCES dbo.PhieuCongViec(MaPhieuCV) ON DELETE CASCADE,
    FOREIGN KEY (ItemID)    REFERENCES dbo.ChecklistItem(ItemID)   ON DELETE CASCADE,
    CONSTRAINT UX_PCV_Checklist UNIQUE(MaPhieuCV, ItemID)
);
GO

/* ===================== 10) NHẬT KÝ & TỆP ===================== */
CREATE TABLE dbo.NhatKyHeThong
(
    MaLog     BIGINT IDENTITY(1,1) PRIMARY KEY,
    TenBang   NVARCHAR(50) NOT NULL,
    MaBanGhi  INT NOT NULL,
    HanhDong  NVARCHAR(50) NOT NULL,
    GiaTriCu  NVARCHAR(MAX) NULL,
    GiaTriMoi NVARCHAR(MAX) NULL,
    ThayDoiBoi NVARCHAR(200) NULL,
    ThoiGian  DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

CREATE TABLE dbo.TepDinhKem
(
    MaTep        INT IDENTITY(1,1) PRIMARY KEY,
    BangLienQuan NVARCHAR(50) NOT NULL,
    MaLienQuan   INT NOT NULL,
    TenTep       NVARCHAR(255) NOT NULL,
    DuongDan     NVARCHAR(500) NOT NULL,
    NgayTaiLen   DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

/* ===================== 11) DỮ LIỆU MẪU ===================== */
INSERT INTO dbo.NhanVien (HoTen, SoDienThoai, Email, TrangThai)
VALUES
(N'Nguyễn Văn A', '0909123456', 'a.nguyen@example.com', N'Hoạt động'),
(N'Lê Thị B', '0911222333', 'b.le@example.com', N'Nghỉ việc');

INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhauHash, Role, FullName, Email, Phone)
VALUES
(N'admin', CONVERT(NVARCHAR(500), HASHBYTES('SHA2_256', N'admin123'), 2), N'QuanLy', N'Nguyễn Văn A', N'admin@example.com', '0909123456'),
(N'ktv1',  CONVERT(NVARCHAR(500), HASHBYTES('SHA2_256', N'ktv123'), 2),  N'NhanVien', N'Lê Thị B', N'ktv@example.com', '0911222333');

INSERT INTO dbo.KhachHang (TenKH, Email, DienThoai, DiaChi)
VALUES (N'Công ty TNHH ABC', 'contact@abc.vn', '02812345678', N'Quận 1, TP.HCM');

INSERT INTO dbo.BaoHanh (NhaCungCap, NgayBatDau, NgayKetThuc, DieuKhoan)
VALUES (N'Samsung VN', '2023-01-01', '2025-01-01', N'Bảo hành 2 năm chính hãng');

INSERT INTO dbo.TaiSan (TenTaiSan, ViTri, NgayMua, MaBaoHanh, MaKH, TrangThai)
VALUES (N'Máy in Canon LBP2900', N'Phòng Kế Toán', '2023-12-01', 1, 1, N'Đang hoạt động');

INSERT INTO dbo.LichBaoTri (MaTaiSan, MaNV, TanSuat, SoNgayLapLai, NgayKeTiep, ChecklistMacDinh)
VALUES (1, 1, N'Hàng tháng', 30, '2025-10-01', N'Vệ sinh máy in, kiểm tra mực');

INSERT INTO dbo.PhieuCongViec (Loai, MaLich, MaTaiSan, TieuDe, MucUuTien, SLA_Gio, MoTa, MaNV_PhanCong)
VALUES (N'PM', 1, 1, N'Bảo trì máy in', N'Trung bình', 24, N'Kiểm tra định kỳ', 1);

INSERT INTO dbo.LinhKien (TenLinhKien, MaSo, TonKho)
VALUES (N'Mực in Canon 303', 'INK-303', 5);

INSERT INTO dbo.PhieuKho (Loai, MaNV, TenNhanVien, GhiChu)
VALUES
(N'Nhap', 1, N'Nguyễn Văn A', N'Nhập kho linh kiện mực in'),
(N'Xuat', 2, N'Lê Thị B', N'Xuất linh kiện cho phòng kỹ thuật');

INSERT INTO dbo.PhieuKho_ChiTiet (MaPhieuKho, MaLinhKien, SoLuong, DonGia)
VALUES (1, 1, 3, 250000);

INSERT INTO dbo.Checklist (Ten, MoTa)
VALUES (N'Bảo trì máy in định kỳ', N'Vệ sinh và kiểm tra mực');

INSERT INTO dbo.ChecklistItem (ChecklistID, NoiDung)
VALUES (1, N'Vệ sinh đầu in'), (1, N'Kiểm tra mực in');

INSERT INTO dbo.PhieuSuCo (MaTaiSan, MoTa, MucDo, TrangThai)
VALUES
(1, N'Máy in không nhận lệnh in', N'Cao', N'Mới'),
(1, N'Giấy bị kẹt trong máy in', N'Trung bình', N'Đang xử lý'),
(1, N'Máy in in mờ, cần thay mực', N'Thấp', N'Đã giải quyết'),
(1, N'Máy in hoạt động bình thường', N'Thấp', N'Đóng');
GO

/* ===================== KIỂM TRA ===================== */
SELECT * FROM dbo.NhanVien;
SELECT * FROM dbo.TaiKhoan;
SELECT * FROM dbo.KhachHang;
SELECT * FROM dbo.BaoHanh;
SELECT * FROM dbo.TaiSan;
SELECT * FROM dbo.LichBaoTri;
SELECT * FROM dbo.PhieuCongViec;
SELECT * FROM dbo.PhieuSuCo;
SELECT * FROM dbo.LinhKien;
SELECT * FROM dbo.PhieuKho;
SELECT * FROM dbo.PhieuKho_ChiTiet;
SELECT * FROM dbo.Checklist;
SELECT * FROM dbo.ChecklistItem;

-- ✅ Hiển thị bảng Phiếu Sự Cố đúng giao diện bạn gửi


SELECT * FROM TaiSan;


SELECT b.MaBaoHanh, b.MaTaiSan, t.TenTaiSan
FROM BaoHanh b
LEFT JOIN TaiSan t ON b.MaTaiSan = t.MaTaiSan;

UPDATE BaoHanh 
SET MaTaiSan = 1
WHERE MaBaoHanh = 1;
SELECT definition
FROM sys.check_constraints
WHERE name = 'CK_PCV_TrangThai';


EXEC sp_columns PhieuKho
;

EXEC sp_helpconstraint 'PhieuKho';


SELECT * FROM dbo.NhatKyHeThong;

INSERT INTO dbo.NhatKyHeThong (TenBang, MaBanGhi, HanhDong, GiaTriCu, GiaTriMoi, ThayDoiBoi)
VALUES (N'TaiSan', 1, N'Thêm', N'{"TrangThai":"Đang hoạt động"}', N'{"TrangThai":"Bảo trì"}', N'admin');


SELECT * FROM dbo.NhatKyHeThong
DELETE FROM dbo.NhatKyHeThong
WHERE HanhDong = N'Cập nhật';


SELECT * FROM PCV_Checklist WHERE MaPhieuCV = 1;


INSERT INTO PCV_Checklist (MaPhieuCV, ItemID, DaHoanThanh)
VALUES 
(1, 1, 1),
(1, 2, 0),
(1, 3, 1);


SELECT MaTaiKhoan, TenDangNhap, MatKhauHash, Email, Phone, TrangThai 
FROM TaiKhoan;