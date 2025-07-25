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
        public List<PhieuNhap> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<PhieuNhap> list = new List<PhieuNhap>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    PhieuNhap entity = new PhieuNhap();
                    entity.MaPN = reader.GetString("MaPN");
                    entity.NgayNhap = reader.GetDateTime("NgayNhap");
                    entity.MaNV = reader.GetString("MaNV");
                    entity.MaNCC = reader.GetString("MaNCC");
                    entity.TrangThai = reader.GetBoolean("TrangThai");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<PhieuNhap> selectAll()
        {
            string sql = "SELECT * FROM PhieuNhap";
            return SelectBySql(sql, new List<object>());
        }

        public List<PhieuNhap> GetPhieuNhapByMa(string maPN)
        {
            string sql = "SELECT * FROM PhieuNhap WHERE MaPN LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object> { maPN };
            return SelectBySql(sql, thamSo);
        }

        public List<PhieuNhap> GetPhieuNhapByMaNCC(string maNCC)
        {
            string sql = "SELECT * FROM PhieuNhap WHERE MaNCC LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object> { maNCC };
            return SelectBySql(sql, thamSo);
        }

        public void updatePhieuNhap(PhieuNhap pn)
        {
            try
            {
                string sql = @"UPDATE PhieuNhap 
                       SET MaNV = @1, MaNCC = @2, NgayNhap = @3, TrangThai = @4 
                       WHERE MaPN = @0";
                List<object> thamSo = new List<object>
        {
            pn.MaPN,
            pn.MaNV,
            pn.MaNCC,
            pn.NgayNhap,
            pn.TrangThai
        };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string generateMaPN()
        {
            string prefix = "PN";
            string sql = "SELECT MaPN FROM PhieuNhap";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        string maPN = reader["MaPN"].ToString();
                        if (maPN.StartsWith(prefix) && int.TryParse(maPN.Substring(prefix.Length), out int number))
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
            catch (Exception)
            {
                throw;
            }
        }

        public void addPhieuNhap(PhieuNhap pn)
        {
            try
            {
                string sql = @"INSERT INTO PhieuNhap (MaPN, MaNV, MaNCC, NgayNhap, TrangThai) 
                       VALUES (@0, @1, @2, @3, @4)";
                List<object> thamSo = new List<object>
        {
            pn.MaPN,
            pn.MaNV,
            pn.MaNCC,
            pn.NgayNhap,
            pn.TrangThai
        };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void deletePhieuNhap(string maPN)
        {
            try
            {
                string sql = "DELETE FROM PhieuNhap WHERE MaPN = @0";
                List<object> thamSo = new List<object> { maPN };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
