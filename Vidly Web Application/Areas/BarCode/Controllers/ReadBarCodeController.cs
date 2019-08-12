using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly_Web_Application.Areas.BarCode.Controllers
{
    public class ReadBarCodeController : Controller
    {
        // GET: BarCode/ReadBarCode
        public ActionResult Index()
        {
            return View();
        }
    }
}