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
            throw new NotImplementedException();
        }

        public List<Arbeitsvertrag> getArbeitsvertragList()
        {
            throw new NotImplementedException();
        }

        public Artikel getArtikel(int value)
        {
            
            Artikel artikel = db.ArtikelSet.Find(value);
            return artikel;
            throw new NotImplementedException();
        }

        public List<Artikel> getArtikelbyBelegId(int id)
        {
            throw new NotImplementedException();
        }

        public Artikel getArtikelbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Artikel> getArtikelbyLieferantId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Artikel> getArtikelbyWarengruppeId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Artikel> getArtikelList()
        {
            throw new NotImplementedException();
        }

        public List<Beleg> getBelegbyArtikelId(int id)
        {
            throw new NotImplementedException();
        }

        public Beleg getBelegbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Beleg> getBelegList()
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public Lieferant getLieferantbyArtikelId(int id)
        {
            throw new NotImplementedException();
        }

        public Lieferant getLieferantbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Lieferant> getLieferantList()
        {
            throw new NotImplementedException();
        }

        public Lieferart getLieferartbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Lieferart> getLieferartList()
        {
            throw new NotImplementedException();
        }

        public Personal getPersonalbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Personal> getPersonalList()
        {
            throw new NotImplementedException();
        }

        public Prognose getPrognosebyArtikelId(int id)
        {
            throw new NotImplementedException();
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

        public List<Recht> getRechtbyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recht> getRechtbyRolleId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rolle> getRollebyId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rolle> getRollebyPersonalId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rolle> getRolleList()
        {
            throw new NotImplementedException();
        }

        public List<Schicht> getSchichtbyId(int id)
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
