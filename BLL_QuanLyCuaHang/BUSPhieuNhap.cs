using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using Microsoft.Data.SqlClient;

namespace BLL_QuanLyCuaHang
{
    public class BUSPhieuNhap
    {
        DALPhieuNhap dalPhieuNhap = new DALPhieuNhap();


        public List<PhieuNhap> GetListPhieuNhap()
        {
            return dalPhieuNhap.selectAll();
        }
        public string TaoMaTuDong()
        {
            return dalPhieuNhap.generateMaPN();
        }

        public string UpdatePhieuNhap(PhieuNhap pn)
        {
            try
            {
                if (string.IsNullOrEmpty(pn.MaPN))
                {
                    return "Mã phiếu nhập không hợp lệ.";
                }

                dalPhieuNhap.updatePhieuNhap(pn);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string AddPhieuNhap(PhieuNhap pn)
        {
            try
            {
                pn.MaPN = dalPhieuNhap.generateMaPN();
                if (string.IsNullOrEmpty(pn.MaPN))
                {
                    return "Mã phiếu nhập không hợp lệ.";
                }

                dalPhieuNhap.addPhieuNhap(pn);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeletePhieuNhap(string maPN)
        {
            try
            {
                if (string.IsNullOrEmpty(maPN))
                {
                    return "Mã phiếu nhập không hợp lệ.";
                }

                dalPhieuNhap.deletePhieuNhap(maPN);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public List<PhieuNhap> TimKiem(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return dalPhieuNhap.selectAll();
                }
                else if (input.StartsWith("PN"))
                {
                    return dalPhieuNhap.GetPhieuNhapByMa(input);
                }
                else
                {
                    return dalPhieuNhap.GetPhieuNhapByMaNCC(input);
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("Lỗi SQL khi tìm kiếm phiếu nhập: " + sqlEx.Message);
            }
        }
    }
}
