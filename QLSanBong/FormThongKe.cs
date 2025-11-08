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
    public partial class FormThongKe: Form
    {
        private string connectionString = "Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True";
        public FormThongKe()
        {
            InitializeComponent();
        }
        private void LoadAllData()
        {
            string query = @"
        SELECT tk.STT, tk.MaSan, tk.NgayDat, tk.TongSoTien, tk.TenKhachHang, nv.HoTenNV AS NhanVienQuanLy
        FROM ThongKe tk
        JOIN NhanVien nv ON tk.MaNV = nv.MaNV";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Mở kết nối

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }


        private void FormThongKe_Load(object sender, EventArgs e)
        {
            LoadAllData(); // Load tất cả dữ liệu khi mở form

            cbxThang.Items.Add("1");
            cbxThang.Items.Add("2");
            cbxThang.Items.Add("3");
            cbxThang.Items.Add("4");
            cbxThang.Items.Add("5");
            cbxThang.Items.Add("6");
            cbxThang.Items.Add("7");
            cbxThang.Items.Add("8");
            cbxThang.Items.Add("9");
            cbxThang.Items.Add("10");
            cbxThang.Items.Add("11");
            cbxThang.Items.Add("12");

            cbxNam1.Items.Add("2024");
            cbxNam1.Items.Add("2025");
            cbxNam1.Items.Add("2026");

            cbxNam2.Items.Add("2024");
            cbxNam2.Items.Add("2025");
            cbxNam2.Items.Add("2026");
        }

        private void btnThongKeThang_Click(object sender, EventArgs e)
        {
            if (cbxThang.SelectedItem == null || cbxNam1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int thang = Convert.ToInt32(cbxThang.SelectedItem);
            int nam = Convert.ToInt32(cbxNam1.SelectedItem);

            LoadThongKe(thang, nam); // Lọc dữ liệu theo tháng và năm
        }

        private void btnThongKeNam_Click(object sender, EventArgs e)
        {
            if (cbxNam2.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int nam = Convert.ToInt32(cbxNam2.SelectedItem);

            LoadThongKeTheoNam(nam); // Lọc dữ liệu theo năm
        }
        //load theo năm
        private void LoadThongKeTheoNam(int nam)
        {
            string query = @"
        SELECT tk.STT, tk.MaSan, tk.NgayDat, tk.TongSoTien, tk.TenKhachHang, nv.HoTenNV AS NhanVienQuanLy
        FROM ThongKe tk
        JOIN NhanVien nv ON tk.MaNV = nv.MaNV
        WHERE YEAR(tk.NgayDat) = @Nam";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; // Hiển thị dữ liệu lọc theo năm
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        //load theo tháng
        private void LoadThongKe(int thang, int nam)
        {
            string query = @"
        SELECT tk.STT, tk.MaSan, tk.NgayDat, tk.TongSoTien, tk.TenKhachHang, nv.HoTenNV AS NhanVienQuanLy
        FROM ThongKe tk
        JOIN NhanVien nv ON tk.MaNV = nv.MaNV
        WHERE MONTH(tk.NgayDat) = @Thang AND YEAR(tk.NgayDat) = @Nam";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; // Hiển thị dữ liệu lọc theo tháng & năm
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnDTM_Click(object sender, EventArgs e)
        {
            if (cbxThang.SelectedItem != null && cbxNam1.SelectedItem != null)
            {
                if (int.TryParse(cbxThang.SelectedItem.ToString(), out int thang) &&
                    int.TryParse(cbxNam1.SelectedItem.ToString(), out int nam))
                {
                    TinhTongDoanhThuThang(thang, nam);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tháng và năm hợp lệ!");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn tháng hoặc năm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDTY_Click(object sender, EventArgs e)
        {
            if (cbxNam2.SelectedItem != null)
            {
                if (int.TryParse(cbxNam2.SelectedItem.ToString(), out int nam))
                {
                    TinhTongDoanhThuNam(nam);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn năm hợp lệ!");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn năm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //hàm tính tổng doanh thu tháng
        private void TinhTongDoanhThuThang(int thang, int nam)
        {
            string query = @"
        SELECT ISNULL(SUM(tk.TongSoTien), 0) 
        FROM ThongKe tk
        WHERE MONTH(tk.NgayDat) = @Thang AND YEAR(tk.NgayDat) = @Nam";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    object result = cmd.ExecuteScalar();
                    txtDoanhThuThang.Text = result.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tổng doanh thu tháng: " + ex.Message);
            }
        }
        //hàm tính tổng doanh thu cả năm
        private void TinhTongDoanhThuNam(int nam)
        {
            string query = @"
        SELECT ISNULL(SUM(tk.TongSoTien), 0) 
        FROM ThongKe tk
        WHERE YEAR(tk.NgayDat) = @Nam";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    object result = cmd.ExecuteScalar();
                    txtDoanhThuNam.Text = result.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tổng doanh thu năm: " + ex.Message);
            }
        }
        private void btnInThang_Click(object sender, EventArgs e)
        {
            if (cbxThang.SelectedItem == null || cbxNam1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int thang = Convert.ToInt32(cbxThang.SelectedItem);
            int nam = Convert.ToInt32(cbxNam1.SelectedItem);

            // Mở form báo cáo và truyền tháng, năm
            FormReportThongKeThang reportForm = new FormReportThongKeThang(thang, nam);
            reportForm.ShowDialog();
        }

        private void btnInNam_Click(object sender, EventArgs e)
        {
            if ( cbxNam2.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int nam = Convert.ToInt32(cbxNam2.SelectedItem);

            // Mở form báo cáo và truyền tháng, năm
            FormReportThongKeNam reportForm = new FormReportThongKeNam(nam);
            reportForm.ShowDialog();
        }
    }
}
