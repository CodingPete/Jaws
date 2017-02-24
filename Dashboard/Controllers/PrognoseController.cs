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
    public class PrognoseController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Prognose
        public ActionResult Index()
        {
            var prognoseSet = db.PrognoseSet.Include(p => p.Artikel);
            return View(prognoseSet.ToList());
        }

        // GET: Prognose/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prognose prognose = db.PrognoseSet.Find(id);
            if (prognose == null)
            {
                return HttpNotFound();
            }
            return View(prognose);
        }

        // GET: Prognose/Create
        public ActionResult Create()
        {
            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name");
            return View();
        }

        // POST: Prognose/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Datum,Abverkauf_soll,Abverkauf_ist,ArtikelId")] Prognose prognose)
        {
            if (ModelState.IsValid)
            {
                db.PrognoseSet.Add(prognose);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name", prognose.ArtikelId);
            return View(prognose);
        }

        // GET: Prognose/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prognose prognose = db.PrognoseSet.Find(id);
            if (prognose == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name", prognose.ArtikelId);
            return View(prognose);
        }

        // POST: Prognose/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Datum,Abverkauf_soll,Abverkauf_ist,ArtikelId")] Prognose prognose)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prognose).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name", prognose.ArtikelId);
            return View(prognose);
        }

        // GET: Prognose/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prognose prognose = db.PrognoseSet.Find(id);
            if (prognose == null)
            {
                return HttpNotFound();
            }
            return View(prognose);
        }

        // POST: Prognose/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prognose prognose = db.PrognoseSet.Find(id);
            db.PrognoseSet.Remove(prognose);
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
