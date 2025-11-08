using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQLSanBong
{
    public partial class FormInHoaDon: Form
    {
        private string maSan, tenKhachHang, ngayDat, gioBatDau, gioKetThuc, loaiSan, loaiThue, donGia, buoi, tongSoTien;

        private void FormInHoaDon_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                Document = printDocument
            };

            printPreviewDialog.ShowDialog();
        }

        public FormInHoaDon(string maSan, string tenKhachHang, string ngayDat, string gioBatDau,
                            string gioKetThuc, string loaiSan, string loaiThue, string donGia,
                            string buoi, string tongSoTien)
        {
            InitializeComponent();

            this.maSan = maSan;
            this.tenKhachHang = tenKhachHang;
            this.ngayDat = ngayDat;
            this.gioBatDau = gioBatDau;
            this.gioKetThuc = gioKetThuc;
            this.loaiSan = loaiSan;
            this.loaiThue = loaiThue;
            this.donGia = donGia;
            this.buoi = buoi;
            this.tongSoTien = tongSoTien;
        }
             private void FormInHoaDon_Load(object sender, EventArgs e)
        {
            lblMaSan.Text = maSan;
            lblTenKhachHang.Text = tenKhachHang;
            lblNgayDat.Text = ngayDat;
            lblGioBatDau.Text = gioBatDau;
            lblGioKetThuc.Text = gioKetThuc;
            lblLoaiSan.Text = loaiSan;
            lblLoaiThue.Text = loaiThue;
            lblDonGia.Text = donGia;
            lblBuoi.Text = buoi;
            lblTongSoTien.Text = tongSoTien;
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 12);
            float y = 100;

            g.DrawString("HÓA ĐƠN THANH TOÁN", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new PointF(250, y));
            y += 40;
            g.DrawString($"Mã Sân: {maSan}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Khách Hàng: {tenKhachHang}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Ngày Đặt: {ngayDat}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Giờ Bắt Đầu: {gioBatDau}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Giờ Kết Thúc: {gioKetThuc}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Loại Sân: {loaiSan}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Loại Thuê: {loaiThue}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Đơn Giá: {donGia} VNĐ", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Buổi: {buoi}", font, Brushes.Black, new PointF(100, y));
            y += 30;
            g.DrawString($"Tổng Số Tiền: {tongSoTien} VNĐ", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new PointF(100, y));
            y += 50;
            g.DrawString("Cảm ơn quý khách!", font, Brushes.Black, new PointF(250, y));
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
