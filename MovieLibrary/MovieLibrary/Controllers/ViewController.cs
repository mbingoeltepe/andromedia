using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieLibrary.Controllers
{
    public class ViewController : Controller
    {
        //
        // GET: /View/

        public ActionResult HomeView()
        {
            return View();
        }

    }
}
