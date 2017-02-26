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
    public class ArtikelController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Artikel
        public ActionResult Index()
        {
            var artikelSet = db.ArtikelSet.Include(a => a.Lieferant).Include(a => a.Warengruppe);
            return View(artikelSet.ToList());
        }

        // GET: Artikel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikel artikel = db.ArtikelSet.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        // GET: Artikel/Create
        public ActionResult Create()
        {
            ViewBag.LieferantId = new SelectList(db.LieferantSet, "Id", "Name");
            ViewBag.WarengruppeId = new SelectList(db.WarengruppeSet, "Id", "Name");
            return View();
        }

        // POST: Artikel/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,GTIN,Bestand,Einheit,Nettoeinkaufspreis,Nettoverkaufspreis,LieferantId,WarengruppeId")] Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.ArtikelSet.Add(artikel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LieferantId = new SelectList(db.LieferantSet, "Id", "Name", artikel.LieferantId);
            ViewBag.WarengruppeId = new SelectList(db.WarengruppeSet, "Id", "Name", artikel.WarengruppeId);
            return View(artikel);
        }

        // GET: Artikel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikel artikel = db.ArtikelSet.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            ViewBag.LieferantId = new SelectList(db.LieferantSet, "Id", "Name", artikel.LieferantId);
            ViewBag.WarengruppeId = new SelectList(db.WarengruppeSet, "Id", "Name", artikel.WarengruppeId);
            return View(artikel);
        }

        // POST: Artikel/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GTIN,Bestand,Einheit,Nettoeinkaufspreis,Nettoverkaufspreis,LieferantId,WarengruppeId")] Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LieferantId = new SelectList(db.LieferantSet, "Id", "Name", artikel.LieferantId);
            ViewBag.WarengruppeId = new SelectList(db.WarengruppeSet, "Id", "Name", artikel.WarengruppeId);
            return View(artikel);
        }

        // GET: Artikel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artikel artikel = db.ArtikelSet.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        // POST: Artikel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artikel artikel = db.ArtikelSet.Find(id);
            db.ArtikelSet.Remove(artikel);
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
