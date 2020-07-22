using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KT100720_59132942.Models;

namespace KT100720_59132942.Controllers
{
    public class TaiSan_59132942Controller : Controller
    {
        private KT100720_59132942Entities db = new KT100720_59132942Entities();

        // GET: TaiSan_59132942
        public ActionResult Index()
        {
            var taiSans = db.TaiSans.Include(t => t.LoaiT);
            return View(taiSans.ToList());
        }

        public ActionResult TimKiem(string MaTS = "", string TenTS = "", int giaMin = 0, int giaMax = int.MaxValue)
        {
            int id = 0;
            if (!MaTS.Equals("")) id = int.Parse(MaTS);

            var ts = db.TaiSans.Where(d => d.MaTS == id || (id == 0 && d.TenTS.Contains(TenTS)))
                               .Where(s => s.DonGia >= giaMin && s.DonGia <= giaMax);
            return View(ts);
        }

        // GET: TaiSan_59132942/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiSan taiSan = db.TaiSans.Find(id);
            if (taiSan == null)
            {
                return HttpNotFound();
            }
            return View(taiSan);
        }

        // GET: TaiSan_59132942/Create
        public ActionResult Create()
        {
            ViewBag.MaLTS = new SelectList(db.LoaiTS, "MaLTS", "TenLTS");
            return View();
        }

        // POST: TaiSan_59132942/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTS,TenTS,DVT,XuatXu,DonGia,AnhMH,GhiChu,MaLTS")] TaiSan taiSan)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgTS = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgTS.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgTS.SaveAs(path); //*************************************************

            if (ModelState.IsValid)
            {
                db.TaiSans.Add(taiSan);
                taiSan.AnhMH = postedFileName;//*************************************************
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLTS = new SelectList(db.LoaiTS, "MaLTS", "TenLTS", taiSan.MaLTS);
            return View(taiSan);
        }

        // GET: TaiSan_59132942/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiSan taiSan = db.TaiSans.Find(id);
            if (taiSan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLTS = new SelectList(db.LoaiTS, "MaLTS", "TenLTS", taiSan.MaLTS);
            return View(taiSan);
        }

        // POST: TaiSan_59132942/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTS,TenTS,DVT,XuatXu,DonGia,AnhMH,GhiChu,MaLTS")] TaiSan taiSan)
        {
            var imgTS = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgTS.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgTS.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(taiSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLTS = new SelectList(db.LoaiTS, "MaLTS", "TenLTS", taiSan.MaLTS);
            return View(taiSan);
        }

        // GET: TaiSan_59132942/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiSan taiSan = db.TaiSans.Find(id);
            if (taiSan == null)
            {
                return HttpNotFound();
            }
            return View(taiSan);
        }

        // POST: TaiSan_59132942/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiSan taiSan = db.TaiSans.Find(id);
            db.TaiSans.Remove(taiSan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
