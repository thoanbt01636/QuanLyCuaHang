using BLL_QuanLyCuaHang;
using DAL_QuanLyCuaHang;
using DTO_QuanLyCuaHang;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QuanLyThuVien
{
    public partial class frmHD : Form
    {
        private BUSHoaDon busHoaDon = new BUSHoaDon();
        private BUSNhanVien busNhanVien = new BUSNhanVien();

        public frmHD()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadHoaDon();
            ClearForm();
            txtMaHD.Enabled = false;
        }

        private void LoadNhanVien()
        {
            try
            {
                List<NhanVien> dsNhanVien = busNhanVien.GetNhanViensList();
                cboMaNhanVien.DataSource = dsNhanVien;
                cboMaNhanVien.ValueMember = "MaNV";
                cboMaNhanVien.DisplayMember = "TenNV";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHoaDon(string maNV = null)
        {
            try
            {
                List<HoaDon> dsHoaDon = busHoaDon.GetHoaDonList(maNV);
                guna2DataGridView1.DataSource = dsHoaDon;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hóa đơn: {ex.Message}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GroupBox2_Click(object sender, EventArgs e) { }

        private void ClearForm()
        {
            BUSHoaDon bUSHoaDon = new BUSHoaDon();
            txtMaHD.Clear();
            txtMaHD.Text = bUSHoaDon.TaoMaTuDong();
            btnCapNhat.Enabled = true;
            guna2GradientButton1.Enabled = true;
            btnXoa.Enabled = true;
            rbChoThanhToan.Checked = true;
            rbDaThanhToan.Checked = false;
            picQRCode.Image = null;
            btnCapNhat.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            bool trangThai = rbDaThanhToan.Checked;
            HoaDon hd = new HoaDon
            {
                MaNV = cboMaNhanVien.SelectedValue.ToString(),
                NgayLap = NgayLap.Value,
                TrangThai = trangThai
            };

            string result = busHoaDon.Insert(hd);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHoaDon();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool trangThai = rbDaThanhToan.Checked;
            HoaDon hd = new HoaDon
            {
                MaHD = txtMaHD.Text,
                MaNV = cboMaNhanVien.SelectedValue.ToString(),
                NgayLap = NgayLap.Value,
                TrangThai = trangThai
            };

            string result = busHoaDon.Update(hd);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHoaDon();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string result = busHoaDon.Delete(txtMaHD.Text);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHoaDon();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e) { }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadHoaDon();
        }

        private void frmHD_Load(object sender, EventArgs e) { }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                txtMaHD.Text = row.Cells["MaHD"].Value?.ToString() ?? string.Empty;
                cboMaNhanVien.SelectedValue = row.Cells["MaNV"].Value?.ToString() ?? string.Empty;
                if (row.Cells["NgayLap"].Value != null)
                    NgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
                if (row.Cells["TrangThai"].Value != null)
                {
                    bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                    rbChoThanhToan.Checked = !trangThai;
                    rbDaThanhToan.Checked = trangThai;
                }
                btnThem.Enabled = false;
                btnCapNhat.Enabled = true;
                btnXoa.Enabled = true;  
            }
        }

        private void guna2DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string maPhieu = guna2DataGridView1.Rows[e.RowIndex].Cells["MaHD"].Value.ToString();
            string maNV = guna2DataGridView1.Rows[e.RowIndex].Cells["MaNV"].Value.ToString();
            HoaDon phieu = (HoaDon)guna2DataGridView1.CurrentRow.DataBoundItem;

            NhanVien nv = new NhanVien();


            foreach (NhanVien item in cboMaNhanVien.Items)
            {
                if (item.MaNV == maNV)
                {
                    nv = item;
                    break;
                }
            }
            frmchitietHD ChiTiet = new frmchitietHD(phieu, nv);
            ChiTiet.ShowDialog();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void QRCODE_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text.Trim();

            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Vui lòng nhập Mã Phiếu Nhập để tạo QR Code!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlCheck = "SELECT COUNT(*) FROM HoaDon WHERE MaHD = @MaHD";

            var args = new Dictionary<string, object> { { "MaHD", maHD } };

            int count = Convert.ToInt32(DBUtil.ScalarQuery1(sqlCheck, args));

            if (count == 0)
            {
                MessageBox.Show("Mã Hóa Đơn không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(maHD, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            picQRCode.Image = qrCodeImage;

            MessageBox.Show("Tạo QR Code thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

