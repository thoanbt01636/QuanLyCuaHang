using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;

namespace BLL_QuanLyCuaHang
{
    public class BUSSanPham
    {
        DALSanPham dalSanPham = new DALSanPham();

        public List<SanPham> GetSanPhamList(int trangThai = -1)
        {
            return dalSanPham.selectAll(trangThai);
        }


        public string AddSanPham(SanPham sp)
        {
            try
            {
                sp.MaSP = dalSanPham.generateMaSanPham();
                if (string.IsNullOrEmpty(sp.MaSP))
                {
                    return "Mã sản phẩm không hợp lệ.";
                }

                dalSanPham.addSanPham(sp);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateSanPham(SanPham sp)
        {
            try
            {
                if (string.IsNullOrEmpty(sp.MaSP))
                {
                    return "Mã sản phẩm không hợp lệ.";
                }              

                dalSanPham.updateSanPham(sp);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteSanPham(string maSP)
        {
            try
            {
                if (string.IsNullOrEmpty(maSP))
                {
                    return "Mã sản phẩm không hợp lệ.";
                }

                dalSanPham.deleteSanPham(maSP);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public List<SanPham> TimKiem(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return dalSanPham.selectAll();
                }

                else if (input.StartsWith("SP"))
                    return dalSanPham.GetSanPhamByMa(input);
                else
                    return dalSanPham.GetSanPhamByTen(input);

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm Sản phẩm: " + ex.Message);
            }
        }
    }
}
