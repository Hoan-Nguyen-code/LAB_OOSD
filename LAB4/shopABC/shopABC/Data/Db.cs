using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace shopABC.Data
{
    public static class Db
    {
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["ShopABCDB"].ConnectionString;

        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                DataTable dt = new DataTable();

                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                return dt;
            }
        }

        public static int Execute(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }

        public static object Scalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
        }
    }
}