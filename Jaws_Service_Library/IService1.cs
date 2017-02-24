using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;

namespace Jaws_Service_Library
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IService1" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        //Personal

        [OperationContract]
        Personal getPersonalbyId(int id);

        [OperationContract]
        void setPersonal(Personal person);

        [OperationContract]
        void updatePersonal(Personal personal);

        [OperationContract]
        List<Personal> getPersonalList();

        //Arbeitsvertrag

        [OperationContract]
        Arbeitsvertrag getArbeitsvertragbyId(int id);

        [OperationContract]
        void setArbeitsvertrag(Arbeitsvertrag vertrag);

        [OperationContract]
        void updateArbeitsvertrag(Arbeitsvertrag arbeitsvertrag);

        [OperationContract]
        List<Arbeitsvertrag> getArbeitsvertragList();

        //Rolle

        [OperationContract]
        List<Rolle> getRollebyId(int id);

        [OperationContract]
        void setRolle(Rolle role);

        [OperationContract]
        void updateRolle(Rolle rolle);

        [OperationContract]
        List<Rolle> getRolleList();

        [OperationContract]
        List<Rolle> getRollebyPersonalId(int id);

        //Recht

        [OperationContract]
        List<Recht> getRechtbyId(int id);

        [OperationContract]
        void setRecht(Recht recht);

        [OperationContract]
        void updateRecht(Recht recht);

        [OperationContract]
        List<Recht> getRechtbyRolleId(int id);

        //Schicht

        [OperationContract]
        List<Schicht> getSchichtbyId(int id);

        [OperationContract]
        void setSchicht(Schicht schicht);

        [OperationContract]
        void updateSchicht(Schicht schicht);

        [OperationContract]
        List<Schicht> getSchichtbyPersonalId(int id);

        //Lieferant

        [OperationContract]
        Lieferant getLieferantbyId(int id);

        [OperationContract]
        void setLieferant(Lieferant lieferant);

        [OperationContract]
        void updateLieferant(Lieferant lieferant);

        [OperationContract]
        List<Lieferant> getLieferantList();

        [OperationContract]
        Lieferant getLieferantbyArtikelId(int id);

        //Artikel

        [OperationContract]
        Artikel getArtikelbyId(int id);

        [OperationContract]
        void setArtikel(Artikel artikel);

        [OperationContract]
        void updateArtikel(Artikel artikel);

        [OperationContract]
        List<Artikel> getArtikelList();

        [OperationContract]
        List<Artikel> getArtikelbyLieferantId(int id);

        [OperationContract]
        List<Artikel> getArtikelbyWarengruppeId(int id);

        [OperationContract]
        List<Artikel> getArtikelbyBelegId(int id);

        //Warengruppe

        [OperationContract]
        Warengruppe getWarengruppebyId(int id);

        [OperationContract]
        void setWarengruppe(Warengruppe gruppe);

        [OperationContract]
        void updateWarengruppe(Warengruppe warengruppe);

        [OperationContract]
        List<Warengruppe> getWarengruppeList();

        [OperationContract]
        Warengruppe getWarengruppebyName(String name);

        //Prognose

        [OperationContract]
        Prognose getPrognosebyId(int id);

        [OperationContract]
        void setPrognose(Prognose prognose);

        [OperationContract]
        void updatePrognose(Prognose prognose);

        [OperationContract]
        List<Prognose> getPrognoseList();

        [OperationContract]
        Prognose getPrognosebyArtikelId(int id);

        [OperationContract]
        List<Prognose> getPrognosebyDate(DateTime date);

        //Beleg

        [OperationContract]
        Beleg getBelegbyId(int id);

        [OperationContract]
        void setBeleg(Beleg beleg);

        [OperationContract]
        void updateBeleg(Beleg beleg);

        [OperationContract]
        List<Beleg> getBelegList();

        [OperationContract]
        List<Beleg> getBelegbyArtikelId(int id);

        //Lieferart

        [OperationContract]
        Lieferart getLieferartbyId(int id);

        [OperationContract]
        void setLieferart(Lieferart lieferart);

        [OperationContract]
        void updateLieferart(Lieferart lieferart);

        [OperationContract]
        List<Lieferart> getLieferartList();



        // TODO: Hier Dienstvorgänge hinzufügen
    }

}
