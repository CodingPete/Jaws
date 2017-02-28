using Schichtplaner.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Schichtplaner.Controllers
{
    public class LoginController : Controller
    {
        private ServiceReference1.Service1Client client;
        private Recht admin;

        public LoginController()
        {
            // Verbindungsaufbau zum Jaws_Server
            client = new ServiceReference1.Service1Client();
            admin = client.getRechtbyName("Admin");

        }
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Username")] String temp) //TODO: Bind Ersetzen, keine Ahnung was man da machen kann
        {
            var post = Request.Form;
            String username = post["Username"];
            String password = post["Password"];
            Personal person = client.getPersonalbyEmail(username);

            FormsAuthentication.RedirectFromLoginPage(post["Username"], true);
            if (person != null && person.passwort == password)
            {
                Rolle role = client.getRollebyId(person.RolleId);
                Boolean isAdmin = false;
                var rechte = client.getRechtbyRolleId(role.Id);

                if (rechte.Contains(admin))
                {
                    isAdmin = true;
                }

                FormsAuthentication.RedirectFromLoginPage(person.Vorname+" "+person.Name, false);
            
                Session["Email"] = person.email;
                Session["Rolle"] = role.Name;
                Session["isAdmin"] = isAdmin;
            }
            return RedirectToAction("Index", "Plan");
        }
    }
}