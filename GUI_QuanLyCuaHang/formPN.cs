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

namespace GUI_QuanLyCuaHang
{
    public partial class formPN : Form
    {

        private bool daLuuPhieuNhap = false;
        BUSPhieuNhap phieunhapBus = new BUSPhieuNhap();
        BUSChiTietPhieuNhap chiTietBus = new BUSChiTietPhieuNhap();
        BUSSanPham sanPhamBus = new BUSSanPham();
        BUSNhanVien nhanVienBus = new BUSNhanVien();
        BUSNhaCungCap nhaCungCapBus = new BUSNhaCungCap();


        private List<ChiTietPN> lstChiTietPN;
        public formPN()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void ResetValues()
        {
            txtMaPN.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
            cboMaNV.Text = "";
            cboMaNCC.Text = "";
            textTenNCC.Text = "";
            TextTongTien.Text = "0";


            TextSoLuong.Text = "";

            textThanhTien.Text = "0";
        }
        private void LoadPhieuNhap(string maNV = null)
        {
            try
            {
                List<PhieuNhap> dsHoaDon = phieunhapBus.GetPhieuNhapList(maNV);
                dgvPN.DataSource = dsHoaDon;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách Phiếu nhập: {ex.Message}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Loadthongtinchitiet(string maPhieu)
        {
            BUSChiTietPhieuNhap bus = new BUSChiTietPhieuNhap();
            lstChiTietPN = bus.GetChiTietHDList(maPhieu);

            dgvChiTietPN.Columns.Clear();
            dgvChiTietPN.Rows.Clear();

            dgvChiTietPN.Columns.Add("MaSP", "Mã SP");
            dgvChiTietPN.Columns.Add("TenSP", "Tên SP");
            dgvChiTietPN.Columns.Add("SoLuong", "Số lượng");
            dgvChiTietPN.Columns.Add("DonGia", "Đơn giá");
            dgvChiTietPN.Columns.Add("ThanhTien", "Thành tiền");

            foreach (var ct in lstChiTietPN)
            {
                dgvChiTietPN.Rows.Add(
                    ct.MaSP,
                    ct.TenSP,
                    ct.SoLuong,
                    ct.DonGiaNhap.ToString("N0"),
                    ct.ThanhTien.ToString("N0")
                );
            }
        }
        private void loadThanhToan()
        {
            decimal tong = lstChiTietPN?.Sum(item => item.ThanhTien) ?? 0;
            TextTongTien.Text = tong.ToString("0");
        }
        private void LoadComboBox()
        {
            cboMaSP.DataSource = sanPhamBus.GetSanPhamList();
            cboMaSP.DisplayMember = "MaSP";
            cboMaSP.ValueMember = "MaSP";



            cboMaNV.DataSource = nhanVienBus.GetNhanViensList();
            cboMaNV.DisplayMember = "MaNV";
            cboMaNV.ValueMember = "MaNV";

            cboMaNCC.DataSource = nhaCungCapBus.GetNhaCungCapList();
            cboMaNCC.DisplayMember = "MaNCC";
            cboMaNCC.ValueMember = "MaNCC";
        }
        private void TinhThanhTien()
        {
            if (decimal.TryParse(TextSoLuong.Text.Trim(), out decimal soLuong) &&
                decimal.TryParse(textDonGia.Text.Replace(",", "").Trim(), out decimal donGia))
            {


                decimal thanhTien = soLuong * donGia;
                textThanhTien.Text = thanhTien.ToString("N0");
            }
            else
            {
                textThanhTien.Text = "";
            }
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            string maPN = txtMaPN.Text.Trim();
            string maNV = cboMaNV.SelectedValue.ToString();
            string maNCC = cboMaNCC.Text.Trim();
            DateTime ngayNhap = dtpNgayNhap.Value;

            string maSP = cboMaSP.SelectedValue.ToString();
            int soLuong = int.Parse(TextSoLuong.Text);
            decimal donGia = decimal.Parse(textDonGia.Text);



            if (!daLuuPhieuNhap)
            {
                PhieuNhap pn = new PhieuNhap()
                {
                    MaPN = maPN,
                    MaNV = maNV,
                    MaNCC = maNCC,
                    NgayNhap = ngayNhap,
                };

                phieunhapBus.ThemPhieuNhap(pn);
                LoadPhieuNhap();
                daLuuPhieuNhap = true;
            }
            ChiTietPN chiTietTonTai = chiTietBus.KiemtraMaSP(maPN, maSP);
            if (chiTietTonTai != null)
            {
                chiTietTonTai.SoLuong += soLuong;
                chiTietBus.CapNhatChiTietPN(chiTietTonTai);
            }
            else
            {
                ChiTietPN ct = new ChiTietPN()
                {
                    MaPN = maPN,
                    MaSP = maSP,
                    SoLuong = soLuong,
                    DonGiaNhap = donGia,

                };

                chiTietBus.LuuChiTietPN(ct);
            }

            Loadthongtinchitiet(maPN);
            loadThanhToan();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có chắc muốn xóa  phiếu nhập này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string maPN = txtMaPN.Text.Trim();

                phieunhapBus.XoaPhieuNhap(maPN);

                ResetValues();
                LoadPhieuNhap();

                btnXoa.Enabled = false;

            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            DALPhieuNhap dALPhieuNhap = new DALPhieuNhap();
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaPN.Text = dALPhieuNhap.TaoMaTuDong();
        }

        private void formPN_Load(object sender, EventArgs e)
        {

            LoadPhieuNhap();

            LoadComboBox();
            loadThanhToan();
        }

        private void TextSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void dgvPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maPN = dgvPN.Rows[e.RowIndex].Cells["MaPN"].Value.ToString();
                string maNV = dgvPN.Rows[e.RowIndex].Cells["MaNV"].Value.ToString();


                PhieuNhap phieunhap = (PhieuNhap)dgvPN.Rows[e.RowIndex].DataBoundItem;

                txtMaPN.Text = phieunhap.MaPN;
                dtpNgayNhap.Value = phieunhap.NgayNhap;


                foreach (object item in cboMaNV.Items)
                {
                    if (item is NhanVien nv && nv.MaNV == maNV)
                    {
                        cboMaNV.SelectedItem = nv;
                        break;
                    }
                }


                string maNCC = dgvPN.Rows[e.RowIndex].Cells["MaNCC"].Value.ToString();

                foreach (object item in cboMaNCC.Items)
                {
                    if (item is NhaCungCap ncc && ncc.MaNCC == maNCC)
                    {
                        cboMaNCC.SelectedItem = ncc;
                        break;
                    }
                }

                Loadthongtinchitiet(maPN);
                LoadComboBox();

                tabControl1.SelectedTab = tabPage1;
            }
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

                decimal donGiaGoc;

                if (dsSP.Count > 1)
                    donGiaGoc = dsSP[1].DonGia;
                else
                    donGiaGoc = dsSP[0].DonGia;


                decimal donGia = donGiaGoc * 0.8m;

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

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboMaNCC.Text))
            {
                textTenNCC.Text = "";
                return;
            }

            string maNCC = cboMaNCC.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maNCC))
                return;

            List<NhaCungCap> dsNV = nhaCungCapBus.TimKiem(maNCC);
            if (dsNV != null && dsNV.Count > 0)
                textTenNCC.Text = dsNV[0].TenNCC;
            else
                textTenNCC.Text = "";
        }

        private void dgvChiTietPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPN.CurrentRow == null || dgvChiTietPN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string maPN = txtMaPN.Text.Trim();
                string maSP = dgvChiTietPN.CurrentRow.Cells["MaSP"].Value.ToString();

                try
                {
                    chiTietBus.XoaChiTietHoaDon(maPN, maSP);


                    Loadthongtinchitiet(maPN);


                    loadThanhToan();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa chi tiết hóa đơn: " + ex.Message);
                }
            }
        }
    }
}
