using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;

namespace BLL_QuanLyCuaHang
{
    public class BUSHoaDon
    {

        private DALHoaDon dalHoaDon = new DALHoaDon();
        private DALChiTietHoaDon dalChiTiet = new DALChiTietHoaDon();
        public List<HoaDon> GetHoaDonList(string maNV = null)
        {
            try
            {
                return dalHoaDon.SelectAll(maNV);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HoaDon GetHoaDonByID(string maHD)
        {
            try
            {
                return dalHoaDon.SelectByID(maHD);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ThemHoaDon(HoaDon hd)
        {
            if (!dalHoaDon.Exists(hd.MaHD))
            {
                dalHoaDon.Insert(hd);
            }
        }

        public void XoaHoaDon(string maHD)
        {
            try
            {

                dalChiTiet.DeleteByMaHD(maHD);
                dalHoaDon.Delete(maHD);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa hóa đơn: " + ex.Message);
            }
        }
    }
}
