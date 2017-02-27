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
        public ActionResult Index([Bind(Include = "Id,Name")] Rolle rolle) //TODO: Bind Ersetzen, keine Ahnung was man da machen kann
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
                HttpCookie myCookie = new HttpCookie("UserInformation");

                //Add key-values in the cookie
                myCookie.Values.Add("Rolle", person.Rolle.Name);
                myCookie.Values.Add("Email", person.email);
                //Most important, write the cookie to client.
                Response.Cookies.Add(myCookie);
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