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
            //prognosen_timer = new Timer(callback, null, 0, 2000);

            Lieferant lieferant = client.getLieferantbyId(1);
            Console.WriteLine(lieferant.Name);
            Console.ReadLine();
        }

        private static void callback(object state)
        {
            // Hole die Abverkaufslieferart aus der Datenbank
            Lieferart lieferart = client.getLieferartByName("Abverkauf");

            Console.WriteLine(lieferart.Name);

            // Alle Artikel holen
            var artikels = client.getArtikelList();

            // ...für jeden Artikel
            foreach(Artikel artikel in artikels)
            {
                // ... hole die Abverkaufsbelege dieses Wochentages der letzen 6 Wochen
                int summe = 0;

                // Morgen in einer Woche. Davon 6 Wochen zurück
                DateTime Verkaufstag = DateTime.Now.AddDays(1 + -(7*6));
                

                for (int i = 0; i < 7; i++ )
                {
                    Verkaufstag = Verkaufstag.AddDays(7);
                    summe += client.getArtikelCountByArtikelIdAndLieferartIdAndBetween(artikel.Id, lieferart.Id, Verkaufstag, Verkaufstag);
                }

                Double prognose_value = 1 / 6 * summe;

                
                Prognose prognose = new Prognose();
                prognose.ArtikelId = artikel.Id;
                prognose.Abverkauf_soll = prognose_value;
                prognose.Abverkauf_ist = 0;
                prognose.Datum = Verkaufstag.Date;

                client.setPrognose(prognose);
                
                Console.WriteLine("Prognose geschrieben");

            }
        }
        
    }
}
