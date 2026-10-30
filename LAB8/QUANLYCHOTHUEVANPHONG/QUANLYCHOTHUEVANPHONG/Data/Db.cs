using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyChoThueVanPhong.Data
{
    public static class Db
    {
        private static readonly string Cs =
            ConfigurationManager.ConnectionStrings[
                "QuanLyVanPhongDB"].ConnectionString;

        public static SqlConnection OpenConnection()
        {
            var c = new SqlConnection(Cs);
            c.Open();
            return c;
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(
                name, value ?? DBNull.Value);
        }

        public static DataTable Query(
            string sql, params SqlParameter[] ps)
        {
            using (var c = OpenConnection())
            using (var cmd = new SqlCommand(sql, c))
            using (var da = new SqlDataAdapter(cmd))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int Execute(
            string sql, params SqlParameter[] ps)
        {
            using (var c = OpenConnection())
            using (var cmd = new SqlCommand(sql, c))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        public static object Scalar(
            string sql, params SqlParameter[] ps)
        {
            using (var c = OpenConnection())
            using (var cmd = new SqlCommand(sql, c))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }

        public static object Scalar(
            SqlConnection c, SqlTransaction tx,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, tx))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }

        public static int Execute(
            SqlConnection c, SqlTransaction tx,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, tx))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        public static DataTable Query(
            SqlConnection c, SqlTransaction tx,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, tx))
            using (var da = new SqlDataAdapter(cmd))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static T Transaction<T>(
            Func<SqlConnection, SqlTransaction, T> work)
        {
            using (var c = OpenConnection())
            using (var tx = c.BeginTransaction(
                IsolationLevel.Serializable))
            {
                try
                {
                    T result = work(c, tx);
                    tx.Commit();
                    return result;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}