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
    public class DALNhaCungCap
    {
        public List<NhaCungCap> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhaCungCap> list = new List<NhaCungCap>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    NhaCungCap entity = new NhaCungCap();
                    entity.MaNCC = reader.GetString("MaNCC");
                    entity.TenNCC = reader.GetString("TenNCC");
                    entity.DiaChi = reader.GetString("DiaChi");
                    entity.DienThoai = reader.GetString("DienThoai");
                    entity.Email = reader.GetString("Email");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
        public List<NhaCungCap> selectAll()
        {
            String sql = "SELECT * FROM NhaCungCap ";
            return SelectBySql(sql, new List<object>());
        }
    }
}
