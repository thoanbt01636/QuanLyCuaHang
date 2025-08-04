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

        public void Insert(ChiTietHD ct)
        {
            string sql = @"INSERT INTO ChiTietHD (MaHD, MaSP, SoLuong, DonGia, GiamGia, ThanhTien)
                       VALUES (@0, @1, @2, @3, @4, @5)";
            var args = new List<object>
        {
            ct.MaHD,
            ct.MaSP,
            ct.SoLuong,
            ct.DonGia,
            ct.GiamGia,
            ct.ThanhTien
        };
            DBUtil.Update(sql, args);
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

                    item.MaHD = reader.GetString("MaHD");
                    item.MaSP = reader.GetString("MaSP");
                    item.TenSP = reader.GetString("TenSP");
                    item.SoLuong = reader.GetInt32("SoLuong");
                    item.DonGia = reader.GetDecimal("DonGia");
                    item.GiamGia = reader.GetDecimal("GiamGia");
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
        public List<ChiTietHD> selectAll()
        {



            string sql = @"select  * from ChiTietHD               ";
            List<object> thamSo = new List<object>();

            return SelectBySql(sql, thamSo);
        }
        public List<ChiTietHD> SelectByMaHD(string maHD)
        {

            string sql = @"
                SELECT  ct.MaHD, ct.MaSP, ct.SoLuong, ct.DonGia,ct.GiamGia, sp.TenSP 
                        FROM ChiTietHD ct 
                        INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
						WHERE ct.MaHD = @0";
            List<object> thamSo = new List<object>();
            thamSo.Add(maHD);
            return SelectBySql(sql, thamSo);
        }


        public ChiTietHD kiemtraMaSP(string maHD, string maSP)
        {
            string sql = $"SELECT * FROM ChiTietHD WHERE MaHD = '{maHD}' AND MaSP = '{maSP}'";

            using (SqlDataReader reader = DBUtil.Query(sql, null, CommandType.Text))
            {
                if (reader.Read())
                {
                    return new ChiTietHD()
                    {
                        MaHD = reader["MaHD"].ToString(),
                        MaSP = reader["MaSP"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        DonGia = Convert.ToDecimal(reader["DonGia"])
                    };
                }
            }
            return null;
        }
        public void UpdateChiTiet(ChiTietHD ct)
        {
            string sql = "UPDATE ChiTietHD SET SoLuong = @SoLuong WHERE MaHD = @MaHD AND MaSP = @MaSP";

            using (SqlConnection conn = new SqlConnection(DBUtil.connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                    cmd.Parameters.AddWithValue("@MaHD", ct.MaHD);
                    cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public DataTable GetByMaHD(string maHD)
        {
            string sql = "SELECT MaSP, SoLuong FROM ChiTietHD WHERE MaHD = @0";
            var args = new List<object> { maHD };
            return DBUtil.QueryDataTable(sql, args);
        }

        public void DeleteByMaHD(string maHD)
        {
            string sql = "DELETE FROM ChiTietHD WHERE MaHD = @0";
            var args = new List<object> { maHD };
            DBUtil.Update(sql, args);
        }
        public void DeleteChiTiet(string maHD, string maSP)
        {
            string sql = "DELETE FROM ChiTietHD WHERE MaHD = @0 AND MaSP = @1";
            DBUtil.Update(sql, new List<object> { maHD, maSP });
        }
    }
}
    