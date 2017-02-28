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
    public class LieferartController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Lieferart
        public ActionResult Index()
        {
            return View(db.LieferartSet.ToList());
        }

        // GET: Lieferart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferart lieferart = db.LieferartSet.Find(id);
            if (lieferart == null)
            {
                return HttpNotFound();
            }
            return View(lieferart);
        }

        // GET: Lieferart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lieferart/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Lieferart lieferart)
        {
            if (ModelState.IsValid)
            {
                db.LieferartSet.Add(lieferart);
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

            return View(lieferart);
        }

        // GET: Lieferart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferart lieferart = db.LieferartSet.Find(id);
            if (lieferart == null)
            {
                return HttpNotFound();
            }
            return View(lieferart);
        }

        // POST: Lieferart/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Lieferart lieferart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lieferart).State = EntityState.Modified;
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
            return View(lieferart);
        }

        // GET: Lieferart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferart lieferart = db.LieferartSet.Find(id);
            if (lieferart == null)
            {
                return HttpNotFound();
            }
            return View(lieferart);
        }

        // POST: Lieferart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lieferart lieferart = db.LieferartSet.Find(id);
            db.LieferartSet.Remove(lieferart);
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
