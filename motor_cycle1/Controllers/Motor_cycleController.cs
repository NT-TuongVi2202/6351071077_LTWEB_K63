using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using motor_cycle1.Models;


namespace motor_cycle1.Controllers
{
   
    public class Motor_cycleController : Controller
    {
        QLBANXEMAYEntities data = new QLBANXEMAYEntities();
        // GET: Motor_cycle
        private List<XEGANMAY> layxemoi (int count)
        {
            return data.XEGANMAYs.OrderByDescending( a => a.Ngaycapnhat ).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var Xemoi = layxemoi(5);
            return View(Xemoi);
        }
        public ActionResult Loaixe()
        {
            var chude = from cd in data.LOAIXEs select cd;

            return PartialView(chude);
        }

        public ActionResult Nhaphanphoi()
        {
            var chude = from cd in data.NHAPHANPHOIs select cd;

            return PartialView(chude);
        }
        public ActionResult SPTheoLoaiXe(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var xe = from s in data.XEGANMAYs where s.MaLX == id select s;

            return PartialView(xe);
        }
       
        public ActionResult Detail(int id )
        {
          
            var xemay = from s in data.LOAIXEs where s.MaLX == id select s;
            if (xemay == null)
            {
                return HttpNotFound(); // Xử lý nếu xe không tồn tại
            }


            return View( xemay.Single());
        }
    }
}