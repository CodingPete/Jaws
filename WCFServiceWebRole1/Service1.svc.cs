using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DAL;
using System.Data.SqlTypes;

namespace WCFServiceWebRole1
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "Service1" sowohl im Code als auch in der SVC- und der Konfigurationsdatei ändern.
    // HINWEIS: Wählen Sie zum Starten des WCF-Testclients zum Testen dieses Diensts Service1.svc oder Service1.svc.cs im Projektmappen-Explorer aus, und starten Sie das Debuggen.
    public class Service1 : IService1
    {

        private DataContainer db;
        public Service1()
        {
            db = new DataContainer();
            db.Configuration.ProxyCreationEnabled = false;
        }

        public Arbeitsvertrag getArbeitsvertragbyId(int id)
        {
            return db.ArbeitsvertragSet.Find(id);
        }
       
        public List<Arbeitsvertrag> getArbeitsvertragList()
        {
            return db.ArbeitsvertragSet.ToList();
        }

        public Artikel getArtikelbyId(int id)
        {
            return db.ArtikelSet.Find(id);
        }

        public Artikel getArtikelByGTIN(String GTIN)
        {
            try
            {
                return db.ArtikelSet.First((x) => x.GTIN.Equals(GTIN));
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<Artikel> getArtikelbyLieferantId(int id)
        {
            return db.ArtikelSet.Where((a) => a.LieferantId == id).ToList();
        }

        public List<Artikel> getArtikelbyWarengruppeId(int id)
        {
            return db.ArtikelSet.Where((a) => a.WarengruppeId == id).ToList();
        }

        public List<Artikel> getArtikelbyBelegId(int id)
        {
            var buff = (from a in db.ArtikelSet
                        join c in db.ArtikelBelegSet on a.Id equals c.ArtikelId
                        where c.BelegId == id
                        select a).ToList();

            return buff;
        }

        public List<Artikel> getArtikelList()
        {
            return db.ArtikelSet.ToList();
        }

        public int getArtikelCountByArtikelIdAndLieferartIdAndBetween(int artikel_id, int lieferart_id, DateTime von, DateTime bis)
        {

            var result = (
                    from ab in db.ArtikelBelegSet
                    join b in db.BelegSet on ab.BelegId equals b.Id
                    where b.Datum >= von.Date && b.Datum <= bis.Date && ab.ArtikelId == artikel_id
                    select ab.ArtikelId
                );
            return result.Count();
        }

        public Beleg getBelegbyId(int id)
        {
            return db.BelegSet.Find(id);
        }

        public List<Beleg> getBelegbyArtikelId(int id)
        {
            var buff = (from a in db.BelegSet
                        join c in db.ArtikelBelegSet on a.Id equals c.BelegId
                        where c.ArtikelId == id
                        select a).ToList();

            return buff;
        }

        public List<Beleg> getBelegList()
        {
            return db.BelegSet.ToList();
        }

        public Lieferant getLieferantbyId(int id)
        {
            return db.LieferantSet.Find(id);
        }

        public List<Lieferant> getLieferantList()
        {
            return db.LieferantSet.ToList();
        }

        public Lieferart getLieferartbyId(int id)
        {
            return db.LieferartSet.Find(id);
        }
        public Lieferart getLieferartByName(String name)
        {
            try
            {
                return db.LieferartSet.First((x) => x.Name.Equals(name));
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<Lieferart> getLieferartList()
        {
            return db.LieferartSet.ToList();
        }

        public Personal getPersonalbyId(int id)
        {
            return db.PersonalSet.Find(id);
        }

        public List<Personal> getPersonalbyRolleId(int id)
        {
            return db.PersonalSet.Where((p) => p.RolleId == id).ToList();
        }

        public List<Personal> getPersonalList()
        {
            return db.PersonalSet.ToList();
        }

        public Prognose getPrognosebyArtikelId(int id)
        {
            return (DAL.Prognose)db.PrognoseSet.Where((p) => p.ArtikelId == id);
        }

        public Prognose getPrognoseByArtikelIdAndDate(int id, DateTime date)
        {
            DateTime start = date.Date;
            DateTime end = date.AddDays(1).Date;

            var prognosen = db.PrognoseSet.Where((p) => p.Id == id && p.Datum >= start && p.Datum <= end);
            if (prognosen !=null && prognosen.Count() != 0) return prognosen.First();
            else return null;
        }

        public List<Prognose> getPrognosebyDate(DateTime date)
        {
            return db.PrognoseSet.Where((p) => p.Datum == new SqlDateTime(date).Value).ToList();
        }

        public Prognose getPrognosebyId(int id)
        {
            return db.PrognoseSet.Find(id);
        }

        public List<Prognose> getPrognoseList()
        {
            return db.PrognoseSet.ToList();
        }

        public Recht getRechtbyId(int id)
        {
            return db.RechtSet.Find(id);
        }

        public List<Recht> getRechtbyRolleId(int id)
        {
            var buff = (from a in db.RechtSet
                        join c in db.RolleRechtSet on a.Id equals c.RechtId
                        where c.RolleId == id
                        select a).ToList();

            return buff;
        }

        public Rolle getRollebyId(int id)
        {
            return db.RolleSet.Find(id);
        }

        public List<Rolle> getRollebyRechtId(int id)
        {
            var buff = (from a in db.RolleSet
                        join c in db.RolleRechtSet on a.Id equals c.RolleId
                        where c.RechtId == id
                        select a).ToList();

            return buff;
        }

        public List<Rolle> getRolleList()
        {
            return db.RolleSet.ToList();
        }

        public List<Schicht> getSchichtBetween(DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public Schicht getSchichtbyId(int id)
        {
            return db.SchichtSet.Find(id);
        }

        public List<Schicht> getSchichtbyPersonalId(int id)
        {
            return db.SchichtSet.Where((s) => s.PersonalId == id).ToList();
        }

        public List<Schicht> getSchichtByPersonalIdAndBetween(int id, DateTime von, DateTime bis)
        {
            return db.SchichtSet.Where((s) => s.PersonalId == id && s.Startzeit_soll >= von.Date && s.Startzeit_soll <= bis.Date).ToList();
        }

        public Warengruppe getWarengruppebyId(int id)
        {
            return db.WarengruppeSet.Find(id);
        }

        public Warengruppe getWarengruppebyName(string name)
        {
            return (DAL.Warengruppe)db.WarengruppeSet.Where((w) => w.Name.Equals(name));
        }

        public List<Warengruppe> getWarengruppeList()
        {
            return db.WarengruppeSet.ToList();
        }

        public void setArbeitsvertrag(Arbeitsvertrag vertrag)
        {
            db.ArbeitsvertragSet.Add(vertrag);
            db.SaveChanges();

        }

        public void setArtikel(Artikel artikel)
        {
            db.ArtikelSet.Add(artikel);
            db.SaveChanges();

        }

        public void setArtikelBeleg(Artikel artikel, Beleg beleg)
        {
            
            db.ArtikelBelegSet.Add(new ArtikelBeleg() { ArtikelId = artikel.Id, BelegId = beleg.Id });
            db.SaveChanges();
            

        }

        public int setBeleg(Beleg beleg)
        {
            Lieferart lfa = this.getLieferartbyId(beleg.LieferartId);
            beleg.Lieferart = lfa;
            beleg.Lieferart.Beleg.Add(beleg);
            db.BelegSet.Add(beleg);
            db.SaveChanges();
            return beleg.Id;

        }

        public void setLieferant(Lieferant lieferant)
        {
            db.LieferantSet.Add(lieferant);
            db.SaveChanges();

        }

        public void setLieferart(Lieferart lieferart)
        {
            db.LieferartSet.Add(lieferart);
            db.SaveChanges();

        }

        public void setPersonal(Personal person)
        {
            db.PersonalSet.Add(person);
            db.SaveChanges();

        }

        public int setPrognose(Prognose prognose)
        {
            Artikel artikel = getArtikelbyId(prognose.ArtikelId);
            prognose.Artikel = artikel;
            prognose.Artikel.Prognose.Add(prognose);
            //updateArtikel(prognose.Artikel);
            db.PrognoseSet.Add(prognose);
            db.SaveChanges();
            return prognose.Id;
        }

        public void setRecht(Recht recht)
        {
            db.RechtSet.Add(recht);
            db.SaveChanges();

        }

        public void setRolle(Rolle role)
        {
            db.RolleSet.Add(role);
            db.SaveChanges();

        }

        public void setRolleRecht(Rolle rolle, Recht recht)
        {
            db.RolleRechtSet.Add(new RolleRecht() { RolleId = rolle.Id, RechtId = recht.Id });
            db.SaveChanges();

        }

        public int setSchicht(Schicht schicht)
        {
            Personal person = getPersonalbyId(schicht.PersonalId);
            schicht.Personal = person;
            schicht.Personal.Schicht.Add(schicht);
            db.SchichtSet.Add(schicht);
            db.SaveChanges();
            return schicht.Id;



        }

        public void setWarengruppe(Warengruppe gruppe)
        {
            db.WarengruppeSet.Add(gruppe);
            db.SaveChanges();

        }

        public void updateArbeitsvertrag(Arbeitsvertrag arbeitsvertrag)
        {
            db.ArbeitsvertragSet.Attach(arbeitsvertrag);
            var entry = db.Entry(arbeitsvertrag);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateArtikel(Artikel artikel)
        {
           
            db.ArtikelSet.Attach(artikel);
            var entry = db.Entry(artikel);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateArtikelBeleg(ArtikelBeleg artikelbeleg)
        {
            db.ArtikelBelegSet.Attach(artikelbeleg);
            var entry = db.Entry(artikelbeleg);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateBeleg(Beleg beleg)
        {
            db.BelegSet.Attach(beleg);
            var entry = db.Entry(beleg);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateLieferant(Lieferant lieferant)
        {
            db.LieferantSet.Attach(lieferant);
            var entry = db.Entry(lieferant);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateLieferart(Lieferart lieferart)
        {
            db.LieferartSet.Attach(lieferart);
            var entry = db.Entry(lieferart);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updatePersonal(Personal personal)
        {
            db.PersonalSet.Attach(personal);
            var entry = db.Entry(personal);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updatePrognose(Prognose prognose)
        {
            db.PrognoseSet.Attach(prognose);
            var entry = db.Entry(prognose);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateRecht(Recht recht)
        {
            db.RechtSet.Attach(recht);
            var entry = db.Entry(recht);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateRolle(Rolle rolle)
        {
            db.RolleSet.Attach(rolle);
            var entry = db.Entry(rolle);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateRolleRecht(RolleRecht rollerecht)
        {
            db.RolleRechtSet.Attach(rollerecht);
            var entry = db.Entry(rollerecht);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateSchicht(Schicht schicht)
        {
            db.SchichtSet.Attach(schicht);
            var entry = db.Entry(schicht);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void updateWarengruppe(Warengruppe warengruppe)
        {
            db.WarengruppeSet.Attach(warengruppe);
            var entry = db.Entry(warengruppe);
            entry.State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public Personal getPersonalbyEmail(string mail)
        {
            Personal person;
            try
            {
                person = db.PersonalSet.First((x) => x.email.Equals(mail));
            }
            catch(Exception e)
            {
                person = null;
            }
            return person;
        }

        public Recht getRechtbyName(string name)
        {
            Recht recht;
            try
            {
                recht = db.RechtSet.First((x) => x.Name.Equals(name));
            }catch(Exception e)
            {
                recht = null;
            }
            return recht;
        }

        public void deleteSchichtByWeek(DateTime date)
        {
            DateTime monday = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime sunday = monday.AddDays(6).Date;

            List<Schicht> list =  db.SchichtSet.Where((s) => s.Startzeit_soll >= monday && s.Startzeit_soll <= sunday).ToList();
            foreach (Schicht schicht in list)
            {
                db.SchichtSet.Attach(schicht);
                db.SchichtSet.Remove(schicht);
            }
            db.SaveChanges();

        }

        public List<Schicht> getSchichtList()
        {
            return db.SchichtSet.ToList();
        }
    }
}
