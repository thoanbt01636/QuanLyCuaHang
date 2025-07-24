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

        }
        public void ClearForm(string maNCC)
        {
            btnthem1111.Enabled = true;
            btncapnhat.Enabled = false;
            btnxoa.Enabled = false;

            textMaPhieu.Clear();

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

    }

}

