using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCuaHang;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyCuaHang
{
    public class DALChiTietHoaDon
    {
        public string GenerateMaCTHD()
        {
            string prefix = "CTHD";
            string sql = "SELECT MAX(MaCTHD) FROM ChiTietHD";
            List<object> thamSo = new List<object>();
            object result = DBUtil.ScalarQuery(sql, thamSo);

            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(4);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }
            return $"{prefix}001";
        }

        public List<ChiTietHD> GetChiTietByMaHD(string maHD)
        {
            string sql = @"SELECT cthd.*, sp.TenSP 
                      FROM ChiTietHD cthd
                      INNER JOIN SanPham sp ON cthd.MaSP = sp.MaSP
                      WHERE cthd.MaHD = @0";

            List<object> thamSo = new List<object> { maHD };
            List<ChiTietHD> list = new List<ChiTietHD>();

            using (SqlDataReader reader = DBUtil.Query(sql, thamSo))
            {
                while (reader.Read())
                {
                    ChiTietHD entity = new ChiTietHD
                    {
                        MaHD = reader["MaHD"].ToString(),
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        DonGiaNhap = Convert.ToDecimal(reader["DonGiaNhap"]),
                        ThanhTien = Convert.ToDecimal(reader["SoLuong"]) * Convert.ToDecimal(reader["DonGiaNhap"])
                    };
                    list.Add(entity);
                }
            }
            return list;
        }

        public bool InsertChiTietHD(ChiTietHD cthd)
        {
            try
            {
                string sql = @"INSERT INTO ChiTietHD (MaCTHD, MaHD, MaSP, SoLuong, DonGiaNhap)
                         VALUES (@0, @1, @2, @3, @4)";

                List<object> thamSo = new List<object>
            {
                GenerateMaCTHD(),
                cthd.MaHD,
                cthd.MaSP,
                cthd.SoLuong,
                cthd.DonGiaNhap
            };

                DBUtil.Update(sql, thamSo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateChiTietHD(ChiTietHD cthd)
        {
            try
            {
                string sql = @"UPDATE ChiTietHD 
                         SET SoLuong = @0, 
                             DonGiaNhap = @1
                         WHERE MaCTHD = @2";

                List<object> thamSo = new List<object>
            {
                cthd.SoLuong,
                cthd.DonGiaNhap,
                cthd.MaHD
            };

                DBUtil.Update(sql, thamSo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteChiTietHD(string maCTHD)
        {
            try
            {
                string sql = "DELETE FROM ChiTietHD WHERE MaCTHD = @0";
                List<object> thamSo = new List<object> { maCTHD };
                DBUtil.Update(sql, thamSo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public decimal GetTongTienByMaHD(string maHD)
        {
            string sql = "SELECT SUM(SoLuong * DonGiaNhap) FROM ChiTietHD WHERE MaHD = @0";
            List<object> thamSo = new List<object> { maHD };
            object result = DBUtil.ScalarQuery(sql, thamSo);
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
    }
    }
