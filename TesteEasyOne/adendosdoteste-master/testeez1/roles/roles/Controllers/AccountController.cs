using DAL.Entity;
using DAL.Persistence;
using Newtonsoft.Json;
using roles.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace roles.Controllers {
    public class AccountController : Controller {

        public ActionResult Index() {
            return View();
        }
        
        public ActionResult LogOff() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public ActionResult LogOn(LogOnModel model) {
            UserDal uDal = new UserDal();
            if (ModelState.IsValid) {
                if (uDal.validateUser(model.UserName, model.Password)) {
                    var user = SetupFormsAuthTicket(model.UserName, true);
                    // -- Snip --
                    Session.Add("nome", model.UserName);
                    if (user != null && user.UserRoles.Any(x => x.RoleId == 1)) {
                        return RedirectToAction("Index", "Admin");

                    }
                    else if(user != null && user.UserRoles.Any(x => x.RoleId == 2)) {
                        Paciente pas = new Paciente();
                        User u = uDal.GetUser(model.UserName, model.Password);
                        //PacienteDal pDal = new PacienteDal();
                        //pas = pDal.GetOneToSession(u.UserId);
                        //TempData["paciente"] = "entrou";
                        //TempData["idPaciente"] = pas.Id;
                        //Session.Add("Paciente", pas);
                            return RedirectToAction("Index", "Paciente");
                    }

                }
                ModelState.AddModelError("",
                  "The user name or password provided is incorrect.");
            }
            ViewBag.Errinho = "login ou senha incorretos";
            return RedirectToAction("Index", "Error");
        }

        // -- Snip --

        private User SetupFormsAuthTicket(string userName, bool persistanceFlag) {
            UserDal uDal = new UserDal();
            User user = uDal.GetUser(userName);

            var oUser = new LogOnModel() {
                IdPerson = user.IdPerson,
                UserId = user.UserId,
                UserName = user.UserName
            };

            var authTicket = new FormsAuthenticationTicket(1, //version
                                userName, // user name
                                DateTime.Now,             //creation
                                DateTime.Now.AddMinutes(30), //Expiration
                                persistanceFlag, //Persistent
                                JsonConvert.SerializeObject(oUser));

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));//Add cookie no browser
            return user;
        }

        [Authorize]
        public ActionResult AdminOrSuperAdmin() {
            if (!User.IsInRole("SuperAdmin") && !User.IsInRole("Admin")) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //[Authorize(Roles = "Admin, ")]


    }
}