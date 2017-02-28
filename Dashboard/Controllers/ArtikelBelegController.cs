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
    public class ArtikelBelegController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: ArtikelBeleg
        public ActionResult Index()
        {
            var artikelBelegSet = db.ArtikelBelegSet.Include(a => a.Artikel).Include(a => a.Beleg);
            return View(artikelBelegSet.ToList());
        }

        // GET: ArtikelBeleg/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelBeleg artikelBeleg = db.ArtikelBelegSet.Find(id);
            if (artikelBeleg == null)
            {
                return HttpNotFound();
            }
            return View(artikelBeleg);
        }

        // GET: ArtikelBeleg/Create
        public ActionResult Create()
        {
            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name");
            ViewBag.BelegId = new SelectList(db.BelegSet, "Id", "Id");
            return View();
        }

        // POST: ArtikelBeleg/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ArtikelId,BelegId")] ArtikelBeleg artikelBeleg)
        {
            if (ModelState.IsValid)
            {
                db.ArtikelBelegSet.Add(artikelBeleg);
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

            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name", artikelBeleg.ArtikelId);
            ViewBag.BelegId = new SelectList(db.BelegSet, "Id", "Id", artikelBeleg.BelegId);
            return View(artikelBeleg);
        }

        // GET: ArtikelBeleg/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelBeleg artikelBeleg = db.ArtikelBelegSet.Find(id);
            if (artikelBeleg == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name", artikelBeleg.ArtikelId);
            ViewBag.BelegId = new SelectList(db.BelegSet, "Id", "Id", artikelBeleg.BelegId);
            return View(artikelBeleg);
        }

        // POST: ArtikelBeleg/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ArtikelId,BelegId")] ArtikelBeleg artikelBeleg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikelBeleg).State = EntityState.Modified;
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
            ViewBag.ArtikelId = new SelectList(db.ArtikelSet, "Id", "Name", artikelBeleg.ArtikelId);
            ViewBag.BelegId = new SelectList(db.BelegSet, "Id", "Id", artikelBeleg.BelegId);
            return View(artikelBeleg);
        }

        // GET: ArtikelBeleg/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelBeleg artikelBeleg = db.ArtikelBelegSet.Find(id);
            if (artikelBeleg == null)
            {
                return HttpNotFound();
            }
            return View(artikelBeleg);
        }

        // POST: ArtikelBeleg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtikelBeleg artikelBeleg = db.ArtikelBelegSet.Find(id);
            db.ArtikelBelegSet.Remove(artikelBeleg);
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
