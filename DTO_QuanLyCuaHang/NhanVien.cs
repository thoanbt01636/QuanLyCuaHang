using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLyCuaHang
{
    public class NhanVien
    {
        public string MaNV {  get; set; }
        public string TenNV {  get; set; }
        public DateTime  NgaySinh {  get; set; }
        public string DienThoai {  get; set; }
        public bool ChucVu {  get; set; }

        public bool TrangThai {  get; set; }
        public string Gmail {  get; set; }
        public string MatKhau {  get; set; }
        public string ChucVuText => ChucVu ? "Quản Lý" : "Nhân Viên";
        public string TrangThaiText => TrangThai ? "Đang Hoat Động " : "không Hoạt Động";
    }
}
