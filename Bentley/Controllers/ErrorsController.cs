using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Bentley.Controllers
{
    public class ErrorsController : Controller
    {

        //
        // GET: /Errors/

        public ViewResult Index(string error)
        {
            //object Error = error;
            ViewData["error"] = Session["error"] != null ? Session["error"].ToString() : "";
            return View("Index");
            //return RedirectToAction("Index", "Vehicles");
        }

    }
}
