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
            
        }

        // GET: Plan
        public ActionResult Index()
        {
            // Hole alle Angestellten
            ViewBag.personalliste = client.getPersonalList();

            // Hole alle Schichten der aktuellen Woche
            var schichtliste = client.getSchichtBetween(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday), DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday));
            
            // Übergeben der Schichten an View
            return View(schichtliste);
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
