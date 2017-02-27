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
    public class BelegController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Beleg
        public ActionResult Index()
        {
            var belegSet = db.BelegSet.Include(b => b.Lieferart);
            return View(belegSet.ToList());
        }

        // GET: Beleg/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beleg beleg = db.BelegSet.Find(id);
            if (beleg == null)
            {
                return HttpNotFound();
            }
            return View(beleg);
        }

        // GET: Beleg/Create
        public ActionResult Create()
        {
            ViewBag.LieferartId = new SelectList(db.LieferartSet, "Id", "Name");
            return View();
        }

        // POST: Beleg/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LieferartId,Datum")] Beleg beleg)
        {
            if (ModelState.IsValid)
            {
                db.BelegSet.Add(beleg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LieferartId = new SelectList(db.LieferartSet, "Id", "Name", beleg.LieferartId);
            return View(beleg);
        }

        // GET: Beleg/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beleg beleg = db.BelegSet.Find(id);
            if (beleg == null)
            {
                return HttpNotFound();
            }
            ViewBag.LieferartId = new SelectList(db.LieferartSet, "Id", "Name", beleg.LieferartId);
            return View(beleg);
        }

        // POST: Beleg/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LieferartId,Datum")] Beleg beleg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beleg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LieferartId = new SelectList(db.LieferartSet, "Id", "Name", beleg.LieferartId);
            return View(beleg);
        }

        // GET: Beleg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beleg beleg = db.BelegSet.Find(id);
            if (beleg == null)
            {
                return HttpNotFound();
            }
            return View(beleg);
        }

        // POST: Beleg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beleg beleg = db.BelegSet.Find(id);
            db.BelegSet.Remove(beleg);
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
