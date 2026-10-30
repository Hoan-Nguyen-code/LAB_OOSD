using System;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    internal static class FormHelper
    {
        public static void Nap(
            ComboBox cbo,
            DataTable dt,
            string display,
            string value)
        {
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.DataSource = dt;
        }

        public static string Gia(ComboBox cbo)
        {
            return cbo.SelectedValue == null
                ? ""
                : cbo.SelectedValue.ToString();
        }

        public static string O(DataGridView dgv, string cot)
        {
            if (dgv.CurrentRow == null)
                return "";

            return Convert.ToString(
                dgv.CurrentRow.Cells[cot].Value);
        }

        public static bool Bao(KetQuaXuLy ketQua)
        {
            MessageBox.Show(
                ketQua.ThongBao,
                ketQua.ThanhCong
                    ? "Thông báo"
                    : "Không thực hiện được",
                MessageBoxButtons.OK,
                ketQua.ThanhCong
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Warning
            );

            return ketQua.ThanhCong;
        }

        public static void Chay(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi xử lý",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}