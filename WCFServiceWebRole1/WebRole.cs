using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WCFServiceWebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // Informationen zum Behandeln von Konfigurationsänderungen
            // finden Sie im MSDN-Thema unter https://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
