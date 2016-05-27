using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp_OpenIDConnect_DotNet_B2C.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Index()
        {

            return RedirectToAction("Index", "Home");
        }
    }
}