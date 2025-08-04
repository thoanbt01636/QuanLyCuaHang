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
using GUI_QuanLyThuVien;
using Guna.UI2.WinForms;
using Microsoft.VisualBasic.ApplicationServices;
using UTIL_QuanLyCuaHang;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GUI_QuanLyCuaHang
{
    public partial class HD : Form
    {
        private bool daLuuHoaDon = false;
        BUSHoaDon hoaDonBus = new BUSHoaDon();
        BUSChiTietHoaDon chiTietBus = new BUSChiTietHoaDon();
        BUSSanPham sanPhamBus = new BUSSanPham();
        BUSNhanVien nhanVienBus = new BUSNhanVien();


        private List<ChiTietHD> lstChiTietHD;
        public HD(HoaDon HD, NhanVien nv)
        {
            InitializeComponent();
            LoadComboBox();
            loadThanhToan();
        }
        private void LoadComboBox()
        {
            cboMaSP.DataSource = sanPhamBus.GetSanPhamList();
            cboMaSP.DisplayMember = "MaSP";
            cboMaSP.ValueMember = "MaSP";



            cboMaNV.DataSource = nhanVienBus.GetNhanViensList();
            cboMaNV.DisplayMember = "MaNV";
            cboMaNV.ValueMember = "MaNV";
        }
        private void Loadthongtinchitiet(string maPhieu)
        {
            BUSChiTietHoaDon bus = new BUSChiTietHoaDon();
            lstChiTietHD = bus.GetChiTietHDList(maPhieu);

            dgvChiTietHD.Columns.Clear();
            dgvChiTietHD.Rows.Clear();

            dgvChiTietHD.Columns.Add("MaSP", "Mã SP");
            dgvChiTietHD.Columns.Add("TenSP", "Tên SP");
            dgvChiTietHD.Columns.Add("SoLuong", "Số lượng");
            dgvChiTietHD.Columns.Add("DonGia", "Đơn giá");
            dgvChiTietHD.Columns.Add("GiamGia", "Giảm giá %");
            dgvChiTietHD.Columns.Add("ThanhTien", "Thành tiền");

            foreach (var ct in lstChiTietHD)
            {
                dgvChiTietHD.Rows.Add(
                    ct.MaSP,
                    ct.TenSP,
                    ct.SoLuong,
                    ct.DonGia.ToString("N0"),
                    ct.GiamGia,
                    ct.ThanhTien.ToString("N0")
                );
            }
        }



        public HD()
        {
            InitializeComponent();
        }
        private Form fromPhanQuyen;
        private void openChildForm(Form formquyen)
        {


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    
        private void LoadHoaDon(string maNV = null)
        {
            try
            {
                List<HoaDon> dsHoaDon = hoaDonBus.GetHoaDonList(maNV);
                dgvHD.DataSource = dsHoaDon;
        
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hóa đơn: {ex.Message}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void HD_Load(object sender, EventArgs e)
        {
            dgvChiTietHD.DataBindingComplete += dgrChiTiet_DataBindingComplete;
            dgvChiTietHD.CellValueChanged += dgvChiTietHD_CellValueChanged;
            LoadHoaDon();
        
            LoadComboBox();
            loadThanhToan();


        }

        private void dgrChiTiet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TinhThanhTien();

        }
        private void loadThanhToan()
        {
            decimal tong = lstChiTietHD?.Sum(item => item.ThanhTien) ?? 0;
            TextTongTien.Text = tong.ToString("0");
        }
        private void TinhThanhTien()
        {
            if (decimal.TryParse(TextSoLuong.Text.Trim(), out decimal soLuong) &&
                decimal.TryParse(textDonGia.Text.Replace(",", "").Trim(), out decimal donGia))
            {
                decimal giamGia = 0;
                if (!string.IsNullOrEmpty(TextGiamGia.Text.Trim()))
                    decimal.TryParse(TextGiamGia.Text.Trim(), out giamGia);

                decimal thanhTien = soLuong * donGia * (1 - giamGia / 100);
                textThanhTien.Text = thanhTien.ToString("N0");
            }
            else
            {
                textThanhTien.Text = "";
            }
        }

        private void dgvHD_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


        }


        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }





        private void dgvChiTietHD_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvChiTietHD_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            dgvChiTietHD.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void dgvHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maHD = dgvHD.Rows[e.RowIndex].Cells["MaHD"].Value.ToString();
                string maNV = dgvHD.Rows[e.RowIndex].Cells["MaNV"].Value.ToString();


                HoaDon hoaDon = (HoaDon)dgvHD.Rows[e.RowIndex].DataBoundItem;

                txtMaHD.Text = hoaDon.MaHD;
                dtpNgayLap.Value = hoaDon.NgayLap;
                textTenKH.Text = hoaDon.TenKhach;

                foreach (NhanVien item in cboMaNV.Items)
                {
                    if (item.MaNV == maNV)
                    {
                        cboMaNV.SelectedItem = item;
                        break;
                    }
                }
                Loadthongtinchitiet(maHD);
                LoadComboBox();

                tabControl1.SelectedTab = tabPage1;
            }
        }

        private void dgvChiTietHD_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvHD.CurrentRow == null || dgvChiTietHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string maHD = txtMaHD.Text.Trim();
                string maSP = dgvChiTietHD.CurrentRow.Cells["MaSP"].Value.ToString();

                try
                {
                    chiTietBus.XoaChiTietHoaDon(maHD, maSP);


                    Loadthongtinchitiet(maHD);


                    loadThanhToan();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa chi tiết hóa đơn: " + ex.Message);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DALHoaDon dALHoaDon = new DALHoaDon();
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHD.Text = dALHoaDon.TaoMaTuDong();
           

        }
        private void ResetValues()
        {
            txtMaHD.Text = "";
            dtpNgayLap.Value = DateTime.Now;
            cboMaNV.Text = "";
            textTenKH.Text = "";
            TextTongTien.Text = "0";

         
            TextSoLuong.Text = "";
            TextGiamGia.Text = "0";
            textThanhTien.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text.Trim();
            string maNV = cboMaNV.SelectedValue.ToString();
            string tenKH = textTenKH.Text.Trim();
            DateTime ngayLap = dtpNgayLap.Value;

            string maSP = cboMaSP.SelectedValue.ToString();
            int soLuong = int.Parse(TextSoLuong.Text);
            decimal donGia = decimal.Parse(textDonGia.Text);
            decimal giamGia = decimal.Parse(TextGiamGia.Text);
            int soLuongTon = sanPhamBus.GetSoLuongTon(maSP);
            if (soLuong > soLuongTon)
            {
                MessageBox.Show($"Sản phẩm chỉ còn {soLuongTon} sản phẩm trong kho.", "Không đủ hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tenKH))
            {
                tenKH = "Khách tự do";
            }
            if (!daLuuHoaDon)
            {
                HoaDon hd = new HoaDon()
                {
                    MaHD = maHD,
                    MaNV = maNV,
                    TenKhach = tenKH,
                    NgayLap = ngayLap
                };

                hoaDonBus.ThemHoaDon(hd);
                LoadHoaDon();
                daLuuHoaDon = true;
            }
            ChiTietHD chiTietTonTai = chiTietBus.KiemtraMaSP(maHD, maSP);
            if (chiTietTonTai != null)
            {
                chiTietTonTai.SoLuong += soLuong;
                chiTietBus.CapNhatChiTietHD(chiTietTonTai);
            }
            else
            {
                ChiTietHD ct = new ChiTietHD()
                {
                    MaHD = maHD,
                    MaSP = maSP,
                    SoLuong = soLuong,
                    DonGia = donGia,
                    GiamGia = giamGia
                };

                chiTietBus.LuuChiTietHD(ct);
            }

            Loadthongtinchitiet(maHD);
            loadThanhToan();

        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboMaSP.Text))
            {
                TextTenNV.Text = "";
                return;
            }

            string masp = cboMaSP.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(masp))
                return;
            List<SanPham> dsSP = sanPhamBus.TimKiem(masp);

            if (dsSP != null && dsSP.Count > 0)
            {
                textTenSP.Text = dsSP[0].TenSP;

                decimal donGia;

                if (dsSP.Count > 1)
                    donGia = dsSP[1].DonGia;
                else
                    donGia = dsSP[0].DonGia;

                textDonGia.Text = donGia.ToString("N0");
            }
            else
            {
                textTenSP.Text = "";
                textDonGia.Text = "";
            }

        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboMaNV.Text))
            {
                TextTenNV.Text = "";
                return;
            }

            string manv = cboMaNV.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(manv))
                return;

            List<NhanVien> dsNV = nhanVienBus.LayTenNV(manv);
            if (dsNV != null && dsNV.Count > 0)
                TextTenNV.Text = dsNV[0].TenNV;
            else
                TextTenNV.Text = "";

        }

        private void TextGiamGia_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void TextSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc muốn xóa hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string maHD = txtMaHD.Text.Trim();

                hoaDonBus.XoaHoaDon(maHD);

                ResetValues();
                LoadHoaDon();

                btnXoa.Enabled = false;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
