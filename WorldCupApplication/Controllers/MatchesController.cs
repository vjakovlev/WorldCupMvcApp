using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldCupApplication.Models;
using PagedList;
using PagedList.Mvc;

namespace WorldCupApplication.Controllers
{
    public class MatchesController : Controller
    {
        private WorldCupDbContext db = new WorldCupDbContext();

        // GET: Matches
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (searchBy == "TeamA")
            {
                return View(db.Matches.Include(x => x.TeamA).Include(x => x.TeamB).Where(x => x.TeamA.TeamName.Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }
            else
            {
                return View(db.Matches.Include(x => x.TeamA).Include(x => x.TeamB).Where(x => x.TeamB.TeamName.Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }
        }

        // GET: Matches/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TeamA = db.Teams.ToList();
            ViewBag.TeamB = db.Teams.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatchId,MatchDateTime,SelectedTeamAId,SelectedTeamBId")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.TeamA = db.Teams.Find(match.SelectedTeamAId);
                match.TeamB = db.Teams.Find(match.SelectedTeamBId);
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(match);
        }

        // GET: Matches/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Include(x => x.TeamA).Include(x => x.TeamB).First(x => x.MatchId == id);
            if (match == null)
            {
                return HttpNotFound();
            }

            match.SelectedTeamAId = match.TeamA.TeamId;
            match.SelectedTeamBId = match.TeamB.TeamId;
            ViewBag.TeamA = db.Teams.ToList();
            ViewBag.TeamB = db.Teams.ToList();

            return View(match);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatchId,MatchDateTime")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: Matches/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
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
