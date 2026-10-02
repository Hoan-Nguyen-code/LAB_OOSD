namespace shopABC.Helpers
{
    public static class Session
    {
        public static int MaKhachHang { get; set; } = 0;
        public static string HoTen { get; set; } = "";

        public static bool DaDangNhap
        {
            get
            {
                return MaKhachHang > 0;
            }
        }

        public static void DangXuat()
        {
            MaKhachHang = 0;
            HoTen = "";
        }
    }
}