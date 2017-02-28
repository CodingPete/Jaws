using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Schichtplaner.ServiceReference1;

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
            List<PersonSchichten> personSchichtenListe = new List<PersonSchichten>();

            // Hole alle Angestellten
            var personalliste = client.getPersonalList();

            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime sunday = monday.AddDays(6).Date;
            foreach (Personal person in personalliste)
            {
                // Person in ihrer Schichtenliste ablegen
                PersonSchichten personSchicht = new PersonSchichten();
                personSchicht.person = person;
                personSchicht.vertrag = client.getArbeitsvertragbyId(person.ArbeitsvertragId);
                
                // Alle Schichten dieser Woche holen             
                personSchicht.schichten = client.getSchichtByPersonalIdAndBetween(person.Id, monday, sunday);

                personSchichtenListe.Add(personSchicht);
            }
            
            return View(personSchichtenListe);
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
        public ActionResult Create(List<Schicht> schichten)
        {
            try
            {
                client.deleteSchichtByWeek(schichten.First().Startzeit_ist);

                foreach (Schicht schicht in schichten)
                {
                    client.setSchicht(schicht);
                }

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
