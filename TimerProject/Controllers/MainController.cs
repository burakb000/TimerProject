using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TimerProject.Models.Entity;
using System.Globalization;

namespace TimerProject.Controllers
{

    public class MainController : Controller
    {
        TIMERPROJECTDBEntities db = new TIMERPROJECTDBEntities();

        [HttpGet]
        public ActionResult Index()
        {
            //var values = db.TBLDATEDATAS.ToList();
            var values = db.TBLDATEDATAS.OrderByDescending(x => x.id).ToList();


            DateTime currentDate = DateTime.Now;
            string dateAsString = currentDate.ToString("dd-MM-yyyy");
            ViewBag.CurrentDate = dateAsString;

            DateTime today = DateTime.Now.Date;
            var formatDate = "dd-MM-yyyy"; // Veritabanındaki tarih formatı

            var sumDayMinute = db.TBLDATEDATAS
                .AsEnumerable()  // Bu noktada, verileri belleğe çekiyoruz.
                .Where(k => DateTime.TryParseExact(k.datestr, formatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                            && DateTime.ParseExact(k.datestr, formatDate, CultureInfo.InvariantCulture).Date == today)
                .Sum(k => k.summinute);

            if (sumDayMinute > 0)
            {
                int hour = (int)(sumDayMinute / 60);
                int minuteCalc = (int)(sumDayMinute - (hour * 60));

                ViewBag.GunlukSure = $"{hour} saat {minuteCalc} dakika";
            }
            else
            {
                ViewBag.GunlukSure = "Bugüne ait veri bulunmamaktadır.";
            }


            return View(values);
        }


        //[HttpPost]
        //public ActionResult Index(TBLDATEDATAS data)
        //{
        //    //DateTime currentDate = DateTime.Now;

        //    //string dateAsString = currentDate.ToString("dd-MM-yyyy");

        //    //data.summinute = sminute;   
        //    //data.date = Convert.ToDateTime(dateAsString);
        //    //data.datestr = dateAsString;
        //    //data.datetime = currentDate.ToString();
        //    //db.TBLDATEDATAS.Add(data);
        //    //db.SaveChanges();
        //    //return RedirectToAction("Index");
        //}

        [HttpGet]
        public ActionResult NewData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewData(int sminute)
        {
            TBLDATEDATAS data = new TBLDATEDATAS();
            DateTime currentDate = DateTime.Now;

            string dateAsString = currentDate.ToString("dd-MM-yyyy");
            data.summinute = sminute;
            data.date = Convert.ToDateTime(dateAsString);
            data.datestr = dateAsString;
            data.datetime = currentDate.ToString();
            data.datetimedt = currentDate;
            db.TBLDATEDATAS.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteData(int id)
        {
            var deger = db.TBLDATEDATAS.Find(id);
            db.TBLDATEDATAS.Remove(deger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateData(int id)
        {
            var deger = db.TBLDATEDATAS.Find(id);
            return View("UpdateData", deger);
        }
        [HttpPost]
        public ActionResult UpdateData(TBLDATEDATAS d)
        {
            var x = db.TBLDATEDATAS.Find(d.id);
            x.summinute = d.summinute;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}