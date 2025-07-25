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
        public string TaoMaTuDong()
        {
            string prefix = "NV";
            string sql = "SELECT MaNV FROM NhanVien";
            List<object> args = new List<object>();
            List<int> existingNumbers = new List<int>();

            try
            {
                using (SqlDataReader reader = DBUtil.Query(sql, args, CommandType.Text))
                {
                    while (reader.Read())
                    {
                        string maNV = reader["MaNV"].ToString();
                        if (maNV.StartsWith(prefix) && int.TryParse(maNV.Substring(prefix.Length), out int number))
                        {
                            existingNumbers.Add(number);
                        }
                    }
                }

                int newNumber = 1;
                existingNumbers.Sort();

                foreach (int number in existingNumbers)
                {
                    if (number == newNumber)
                        newNumber++;
                    else if (number > newNumber)
                        break;
                }

                return $"{prefix}{newNumber:D3}";
            }
            catch (Exception)
            {
                throw;
            }
        }
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
        public void ResetMatKhau(string mk, string email)
        {
            try
            {
                string sql = "UPDATE NhanVien SET MatKhau = @0 WHERE Gmail = @1";
                List<object> thamSo = new List<object>();
                thamSo.Add(mk);
                thamSo.Add(email);
                DBUtil.Update(sql, thamSo);

            }
            catch (Exception e)
            {
                throw;
            }
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
            string sql = "SELECT MAX(MaNV) FROM NhanVien";
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
        public void addNhanVien(NhanVien nv)
        {
            try
            {
                string sql = @"INSERT INTO NhanVien 
                       (MaNV, TenNV, NgaySinh, DienThoai, ChucVu, Gmail, MatKhau, TrangThai)
                       VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";

                List<object> thamSo = new List<object>
        {
            nv.MaNV,
            nv.TenNV,
            nv.NgaySinh,
            nv.DienThoai,
            nv.ChucVu,
            nv.Gmail,
            nv.MatKhau,
            nv.TrangThai
        };

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm nhân viên.", ex);
            }
        }
        public void updateNhanVien(NhanVien nv)
        {
            try
            {
                string sql = @"UPDATE NhanVien
                       SET TenNV = @1,
                           NgaySinh = @2,
                           DienThoai = @3,
                           ChucVu = @4,
                           Gmail = @5,
                           MatKhau = @6,
                           TrangThai = @7
                       WHERE MaNV = @0";

                List<object> thamSo = new List<object>
        {
            nv.MaNV,
            nv.TenNV,
            nv.NgaySinh,
            nv.DienThoai,
            nv.ChucVu,
            nv.Gmail,
            nv.MatKhau,
            nv.TrangThai
        };

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật nhân viên.", ex);
            }
        }
        public void deleteNhanVien(string maNv)
        {
            try
            {
                string sql = "DELETE FROM NhanVien WHERE MaNV = @0";
                List<object> thamSo = new List<object> { maNv };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa nhân viên.", ex);
            }
        }

    }
}
