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
    public class DALLoaiSanPham
    {
        public List<LoaiSanPham> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<LoaiSanPham> list = new List<LoaiSanPham>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    LoaiSanPham entity = new LoaiSanPham();
                    entity.MaLoai = reader.GetString("MaLoai");
                    entity.TenLoai = reader.GetString("TenLoai");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public List<LoaiSanPham> selectAll()
        {
            String sql = "SELECT * FROM LoaiSanPham";
            return SelectBySql(sql, new List<object>());
        }
        public string genereteMaLoaiSP()
        {
            string prefix = "LSP";
            string sql = "SELECT MaLoai FROM LoaiSanPham";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        string malsp = reader["MaLoai"].ToString();
                        if (malsp.StartsWith(prefix) && int.TryParse(malsp.Substring(prefix.Length), out int number))
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
        public void addLoaiSampham(LoaiSanPham lsp)
        {
            try
            {
                string sql = @"INSERT INTO LoaiSanPham(MaLoai,Tenloai) VALUES(@0,@1)";
                List<object> thamso = new List<object>();
                thamso.Add(lsp.MaLoai);
                thamso.Add(lsp.TenLoai);
                DBUtil.Update(sql, thamso);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void updateLoaiSanPham(LoaiSanPham lsp)
        {
            try
            {
                string sql = @"UPDATE LoaiSanPham
                SET TenLoai=@1
                WHERE  MaLoai=@0";
                List<object> thamso = new List<object>();
                thamso.Add(lsp.MaLoai);
                thamso.Add(lsp.TenLoai);
                DBUtil.Update(sql, thamso);
            }
            catch (Exception) { throw; }
        }
        public void deleteLoaisanpham(string lsp)
        {
            try
            {
                string sql = @"DELETE FROM LoaiSanPham WHERE MaLoai=@0";
                List<object> thamso = new List<object>();
                thamso.Add(lsp);
                DBUtil.Update(sql, thamso);
            }
            catch (Exception) { throw; }
        }
        public List<LoaiSanPham> GetNhanVienByMa(string MaLoai)
        {
            string sql = "SELECT * FROM LoaiSanPham WHERE MaLoai LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object>();
            thamSo.Add(MaLoai);
            return SelectBySql(sql, thamSo);
        }
        public List<LoaiSanPham> GetNhanVienByTen(string TenLoai)
        {
            string sql = "SELECT * FROM LoaiSanPham WHERE TenLoai LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object>();
            thamSo.Add(TenLoai);
            return SelectBySql(sql, thamSo);
        }
    }
    }
