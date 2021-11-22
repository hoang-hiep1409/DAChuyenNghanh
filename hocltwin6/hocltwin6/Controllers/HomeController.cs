using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hocltwin6.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Models.ModelDonHang db = new Models.ModelDonHang();
        public ActionResult Index()
        {
            List<Models.hanghoa> dsHangHoa = db.hanghoas.ToList();
            return View(dsHangHoa);
        }

        [HttpPost]
        public ActionResult chonHangHoa(string mahang, int soluong)
        {
            Models.hanghoa hh = db.hanghoas.Find(mahang);
            if(hh != null)
            {
                Models.hoadon dh = Session["Donhang"] as Models.hoadon;             
                Models.chitiethoadon ct = new Models.chitiethoadon();
                ct.hanghoa = hh;
                ct.mahang = hh.mahang;
                ct.soluong = soluong;
                ct.dongia = hh.dongia;

                dh.chitiethoadons.Add(ct);

                return View("xemgiohang",dh);
            }
            return RedirectToAction("Index");
        }

        public ActionResult xemgiohang()
        {
            Models.hoadon dh = Session["Donhang"] as Models.hoadon;
            return View(dh);
        }

        [HttpPost]
        public ActionResult lapDonHang(Models.hoadon x)
        {
            Models.hoadon dh = Session["Donhang"] as Models.hoadon;
            if (ModelState.IsValid)
            {
                foreach(var a in dh.chitiethoadons)
                {
                    Models.chitiethoadon ct = new Models.chitiethoadon();
                    ct.sohd = x.sohd;
                    ct.mahang = a.mahang;
                    ct.dongia = a.dongia;
                    ct.soluong = a.soluong;
                    x.chitiethoadons.Add(ct);
                }
                db.hoadons.Add(x);
                db.SaveChanges();

                dh.chitiethoadons.Clear();
            }
            return RedirectToAction("Index");
        }
    }
}