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

namespace GUI_QuanLyThuVien
{
    public partial class frmQuanLyNCC : Form
    {
        public frmQuanLyNCC()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void frmQuanLyNCC_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();
        }
        private void LoadNhaCungCap()
        {

            BUSNhaCungCap bUSNhaCungCap = new BUSNhaCungCap();
            guna2DataGridView1.DataSource = null;
            List<NhaCungCap> lstSP = bUSNhaCungCap.GetNhaCungCapList();
            guna2DataGridView1.DataSource = lstSP;

        }
    }
}
