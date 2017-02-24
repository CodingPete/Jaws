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

        [OperationContract]
        Artikel getArtikel(int value);
       

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Hier Dienstvorgänge hinzufügen
    }

    // Verwenden Sie einen Datenvertrag, wie im folgenden Beispiel dargestellt, um Dienstvorgängen zusammengesetzte Typen hinzuzufügen.
    // Sie können im Projekt XSD-Dateien hinzufügen. Sie können nach dem Erstellen des Projekts dort definierte Datentypen über den Namespace "Jaws_Service_Library.ContractType" direkt verwenden.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
