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
    public partial class FormHeThong : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True");

        public FormHeThong()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT * FROM TaiKhoan";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection))
                {
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    gdvHeThong.DataSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string timkiemQuery = "SELECT * FROM TaiKhoan WHERE TenTaiKhoan LIKE @TenTaiKhoan";
                using (SqlCommand sqlCommand = new SqlCommand(timkiemQuery, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@TenTaiKhoan", "%" + txtTenTaiKhoan.Text.Trim() + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        DataTable data = new DataTable();
                        adapter.Fill(data);
                        gdvHeThong.DataSource = data;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gdvHeThong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = gdvHeThong.Rows[e.RowIndex];

                txtTaiKhoan.Text = row.Cells["TenTaiKhoan"].Value?.ToString() ?? "";
                txtMatKhau.Text = row.Cells["MatKhau"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtQuyen.Text = row.Cells["Quyen"].Value?.ToString() ?? "";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtQuyen.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                sqlConnection.Open();
                string insertQuery = "INSERT INTO TaiKhoan (TenTaiKhoan, MatKhau, Email, Quyen) VALUES (@TenTaiKhoan, @MatKhau, @Email, @Quyen)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@TenTaiKhoan", txtTaiKhoan.Text.Trim());
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quyen", txtQuyen.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtQuyen.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản và điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                sqlConnection.Open();
                string checkQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE LOWER(TenTaiKhoan) = LOWER(@TenTaiKhoan)";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, sqlConnection))
                {
                    checkCmd.Parameters.AddWithValue("@TenTaiKhoan", txtTaiKhoan.Text.Trim());
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Không được sửa tên tài khoản, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string suaQuery = "UPDATE TaiKhoan SET MatKhau=@MatKhau, Email=@Email, Quyen=@Quyen WHERE TenTaiKhoan=@TenTaiKhoan";
                using (SqlCommand cmd = new SqlCommand(suaQuery, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@TenTaiKhoan", txtTaiKhoan.Text.Trim());
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Quyen", txtQuyen.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                sqlConnection.Open();
                string xoatk = "DELETE FROM TaiKhoan WHERE TenTaiKhoan=@TenTaiKhoan";
                using (SqlCommand cmd = new SqlCommand(xoatk, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@TenTaiKhoan", txtTaiKhoan.Text.Trim());
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void FormHeThong_Load(object sender, EventArgs e)
        {

        }
    }
}
