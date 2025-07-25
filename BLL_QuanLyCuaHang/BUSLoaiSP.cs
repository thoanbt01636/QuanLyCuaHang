using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;

namespace BLL_QuanLyCuaHang
{
    public class BUSLoaiSP
    {
        DALLoaiSanPham dalloaisp = new DALLoaiSanPham();
        public List<LoaiSanPham> GetLoaiSanPhamList()
        {
            return dalloaisp.selectAll();
        }
        public string TaoMaTuDong()
        {
            return dalloaisp.genereteMaLoaiSP();
        }
        public string AddLoaiSanPham(LoaiSanPham lsp)
        {
            try
            {
                lsp.MaLoai = dalloaisp.genereteMaLoaiSP();
                if (string.IsNullOrEmpty(lsp.MaLoai))
                {
                    return "Mã Loại không đúng";
                }
                dalloaisp.addLoaiSampham(lsp);
                return string.Empty;
            }
            catch (Exception ex) { return "Lỗi: " + ex.Message; }
        }
        public string UpdateLoaiSanPham(LoaiSanPham lsp)
        {
            try
            {
                if (string.IsNullOrEmpty(lsp.MaLoai))
                {
                    return "Mã Loại không đúng";
                }
                dalloaisp.updateLoaiSanPham(lsp);
                return string.Empty;
            }
            catch (Exception ex) { return "Lỗi: " + ex.Message; }
        }
        public string DeleteLoaiSanPham(string lsp)
        {
            try
            {
                if (string.IsNullOrEmpty(lsp))
                {
                    return "Mã loại sản phẩm không hợp lệ.";
                }
                dalloaisp.deleteLoaisanpham(lsp);
                return string.Empty;
            }
            catch (Exception ex) { return "Lỗi: " + ex.Message; }
        }
        public List<LoaiSanPham> TimKiem(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return dalloaisp.selectAll();
                }

                else if (input.StartsWith("LSP"))
                {
                    return dalloaisp.GetNhanVienByMa(input);
                }
                else
                {
                    return dalloaisp.GetNhanVienByTen(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm Thẻ lưu động: " + ex.Message);
            }
        }
    }
}
