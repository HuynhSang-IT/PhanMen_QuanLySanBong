using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQLSanBong
{
    public partial class FormThanhToan: Form
    {
        private string maSan; // Biến lưu mã sân để cập nhật
        public FormThanhToan(string maSan, string tenKhachHang, string ngayDat, string gioBatDau,
                         string gioKetThuc, string loaiSan, string loaiThue, string donGia,
                         string buoi, string tongSoTien)
        {
            InitializeComponent();

            // Gán giá trị cho biến maSan
            this.maSan = maSan;

            // Gán dữ liệu lên các điều khiển trong form thanh toán
            txtMaSan.Text = maSan;
            txtTenKhachHang.Text = tenKhachHang;
            txtNgaydat.Text = ngayDat;
            txtGioBatDau.Text = gioBatDau;
            txtGioKetThuc.Text = gioKetThuc;
            txtLoaiSan.Text = loaiSan;
            txtLoaiThue.Text = loaiThue;
            txtDonGia.Text = donGia;
            txtBuoi.Text = buoi;
            txtTongSoTien.Text = tongSoTien;
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cảm ơn bạn đã chọn Thanh Toán Trực tiếp", "Cảm Ơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Chuỗi kết nối đến SQL Server
            string connectionString = "Data Source=ASUSANG\\MSSQLSERVER04;Initial Catalog=QuanLySanBong;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE DatSan SET TrangThaiThanhToan = N'Đã thanh toán' WHERE MaSan = @MaSan";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSan", maSan);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Hiển thị thông báo
                MessageBox.Show("Thanh Toán Thành Công, Bạn có muốn in hóa đơn", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                // Mở Form In Hóa Đơn và truyền dữ liệu
                /*FormInHoaDon formInHoaDon = new FormInHoaDon(
                    txtMaSan.Text, txtTenKhachHang.Text, txtNgaydat.Text, txtGioBatDau.Text,
                    txtGioKetThuc.Text, txtLoaiSan.Text, txtLoaiThue.Text, txtDonGia.Text,
                    txtBuoi.Text, txtTongSoTien.Text);

                formInHoaDon.ShowDialog(); // Mở form in hóa đơn*/
                // Đóng form sau khi thanh toán
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
                {
                    Document = printDocument
                };

                printPreviewDialog.ShowDialog();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float pageWidth = e.Graphics.VisibleClipBounds.Width;
            float centerX = pageWidth / 2;
            float y = 100;

            // Font styles
            Font titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
            Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
            Font textFont = new Font("Segoe UI", 12);
            Font totalFont = new Font("Segoe UI", 14, FontStyle.Bold);

            // Căn giữa
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;

            // Kẻ viền hóa đơn
            Pen pen = new Pen(Color.Black, 2);
            g.DrawRectangle(pen, 50, 50, pageWidth - 100, 500); // Vẽ khung tổng thể

            // Tiêu đề hóa đơn
            g.DrawString(" HÓA ĐƠN THANH TOÁN ", titleFont, Brushes.Black, new PointF(centerX, y), centerFormat);
            y += 40;

            // Đường kẻ ngang
            g.DrawLine(pen, 70, y, pageWidth - 70, y);
            y += 20;

            // Thông tin khách hàng
            g.DrawString($"Mã Sân: {txtMaSan.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;
            g.DrawString($"Khách Hàng: {txtTenKhachHang.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;
            g.DrawString($"Ngày Đặt: {txtNgaydat.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;
            g.DrawString($"Giờ Bắt Đầu: {txtGioBatDau.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;
            g.DrawString($"Giờ Kết Thúc: {txtGioKetThuc.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;
            g.DrawString($"Loại Sân: {txtLoaiSan.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;
            g.DrawString($"Loại Thuê: {txtLoaiThue.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;
            g.DrawString($"Buổi: {txtBuoi.Text}", textFont, Brushes.Black, new PointF(100, y));
            y += 25;

            // Đường kẻ ngang
            y += 10;
            g.DrawLine(pen, 70, y, pageWidth - 70, y);
            y += 20;

            // Đơn giá
            g.DrawString($"Đơn Giá: {txtDonGia.Text} VNĐ", headerFont, Brushes.Black, new PointF(100, y));
            y += 30;

            // Tổng số tiền in đậm
            g.DrawString($"TỔNG CỘNG: {txtTongSoTien.Text} VNĐ", totalFont, Brushes.Red, new PointF(centerX, y), centerFormat);
            y += 50;

            // Lời cảm ơn
            g.DrawString(" CẢM ƠN QUÝ KHÁCH ", headerFont, Brushes.Black, new PointF(centerX, y), centerFormat);

        }
    }
}
