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
        public string TaoMaTuDong()
        {
            return dalNhanVien.TaoMaTuDong();
        }
        public List<NhanVien> GetNhanViensList()
        {
            return dalNhanVien.seletAll();
        }
        public bool ResetMatKhau(string email, string mk)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mk))
                {
                    return false;
                }
                dalNhanVien.ResetMatKhau(mk, email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string UpdateNhanVien(NhanVien nv)
        {
            try
            {
                if (string.IsNullOrEmpty(nv.MaNV))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalNhanVien.updateNhanVien(nv);
                return string.Empty;
            }
            catch (Exception e)
            {
                return "Lỗi: " + e.Message;
            }
        }

        public string AddNhanVien(NhanVien nv)
        {
            try
            {
                nv.MaNV = dalNhanVien.TaoMaTuDong();
                if (string.IsNullOrEmpty(nv.MaNV))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalNhanVien.addNhanVien(nv);
                return string.Empty;
            }
            catch (Exception e)
            {
                return "Lỗi: " + e.Message;
            }
        }

        public string DeleteNhanVien(string maNv)
        {
            try
            {
                if (string.IsNullOrEmpty(maNv))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalNhanVien.deleteNhanVien(maNv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public List<NhanVien> LayTenNV(string manv)
        {

            try
            {
                if (string.IsNullOrEmpty(manv))
                {
                    return dalNhanVien.seletAll();
                }

                else if (manv.StartsWith("NV"))
                {
                    return dalNhanVien.GetNhanVienByMa(manv);
                }
                return new List<NhanVien>();

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm nhân viên: " + ex.Message);
            }
        }
    }
}
