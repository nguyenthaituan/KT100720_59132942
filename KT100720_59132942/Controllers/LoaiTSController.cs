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
    public class LoaiTSController : Controller
    {
        private KT100720_59132942Entities db = new KT100720_59132942Entities();

        // GET: LoaiTS
        public ActionResult Index()
        {
            return View(db.LoaiTS.ToList());
        }

        // GET: LoaiTS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiT loaiT = db.LoaiTS.Find(id);
            if (loaiT == null)
            {
                return HttpNotFound();
            }
            return View(loaiT);
        }

        // GET: LoaiTS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLTS,TenLTS")] LoaiT loaiT)
        {
            if (ModelState.IsValid)
            {
                db.LoaiTS.Add(loaiT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiT);
        }

        // GET: LoaiTS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiT loaiT = db.LoaiTS.Find(id);
            if (loaiT == null)
            {
                return HttpNotFound();
            }
            return View(loaiT);
        }

        // POST: LoaiTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLTS,TenLTS")] LoaiT loaiT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiT);
        }

        // GET: LoaiTS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiT loaiT = db.LoaiTS.Find(id);
            if (loaiT == null)
            {
                return HttpNotFound();
            }
            return View(loaiT);
        }

        // POST: LoaiTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiT loaiT = db.LoaiTS.Find(id);
            db.LoaiTS.Remove(loaiT);
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
