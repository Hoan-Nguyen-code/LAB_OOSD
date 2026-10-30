namespace QuanLyNhanSu.Services
{
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; private set; }

        public string ThongBao { get; private set; }

        public static KetQuaXuLy Ok(string thongBao)
        {
            return new KetQuaXuLy
            {
                ThanhCong = true,
                ThongBao = thongBao
            };
        }

        public static KetQuaXuLy Fail(string thongBao)
        {
            return new KetQuaXuLy
            {
                ThanhCong = false,
                ThongBao = thongBao
            };
        }
    }
}