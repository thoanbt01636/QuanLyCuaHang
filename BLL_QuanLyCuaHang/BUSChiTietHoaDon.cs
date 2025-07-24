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

            public string InsertChiTietPhieu(ChiTietHD ct)
            {
                try
                {
                    ct.MaCTHD = dalChiTietPhieu.GenerateChiTietID();

                    if (string.IsNullOrEmpty(ct.MaCTHD))
                    {
                        return "Không tạo được mã chi tiết phiếu.";
                    }

                    dalChiTietPhieu.InsertChiTiet(ct);
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    return $"Lỗi khi thêm chi tiết phiếu: {ex.Message}";
                }
            }

            public string UpdateSoLuong(ChiTietHD ct)
            {
                try
                {
                    if (string.IsNullOrEmpty(ct.MaCTHD))
                    {
                        return "Mã chi tiết phiếu không hợp lệ.";
                    }

                    dalChiTietPhieu.UpdateSoLuong(ct);
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    return $"Lỗi khi cập nhật số lượng: {ex.Message}";
                }
            }

            public string XoaChiTietHD(string maPhieu)
            {
                try
                {
                    if (string.IsNullOrEmpty(maPhieu))
                    {
                        return "Mã phiếu bán hàng không hợp lệ.";
                    }

                    dalChiTietPhieu.deleteChiTietHD(maPhieu);
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    return $"Lỗi khi xóa chi tiết: {ex.Message}";
                }
            }
        }
    }

