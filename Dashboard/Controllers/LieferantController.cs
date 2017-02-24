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
    public class LieferantController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Lieferant
        public ActionResult Index()
        {
            return View(db.LieferantSet.ToList());
        }

        // GET: Lieferant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferant lieferant = db.LieferantSet.Find(id);
            if (lieferant == null)
            {
                return HttpNotFound();
            }
            return View(lieferant);
        }

        // GET: Lieferant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lieferant/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Straße,Hausnummer,Zusatz,Postleitzahl,Ort,Telefon,Mobil,Email,IBAN,BIC")] Lieferant lieferant)
        {
            if (ModelState.IsValid)
            {
                db.LieferantSet.Add(lieferant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lieferant);
        }

        // GET: Lieferant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferant lieferant = db.LieferantSet.Find(id);
            if (lieferant == null)
            {
                return HttpNotFound();
            }
            return View(lieferant);
        }

        // POST: Lieferant/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Straße,Hausnummer,Zusatz,Postleitzahl,Ort,Telefon,Mobil,Email,IBAN,BIC")] Lieferant lieferant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lieferant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lieferant);
        }

        // GET: Lieferant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferant lieferant = db.LieferantSet.Find(id);
            if (lieferant == null)
            {
                return HttpNotFound();
            }
            return View(lieferant);
        }

        // POST: Lieferant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lieferant lieferant = db.LieferantSet.Find(id);
            db.LieferantSet.Remove(lieferant);
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
