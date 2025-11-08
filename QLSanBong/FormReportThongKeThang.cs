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
    public partial class FormReportThongKeThang : Form
    {
        private int thang;
        private int nam;

        private string connectionString = "Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True";


        // Constructor mới nhận tháng và năm
        public FormReportThongKeThang(int thang, int nam)
        {
            InitializeComponent();
            this.thang = thang;
            this.nam = nam;
        }

        private void FormReportThongKe_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            LoadBaoCao();
        }
        private void LoadBaoCao()
        {
            /*string query = @"
    SELECT tk.STT, 
           tk.MaSan, 
           tk.NgayDat, 
           tk.TongSoTien, 
           tk.TenKhachHang, 
           ISNULL(nv.HoTenNV, 'Không có dữ liệu') AS NhanVienQuanLy
    FROM ThongKe tk
    LEFT JOIN NhanVien nv ON tk.MaNV = nv.MaNV
    WHERE MONTH(tk.NgayDat) = @Thang 
    AND YEAR(tk.NgayDat) = @Nam";*/
            string query = @"
SELECT tk.STT, tk.MaSan, tk.NgayDat, tk.TongSoTien, tk.TenKhachHang, tk.MaNV
FROM ThongKe tk
WHERE MONTH(tk.NgayDat) = @Thang 
AND YEAR(tk.NgayDat) = @Nam";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Đặt đường dẫn tới file RDLC
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\ReportThongKe.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet_TK", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Tính tổng doanh thu tháng
                decimal tongDoanhThuThang = dt.AsEnumerable().Sum(row => row.Field<decimal>("TongSoTien"));

                // Gửi tổng doanh thu vào report
                ReportParameter rp = new ReportParameter("TongDoanhThu", tongDoanhThuThang.ToString("N0"));
                reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });

                reportViewer1.RefreshReport();
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
