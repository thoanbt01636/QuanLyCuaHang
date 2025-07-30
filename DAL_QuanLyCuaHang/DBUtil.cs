using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace DAL_QuanLyCuaHang
{
    public class DBUtil
    {

        public static string connString = @"Data Source=DESKTOP-KH2OE1U\SQLEXPRESS;Initial Catalog=QuanLyCuaHang;Integrated Security=True;Trust Server Certificate=True";
        public static SqlCommand GetCommand(string sql, List<object> args, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;

            for (int i = 0; i < args.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@{i}", args[i]);
            }


            return cmd;
        }
        public static SqlCommand GetCommand(string sql, Dictionary<string, object> args, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = cmdType;

            foreach (var param in args)
            {
                string paramName = param.Key.StartsWith("@") ? param.Key : "@" + param.Key;
                cmd.Parameters.AddWithValue(paramName, param.Value);
            }

            return cmd;
        }



        public static void Update(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            SqlCommand cmd = GetCommand(sql, args, cmdType);
            cmd.Connection.Open();
            cmd.Transaction = cmd.Connection.BeginTransaction();
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();

            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                throw;
            }

        }
        public static SqlDataReader Query(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                Console.WriteLine("SQL = " + cmd.CommandText);
                cmd.Connection.Open();
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực hiện câu lệnh SQL3: " + ex.Message);
            }
        }

        public static object ScalarQuery(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            try
            {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                cmd.Connection.Open();
                return cmd.ExecuteScalar();
            }

            catch (Exception)
            {
                throw;
            }
        }
        public static object ScalarQuery1(string sql, Dictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        public static SqlCommand GetCommand1(string sql, Dictionary<string, object> args, CommandType cmdType)
        {
            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandType = cmdType;

            foreach (var param in args)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }

            return cmd;
        }

        public static T Value<T>(string sql, List<object> args, CommandType cmdType = CommandType.Text) where T : new()
        {
            try
            {
                SqlCommand cmd = GetCommand(sql, args, cmdType);
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    T result = new T();
                    Type type = typeof(T);

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        PropertyInfo propertyInfo = type.GetProperty(columnName);

                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            if (value != null)
                                propertyInfo.SetValue(result, Convert.ChangeType(value, propertyInfo.PropertyType));
                        }
                    }

                    return result;
                }

                return default;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }

    }
}
