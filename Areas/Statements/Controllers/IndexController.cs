using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrmCoreLite.Areas.Statements.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        // GET: Statements/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}