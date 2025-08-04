using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using Microsoft.Data.SqlClient;
using static DAL_QuanLyCuaHang.DALChiTietPhieuNhap;

namespace BLL_QuanLyCuaHang
{
    public class BUSPhieuNhap
    {
        DALPhieuNhap dalPhieuNhap = new DALPhieuNhap();
        DALChiTietPN dalChiTiet = new DALChiTietPN();

        public List<PhieuNhap> GetPhieuNhapList(string maNV = null)
        {
            try
            {
                return dalPhieuNhap.SelectAll(maNV);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public PhieuNhap GetPhieuNhapByID(string maHD)
        {
            try
            {
                return dalPhieuNhap.SelectByID(maHD);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ThemPhieuNhap(PhieuNhap pn)
        {
            if (!dalPhieuNhap.Exists(pn.MaPN))
            {
                dalPhieuNhap.Insert(pn);
            }

        }
        public void XoaPhieuNhap(string maPN)
        {
            try
            {
               dalChiTiet.DeleteByMaPN(maPN);
                dalPhieuNhap.Delete(maPN);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa Phiếu nhập: " + ex.Message);
            }
        }
    }
}
