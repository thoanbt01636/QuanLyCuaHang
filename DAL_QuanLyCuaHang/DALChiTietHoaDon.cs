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
    public class DALChiTietHoaDon
    {

        public string GenerateChiTietID()
        {
            string prefix = "CTHD";
            string sql = "SELECT MAX(MaCTHD) FROM ChiTietHD";
            object result = DBUtil.ScalarQuery(sql, new List<object>());

            if (result != null && result != DBNull.Value && result.ToString().StartsWith(prefix))
            {
                int next = int.Parse(result.ToString().Substring(prefix.Length)) + 1;
                return $"{prefix}{next:D3}";
            }

            return $"{prefix}001";
        }


        public List<ChiTietHD> SelectBySql(string sql, List<object> args)
        {
            var list = new List<ChiTietHD>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    ChiTietHD item = new ChiTietHD();
                    item.MaCTHD = reader.GetString("MaCTHD");
                    item.MaHD = reader.GetString("MaHD");
                    item.MaSP = reader.GetString("MaSP");
                    item.TenSP = reader.GetString("TenSP");
                    item.SoLuong = reader.GetInt32("SoLuong");
                    item.DonGia = reader.GetDecimal("DonGia");
                   
                    list.Add(item);
                }
                reader.Close();
            }
            catch
            {
                throw;
            }
            return list;
        }

        public List<ChiTietHD> SelectByMaHD(string maHD)
        {
            


            string sql = @"
                SELECT ct.MaCTHD, ct.MaHD, ct.MaSP, ct.SoLuong, ct.DonGia, sp.TenSP 
                        FROM ChiTietHD ct 
                        INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
						WHERE ct.MaHD = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(maHD);
            return SelectBySql(sql, thamSo);
        }

        public void InsertChiTiet(ChiTietHD ct)
        {
            try
            {
                string sql = @"INSERT INTO ChiTietHD (MaCTHD, MaHD, MaSP, SoLuong, DonGia)
                                VALUES (@0, @1, @2, @3, @4)";
                List<object> thamSo = new List<object>();
                thamSo.Add(ct.MaCTHD);
                thamSo.Add(ct.MaHD);
                thamSo.Add(ct.MaSP);
                thamSo.Add(ct.SoLuong);
                thamSo.Add(ct.DonGia);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void InsertListChiTiet(List<ChiTietHD> items)
        {
            try
            {
                foreach (ChiTietHD item in items)
                {
                    InsertChiTiet(item);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void UpdateSoLuong(ChiTietHD ct)
        {
            try
            {
                string sql = @"UPDATE ChiTietHD 
                   SET SoLuong = @1 
                   WHERE MaCTHD = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(ct.MaCTHD);
                thamSo.Add(ct.SoLuong);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        
        public void deleteChiTietHD(string Id)
        {
            try
            {
                string sql = "DELETE FROM ChiTietHD WHERE MaCTHD = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(Id);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
    