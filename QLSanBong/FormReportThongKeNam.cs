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
    public partial class FormReportThongKeNam : Form
    {
        private int nam;
        private string connectionString = "Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True";
        public FormReportThongKeNam(int nam)
        {
            InitializeComponent();
            this.nam = nam;
        }

        private void FormReportThongKeNam_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            LoadBaoCao();
        }
        private void LoadBaoCao()
        {
            // Cập nhật câu truy vấn để lọc theo năm
            string query = @"
SELECT tk.STT, tk.MaSan, tk.NgayDat, tk.TongSoTien, tk.TenKhachHang, tk.MaNV
FROM ThongKe tk
WHERE YEAR(tk.NgayDat) = @Nam";  // Chỉ lọc theo năm

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nam", nam); // Truyền tham số năm vào câu truy vấn

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Đặt đường dẫn tới file RDLC
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\ReportTKNam.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("TKNam", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Tính tổng doanh thu năm
                decimal tongDoanhThuNam = dt.AsEnumerable().Sum(row => row.Field<decimal>("TongSoTien"));

                // Gửi tổng doanh thu vào report
                ReportParameter rp = new ReportParameter("TongDoanhThu", tongDoanhThuNam.ToString());
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                reportViewer1.RefreshReport();
            }
        }
    }
}
