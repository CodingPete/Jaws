using Schichtplaner.Security;
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
            if (new MyMembershipProvider().ValidateUser(username, password))
            {
                FormsAuthentication.RedirectFromLoginPage(username, false);
                var person = client.getPersonalbyEmail(username);
                Session["Name"] = person.Name;
                Session["Vorname"] = person.Vorname;

                return RedirectToAction("Schichtplan", "Home");

            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}