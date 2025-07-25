using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTIL_QuanLyCuaHang;
using BLL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using Microsoft.VisualBasic.Devices;
namespace GUI_QuanLyThuVien
{
    public partial class frmloaiSP : Form
    {
        public frmloaiSP()
        {
            InitializeComponent();
        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmloaiSP_Load(object sender, EventArgs e)
        {
            LoadDanhSachLoaiSP();
            ClearFrom();
        }
        private void LoadDanhSachLoaiSP()
        {

            BUSLoaiSP busLoaiSp = new BUSLoaiSP();
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = busLoaiSp.GetLoaiSanPhamList();
            guna2DataGridView1.Columns["MaLoai"].HeaderText = "Mã Loại";
            guna2DataGridView1.Columns["TenLoai"].HeaderText = "Tên Loại";
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnthem1111_Click(object sender, EventArgs e)
        {
            string maLoai = textMaLoaiSP.Text.Trim();
            string tenLoai = textTenLoaiSP.Text.Trim();



            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin .");
                return;
            }

            LoaiSanPham loai = new LoaiSanPham
            {
                MaLoai = maLoai,
                TenLoai = tenLoai,

            };
            BUSLoaiSP busloaisp = new BUSLoaiSP();
            string result = busloaisp.AddLoaiSanPham(loai);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearFrom();
                LoadDanhSachLoaiSP();
            }
            else
            {
                MessageBox.Show(result);
            }
            btnthem.Enabled = false;
            btnxoa.Enabled = true;
            btncapnhat.Enabled = true;

        }
        private void ClearFrom()
        {
            BUSLoaiSP bUSLoaiSP = new BUSLoaiSP();
            textMaLoaiSP.Enabled = false;
            textMaLoaiSP.Text = bUSLoaiSP.TaoMaTuDong();
            btnthem.Enabled = true;
            btncapnhat.Enabled = false;
            btnxoa.Enabled = true;
            textTenLoaiSP.Clear();
            textMaLoaiSP.Enabled = false;
            btnxoa.Enabled = false;
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string maLoai = textMaLoaiSP.Text.Trim();
            string tenLoai = textTenLoaiSP.Text.Trim();

            if (string.IsNullOrEmpty(maLoai))
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                    maLoai = selectedRow.Cells["MaLoai"].Value.ToString();
                    tenLoai = selectedRow.Cells["TenLoai"].Value.ToString();

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin loại sản phẩm cần xóa xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maLoai))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại sản phẩm {maLoai} - {tenLoai}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSLoaiSP bus = new BUSLoaiSP();
                string kq = bus.DeleteLoaiSanPham(maLoai);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin loại sản phẩm {maLoai} - {tenLoai} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFrom();
                    LoadDanhSachLoaiSP();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            string maLoai = textMaLoaiSP.Text.Trim();
            string tenLoai = textTenLoaiSP.Text.Trim();


            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin loại sản phẩm.");
                return;
            }
            LoaiSanPham loaiSanPham = new LoaiSanPham
            {
                MaLoai = maLoai,
                TenLoai = tenLoai,

            };
            BUSLoaiSP bus = new BUSLoaiSP();
            string result = bus.UpdateLoaiSanPham(loaiSanPham);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearFrom();
                LoadDanhSachLoaiSP();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnlamoi_Click(object sender, EventArgs e)
        {
            ClearFrom();
            LoadDanhSachLoaiSP();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string keyword = textTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadDanhSachLoaiSP();
            }
            else
            {
                TimKiemLoaiSanPham(keyword);
            }
        }

        public void TimKiemLoaiSanPham(string keyword)
        {
            BUSLoaiSP bUSLoaiSP = new BUSLoaiSP();
            guna2DataGridView1.DataSource = bUSLoaiSP.TimKiem(keyword);
        }

        private void guna2DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

            textMaLoaiSP.Text = row.Cells["MaLoai"].Value.ToString();
            textTenLoaiSP.Text = row.Cells["TenLoai"].Value.ToString();

            btnthem.Enabled = false;
            btncapnhat.Enabled = true;
            btnxoa.Enabled = true;
            textMaLoaiSP.Enabled = false;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
