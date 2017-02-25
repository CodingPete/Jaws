using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;

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
            throw new NotImplementedException();
        }

        public Prognose getPrognosebyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Prognose> getPrognoseList()
        {
            throw new NotImplementedException();
        }

        public Recht getRechtbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recht> getRechtfromRolleId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rolle> getRollebyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rolle> getRollefromRechtId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rolle> getRolleList()
        {
            throw new NotImplementedException();
        }

        public List<Schicht> getSchichtBetween(DateTime von, DateTime bis)
        {
            throw new NotImplementedException();
        }

        public Schicht getSchichtbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Schicht> getSchichtbyPersonalId(int id)
        {
            throw new NotImplementedException();
        }

        public Warengruppe getWarengruppebyId(int id)
        {
            throw new NotImplementedException();
        }

        public Warengruppe getWarengruppebyName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Warengruppe> getWarengruppeList()
        {
            throw new NotImplementedException();
        }

        public void setArbeitsvertrag(Arbeitsvertrag vertrag)
        {
            throw new NotImplementedException();
        }

        public void setArtikel(Artikel artikel)
        {
            throw new NotImplementedException();
        }

        public void setArtikelBeleg(Artikel artikel, Beleg beleg)
        {
            throw new NotImplementedException();
        }

        public void setBeleg(Beleg beleg)
        {
            throw new NotImplementedException();
        }

        public void setLieferant(Lieferant lieferant)
        {
            throw new NotImplementedException();
        }

        public void setLieferart(Lieferart lieferart)
        {
            throw new NotImplementedException();
        }

        public void setPersonal(Personal person)
        {
            throw new NotImplementedException();
        }

        public void setPrognose(Prognose prognose)
        {
            throw new NotImplementedException();
        }

        public void setRecht(Recht recht)
        {
            throw new NotImplementedException();
        }

        public void setRolle(Rolle role)
        {
            throw new NotImplementedException();
        }

        public void setRolleRecht(Rolle rolle, Recht recht)
        {
            throw new NotImplementedException();
        }

        public void setSchicht(Schicht schicht)
        {
            throw new NotImplementedException();
        }

        public void setWarengruppe(Warengruppe gruppe)
        {
            throw new NotImplementedException();
        }

        public void updateArbeitsvertrag(Arbeitsvertrag arbeitsvertrag)
        {
            throw new NotImplementedException();
        }

        public void updateArtikel(Artikel artikel)
        {
            throw new NotImplementedException();
        }

        public void updateArtikelBeleg(ArtikelBeleg artikelbeleg)
        {
            throw new NotImplementedException();
        }

        public void updateBeleg(Beleg beleg)
        {
            throw new NotImplementedException();
        }

        public void updateLieferant(Lieferant lieferant)
        {
            throw new NotImplementedException();
        }

        public void updateLieferart(Lieferart lieferart)
        {
            throw new NotImplementedException();
        }

        public void updatePersonal(Personal personal)
        {
            throw new NotImplementedException();
        }

        public void updatePrognose(Prognose prognose)
        {
            throw new NotImplementedException();
        }

        public void updateRecht(Recht recht)
        {
            throw new NotImplementedException();
        }

        public void updateRolle(Rolle rolle)
        {
            throw new NotImplementedException();
        }

        public void updateRolleRecht(RolleRecht rollerecht)
        {
            throw new NotImplementedException();
        }

        public void updateSchicht(Schicht schicht)
        {
            throw new NotImplementedException();
        }

        public void updateWarengruppe(Warengruppe warengruppe)
        {
            throw new NotImplementedException();
        }
    }
}
