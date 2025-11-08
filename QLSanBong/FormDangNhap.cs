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
    public partial class FormDangNhap: Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

       Modify modify = new Modify();
        private void btnDanhNhap_Click(object sender, EventArgs e)
        {
            string tentk = txtTaiKhoan.Text.Trim();
            string matkhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tentk))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query = "SELECT * FROM TaiKhoan WHERE TenTaiKhoan = @username AND MatKhau = @password";
            SqlParameter[] parameters = {
        new SqlParameter("@username", tentk),
        new SqlParameter("@password", matkhau)
    };

            try
            {
                var taiKhoans = modify.TaiKhoans(query, parameters);
                if (taiKhoans.Count > 0)
                {
                    TaiKhoan taiKhoan = taiKhoans.First();
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();

                    // Kiểm tra quyền và mở form tương ứng
                    if (taiKhoan.Quyen.Trim().ToLower() == "admin")
                    {
                        FormTrangChu formTrangChu = new FormTrangChu(taiKhoan);
                        formTrangChu.Show();
                    }
                    else if (taiKhoan.Quyen.Trim().ToLower() == "user")
                    {
                        MessageBox.Show("Nếu bạn Muốn đặt sân, hãy cung cấp các thông tin của bạn ở Thông Tin Khách Hàng nhé!!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormTrangUser formTrangUser = new FormTrangUser(taiKhoan);
                        formTrangUser.Show();
                    }
                    else
                    {
                        MessageBox.Show("Quyền tài khoản không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Show(); // Hiện lại Form đăng nhập nếu quyền không hợp lệ
                    }
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*string tentk = txtTaiKhoan.Text.Trim();
            string matkhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tentk))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Chuỗi kết nối SQL
            string connectionString = "Data Source=.;Initial Catalog=QuanLySanBong;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Quyen FROM TaiKhoan WHERE TenTaiKhoan = @username AND MatKhau = @password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", tentk);
                        command.Parameters.AddWithValue("@password", matkhau);

                        object result = command.ExecuteScalar(); // Lấy giá trị của cột Quyen

                        if (result != null) // Nếu có dữ liệu
                        {
                            string role = result.ToString().Trim(); // Lấy quyền

                            MessageBox.Show($"Quyền tài khoản: [{role}]", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Hide();

                            if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("Chuyển sang FormTrangChu (Admin)", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormTrangChu formTrangChu = new FormTrangChu();
                                formTrangChu.Show();
                            }
                            else if (role.Equals("user", StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show("Chuyển sang FormTrangUser", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormTrangUser formTrangUser = new FormTrangUser();
                                formTrangUser.Show();
                            }
                            else
                            {
                                MessageBox.Show("Quyền tài khoản không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Show(); // Hiện lại form nếu quyền không hợp lệ
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
        }

        private void linkLabel_DangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDangKy dangky = new FormDangKy();
            dangky.ShowDialog();
        }

        private void linkLabel_QuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormQuenMatKhau quenMatKhau = new FormQuenMatKhau();
            quenMatKhau.ShowDialog();
        }

        private void pictureBox_An_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '\0')
            {
                pictureBox_Hien.BringToFront();
                txtMatKhau.PasswordChar = '*';
            }
        }

        private void pictureBox_Hien_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '*')
            {
                pictureBox_An.BringToFront();
                txtMatKhau.PasswordChar = '\0';
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
