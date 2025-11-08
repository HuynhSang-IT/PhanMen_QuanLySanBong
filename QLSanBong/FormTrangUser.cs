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
    public partial class FormTrangUser: Form
    {
        private TaiKhoan taiKhoan; // Lưu thông tin tài khoản
        public FormTrangUser(TaiKhoan taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan; // Gán giá trị tài khoản
        }

        private void FormTrangUser_Load(object sender, EventArgs e)
        {
            lblTaiKhoan.Text = "Xin chào, " + taiKhoan.TenTaiKhoan; // Hiển thị tên người dùng

            FormQuangCao pn = new FormQuangCao();
            pn.TopLevel = false; // Đặt thành false để hiển thị trong panel
            pn.FormBorderStyle = FormBorderStyle.None; // Xóa đường viền của form
            pn.Dock = DockStyle.Fill; // Để form trẻ chiếm toàn bộ panel

            panel_BODY.Controls.Clear(); // Xóa các điều khiển hiện tại trong panel (nếu có)
            panel_BODY.Controls.Add(pn); // Thêm form con vào panel

            pn.Show(); // Hiển thị form con
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xin Lỗi, Bạn Không Phải là Admin", "Thông báo!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            btnDatSan_Click();
        }
        private void btnDatSan_Click()
        {
            //Kiểm tra nếu form đã mở
            foreach (Form form in panel_BODY.Controls)
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

            panel_BODY.Controls.Add(formdatsan);
            panel_BODY.Tag = formdatsan;

            formdatsan.Show(); // Hiển thị Form con.
        }
        private void btnQuanLySan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xin Lỗi, Bạn Không Phải là Admin", "Thông báo!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xin Lỗi, Bạn Không Phải là Admin", "Thông báo!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xin Lỗi, Bạn Không Phải là Admin", "Thông báo!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnThongTinKhachHang_Click();
        }
        private void btnThongTinKhachHang_Click()
        {
            foreach (Form form in panel_BODY.Controls)
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

            panel_BODY.Controls.Add(formntk);
            panel_BODY.Tag = formntk;
            formntk.Show();
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn đăng xuất?", "Thông báo!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Hide();
                FormDangNhap dangNhap = new FormDangNhap();
                dangNhap.ShowDialog();
            }
        }
    }
}
