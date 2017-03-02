using DAL;
using Dashboard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Dashboard.Controllers
{
    public class LoginController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Login
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
            
            if (new MyMembershipProvider().ValidateUser(username,password))
            {
                Personal person = db.PersonalSet.First((x) => x.email == username);
                FormsAuthentication.RedirectFromLoginPage(person.email, false);
                //create a Session cookie
                Session["Name"] = person.Name;
                Session["Vorname"] = person.Vorname;
                Session["Rolle"] = person.Rolle.Name;

            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}