using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;

namespace BLL_QuanLyCuaHang
{
    public class BUSNhaCungCap
    {
        DALNhaCungCap dalnhacungcap = new DALNhaCungCap();

        public List<NhaCungCap> GetNhaCungCapList()
        {
            return dalnhacungcap.selectAll();
        }
        public string AddNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                ncc.MaNCC = dalnhacungcap.generateMaNCC();
                if (string.IsNullOrEmpty(ncc.MaNCC))
                {
                    return "Mã nhà cung cấp không đúng";
                }
                dalnhacungcap.addNhaCungCap(ncc);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateNhaCungCap(NhaCungCap ncc)
        {
            try
            {
                if (string.IsNullOrEmpty(ncc.MaNCC))
                {
                    return "Mã nhà cung cấp không đúng";
                }
                dalnhacungcap.updateNhaCungCap(ncc);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteNhaCungCap(string maNCC)
        {
            try
            {
                if (string.IsNullOrEmpty(maNCC))
                {
                    return "Mã nhà cung cấp không hợp lệ.";
                }
                dalnhacungcap.deleteNhaCungCap(maNCC);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public List<NhaCungCap> TimKiem(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return dalnhacungcap.selectAll();
                }
                else if (input.StartsWith("NCC"))
                {
                    return dalnhacungcap.GetNhaCungCapByMa(input);
                }
                else
                {
                    return dalnhacungcap.GetNhaCungCapByTen(input);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm nhà cung cấp: " + ex.Message);
            }
        }

    }
}
