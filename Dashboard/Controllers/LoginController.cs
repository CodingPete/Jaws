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
            var User = db.PersonalSet.Where((x) => x.Name.Equals(username));
            FormsAuthentication.RedirectFromLoginPage(post["Username"], true);
            if (User != null)
            {
                FormsAuthentication.RedirectFromLoginPage(post["Username"], true);
            }
            return RedirectToAction("Index","Home");
        }
    }
}