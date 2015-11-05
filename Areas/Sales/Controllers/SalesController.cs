using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrmCoreLite.Areas.Sales.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        // GET: Sales/Sales
        public ActionResult Index()
        {
            return View();
        }
    }
}