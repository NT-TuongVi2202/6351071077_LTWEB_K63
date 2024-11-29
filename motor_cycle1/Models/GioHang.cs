using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace motor_cycle1.Models
{
    public class GioHang
    {
        QLBANXEMAYEntities data = new QLBANXEMAYEntities();
        public int iMaXe { get; set; }
        public string sTenXe { get; set; }
        public string sAnhbia { get; set; }
        public double dDongia { get; set; }
        public int iSoluong { get; set; }
        public double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        // khởi tạo giỏ hàng 
        public GioHang(int MaXe)
        {
            iMaXe = MaXe;
            XEGANMAY Xe = data.XEGANMAYs.Single(n => n.MaXe == MaXe);
            sTenXe = Xe.TenXe;
            sAnhbia = Xe.Anhbia;
            dDongia = double.Parse(Xe.Giaban.ToString());
            iSoluong = 1;
        }

    }
}
