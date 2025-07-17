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
    }
}
