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
    public class RolleController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Rolle
        public ActionResult Index()
        {
            return View(db.RolleSet.ToList());
        }

        // GET: Rolle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rolle rolle = db.RolleSet.Find(id);
            if (rolle == null)
            {
                return HttpNotFound();
            }
            return View(rolle);
        }

        // GET: Rolle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rolle/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Rolle rolle)
        {
            if (ModelState.IsValid)
            {
                db.RolleSet.Add(rolle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rolle);
        }

        // GET: Rolle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rolle rolle = db.RolleSet.Find(id);
            if (rolle == null)
            {
                return HttpNotFound();
            }
            return View(rolle);
        }

        // POST: Rolle/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Rolle rolle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rolle);
        }

        // GET: Rolle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rolle rolle = db.RolleSet.Find(id);
            if (rolle == null)
            {
                return HttpNotFound();
            }
            return View(rolle);
        }

        // POST: Rolle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rolle rolle = db.RolleSet.Find(id);
            db.RolleSet.Remove(rolle);
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
