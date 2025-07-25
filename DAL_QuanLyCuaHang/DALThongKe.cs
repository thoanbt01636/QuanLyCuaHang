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
    public class DALThongKe
    {
            public List<thongkeNV> GetDoanhThuTheoNhanVien(string maNV, DateTime tuNgay, DateTime denNgay)
            {
                string sql = "TKDoanhThuTheoNhanVien";
                var thamSo = new Dictionary<string, object>
        {
            { "@MaNV", maNV },
            { "@TuNgay", tuNgay },
            { "@DenNgay", denNgay }
        };

                List<thongkeNV> result = new List<thongkeNV>();

                using (SqlConnection conn = DBUtil.GetConnection())
                {
                    SqlCommand cmd = DBUtil.GetCommand(sql, thamSo, CommandType.StoredProcedure);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                            thongkeNV tk = new thongkeNV();
                            tk.MaNV = reader["MaNV"]?.ToString();
                            tk.TenNV = reader["TenNV"]?.ToString();
                            tk.SoLy = reader["SoLuongBan"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongBan"]) : 0;
                            tk.SoLuongPhieu = reader["SoLuongHoaDon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongHoaDon"]) : 0;
                            tk.NgayBan = reader["NgayBan"] != DBNull.Value ? Convert.ToDateTime(reader["NgayBan"]).ToString("dd/MM/yyyy") : "";
                            tk.TongTien = reader["TongTien"] != DBNull.Value ? Convert.ToDecimal(reader["TongTien"]).ToString("#,0") : "0";                           
                            result.Add(tk);
                            }
                        }
                    }
                }
                return result;
            }

            public List<thongkeSP> GetDoanhThuTheoLoaiSP(string maLoaiSP, DateTime tuNgay, DateTime denNgay)
            {
                string sql = "TKDoanhThuTheoLoaiSP";
                var thamSo = new Dictionary<string, object>
        {
            { "@MaLoai", maLoaiSP },
            { "@TuNgay", tuNgay },
            { "@DenNgay", denNgay }
        };

                List<thongkeSP> result = new List<thongkeSP>();

                using (SqlConnection conn = DBUtil.GetConnection())
                {
                    SqlCommand cmd = DBUtil.GetCommand(sql, thamSo, CommandType.StoredProcedure);
                    cmd.Connection = conn;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                        while (reader.Read())
                        {
                            thongkeSP tksp = new thongkeSP();
                            tksp.MaSP = reader["MaSP"]?.ToString();
                            tksp.TenSP = reader["TenSP"]?.ToString();
                            tksp.SoLy = reader["SoLuongBan"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongBan"]) : 0;
                            tksp.SoLuongPhieu = reader["SoLuongHoaDon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongHoaDon"]) : 0;
                            tksp.NgayBan = reader["NgayBan"] != DBNull.Value ? Convert.ToDateTime(reader["NgayBan"]).ToString("dd/MM/yyyy") : "";
                            tksp.TongTien = reader["TongTien"] != DBNull.Value ? Convert.ToDecimal(reader["TongTien"]).ToString("#,0") : "0";

                            result.Add(tksp);
                        }

                    }
                }
                }
                return result;
            }
        }
    }
