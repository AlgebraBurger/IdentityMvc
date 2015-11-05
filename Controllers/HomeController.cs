using CrmCoreLite.Infrastructure;
using CrmCoreLite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CrmCoreLite.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Summary()
        {
            return View();
        }

        public ActionResult Ajax()
        {
            var countries = new List<SearchTypeAheadEntity>
            {
                new SearchTypeAheadEntity {ShortCode = "US", Name = "United States"},
                new SearchTypeAheadEntity {ShortCode = "CA", Name = "Canada"},
                new SearchTypeAheadEntity {ShortCode = "AF", Name = "Afghanistan"},
                new SearchTypeAheadEntity {ShortCode = "AL", Name = "Albania"},
                new SearchTypeAheadEntity {ShortCode = "DZ", Name = "Algeria"},
                new SearchTypeAheadEntity {ShortCode = "DS", Name = "American Samoa"},
                new SearchTypeAheadEntity {ShortCode = "AD", Name = "Andorra"},
                new SearchTypeAheadEntity {ShortCode = "AO", Name = "Angola"},
                new SearchTypeAheadEntity {ShortCode = "AI", Name = "Anguilla"},
                new SearchTypeAheadEntity {ShortCode = "AQ", Name = "Antarctica"},
                new SearchTypeAheadEntity {ShortCode = "AG", Name = "Antigua and/or Barbuda"}
            };

            return Json(countries, JsonRequestBehavior.AllowGet);
        }

    }

    public class SearchTypeAheadEntity
    {
        public string ShortCode { get; set; }
        public string Name { get; set; }
    }


}