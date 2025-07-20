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
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using static BLL_QuanLyCuaHang.BUSChiTietHoaDon;

namespace GUI_QuanLyThuVien
{
    public partial class frmchitietHD : Form
    {
        private string maHD;
        private string tenNV;
        private BLLChiTietHD bllChiTietHD = new BLLChiTietHD();
        private List<ChiTietHD> lstChiTietHD;
        private List<SanPham> lstSanPham;

        public frmchitietHD(HoaDon hoadon)
        {
            InitializeComponent();
            this.maHD = hoadon.MaHD;
            lstChiTietHD = new List<ChiTietHD>();
            lstSanPham = new List<SanPham>();
        }
        private void LoadThongTinHoaDon()
        {
            txtMaHD.Text = maHD;
            txtTenNV.Text = tenNV;
            NgayLap.Value = DateTime.Now;
        }
        private void LoadDanhSachSanPham()
        {
            BUSSanPham bllSanPham = new BUSSanPham();
            lstSanPham = bllSanPham.GetSanPhamList();

            dgvSanPham.DataSource = lstSanPham;
            dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadChiTietHD()
        {
            lstChiTietHD = bllChiTietHD.GetChiTietHDByMaHD(maHD);
            dgvChiTietHD.DataSource = lstChiTietHD;

            dgvChiTietHD.Columns["MaHD"].Visible = false;
            dgvChiTietHD.Columns["MaSP"].HeaderText = "Mã SP";
            dgvChiTietHD.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvChiTietHD.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTietHD.Columns["DonGiaNhap"].HeaderText = "Đơn giá";
            dgvChiTietHD.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvChiTietHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void frmchitietHD_Load(object sender, EventArgs e)
        {
            LoadThongTinHoaDon();
            LoadDanhSachSanPham();
            LoadChiTietHD();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow != null)
            {
                string maSP = dgvSanPham.CurrentRow.Cells["MaSP"].Value.ToString();
                string tenSP = dgvSanPham.CurrentRow.Cells["TenSP"].Value.ToString();
                decimal donGia = Convert.ToDecimal(dgvSanPham.CurrentRow.Cells["DonGia"].Value);
                int soLuong = 1;

                ChiTietHD cthd = new ChiTietHD
                {
                    MaHD = maHD,
                    MaSP = maSP,
                    TenSP = tenSP,
                    SoLuong = soLuong,
                    DonGiaNhap = donGia,
                    ThanhTien = bllChiTietHD.TinhThanhTien(soLuong, donGia)
                };

                if (bllChiTietHD.ThemChiTietHD(cthd))
                {
                    LoadChiTietHD();
                }
                else
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHD.CurrentRow != null)
            {
                string maCTHD = dgvChiTietHD.CurrentRow.Cells["MaCTHD"].Value.ToString();
                string tenSP = dgvChiTietHD.CurrentRow.Cells["TenSP"].Value.ToString();

                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm {tenSP}?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (bllChiTietHD.XoaChiTietHD(maCTHD))
                    {
                        LoadChiTietHD();
                    }
                    else
                    {
                        MessageBox.Show("Xóa chi tiết hóa đơn thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvChiTietHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvChiTietHD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvChiTietHD.Columns["SoLuong"].Index)
            {
                DataGridViewRow row = dgvChiTietHD.Rows[e.RowIndex];
                string maCTHD = row.Cells["MaCTHD"].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal donGia = Convert.ToDecimal(row.Cells["DonGiaNhap"].Value);

                ChiTietHD cthd = new ChiTietHD
                {
                    MaHD = maCTHD,
                    SoLuong = soLuong,
                    DonGiaNhap = donGia,
                    ThanhTien = bllChiTietHD.TinhThanhTien(soLuong, donGia)
                };

                if (bllChiTietHD.CapNhatChiTietHD(cthd))
                {
                    row.Cells["ThanhTien"].Value = cthd.ThanhTien;
                }
                else
                {
                    MessageBox.Show("Cập nhật số lượng thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
