using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private DataContainer db = new DataContainer();

        public ActionResult Index()
        {
            tagesUmsatz();
            return View();
        }

        private Double tagesUmsatz()
        {
            DateTime heute = DateTime.Today.Date;

            var result = (from b in db.BelegSet
                          join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                          join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                          join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                          where DbFunctions.TruncateTime(b.Datum) == DbFunctions.TruncateTime(heute)  && lfa.Name == "Verkauf"
                          select a
                          ).ToList();

            Double summe = 0;

            foreach (Artikel artikel in result) {
                summe += artikel.Nettoverkaufspreis;
            }

            return summe;
        }

        private Double tagesVerlust()
        {
            DateTime heute = DateTime.Today.Date;

            var result = (from b in db.BelegSet
                          join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                          join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                          join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                          where DbFunctions.TruncateTime(b.Datum) == DbFunctions.TruncateTime(heute) && lfa.Name == "Verlust"
                          select a
                          ).ToList();

            Double summe = 0;

            foreach (Artikel artikel in result)
            {
                summe += artikel.Nettoeinkaufspreis;
            }

            return summe;
        }
    }
}