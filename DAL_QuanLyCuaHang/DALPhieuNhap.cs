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
    public class DALPhieuNhap
    {
        private readonly string prefix = "PN";

       
        public string TaoMaTuDong()
        {
            string sql = "SELECT MaPN FROM PhieuNhap";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
            {
                while (reader.Read())
                {
                    string maHD = reader["MaPN"].ToString();
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
        public PhieuNhap SelectByID(string maPN)
        {
            string sql = "SELECT MaPN, NgayNhap, MaNV, MaNCC FROM PhieuNhap WHERE MaPN = @0";
            var args = new List<object> { maPN };

            using (SqlDataReader reader = DBUtil.Query(sql, args))
            {
                if (reader.Read())
                {
                    return new PhieuNhap
                    {
                        MaPN = reader["MaPN"].ToString(),
                        NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                        MaNV = reader["MaNV"].ToString(),
                        MaNCC = reader["MaNCC"].ToString()
                    };
                }
            }

            return null;
        }


        public List<PhieuNhap> SelectAll(string maNV = null)
        {
            var list = new List<PhieuNhap>();
            string sql = "SELECT MaPN, NgayNhap, MaNV, MaNCC FROM PhieuNhap";
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
                    var pn = new PhieuNhap
                    {
                        MaPN = reader["MaPN"].ToString(),
                        NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                        MaNV = reader["MaNV"].ToString(),
                        MaNCC = reader["MaNCC"].ToString()
                    };
                    list.Add(pn);
                }
            }

            return list;
        }
        public void Insert(PhieuNhap hd)
        {
            string sql = @"INSERT INTO PhieuNhap (MaPN, MaNV, MaNCC, NgayNhap) 
                       VALUES (@0, @1, @2, @3)";
            var args = new List<object> { hd.MaPN, hd.MaNV, hd.MaNCC, hd.NgayNhap };
            DBUtil.Update(sql, args);
        }

        public bool Exists(string maHD)
        {
            string sql = " SELECT COUNT(*) FROM PhieuNhap WHERE MaPN  = @0";
            var args = new List<object> { maHD };
            return Convert.ToInt32(DBUtil.ScalarQuery(sql, args)) > 0;
        }
        public void Delete(string maHD)
        {
            try
            {
                string sql = "DELETE  FROM PhieuNhap WHERE MaPN = @0";
                var args = new List<object> { maHD };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa Phiếu Nhập: " + ex.Message);
            }
        }
    }
}
