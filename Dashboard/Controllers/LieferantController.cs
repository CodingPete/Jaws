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
    public class LieferantController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Lieferant
        public ActionResult Index()
        {
            return View(db.LieferantSet.ToList());
        }


        public ActionResult TestData()
        {
            Artikel artikel = db.ArtikelSet.Where((a) => a.GTIN == "100").First();
            Lieferart lfa = db.LieferartSet.Where((l) => l.Name == "Verkauf").First();

            DateTime damals = new DateTime(2016, 12, 12).Date;
            while (!damals.Equals(DateTime.Today.Date))
            {
                damals = damals.AddDays(1);

                // Neuen Verkaufsbeleg erstellen
                Beleg beleg = new Beleg();
                beleg.Datum = damals;
                beleg.Lieferart = lfa;
                beleg.LieferartId = lfa.Id;
                db.BelegSet.Add(beleg);
                db.SaveChanges();

                Random rndm = new Random();
                int menge = rndm.Next(1, 13);

                for (int i = 0; i < menge; i++)
                {
                    ArtikelBeleg artikelBeleg = new ArtikelBeleg();
                    artikelBeleg.Artikel = artikel;
                    artikelBeleg.ArtikelId = artikel.Id;
                    artikelBeleg.Beleg = beleg;
                    artikelBeleg.BelegId = beleg.Id;
                    artikelBeleg.Menge = 0;
                    db.ArtikelBelegSet.Add(artikelBeleg);
                }
                db.SaveChanges();
            }
            return null;
        }

        public ActionResult Modal(int? id)
        {
            var currentLieferant = db.LieferantSet.Find(id);
            List<Artikel> articles = db.ArtikelSet.Where((x) => x.LieferantId == currentLieferant.Id).ToList();
            List<Helper_ArtikelPrognose> artikelprognose = new List<Helper_ArtikelPrognose>();
            DateTime heute = DateTime.Today.Date;
            DateTime heuteInEinerWoche = heute.AddDays(7);
            foreach (Artikel article in articles)
            {
                Double prognose_summe = (from p in db.PrognoseSet
                                         where p.Datum >= heute && p.Datum <= heuteInEinerWoche && p.ArtikelId == article.Id
                                         select p.Abverkauf_soll).DefaultIfEmpty(0).Sum();
                // Benötigte Menge 
                int menge = (int)Math.Ceiling(article.Bestand - prognose_summe)*(-1);
                artikelprognose.Add(new Helper_ArtikelPrognose() { prognosemenge = menge, artikel = article });
            }
            ViewBag.ArtikelPrognose = artikelprognose;

            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bestellung()
        {
            var post = Request.Form;
            var artikelid = post["selarticle"]; //get selected article

            if (artikelid != null)
            {
                this.Bestellung(Int32.Parse(artikelid));
            }
            return RedirectToAction("Index", "Lieferant");
        }
        // GET: Lieferant/Bestellung/5
        public ActionResult Bestellung(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DateTime heute = DateTime.Today.Date;
            DateTime heuteInEinerWoche = heute.AddDays(7);

            Beleg bestellung = new Beleg();

            Artikel artikel = db.ArtikelSet.Find(id);
            // Der Lieferant zu dem die Bestellung erstellt werden soll
            

            if (artikel != null)
            {
                Lieferant lieferant = db.LieferantSet.Where((x) => x.Id == artikel.LieferantId).First();
                // Die Lieferart
                Lieferart lfa = db.LieferartSet.Where((l) => l.Name == "Einkauf").First();

                // Alle Artikel des Lieferanten

                // Artikel-Beleg zuordnung
                List<ArtikelBeleg> artikelbelegs = new List<ArtikelBeleg>();

                // Der neue Bestellbeleg

                bestellung.Lieferart = lfa;
                bestellung.LieferartId = lfa.Id;
                bestellung.Datum = heute;
                db.BelegSet.Add(bestellung);
                db.SaveChanges();

                // Zu jedem Artikel Bestellmenge anhand des derzeitigen Bestandes und der erwarteten Prognosen der kommenden Woche berechnen


                    Double prognose_summe = (from p in db.PrognoseSet
                                             where p.Datum >= heute && p.Datum <= heuteInEinerWoche && p.ArtikelId == artikel.Id
                                             select p.Abverkauf_soll).DefaultIfEmpty(0).Sum();

                    // Benötigte Menge 
                    int menge = (int)Math.Ceiling(artikel.Bestand - prognose_summe);

                    // Menge kleiner Null => Bestand reicht nicht aus um den prognostizierten Abverkauf zu decken
                    if (menge < 0)
                    {
                        menge = Math.Abs(menge);

                        for (int i = 0; i < menge; i++)
                        {
                            ArtikelBeleg artikelbeleg = new ArtikelBeleg();
                            artikelbeleg.Artikel = artikel;
                            artikelbeleg.ArtikelId = artikel.Id;
                            artikelbeleg.Beleg = bestellung;
                            artikelbeleg.BelegId = bestellung.Id;
                            artikelbeleg.Menge = 0;
                            db.ArtikelBelegSet.Add(artikelbeleg);
                            artikelbelegs.Add(artikelbeleg);


                        }
                        artikel.Bestand += menge;
                        db.ArtikelSet.Attach(artikel);
                        var entry = db.Entry(artikel);
                        entry.State = System.Data.Entity.EntityState.Modified;
                    }


                // Wenn keine Artikel in der Bestellung vorliegen
                if (artikelbelegs.Count() == 0)
                {
                    // Leeren Bestellbeleg wieder entfernen
                    db.BelegSet.Remove(bestellung);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Lieferant");
                }
                db.SaveChanges();
                return RedirectToAction("Details", "Beleg", new { id = bestellung.Id });
            }
            return RedirectToAction("Index", "Lieferant");
        }
        // GET: Lieferant/Bestellung/5
        /*public ActionResult Bestellung(int? id)
        {
            if(id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DateTime heute = DateTime.Today.Date;
            DateTime heuteInEinerWoche = heute.AddDays(7);

            Beleg bestellung = new Beleg();

            // Der Lieferant zu dem die Bestellung erstellt werden soll
            Lieferant lieferant = db.LieferantSet.Find(id);

            if (lieferant != null)
            {
                // Die Lieferart
                Lieferart lfa = db.LieferartSet.Where((l) => l.Name == "Einkauf").First();

                // Alle Artikel des Lieferanten
                List<Artikel> artikels = db.ArtikelSet.Where((a) => a.LieferantId == lieferant.Id).ToList();

                // Artikel-Beleg zuordnung
                List<ArtikelBeleg> artikelbelegs = new List<ArtikelBeleg>();

                // Der neue Bestellbeleg
                
                bestellung.Lieferart = lfa;
                bestellung.LieferartId = lfa.Id;
                bestellung.Datum = heute;
                db.BelegSet.Add(bestellung);
                db.SaveChanges();

                // Zu jedem Artikel Bestellmenge anhand des derzeitigen Bestandes und der erwarteten Prognosen der kommenden Woche berechnen
               

                foreach (Artikel artikel in artikels)
                {
                    Double prognose_summe = (from p in db.PrognoseSet
                                             where p.Datum >= heute && p.Datum <= heuteInEinerWoche && p.ArtikelId == artikel.Id
                                             select p.Abverkauf_soll).DefaultIfEmpty(0).Sum();

                    // Benötigte Menge 
                    int menge = (int)Math.Ceiling(artikel.Bestand - prognose_summe);

                    // Menge kleiner Null => Bestand reicht nicht aus um den prognostizierten Abverkauf zu decken
                    if (menge < 0)
                    {
                        menge = Math.Abs(menge);

                        for (int i = 0; i < menge; i++)
                        {
                            ArtikelBeleg artikelbeleg = new ArtikelBeleg();
                            artikelbeleg.Artikel = artikel;
                            artikelbeleg.ArtikelId = artikel.Id;
                            artikelbeleg.Beleg = bestellung;
                            artikelbeleg.BelegId = bestellung.Id;
                            artikelbeleg.Menge = 0;
                            db.ArtikelBelegSet.Add(artikelbeleg);
                            artikelbelegs.Add(artikelbeleg);

                            
                        }
                        artikel.Bestand += menge;
                        db.ArtikelSet.Attach(artikel);
                        var entry = db.Entry(artikel);
                        entry.State = System.Data.Entity.EntityState.Modified;
                    }

                }

                // Wenn keine Artikel in der Bestellung vorliegen
                if (artikelbelegs.Count() == 0)
                {
                    // Leeren Bestellbeleg wieder entfernen
                    db.BelegSet.Remove(bestellung);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Lieferant");
                }
                db.SaveChanges();
                return RedirectToAction("Details", "Beleg", new { id = bestellung.Id });
            }
            return RedirectToAction("Index", "Lieferant");
        }*/

        // GET: Lieferant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferant lieferant = db.LieferantSet.Find(id);
            if (lieferant == null)
            {
                return HttpNotFound();
            }
            return View(lieferant);
        }

        // GET: Lieferant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lieferant/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Straße,Hausnummer,Zusatz,Postleitzahl,Ort,Telefon,Mobil,Email,IBAN,BIC")] Lieferant lieferant)
        {
            if (ModelState.IsValid)
            {
                db.LieferantSet.Add(lieferant);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
                return RedirectToAction("Index");
            }

            return View(lieferant);
        }

        // GET: Lieferant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferant lieferant = db.LieferantSet.Find(id);
            if (lieferant == null)
            {
                return HttpNotFound();
            }
            return View(lieferant);
        }

        // POST: Lieferant/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Straße,Hausnummer,Zusatz,Postleitzahl,Ort,Telefon,Mobil,Email,IBAN,BIC")] Lieferant lieferant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lieferant).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
                return RedirectToAction("Index");
            }
            return View(lieferant);
        }

        // GET: Lieferant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lieferant lieferant = db.LieferantSet.Find(id);
            if (lieferant == null)
            {
                return HttpNotFound();
            }
            return View(lieferant);
        }

        // POST: Lieferant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lieferant lieferant = db.LieferantSet.Find(id);
            db.LieferantSet.Remove(lieferant);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }
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
