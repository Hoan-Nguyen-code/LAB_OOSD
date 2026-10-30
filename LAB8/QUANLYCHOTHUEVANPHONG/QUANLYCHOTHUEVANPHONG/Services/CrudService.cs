using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public abstract class CrudService
    {
        protected readonly List<TableSpec> _tables =
            new List<TableSpec>();

        public IList<TableSpec> Tables
        {
            get { return _tables.AsReadOnly(); }
        }

        protected void Add(TableSpec spec)
        {
            _tables.Add(spec);
        }

        public virtual DataTable List(TableSpec t)
        {
            return Db.Query(
                "SELECT * FROM dbo.[" + t.Table + "]");
        }

        public virtual void BeforeSave(
            TableSpec t,
            Dictionary<string, object> values,
            bool exists,
            SqlConnection c,
            SqlTransaction tx)
        {
        }

        public virtual void Save(TableSpec t,
            Dictionary<string, object> values)
        {
            if (t.ReadOnly)
                throw new Exception(
                    "Bảng này chỉ xem. Hãy sử dụng nút nghiệp vụ.");

            string key = V.Id(V.S(values, t.Key));

            Db.Transaction<bool>((c, tx) =>
            {
                object found = Db.Scalar(c, tx,
                    "SELECT COUNT(*) FROM dbo.[" + t.Table +
                    "] WITH (UPDLOCK,HOLDLOCK) WHERE [" +
                    t.Key + "]=@id",
                    Db.P("@id", key));

                bool exists = Convert.ToInt32(found) > 0;

                BeforeSave(t, values, exists, c, tx);

                var fields = t.Fields
                    .Where(x => x.Name != t.Key)
                    .ToList();

                var ps = new List<SqlParameter>();

                ps.Add(Db.P("@id", key));

                for (int i = 0; i < fields.Count; i++)
                {
                    string name = fields[i].Name;
                    object value = values[name];

                    if (value is string &&
                        string.IsNullOrWhiteSpace((string)value))
                        value = DBNull.Value;

                    ps.Add(Db.P("@p" + i, value));
                }

                string sql;

                if (exists)
                {
                    string sets = string.Join(",",
                        fields.Select((f, i) =>
                            "[" + f.Name + "]=@p" + i));

                    sql = "UPDATE dbo.[" + t.Table +
                        "] SET " + sets +
                        " WHERE [" + t.Key + "]=@id";
                }
                else
                {
                    string columns = "[" + t.Key + "]," +
                        string.Join(",",
                            fields.Select(f =>
                                "[" + f.Name + "]"));

                    string parameters = "@id," +
                        string.Join(",",
                            fields.Select((f, i) =>
                                "@p" + i));

                    sql = "INSERT INTO dbo.[" + t.Table +
                        "] (" + columns + ") VALUES (" +
                        parameters + ")";
                }

                Db.Execute(c, tx, sql, ps.ToArray());

                return true;
            });
        }

        public virtual void Delete(TableSpec t, string id)
        {
            throw new Exception(
                "Không hỗ trợ xóa trực tiếp để bảo toàn dữ liệu lịch sử.");
        }

        protected static decimal Money(
            SqlConnection c, SqlTransaction tx,
            string sql, params SqlParameter[] ps)
        {
            object o = Db.Scalar(c, tx, sql, ps);
            return o == null || o == DBNull.Value
                ? 0m : Convert.ToDecimal(o);
        }

        protected static int Count(
            SqlConnection c, SqlTransaction tx,
            string sql, params SqlParameter[] ps)
        {
            object o = Db.Scalar(c, tx, sql, ps);
            return Convert.ToInt32(o);
        }
    }
}