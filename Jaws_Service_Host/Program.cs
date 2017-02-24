using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jaws_Service_Library;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Jaws_Service_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri adresse = new Uri("http://localhost:1337");
            ServiceHost host = new ServiceHost(typeof(Service1), adresse);

            try
            {
                host.AddServiceEndpoint(typeof(IService1), new WSHttpBinding(), "Jaws_Service");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);

                host.Open();

                Console.Write(""+
    "\t\t888888        d8888 888       888.d8888b.\n" +
    "\t\t   88b       d88888 888   o   888 d88P  Y88b \n" +
    "\t\t   888      d88P888 888  d8b  888 Y88b.\n" +
    "\t\t   888     d88P 888 888 d888b 888  Y888b.\n" +
    "\t\t   888    d88P  888 888d88888b888     Y88b. \n" +
    "\t\t   888   d88P   888 88888P Y88888       888 \n" +
    "\t\t   88P  d8888888888 8888P   Y8888 Y88b  d88P\n" +
    "\t\t   888 d88P     888 888P     Y888  Y8888P\n" +
    "\t\t  .d88P\n" +
    "\t\t.d88P\n" +
    "\t\t888P\n");

                Console.ReadLine();
                host.Close();
            } 
            catch(CommunicationException ce)
            {
                Console.WriteLine("Ausnahme: {0}", ce.Message);
                host.Abort();
            }

        }
    }
}
