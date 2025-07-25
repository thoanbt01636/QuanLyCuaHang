
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI_PoLyCafe
{
    public partial class frmthongkeSP : Form
    {
        private BUSThongKe busThongKe = new BUSThongKe();
        public frmthongkeSP()
        {
            InitializeComponent();
        }

        private void frmthongkeSP_Load(object sender, EventArgs e)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTuNgay.Value = firstDayOfMonth;
            dtDenNgay.Value = DateTime.Now;

            LoadLoaiSanPham();
            guna2Button1_Click(sender, e);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            string loai = cboLoaiSanPham.SelectedValue.ToString();
            DateTime bd = dtTuNgay.Value.Date;
            DateTime kt = dtDenNgay.Value.Date;

            List<thongkeSP> result = busThongKe.getThongKeLoaiSP(loai, bd, kt);
            guna2DataGridView2.DataSource = result;
        }
        private void LoadLoaiSanPham()
        {
            try
            {
                BUSLoaiSP bUSLoaiSanPham = new BUSLoaiSP();
                List<LoaiSanPham> dsLoai = bUSLoaiSanPham.GetLoaiSanPhamList();

                dsLoai.Insert(0, new LoaiSanPham() { MaLoai = string.Empty, TenLoai = "--Tất Cả--" });
                cboLoaiSanPham.DataSource = dsLoai;
                cboLoaiSanPham.ValueMember = "MaLoai";
                cboLoaiSanPham.DisplayMember = "TenLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
