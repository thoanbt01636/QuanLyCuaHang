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
    public class DALSanPham
    {

        public List<SanPham> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<SanPham> list = new List<SanPham>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    SanPham entity = new SanPham();
                    entity.MaSP = reader.GetString("MaSP");
                    entity.TenSP = reader.GetString("TenSP");
                    entity.DonGia = reader.GetDecimal("DonGia");
                    entity.MaLoai = reader.GetString("MaLoai");
                    entity.SoLuongTon = reader.GetInt32(reader.GetOrdinal("SoLuongTon"));
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<SanPham> selectAll(int trangThai = -1)
        {
            string sql = "SELECT MaSP, TenSP, DonGia, LoaiSanPham.MaLoai, SoLuongTon, TenLoai " +
                        "FROM SanPham INNER JOIN LoaiSanPham ON SanPham.MaLoai = LoaiSanPham.MaLoai ";
           


            List<object> p = new List<object>();

            if (trangThai > -1)
            {
                sql += "WHERE SanPham.TrangThai = @0";
                p.Add(trangThai);
            }

            return SelectBySql(sql, p);
        }


        public SanPham selectById(string id)
        {
            String sql = "SELECT * FROM SanPham WHERE MaSP=@0";
            List<object> thamSo = new List<object>();
            thamSo.Add(id);
            List<SanPham> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public void addSanPham(SanPham sp)
        {
            try
            {
                string sql = @"INSERT INTO SanPham (MaSP, TenSP, DonGia, MaLoai, SoLuongTon) 
                       VALUES (@0, @1, @2, @3, @4)"; 

                List<object> thamSo = new List<object>
        {
            sp.MaSP,
            sp.TenSP,
            sp.DonGia,
            sp.MaLoai,
            sp.SoLuongTon
        };

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi thêm sản phẩm: " + e.Message);
            }
        }


        public void updateSanPham(SanPham sp)
        {
            try
            {
                string sql = @"UPDATE SanPham 
                       SET TenSP = @1, DonGia = @2, MaLoai = @3, SoLuongTon = @4 
                       WHERE MaSP = @0";

                List<object> thamSo = new List<object>
        {
            sp.MaSP,
            sp.TenSP,
            sp.DonGia,
            sp.MaLoai,
            sp.SoLuongTon
        };

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi cập nhật sản phẩm: " + e.Message);
            }
        }


        public void deleteSanPham(string maSP)
        {
            try
            {
                string sql = "DELETE FROM SanPham WHERE MaSP = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maSP);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public string generateMaSanPham()
        {
            string prefix = "SP";
            string sql = "SELECT MaSP FROM SanPham";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        string masp = reader["MaSP"].ToString();
                        if (masp.StartsWith(prefix) && int.TryParse(masp.Substring(prefix.Length), out int number))
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

                return $"{prefix}{newNumber:D4}";
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SanPham> GetSanPhamByMa(string ma)
        {
            string sql = "SELECT * FROM SanPham WHERE MaSP LIKE '%' + @0 + '%'";
            return SelectBySql(sql, new List<object> { ma });
        }

        public List<SanPham> GetSanPhamByTen(string ten)
        {
            string sql = "SELECT * FROM SanPham WHERE TenSP LIKE '%' + @0 + '%'";
            return SelectBySql(sql, new List<object> { ten });
        }
    }
}
