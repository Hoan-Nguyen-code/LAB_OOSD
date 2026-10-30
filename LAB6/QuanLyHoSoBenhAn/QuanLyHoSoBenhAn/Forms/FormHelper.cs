
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    public static class FormHelper
    {
        public static FlowLayoutPanel TaoGiaoDien(
            Form form, string tieuDe, out DataGridView bang)
        {
            form.Text = tieuDe;
            form.StartPosition = FormStartPosition.CenterParent;
            form.Size = new Size(1180, 720);
            form.MinimumSize = new Size(950, 600);
            form.Font = new Font("Segoe UI", 10F);
            form.BackColor = Color.FromArgb(245, 247, 250);

            var dauTrang = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65
            };

            dauTrang.Controls.Add(new Label
            {
                Text = tieuDe.ToUpper(),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 19F, FontStyle.Bold),
                ForeColor = Color.FromArgb(32, 62, 95)
            });

            var nhapLieu = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 230,
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(15),
                BackColor = Color.White
            };

            bang = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            form.Controls.Add(bang);
            form.Controls.Add(nhapLieu);
            form.Controls.Add(dauTrang);

            return nhapLieu;
        }

        public static TextBox Text(
            FlowLayoutPanel panel, string ten, int rong = 215)
        {
            var txt = new TextBox { Width = rong };
            ThemTruong(panel, ten, txt);
            return txt;
        }

        public static ComboBox Combo(
            FlowLayoutPanel panel, string ten)
        {
            var cbo = new ComboBox
            {
                Width = 215,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            ThemTruong(panel, ten, cbo);
            return cbo;
        }

        public static DateTimePicker Ngay(
            FlowLayoutPanel panel, string ten)
        {
            var ngay = new DateTimePicker
            {
                Width = 215,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                ShowUpDown = false
            };
            ThemTruong(panel, ten, ngay);
            return ngay;
        }

        public static void ThemTruong(
            FlowLayoutPanel panel, string ten, Control control)
        {
            var group = new FlowLayoutPanel
            {
                Width = 355,
                Height = 40,
                WrapContents = false,
                Margin = new Padding(5)
            };

            group.Controls.Add(new Label
            {
                Text = ten,
                Width = 130,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            });

            control.Margin = new Padding(2, 4, 0, 0);
            group.Controls.Add(control);
            panel.Controls.Add(group);
        }

        public static Button Nut(
            FlowLayoutPanel panel, string text, Action action)
        {
            var button = new Button
            {
                Text = text,
                Width = 165,
                Height = 38,
                Margin = new Padding(8),
                BackColor = Color.FromArgb(32, 85, 145),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.Click += (s, e) => Chay(action);
            panel.Controls.Add(button);
            return button;
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
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        public static string Ma(string tienTo)
        {
            return tienTo +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpper();
        }

        public static string Chon(ComboBox cbo)
        {
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue is System.Data.DataRowView)
                throw new Exception("Vui lòng chọn thông tin hợp lệ.");

            return cbo.SelectedValue.ToString();
        }
    }
}
