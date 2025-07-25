
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

namespace GUI_PoLyCafe
{
    public partial class frmthongkeNV : Form
    {
        private BUSThongKe busThongKe = new BUSThongKe();
        public frmthongkeNV()
        {
            InitializeComponent();
        }

        private void frmthongkeNV_Load(object sender, EventArgs e)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTuNgay.Value = firstDayOfMonth;
            dtDenNgay.Value = DateTime.Now;

            LoadNhanVien();
            button1_Click(sender, e);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maNV = "";
            if (cboNhanVien.SelectedValue != null)
                maNV = cboNhanVien.SelectedValue.ToString();

            DateTime tuNgay = dtTuNgay.Value.Date;
            DateTime denNgay = dtDenNgay.Value.Date;

            List<thongkeNV> result = busThongKe.getThongKeNhanVien(maNV, tuNgay, denNgay);
            guna2DataGridView1.DataSource = result;
        }
        private void LoadNhanVien()
        {
            try
            {
                BUSNhanVien busNhanVien = new BUSNhanVien();
                List<NhanVien> dsNV = busNhanVien.GetNhanViensList();

                dsNV.Insert(0, new NhanVien { MaNV = string.Empty, TenNV = "--Tất Cả--" });

                cboNhanVien.DataSource = null;
                cboNhanVien.DisplayMember = "TenNV";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.DataSource = dsNV;
                cboNhanVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
