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
    public partial class FormQuenMatKhau: Form
    {
        public FormQuenMatKhau()
        {
            InitializeComponent();
            label3.Text = " ";
        }

        private void FormQuenMatKhau_Load(object sender, EventArgs e)
        {

        }
        Modify modify = new Modify();
        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (email.Trim() == "") { MessageBox.Show("Vui lòng nhập email đăng ký!"); }
            else
            {
                string query = "Select * from TaiKhoan where Email = '" + email + "'";
                if (modify.TaiKhoans(query).Count != 0)
                {
                    label3.ForeColor = Color.Blue;
                    label3.Text = "Mật Khẩu: " + modify.TaiKhoans(query)[0].MatKhau;
                }
                else
                {
                    label3.ForeColor = Color.Red;
                    label3.Text = "Email này chưa được đăng ký!";
                }
            }
            /*(string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email đăng ký!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT MatKhau FROM TaiKhoan WHERE Email = @Email";
            SqlParameter[] parameters = { new SqlParameter("@Email", email) };

            try
            {
                var taiKhoans = modify.TaiKhoans(query, parameters);
                if (taiKhoans != null && taiKhoans.Count > 0) // Kiểm tra danh sách không rỗng
                {
                    label3.ForeColor = Color.Blue;
                    label3.Text = "Mật khẩu: " + taiKhoans[0].MatKhau;
                }
                else
                {
                    label3.ForeColor = Color.Red;
                    label3.Text = "Email này chưa được đăng ký!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }
    }
}
