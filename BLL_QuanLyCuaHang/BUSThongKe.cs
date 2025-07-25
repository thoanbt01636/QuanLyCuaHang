using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;

namespace BLL_QuanLyCuaHang
{
    public class BUSThongKe
    {
            DALThongKe dalThongKe = new DALThongKe();

            public List<thongkeNV> getThongKeNhanVien(string maNV, DateTime ngayBD, DateTime ngayKt)
            {
                return dalThongKe.GetDoanhThuTheoNhanVien(maNV, ngayBD, ngayKt);
            }

            public List<thongkeSP> getThongKeLoaiSP(string loaiSP, DateTime ngayBD, DateTime ngayKt)
            {
                return dalThongKe.GetDoanhThuTheoLoaiSP(loaiSP, ngayBD, ngayKt);
            }
    }
}
