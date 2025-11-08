using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQLSanBong
{
   public class Modify
    {
        /*public Modify()
        {
        }
        SqlCommand sqlCommand;  // Khai báo biến sqlCommand để thực hiện các câu lệnh SQL như insert, update, delete...
        SqlDataReader dataReader; //Khai báo biến dataReader để đọc dữ liệu từ bảng dùng để đọc dữ liệu trong bảng.

        // Phương thức TaiKhoans để kiểm tra tài khoản
        public List<TaiKhoan> TaiKhoans(string query)//check tài khoản
        {
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>(); // Khởi tạo danh sách để lưu trữ các đối tượng TaiKhoan

            using (SqlConnection sqlConnection = KetNoi.GetSqlConnection()) // Sử dụng khối using để tự động giải phóng tài nguyên SqlConnection
            {
                sqlConnection.Open();

                sqlCommand = new SqlCommand(query, sqlConnection); // Khởi tạo SqlCommand với câu truy vấn và kết nối
                dataReader = sqlCommand.ExecuteReader();  // Thực thi câu lệnh và trả về SqlDataReader để đọc dữ liệu
                while (dataReader.Read()) // Đọc dữ liệu từ SqlDataReader
                {
                    taiKhoans.Add(new TaiKhoan(
                                         dataReader.GetString(0),  // TenTaiKhoan
                                         dataReader.GetString(1),  // MatKhau
                                         dataReader.IsDBNull(2) ? "user" : dataReader.GetString(2) // Nếu NULL, gán "user"
                                                                                )); // Thêm đối tượng TaiKhoan vào danh sách từ dữ liệu đọc được
                }

                sqlConnection.Close();  // Đóng kết nối (tự động thực hiện khi ra khỏi khối using)
            }

            return taiKhoans;
        }

        // Phương thức Command để thực hiện các câu lệnh SQL như đăng ký tài khoản
        public void Command(string query) //dùng để đăng kí tài khoản
        {
            using (SqlConnection sqlConnection = KetNoi.GetSqlConnection())
            {
                sqlConnection.Open();
                // Khởi tạo SqlCommand với câu truy vấn và kết nối
                sqlCommand = new SqlCommand(query, sqlConnection);
                // Thực thi câu lệnh SQL không trả về dữ liệu (insert, update, delete)
                sqlCommand.ExecuteNonQuery();//thực thi câu truy vấn 
                sqlConnection.Close();
            }
        }
    }*/
            public Modify() { }

            SqlCommand sqlCommand;
            SqlDataReader dataReader;

        // Kiểm tra tài khoản tồn tại
        public List<TaiKhoan> TaiKhoans(string query, SqlParameter[] parameters = null)
        {
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>();

            try
            {
                using (SqlConnection sqlConnection = KetNoi.GetSqlConnection())
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection)) // Tạo SqlCommand đúng cách
                    {
                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader()) // Đọc dữ liệu đúng cách
                        {
                            while (dataReader.Read())
                            {
                                // Đọc dữ liệu an toàn, tránh lỗi NULL
                                string tenTaiKhoan = dataReader.GetString(0);
                                string matKhau = dataReader.GetString(1);
                                string quyen = dataReader.IsDBNull(2) ? "user" : dataReader.GetString(2); // Tránh lỗi NULL

                                taiKhoans.Add(new TaiKhoan(tenTaiKhoan, matKhau, quyen));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return taiKhoans;
        }

        /*public DataTable GetDataTable(string query, SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = KetNoi.GetSqlConnection())
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(query, sqlConnection);

                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                throw;
            }

            return table;
        }*/



        // Thực thi câu lệnh SQL
        public void Command(string query, SqlParameter[] parameters)
            {
                using (SqlConnection sqlConnection = KetNoi.GetSqlConnection())
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand(query, sqlConnection);

                    // Thêm tham số nếu có
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
