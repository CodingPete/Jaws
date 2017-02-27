using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schichtplaner.Controllers
{
    public class PlanController : Controller
    {
        private ServiceReference1.Service1Client client;

        public PlanController()
        {
            // Verbindungsaufbau zum Jaws_Server
            client = new ServiceReference1.Service1Client();
            
        }

        // GET: Plan
        public ActionResult Index()
        {
            List<Object> schichtenListe = new List<Object>();

            // Hole alle Angestellten
            var personalliste = client.getPersonalList();
            ViewBag.personalliste = personalliste;
            /*foreach(Jaws_Service.Personal person in personalliste)
            {
                person.V
                // Für jede Person, die Schichten dieser Woche holen
                DateTime dieseWocheMontag = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);

                // Für jeden Wochentag eine Schicht oder 0 holen
                for(int i = 0; i < 7; i++)
                {
                    var schichten = client.getSchichtByPersonalIdAndBetween(person.Id, dieseWocheMontag, dieseWocheMontag.AddDays(i));

                    // Ablage der Schichten in Array
                    if (schichten.Count() == 0)
                    {

                    }

                }
                
                   
            }*/

            ViewBag.schichtenListe = schichtenListe;

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
            ViewBag.personalliste = client.getPersonalList();
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
            //var person = client.getPersonalbyId(id);
            var schicht = client.getSchichtbyPersonalId(id);
            return View(schicht);
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
