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
    public class DALNhanVien
    {
        public NhanVien? getNhanVien1(string email, string password)
        {
            string sql = "SELECT TOP 1 * FROM NhanVien WHERE Gmail = @0 AND MatKhau = @1";
            List<object> thamso = new List<object>();
            thamso.Add(email);
            thamso.Add(password);
            SqlDataReader reader = DBUtil.Query(sql, thamso);
            if (reader.Read())
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = reader["MaNV"].ToString();
                nv.TenNV = reader["TenNV"].ToString();
                nv.NgaySinh = DateTime.Parse(reader["NgaySinh"].ToString());
                nv.DienThoai = reader["DienThoai"].ToString();
                nv.Gmail = reader["Gmail"].ToString();
                nv.MatKhau = reader["MatKhau"].ToString();
                nv.ChucVu = bool.Parse(reader["ChucVu"].ToString());
                nv.TrangThai = bool.Parse(reader["TrangThai"].ToString());
                return nv;
            }

            return null;
        }
        public List<NhanVien> seletAll()
        {
            string sql = "SELECT * FROM NhanVien";
            return selectBySql(sql, new List<object>());
        }

        public List<NhanVien> selectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    NhanVien entity = new NhanVien();
                    entity.MaNV = reader.GetString("MaNV");
                    entity.TenNV = reader.GetString("TenNV");
                    entity.Gmail = reader.GetString("Gmail");
                    entity.NgaySinh = reader.GetDateTime("NgaySinh");
                    entity.DienThoai = reader.GetString("DienThoai");                   
                    entity.MatKhau = reader.GetString("MatKhau");
                    entity.ChucVu = reader.GetBoolean("ChucVu");
                    entity.TrangThai = reader.GetBoolean("TrangThai");
                    list.Add(entity);
                }
            }
            catch
            {
                throw;
            }
            return list;
        }
        public string generateMaNhanVien()
        {
            string prefix = "NV";
            string sql = "SELECT MAX(MaNhanVien) FROM NhanVien";
            List<object> thamSo = new List<object>();
            object result = DBUtil.ScalarQuery(sql, thamSo);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(2);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }
    }
}
