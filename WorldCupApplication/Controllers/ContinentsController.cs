using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldCupApplication.Models;

namespace WorldCupApplication.Controllers
{
    // TODO: So ogled na toa sto kontinetite se fiksni nema potreba od strana za nivno manipuliranje, i dovolno e samo da ima model koj e vrzan so baza koja ima static data
    public class ContinentsController : Controller
    {
        private WorldCupDbContext db = new WorldCupDbContext();

        // GET: Continents
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Continents.ToList());
        }

        //TODO: Nepotrebena akcija, istata ne e implementirana
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return HttpNotFound();
            }
            return View(continent);
        }

        // GET: Continents/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContinentId,ContinentName")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                db.Continents.Add(continent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(continent);
        }

        // GET: Continents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return HttpNotFound();
            }
            return View(continent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContinentId,ContinentName")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(continent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(continent);
        }

        // GET: Continents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return HttpNotFound();
            }
            return View(continent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Continent continent = db.Continents.Find(id);
            db.Continents.Remove(continent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // Dispose
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
