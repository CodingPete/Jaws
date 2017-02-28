using DAL;
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
            Personal person = null;
            try
            {
                person = db.PersonalSet.First((x) => x.email == username);

            }catch(Exception e)
            {
                person = null;
            }
            FormsAuthentication.RedirectFromLoginPage(post["Username"], true);
            if (person != null && person.passwort==password)
            {
                FormsAuthentication.RedirectFromLoginPage(person.Vorname+" "+person.Name, false);
                //create a cookie
                Session["Rolle"] = person.Rolle.Name;
                Session["Email"] = person.email;

            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-1);
            Response.Cookies["UserInformation"].Expires = DateTime.Now.AddYears(-1);
            return RedirectToAction("Index", "Home");
        }
    }
}