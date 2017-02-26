using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DAL;

namespace WCFServiceWebRole1
{
    // HINWEIS: Mit dem Befehl "Umbenennen" im Menü "Umgestalten" können Sie den Schnittstellennamen "IService1" sowohl im Code als auch in der Konfigurationsdatei ändern.
    [ServiceContract]
    public interface IService1
    {

        //Personal

        [OperationContract]
        Personal getPersonalbyId(int id);

        [OperationContract]
        void setPersonal(Personal person);

        [OperationContract]
        void updatePersonal(Personal personal);

        [OperationContract]
        List<Personal> getPersonalList();

        [OperationContract]
        List<Personal> getPersonalbyRolleId(int id);




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
        Rolle getRollebyId(int id);

        [OperationContract]
        void setRolle(Rolle role);

        [OperationContract]
        void updateRolle(Rolle rolle);

        [OperationContract]
        List<Rolle> getRolleList();


        //Recht

        [OperationContract]
        Recht getRechtbyId(int id);

        [OperationContract]
        void setRecht(Recht recht);

        [OperationContract]
        void updateRecht(Recht recht);


        //RolleRecht

        [OperationContract]
        void setRolleRecht(Rolle rolle, Recht recht);

        [OperationContract]
        void updateRolleRecht(RolleRecht rollerecht);

        [OperationContract]
        List<Rolle> getRollefromRechtId(int id);

        [OperationContract]
        List<Recht> getRechtfromRolleId(int id);



        //Schicht

        [OperationContract]
        Schicht getSchichtbyId(int id);

        [OperationContract]
        void setSchicht(Schicht schicht);

        [OperationContract]
        void updateSchicht(Schicht schicht);

        [OperationContract]
        List<Schicht> getSchichtbyPersonalId(int id);

        [OperationContract]
        List<Schicht> getSchichtBetween(DateTime von, DateTime bis);

        [OperationContract]
        List<Schicht> getSchichtByPersonalIdAndBetween(int id, DateTime von, DateTime bis);

        //Lieferant

        [OperationContract]
        Lieferant getLieferantbyId(int id);

        [OperationContract]
        void setLieferant(Lieferant lieferant);

        [OperationContract]
        void updateLieferant(Lieferant lieferant);

        [OperationContract]
        List<Lieferant> getLieferantList();


        //Artikel

        [OperationContract]
        Artikel getArtikelbyId(int id);

        [OperationContract]
        Artikel getArtikelByGTIN(String GTIN);

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
        int getArtikelCountByArtikelIdAndLieferartIdAndBetween(int artikel_id, int lieferart_id, DateTime von, DateTime bis);

        //ArtikelBeleg

        [OperationContract]
        void setArtikelBeleg(Artikel artikel, Beleg beleg);

        [OperationContract]
        void updateArtikelBeleg(ArtikelBeleg artikelbeleg);

        [OperationContract]
        List<Artikel> getArtikelfromBelegId(int id);

        [OperationContract]
        List<Beleg> getBelegfromArtikelId(int id);


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


        //Lieferart

        [OperationContract]
        Lieferart getLieferartbyId(int id);

        [OperationContract]
        Lieferart getLieferartByName(String name);

        [OperationContract]
        void setLieferart(Lieferart lieferart);

        [OperationContract]
        void updateLieferart(Lieferart lieferart);

        [OperationContract]
        List<Lieferart> getLieferartList();
    }
}
