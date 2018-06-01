using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WorldCupApplication.Models;

namespace WorldCupApplication.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult Users()
        {
            using (WorldCupDbContext db = new WorldCupDbContext())
            {
                return View(db.UserAccount.ToList());
            }
        }

        //Rgister
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (WorldCupDbContext db = new WorldCupDbContext())
                {
                    db.UserAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.Firstname + " " + account.LastName + " successfully registered. ";
            }
            return View();
        }

        //LogIn
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (WorldCupDbContext db = new WorldCupDbContext())
            {
                var usr = db.UserAccount.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();

                    //TODO: Nema potreba toString na string property
                    Session["Username"] = usr.Username.ToString();
                    Session["Firstname"] = usr.Firstname.ToString();
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
            }
            return View();
        }

        //LoggedIn
        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //LogOut
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //redirect
        public ActionResult NotAdmin()
        {
            return View();
        }

        //already logged
        public ActionResult AlreadyLogged()
        {
            return View();
        }

        //already registered
        public ActionResult AlreadyRegistered()
        {
            return View();
        }
    }
}