using motor_cycle1.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace motor_cycle1.Controllers
{
    public class NguoidungController : Controller
    {
        QLBANXEMAYEntities db = new QLBANXEMAYEntities();


        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            // Gán giá trị từ FormCollection
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = collection["Ngaysinh"];

            // Kiểm tra lỗi nhập liệu
            if (string.IsNullOrEmpty(hoten))
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            else if (string.IsNullOrEmpty(tendn))
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            else if (string.IsNullOrEmpty(matkhau))
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            else if (string.IsNullOrEmpty(matkhaunhaplai))
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            else if (matkhau != matkhaunhaplai)
                ViewData["Loi4"] = "Mật khẩu và mật khẩu nhập lại không khớp";
            else if (string.IsNullOrEmpty(email))
                ViewData["Loi5"] = "Email không được bỏ trống";
            else if (string.IsNullOrEmpty(dienthoai))
                ViewData["Loi6"] = "Phải nhập điện thoại";
            else
            {
                // Kiểm tra và gán ngày sinh
                if (DateTime.TryParse(ngaysinh, out DateTime parsedNgaySinh))
                {
                    kh.Ngaysinh = parsedNgaySinh;
                }
                else
                {
                    ViewData["Loi7"] = "Ngày sinh không hợp lệ";
                    return this.DangKy();
                }

                // Gán giá trị cho thực thể
                kh.HoTen = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;

                // Thêm vào DbSet và lưu
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();

                // Chuyển hướng đến trang đăng nhập
                return RedirectToAction("Dangnhap");
            }

            return this.DangKy();

        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];

            // Kiểm tra nếu tên đăng nhập hoặc mật khẩu trống
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập ";
                return View(); // Trả về View để hiển thị lỗi
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
                return View(); // Trả về View để hiển thị lỗi
            }
            else
            {
                // Tìm khách hàng trong cơ sở dữ liệu
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chính sau khi đăng nhập thành công
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View(); // Trả về View nếu đăng nhập thất bại
                }
            }
        }

    }
}
   

