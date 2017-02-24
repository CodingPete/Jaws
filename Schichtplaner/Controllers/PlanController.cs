using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schichtplaner.Controllers
{
    public class PlanController : Controller
    {
        private Jaws_Service.Service1Client client;

        public PlanController()
        {
            // Verbindungsaufbau zum Jaws_Server
            client = new Jaws_Service.Service1Client();
            // Prüfen ob Benutzer erlaubt
            // Wenn ja darf er passieren
            // Ansonsten Login View anzeigen.
        }

        // GET: Plan
        public ActionResult Index()
        {
            // Hole alle Angestellten
            var personalliste = client.getPersonalList();

            // Hole alle Schichten der aktuellen Woche
            //var schichtliste = List<Jaws_Service.Schicht>//client.getSchichtBetween(new DateTime(), new DateTime());

            // Übergeben der Personalliste an ViewBag
            ViewBag.personalliste = personalliste;

            // Übergeben der Schichten an View
            return View();
        }

        // GET: Plan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Plan/Create
        public ActionResult Create()
        {
            // Empfange Liste an Schichten

            // Sende Schichten an den Server

            return View();
        }

        // POST: Plan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // Empfange Liste an Schichten

                // Sende Schichten an den Server

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Plan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Plan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Plan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Plan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
