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
    public class DALChiTietPhieuNhap
    {
        public class DALChiTietPN
        {
     
            private List<ChiTietPN> SelectBySql(string sql, List<object> args)
            {
                List<ChiTietPN> list = new List<ChiTietPN>();
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    ChiTietPN ct = new ChiTietPN
                    {
                      
                        MaPN = reader["MaPN"].ToString(),
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        DonGiaNhap = Convert.ToDecimal(reader["DonGiaNhap"])
                    };
                    list.Add(ct);
                }
                return list;
            }

           

            public List<ChiTietPN> SelectByMaPN(string maPN)
            {

                string sql = @"
                SELECT  ct.MaPN, ct.MaSP, ct.SoLuong, ct.DonGiaNhap, sp.TenSP 
                                FROM ChiTietPN ct 
                                INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
			                    WHERE ct.MaPN =  @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(maPN);
                return SelectBySql(sql, thamSo);
            }
            public ChiTietPN kiemtraMaSP(string maPN, string maSP)
            {
                string sql = $"SELECT * FROM ChiTietPN WHERE MaPN = '{maPN}' AND MaSP = '{maSP}'";

                using (SqlDataReader reader = DBUtil.Query(sql, null, CommandType.Text))
                {
                    if (reader.Read())
                    {
                        return new ChiTietPN()
                        {
                            MaPN = reader["MaPN"].ToString(),
                            MaSP = reader["MaSP"].ToString(),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            DonGiaNhap = Convert.ToDecimal(reader["DonGia"])
                        };
                    }
                }
                return null;
            }
            public void Insert(ChiTietPN ct)
            {
                string sql = @" INSERT INTO ChiTietPN (MaPN, MaSP, SoLuong, DonGiaNhap, ThanhTien)
                       VALUES (@0, @1, @2, @3, @4)";
                var args = new List<object>
        {
            ct.MaPN,
            ct.MaSP,
            ct.SoLuong,
            ct.DonGiaNhap,        
            ct.ThanhTien
        };
                DBUtil.Update(sql, args);
            }

            public void DeleteByMaPN(string maPN)
            {
                string sql = "DELETE FROM ChiTietPN WHERE MaPN = @0";
                var args = new List<object> { maPN };
                DBUtil.Update(sql, args);
            }
            public void UpdateChiTiet(ChiTietPN ct)
            {
                string sql = " UPDATE ChiTietPN SET SoLuong = @SoLuong WHERE MaPN = @MaPN AND MaSP = @MaSP";

                using (SqlConnection conn = new SqlConnection(DBUtil.connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                        cmd.Parameters.AddWithValue("@MaPN", ct.MaPN);
                        cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            public void DeleteChiTiet(string maHD, string maSP)
            {
                string sql = "DELETE FROM ChiTietPN WHERE MaPN = @0 AND MaSP = @1";
                DBUtil.Update(sql, new List<object> { maHD, maSP });
            }
        }
    }
}
