using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyCuaHang;
namespace UTIL_QuanLyCuaHang
{
    public class AuthUtil
    {
        public static NhanVien User = null;
        public static bool isLogin()
        {
            if (User == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(User.MaNV))
            {
                return false;
            }
            return true;
        }
    }
}
