using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCuaHang;
using static DAL_QuanLyCuaHang.DALChiTietPhieuNhap;

namespace BLL_QuanLyCuaHang
{
    public class BUSChiTietPhieuNhap
    {
        DAOChiTietPN daoChiTietPN = new DAOChiTietPN();

        public List<ChiTietPN> GetChiTietPNList()
        {
            return daoChiTietPN.SelectAll();
        }

        public string AddChiTietPN(ChiTietPN ct)
        {
            try
            {
                ct.MaCTPN = daoChiTietPN.GenerateMaCTPN();
                if (string.IsNullOrEmpty(ct.MaCTPN))
                {
                    return "Mã chi tiết phiếu nhập không hợp lệ.";
                }

                daoChiTietPN.AddChiTietPN(ct);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi thêm chi tiết phiếu nhập: " + ex.Message;
            }
        }

        public string UpdateChiTietPN(ChiTietPN ct)
        {
            try
            {
                if (string.IsNullOrEmpty(ct.MaCTPN))
                {
                    return "Mã chi tiết phiếu nhập không hợp lệ.";
                }

                daoChiTietPN.UpdateChiTietPN(ct);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi cập nhật chi tiết phiếu nhập: " + ex.Message;
            }
        }

        public string DeleteChiTietPN(string maCTPN)
        {
            try
            {
                if (string.IsNullOrEmpty(maCTPN))
                {
                    return "Mã chi tiết phiếu nhập không hợp lệ.";
                }

                daoChiTietPN.DeleteChiTietPN(maCTPN);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi xóa chi tiết phiếu nhập: " + ex.Message;
            }
        }

        public List<ChiTietPN> TimKiemTheoMaPN(string maPN)
        {
            try
            {
                if (string.IsNullOrEmpty(maPN))
                {
                    return daoChiTietPN.SelectAll();
                }

                return daoChiTietPN.SelectByMaPN(maPN);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm chi tiết phiếu nhập: " + ex.Message);
            }
        }
    }
}
