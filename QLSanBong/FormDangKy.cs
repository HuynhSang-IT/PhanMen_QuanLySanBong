using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQLSanBong
{
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {

        }
        public bool CheckAccount(string ac)
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,24}$") && ac.ToLower() != "admin";
        }

        // Kiểm tra định dạng Email
        public bool CheckEmail(string em)
        {
            return Regex.IsMatch(em, "^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }

        Modify modify = new Modify();

        private void btnDangKy_Click_1(object sender, EventArgs e)
        {
            string tentk = txtTK.Text.Trim();
            string matkhau = txtMK.Text.Trim();
            string nhaplaimk = txtNhapLai.Text.Trim();
            string email = txtEmailDK.Text.Trim();

            if (!CheckAccount(tentk))
            {
                MessageBox.Show("Tên tài khoản không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckAccount(matkhau))
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nhaplaimk != matkhau)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra email đã tồn tại chưa
            string checkEmailQuery = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email";
            using (SqlConnection connection = KetNoi.GetSqlConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", email);
                        int emailCount = (int)checkCmd.ExecuteScalar();
                        if (emailCount > 0)
                        {
                            MessageBox.Show("Email này đã được đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Thực hiện đăng ký tài khoản
                    string insertQuery = "INSERT INTO TaiKhoan (TenTaiKhoan, MatKhau, Email, Quyen) VALUES (@TenTaiKhoan, @MatKhau, @Email, @Quyen)";
                    SqlParameter[] parameters =
                    {
    new SqlParameter("@TenTaiKhoan", tentk),
    new SqlParameter("@MatKhau", matkhau),
    new SqlParameter("@Email", email),
    new SqlParameter("@Quyen", "user")  // Gán mặc định là 'user'
};


                    modify.Command(insertQuery, parameters);
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi người dùng có muốn đăng nhập luôn không
                    if (MessageBox.Show("Bạn có muốn đăng nhập ngay không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
