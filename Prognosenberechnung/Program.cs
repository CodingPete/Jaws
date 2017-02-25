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
            prognosen_timer = new Timer(callback, null, 0, 2000);
            Console.ReadLine();
        }

        private static void callback(object state)
        {
            // Alle Artikel holen
            var artikels = client.getArtikelList();

            // ...für jeden Artikel
            foreach(Artikel artikel in artikels)
            {
                // ... hole die Abverkaufsbelege der letzen 6 Wochen
                DateTime heute = new DateTime();
                DateTime damals = heute.AddDays(-(6 * 7));
                

            }
        }
        
    }
}
