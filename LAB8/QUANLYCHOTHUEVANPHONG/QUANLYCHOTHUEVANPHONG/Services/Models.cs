using System;
using System.Collections.Generic;

namespace QuanLyChoThueVanPhong.Services
{
    public enum FieldType
    {
        Text,
        Date,
        Money,
        Integer,
        Choice
    }

    public class FieldSpec
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public FieldType Type { get; set; }
        public string[] Choices { get; set; }

        public FieldSpec(string name, string label,
            FieldType type = FieldType.Text,
            params string[] choices)
        {
            Name = name;
            Label = label;
            Type = type;
            Choices = choices;
        }
    }

    public class TableSpec
    {
        public string Title { get; set; }
        public string Table { get; set; }
        public string Key { get; set; }
        public bool ReadOnly { get; set; }
        public List<FieldSpec> Fields { get; set; }

        public TableSpec(string title, string table,
            string key, bool readOnly,
            params FieldSpec[] fields)
        {
            Title = title;
            Table = table;
            Key = key;
            ReadOnly = readOnly;
            Fields = new List<FieldSpec>(fields);
        }
    }

    public static class F
    {
        public static FieldSpec T(string name,
            string label = null)
        {
            return new FieldSpec(
                name, label ?? name, FieldType.Text);
        }

        public static FieldSpec D(string name,
            string label = null)
        {
            return new FieldSpec(
                name, label ?? name, FieldType.Date);
        }

        public static FieldSpec M(string name,
            string label = null)
        {
            return new FieldSpec(
                name, label ?? name, FieldType.Money);
        }

        public static FieldSpec I(string name,
            string label = null)
        {
            return new FieldSpec(
                name, label ?? name, FieldType.Integer);
        }

        public static FieldSpec C(string name,
            params string[] values)
        {
            return new FieldSpec(
                name, name, FieldType.Choice, values);
        }
    }

    public static class V
    {
        public static string S(
            Dictionary<string, object> d, string key)
        {
            return Convert.ToString(d[key]).Trim();
        }

        public static DateTime D(
            Dictionary<string, object> d, string key)
        {
            return Convert.ToDateTime(d[key]).Date;
        }

        public static decimal M(
            Dictionary<string, object> d, string key)
        {
            return Convert.ToDecimal(d[key]);
        }

        public static int I(
            Dictionary<string, object> d, string key)
        {
            return Convert.ToInt32(d[key]);
        }

        public static void Require(
            string value, string field)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception(
                    "Không được để trống: " + field);
        }

        public static string Id(string value)
        {
            Require(value, "Mã định danh");

            if (value.Length > 20)
                throw new Exception(
                    "Mã định danh tối đa 20 ký tự.");

            return value;
        }
    }
}