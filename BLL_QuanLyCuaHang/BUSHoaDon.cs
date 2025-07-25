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

        public string Insert(HoaDon hd)
        {
            try
            {
                if (string.IsNullOrEmpty(hd.MaNV))
                    return "Vui lòng chọn nhân viên";
                if (hd.NgayLap > DateTime.Now)
                    return "Ngày lập không hợp lệ";

                hd.MaHD = dalHoaDon.TaoMaTuDong();
                dalHoaDon.Insert(hd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string TaoMaTuDong()
        {
            return dalHoaDon.TaoMaTuDong();
        }
        public string Update(HoaDon hd)
        {
            try
            {
                if (string.IsNullOrEmpty(hd.MaHD))
                    return "Mã hóa đơn không hợp lệ";
                if (string.IsNullOrEmpty(hd.MaNV))
                    return "Vui lòng chọn nhân viên";
                if (hd.NgayLap > DateTime.Now)
                    return "Ngày lập không hợp lệ";

                dalHoaDon.Update(hd);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(string maHD)
        {
            try
            {
                if (string.IsNullOrEmpty(maHD))
                    return "Mã hóa đơn không hợp lệ";

                dalHoaDon.Delete(maHD);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

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
    }
    }
