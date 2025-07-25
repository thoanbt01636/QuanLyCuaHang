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
using static DAL_QuanLyCuaHang.DALChiTietPhieuNhap;

namespace GUI_QuanLyThuVien
{
    public partial class frmchitietPN : Form
    {
        private NhaCungCap nhaCungCap;
        private PhieuNhap phieuNhapHang;
        private NhanVien nhanVien;
        private List<ChiTietPN> lstChiTiet;
        private List<SanPham> lstSanPham;
        private bool isActive;
        public frmchitietPN(PhieuNhap phieu, NhanVien nv, NhaCungCap ncc)
        {
            InitializeComponent();
            nhaCungCap = ncc;
            phieuNhapHang = phieu;
            nhanVien = nv;
            lstChiTiet = new List<ChiTietPN>();
            lstSanPham = new List<SanPham>();
            isActive = phieu.TrangThai;
        }
        private void LoadDanhSachChiTietPN()
        {
            BUSChiTietPhieuNhap busChiTietPN = new BUSChiTietPhieuNhap();
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = busChiTietPN.GetChiTietPNList();

            guna2DataGridView1.Columns["MaCTPN"].HeaderText = "Mã CTPN";
            guna2DataGridView1.Columns["MaPN"].HeaderText = "Mã Phiếu Nhập";
            guna2DataGridView1.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
            guna2DataGridView1.Columns["SoLuong"].HeaderText = "Số Lượng";
            guna2DataGridView1.Columns["DonGiaNhap"].HeaderText = "Đơn Giá Nhập";

            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadSanPham()
        {
            try
            {
                BUSSanPham bUSSanPham = new BUSSanPham();
                List<SanPham> dsLoai = bUSSanPham.GetSanPhamList();
                cboTenSP.DataSource = dsLoai;
                cboTenSP.ValueMember = "MaSP";
                cboTenSP.DisplayMember = "TenSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton8_Click(object sender, EventArgs e)
        {
            string maCTPN = textMaCTPN.Text.Trim();
            string maPN = textMaPN.Text.Trim();
            string maSP = cboTenSP.SelectedValue.ToString();

            int soLuong;
            decimal donGiaNhap;

            if (string.IsNullOrEmpty(maPN) || string.IsNullOrEmpty(maSP) ||
                !int.TryParse(textSoLuong.Text.Trim(), out soLuong) ||
                !decimal.TryParse(textDonGiaNhap.Text.Trim(), out donGiaNhap))
            {
                MessageBox.Show("Vui lòng điền đầy đủ và đúng thông tin.");
                return;
            }

            ChiTietPN chiTiet = new ChiTietPN
            {
                MaCTPN = maCTPN,
                MaPN = maPN,
                MaSP = maSP,
                SoLuong = soLuong,
                DonGiaNhap = donGiaNhap
            };
            BUSChiTietPhieuNhap busChiTiet = new BUSChiTietPhieuNhap();
            string result = busChiTiet.AddChiTietPN(chiTiet);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm chi tiết phiếu nhập thành công!");
                ClearForm();
                LoadDanhSachChiTietPN();
            }
            else
            {
                MessageBox.Show(result);
            }

        }
        private void LoadInfo()
        {
            textMaPN.Text = phieuNhapHang.MaPN;
        }
        private void ClearForm()
        {
            btnthem1111.Enabled = true;
            btncapnhat.Enabled = false;
            btnxoa.Enabled = true;
            textMaCTPN.Clear();

            textSoLuong.Clear();
            textDonGiaNhap.Clear();
            textMaCTPN.Enabled = false;
        }

        private void frmchitietPN_Load(object sender, EventArgs e)
        {
            LoadInfo();
            LoadSanPham();
            LoadDanhSachChiTietPN();
            ClearForm();
        }

        private void guna2GradientButton6_Click(object sender, EventArgs e)
        {
            try
            {
                string maCTPN = textMaCTPN.Text.Trim();
                string maPN = textMaPN.Text.Trim();
                string maSP = cboTenSP.SelectedValue.ToString();
                string donGiaText = textDonGiaNhap.Text.Trim();
                string soLuongText = textSoLuong.Text.Trim();
                if (string.IsNullOrEmpty(maPN) || string.IsNullOrEmpty(maSP) ||
                    string.IsNullOrEmpty(donGiaText) || string.IsNullOrEmpty(soLuongText))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(donGiaText, out decimal donGiaNhap))
                {
                    MessageBox.Show("Đơn giá nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(soLuongText, out int soLuong))
                {
                    MessageBox.Show("Số lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ChiTietPN ct = new ChiTietPN
                {
                    MaCTPN = maCTPN,
                    MaPN = maPN,
                    MaSP = maSP,
                    SoLuong = soLuong,
                    DonGiaNhap = donGiaNhap
                };

                BUSChiTietPhieuNhap bus = new BUSChiTietPhieuNhap();
                bus.UpdateChiTietPN(ct);

                MessageBox.Show("Cập nhật chi tiết phiếu nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadDanhSachChiTietPN();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                    textMaCTPN.Text = row.Cells["MaCTPN"].Value?.ToString();
                    textMaPN.Text = row.Cells["MaPN"].Value?.ToString();
                    cboTenSP.SelectedValue = row.Cells["MaSP"].Value?.ToString();
                    textSoLuong.Text = row.Cells["SoLuong"].Value?.ToString();
                    textDonGiaNhap.Text = row.Cells["DonGiaNhap"].Value?.ToString();

                    btntimkiem.Enabled = false;
                    btncapnhat.Enabled = true;
                    btnxoa.Enabled = true;
                    textMaCTPN.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {
            string maCTPN = textMaCTPN.Text.Trim();
            string maSP = cboTenSP.SelectedValue.ToString();

            if (string.IsNullOrEmpty(maCTPN))
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                    maCTPN = selectedRow.Cells["MaCTPN"].Value?.ToString();
                    maSP = selectedRow.Cells["MaSP"].Value?.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn chi tiết phiếu nhập cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maCTPN))
            {
                MessageBox.Show("Xóa không thành công. Thiếu mã chi tiết phiếu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xác nhận xóa
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa chi tiết phiếu nhập:\n\nMã: {maCTPN}\nSản phẩm: {maSP}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    BUSChiTietPhieuNhap bus = new BUSChiTietPhieuNhap();
                    string kq = bus.DeleteChiTietPN(maCTPN);

                    if (string.IsNullOrEmpty(kq))
                    {
                        MessageBox.Show("Xóa chi tiết phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadDanhSachChiTietPN();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa: " + kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {

        }
    }
}
