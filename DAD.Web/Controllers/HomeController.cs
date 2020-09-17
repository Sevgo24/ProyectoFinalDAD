using DAD.BusinessLogic.ExternalAgent;
using DAD.BusinessLogic.Implementation;
using System.Web.Mvc;

namespace DAD.Web.Controllers
{
    public class HomeController : Controller
    {
        AlumnoBL alumnoBL = new AlumnoBL();
        RespuestaBL respuestaBL = new RespuestaBL();
        DimensionBL dimensionBL = new DimensionBL();
        ConnectionApiExcel apiExcel = new ConnectionApiExcel();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string CODALUM)
        {
            var list = alumnoBL.ListaUsuarioBandejaCms(CODALUM);
            return View(list);
        }

        public ActionResult ActualizarBaseDatos()
        {
            apiExcel.Excel();

            return RedirectToAction("Index");
        }
        public ActionResult Respuesta()
        {
            ViewBag.ListaDimension = dimensionBL.ListarDimensionCms();
            return View();
        }
        [HttpPost]
        public ActionResult Respuesta(string CODALUM, int dimensiones)
        {
            if (CODALUM == null)
            {
                CODALUM = "";
            }
            var lista = respuestaBL.ListarRespuestaBandejaCms(CODALUM, dimensiones);
            ViewBag.ListaDimension = dimensionBL.ListarDimensionCms();
            return View(lista);
        }
    }
}