//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Personal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personal()
        {
            this.Schicht = new HashSet<Schicht>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Straße { get; set; }
        public int Hausnummer { get; set; }
        public string Zusatz { get; set; }
        public int Postleitzahl { get; set; }
        public string Ort { get; set; }
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public int Steuerklasse { get; set; }
        public string Telefon { get; set; }
        public string Mobil { get; set; }
        public int ArbeitsvertragId { get; set; }
        public int RolleId { get; set; }
        public string email { get; set; }
        public string passwort { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schicht> Schicht { get; set; }
        public virtual Arbeitsvertrag Arbeitsvertrag { get; set; }
        public virtual Rolle Rolle { get; set; }
    }
}
