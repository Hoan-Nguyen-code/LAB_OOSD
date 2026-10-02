using System;

namespace shopABC.Adapters
{
    public static class PaymentAdapter
    {
        // =====================================================
        // MÔ PHỎNG ONLINE PAYMENT SERVICE
        // =====================================================
        public static bool ThanhToan(
            string loaiThe,
            string soThe,
            string chuThe,
            string hetHan,
            string csv,
            decimal soTien,
            out string thongBao)
        {
            // =============================================
            // 1. KIỂM TRA DỮ LIỆU BẮT BUỘC
            // =============================================
            if (string.IsNullOrWhiteSpace(loaiThe) ||
                string.IsNullOrWhiteSpace(soThe) ||
                string.IsNullOrWhiteSpace(chuThe) ||
                string.IsNullOrWhiteSpace(hetHan) ||
                string.IsNullOrWhiteSpace(csv))
            {
                thongBao =
                    "Vui lòng nhập đầy đủ thông tin thẻ.";

                return false;
            }

            // =============================================
            // 2. SỐ THẺ CHỈ ĐƯỢC CHỨA SỐ
            // =============================================
            foreach (char c in soThe)
            {
                if (!char.IsDigit(c))
                {
                    thongBao =
                        "Số thẻ chỉ được chứa chữ số.";

                    return false;
                }
            }

            // =============================================
            // 3. CSV CHỈ ĐƯỢC CHỨA SỐ
            // =============================================
            foreach (char c in csv)
            {
                if (!char.IsDigit(c))
                {
                    thongBao =
                        "CSV chỉ được chứa chữ số.";

                    return false;
                }
            }

            // =============================================
            // 4. KIỂM TRA ĐỘ DÀI THEO LOẠI THẺ
            // =============================================
            bool laAmericanExpress =
                loaiThe.Equals(
                    "American Express",
                    StringComparison.OrdinalIgnoreCase
                );

            if (laAmericanExpress)
            {
                if (soThe.Length != 15)
                {
                    thongBao =
                        "American Express phải có 15 chữ số.";

                    return false;
                }

                if (csv.Length != 4)
                {
                    thongBao =
                        "CSV của American Express phải có 4 chữ số.";

                    return false;
                }
            }
            else
            {
                if (soThe.Length != 16)
                {
                    thongBao =
                        loaiThe +
                        " phải có 16 chữ số.";

                    return false;
                }

                if (csv.Length != 3)
                {
                    thongBao =
                        "CSV của " +
                        loaiThe +
                        " phải có 3 chữ số.";

                    return false;
                }
            }

            // =============================================
            // 5. KIỂM TRA NGÀY HẾT HẠN MM/YY
            // =============================================
            if (!KiemTraNgayHetHan(hetHan))
            {
                thongBao =
                    "Ngày hết hạn không hợp lệ hoặc thẻ đã hết hạn.\n" +
                    "Định dạng yêu cầu: MM/YY.";

                return false;
            }

            // =============================================
            // 6. KIỂM TRA SỐ TIỀN
            // =============================================
            if (soTien <= 0)
            {
                thongBao =
                    "Số tiền thanh toán không hợp lệ.";

                return false;
            }

            // =============================================
            // 7. MÔ PHỎNG ONLINE PAYMENT SERVICE TỪ CHỐI
            // =============================================
            // Dùng để test trường hợp thanh toán thất bại.
            if (soThe.EndsWith("0000"))
            {
                thongBao =
                    "Online Payment Service từ chối giao dịch " +
                    "(mô phỏng).";

                return false;
            }

            // =============================================
            // 8. THANH TOÁN THÀNH CÔNG
            // =============================================
            thongBao =
                "Thanh toán thành công.";

            return true;
        }

        // =====================================================
        // KIỂM TRA NGÀY HẾT HẠN
        // =====================================================
        private static bool KiemTraNgayHetHan(
            string hetHan)
        {
            if (string.IsNullOrWhiteSpace(hetHan))
                return false;

            string[] parts =
                hetHan.Split('/');

            if (parts.Length != 2)
                return false;

            int thang;
            int nam;

            if (!int.TryParse(parts[0], out thang))
                return false;

            if (!int.TryParse(parts[1], out nam))
                return false;

            if (thang < 1 || thang > 12)
                return false;

            // Chấp nhận đúng dạng YY
            if (parts[1].Length != 2)
                return false;

            int namDayDu =
                2000 + nam;

            // Thẻ có hiệu lực đến hết tháng
            DateTime ngayHetHan =
                new DateTime(
                    namDayDu,
                    thang,
                    DateTime.DaysInMonth(
                        namDayDu,
                        thang
                    )
                );

            return ngayHetHan >=
                   DateTime.Today;
        }

        // =====================================================
        // PHÍ GIAO DỊCH DEMO
        // =====================================================
        public static decimal TinhPhiGiaoDich(
            string loaiThe,
            decimal soTien)
        {
            if (soTien <= 0)
                return 0;

            // Đây là mức phí MÔ PHỎNG cho LAB.
            // File gốc không cung cấp tỷ lệ cụ thể.

            switch (loaiThe)
            {
                case "MasterCard":
                    return soTien * 0.005m;   // 0.5%

                case "Discover":
                    return soTien * 0.0075m;  // 0.75%

                case "American Express":
                    return soTien * 0.01m;    // 1%

                case "VISA":
                default:
                    return 0;
            }
        }
    }
}