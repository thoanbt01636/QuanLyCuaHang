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
            string sql = "SELECT MAX(MaLoai) FROM SanPham";
            List<object> thamSo = new List<object>();
            object result = DBUtil.ScalarQuery(sql, thamSo);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(3);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }
        public void addLoaiSampham(LoaiSanPham lsp)
        {
            try
            {
                string sql = @"INSERT INTO SanPham(MaLoai,Tenloai) VALUES(@0,@1)";
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
                string sql = @"UPDATE SanPham
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
                string sql = @"DELETE FROM SanPham WHERE MaLoai=@0";
                List<object> thamso = new List<object>();
                thamso.Add(lsp);
                DBUtil.Update(sql, thamso);
            }
            catch (Exception) { throw; }
        }
        public List<LoaiSanPham> GetNhanVienByMa(string MaLoai)
        {
            string sql = "SELECT * FROM SanPham WHERE MaLoai LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object>();
            thamSo.Add(MaLoai);
            return SelectBySql(sql, thamSo);
        }
        public List<LoaiSanPham> GetNhanVienByTen(string TenLoai)
        {
            string sql = "SELECT * FROM SanPham WHERE TenLoai LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object>();
            thamSo.Add(TenLoai);
            return SelectBySql(sql, thamSo);
        }
    }
    }
