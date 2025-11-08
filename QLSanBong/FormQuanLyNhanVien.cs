using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQLSanBong
{
    public partial class FormQuanLyNhanVien: Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True");
        public FormQuanLyNhanVien()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            //lấy dữ liệu từ sơ sỡ dữ liệu
            string query = "Select * From NhanVien";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            dgvNhanVien.DataSource = data;
        }
        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            //Thêm dữ liệu vào Mã Nhân Viên
            cbxMaNhanVien.Items.Add("NV01");
            cbxMaNhanVien.Items.Add("NV02");
            cbxMaNhanVien.Items.Add("NV03");
            cbxMaNhanVien.Items.Add("NV04");
            cbxMaNhanVien.Items.Add("NV05");

            //Thêm dữ liệu vào Vị Trí
            cbxViTri.Items.Add("Quản Lý");
            cbxViTri.Items.Add("Nhân Viên");
            cbxViTri.Items.Add("Trọng Tài");

            //Thêm dữ liệu vào Khu
            cbxKhu.Items.Add("Sân 1");
            cbxKhu.Items.Add("Sân 2");
            cbxKhu.Items.Add("Sân 3");
            cbxKhu.Items.Add("Sân 4");
            cbxKhu.Items.Add("Sân 5");
            cbxKhu.Items.Add("Sân 6");

            //Thêm dữ liệu vào bảng lương
            cbxLuong.Items.Add("17.000VNĐ");
            cbxLuong.Items.Add("20.000VNĐ");
            cbxLuong.Items.Add("25.000VNĐ");
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0) // e.RowIndex là -1 cho các ô tiêu đề
            {
                //Truy xuất dữ liệu từ ô đã nhấp
                DataGridViewRow selectedRow = dgvNhanVien.Rows[e.RowIndex];

                //Giả sử các cột trong DataGridView được lập chỉ mục chính xác
                string manhanvien = selectedRow.Cells["MaNV"].Value.ToString();
                string hotennhanvien = selectedRow.Cells["HoTenNV"].Value.ToString();
                string quequan = selectedRow.Cells["QueQuan"].Value.ToString();
                string ngaysinh = selectedRow.Cells["Ngaysinh"].Value.ToString();
                string ngayvaolam = selectedRow.Cells["NgayVaoLam"].Value.ToString();
                string vitri = selectedRow.Cells["ViTri"].Value.ToString();
                string khu = selectedRow.Cells["QuanLyKhu"].Value.ToString();
                string luong = selectedRow.Cells["Luong"].Value.ToString();

                //Hiển thị dữ liệu trong các điều khiển có liên quan
                cbxMaNhanVien.Text = manhanvien;
                txtHoTen.Text = hotennhanvien;
                txtQueQuan.Text = quequan;
                dtpNgaySinh.Text = ngaysinh;
                dtpNgayVaoLam.Text = ngayvaolam;
                cbxViTri.SelectedItem = vitri;
                cbxKhu.SelectedItem = khu;
                cbxLuong.Text = luong;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có trường nào trống không
                if (string.IsNullOrWhiteSpace(cbxMaNhanVien.Text) ||
                    string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtQueQuan.Text) ||
                    dtpNgaySinh.Value == null ||
                    dtpNgayVaoLam.Value == null ||
                    string.IsNullOrWhiteSpace(cbxViTri.Text) ||
                    string.IsNullOrWhiteSpace(cbxKhu.Text) ||
                    string.IsNullOrWhiteSpace(cbxLuong.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    return; // Thoát khỏi phương thức nếu xác thực không thành công
                }

                string query = "Insert into NhanVien (MaNV, HoTenNV, QueQuan, NgaySinh, NgayVaoLam, ViTri, QuanLyKhu, Luong) Values (@MaNV, @HoTenNV, @QueQuan, @NgaySinh, @NgayVaoLam, @ViTri, @QuanLyKhu, @Luong)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();

                    cmd.Parameters.AddWithValue("@MaNV", cbxMaNhanVien.Text);
                    cmd.Parameters.AddWithValue("@HoTenNV", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@NgayVaolam", dtpNgayVaoLam.Value);
                    cmd.Parameters.AddWithValue("@ViTri", cbxViTri.Text.ToString());
                    cmd.Parameters.AddWithValue("@QuanLyKhu", cbxKhu.Text.ToString());
                    cmd.Parameters.AddWithValue("@Luong", cbxLuong.Text.ToString());
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                LoadData();// Tải lại dữ liệu để hiển thị mục nhập mới
                MessageBox.Show("Thêm nhân viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE NhanVien SET HoTenNV = @HoTenNV, QueQuan = @QueQuan, Ngaysinh = @Ngaysinh, NgayVaoLam = @NgayVaoLam, ViTri = @ViTri, QuanLyKhu = @QuanLyKhu, Luong = @Luong WHERE MaNV = @MaNV";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@MaNV", cbxMaNhanVien.Text);
                    command.Parameters.AddWithValue("@HoTenNV", txtHoTen.Text);
                    command.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                    command.Parameters.AddWithValue("@Ngaysinh", dtpNgaySinh.Value);
                    command.Parameters.AddWithValue("@NgayVaoLam", dtpNgayVaoLam.Value);
                    command.Parameters.AddWithValue("@ViTri", cbxViTri.Text.ToString());
                    command.Parameters.AddWithValue("@QuanLyKhu", cbxKhu.Text.ToString());
                    command.Parameters.AddWithValue("@Luong", cbxLuong.Text.ToString());

                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                LoadData();
                MessageBox.Show("Cập nhật nhân viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbxMaNhanVien.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để xoá.!!");
                    return;
                }

                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@MaNV", cbxMaNhanVien.Text);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                LoadData();
                MessageBox.Show("Xóa nhân viên thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnIN_Click(object sender, EventArgs e)
        {
            FormReportNhanVien form = new FormReportNhanVien();
            form.Show();
        }
    }
}
