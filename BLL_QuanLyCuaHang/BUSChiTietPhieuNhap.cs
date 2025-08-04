using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using static DAL_QuanLyCuaHang.DALChiTietPhieuNhap;

namespace BLL_QuanLyCuaHang
{
    public class BUSChiTietPhieuNhap
    {
        DALChiTietPN daoChiTietPN = new DALChiTietPN();

      
        public List<ChiTietPN> GetChiTietHDList(string maPhieu)
        {
            if (string.IsNullOrEmpty(maPhieu))
            {
                return new List<ChiTietPN>();
            }

            return daoChiTietPN.SelectByMaPN(maPhieu);
        }


        public ChiTietPN KiemtraMaSP(string maHD, string maSP)
        {
            return daoChiTietPN.kiemtraMaSP(maHD, maSP);
        }

        public void LuuChiTietPN(ChiTietPN ct)
        {
            daoChiTietPN.Insert(ct);
        }
        public void CapNhatChiTietPN(ChiTietPN ct)
        {
            daoChiTietPN.UpdateChiTiet(ct);
        }

        public void XoaChiTietHoaDon(string maHD, string maHang)
        {

            daoChiTietPN.DeleteChiTiet(maHD, maHang);
        }
    }
}
