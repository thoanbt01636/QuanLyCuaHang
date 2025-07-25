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
using UTIL_QuanLyCuaHang;

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
            string tuKhoa = textTimKiem.Text.Trim();

            BUSNhaCungCap bUS = new BUSNhaCungCap();
            List<NhaCungCap> danhSach = bUS.GetNhaCungCapList();

            if (!AuthUtil.User.ChucVu)
            {
                danhSach = danhSach.Where(pn => pn.MaNCC == AuthUtil.User.MaNV).ToList();
            }

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                danhSach = danhSach.Where(pn =>
                    
                    pn.MaNCC.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.DataSource = danhSach;


            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        


        private void frmQuanLyNCC_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadNhaCungCap();

        }
        private void LoadNhaCungCap()
        {

            BUSNhaCungCap bUSNhaCungCap = new BUSNhaCungCap();
            guna2DataGridView1.DataSource = null;
            List<NhaCungCap> lstSP = bUSNhaCungCap.GetNhaCungCapList();
            guna2DataGridView1.DataSource = lstSP;

        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            string tenNCC = textTenNCC.Text.Trim();
            string diaChi = textDiaChi.Text.Trim();
            string dienThoai = textDienThoai.Text.Trim();
            string email = textEmail.Text.Trim();
            BUSNhaCungCap busNCC = new BUSNhaCungCap();


            NhaCungCap ncc = new NhaCungCap
            {
                MaNCC = busNCC.TaoMaTuDong(),
                TenNCC = tenNCC,
                DiaChi = diaChi,
                DienThoai = dienThoai,
                Email = email
            };

            string result = busNCC.AddNhaCungCap(ncc);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công.");
                LoadNhaCungCap();
            }
            else
            {
                MessageBox.Show(result);
            }
            ClearForm();
        }
        private void ClearForm()
        {
            BUSNhaCungCap bUSNhaCungCap = new BUSNhaCungCap();
            txtMaNCC.Clear();
            txtMaNCC.Text = bUSNhaCungCap.TaoMaTuDong();
            txtMaNCC.Enabled = false;
            textTenNCC.Clear();
            textDienThoai.Clear();
            textEmail.Clear();
            textDiaChi.Clear();
            txtMaNCC.Enabled = false;
            btnXoa.Enabled = false;
            btnCapNhat.Enabled = false;

        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = textTenNCC.Text.Trim();
            string diaChi = textDiaChi.Text.Trim();
            string dienThoai = textDienThoai.Text.Trim();
            string email = textEmail.Text.Trim();

            if (string.IsNullOrEmpty(tenNCC))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.");
                return;
            }

            NhaCungCap ncc = new NhaCungCap
            {
                MaNCC = maNCC,
                TenNCC = tenNCC,
                DiaChi = diaChi,
                DienThoai = dienThoai,
                Email = email
            };

            BUSNhaCungCap bus = new BUSNhaCungCap();
            string result = bus.UpdateNhaCungCap(ncc);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadNhaCungCap();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {

            string maNCC = txtMaNCC.Text.Trim();
            string tenNCC = textTenNCC.Text.Trim();

            if (string.IsNullOrEmpty(maNCC))
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                    maNCC = selectedRow.Cells["MaNCC"].Value.ToString();
                    tenNCC = selectedRow.Cells["TenNCC"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp {maNCC} - {tenNCC}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSNhaCungCap bus = new BUSNhaCungCap();
                string kq = bus.DeleteNhaCungCap(maNCC);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa nhà cung cấp {maNCC} - {tenNCC} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadNhaCungCap();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

            txtMaNCC.Text = row.Cells["MaNCC"].Value.ToString();
            textTenNCC.Text = row.Cells["TenNCC"].Value.ToString();
            textDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            textDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
            textEmail.Text = row.Cells["Email"].Value.ToString();
            btnCapNhat.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
        }


        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadNhaCungCap();
        }
    }
}


