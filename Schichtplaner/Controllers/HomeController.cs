﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schichtplaner.Controllers
{
    public class HomeController : Controller
    {
        private ServiceReference1.Service1Client client;

        public HomeController()
        {
            // Verbindungsaufbau zum Jaws_Server
            client = new ServiceReference1.Service1Client();

        }
        public ActionResult Index()
        {

            return View(client.getSchichtList());
        }

        public ActionResult Schichtplan()
        {
            var personen = client.getPersonalList();
            List<PersonSchichten> personschichten = new List<PersonSchichten>();
            foreach(var item in personen)
            {
                personschichten.Add(new PersonSchichten(item, client.getArbeitsvertragbyId(item.ArbeitsvertragId), client.getSchichtbyPersonalId(item.Id).ToArray()));
            }
            return View(personschichten);
        }
    }
}