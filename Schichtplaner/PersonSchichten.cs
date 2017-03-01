using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schichtplaner.ServiceReference1;

namespace Schichtplaner
{
    public class PersonSchichten
    {
        public PersonSchichten()
        {

        }
        public PersonSchichten(Personal p, Arbeitsvertrag a, Schicht[] s)
        {
            this.person = p;
            this.vertrag = a;
            this.schichten = s;
        }

        public Personal person { get; set; }
        public Arbeitsvertrag vertrag { get; set; }
        public Schicht[] schichten { get; set; }
    }
}