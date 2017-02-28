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
    public class SchichtController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Schicht
        public ActionResult Index()
        {
            var schichtSet = db.SchichtSet.Include(s => s.Personal);
            return View(schichtSet.ToList());
        }

        public ActionResult GetAll()
        {
            var schichtSet = db.SchichtSet.Include(s => s.Personal);

            var javaScriptSerializer = new
            System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(schichtSet.ToArray());

            //  Send "Success"
            return Json(new { success = true, responseText = jsonString }, JsonRequestBehavior.AllowGet);
        }

        // GET: Schicht/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schicht schicht = db.SchichtSet.Find(id);
            if (schicht == null)
            {
                return HttpNotFound();
            }
            return View(schicht);
        }

        // GET: Schicht/Create
        public ActionResult Create()
        {
            ViewBag.PersonalId = new SelectList(db.PersonalSet, "Id", "Name");
            return View();
        }

        // POST: Schicht/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Startzeit_soll,Endzeit_soll,Startzeit_ist,Endzeit_ist,Pause,PersonalId")] Schicht schicht)
        {
            if (ModelState.IsValid)
            {
                db.SchichtSet.Add(schicht);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonalId = new SelectList(db.PersonalSet, "Id", "Name", schicht.PersonalId);
            return View(schicht);
        }

        // GET: Schicht/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schicht schicht = db.SchichtSet.Find(id);
            if (schicht == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonalId = new SelectList(db.PersonalSet, "Id", "Name", schicht.PersonalId);
            return View(schicht);
        }

        // POST: Schicht/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Startzeit_soll,Endzeit_soll,Startzeit_ist,Endzeit_ist,Pause,PersonalId")] Schicht schicht)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schicht).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonalId = new SelectList(db.PersonalSet, "Id", "Name", schicht.PersonalId);
            return View(schicht);
        }

        // GET: Schicht/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schicht schicht = db.SchichtSet.Find(id);
            if (schicht == null)
            {
                return HttpNotFound();
            }
            return View(schicht);
        }

        // POST: Schicht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schicht schicht = db.SchichtSet.Find(id);
            db.SchichtSet.Remove(schicht);
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
