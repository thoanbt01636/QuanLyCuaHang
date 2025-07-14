
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI_PoLyCafe;
using UTIL_QuanLyCuaHang;

namespace GUI_QuanLyThuVien
{
    public partial class frmmain : Form
    {

        public frmmain()
        {

            InitializeComponent();
        }
        private Form fromPhanQuyen;
        private void openChildForm(Form formquyen)
        {
            if (fromPhanQuyen != null)
            {
                fromPhanQuyen.Close();
            }
            fromPhanQuyen = formquyen;
            formquyen.TopLevel = false;
            formquyen.FormBorderStyle = FormBorderStyle.None;
            formquyen.Dock = DockStyle.Fill;
            guna2PictureBox3.Controls.Add(formquyen);
            guna2PictureBox3.Tag = formquyen;
            formquyen.BringToFront();
            formquyen.Show();


        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

        }

        private void frmmain_Load(object sender, EventArgs e)
        {
        }
        private void TimKiemSach(string keyword)
        {
        }
        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton9_Click(object sender, EventArgs e)
        {
            if (!AuthUtil.User.ChucVu)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào danh sách nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                openChildForm(new frmQuanLyNhanVien());
            }
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            if (!AuthUtil.User.ChucVu)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào danh sách nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                openChildForm(new frmthongkeNV());
            }
        }

        private void guna2GradientButton2_Click_1(object sender, EventArgs e)
        {
            if (!AuthUtil.User.ChucVu)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào danh sách nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                openChildForm(new frmPhieuNhap());
            }
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            if(!AuthUtil.User.ChucVu)
            {
                MessageBox.Show("Bạn không có quyền truy cập vào danh sách nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                openChildForm(new frmSP());
            }
        }
    }
}
