using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnQLSanBong
{
    // Định nghĩa lớp TaiKhoan
   public class TaiKhoan
    {
        // Khai báo biến riêng tư để lưu trữ tên tài khoản
        private string tenTaiKhoan;
        // Khai báo biến riêng tư để lưu trữ mật khẩu
        private string matKhau;
        private string quyeN;
        // Constructor không tham số
        public TaiKhoan()
        {
            // Constructor này có thể được sử dụng để khởi tạo một đối tượng TaiKhoan mà không cần thông tin ban đầu
        }

        // Constructor có tham số để khởi tạo đối tượng với tên tài khoản và mật khẩu
        public TaiKhoan(string tentaikhoan, string matkhau, string quyen)
        {
            // Gán giá trị cho biến tenTaiKhoan từ tham số đầu vào
            this.tenTaiKhoan = tentaikhoan;
            // Gán giá trị cho biến matKhau từ tham số đầu vào
            this.matKhau = matkhau;
            // Gán giá trị cho quyền
            this.quyeN = quyen; 
        }
        // Thuộc tính TenTaiKhoan để truy cập và thiết lập giá trị cho biến tenTaiKhoan
        public string TenTaiKhoan
        { 
            get => tenTaiKhoan;  // Trả về giá trị của tenTaiKhoan
            set => tenTaiKhoan = value; // Gán giá trị cho tenTaiKhoan
        }
        // Thuộc tính MatKhau để truy cập và thiết lập giá trị cho biến matKhau
        public string MatKhau 
        { 
            get => matKhau; // Trả về giá trị của matKhau
            set => matKhau = value; // Gán giá trị cho matKhau
        }
        public string Quyen // Thuộc tính để truy cập quyền
        {
            get => quyeN;
            set => quyeN = value;
        }
    }
}
