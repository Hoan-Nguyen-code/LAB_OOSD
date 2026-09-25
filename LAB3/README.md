# LAB 3 - HỆ THỐNG QUẢN LÝ KHÁCH SẠN

## 1. Thông tin sinh viên

- **Họ và tên:** Nguyễn Đức Hoan
- **MSSV:** 1250080056
- **Repository:** LAB_OOSD
- **Thư mục bài thực hành:** LAB3

---

## 2. Tên bài Lab

**LAB 3 - Hệ thống quản lý khách sạn**

Bài thực hành xây dựng chương trình quản lý khách sạn bằng C# Windows Forms, kết nối cơ sở dữ liệu SQL Server và xử lý các nghiệp vụ chính như quản lý danh mục, phòng, tiện nghi, đặt phòng, sử dụng dịch vụ, trả phòng, thanh toán và thống kê.

---

## 3. Môi trường và phiên bản

- **IDE:** Visual Studio 2022
- **Ngôn ngữ:** C#
- **Framework:** .NET Framework 4.7.2
- **Giao diện:** Windows Forms
- **Cơ sở dữ liệu:** Microsoft SQL Server
- **Công cụ quản lý CSDL:** SQL Server Management Studio (SSMS)
- **Truy xuất dữ liệu:** ADO.NET
- **Quản lý mã nguồn:** Git và GitHub

Kiến trúc chính của chương trình:

```text
WinForms UI
     ↓
Service / Business Logic
     ↓
Data / Db
     ↓
SQL Server
```

---

## 4. Nội dung đã thực hiện

### 4.1. Quản lý danh mục

Đã xây dựng các chức năng quản lý:

- Khu vực
- Nhân viên
- Loại tiện nghi
- Dịch vụ
- Quy định đền bù

### 4.2. Quản lý phòng và tiện nghi

- Quản lý thông tin phòng.
- Quản lý tiện nghi.
- Quản lý việc lắp đặt tiện nghi vào phòng.
- Kiểm tra tiện nghi đã được lắp đặt trong cùng ngày.
- Theo dõi trạng thái phòng và tình trạng tiện nghi.

### 4.3. Đặt phòng và nhận phòng

- Lập phiếu đặt phòng.
- Lưu thông tin khách hàng.
- Chọn phòng và số lượng người.
- Kiểm tra sức chứa tối đa của phòng.
- Kiểm tra trùng thời gian đặt phòng.
- Quản lý người lưu trú.
- Thực hiện nhận phòng.
- Cập nhật trạng thái phòng và phiếu đặt.
- Xử lý trường hợp khách không đến (No-show).

### 4.4. Quản lý sử dụng dịch vụ

- Ghi nhận dịch vụ khách sử dụng.
- Kiểm tra khách phải ở trạng thái **Đang ở** trước khi sử dụng dịch vụ.
- Ghi nhận số lượng sử dụng.
- Cộng dồn số lượng nếu cùng một dịch vụ được sử dụng nhiều lần trong cùng ngày.
- Hiển thị lịch sử sử dụng dịch vụ.

### 4.5. Trả phòng, đền bù và thanh toán

- Kiểm tra tiện nghi khi trả phòng.
- Lập phiếu đền bù khi tiện nghi bị hư hỏng hoặc mất.
- Lập hóa đơn.
- Tính tiền phòng.
- Tính tiền dịch vụ.
- Tính tổng tiền hóa đơn.
- Ghi nhận thanh toán.
- Hỗ trợ thanh toán nhiều lần.
- Kiểm tra hóa đơn đã thanh toán đủ trước khi trả phòng.
- Không cho phép hoàn tất trả phòng nếu hóa đơn chưa thanh toán đủ.
- Cập nhật trạng thái phòng về **Trống** sau khi trả phòng thành công.

Các hình thức thanh toán được hỗ trợ:

- Tiền mặt
- Chuyển khoản
- Thẻ
- Ví điện tử

### 4.6. Thống kê

Đã xây dựng chức năng thống kê theo khoảng thời gian, bao gồm:

- Số phiếu đặt phòng.
- Số lượt đang ở.
- Số hóa đơn.
- Doanh thu hóa đơn.
- Tổng tiền đền bù.
- Tổng số lượng sử dụng theo từng dịch vụ.
- Tổng tiền theo từng dịch vụ.

---

## 5. Cơ sở dữ liệu

Cơ sở dữ liệu sử dụng:

```text
QuanLyKhachSan
```

Các bảng chính:

- `NhanVien`
- `KhuVuc`
- `Phong`
- `LoaiTienNghi`
- `TienNghi`
- `PhieuLapDat`
- `KhachHang`
- `PhieuDatPhong`
- `ChiTietDatPhong`
- `NguoiLuuTru`
- `DichVu`
- `PhieuSuDungDV`
- `ChiTietPhieuSuDungDV`
- `QuyDinhDenBu`
- `PhieuDenBu`
- `ChiTietPhieuDenBu`
- `HoaDon`
- `ThanhToan`

Các bảng được liên kết thông qua khóa chính và khóa ngoại nhằm đảm bảo tính toàn vẹn dữ liệu.

File SQL dùng để tạo cơ sở dữ liệu được lưu trong thư mục:

```text
QuanLyKhachSan/QuanLyKhachSan/Database/
```

---

## 6. Kết quả thực hiện

Chương trình đã kết nối thành công với SQL Server và các chức năng chính đã được chạy thử trực tiếp trên máy cá nhân.

Các trường hợp kiểm thử nghiệp vụ từ **TC01 đến TC15** đã được thực hiện, bao gồm:

- Đặt phòng hợp lệ.
- Từ chối khi số người vượt quá sức chứa phòng.
- Từ chối khi phòng bị trùng lịch đặt.
- Kiểm tra việc lắp đặt tiện nghi.
- Nhận phòng và chuyển trạng thái sang **Đang ở**.
- Kiểm tra số lượng người lưu trú.
- Ghi nhận dịch vụ.
- Cộng dồn cùng dịch vụ trong cùng ngày.
- Không cho sử dụng dịch vụ khi khách chưa ở.
- Lập phiếu đền bù.
- Lập hóa đơn gồm tiền phòng và tiền dịch vụ.
- Thanh toán một phần.
- Thanh toán đầy đủ.
- Không cho trả phòng khi hóa đơn chưa được thanh toán đủ.
- Hoàn tất trả phòng và giải phóng phòng.
- Thống kê dữ liệu theo khoảng thời gian.

**Kết quả:** các chức năng chính của chương trình hoạt động đúng theo các trường hợp kiểm thử đã thực hiện.

---

## 7. Lỗi gặp phải và cách khắc phục

### 7.1. Kết nối SQL Server

**Vấn đề:**  
Ứng dụng cần sử dụng đúng tên SQL Server và đúng cơ sở dữ liệu trên máy thực hiện.

**Cách khắc phục:**  
Kiểm tra lại `connectionString` trong file `App.config`, đảm bảo `Data Source` và `Initial Catalog` được cấu hình chính xác.

---

### 7.2. Đặt phòng bị trùng thời gian

**Vấn đề:**  
Một phòng có thể bị đặt bởi nhiều khách trong cùng khoảng thời gian nếu không kiểm tra trước khi tạo phiếu.

**Cách khắc phục:**  
Kiểm tra khoảng thời gian đặt phòng trong tầng Service trước khi cho phép tạo phiếu đặt phòng.

---

### 7.3. Số người vượt quá sức chứa phòng

**Vấn đề:**  
Số người đặt phòng hoặc số người lưu trú có thể vượt quá sức chứa tối đa của phòng.

**Cách khắc phục:**  
Kiểm tra `SoNguoiToiDa` của phòng trước khi tạo đặt phòng hoặc thêm người lưu trú.

---

### 7.4. Dịch vụ được sử dụng nhiều lần trong cùng ngày

**Vấn đề:**  
Một khách có thể sử dụng cùng một dịch vụ nhiều lần trong một ngày.

**Cách khắc phục:**  
Nếu dịch vụ đã tồn tại trong phiếu sử dụng dịch vụ của ngày đó thì cập nhật và cộng dồn số lượng thay vì tạo bản ghi trùng.

---

### 7.5. Thanh toán hóa đơn nhiều lần

**Vấn đề:**  
Khách có thể thanh toán hóa đơn thành nhiều lần nên không thể chỉ kiểm tra một lần thanh toán.

**Cách khắc phục:**  
Tính tổng tất cả các khoản thanh toán của hóa đơn và chỉ cho phép hoàn tất trả phòng khi tổng số tiền đã thanh toán đủ giá trị hóa đơn.

---

### 7.6. Hiển thị DataGridView ở màn hình thống kê

**Vấn đề:**  
DataGridView của giao diện thống kê ban đầu hiển thị nhỏ, khiến các cột và dữ liệu khó quan sát.

**Cách khắc phục:**  
Điều chỉnh `Dock`, `AutoSizeColumnsMode` và kích thước các thành phần giao diện để DataGridView sử dụng toàn bộ vùng hiển thị.

---

## 8. Hướng dẫn chạy và kiểm tra

### Bước 1: Chuẩn bị cơ sở dữ liệu

Mở **SQL Server Management Studio (SSMS)**.

Chạy file SQL nằm trong thư mục:

```text
QuanLyKhachSan/QuanLyKhachSan/Database/
```

để tạo cơ sở dữ liệu:

```text
QuanLyKhachSan
```

cùng các bảng và dữ liệu mẫu cần thiết.

> **Lưu ý:** Nếu cơ sở dữ liệu `QuanLyKhachSan` đã tồn tại trên máy thì cần kiểm tra trước khi chạy lại toàn bộ script để tránh lỗi do các bảng đã tồn tại.

### Bước 2: Kiểm tra chuỗi kết nối

Mở file:

```text
QuanLyKhachSan/QuanLyKhachSan/App.config
```

Kiểm tra phần `connectionStrings`.

Tên SQL Server trên máy kiểm tra có thể khác với máy thực hiện bài Lab, vì vậy cần thay đổi `Data Source` cho phù hợp nếu cần.

Database sử dụng là:

```text
QuanLyKhachSan
```

### Bước 3: Mở project

Mở solution:

```text
QuanLyKhachSan/QuanLyKhachSan.slnx
```

bằng **Visual Studio 2022**.

Project sử dụng:

```text
.NET Framework 4.7.2
```

### Bước 4: Build chương trình

Trong Visual Studio chọn:

```text
Build -> Build Solution
```

hoặc sử dụng phím tắt:

```text
Ctrl + Shift + B
```

### Bước 5: Chạy chương trình

Nhấn:

```text
F5
```

Sau khi chương trình khởi động, có thể kiểm tra lần lượt các chức năng:

1. Quản lý danh mục.
2. Quản lý phòng và tiện nghi.
3. Đặt phòng và nhận phòng.
4. Sử dụng dịch vụ.
5. Trả phòng, đền bù và thanh toán.
6. Thống kê.

---

## 9. Bằng chứng thực hiện

Báo cáo Word của LAB3 có các hình ảnh được chụp trực tiếp trong quá trình thực hiện trên máy cá nhân, bao gồm:

- Cấu trúc cơ sở dữ liệu trên SQL Server Management Studio.
- Database Diagram của hệ thống.
- Các giao diện Windows Forms.
- Kết quả chạy và kiểm tra các chức năng.

Các hình ảnh được sử dụng làm bằng chứng cho quá trình xây dựng và kiểm thử hệ thống.

---

## 10. Cấu trúc thư mục

```text
LAB3/
│
├── README.md
│
└── QuanLyKhachSan/
    │
    ├── QuanLyKhachSan.slnx
    │
    └── QuanLyKhachSan/
        │
        ├── Data/
        ├── Database/
        ├── Services/
        ├── FrmMain.cs
        ├── FrmDanhMuc.cs
        ├── FrmPhongTienNghi.cs
        ├── FrmDatPhong.cs
        ├── FrmDichVu.cs
        ├── FrmTraPhong.cs
        ├── FrmThongKe.cs
        ├── App.config
        └── ...
```

---

## 11. Ghi chú

- Bài Lab được thực hiện và kiểm thử trên máy cá nhân.
- Khi chạy trên máy khác cần kiểm tra lại tên SQL Server trong `App.config`.
- Không đưa thông tin nhạy cảm hoặc dữ liệu cá nhân không cần thiết lên repository.
- Báo cáo Word và các bằng chứng thực hiện được lưu trong thư mục LAB3 theo yêu cầu của học phần.

---

**Sinh viên thực hiện:** Nguyễn Đức Hoan  
**MSSV:** 1250080056
