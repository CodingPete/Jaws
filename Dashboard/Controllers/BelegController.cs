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
            
                List< Helper_BelegSumme > helper = new List<Helper_BelegSumme>();

            foreach (Beleg beleg in belegSet)
            {
                Helper_BelegSumme helpi = new Helper_BelegSumme();
                helpi.beleg = beleg;
                helpi.artikels = (from b in db.BelegSet
                                  join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                                  join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                                  where ab.BelegId == beleg.Id
                                  select a).ToList();
                helpi.summe = helpi.artikels.Select((a) => a.Nettoverkaufspreis).Sum();
                helper.Add(helpi);
            }
            ViewBag.helper = helper;
            
            return View(belegSet);
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

            List<ArtikelBeleg> artikelBeleg = db.ArtikelBelegSet.Where((ab) => ab.BelegId == beleg.Id).ToList();
            db.ArtikelBelegSet.RemoveRange(artikelBeleg);

            db.BelegSet.Remove(beleg);
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
