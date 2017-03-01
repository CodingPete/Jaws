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
            List<MiniStatistik> statistikListe = new List<MiniStatistik>();
            statistikListe.Add(tagesUmsatz());
            statistikListe.Add(wochenUmsatz());
            statistikListe.Add(tagesVerlust());
            statistikListe.Add(tagesStundenLeistung());

            return View(statistikListe);
        }

        private MiniStatistik tagesUmsatz()
        {
            DateTime jetzt = DateTime.Now;
            DateTime heute = DateTime.Today.Date;

            DateTime letzteWoche = jetzt.AddDays(-7);

            MiniStatistik tagesUmsatz = new MiniStatistik();
            tagesUmsatz.Name = "Tagesumsatz";
            tagesUmsatz.Einheit = "€";
            tagesUmsatz.Wert = (from b in db.BelegSet
                                  join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                                  join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                                  join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                                  where DbFunctions.TruncateTime(b.Datum) == DbFunctions.TruncateTime(heute)  && lfa.Name == "Verkauf"
                                  select a.Nettoverkaufspreis
                                  ).DefaultIfEmpty(0).Sum();
            tagesUmsatz.Wert = Math.Round(tagesUmsatz.Wert, 2);

            Double tagesUmsatzLetzteWoche = (from b in db.BelegSet
                                      join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                                      join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                                      join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                                      where DbFunctions.TruncateTime(b.Datum) == DbFunctions.TruncateTime(letzteWoche) && b.Datum <= letzteWoche && lfa.Name == "Verkauf"
                                      select a.Nettoverkaufspreis
                                  ).DefaultIfEmpty(0).Sum();

            if (tagesUmsatzLetzteWoche != 0)
                tagesUmsatz.Prozent = (int)(tagesUmsatzLetzteWoche / tagesUmsatz.Wert * 100);
            else tagesUmsatz.Prozent = 0;
            return tagesUmsatz;
        }

        private MiniStatistik wochenUmsatz()
        {

            DateTime heute = DateTime.Now;
            DateTime montag = heute.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).Date;
            DateTime heuteLetzteWoche = heute.AddDays(-7);
            DateTime montagLetzteWoche = heuteLetzteWoche.AddDays(-(int)heuteLetzteWoche.DayOfWeek + (int)DayOfWeek.Monday);

            MiniStatistik wochenUmsatz = new MiniStatistik();
            wochenUmsatz.Name = "Wochenumsatz";
            wochenUmsatz.Einheit = "€";
            wochenUmsatz.Wert = (from b in db.BelegSet
                                join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                                join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                                join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                                where DbFunctions.TruncateTime(b.Datum) >= DbFunctions.TruncateTime(montag) && b.Datum <= heute && lfa.Name == "Verkauf"
                                select a.Nettoverkaufspreis
                                  ).DefaultIfEmpty(0).Sum();
            wochenUmsatz.Wert = Math.Round(wochenUmsatz.Wert, 2);

            Double wochenUmsatzLetzteWoche = (from b in db.BelegSet
                                       join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                                       join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                                       join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                                       where DbFunctions.TruncateTime(b.Datum) >= DbFunctions.TruncateTime(montagLetzteWoche) && b.Datum <= heuteLetzteWoche && lfa.Name == "Verkauf"
                                       select a.Nettoverkaufspreis
                                  ).DefaultIfEmpty(0).Sum();
            if (wochenUmsatzLetzteWoche != 0)
            {
                wochenUmsatz.Prozent = (int)(wochenUmsatz.Wert / wochenUmsatzLetzteWoche * 100);
            }
            else wochenUmsatz.Prozent = 0;

            return wochenUmsatz;
        }

        private MiniStatistik tagesVerlust()
        {
            DateTime jetzt = DateTime.Now;
            DateTime heute = DateTime.Today.Date;

            DateTime letzteWoche = jetzt.AddDays(-7);

            MiniStatistik tagesVerlust = new MiniStatistik();
            tagesVerlust.Name = "Tagesverlust";
            tagesVerlust.Einheit = "€";
            tagesVerlust.Wert = (from b in db.BelegSet
                                  join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                                  join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                                  join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                                  where DbFunctions.TruncateTime(b.Datum) == DbFunctions.TruncateTime(heute) && lfa.Name == "Verlust"
                                  select a.Nettoeinkaufspreis
                                  ).DefaultIfEmpty(0).Sum();
            tagesVerlust.Wert = Math.Round(tagesVerlust.Wert, 2);

            Double tagesVerlustLetzteWoche = (from b in db.BelegSet
                                             join lfa in db.LieferartSet on b.LieferartId equals lfa.Id
                                             join ab in db.ArtikelBelegSet on b.Id equals ab.BelegId
                                             join a in db.ArtikelSet on ab.ArtikelId equals a.Id
                                             where DbFunctions.TruncateTime(b.Datum) == DbFunctions.TruncateTime(letzteWoche) && b.Datum <= letzteWoche && lfa.Name == "Verkauf"
                                             select a.Nettoverkaufspreis
                                  ).DefaultIfEmpty(0).Sum();
            if (tagesVerlustLetzteWoche != 0)
                tagesVerlust.Prozent = (int)(tagesVerlustLetzteWoche / tagesVerlust.Wert * 100);
            else tagesVerlust.Prozent = 0;
            return tagesVerlust;
        }

      

        private MiniStatistik tagesStundenLeistung()
        {
            DateTime heute = DateTime.Now;
            DateTime montag = heute.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime heuteLetzteWoche = heute.AddDays(-7);
            DateTime montagLetzteWoche = heuteLetzteWoche.AddDays(-(int)heuteLetzteWoche.DayOfWeek + (int)DayOfWeek.Monday);

            List<Schicht> abgelaufen = (from s in db.SchichtSet
                          where DbFunctions.TruncateTime(s.Startzeit_soll) >= DbFunctions.TruncateTime(heute) && s.Endzeit_soll <= heute
                          select s).ToList();
            List<Schicht> laufend = (from s in db.SchichtSet
                                     where DbFunctions.TruncateTime(s.Startzeit_soll) >= DbFunctions.TruncateTime(heute) && s.Endzeit_soll > heute
                                     select s).ToList();

            TimeSpan dauer = new TimeSpan();
            foreach (Schicht schicht in abgelaufen)
            {
                dauer.Add(schicht.Endzeit_soll.Subtract(schicht.Startzeit_soll));
            }
            foreach (Schicht schicht in laufend)
            {
                dauer.Add(heute.Subtract(schicht.Startzeit_soll));
            }

            Double stundenleistung = 0;
            var stunden = dauer.TotalHours;
            if (stunden != 0)
                stundenleistung = tagesUmsatz().Wert / stunden;
            else stundenleistung = 0;

            MiniStatistik tagesStundenLeistung = new MiniStatistik();
            tagesStundenLeistung.Name = "Tagesstundenleistung";
            tagesStundenLeistung.Wert = Math.Round(stundenleistung, 2);
            tagesStundenLeistung.Einheit = "€";
            tagesStundenLeistung.Prozent = 0;
            return tagesStundenLeistung;
        }
    }
}