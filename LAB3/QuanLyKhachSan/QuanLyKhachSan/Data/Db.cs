using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyKhachSan.Data
{
    public static class Db
    {
        private static readonly string ConnectionString =
            ConfigurationManager
                .ConnectionStrings["QuanLyKhachSanDb"]
                .ConnectionString;

        public static SqlConnection OpenConnection()
        {
            SqlConnection connection =
                new SqlConnection(ConnectionString);

            connection.Open();

            return connection;
        }

        public static DataTable Query(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command =
                   new SqlCommand(sql, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                using (SqlDataAdapter adapter =
                       new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
            }
        }

        public static int Execute(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command =
                   new SqlCommand(sql, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                return command.ExecuteNonQuery();
            }
        }

        public static object Scalar(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command =
                   new SqlCommand(sql, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                return command.ExecuteScalar();
            }
        }
    }
}