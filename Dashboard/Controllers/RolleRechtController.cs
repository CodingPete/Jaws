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
    public class RolleRechtController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: RolleRecht
        public ActionResult Index()
        {
            var rolleRechtSet = db.RolleRechtSet.Include(r => r.Rolle).Include(r => r.Recht);
            return View(rolleRechtSet.ToList());
        }

        // GET: RolleRecht/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolleRecht rolleRecht = db.RolleRechtSet.Find(id);
            if (rolleRecht == null)
            {
                return HttpNotFound();
            }
            return View(rolleRecht);
        }

        // GET: RolleRecht/Create
        public ActionResult Create()
        {
            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name");
            ViewBag.RechtId = new SelectList(db.RechtSet, "Id", "Name");
            return View();
        }

        // POST: RolleRecht/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RolleId,RechtId")] RolleRecht rolleRecht)
        {
            if (ModelState.IsValid)
            {
                db.RolleRechtSet.Add(rolleRecht);
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

            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name", rolleRecht.RolleId);
            ViewBag.RechtId = new SelectList(db.RechtSet, "Id", "Name", rolleRecht.RechtId);
            return View(rolleRecht);
        }

        // GET: RolleRecht/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolleRecht rolleRecht = db.RolleRechtSet.Find(id);
            if (rolleRecht == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name", rolleRecht.RolleId);
            ViewBag.RechtId = new SelectList(db.RechtSet, "Id", "Name", rolleRecht.RechtId);
            return View(rolleRecht);
        }

        // POST: RolleRecht/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RolleId,RechtId")] RolleRecht rolleRecht)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolleRecht).State = EntityState.Modified;
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
            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name", rolleRecht.RolleId);
            ViewBag.RechtId = new SelectList(db.RechtSet, "Id", "Name", rolleRecht.RechtId);
            return View(rolleRecht);
        }

        // GET: RolleRecht/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolleRecht rolleRecht = db.RolleRechtSet.Find(id);
            if (rolleRecht == null)
            {
                return HttpNotFound();
            }
            return View(rolleRecht);
        }

        // POST: RolleRecht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RolleRecht rolleRecht = db.RolleRechtSet.Find(id);
            db.RolleRechtSet.Remove(rolleRecht);
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
