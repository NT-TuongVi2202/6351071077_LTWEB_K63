using motor_cycle1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace motor_cycle1.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        QLBANXEMAYEntities db = new QLBANXEMAYEntities();
       
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if(lstGiohang == null)
            {
                // nếu giỏ hàng chưa tồn tại thì khởi tạo listGiohang
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;

            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int iMaXe ,string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.iMaXe == iMaXe);
            if(sanpham == null )
            {
                sanpham = new GioHang(iMaXe);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        // cập nhật phương thức tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        // cập nhật phương thức tính tổng tiền
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        // cập nhật phương thức xử lý hiển thị giỏ hàng 
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = Laygiohang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Moto_cycle");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
      
    }
}