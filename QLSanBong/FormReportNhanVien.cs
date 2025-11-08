using Microsoft.Reporting.WinForms;
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
    public partial class FormReportNhanVien : Form
    {
        public FormReportNhanVien()
        {
            InitializeComponent();
        }

        private void FormReportNhanVien_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {

            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\ReportNhanVien.rdlc";

                DataTable dt = GetDataFromDatabase(); // Gọi hàm lấy dữ liệu

                if (dt.Rows.Count > 0)
                {
                    ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.RefreshReport();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable GetDataFromDatabase()
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True"; // Sửa lại kết nối đúng

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaNV, HoTenNV, QueQuan, NgaySinh, NgayVaoLam, ViTri, QuanLyKhu, Luong FROM NhanVien"; // Sửa lại nếu cần
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            return dt;
        }
    }
}
