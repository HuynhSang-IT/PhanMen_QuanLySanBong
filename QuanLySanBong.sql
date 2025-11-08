-- Tạo CSDL
CREATE DATABASE QuanLySanBong;
USE QuanLySanBong;
DROP DATABASE QuanLySanBong;
-- Bảng Tài Khoản
CREATE TABLE TaiKhoan (
    TenTaiKhoan NVARCHAR(50) PRIMARY KEY,
    MatKhau CHAR(20) NOT NULL,
    Quyen NVARCHAR(30) NOT NULL,
    Email NVARCHAR(50) NOT NULL
);
Insert into TaiKhoan( TenTaiKhoan, MatKhau, Quyen, Email) Values
('admin','12345','admin','admin@gmail.com');
-- Bảng Nhân Viên
CREATE TABLE NhanVien (
    MaNV CHAR(10) PRIMARY KEY,
    HoTenNV NVARCHAR(50) NOT NULL,
    QueQuan NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    NgayVaoLam DATE NOT NULL,
    ViTri NVARCHAR(20) NOT NULL,
    QuanLyKhu NVARCHAR(30) NOT NULL,
    Luong nvarchar(20) NOT NULL
);

--nhập dữ liệu trước--
INSERT INTO NhanVien (MaNV, HoTenNV, QueQuan, NgaySinh, NgayVaoLam, ViTri, QuanLyKhu, Luong)
VALUES 
('NV01', N'Nguyễn Văn A', N'Hà Nội', '1990-05-15', '2020-06-01', N'Nhân Viên', N'Sân 1', '15.000VNĐ'),
('NV02', N'Trần Thị B', N'Hải Phòng', '1995-08-20', '2021-03-10', N'Nhân Viên', N'Sân 2', '20.000VNĐ'),
('NV03', N'Phạm Văn C', N'Đà Nẵng', '1992-11-25', '2019-09-05', N'Nhân Viên', N'Sân 3', '20.000VNĐ'),
('NV04', N'Lê Thị D', N'Bắc Ninh', '1998-02-14', '2022-01-15', N'Nhân viên', N'Sân 4', '20.000VNĐ'),
('NV05', N'Hoàng Văn E', N'TP.HCM', '1987-07-30', '2018-12-20', N'Nhân Viên', N'Sân 5', '20.000VNĐ');

-- Bảng Đặt Sân
CREATE TABLE DatSan (
    STT INT IDENTITY(1,1) PRIMARY KEY,
    MaSan CHAR(10) NOT NULL,
    TenKhachHang NVARCHAR(50) NOT NULL,
    NgayDat DATE NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    LoaiSan NVARCHAR(30) NOT NULL,
    LoaiThue NVARCHAR(20) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    Buoi NVARCHAR(30) NOT NULL,
    TongSoTien DECIMAL(18,2) NOT NULL
);
ALTER TABLE DatSan
ALTER COLUMN DonGia nvarchar(20);

ALTER TABLE DatSan ADD TrangThaiThanhToan NVARCHAR(20) DEFAULT N'Chưa thanh toán';

ALTER TABLE DatSan 
ADD CONSTRAINT UQ_DatSan UNIQUE (MaSan, NgayDat, GioBatDau, GioKetThuc);

-- Bảng Quản Lý Sân
CREATE TABLE QuanLySan (
    STT INT PRIMARY KEY,
    TenKhachHang NVARCHAR(50) NOT NULL,
    MaSan CHAR(10) NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    NgayDat DATE NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chưa xác nhận',
    FOREIGN KEY (STT) REFERENCES DatSan(STT) ON DELETE CASCADE
);

-- Bảng Thống Kê
CREATE TABLE ThongKe (
    STT INT IDENTITY(1,1) PRIMARY KEY,
    STT_DatSan INT NOT NULL,
	MaSan CHAR(10) NOT NULL,
	NgayDat Date Not NUll,
	TongSoTien DECIMAL (18, 2) NOT NULL,
	TenKhachHang NvarChar(50) not null,
    MaNV CHAR(10) NOT NULL,
	CONSTRAINT FK_ThongKe_STT_Dat
	FOREIGN KEY (STT_DatSan) REFERENCES DatSan(STT),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
ALTER TABLE ThongKe DROP CONSTRAINT FK_ThongKe_STT_Dat; -- Xóa ràng buộc cũ
ALTER TABLE ThongKe
ADD CONSTRAINT FK_ThongKe_STT_Dat FOREIGN KEY (STT_DatSan) 
REFERENCES DatSan(STT) ON DELETE CASCADE; -- Thêm lại với ON DELETE CASCADE
-- Tạo bảng Khách Hàng
CREATE TABLE KhachHang (
    STT INT IDENTITY(1,1) PRIMARY KEY,
    TenKhachHang NVARCHAR(50) NOT NULL,
    DiaChi NVARCHAR(50) NOT NULL,
    NgaySinh DATE NOT NULL,
    QueQuan NVARCHAR(50) NOT NULL,
    SDT INT NOT NULL,
    TenTaiKhoan NVARCHAR(50) NOT NULL,
    FOREIGN KEY (TenTaiKhoan) REFERENCES TaiKhoan(TenTaiKhoan)
);
-- Đảm bảo TenKhachHang là duy nhất
ALTER TABLE KhachHang ADD CONSTRAINT UQ_KhachHang_TenKhach UNIQUE (TenKhachHang);
-- Thêm khóa ngoại vào bảng DatSan
ALTER TABLE DatSan
ADD CONSTRAINT FK_DatSan_KhachHang FOREIGN KEY (TenKhachHang) REFERENCES KhachHang(TenKhachHang);
-- Trigger: Khi đặt sân, tự động thêm vào QuanLySan
DROP TRIGGER IF EXISTS trg_InsertQuanLySan;
GO
CREATE TRIGGER trg_InsertQuanLySan
ON DatSan 
AFTER INSERT --Tạo Trigger
AS
BEGIN --Bắt đầu khối lệnh
    INSERT INTO QuanLySan (STT, TenKhachHang, MaSan, GioBatDau, GioKetThuc, NgayDat, TrangThai) --Chèn dữ liệu vào bảng QuanLySan
    SELECT 
        i.STT, i.TenKhachHang, i.MaSan, i.GioBatDau, i.GioKetThuc, i.NgayDat, N'Đã đặt'
    FROM inserted i; --Lấy dữ liệu từ bảng inserted
END;
GO -- Kết thúc Trigger

-- Trigger: Khi cập nhật thông tin đặt sân, cập nhật QuanLySan
DROP TRIGGER IF EXISTS trg_UpdateQuanLySan;
GO
CREATE TRIGGER trg_UpdateQuanLySan
ON DatSan
AFTER UPDATE
AS
BEGIN
    UPDATE q
    SET 
        q.TenKhachHang = i.TenKhachHang,
        q.MaSan = i.MaSan,
        q.GioBatDau = i.GioBatDau,
        q.GioKetThuc = i.GioKetThuc,
        q.NgayDat = i.NgayDat,
        q.TrangThai = N'Vừa cập nhật'
    FROM QuanLySan q
    INNER JOIN inserted i ON q.STT = i.STT;
END;
GO

-- Trigger: Khi hủy đặt sân, cập nhật trạng thái "Đã hủy" thay vì xóa
DROP TRIGGER IF EXISTS trg_DeleteQuanLySan;
GO
CREATE TRIGGER trg_DeleteQuanLySan
ON DatSan
AFTER DELETE
AS
BEGIN
    UPDATE q
    SET TrangThai = N'Đã hủy'
    FROM QuanLySan q
    INNER JOIN deleted d ON q.STT = d.STT;
END;
GO

-- Trigger: Khi đặt sân, thêm dữ liệu vào Thống Kê
DROP TRIGGER IF EXISTS trg_InsertThongKe;
GO
CREATE TRIGGER trg_InsertThongKe
ON DatSan
AFTER INSERT
AS
BEGIN
    INSERT INTO ThongKe (STT_DatSan, MaSan, NgayDat, TongSoTien, TenKhachHang, MaNV)
    SELECT 
        i.STT, 
        i.MaSan, 
        i.NgayDat, 
        i.TongSoTien, 
        i.TenKhachHang, 
        COALESCE(nv.MaNV, 'NV001') 
    FROM inserted i
    LEFT JOIN NhanVien nv ON nv.QuanLyKhu = i.MaSan;
END;
GO

-- Kiểm tra thông tin sân
SELECT * FROM QuanLySan;
-- Kiểm tra thông tin sân
SELECT * FROM KhachHang;

-- Kiểm tra thông tin đặt sân
SELECT * FROM DatSan;

-- Kiểm tra thống kê doanh thu theo ngày
SELECT NgayDat, SUM(TongSoTien) AS TongDoanhThu
FROM DatSan
GROUP BY NgayDat;

SELECT tk.MaNV, nv.HoTenNV
FROM ThongKe tk
LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV;

SELECT * FROM ThongKe WHERE YEAR(NgayDat) = 2025;