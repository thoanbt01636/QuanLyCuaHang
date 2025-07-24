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

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {

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
                TenNCC = tenNCC,
                DiaChi = diaChi,
                DienThoai = dienThoai,
                Email = email
            };

            BUSNhaCungCap busNCC = new BUSNhaCungCap();
            string result = busNCC.AddNhaCungCap(ncc);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm nhà cung cấp thành công.");
                ClearForm();
                LoadNhaCungCap();
            }
            else
            {
                MessageBox.Show(result);
            }

        }
        private void ClearForm()
        {
            btnthem1111.Enabled = true;
            btncapnhat.Enabled = false;
            btnxoa.Enabled = true;
            textMaNCC.Clear();
            textTenNCC.Clear();
            textDienThoai.Clear();
            textEmail.Clear();
            textDiaChi.Clear();
            textMaNCC.Enabled = false;
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

            string maNCC = textMaNCC.Text.Trim();
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

            string maNCC = textMaNCC.Text.Trim();
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

            textMaNCC.Text = row.Cells["MaNCC"].Value.ToString();
            textTenNCC.Text = row.Cells["TenNCC"].Value.ToString();
            textDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            textDienThoai.Text = row.Cells["DienThoai"].Value.ToString();
            textEmail.Text = row.Cells["Email"].Value.ToString();            
            btnthem1111.Enabled = false;
            btncapnhat.Enabled = true;
            btnxoa.Enabled = true;
            textMaNCC.Enabled = false;
        }
    }
}


