using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoAnQLSanBong
{
    public partial class FormQuanLySan: Form
    {
        private string ChuoiKetNoi = "Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True";
        public FormQuanLySan()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData(string loaiSan = "", string tenKhachHang = "")
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            {
                conn.Open();
                string query = "SELECT STT, TenKhachHang, MaSan, " +
                               "CONVERT(VARCHAR(5), GioBatDau, 108) AS GioBatDau, " +
                               "CONVERT(VARCHAR(5), GioKetThuc, 108) AS GioKetThuc, " +
                               "NgayDat, TrangThai FROM QuanLySan WHERE 1=1";

                if (!string.IsNullOrEmpty(loaiSan))
                {
                    query += " AND MaSan LIKE @MaSanSan";
                }
                if (!string.IsNullOrEmpty(tenKhachHang))
                {
                    query += " AND TenKhachHang LIKE @TenKhachHang";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(loaiSan))
                    {
                        cmd.Parameters.AddWithValue("@MaSan", "%" + loaiSan + "%");
                    }
                    if (!string.IsNullOrEmpty(tenKhachHang))
                    {
                        cmd.Parameters.AddWithValue("@TenKhachHang", "%" + tenKhachHang + "%");
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtTenKhachHang.Text.Trim();
            LoadData("", tenKhachHang); // Gọi LoadData với tham số tìm kiếm
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormQuanLySan_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            {
                conn.Open();
                string query = "SELECT * FROM QuanLySan WHERE TrangThai = N'Đã Hủy'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void btnDaDat_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            {
                conn.Open();
                string query = "SELECT * FROM QuanLySan WHERE TrangThai = N'Đã Đặt'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
            {
                conn.Open();
                string query = "SELECT * FROM QuanLySan WHERE TrangThai = N'Vừa Cập Nhật'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            txtTenKhachHang.Text = ""; // Xóa nội dung ô tìm kiếm

            LoadData(); // Gọi lại hàm LoadData() để hiển thị toàn bộ dữ liệu
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sân này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int stt = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["STT"].Value);

                    using (SqlConnection conn = new SqlConnection(ChuoiKetNoi))
                    {
                        conn.Open();
                        string query = "DELETE FROM QuanLySan WHERE STT = @STT";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@STT", stt);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Cập nhật lại danh sách sân
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sân để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            FormReportQLSan form = new FormReportQLSan();
            form.Show();
        }
    }
}
