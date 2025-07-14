using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCuaHang;
using DAL_QuanLyCuaHang;
namespace BLL_QuanLyCuaHang
{
    public class BUSNhanVien
    {
        DALNhanVien dalNhanVien = new DALNhanVien();

        public NhanVien DangNhap(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            return dalNhanVien.getNhanVien1(username, password);
        }
        public List<NhanVien> GetNhanViensList()
        {
            return dalNhanVien.seletAll();
        }
    }
}
