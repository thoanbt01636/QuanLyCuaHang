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
    public partial class frmSP : Form
    {
        public frmSP()
        {
            InitializeComponent();
        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmSP_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            ClearForm();
            LoadLoaiSanPham();
            
        }
        private void ClearForm()
        {
            BUSSanPham bUSSanPham = new BUSSanPham();
            textMaSP.Clear();
            textMaSP.Text = bUSSanPham.TaoMaTuDong();
            btnThem.Enabled = true;
            btnCapNhat.Enabled = false;
            btnXoa.Enabled = true;
            textTenSP.Clear();
            textDonGIa.Clear();
            txtSoluongton.Clear();
            textMaSP.Enabled = false;
            btnXoa.Enabled = false;

        }
        private void LoadSanPham()
        {

            BUSSanPham bUSSanPham = new BUSSanPham();
            guna2DataGridView1.DataSource = null;
            List<SanPham> lstSP = bUSSanPham.GetSanPhamList();
            guna2DataGridView1.DataSource = lstSP;

        }
        private void LoadLoaiSanPham()
        {
            try
            {
                BUSLoaiSP bUSLoaiSanPham = new BUSLoaiSP();
                List<LoaiSanPham> dsLoai = bUSLoaiSanPham.GetLoaiSanPhamList();
                cboMaLoaiSP.DataSource = dsLoai;
                cboMaLoaiSP.ValueMember = "MaLoai";
                cboMaLoaiSP.DisplayMember = "TenLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {


            try
            {
                string tenSP = textTenSP.Text.Trim();
                string donGiaText = textDonGIa.Text.Trim();
                string maLoai = cboMaLoaiSP.SelectedValue?.ToString();
                string soLuongTonText = txtSoluongton.Text.Trim();

                if (string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(donGiaText) || string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(soLuongTonText))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(donGiaText, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(soLuongTonText, out int soLuongTon))
                {
                    MessageBox.Show("Số lượng tồn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SanPham sp = new SanPham
                {
                    TenSP = tenSP,
                    DonGia = donGia,
                    MaLoai = maLoai,
                    SoLuongTon = soLuongTon

                };
                BUSSanPham bUSSanPham = new BUSSanPham();
                bUSSanPham.AddSanPham(sp);

                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                string maSP = textMaSP.Text.Trim();
                string tenSP = textTenSP.Text.Trim();
                string donGiaText = textDonGIa.Text.Trim();
                string maLoai = cboMaLoaiSP.SelectedValue?.ToString();
                string soLuongTonText = txtSoluongton.Text.Trim();

                if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(donGiaText) || string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(soLuongTonText))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(donGiaText, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(soLuongTonText, out int soLuongTon))
                {
                    MessageBox.Show("Số lượng tồn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SanPham sp = new SanPham
                {
                    MaSP = maSP,
                    TenSP = tenSP,
                    DonGia = donGia,
                    MaLoai = maLoai,
                    SoLuongTon = soLuongTon
                };

                BUSSanPham bUSSanPham = new BUSSanPham();
                bUSSanPham.UpdateSanPham(sp);

                MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            string maSP = textMaSP.Text.Trim();
            string tenSP = textTenSP.Text.Trim();

            if (string.IsNullOrEmpty(maSP))
            {
                if (guna2DataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                    maSP = selectedRow.Cells["MaSP"].Value.ToString();
                    tenSP = selectedRow.Cells["TenSP"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sản phẩm để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm {maSP} - {tenSP}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSSanPham bus = new BUSSanPham();
                string kq = bus.DeleteSanPham(maSP);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin sản phẩm {maSP} - {tenSP} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadSanPham();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                textMaSP.Text = row.Cells["MaSP"].Value.ToString();
                textTenSP.Text = row.Cells["TenSP"].Value.ToString();
                textDonGIa.Text = row.Cells["DonGia"].Value.ToString();
                txtSoluongton.Text = row.Cells["SoLuongTon"].Value.ToString();
                cboMaLoaiSP.SelectedValue = row.Cells["MaLoai"].Value.ToString();
                btnThem.Enabled = false;
                btnCapNhat.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadSanPham();
            LoadLoaiSanPham();
        }
    }
}




