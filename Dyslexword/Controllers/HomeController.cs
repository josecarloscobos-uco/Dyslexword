using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dyslexword.Controllers
{
    public class HomeController : Controller
    {

        // Pagina del home
        public ActionResult Index(string coins)
        {
            return View();
        }

        // Pagina de acerca de 
        public ActionResult About()
        {

            return View();
        }
    }
}