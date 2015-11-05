using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrmCoreLite.Areas.Settings.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        // GET: Settings/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}