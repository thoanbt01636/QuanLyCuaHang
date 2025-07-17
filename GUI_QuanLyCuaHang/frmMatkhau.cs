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
using UTIL_QuanLyCuaHang;

namespace GUI_PoLyCafe
{
    public partial class frmMatkhau : Form
    {
        BUSNhanVien busNhanVien = new BUSNhanVien();
        public frmMatkhau()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (!AuthUtil.User.MatKhau.Equals(txtMatKhauCu.Text))
            {
                MessageBox.Show(this, "Mật khẩu cũ chưa đúng!!!");
            }
            else
            {
                if (!txtMatKhauMoi.Text.Equals(txtXacNhanMK.Text))
                {
                    MessageBox.Show(this, "Xác nhận mật khẩu mới chưa trùng khớp!!!");
                }
                else
                {
                    AuthUtil.User.MatKhau = txtMatKhauMoi.Text;

                    if (busNhanVien.ResetMatKhau(AuthUtil.User.Gmail, txtMatKhauMoi.Text))
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công!!!");
                    }
                    else MessageBox.Show("Đổi mật khẩu thất bại, vui lòng kiểm tra lại!!!");
                }
            }
        }

        private void txtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmMatkhau_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtXacNhanMK.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauCu.PasswordChar = checkBox3.Checked ? '\0' : '*';
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauMoi.PasswordChar = checkBox2.Checked ? '\0' : '*';
        }

        private void frmMatkhau_Load(object sender, EventArgs e)
        {
            if (AuthUtil.isLogin())
            {
                textTaiKhoan.Text = AuthUtil.User.Gmail;
            }
        }
    }
}
