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
    public class TeamsController : Controller
    {
        private WorldCupDbContext db = new WorldCupDbContext();

        // GET: Teams
        public ActionResult Index()
        {
            return View(db.Teams.Include(x => x.Continent).ToList());
        }

        // GET: Teams/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Continents = db.Continents.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,TeamName,SelectedContinentId")] Team team)
        {
            if (ModelState.IsValid)
            {
                team.Continent = db.Continents.Find(team.SelectedContinentId);
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Include(x => x.Continent).First(x => x.TeamId == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            team.SelectedContinentId = team.Continent.ContinentId;
            ViewBag.Continents = db.Continents.ToList();
            return View(team);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,TeamName,SelectedContinentId")] Team team)
        {
            if (ModelState.IsValid)
            {
                //team.Continent = db.Continents.Find(team.SelectedContinentId);
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                var dbTaem = db.Teams.Include(x => x.Continent).FirstOrDefault(x => x.TeamId == team.TeamId);
                if (dbTaem != null && dbTaem.Continent.ContinentId != team.SelectedContinentId)
                {
                    var oldContinent = db.Continents.Include(x => x.Teams).FirstOrDefault(x => x.ContinentId == dbTaem.Continent.ContinentId);

                    oldContinent?.Teams.Remove(dbTaem);

                    var newContinent = db.Continents.Include(x => x.Teams).FirstOrDefault(x => x.ContinentId == team.SelectedContinentId);

                    newContinent?.Teams.Add(dbTaem);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
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
