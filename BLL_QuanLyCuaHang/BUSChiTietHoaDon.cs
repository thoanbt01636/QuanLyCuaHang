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
        DALChiTietHoaDon dalChiTietPhieu = new DALChiTietHoaDon();

        public List<ChiTietHD> GetChiTietHDList(string maPhieu)
        {
            if (string.IsNullOrEmpty(maPhieu))
            {
                return new List<ChiTietHD>();
            }

            return dalChiTietPhieu.SelectByMaHD(maPhieu);
        }

        public ChiTietHD KiemtraMaSP(string maHD, string maSP)
        {
            return dalChiTietPhieu.kiemtraMaSP(maHD, maSP);
        }

        public void LuuChiTietHD(ChiTietHD ct)
        {
            dalChiTietPhieu.Insert(ct);
        }
        public void CapNhatChiTietHD(ChiTietHD ct)
        {
            dalChiTietPhieu.UpdateChiTiet(ct);
        }
        public void XoaChiTietHoaDon(string maHD, string maHang)
        {

            dalChiTietPhieu.DeleteChiTiet(maHD, maHang);
        }
    }
    }

