using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Schichtplaner.ServiceReference1;

namespace Schichtplaner
{
    public class PersonSchichten
    {
        public Personal person;
        public Arbeitsvertrag vertrag;
        public Schicht[] schichten;
    }
}