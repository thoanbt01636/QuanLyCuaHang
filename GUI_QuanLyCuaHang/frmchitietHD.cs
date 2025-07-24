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
using DAL_QuanLyCuaHang;
using static BLL_QuanLyCuaHang.BUSChiTietHoaDon;

namespace GUI_QuanLyThuVien
{
    public partial class frmchitietHD : Form
    {

        DALChiTietHoaDon dalChiTietPhieu = new DALChiTietHoaDon();
        private HoaDon hoaDon;
        private NhanVien nhanVien;
        private List<ChiTietHD> lstChiTietHD;
        private List<SanPham> lstSanPham;
        bool isActive = true;
        public frmchitietHD(HoaDon HD ,NhanVien nv)
        {
            InitializeComponent();
            hoaDon = HD;
            nhanVien = nv;
            lstChiTietHD = new List<ChiTietHD>();
            lstSanPham = new List<SanPham>();
            isActive = hoaDon.TrangThai;
        }
        
        private void LoadDanhSachSanPham()
        {
            BUSSanPham bllSanPham = new BUSSanPham();
            lstSanPham = bllSanPham.GetSanPhamList();

            dgvSanPham.DataSource = lstSanPham;
            dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
            dgvSanPham.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvSanPham.Columns["MaLoai"].HeaderText = "Mã loại";
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadChiTietHD(string maPhieu)
        {
            BUSChiTietHoaDon bus = new BUSChiTietHoaDon();
            lstChiTietHD = bus.GetChiTietHDList(maPhieu);
            dgvChiTietHD.DataSource = lstChiTietHD;
            dgvChiTietHD.Columns["MaCTHD"].Visible = false;
            dgvChiTietHD.Columns["MaHD"].Visible = false;
            dgvChiTietHD.Columns["MaSP"].Visible = false;
            dgvChiTietHD.Columns["TenSP"].HeaderText = "Sản Phẩm";
            dgvChiTietHD.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTietHD.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChiTietHD.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            dgvChiTietHD.Columns["SoLuong"].ReadOnly = false;

            dgvChiTietHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn col in dgvChiTietHD.Columns)
            {
                col.ReadOnly = true;
            }
            dgvChiTietHD.Columns["SoLuong"].ReadOnly = false;
        }


        private void frmchitietHD_Load(object sender, EventArgs e)
        {
            dgvChiTietHD.DataBindingComplete += dgrChiTiet_DataBindingComplete;
            loadThanhToan();
           // LoadInfo();
            LoadDanhSachSanPham();
            LoadChiTietHD(hoaDon.MaHD);
            activeTranfer();
        }
        private void dgrChiTiet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            loadThanhToan();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (isActive)
            {
                return;
            }

            if (dgvSanPham.CurrentRow != null)
            {
                string id = Convert.ToString(dgvSanPham.CurrentRow.Cells["MaSP"].Value);
                string ten = Convert.ToString(dgvSanPham.CurrentRow.Cells["TenSP"].Value);
                decimal dongia = Convert.ToDecimal(dgvSanPham.CurrentRow.Cells["DonGia"].Value);

                SanPham sanPham = new SanPham
                {
                    MaSP = id,
                    TenSP = ten,
                    DonGia = dongia
                };
                transfer(sanPham, 1);
                LoadDanhSachSanPham();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void transfer(SanPham sp, int soLuong = 1)
        {
            if (isActive)
            {
                return;
            }
            if (sp != null)
            {
                BUSChiTietHoaDon bus = new BUSChiTietHoaDon();
                ChiTietHD existingItem = lstChiTietHD.FirstOrDefault(item => item.MaSP == sp.MaSP);
                if (existingItem != null)
                {
                    existingItem.SoLuong += soLuong;
                    string result = bus.UpdateSoLuong(existingItem);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Thêm sản phẩm không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    ChiTietHD ct = new ChiTietHD()
                    {
                       MaCTHD  = dalChiTietPhieu.GenerateChiTietID(),
                        MaHD = hoaDon.MaHD,
                        MaSP = sp.MaSP,
                        SoLuong = soLuong,
                        DonGia = sp.DonGia,
                    };
                    string result = bus.InsertChiTietPhieu(ct);
                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("Thêm sản phẩm không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadChiTietHD(hoaDon.MaHD);

            }
        }

        private void activeTranfer()
        {
            if (isActive)
            {
                guna2Button2.Enabled = true;
                guna2Button1.Enabled = true;
              
            }
        }
        private void loadThanhToan()
        {
            decimal tong = lstChiTietHD.Sum(item => item.ThanhTien);

            decimal giamGiaPhanTram = 0;
            if (!string.IsNullOrWhiteSpace(txtGiamGia.Text) &&
                decimal.TryParse(txtGiamGia.Text, out decimal giamGiaInput))
            {
                giamGiaPhanTram = giamGiaInput;
            }

            decimal tienGiam = tong * (giamGiaPhanTram / 100);
            decimal thanhTienCuoi = tong - tienGiam;

            txtThanhTien.Text = thanhTienCuoi.ToString("0");
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (isActive)
            {
                return;
            }
            deleteChiTiet();
            LoadDanhSachSanPham();
        }
        private void deleteChiTiet()
        {
            if (dgvChiTietHD.CurrentRow != null)
            {
                string id = Convert.ToString(dgvChiTietHD.CurrentRow.Cells["MaCTHD"].Value);
                string name = Convert.ToString(dgvChiTietHD.CurrentRow.Cells["TenSP"].Value);
                int soLuongHienTai = Convert.ToInt32(dgvChiTietHD.CurrentRow.Cells["SoLuong"].Value);

                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn giảm số lượng sản phẩm {name}?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    BUSChiTietHoaDon bus = new BUSChiTietHoaDon();

                    if (soLuongHienTai > 1)
                    {

                        ChiTietHD chiTiet = lstChiTietHD.FirstOrDefault(item => item.MaCTHD == id);
                        if (chiTiet != null)
                        {
                            chiTiet.SoLuong -= 1;
                            string kq = bus.UpdateSoLuong(chiTiet);

                            if (!string.IsNullOrEmpty(kq))
                            {
                                MessageBox.Show("Cập nhật số lượng không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {

                        string kq = bus.XoaChiTietHD(id);

                        if (!string.IsNullOrEmpty(kq))
                        {
                            MessageBox.Show("Xóa sản phẩm không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    LoadChiTietHD(hoaDon.MaHD);
                    
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để giảm số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void dgvChiTietHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvChiTietHD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }
    }
}
