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
    public class WarengruppeController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Warengruppe
        public ActionResult Index()
        {
            return View(db.WarengruppeSet.ToList());
        }

        // GET: Warengruppe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warengruppe warengruppe = db.WarengruppeSet.Find(id);
            if (warengruppe == null)
            {
                return HttpNotFound();
            }
            return View(warengruppe);
        }

        // GET: Warengruppe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warengruppe/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Steuersatz")] Warengruppe warengruppe)
        {
            if (ModelState.IsValid)
            {
                db.WarengruppeSet.Add(warengruppe);
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

            return View(warengruppe);
        }

        // GET: Warengruppe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warengruppe warengruppe = db.WarengruppeSet.Find(id);
            if (warengruppe == null)
            {
                return HttpNotFound();
            }
            return View(warengruppe);
        }

        // POST: Warengruppe/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Steuersatz")] Warengruppe warengruppe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warengruppe).State = EntityState.Modified;
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
            return View(warengruppe);
        }

        // GET: Warengruppe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warengruppe warengruppe = db.WarengruppeSet.Find(id);
            List < Artikel > artikel = warengruppe.Artikel.ToList();
            db.ArtikelSet.RemoveRange(artikel);
            
            if (warengruppe == null)
            {
                return HttpNotFound();
            }
            return View(warengruppe);
        }

        // POST: Warengruppe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Warengruppe warengruppe = db.WarengruppeSet.Find(id);
            db.WarengruppeSet.Remove(warengruppe);
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
