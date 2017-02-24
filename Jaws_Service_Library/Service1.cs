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
        public Artikel getArtikel(int value)
        {
            
            Artikel artikel = db.ArtikelSet.Find(value);
            return artikel;
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
