using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Dashboard.Controllers
{
    public class RolleController : Controller
    {
        private DataContainer db = new DataContainer();

        // GET: Rolle
        public ActionResult Index()
        {
            return View(db.RolleSet.ToList());
        }

        // GET: Rolle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rolle rolle = db.RolleSet.Find(id);
            if (rolle == null)
            {
                return HttpNotFound();
            }
            return View(rolle);
        }

        // GET: Rolle/Create
        public ActionResult Create()
        {
            ViewBag.Rechte=db.RechtSet.ToList();
            return View();
        }

        // POST: Rolle/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Rolle rolle)
        {
            
            if (ModelState.IsValid)
            {
                db.RolleSet.Add(rolle);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }


                var Rechte = db.RechtSet.ToList();

                var post = Request.Form;

                Rechte.ForEach(item =>
                {
                    if (post[item.Name] != null)
                    {
                        RolleRecht nRolleRecht = new RolleRecht();
                        nRolleRecht.RechtId = item.Id;
                        nRolleRecht.RolleId = rolle.Id;
                        db.RolleRechtSet.Add(nRolleRecht);

                    }
                });
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
                return RedirectToAction("Index");

            }

            return View(rolle);
        }

        // GET: Rolle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rolle rolle = db.RolleSet.Find(id);
            if (rolle == null)
            {
                return HttpNotFound();
            }
            var userRights = (from a in db.RechtSet
                       join c in db.RolleRechtSet on a.Id equals c.RechtId
                       where c.RolleId == id
                       select a).ToList();

            ViewBag.UserRechte = userRights;
            ViewBag.Rechte = db.RechtSet.ToList();
            return View(rolle);
        }

        // POST: Rolle/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Rolle rolle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolle).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }

                var Rechte = db.RechtSet.ToList();

                var test = Request.Form;

                var userRights = (from a in db.RechtSet
                                  join c in db.RolleRechtSet on a.Id equals c.RechtId
                                  where c.RolleId == rolle.Id
                                  select a).ToList();


                Rechte.ForEach(item =>
                {
                    if (test[item.Name] == null && userRights.Contains(item))
                    { //Wurden einem User Rechte entzogen?
                        var temp = db.RolleRechtSet.Where((x) => x.RechtId == item.Id && x.RolleId==rolle.Id).ToArray()[0];
                        db.RolleRechtSet.Remove(temp);
                    }else
                    {
                        if (test[item.Name] != null && !userRights.Contains(item))
                        {
                            RolleRecht nRolleRecht = new RolleRecht();
                            nRolleRecht.RechtId = item.Id;
                            nRolleRecht.RolleId = rolle.Id;
                            db.RolleRechtSet.Add(nRolleRecht);
                        }
                       
                    }
                });
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
                return RedirectToAction("Index");
            }
            return View(rolle);
        }

        // GET: Rolle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rolle rolle = db.RolleSet.Find(id);
            if (rolle == null)
            {
                return HttpNotFound();
            }
            return View(rolle);
        }

        // POST: Rolle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var rollerecht = db.RolleRechtSet.ToList();
            rollerecht.ForEach(item =>
            {
                if(item.RolleId== id)
                {
                    rollerecht.Remove(item);
                }
            });
            Rolle rolle = db.RolleSet.Find(id);
            db.RolleSet.Remove(rolle);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
