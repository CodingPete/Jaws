using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;
using System.Data.SqlTypes;
using System.Reflection;

namespace Jaws_Service_Library
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Klassennamen "Service1" sowohl im Code als auch in der Konfigurationsdatei ändern.
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

        public List<Artikel> getArtikelbyLieferantId(int id)
        {
            return db.ArtikelSet.Where((a) => a.LieferantId == id).ToList();
        }

        public List<Artikel> getArtikelbyWarengruppeId(int id)
        {
            return db.ArtikelSet.Where((a) => a.WarengruppeId == id).ToList();
        }

        public List<Artikel> getArtikelfromBelegId(int id)
        {
            var buff = db.ArtikelBelegSet.Where((ab) => ab.BelegId == id).ToList();
            List<Artikel> alist = new List<Artikel>();
            buff.ForEach(x => alist.Add(db.ArtikelSet.Find(x.ArtikelId)));
            return alist;
        }

        public List<Artikel> getArtikelList()
        {
            return db.ArtikelSet.ToList();
        }

        public Beleg getBelegbyId(int id)
        {
            return db.BelegSet.Find(id);
        }

        public List<Beleg> getBelegfromArtikelId(int id)
        {
            var buff = db.ArtikelBelegSet.Where((ab) => ab.ArtikelId == id).ToList();
            List<Beleg> alist = new List<Beleg>();
            buff.ForEach(x => alist.Add(db.BelegSet.Find(x.ArtikelId)));
            return alist;
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

        public List<Recht> getRechtfromRolleId(int id)
        {
            var buff = db.RolleRechtSet.Where((ab) => ab.RolleId == id).ToList();
            List<Recht> alist = new List<Recht>();
            buff.ForEach(x => alist.Add(db.RechtSet.Find(x.RechtId)));
            return alist;
        }

        public Rolle getRollebyId(int id)
        {
            return db.RolleSet.Find(id);
        }

        public List<Rolle> getRollefromRechtId(int id)
        {
            var buff = db.RolleRechtSet.Where((ab) => ab.RechtId == id).ToList();
            List<Rolle> alist = new List<Rolle>();
            buff.ForEach(x => alist.Add(db.RolleSet.Find(x.RolleId)));
            return alist;
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

        public void setBeleg(Beleg beleg)
        {
            db.BelegSet.Add(beleg);
            db.SaveChanges();

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

        public void setPrognose(Prognose prognose)
        {
            db.PrognoseSet.Add(prognose);
            db.SaveChanges();
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

        public void setSchicht(Schicht schicht)
        {
            db.SchichtSet.Add(schicht);
            db.SaveChanges();

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
            /*db.Users.Attach(updatedUser);
            var entry = db.Entry(updatedUser);
            entry.Property(e => e.Email).IsModified = true;
            // other changed properties
            db.SaveChanges();*/
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

    }
}
