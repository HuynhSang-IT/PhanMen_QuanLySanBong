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
    public partial class FormThongTinKhachHang: Form
    {
        private string tenTaiKhoan;
        SqlConnection sqlConnection = new SqlConnection("Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True");
        public FormThongTinKhachHang(string tenTaiKhoan)
        {
            InitializeComponent();
            this.tenTaiKhoan = tenTaiKhoan;
            LoadData();
            LoadDataGridView();
            
        }

        private void LoadData()
        {
            try
            {
                sqlConnection.Open();
                string query = @"
            SELECT STT, TenKhachHang, DiaChi, NgaySinh, QueQuan, SDT, TenTaiKhoan 
            FROM KhachHang 
            WHERE TenTaiKhoan = @TenTaiKhoan";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.Add("@TenTaiKhoan", SqlDbType.NVarChar, 50).Value = tenTaiKhoan;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0) // Đã có thông tin khách hàng
                        {
                            DataRow row = dataTable.Rows[0];

                            txtHoten.Text = row["TenKhachHang"].ToString();
                            txtDiaChi.Text = row["DiaChi"].ToString();
                            dtpNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                            txtQueQuan.Text = row["QueQuan"].ToString();
                            txtSDT.Text = row["SDT"].ToString();
                        }
                        else // Chưa có thông tin, yêu cầu nhập mới
                        {
                            MessageBox.Show("Tài khoản chưa có thông tin khách hàng. Vui lòng nhập đầy đủ.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void LoadDataGridView()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection("Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True"))
                {
                    sqlConnection.Open();
                    string query = "SELECT STT, TenKhachHang, DiaChi, NgaySinh, QueQuan, SDT, TenTaiKhoan FROM KhachHang";

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Đổ dữ liệu vào DataGridView
                            dgvKhachHang.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        private void btnNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                string query = @"
            INSERT INTO KhachHang (TenKhachHang, DiaChi, NgaySinh, QueQuan, SDT, TenTaiKhoan) 
            VALUES (@TenKhachHang, @DiaChi, @NgaySinh, @QueQuan, @SDT, @TenTaiKhoan)";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@TenKhachHang", txtHoten.Text);
                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    command.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                    command.Parameters.AddWithValue("@SDT", int.Parse(txtSDT.Text)); // Chuyển SDT về kiểu int
                    command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm khách hàng thành công!");
                        LoadDataGridView(); // Cập nhật danh sách
                    }
                    else
                    {
                        MessageBox.Show("Thêm khách hàng thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoten.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!");
                return;
            }

            try
            {
                sqlConnection.Open();
                string query = @"
            UPDATE KhachHang 
            SET TenKhachHang = @TenKhachHang, DiaChi = @DiaChi, NgaySinh = @NgaySinh, 
                QueQuan = @QueQuan, SDT = @SDT
            WHERE TenTaiKhoan = @TenTaiKhoan";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@TenKhachHang", txtHoten.Text);
                    command.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    command.Parameters.AddWithValue("@QueQuan", txtQueQuan.Text);
                    command.Parameters.AddWithValue("@SDT", int.Parse(txtSDT.Text));
                    command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật khách hàng thành công!");
                        LoadDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoten.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?","Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No)
                return;

            try
            {
                sqlConnection.Open();
                string query = "DELETE FROM KhachHang WHERE TenTaiKhoan = @TenTaiKhoan";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!");
                        LoadDataGridView();
                        ClearForm(); // Xóa dữ liệu trên form
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void ClearForm()
        {
            txtHoten.Clear();
            txtDiaChi.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            txtQueQuan.Clear();
            txtSDT.Clear();
        }
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không click vào tiêu đề
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtHoten.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtQueQuan.Text = row.Cells["QueQuan"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString(); // SDT là kiểu int, cần ToString()
            }
        }

        private void FormThongTinKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}
