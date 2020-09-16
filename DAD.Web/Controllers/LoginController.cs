using DAD.BusinessLogic.ExternalAgent;
using DAD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAD.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (login.usuario == "admin" && login.password == "admin")
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}