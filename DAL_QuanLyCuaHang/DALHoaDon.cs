using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCuaHang;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyCuaHang
{
    public class DALHoaDon
    {
        private readonly string prefix = "HD";


        public string TaoMaTuDong()
        {
            string sql = "SELECT MaHD FROM HoaDon";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
            {
                while (reader.Read())
                {
                    string maHD = reader["MaHD"].ToString();
                    if (maHD.StartsWith(prefix) && int.TryParse(maHD.Substring(prefix.Length), out int number))
                    {
                        existingNumbers.Add(number);
                    }
                }
            }

            int newNumber = 1;
            existingNumbers.Sort();
            foreach (int number in existingNumbers)
            {
                if (number == newNumber)
                    newNumber++;
                else if (number > newNumber)
                    break;
            }

            return $"{prefix}{newNumber:D3}";
        }




        public void Insert(HoaDon hd)
        {
            string sql = @"INSERT INTO HoaDon (MaHD, MaNV, TenKhach, NgayLap) 
                       VALUES (@0, @1, @2, @3)";
            var args = new List<object> { hd.MaHD, hd.MaNV, hd.TenKhach, hd.NgayLap };
            DBUtil.Update(sql, args);
        }

        public bool Exists(string maHD)
        {
            string sql = "SELECT COUNT(*) FROM HoaDon WHERE MaHD = @0";
            var args = new List<object> { maHD };
            return Convert.ToInt32(DBUtil.ScalarQuery(sql, args)) > 0;
        }




        public List<HoaDon> SelectAll(string maNV = null)
        {
            var list = new List<HoaDon>();
            string sql = "  SELECT MaHD, MaNV, TenKhach,NgayLap FROM HoaDon";
            var args = new List<object>();

            if (!string.IsNullOrEmpty(maNV))
            {
                sql += " WHERE MaNV = @0";
                args.Add(maNV);
            }

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                while (reader.Read())
                {
                    var hd = new HoaDon
                    {
                        MaHD = reader["MaHD"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        TenKhach = reader["TenKhach"].ToString(),
                        NgayLap = Convert.ToDateTime(reader["NgayLap"]),

                    };
                    list.Add(hd);
                }
            }

            return list;
        }


        public HoaDon SelectByID(string maHD)
        {
            string sql = "  SELECT MaHD, MaNV, TenKhach,NgayLap FROM HoaDon WHERE MaHD = @0";
            var args = new List<object> { maHD };

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                if (reader.Read())
                {
                    return new HoaDon
                    {
                        MaHD = reader["MaHD"].ToString(),
                        MaNV = reader["MaNV"].ToString(),
                        TenKhach = reader["TenKhach"].ToString(),
                        NgayLap = Convert.ToDateTime(reader["NgayLap"]),

                    };
                }
            }

            return null;
        }
        public void Delete(string maHD)
        {
            try
            {
                string sql = "DELETE  FROM HoaDon WHERE MaHD = @0";
                var args = new List<object> { maHD };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa hóa đơn: " + ex.Message);
            }
        }
    }
}
