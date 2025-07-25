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
using UTIL_QuanLyCuaHang;

namespace GUI_QuanLyThuVien
{
    public partial class frmPhieuNhap : Form
    {
        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void lblQLNV_Click(object sender, EventArgs e)
        {

        }

        private void frmMuonTraSach_Load(object sender, EventArgs e)
        {
            LoadPhieuNhap();
            LoadNhacungcap();
            LoadNhanVien();
            ClearForm(string.Empty);
            textMaPhieu.Enabled = false;

        }
        public void ClearForm(string maNCC)
        {
            BUSPhieuNhap busPhieuNhap = new BUSPhieuNhap();
            btnthem1111.Enabled = true;
            btncapnhat.Enabled = false;
            btnxoa.Enabled = false;
            textMaPhieu.Clear();
            textMaPhieu.Text = busPhieuNhap.TaoMaTuDong();
            cboNhanVien.Enabled = true;
            cboNhaCungCap.Enabled = true;
            cboNhaCungCap.SelectedValue = maNCC;

            dateTPNgayTao.Enabled = true;
            dateTPNgayTao.Value = DateTime.Now;

            rbDaNhapKho.Enabled = true;
            rbDangXuLy.Enabled = true;

            rbDaNhapKho.Checked = true;
        }
        private void LoadPhieuNhap()
        {
            BUSPhieuNhap busPhieuBanHang = new BUSPhieuNhap();
            List<PhieuNhap> lst = busPhieuBanHang.GetListPhieuNhap();


            if (!AuthUtil.User.ChucVu)
            {
                lst = lst.Where(x => x.MaNV == AuthUtil.User.MaNV).ToList();
                cboNhanVien.Enabled = false;
            }
            else
            {
                cboNhanVien.Enabled = true;
            }

            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.DataSource = lst;
            DataGridViewImageColumn buttonColumn = new DataGridViewImageColumn();
            buttonColumn.Name = "XemHang";
            buttonColumn.HeaderText = "xem đơn hàng";
            buttonColumn.Image = GUI_QuanLyCuaHang.Properties.Resources.may;
            buttonColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            buttonColumn.DefaultCellStyle.BackColor = Color.LightBlue;
            buttonColumn.DefaultCellStyle.ForeColor = Color.DarkBlue;
            buttonColumn.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            guna2DataGridView1.Columns.Add(buttonColumn);
            guna2DataGridView1.Columns["xemHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            guna2DataGridView1.RowTemplate.Height = 50;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }
        private void LoadNhacungcap()
        {
            try
            {
                BUSNhaCungCap bUSNhaCungCap = new BUSNhaCungCap();
                List<NhaCungCap> dslNCC = bUSNhaCungCap.GetNhaCungCapList();
                cboNhaCungCap.DataSource = dslNCC;
                cboNhaCungCap.ValueMember = "MaNCC";
                cboNhaCungCap.DisplayMember = "TenNCC";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadNhanVien()

        {
            try
            {
                BUSNhanVien busNhanVien = new BUSNhanVien();
                List<NhanVien> dsLoai = busNhanVien.GetNhanViensList();
                if (AuthUtil.User.ChucVu)
                {
                    dsLoai.Insert(0, new NhanVien() { MaNV = string.Empty, TenNV = string.Format("--Vui lòng chọn nhân viên--") });
                }
                else
                {
                    dsLoai = dsLoai.Where(x => x.MaNV == AuthUtil.User.MaNV).ToList();
                    cboNhanVien.Enabled = false;
                }
                cboNhanVien.DataSource = dsLoai;
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.DisplayMember = "TenNV";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnthem1111_Click(object sender, EventArgs e)
        {

            string maNCC = cboNhaCungCap.SelectedValue?.ToString();
            string maNhanVien = cboNhanVien.SelectedValue?.ToString();
            DateTime ngayTao = dateTPNgayTao.Value;
            bool trangThai = rbDaNhapKho.Checked;

            if (string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phiếu nhập.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PhieuNhap phieuNhap = new PhieuNhap
            {
                MaNCC = maNCC,
                MaNV = maNhanVien,
                NgayNhap = ngayTao,
                TrangThai = trangThai
            };

            BUSPhieuNhap bus = new BUSPhieuNhap();
            string result = bus.AddPhieuNhap(phieuNhap);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm(maNCC);
                LoadPhieuNhap();
                cboNhaCungCap.SelectedValue = maNCC;
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string maPN = guna2DataGridView1.Rows[e.RowIndex].Cells["MaPN"].Value.ToString();
            string maNV = guna2DataGridView1.Rows[e.RowIndex].Cells["MaNV"].Value.ToString();
            string maNCC = guna2DataGridView1.Rows[e.RowIndex].Cells["MaNCC"].Value.ToString();

            PhieuNhap phieuNhap = (PhieuNhap)guna2DataGridView1.CurrentRow.DataBoundItem;
            NhanVien nv = new NhanVien();
            NhaCungCap ncc = new NhaCungCap();

            foreach (NhanVien item in cboNhanVien.Items)
            {
                if (item.MaNV == maNV)
                {
                    nv = item;
                    break;
                }
            }

            foreach (NhaCungCap item in cboNhaCungCap.Items)
            {
                if (item.MaNCC == maNCC)
                {
                    ncc = item;
                    break;
                }
            }
            frmchitietPN chiTiet = new frmchitietPN(phieuNhap, nv, ncc);
            chiTiet.ShowDialog();

        }

        private void btncapnhat_Click(object sender, EventArgs e)
        {
            string maPhieu = textMaPhieu.Text;
            string maNhanVien = cboNhanVien.SelectedValue?.ToString();
            string maNCC = cboNhaCungCap.SelectedValue?.ToString();
            DateTime ngayNhap = dateTPNgayTao.Value;

            bool trangThai;

            if (rbDaNhapKho.Checked)
            {
                trangThai = true;
            }
            else
            {
                trangThai = false;
            }

            if (!trangThai)
            {
                MessageBox.Show("Chỉ được phép sửa phiếu đã thanh toán!");
                return;
            }

            if (string.IsNullOrEmpty(maNhanVien) || string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
                return;
            }

            PhieuNhap phieu = new PhieuNhap()
            {
                MaPN = maPhieu,
                MaNV = maNhanVien,
                MaNCC = maNCC,
                NgayNhap = ngayNhap,
                TrangThai = trangThai
            };

            BUSPhieuNhap bus = new BUSPhieuNhap();
            string result = bus.UpdatePhieuNhap(phieu);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công!");
                ClearForm(maNCC);
                LoadPhieuNhap();
                LoadNhacungcap();
                LoadNhanVien();
                cboNhaCungCap.SelectedValue = maNCC;
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string maPhieu = textMaPhieu.Text;

            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BUSPhieuNhap bus = new BUSPhieuNhap();
                string error = bus.DeletePhieuNhap(maPhieu);

                if (string.IsNullOrEmpty(error))
                {
                    MessageBox.Show("Xóa phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm(string.Empty);
                    LoadPhieuNhap();
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa: " + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                textMaPhieu.Text = row.Cells["MaPN"].Value.ToString();
                dateTPNgayTao.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                cboNhanVien.SelectedValue = row.Cells["MaNV"].Value.ToString();
                cboNhaCungCap.SelectedValue = row.Cells["MaNCC"].Value.ToString();

                bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                if (trangThai)
                {
                    rbDaNhapKho.Checked = true;
                }
                else
                {
                    rbDangXuLy.Checked = true;
                }

                btncapnhat.Enabled = true;
                btnxoa.Enabled = true;
                btnthem1111.Enabled = false;
            }
        }

        private void btnlamoi_Click(object sender, EventArgs e)
        {
            ClearForm(string.Empty);
        }

        private void textMaPhieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void QRCODE_Click(object sender, EventArgs e)
        {
            string maPhieu = textMaPhieu.Text.Trim();

            if (string.IsNullOrEmpty(maPhieu))
            {
                MessageBox.Show("Vui lòng nhập Mã Phiếu Nhập để tạo QR Code!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sqlCheck = "SELECT COUNT(*) FROM PhieuNhap WHERE MaPN = @MaPN";

            var args = new Dictionary<string, object> { { "MaPN", maPhieu } };

            int count = Convert.ToInt32(DBUtil.ScalarQuery1(sqlCheck, args));

            if (count == 0)
            {
                MessageBox.Show("Mã Phiếu Nhập không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(maPhieu, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            picQRCode.Image = qrCodeImage;

            MessageBox.Show("Tạo QR Code thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            BUSPhieuNhap busPhieuNhap = new BUSPhieuNhap();
            List<PhieuNhap> danhSach = busPhieuNhap.GetListPhieuNhap();

            if (!AuthUtil.User.ChucVu)
            {
                danhSach = danhSach.Where(pn => pn.MaNV == AuthUtil.User.MaNV).ToList();
            }

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                danhSach = danhSach.Where(pn =>
                    pn.MaPN.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    pn.MaNV.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase) ||
                    pn.MaNCC.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.DataSource = danhSach;

            DataGridViewImageColumn buttonColumn = new DataGridViewImageColumn();
            buttonColumn.Name = "XemHang";
            buttonColumn.HeaderText = "xem đơn hàng";
            buttonColumn.Image = GUI_QuanLyCuaHang.Properties.Resources.may;
            buttonColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            buttonColumn.DefaultCellStyle.BackColor = Color.LightBlue;
            buttonColumn.DefaultCellStyle.ForeColor = Color.DarkBlue;
            buttonColumn.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            guna2DataGridView1.Columns.Add(buttonColumn);
            guna2DataGridView1.Columns["xemHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            guna2DataGridView1.RowTemplate.Height = 50;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }

}

