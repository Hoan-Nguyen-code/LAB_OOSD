using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyNhanSu.Data
{
    public static class Db
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager
                    .ConnectionStrings["QuanLyNhanSuDB"]
                    .ConnectionString;
            }
        }

        public static SqlConnection OpenConnection()
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            cn.Open();
            return cn;
        }

        public static DataTable Query(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection cn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public static int Execute(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection cn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }

        public static object Scalar(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection cn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
        }

        public static SqlParameter P(string name, object value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);

            if (value is string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return new SqlParameter(name, DBNull.Value);

                value = text.Trim();
            }

            return new SqlParameter(name, value);
        }
    }
}