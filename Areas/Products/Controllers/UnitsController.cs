using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrmCoreLite.Areas.Units.Models;
using CrmCoreLite.Infrastructure;

namespace CrmCoreLite.Areas.Products.Controllers
{
    [Authorize]
    public class UnitsController : Controller
    {
        private CrmCore db = new CrmCore();

        // GET: Products/Units
    
        public ActionResult Index(string UnitNo, string uType, string vType, string uStatus)
        {
            var unitList = from u in db.Units
                           select u;
            if (!String.IsNullOrEmpty(UnitNo))
            {
                unitList = unitList.Where(x => x.UnitNo.Contains(UnitNo));
            }

            if (!String.IsNullOrEmpty(uType))
            {
                var u = (UnitType)Enum.Parse(typeof(UnitType), uType);
                unitList = unitList.Where(x => x.UnitType == u);
            }
            if (!String.IsNullOrEmpty(vType))
            {
                var u = (uViewType)Enum.Parse(typeof(uViewType), vType);
                unitList = unitList.Where(x => x.uViewType == u);
            }
            if (!String.IsNullOrEmpty(uStatus))
            {
                var u = (UnitStatus)Enum.Parse(typeof(UnitStatus), uStatus);
                unitList = unitList.Where(x => x.UnitStatus == u);
            }


            var uDDL = Enum.GetValues(typeof(UnitType)).Cast<UnitType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            ViewBag.uType = uDDL.ToList();


            var vDDL = Enum.GetValues(typeof(uViewType)).Cast<uViewType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            ViewBag.vType = vDDL.ToList();

            var usDDL = Enum.GetValues(typeof(UnitStatus)).Cast<UnitStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            ViewBag.uStatus = usDDL.ToList();


            

            return View(unitList);
        }

        // GET: Products/Units/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Products/Units/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ProductNo,UnitNo,UnitType,uViewType,UnitArea,Balcony,TotalArea,TotalListPrice,TransferMiscFees,TotalContractPrice,UnitStatus")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(unit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(unit);
        }

        // GET: Products/Units/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Products/Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ProductNo,UnitNo,UnitType,uViewType,UnitArea,Balcony,TotalArea,TotalListPrice,TransferMiscFees,TotalContractPrice,UnitStatus")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unit);
        }

        // GET: Products/Units/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Units.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Products/Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Unit unit = await db.Units.FindAsync(id);
            db.Products.Remove(unit);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
