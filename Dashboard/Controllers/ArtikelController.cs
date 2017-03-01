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


            DateTime heuteVor6Wochen = DateTime.Today.AddDays(- (7 * 6)).Date;
            DateTime heuteIn1Wochen = DateTime.Today.AddDays(7 * 6).Date;

            List<Helper_Artikel> ha = new List<Helper_Artikel>();

            foreach (Artikel artikel in artikelSet)
            {
                Helper_Artikel helper = new Helper_Artikel();
                helper.artikel = artikel;
                helper.artikelbeleg = (from ab in db.ArtikelBelegSet
                                       join b in db.BelegSet
                                       on ab.BelegId equals b.Id
                                       join lfa in db.LieferartSet 
                                       on b.LieferartId equals lfa.Id
                                       where b.Datum >= heuteVor6Wochen && b.Datum <= heuteIn1Wochen
                                       && lfa.Name == "Verkauf"
                                       select ab).ToList();
                helper.prognosen = (from p in db.PrognoseSet
                                    where p.Datum >= heuteVor6Wochen && p.Datum <= heuteIn1Wochen
                                    select p).ToList();
                ha.Add(helper);
            }

            ViewBag.helper = ha;
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
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
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }
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
