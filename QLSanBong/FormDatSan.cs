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
    public partial class FormDatSan: Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True");
        public FormDatSan()
        {
            InitializeComponent();
            LoadData();
            CapNhatDataGridView();
        }
        private void LoadData()
        {
            //Lấy dữ liệu từ cơ sỡ dữ liệu
            string query = "Select * From DatSan";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            dgvDatSan.DataSource = data;
        }
        private void CapNhatDataGridView()
        {
            string query = "SELECT * FROM DatSan";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            dgvDatSan.DataSource = data;
        }

        private void FormDatSan_Load(object sender, EventArgs e)
        {
            //thêm dữ liệu Mã sân
           
            cbxMaSan.Items.Add("Sân 1");
            cbxMaSan.Items.Add("Sân 2");
            cbxMaSan.Items.Add("Sân 3");
            cbxMaSan.Items.Add("Sân 4");
            cbxMaSan.Items.Add("Sân 5");
            cbxMaSan.Items.Add("Sân 6");

            //thêm dữ liệu Loại sân
            cbxLoaiSan.Items.Add("Sân 5 Người");
            cbxLoaiSan.Items.Add("Sân 7 Người");
        
            //Thêm dữ liệu Loại thuê
            cbxLoaiThue.Items.Add("Trực tiếp");
            cbxLoaiThue.Items.Add("Đặt Trước");
            //thêm dữ liệu Buổi
            cbxBuoi.Items.Add("Sáng");
            cbxBuoi.Items.Add("Trưa");
            cbxBuoi.Items.Add("Chiều");
            cbxBuoi.Items.Add("Tối");
            //Thêm dữ liệu Đơn giá
            cbxDonGia.Items.Add("300.000VNĐ");
            cbxDonGia.Items.Add("500.000VNĐ");
        }

        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex là -1 cho các ô tiêu đề
            {
                //Truy xuất dữ liệu từ ô đã nhấp
                DataGridViewRow selectedRow = dgvDatSan.Rows[e.RowIndex];

                //Giả sử các cột trong DataGridView được lập chỉ mục chính xác
                string maSan = selectedRow.Cells["MaSan"].Value.ToString();
                string tenKhachHang = selectedRow.Cells["TenKhachHang"].Value.ToString();
                string ngaydat = selectedRow.Cells["NgayDat"].Value.ToString();
                string gioBatDau = selectedRow.Cells["GioBatDau"].Value.ToString();
                string gioKetThuc = selectedRow.Cells["GioKetThuc"].Value.ToString();
                string loaisan = selectedRow.Cells["LoaiSan"].Value.ToString();
                string loaiThue = selectedRow.Cells["LoaiThue"].Value.ToString();
                string donGia = selectedRow.Cells["DonGia"].Value.ToString();
                string buoi = selectedRow.Cells["Buoi"].Value.ToString();

                //Hiển thị dữ liệu trong các điều khiển có liên quan
                cbxMaSan.Text = maSan;
                txtTenKhachHang.Text = tenKhachHang;
                dtpNgayDat.Text = ngaydat;
                dtpGioBatDau.Text = gioBatDau;
                dtpGioKetThuc.Text = gioKetThuc;
                cbxLoaiSan.Text = loaisan;
                cbxLoaiThue.Text = loaiThue;
                cbxDonGia.Text = donGia;
                cbxBuoi.Text = buoi;

                // Tùy chọn, bạn có thể muốn xử lý bất kỳ chức năng bổ sung nào ở đây chẳng hạn như bật hoặc tắt các nút dựa trên lựa chọn.
            }
        }

       

        private void btnDatSAn_Click(object sender, EventArgs e)
        {

            // Thêm dữ liệu vào DataGridView
            if (string.IsNullOrWhiteSpace(cbxMaSan.Text) ||
                string.IsNullOrWhiteSpace(txtTenKhachHang.Text) ||
                string.IsNullOrWhiteSpace(cbxLoaiThue.Text) ||
                string.IsNullOrWhiteSpace(cbxDonGia.Text) ||
                string.IsNullOrWhiteSpace(cbxBuoi.Text) ||
                string.IsNullOrWhiteSpace(cbxLoaiSan.Text) ||
                dtpGioBatDau.Value >= dtpGioKetThuc.Value)
            {
                MessageBox.Show("Vui lòng điền chính xác tất cả các thông tin và đảm bảo thời gian bắt đầu là trước thời gian kết thúc.");
                return;
            }
            // Kiểm tra điều kiện loại sân và đơn giá
            if ((cbxLoaiSan.Text == "Sân 5 Người" && cbxDonGia.Text != "300.000VNĐ") ||
                (cbxLoaiSan.Text == "Sân 7 Người" && cbxDonGia.Text != "500.000VNĐ"))
            {
                MessageBox.Show("Đơn giá không hợp lệ với loại sân đã chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tính số phút
            double soPhut = (dtpGioKetThuc.Value - dtpGioBatDau.Value).TotalMinutes;

            // Xác định giá dựa trên loại sân
            double donGia;
            if (cbxLoaiSan.Text.Contains("Sân 5 Người")) // Giả sử tên sân có chứa "5 người"
            {
                donGia = 300.000; // 300.000 VNĐ cho sân 5 người
            }
            else if (cbxLoaiSan.Text.Contains("Sân 7 Người")) // Giả sử tên sân có chứa "7 người"
            {
                donGia = 500.000; // 500.000 VNĐ cho sân 7 người
            }
            else
            {
                MessageBox.Show("Loại sân không hợp lệ.");
                return;
            }
            // Tính tổng số tiền
            double tongSoTien = (soPhut / 60) * donGia; // Tính tiền theo từng phút


            sqlConnection.Open();
            try
            {
               
                {
                    // Kiểm tra các khoảng thời gian chồng chéo cho cùng một trường
                    string checkQuery = @"SELECT COUNT(*) FROM DatSan 
                      WHERE MaSan = @MaSan 
                      AND LoaiSan = @LoaiSan 
                      AND NOT (
                          @GioBatDau >= GioKetThuc 
                          OR @GioKetThuc <= GioBatDau
                      )";


                    SqlCommand checkCmd = new SqlCommand(checkQuery, sqlConnection);
                    checkCmd.Parameters.AddWithValue("@MaSan", cbxMaSan.Text);
                    checkCmd.Parameters.AddWithValue("@LoaiSan", cbxLoaiSan.SelectedItem.ToString());
                    checkCmd.Parameters.AddWithValue("@GioBatDau", dtpGioBatDau.Value.TimeOfDay);
                    checkCmd.Parameters.AddWithValue("@GioKetThuc", dtpGioKetThuc.Value.TimeOfDay);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Khung giờ đã được đặt. Vui lòng chọn khung giờ khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Nhận Stt có sẵn tiếp theo
                string maxSttQuery = "SELECT ISNULL(MAX(STT), 0) FROM DatSan";
                SqlCommand maxSttCmd = new SqlCommand(maxSttQuery, sqlConnection);
                int maxStt = (int)maxSttCmd.ExecuteScalar() + 1;

                // Chuẩn bị truy vấn chèn
                string insertQuery = "INSERT INTO DatSan ( MaSan, TenKhachHang, NgayDat, GioBatDau, GioKetThuc, LoaiSan, LoaiThue, DonGia, Buoi, TongSoTien) " +
                                     "VALUES ( @MaSan, @TenKhachHang, @NgayDat, @GioBatDau, @GioKetThuc, @LoaiSan, @LoaiThue, @DonGia, @Buoi, @TongSoTien)";
                SqlCommand cmd = new SqlCommand(insertQuery, sqlConnection);
                cmd.Parameters.AddWithValue("@MaSan", cbxMaSan.Text);
                cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKhachHang.Text);
                cmd.Parameters.AddWithValue("@NgayDat", dtpNgayDat.Value);
                cmd.Parameters.AddWithValue("@GioBatDau", dtpGioBatDau.Value);
                cmd.Parameters.AddWithValue("@GioKetThuc", dtpGioKetThuc.Value);
                cmd.Parameters.AddWithValue("@LoaiSan", cbxLoaiSan.Text);
                cmd.Parameters.AddWithValue("@LoaiThue", cbxLoaiThue.Text);
                cmd.Parameters.AddWithValue("@DonGia", cbxDonGia.Text);
                cmd.Parameters.AddWithValue("@Buoi", cbxBuoi.Text);
                cmd.Parameters.AddWithValue("@TongSoTien", tongSoTien);
                cmd.ExecuteNonQuery();
                LoadData(); // Làm mới DataGridView
                MessageBox.Show("Đặt Sân Thành Công!!", "Thông Báo!!", MessageBoxButtons.OK);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Vui Lòng Điền Thông Tin của Khách hàng trước: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnHuySan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có một hàng được chọn trong DataGridView hay không
            if (dgvDatSan.CurrentRow != null)
            {
                int sttToCancel = (int)dgvDatSan.CurrentRow.Cells["STT"].Value; // Giả sử 'STT' là cột chính xác

                // Chuẩn bị truy vấn xóa
                //string deleteQuery = "DELETE FROM DatSan WHERE STT = @STT";

                try
                {
                    sqlConnection.Open(); // Mở kết nối

                    // 1️⃣ Cập nhật trạng thái trong bảng QuanLySan
                    string updateQuery = "UPDATE QuanLySan SET TrangThai = N'Đã hủy' WHERE STT = @STT";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, sqlConnection);
                    updateCmd.Parameters.AddWithValue("@STT", sttToCancel);
                    updateCmd.ExecuteNonQuery();

                    // 2️⃣ Không xóa mà chỉ thông báo đã hủy
                    MessageBox.Show("Sân đã được hủy, trạng thái cập nhật thành 'Đã hủy'.Vui Lòng hãy chọn xóa để bỏ sân", "Thông báo");

                    // 3️⃣ Load lại dữ liệu
                    LoadData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi");
                }
                finally
                {
                    sqlConnection.Close(); // Đảm bảo kết nối luôn đóng
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đặt chỗ để hủy.", "Thông báo");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có một hàng được chọn không
            if (dgvDatSan.CurrentRow != null)
            {
                int sttToDelete = (int)dgvDatSan.CurrentRow.Cells["STT"].Value; // Lấy STT của dòng được chọn

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đặt sân này?",
                                                      "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        sqlConnection.Open(); // Mở kết nối

                        // Xóa dữ liệu trong bảng DatSan (KHÔNG xóa trong QuanLySan)
                        string deleteQuery = "DELETE FROM DatSan WHERE STT = @STT";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, sqlConnection);
                        deleteCmd.Parameters.AddWithValue("@STT", sttToDelete);

                        int rowsAffected = deleteCmd.ExecuteNonQuery(); // Thực hiện lệnh xóa

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa đặt sân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy đặt sân để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        LoadData(); // Làm mới DataGridView
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sqlConnection.Close(); // Đảm bảo kết nối được đóng
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvDatSan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thông tin của mình để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            else
            {
                // Lấy dữ liệu từ dòng đã chọn
                string maSan = dgvDatSan.CurrentRow.Cells["MaSan"].Value.ToString();
                string tenKhachHang = dgvDatSan.CurrentRow.Cells["TenKhachHang"].Value.ToString();
                string ngayDat = dgvDatSan.CurrentRow.Cells["NgayDat"].Value.ToString();
                string gioBatDau = dgvDatSan.CurrentRow.Cells["GioBatDau"].Value.ToString();
                string gioKetThuc = dgvDatSan.CurrentRow.Cells["GioKetThuc"].Value.ToString();
                string loaiSan = dgvDatSan.CurrentRow.Cells["LoaiSan"].Value.ToString();
                string loaiThue = dgvDatSan.CurrentRow.Cells["LoaiThue"].Value.ToString();
                string donGia = dgvDatSan.CurrentRow.Cells["DonGia"].Value.ToString();
                string buoi = dgvDatSan.CurrentRow.Cells["Buoi"].Value.ToString();
                string tongSoTien = dgvDatSan.CurrentRow.Cells["TongSoTien"].Value.ToString();

                // Mở form thanh toán và truyền dữ liệu sang
                FormThanhToan formThanhToan = new FormThanhToan(maSan, tenKhachHang, ngayDat, gioBatDau, gioKetThuc, loaiSan, loaiThue, donGia, buoi, tongSoTien);
                formThanhToan.ShowDialog();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có một hàng được chọn trong DataGridView hay không
            if (dgvDatSan.CurrentRow != null)
            {
                // Lấy STT của dòng được chọn
                int sttToUpdate = (int)dgvDatSan.CurrentRow.Cells["STT"].Value;

                // Lấy dữ liệu từ các điều khiển
                string maSan = cbxMaSan.Text;
                string tenKhachHang = txtTenKhachHang.Text;
                DateTime ngayDat = dtpNgayDat.Value;
                DateTime gioBatDau = dtpGioBatDau.Value;
                DateTime gioKetThuc = dtpGioKetThuc.Value;
                string loaiSan = cbxLoaiSan.Text;
                string loaiThue = cbxLoaiThue.Text;
                string donGia = cbxDonGia.Text;
                string buoi = cbxBuoi.Text;

                // Kiểm tra điều kiện trước khi cập nhật
                if (string.IsNullOrWhiteSpace(maSan) ||
                    string.IsNullOrWhiteSpace(tenKhachHang) ||
                    string.IsNullOrWhiteSpace(loaiThue) ||
                    string.IsNullOrWhiteSpace(donGia) ||
                    string.IsNullOrWhiteSpace(buoi) ||
                    string.IsNullOrWhiteSpace(loaiSan) ||
                    gioBatDau >= gioKetThuc)
                {
                    MessageBox.Show("Vui lòng điền chính xác tất cả các thông tin và đảm bảo thời gian bắt đầu là trước thời gian kết thúc.");
                    return;
                }
                // Kiểm tra điều kiện loại sân và đơn giá
                if ((cbxLoaiSan.Text == "Sân 5 Người" && cbxDonGia.Text != "300.000VNĐ") ||
                    (cbxLoaiSan.Text == "Sân 7 Người" && cbxDonGia.Text != "500.000VNĐ"))
                {
                    MessageBox.Show("Đơn giá không hợp lệ với loại sân đã chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Tính tổng số tiền
                double tongSoTien = (gioKetThuc - gioBatDau).TotalHours * (loaiSan == "Sân 5 Người" ? 300.000 : 500.000);

                try
                {
                    sqlConnection.Open(); // Mở kết nối

                    // Chuẩn bị truy vấn cập nhật
                    string updateQuery = "UPDATE DatSan SET MaSan = @MaSan, TenKhachHang = @TenKhachHang, NgayDat = @NgayDat, " +
                                         "GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc, LoaiSan = @LoaiSan, " +
                                         "LoaiThue = @LoaiThue, DonGia = @DonGia, Buoi = @Buoi, TongSoTien = @TongSoTien " +
                                         "WHERE STT = @STT";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, sqlConnection);
                    updateCmd.Parameters.AddWithValue("@MaSan", maSan);
                    updateCmd.Parameters.AddWithValue("@TenKhachHang", tenKhachHang);
                    updateCmd.Parameters.AddWithValue("@NgayDat", ngayDat);
                    updateCmd.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                    updateCmd.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);
                    updateCmd.Parameters.AddWithValue("@LoaiSan", loaiSan);
                    updateCmd.Parameters.AddWithValue("@LoaiThue", loaiThue);
                    updateCmd.Parameters.AddWithValue("@DonGia", donGia);
                    updateCmd.Parameters.AddWithValue("@Buoi", buoi);
                    updateCmd.Parameters.AddWithValue("@TongSoTien", tongSoTien);
                    updateCmd.Parameters.AddWithValue("@STT", sttToUpdate);

                    int rowsAffected = updateCmd.ExecuteNonQuery(); // Thực hiện lệnh cập nhật

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin đặt sân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Làm mới DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlConnection.Close(); // Đảm bảo kết nối được đóng
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
