using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DAD.Web.Models;

namespace DAD.Web.Controllers
{
    public class ApiExcelController : Controller
    {
        // GET: ApiExcel
        public ActionResult ApiExcel()
        {
            ApiExcel excel = new ApiExcel();
            excel.Excel();
            return View();
        }
    }
}