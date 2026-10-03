# LAB 4 – e-SHOPPING

## 1. Thông tin sinh viên

- **Họ và tên:** Nguyễn Đức Hoàn
- **MSSV:** 1250080056
- **Môn học:** Phương pháp phát triển phần mềm hướng đối tượng (OOASD)
- **Bài Lab:** LAB 4 – e-SHOPPING

---

## 2. Môi trường và phiên bản

- **Hệ điều hành:** Windows
- **IDE:** Visual Studio 2022
- **Ngôn ngữ:** C#
- **Framework:** .NET Framework 4.7.2
- **Giao diện:** Windows Forms
- **Hệ quản trị CSDL:** Microsoft SQL Server
- **Công cụ quản lý CSDL:** SQL Server Management Studio (SSMS)
- **Quản lý mã nguồn:** Git / GitHub

---

## 3. Nội dung đã thực hiện

### 3.1. Phân tích và thiết kế

Đã thực hiện phân tích và thiết kế hệ thống e-Shopping gồm:

- Use Case Diagram.
- Đặc tả Use Case.
- Class Diagram.
- Activity Diagram.
- Sequence Diagram.
- ERD / Database Diagram.
- Ma trận truy vết yêu cầu.

Hệ thống có giao tiếp với ba hệ thống/dịch vụ bên ngoài:

- Product Management System.
- Online Payment Service.
- Email Service.

Trong phạm vi bài Lab, các hệ thống bên ngoài được mô phỏng thông qua các lớp Adapter.

### 3.2. Chức năng hệ thống

Đã xây dựng các chức năng:

- Đăng ký tài khoản khách hàng.
- Đăng nhập và đăng xuất.
- Xem danh sách sản phẩm.
- Lọc sản phẩm theo nhóm.
- Tìm kiếm sản phẩm.
- Xem chi tiết sản phẩm và hình ảnh.
- Thêm sản phẩm vào giỏ hàng.
- Xem giỏ hàng.
- Cập nhật số lượng sản phẩm.
- Xóa sản phẩm khỏi giỏ hàng.
- Nhập thông tin người nhận.
- Chọn khu vực giao hàng.
- Chọn loại giao hàng.
- Tính chi phí giao hàng.
- Áp dụng điều kiện miễn phí giao hàng.
- Nhập và kiểm tra thông tin thẻ.
- Mô phỏng thanh toán qua Online Payment Service.
- Ghi nhận đơn đặt hàng và chi tiết đơn hàng.
- Mô phỏng gửi email xác nhận đơn hàng.

### 3.3. Cơ sở dữ liệu

Cơ sở dữ liệu sử dụng tên:

`ShopABCDB`

Các bảng chính:

- `KHACHHANG`
- `NHOMSANPHAM`
- `SANPHAM`
- `GIOHANG`
- `CHITIETGIOHANG`
- `NGUOINHAN`
- `LOAIGIAOHANG`
- `KHUVUCGIAOHANG`
- `DONDATHANG`
- `CHITIETDONHANG`

Script tạo và cập nhật cơ sở dữ liệu được lưu tại:

`shopABC/Database/Lab4.sql`

---

## 4. Kết quả thực hiện

Hệ thống đã chạy và kiểm thử thành công các luồng chức năng chính:

- Đăng ký và đăng nhập.
- Hiển thị, tìm kiếm và lọc sản phẩm.
- Hiển thị chi tiết và hình ảnh sản phẩm.
- Thêm, cập nhật và xóa sản phẩm trong giỏ hàng.
- Tính tiền hàng và chi phí giao hàng.
- Kiểm tra thông tin thẻ thanh toán.
- Xử lý trường hợp dịch vụ thanh toán từ chối giao dịch.
- Thanh toán và đặt hàng thành công.
- Lưu đơn hàng và chi tiết đơn hàng vào SQL Server.
- Gửi thông báo xác nhận đơn hàng thông qua Email Service mô phỏng.

Các chức năng chính đã được kiểm thử và có hình ảnh minh chứng trong báo cáo LAB4.

---

## 5. Lỗi gặp phải và cách khắc phục

### 5.1. Hình ảnh sản phẩm không hiển thị

**Lỗi:** Đường dẫn hình ảnh lưu trong cơ sở dữ liệu là đường dẫn tương đối nên chương trình không tìm thấy file khi chạy.

**Khắc phục:** Sử dụng `Application.StartupPath` để xác định đường dẫn thực tế và cấu hình hình ảnh với `Copy to Output Directory`.

### 5.2. Dữ liệu khu vực giao hàng chưa được lưu vào đơn hàng

**Lỗi:** Ban đầu khu vực chỉ được sử dụng để tính phí giao hàng nhưng chưa được ghi nhận trong bảng đơn đặt hàng.

**Khắc phục:** Bổ sung `MaKhuVuc` vào bảng `DONDATHANG`, tạo khóa ngoại tới `KHUVUCGIAOHANG` và cập nhật chức năng Checkout.

### 5.3. Kiểm tra thông tin thẻ thanh toán

**Lỗi:** Cần xử lý khác nhau về độ dài số thẻ và CSV giữa các loại thẻ.

**Khắc phục:** Bổ sung kiểm tra theo loại thẻ trong `PaymentAdapter`, kiểm tra số thẻ, CSV và ngày hết hạn trước khi thực hiện thanh toán.

### 5.4. Đồng bộ mô hình với chương trình

**Lỗi:** Sau khi bổ sung nhóm sản phẩm, hình ảnh và khu vực giao hàng, mô hình ban đầu chưa phản ánh đầy đủ chương trình.

**Khắc phục:** Cập nhật Class Diagram và ERD theo cấu trúc cuối cùng của hệ thống và cơ sở dữ liệu.

---

## 6. Hướng dẫn chạy và kiểm tra

### Bước 1 – Chuẩn bị cơ sở dữ liệu

1. Mở SQL Server Management Studio.
2. Kết nối tới SQL Server.
3. Mở file:

`shopABC/Database/Lab4.sql`

4. Thực thi script để tạo/cập nhật cơ sở dữ liệu `ShopABCDB`.
5. Kiểm tra các bảng và dữ liệu mẫu sau khi chạy script.

### Bước 2 – Kiểm tra chuỗi kết nối

Kiểm tra cấu hình kết nối SQL Server của project và thay đổi tên SQL Server nếu máy kiểm tra sử dụng server khác.

### Bước 3 – Mở chương trình

Mở file solution:

`shopABC.sln`

bằng Visual Studio 2022.

### Bước 4 – Build và chạy

1. Chọn **Build Solution**.
2. Đảm bảo project build thành công.
3. Nhấn **Start** để chạy chương trình.

### Bước 5 – Kiểm tra chức năng

Có thể kiểm tra lần lượt:

1. Đăng ký tài khoản.
2. Đăng nhập.
3. Xem và lọc sản phẩm.
4. Xem chi tiết sản phẩm.
5. Thêm sản phẩm vào giỏ hàng.
6. Cập nhật hoặc xóa sản phẩm trong giỏ.
7. Tiến hành Checkout.
8. Nhập thông tin người nhận.
9. Chọn khu vực và loại giao hàng.
10. Nhập thông tin thẻ.
11. Xác nhận đặt hàng.
12. Kiểm tra dữ liệu trong `DONDATHANG` và `CHITIETDONHANG`.

> **Lưu ý:** Product Management System, Online Payment Service và Email Service được mô phỏng trong phạm vi bài Lab, không kết nối tới dịch vụ thực tế.

---

## 7. Cấu trúc chính của LAB4

```text
LAB4/
├── README.md
├── 1250080056_NguyenDucHoan_CNPM1_Lab4.docx
├── 1250080056_NguyenDucHoan_CNPM1_Lab4_OOSD.docx
├── shopABC.zip
└── shopABC/
    ├── shopABC.sln
    └── shopABC/
        ├── Data/
        ├── Database/
        │   └── Lab4.sql
        ├── Forms/
        ├── Helpers/
        ├── Images/
        ├── Services/
        └── shopABC.csproj
```

---

## 8. Repository

Source code, script cơ sở dữ liệu, báo cáo và file đóng gói của LAB4 được lưu trong repository GitHub của bài tập OOASD.
