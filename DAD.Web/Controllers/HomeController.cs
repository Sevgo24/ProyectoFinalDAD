using DAD.BusinessLogic.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAD.Web.Controllers
{
    public class HomeController : Controller
    {
        AlumnoBL alumnoBL = new AlumnoBL();
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(string codalum)
        {
            var list = alumnoBL.ListaUsuarioBandejaCms(codalum);
            return View(list);
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}