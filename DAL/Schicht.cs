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
    
    public partial class Schicht
    {
        public int Id { get; set; }
        public string Startzeit_soll { get; set; }
        public string Endzeit_soll { get; set; }
        public string Startzeit_ist { get; set; }
        public string Endzeit_ist { get; set; }
        public string Pause { get; set; }
        public int PersonalId { get; set; }
    
        public virtual Personal Personal { get; set; }
    }
}