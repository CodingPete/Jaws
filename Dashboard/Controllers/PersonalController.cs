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
    public class PersonalController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Personal
        public ActionResult Index()
        {
            var personalSet = db.PersonalSet.Include(p => p.Arbeitsvertrag).Include(p => p.Rolle);
            return View(personalSet.ToList());
        }

        // GET: Personal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.PersonalSet.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Personal/Create
        public ActionResult Create()
        {
            ViewBag.ArbeitsvertragId = new SelectList(db.ArbeitsvertragSet, "Id", "Id");
            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name");
            return View();
        }

        // POST: Personal/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Vorname,Straße,Hausnummer,Zusatz,Postleitzahl,Ort,IBAN,BIC,Steuerklasse,Telefon,Mobil,ArbeitsvertragId,RolleId")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                db.PersonalSet.Add(personal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArbeitsvertragId = new SelectList(db.ArbeitsvertragSet, "Id", "Id", personal.ArbeitsvertragId);
            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name", personal.RolleId);
            return View(personal);
        }

        // GET: Personal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.PersonalSet.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArbeitsvertragId = new SelectList(db.ArbeitsvertragSet, "Id", "Id", personal.ArbeitsvertragId);
            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name", personal.RolleId);
            return View(personal);
        }

        // POST: Personal/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Vorname,Straße,Hausnummer,Zusatz,Postleitzahl,Ort,IBAN,BIC,Steuerklasse,Telefon,Mobil,ArbeitsvertragId,RolleId")] Personal personal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArbeitsvertragId = new SelectList(db.ArbeitsvertragSet, "Id", "Id", personal.ArbeitsvertragId);
            ViewBag.RolleId = new SelectList(db.RolleSet, "Id", "Name", personal.RolleId);
            return View(personal);
        }

        // GET: Personal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal personal = db.PersonalSet.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // POST: Personal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personal personal = db.PersonalSet.Find(id);
            db.PersonalSet.Remove(personal);
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
