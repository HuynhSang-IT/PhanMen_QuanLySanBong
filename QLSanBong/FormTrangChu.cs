using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnQLSanBong
{
    public partial class FormTrangChu: Form
    {
        private TaiKhoan taiKhoan;

        // Constructor nhận đối tượng TaiKhoan
        public FormTrangChu(TaiKhoan taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
            //PhanQuyen();
        }
        /*private void PhanQuyen()
        {
            if (taiKhoan.Quyen == "admin")
            {
                // Hiển thị các nút hoặc chức năng dành cho admin
                btnNhanVien.Visible = true; // Ví dụ: cho phép quản lý nhân viên
                btnQuanLySan.Visible = true; // Ví dụ: cho phép quản lý sân
                btnHeThong.Visible = true;
                btnDatSan.Visible = true;
                btnThongKe.Visible = true;
                btnThongTinKhachHang.Visible = true;
            }
            else if (taiKhoan.Quyen == "user")
            {
                // Hiển Thị các nút hoặc chức năng không dành cho user
                btnDatSan.Visible = false;
                btnThongTinKhachHang.Visible = true;
            }
        }*/

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất?", "Thông báo!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Hide();
                FormDangNhap dangNhap = new FormDangNhap();
                dangNhap.ShowDialog();
            }
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            btnHeThong_Click();
        }
        private void btnHeThong_Click()
        {
            //Kiểm tra nếu form đã mở
            foreach (Form form in panel_Body.Controls)
            {
                if (form.GetType() == typeof(FormHeThong))
                {
                    form.BringToFront();
                    return;
                }
            }
            //Tạo và thêm form con vào panel 
            FormHeThong formheThong = new FormHeThong();
            formheThong.TopLevel = false; //không phải là top-level form
            //formheThong.FormBorderStyle = FormBorderStyle.None;//ẩn viền của form
            formheThong.Dock = DockStyle.Fill; //Định dạng cho fit vào panel

            panel_Body.Controls.Add(formheThong);
            panel_Body.Tag = formheThong;

            formheThong.Show(); // Hiển thị Form con.
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            btnDatSan_Click();
        }
        private void btnDatSan_Click()
        {
            //Kiểm tra nếu form đã mở
            foreach (Form form in panel_Body.Controls)
            {
                if (form.GetType() == typeof(FormDatSan))
                {
                    form.BringToFront();
                    return;
                }
            }
            FormDatSan formdatsan = new FormDatSan();
            formdatsan.TopLevel = false;
            formdatsan.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(formdatsan);
            panel_Body.Tag = formdatsan;

            formdatsan.Show(); // Hiển thị Form con.
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            btnQuanLyNhanVien_Click();
        }
        private void btnQuanLyNhanVien_Click()
        {
            foreach (Form form in panel_Body.Controls)
            {
                if (form.GetType() == typeof(FormQuanLyNhanVien))
                {
                    form.BringToFront();
                    return;
                }
            }
            FormQuanLyNhanVien formqlnv = new FormQuanLyNhanVien();
            formqlnv.TopLevel = false;
            formqlnv.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(formqlnv);
            panel_Body.Tag = formqlnv;
            formqlnv.Show();
        }

        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            lblXinChao.Text = "Xin chào, " + taiKhoan.TenTaiKhoan; // Hiển thị tên người dùng

            FormQuangCao pn = new FormQuangCao();
            pn.TopLevel = false; // Đặt thành false để hiển thị trong panel
            pn.FormBorderStyle = FormBorderStyle.None; // Xóa đường viền của form
            pn.Dock = DockStyle.Fill; // Để form trẻ chiếm toàn bộ panel

            panel_Body.Controls.Clear(); // Xóa các điều khiển hiện tại trong panel (nếu có)
            panel_Body.Controls.Add(pn); // Thêm form con vào panel

            pn.Show(); // Hiển thị form con
        }

        private void btnThongTinKhachHang_Click(object sender, EventArgs e)
        {
            btnThongTinKhachHang_Click();
        }
        private void btnThongTinKhachHang_Click()
        {
            foreach (Form form in panel_Body.Controls)
            {
                if (form.GetType() == typeof(FormNhapTentaiKhoan))
                {
                    form.BringToFront();
                    return;
                }
            }
            FormNhapTentaiKhoan formntk = new FormNhapTentaiKhoan();
            formntk.TopLevel = false;
            formntk.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(formntk);
            panel_Body.Tag = formntk;
            formntk.Show();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            btnThongKe_Click();
        }
        private void btnThongKe_Click()
        {
            foreach (Form form in panel_Body.Controls)
            {
                if (form.GetType() == typeof(FormThongKe))
                {
                    form.BringToFront();
                    return;
                }
            }
            FormThongKe formtk = new FormThongKe();
            formtk.TopLevel = false;
            formtk.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(formtk);
            panel_Body.Tag = formtk;
            formtk.Show();
        }

        private void btnQuanLySan_Click(object sender, EventArgs e)
        {
            btnQuanLySan_Click();
        }
        private void btnQuanLySan_Click()
        {
            foreach (Form form in panel_Body.Controls)
            {
                if (form.GetType() == typeof(FormQuanLySan))
                {
                    form.BringToFront();
                    return;
                }
            }
            FormQuanLySan formqls = new FormQuanLySan();
            formqls.TopLevel = false;
            formqls.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(formqls);
            panel_Body.Tag = formqls;
            formqls.Show();
        }

        private void panel_Body_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
