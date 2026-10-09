# LAB5 – QUẢN LÝ CÔNG TY DU LỊCH

## 1. Thông tin sinh viên

- **Họ và tên:** Nguyễn Đức Hoan
- **MSSV:** 1250080056
- **Tên bài Lab:** LAB5 – Quản lý công ty du lịch
- **Môn học:** Phân tích và Thiết kế Hệ thống Hướng đối tượng (OOASD)
- **Repository:** https://github.com/Hoan-Nguyen-code/LAB_OOSD

## 2. Môi trường và công nghệ sử dụng

| Thành phần | Công nghệ / Phiên bản |
|---|---|
| Ngôn ngữ lập trình | C# |
| Nền tảng | Windows Forms |
| Framework | .NET Framework 4.7.2 |
| IDE | Visual Studio 2022 |
| Hệ quản trị CSDL | Microsoft SQL Server |
| Công cụ quản lý CSDL | SQL Server Management Studio (SSMS) |
| Hệ điều hành | Windows |
| Quản lý mã nguồn | Git và GitHub |

*Phiên bản SQL Server và SSMS cụ thể cần kiểm tra trên máy nếu giảng viên yêu cầu ghi chính xác.*

## 3. Nội dung đã thực hiện

### 3.1. Phân tích và thiết kế hệ thống

- Phân tích nghiệp vụ quản lý công ty du lịch Văn Hóa Việt.
- Xác định các tác nhân, chức năng và quy tắc nghiệp vụ.
- Xây dựng các sơ đồ Use Case, Activity, Sequence và Class Diagram.
- Thiết kế cơ sở dữ liệu và các mối quan hệ giữa các bảng.

### 3.2. Xây dựng cơ sở dữ liệu

- Tạo cơ sở dữ liệu `QuanLyCongTyDuLich` trên SQL Server.
- Xây dựng 16 bảng dữ liệu phục vụ các chức năng nghiệp vụ.
- Thiết lập khóa chính, khóa ngoại và các ràng buộc dữ liệu.
- Thêm dữ liệu mẫu để kiểm tra hoạt động của chương trình.

### 3.3. Xây dựng chương trình Windows Forms

Đã xây dựng 8 nhóm chức năng:

1. **Quản lý danh mục:** Phương tiện, điểm bán vé, hướng dẫn viên và điểm tham quan.
2. **Quản lý tour – hành trình:** Tour, điểm dừng, phương tiện theo chặng và điểm tham quan.
3. **Quản lý chuyến khách lẻ:** Tạo lịch chuyến, tự động tính ngày về và đóng đăng ký.
4. **Đăng ký khách lẻ:** Lập phiếu đăng ký và tính tiền vé.
5. **Đăng ký khách đoàn:** Quản lý đoàn, thành viên, đặt cọc, bảo hiểm và hủy đăng ký.
6. **Phân công hướng dẫn viên:** Phân công theo chuyến hoặc đoàn, kiểm tra trùng lịch.
7. **Kết thúc tour – khảo sát:** Thanh toán kinh phí đoàn, ghi nhận đánh giá và góp ý.
8. **Lương – thống kê:** Tính lương hướng dẫn viên và thống kê hoạt động kinh doanh.

Chương trình tổ chức theo mô hình:

**Windows Forms → Services → Data → SQL Server**

## 4. Kết quả thực hiện

- Xây dựng các Form theo từng nhóm chức năng của hệ thống.
- Kết nối ứng dụng C# Windows Forms với SQL Server.
- Chạy chương trình và hiển thị dữ liệu từ cơ sở dữ liệu.
- Thực hiện thao tác thêm dữ liệu và hiển thị kết quả trên DataGridView.
- Hoàn thiện các sơ đồ UML và hình ảnh minh chứng thực tế trong báo cáo Word.
- Đưa mã nguồn và script SQL lên GitHub.

Các hình ảnh giao diện, cơ sở dữ liệu và kết quả chạy thực tế được trình bày trong file báo cáo Word đi kèm.

## 5. Lỗi gặp phải và cách khắc phục

| Lỗi / Vấn đề | Cách khắc phục |
|---|---|
| Cần kết nối WinForms với SQL Server trên máy cá nhân | Cấu hình chuỗi kết nối trong `App.config`, sử dụng `Db.cs` để thực hiện truy vấn |
| Dữ liệu chưa hiển thị đúng trên Form | Kiểm tra truy vấn trong Service và nạp kết quả vào DataGridView |
| Cần tự động tính ngày về của chuyến | Lấy số ngày từ Tour và tính ngày về theo ngày khởi hành |
| Dễ nhập dữ liệu không hợp lệ khi đăng ký hoặc phân công | Bổ sung kiểm tra điều kiện nghiệp vụ trong Service |
| Thay đổi vị trí file SQL trong project | Đưa script vào thư mục `Database` để dễ quản lý và kiểm tra lại đường dẫn |

**Lưu ý:** Các nội dung trên mô tả cách xử lý kỹ thuật trong quá trình xây dựng chương trình. Khi hoàn thiện báo cáo, cần chỉnh lại để phản ánh đúng những lỗi thực tế đã gặp trên máy.

## 6. Hướng dẫn cài đặt và chạy lại

### Bước 1: Chuẩn bị môi trường

Máy tính cần có:

- Visual Studio 2022, cài workload `.NET desktop development`.
- .NET Framework 4.7.2 Developer Pack hoặc targeting pack tương ứng.
- Microsoft SQL Server.
- SQL Server Management Studio.

### Bước 2: Tải mã nguồn

Truy cập repository:

https://github.com/Hoan-Nguyen-code/LAB_OOSD

Có thể tải ZIP từ GitHub hoặc clone bằng lệnh:

`git clone https://github.com/Hoan-Nguyen-code/LAB_OOSD.git`

Sau đó mở thư mục `LAB5`.

### Bước 3: Khởi tạo cơ sở dữ liệu

1. Mở SQL Server Management Studio.
2. Kết nối đến SQL Server trên máy.
3. Mở file `lab5.sql` trong thư mục `Database`.
4. Chạy script để tạo cơ sở dữ liệu `QuanLyCongTyDuLich` và dữ liệu mẫu.
5. Kiểm tra các bảng đã được tạo thành công.

### Bước 4: Cấu hình kết nối SQL Server

Mở file `App.config` của project.

Chỉnh tên SQL Server và thông tin xác thực trong chuỗi kết nối cho phù hợp với máy đang chạy.

Tên cơ sở dữ liệu: `QuanLyCongTyDuLich`.

### Bước 5: Chạy ứng dụng

1. Mở file `QuanLyCongTyDuLich.slnx` bằng Visual Studio 2022.
2. Chọn project `QuanLyCongTyDuLich` làm Startup Project nếu cần.
3. Chọn **Build → Rebuild Solution**.
4. Nhấn **F5** để chạy chương trình.
5. Từ màn hình chính, chọn chức năng muốn kiểm tra.

### Bước 6: Kiểm tra hoạt động

Giảng viên có thể thực hiện các thao tác:

- Xem danh sách tour và tạo tour mới.
- Tạo chuyến khách lẻ và kiểm tra ngày về tự động.
- Đăng ký khách lẻ hoặc khách đoàn.
- Phân công hướng dẫn viên và kiểm tra trùng lịch.
- Kiểm tra thanh toán, khảo sát khách hàng.
- Tính lương hướng dẫn viên và xem thống kê.

Dữ liệu phát sinh có thể đối chiếu trực tiếp bằng các câu lệnh `SELECT` trong SQL Server Management Studio.

## 7. Tài liệu và sản phẩm đính kèm

- Mã nguồn C# Windows Forms.
- Script cơ sở dữ liệu SQL Server.
- File ZIP project.
- Báo cáo Word chứa UML, giao diện và hình ảnh CSDL thực tế.

## 8. Kết luận

LAB5 vận dụng kiến thức phân tích và thiết kế hệ thống hướng đối tượng vào bài toán quản lý công ty du lịch. Hệ thống đã được xây dựng với các chức năng quản lý tour, đăng ký khách hàng, phân công hướng dẫn viên, thanh toán, khảo sát và thống kê.

Thông qua bài thực hành, sinh viên củng cố kỹ năng thiết kế UML, tổ chức chương trình theo từng lớp xử lý nghiệp vụ, kết nối SQL Server và xây dựng giao diện Windows Forms.
