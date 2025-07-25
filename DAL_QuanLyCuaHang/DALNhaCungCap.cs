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
    public class DALNhaCungCap
    {
        public List<NhaCungCap> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhaCungCap> list = new List<NhaCungCap>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    NhaCungCap entity = new NhaCungCap();
                    entity.MaNCC = reader.GetString("MaNCC");
                    entity.TenNCC = reader.GetString("TenNCC");
                    entity.DiaChi = reader.GetString("DiaChi");
                    entity.DienThoai = reader.GetString("DienThoai");
                    entity.Email = reader.GetString("Email");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public List<NhaCungCap> selectAll()
        {
            String sql = "SELECT * FROM NhaCungCap ";
            return SelectBySql(sql, new List<object>());
        }
        public string generateMaNCC()
        {
            string prefix = "NCC";
            string sql = "SELECT MaNCC FROM NhaCungCap";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        string mancc = reader["MaNCC"].ToString();
                        if (mancc.StartsWith(prefix) && int.TryParse(mancc.Substring(prefix.Length), out int number))
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

        public void addNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                string sql = @"INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai, Email) 
                       VALUES (@0, @1, @2, @3, @4)";
                List<object> thamso = new List<object>
        {
            ncc.MaNCC,
            ncc.TenNCC,
            ncc.DiaChi,
            ncc.DienThoai,
            ncc.Email
        };
                DBUtil.Update(sql, thamso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                string sql = @"UPDATE NhaCungCap
                       SET TenNCC = @1, DiaChi = @2, DienThoai = @3, Email = @4
                       WHERE MaNCC = @0";
                List<object> thamso = new List<object>
        {
            ncc.MaNCC,
            ncc.TenNCC,
            ncc.DiaChi,
            ncc.DienThoai,
            ncc.Email
        };
                DBUtil.Update(sql, thamso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void deleteNhaCungCap(string maNCC)
        {
            try
            {
                string sql = @"DELETE FROM NhaCungCap WHERE MaNCC = @0";
                List<object> thamso = new List<object> { maNCC };
                DBUtil.Update(sql, thamso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NhaCungCap> GetNhaCungCapByMa(string maNCC)
        {
            string sql = "SELECT * FROM NhaCungCap WHERE MaNCC LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object> { maNCC };
            return SelectBySql(sql, thamSo);
        }

        public List<NhaCungCap> GetNhaCungCapByTen(string tenNCC)
        {
            string sql = "SELECT * FROM NhaCungCap WHERE TenNCC LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object> { tenNCC };
            return SelectBySql(sql, thamSo);
        }

    }
}
