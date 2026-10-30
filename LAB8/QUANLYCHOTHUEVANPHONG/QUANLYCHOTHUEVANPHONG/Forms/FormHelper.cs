using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong.Forms
{
    public static class FormHelper
    {
        public static Control Create(FieldSpec f)
        {
            if (f.Type == FieldType.Date)
            {
                return new DateTimePicker
                {
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "dd/MM/yyyy",
                    Width = 250
                };
            }

            if (f.Type == FieldType.Money ||
                f.Type == FieldType.Integer)
            {
                return new NumericUpDown
                {
                    Minimum = 0,
                    Maximum = 999999999999m,
                    DecimalPlaces =
                        f.Type == FieldType.Money ? 2 : 0,
                    ThousandsSeparator = true,
                    Width = 250
                };
            }

            if (f.Type == FieldType.Choice)
            {
                var cb = new ComboBox
                {
                    DropDownStyle =
                        ComboBoxStyle.DropDownList,
                    Width = 250
                };

                if (f.Choices != null)
                    cb.Items.AddRange(f.Choices);

                if (cb.Items.Count > 0)
                    cb.SelectedIndex = 0;

                return cb;
            }

            return new TextBox { Width = 250 };
        }

        public static object Value(Control c)
        {
            if (c is DateTimePicker)
                return ((DateTimePicker)c).Value.Date;

            if (c is NumericUpDown)
                return ((NumericUpDown)c).Value;

            if (c is ComboBox)
                return Convert.ToString(
                    ((ComboBox)c).SelectedItem);

            return c.Text.Trim();
        }

        public static void Set(Control c, object value)
        {
            if (value == null || value == DBNull.Value)
                return;

            if (c is DateTimePicker)
            {
                ((DateTimePicker)c).Value =
                    Convert.ToDateTime(value);
            }
            else if (c is NumericUpDown)
            {
                var n = (NumericUpDown)c;
                decimal v = Convert.ToDecimal(value);

                if (v >= n.Minimum && v <= n.Maximum)
                    n.Value = v;
            }
            else if (c is ComboBox)
            {
                ((ComboBox)c).SelectedItem =
                    Convert.ToString(value);
            }
            else
            {
                c.Text = Convert.ToString(value);
            }
        }

        public static Dictionary<string, object> Ask(
            string title, params FieldSpec[] fields)
        {
            using (var form = new Form())
            {
                form.Text = title;
                form.Size = new Size(560, 650);
                form.StartPosition =
                    FormStartPosition.CenterParent;
                form.AutoScroll = true;

                var panel = new TableLayoutPanel
                {
                    Dock = DockStyle.Top,
                    AutoSize = true,
                    ColumnCount = 2,
                    Padding = new Padding(14)
                };

                panel.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Absolute, 190));
                panel.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100));

                var controls =
                    new Dictionary<string, Control>();

                foreach (FieldSpec field in fields)
                {
                    int row = panel.RowCount++;
                    panel.RowStyles.Add(
                        new RowStyle(SizeType.Absolute, 39));

                    var label = new Label
                    {
                        Text = field.Label,
                        AutoSize = true,
                        Anchor = AnchorStyles.Left
                    };

                    Control input = Create(field);
                    input.Anchor = AnchorStyles.Left;

                    panel.Controls.Add(label, 0, row);
                    panel.Controls.Add(input, 1, row);

                    controls[field.Name] = input;
                }

                var ok = new Button
                {
                    Text = "Xác nhận",
                    Width = 130,
                    Height = 35,
                    DialogResult = DialogResult.OK
                };

                var cancel = new Button
                {
                    Text = "Hủy",
                    Width = 90,
                    Height = 35,
                    DialogResult = DialogResult.Cancel
                };

                var buttons = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 50
                };
                buttons.Controls.Add(ok);
                buttons.Controls.Add(cancel);

                form.Controls.Add(panel);
                form.Controls.Add(buttons);
                form.AcceptButton = ok;
                form.CancelButton = cancel;

                if (form.ShowDialog() != DialogResult.OK)
                    return null;

                var result =
                    new Dictionary<string, object>();

                foreach (FieldSpec field in fields)
                    result[field.Name] =
                        Value(controls[field.Name]);

                return result;
            }
        }

        public static void Run(Action work)
        {
            try
            {
                work();
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
    }
}