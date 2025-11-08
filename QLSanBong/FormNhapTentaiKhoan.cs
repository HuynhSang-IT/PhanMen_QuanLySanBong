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
    public partial class FormNhapTentaiKhoan: Form
    {
        public FormNhapTentaiKhoan()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtNhapTenTaiKhoan.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenTaiKhoan))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản.");
                return;
            }

            using (SqlConnection sqlConnection = new SqlConnection("Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True"))
            {
                try
                {
                    sqlConnection.Open();

                    // Kiểm tra tài khoản có tồn tại trong bảng TaiKhoan không
                    string checkTaiKhoan = "SELECT COUNT(*) FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan";
                    SqlCommand cmdCheckTK = new SqlCommand(checkTaiKhoan, sqlConnection);
                    cmdCheckTK.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                    int countTK = (int)cmdCheckTK.ExecuteScalar();

                    if (countTK == 0)
                    {
                        MessageBox.Show("Tên tài khoản không tồn tại. Vui lòng kiểm tra lại.");
                        return;
                    }

                    // Kiểm tra tài khoản đã có trong KhachHang chưa
                    string checkKhachHang = "SELECT COUNT(*) FROM KhachHang WHERE TenTaiKhoan = @TenTaiKhoan";
                    SqlCommand cmdCheckKH = new SqlCommand(checkKhachHang, sqlConnection);
                    cmdCheckKH.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                    int countKH = (int)cmdCheckKH.ExecuteScalar();

                    if (countKH == 0)
                    {
                        // Nếu chưa có, yêu cầu nhập thông tin
                        MessageBox.Show("Tài khoản chưa có thông tin khách hàng. Vui lòng nhập thông tin mới.");
                    }

                    // Mở Form Thông Tin Khách Hàng
                    FormThongTinKhachHang formThongTinKhachHang = new FormThongTinKhachHang(tenTaiKhoan);
                    formThongTinKhachHang.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        

        private void FormNhapTentaiKhoan_Load(object sender, EventArgs e)
        {

        }
    }
 }
