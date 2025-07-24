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
    public class DALPhieuNhap
    {
        public List<PhieuNhap> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<PhieuNhap> list = new List<PhieuNhap>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    PhieuNhap entity = new PhieuNhap();
                    entity.MaPN = reader.GetString("MaPN");
                    entity.NgayNhap = reader.GetDateTime("NgayNhap");
                    entity.MaNV = reader.GetString("MaNV");
                    entity.MaNCC = reader.GetString("MaNCC");
                    entity.TrangThai = reader.GetBoolean("TrangThai");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<PhieuNhap> selectAll()
        {
            string sql = "SELECT * FROM PhieuNhap";
            return SelectBySql(sql, new List<object>());
        }

        public List<PhieuNhap> GetPhieuNhapByMa(string maPN)
        {
            string sql = "SELECT * FROM PhieuNhap WHERE MaPN LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object> { maPN };
            return SelectBySql(sql, thamSo);
        }

        public List<PhieuNhap> GetPhieuNhapByMaNCC(string maNCC)
        {
            string sql = "SELECT * FROM PhieuNhap WHERE MaNCC LIKE '%' + @0 + '%'";
            List<object> thamSo = new List<object> { maNCC };
            return SelectBySql(sql, thamSo);
        }

        public void updatePhieuNhap(PhieuNhap pn)
        {
            try
            {
                string sql = @"UPDATE PhieuNhap 
                       SET MaNV = @1, MaNCC = @2, NgayNhap = @3, TrangThai = @4 
                       WHERE MaPN = @0";
                List<object> thamSo = new List<object>
        {
            pn.MaPN,
            pn.MaNV,
            pn.MaNCC,
            pn.NgayNhap,
            pn.TrangThai
        };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string generateMaPN()
        {
            string prefix = "PN";
            string sql = "SELECT MAX(MaPN) FROM PhieuNhap";
            object result = DBUtil.ScalarQuery(sql, new List<object>());

            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(prefix.Length);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }

        public void addPhieuNhap(PhieuNhap pn)
        {
            try
            {
                string sql = @"INSERT INTO PhieuNhap (MaPN, MaNV, MaNCC, NgayNhap, TrangThai) 
                       VALUES (@0, @1, @2, @3, @4)";
                List<object> thamSo = new List<object>
        {
            pn.MaPN,
            pn.MaNV,
            pn.MaNCC,
            pn.NgayNhap,
            pn.TrangThai
        };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void deletePhieuNhap(string maPN)
        {
            try
            {
                string sql = "DELETE FROM PhieuNhap WHERE MaPN = @0";
                List<object> thamSo = new List<object> { maPN };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
