using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using static BLL_QuanLyCuaHang.BUSChiTietHoaDon;

namespace BLL_QuanLyCuaHang
{
    public class BUSChiTietHoaDon
    {
        public class BLLChiTietHD
        {
            private DALChiTietHoaDon dalChiTietHD = new DALChiTietHoaDon();

            public List<ChiTietHD> GetChiTietHDByMaHD(string maHD)
            {
                return dalChiTietHD.GetChiTietByMaHD(maHD);
            }

            public bool ThemChiTietHD(ChiTietHD cthd)
            {
                return dalChiTietHD.InsertChiTietHD(cthd);
            }

            public bool CapNhatChiTietHD(ChiTietHD cthd)
            {
                return dalChiTietHD.UpdateChiTietHD(cthd);
            }

            public bool XoaChiTietHD(string maCTHD)
            {
                return dalChiTietHD.DeleteChiTietHD(maCTHD);
            }

            public decimal TinhTongTien(string maHD)
            {
                return dalChiTietHD.GetTongTienByMaHD(maHD);
            }

            public decimal TinhThanhTien(int soLuong, decimal donGia)
            {
                return soLuong * donGia;
            }

            public decimal TinhTongTienSauGiamGia(decimal tongTien, decimal phanTramGiamGia)
            {
                return tongTien * (1 - phanTramGiamGia / 100);
            }
        }
    }
}
