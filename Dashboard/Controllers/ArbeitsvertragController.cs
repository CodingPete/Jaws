﻿using System;
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
    public class ArbeitsvertragController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Arbeitsvertrag
        public ActionResult Index()
        {
            return View(db.ArbeitsvertragSet.ToList());
        }

        // GET: Arbeitsvertrag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbeitsvertrag arbeitsvertrag = db.ArbeitsvertragSet.Find(id);
            if (arbeitsvertrag == null)
            {
                return HttpNotFound();
            }
            return View(arbeitsvertrag);
        }

        // GET: Arbeitsvertrag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arbeitsvertrag/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Wochenstunden,Stundenlohn,Name")] Arbeitsvertrag arbeitsvertrag)
        {
            if (ModelState.IsValid)
            {
                db.ArbeitsvertragSet.Add(arbeitsvertrag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arbeitsvertrag);
        }

        // GET: Arbeitsvertrag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbeitsvertrag arbeitsvertrag = db.ArbeitsvertragSet.Find(id);
            if (arbeitsvertrag == null)
            {
                return HttpNotFound();
            }
            return View(arbeitsvertrag);
        }

        // POST: Arbeitsvertrag/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Wochenstunden,Stundenlohn,Name")] Arbeitsvertrag arbeitsvertrag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arbeitsvertrag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arbeitsvertrag);
        }

        // GET: Arbeitsvertrag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arbeitsvertrag arbeitsvertrag = db.ArbeitsvertragSet.Find(id);
            if (arbeitsvertrag == null)
            {
                return HttpNotFound();
            }
            return View(arbeitsvertrag);
        }

        // POST: Arbeitsvertrag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arbeitsvertrag arbeitsvertrag = db.ArbeitsvertragSet.Find(id);
            db.ArbeitsvertragSet.Remove(arbeitsvertrag);
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
