using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCuaHang;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyCuaHang
{
    public class DALChiTietPhieuNhap
    {
        public class DAOChiTietPN
        {
     
            private List<ChiTietPN> SelectBySql(string sql, List<object> args)
            {
                List<ChiTietPN> list = new List<ChiTietPN>();
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    ChiTietPN ct = new ChiTietPN
                    {
                        MaCTPN = reader["MaCTPN"].ToString(),
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

            public List<ChiTietPN> SelectAll()
            {
                string sql = @"SELECT MaCTPN,MaPN, SanPham.MaSP, SanPham.TenSP,  SoLuong  ,DonGiaNhap
                        FROM ChiTietPN INNER JOIN SanPham ON SanPham.MaSP = ChiTietPN.MaSP ";
                return SelectBySql(sql, new List<object>());
            }

            public List<ChiTietPN> SelectByMaPN(string maPN)
            {
                string sql = "SELECT * FROM ChiTietPN WHERE MaPN = @0";
                return SelectBySql(sql, new List<object> { maPN });
            }

            public void AddChiTietPN(ChiTietPN ct)
            {
                string sql = @"INSERT INTO ChiTietPN (MaCTPN, MaPN, MaSP, SoLuong, DonGiaNhap)
                       VALUES (@0, @1, @2, @3, @4)";
                List<object> parameters = new List<object>
        {
            ct.MaCTPN, ct.MaPN, ct.MaSP, ct.SoLuong, ct.DonGiaNhap
        };
                DBUtil.Update(sql, parameters);
            }




            public void UpdateChiTietPN(ChiTietPN ct)
            {
                string sql = @"UPDATE ChiTietPN 
                       SET MaPN = @1, MaSP = @2, SoLuong = @3, DonGiaNhap = @4 
                       WHERE MaCTPN = @0";
                List<object> parameters = new List<object>
        {
            ct.MaCTPN, ct.MaPN, ct.MaSP, ct.SoLuong, ct.DonGiaNhap
        };
                DBUtil.Update(sql, parameters);
            }

            public void DeleteChiTietPN(string maCTPN)
            {
                string sql = "DELETE FROM ChiTietPN WHERE MaCTPN = @0";
                DBUtil.Update(sql, new List<object> { maCTPN });
            }

            public string GenerateMaCTPN()
            {
                string prefix = "CTPN";
                string sql = "SELECT MAX(MaCTPN) FROM ChiTietPN";
                object result = DBUtil.ScalarQuery(sql, new List<object>());
                if (result != null && result.ToString().StartsWith(prefix))
                {
                    string maxCode = result.ToString().Substring(prefix.Length);
                    int newNumber = int.Parse(maxCode) + 1;
                    return $"{prefix}{newNumber:D3}";
                }
                return $"{prefix}001";
            }
        }


    }
}
