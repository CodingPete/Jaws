using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Dashboard.Controllers
{
    public class MarktController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Markt
        public ActionResult Index()
        {
            return View(db.MarktSet.ToList());
        }

        // GET: Markt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markt markt = db.MarktSet.Find(id);
            if (markt == null)
            {
                return HttpNotFound();
            }
            return View(markt);
        }

        // GET: Markt/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Markt/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Wert")] Markt markt)
        {
            if (ModelState.IsValid)
            {
                db.MarktSet.Add(markt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markt);
        }

        // GET: Markt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markt markt = db.MarktSet.Find(id);
            if (markt == null)
            {
                return HttpNotFound();
            }
            return View(markt);
        }

        // POST: Markt/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Wert")] Markt markt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markt);
        }

        // GET: Markt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markt markt = db.MarktSet.Find(id);
            if (markt == null)
            {
                return HttpNotFound();
            }
            return View(markt);
        }

        // POST: Markt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Markt markt = db.MarktSet.Find(id);
            db.MarktSet.Remove(markt);
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
