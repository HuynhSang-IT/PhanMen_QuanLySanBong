# ⚽ HỆ THỐNG QUẢN LÝ SÂN BÓNG

## 📝 Giới thiệu
Ứng dụng **Quản lý sân bóng** được phát triển nhằm hỗ trợ chủ sân và nhân viên trong việc **quản lý đặt sân, khách hàng, nhân viên, thống kê doanh thu** và **theo dõi trạng thái sân** một cách trực quan và hiệu quả.

Dự án được xây dựng bằng **C# WinForms** và sử dụng **SQL Server** làm cơ sở dữ liệu.

---

## 🧩 Chức năng chính

### 🔹 1. Quản lý tài khoản
- Đăng nhập, phân quyền **Admin / Nhân viên**  
- Quản lý thông tin người dùng trong bảng `TaiKhoan`

### 🔹 2. Quản lý đặt sân
- Đặt sân mới, sửa thông tin, hủy sân  
- Tự động cập nhật trạng thái trong bảng `QuanLySan`  
- Hỗ trợ các trạng thái: **Đã đặt**, **Đã hủy**, **Vừa cập nhật**

### 🔹 3. Quản lý khách hàng và nhân viên
- Thêm, sửa, xóa và tìm kiếm thông tin khách hàng  
- Quản lý danh sách nhân viên làm việc tại sân

### 🔹 4. Thống kê doanh thu
- Hiển thị thống kê theo **ngày, tháng, năm**  
- Tổng hợp **doanh thu** và **số lượt đặt sân**  
- Liên kết giữa các bảng `DatSan`, `NhanVien`, `ThongKe`

### 🔹 5. Giao diện hiển thị trực quan
- Hiển thị danh sách sân bằng **hình ảnh và trạng thái**  
- Cho phép chỉnh sửa trạng thái sân trực tiếp trên form  
- Giao diện thân thiện, dễ sử dụng

---

## 🗂️ Cấu trúc cơ sở dữ liệu

| Bảng | Mô tả |
|------|-------|
| `TaiKhoan` | Lưu thông tin tài khoản và quyền đăng nhập |
| `KhachHang` | Lưu thông tin khách hàng |
| `NhanVien` | Quản lý nhân viên |
| `DatSan` | Thông tin đặt sân (mã sân, giờ, ngày, khách hàng, nhân viên) |
| `QuanLySan` | Quản lý trạng thái sân |
| `ThongKe` | Liên kết dữ liệu để thống kê doanh thu |

---

## ⚙️ Công nghệ sử dụng

- **Ngôn ngữ:** C# (.NET Framework, WinForms)  
- **Cơ sở dữ liệu:** Microsoft SQL Server  
- **IDE:** Visual Studio  
- **Công cụ quản lý mã nguồn:** Git & GitHub

---

## 🚀 Cách chạy chương trình

1. Clone dự án về máy:
     ```bash
   git clone https://github.com/<tenuser>/quanlysanbong.git
2. Mở file .sln trong Visual Studio.
3. Cấu hình chuỗi kết nối (Connection String) trong App.config.
4. Khởi tạo cơ sở dữ liệu bằng file Database.sql (nếu có).
5. Nhấn Start (F5) để chạy ứng dụng.
👨‍💻 Tác giả
Trần Huỳnh Sang
📧 Email: [hyhsang24@gmail.com]
💼 GitHub: https://github.com/HuynhSang-IT
📸 Hình minh họa giao diện
<img width="1349" height="720" alt="Ảnh chụp màn hình 2025-10-15 140537" src="https://github.com/user-attachments/assets/6b8877b9-6933-4d60-8455-47be054b9d5a" />

🏁 Ghi chú
Đây là dự án học tập và thực hành lập trình WinForms kết hợp SQL Server.
Có thể mở rộng thêm các tính năng như thanh toán, xuất hóa đơn PDF, hoặc quản lý sân qua ứng dụng web/mobile.
⭐ Nếu bạn thấy dự án hữu ích, hãy để lại một ⭐ Star trên GitHub nhé!
   ```bash
   git clone https://github.com/<tenuser>/quanlysanbong.git
