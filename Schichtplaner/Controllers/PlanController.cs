using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Schichtplaner.ServiceReference1;

namespace Schichtplaner.Controllers
{
    [Authorize(Roles = "Schichtplaner")]
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
            DateTime monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);

            if (Request["monday"] != null)
            {
                monday = DateTime.Parse(Request["monday"]);
            }
            DateTime sunday = monday.AddDays(6).Date;

            List<PersonSchichten> personSchichtenListe = new List<PersonSchichten>();

            // Hole alle Angestellten
            var personalliste = client.getPersonalList();
           

           
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

            ViewBag.monday = monday;
            ViewBag.sunday = sunday;
            return View(personSchichtenListe);
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

        
    }
}
