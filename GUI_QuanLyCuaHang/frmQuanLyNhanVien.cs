using System;
using System.Data;
using System.Windows.Forms;
using BLL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using Guna.UI2.WinForms;

namespace GUI_QuanLyThuVien
{
    public partial class frmQuanLyNhanVien : Form
    {


        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            loadNhanvien();
            ClearForm();
        }
        private void ClearForm()
        {
            BUSNhanVien bUSNhanVien = new BUSNhanVien();
            textMaNV.Clear();

            textHoTen.Clear();
            textEmail.Clear();
            textMatKhau.Clear();
            textDienThoai.Clear();
            dtpNgaySinh.Value = DateTime.Now;

            rbNhanVien.Checked = true;
            rbConHoatDong.Checked = true;
            rbNhanVien.Enabled = true;
            rbQuanLy.Enabled = true;
            textMaNV.Enabled = false;
            btnCapNhat.Enabled = false;
            btnXoa.Enabled = false;

        }

        private void loadNhanvien()
        {
            BUSNhanVien busNhanVien = new BUSNhanVien();
            List<NhanVien> lst = busNhanVien.GetNhanViensList();
            guna2DataGridView1.DataSource = lst;
            guna2DataGridView1.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            guna2DataGridView1.Columns["TenNV"].HeaderText = "Họ Và Tên";
            guna2DataGridView1.Columns["NgaySinh"].HeaderText = "Ngay Sinh";
            guna2DataGridView1.Columns["DienThoai"].HeaderText = "so Dien Thoai";
            guna2DataGridView1.Columns["Gmail"].HeaderText = "Email";
            guna2DataGridView1.Columns["MatKhau"].HeaderText = "Mật khẩu";
            guna2DataGridView1.Columns["ChucVuText"].HeaderText = "Chuc Vu";
            guna2DataGridView1.Columns["TrangThaiText"].HeaderText = "Trạng Thái";
            guna2DataGridView1.Columns["ChucVu"].Visible = false;
            guna2DataGridView1.Columns["TrangThai"].Visible = false;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            string hoTen = textHoTen.Text.Trim();
            string gmail = textEmail.Text.Trim();
            string matKhau = textMatKhau.Text.Trim();
            string dienThoai = textDienThoai.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            bool chucVu = rbQuanLy.Checked;
            bool trangThai = rbConHoatDong.Checked;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }

            NhanVien nv = new NhanVien
            {
                TenNV = hoTen,
                Gmail = gmail,
                MatKhau = matKhau,
                DienThoai = dienThoai,
                NgaySinh = ngaySinh,
                ChucVu = chucVu,
                TrangThai = trangThai
            };

            BUSNhanVien bll = new BUSNhanVien();
            string result = bll.AddNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm mới nhân viên thành công");
                loadNhanvien();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lỗi: " + result);
            }
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {

            string maNv = textMaNV.Text.Trim();

            if (string.IsNullOrEmpty(maNv))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên có mã {maNv} không?",
                                                   "Xác nhận xóa",
                                                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    BUSNhanVien bll = new BUSNhanVien();
                    string result = bll.DeleteNhanVien(maNv);

                    if (string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadNhanvien();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi khi xóa: {result}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            string maNV = textMaNV.Text.Trim();
            string hoTen = textHoTen.Text.Trim();
            string gmail = textEmail.Text.Trim();
            string matKhau = textMatKhau.Text.Trim();
            string dienThoai = textDienThoai.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            bool chucVu = rbQuanLy.Checked;
            bool trangThai = rbConHoatDong.Checked;

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            NhanVien nv = new NhanVien
            {
                MaNV = maNV,
                TenNV = hoTen,
                Gmail = gmail,
                MatKhau = matKhau,
                DienThoai = dienThoai,
                NgaySinh = ngaySinh,
                ChucVu = chucVu,
                TrangThai = trangThai
            };

            BUSNhanVien bll = new BUSNhanVien();
            string result = bll.UpdateNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thành công");
                loadNhanvien();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Lỗi: " + result);
            }
        }

        private void guna2DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

            textMaNV.Text = row.Cells["MaNV"].Value.ToString();
            textHoTen.Text = row.Cells["TenNV"].Value.ToString();
            textEmail.Text = row.Cells["Gmail"].Value.ToString();
            textMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
            textDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
            if (DateTime.TryParse(row.Cells["NgaySinh"].Value.ToString(), out DateTime ngaySinh))
            {
                dtpNgaySinh.Value = ngaySinh;
            }
            else
            {
                dtpNgaySinh.Value = DateTime.Now;
            }
            bool chucVu = Convert.ToBoolean(row.Cells["ChucVu"].Value);
            if (chucVu)
                rbQuanLy.Checked = true;
            else
                rbNhanVien.Checked = true;
            bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            if (trangThai)
                rbConHoatDong.Checked = true;
            else
                rbKhongHoatDong.Checked = true;
            btnThem.Enabled = false;
            btnCapNhat.Enabled = true;
            btnXoa.Enabled = true;
            textMaNV.Enabled = false;

        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
            ClearForm();
            loadNhanvien();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            BUSNhanVien busNhanVien = new BUSNhanVien();
            List<NhanVien> danhSachNhanVien = busNhanVien.GetNhanViensList();

            var ketQua = danhSachNhanVien
                .Where(nv =>
                    (!string.IsNullOrEmpty(nv.MaNV) && nv.MaNV.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(nv.TenNV) && nv.TenNV.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(nv.Gmail) && nv.Gmail.ToLower().Contains(keyword))
                ).ToList();

            if (ketQua.Count > 0)
            {
                guna2DataGridView1.DataSource = ketQua;
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên nào phù hợp!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}
