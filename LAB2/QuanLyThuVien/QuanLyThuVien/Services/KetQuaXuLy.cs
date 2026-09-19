namespace QuanLyThuVien.Services
{
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; private set; }

        public string ThongBao { get; private set; }

        private KetQuaXuLy(
            bool thanhCong,
            string thongBao)
        {
            ThanhCong = thanhCong;
            ThongBao = thongBao;
        }

        public static KetQuaXuLy Ok(string thongBao)
        {
            return new KetQuaXuLy(
                true,
                thongBao);
        }

        public static KetQuaXuLy Loi(string thongBao)
        {
            return new KetQuaXuLy(
                false,
                thongBao);
        }
    }
}