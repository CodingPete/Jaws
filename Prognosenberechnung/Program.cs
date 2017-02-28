using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prognosenberechnung.ServiceReference1;
using System.Threading;

namespace Prognosenberechnung
{
    class Program
    {
        private static Timer prognosen_timer = null;
        private static Service1Client client = null;

        static void Main(string[] args)
        {
            client = new Service1Client();

            // Alle 24 Stunden die Prognosen aller Artikel berechnen
            prognosen_timer = new Timer(callback, null,  0, 24 * 3600000);
            Console.ReadLine();
        }

        private static void callback(object state)
        {
            // Hole die Abverkaufslieferart aus der Datenbank
            Lieferart lieferart = client.getLieferartByName("Verkauf");

            // Alle Artikel holen
            var artikels = client.getArtikelList();

            // ...für jeden Artikel
            foreach(Artikel artikel in artikels)
            {
                // ... hole die Abverkaufsbelege dieses Wochentages der letzen 6 Wochen
                int summe = 0;

                // Gestern in einer Woche. Davon 6 Wochen zurück
                DateTime Verkaufstag = DateTime.Now.AddDays(-1 -(7*6));
                
                // Gleitenden Mittelwert über die vergangenen 6 Wochen berechnen
                for (int i = 0; i < 7; i++ )
                {
                    Verkaufstag = Verkaufstag.AddDays(7);
                    var tagesVerkauf = client.getArtikelCountByArtikelIdAndLieferartIdAndBetween(artikel.Id, lieferart.Id, Verkaufstag, Verkaufstag.AddDays(1));
                    summe += tagesVerkauf;

                    // Wenn Gestern, dann Prognose holen falls vorhanden und Abverkauf_ist setzen
                    if(Verkaufstag.Date == DateTime.Now.AddDays(-1).Date)
                    {
                        Prognose old_prognose = client.getPrognoseByArtikelIdAndDate(artikel.Id, Verkaufstag);
                        if(old_prognose != null)
                        {
                            old_prognose.Abverkauf_ist = tagesVerkauf;
                            client.updatePrognose(old_prognose);
                        }
                    }
                }
                Double prognose_value = 1.0 / 6.0 * summe;

                // Prognose speichern
                Prognose prognose = new Prognose();
                prognose.ArtikelId = artikel.Id;
                prognose.Abverkauf_soll = prognose_value;
                prognose.Abverkauf_ist = 0;
                prognose.Datum = Verkaufstag.Date;
                client.setPrognose(prognose);
                
                Console.WriteLine(prognose.Abverkauf_soll);
                Console.ReadLine();

            }
        }
        
    }
}
