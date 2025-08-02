using GUI_QuanLyThuVien;
using UTIL_QuanLyCuaHang;
using BLL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
namespace GUI_QuanLyCuaHang
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            BUSNhanVien bUSNhanVien = new BUSNhanVien();
            string username = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            //nhập chay
            //NhanVien nv = bUSNhanVien.DangNhap(username, password);
            //quản lý
            NhanVien nv = bUSNhanVien.DangNhap("minh2410@gmail.com", "matkhau123");

            if (nv == null)
            {
                MessageBox.Show(this, "Tài khoải hoặc mật khẩu không chính xác");
                loadlai();
                return;
            }
            else
            {
                MessageBox.Show(this, "đăng nhập thành công");
                frmmain home = new frmmain();
                home.Show();
            }
            AuthUtil.User = nv;
        }
        private void loadlai()
        {
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("bạn có muốn thoát khỏi chương trình ", "thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes) { Application.Exit(); }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = guna2CheckBox1.Checked ? '\0' : '*';
        }
    }
}
