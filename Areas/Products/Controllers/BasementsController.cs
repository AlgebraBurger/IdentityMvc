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
    public class BasementsController : Controller
    {
        private CrmCore db = new CrmCore();

        // GET: Products/Basements
        public ActionResult Index(string UnitNo, string bType)
        {
            var basementList = from b in db.Basements
                               select b;

            if (!String.IsNullOrEmpty(UnitNo))
            {
                basementList = basementList.Where(x => x.UnitNo==UnitNo);
            }
            if (!String.IsNullOrEmpty(bType))
            {
                var u = (BaseNo)Enum.Parse(typeof(BaseNo), bType);
                basementList = basementList.Where(x => x.BaseNo == u);
            }

            var bDDL = Enum.GetValues(typeof(BaseNo)).Cast<BaseNo>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            ViewBag.bType = bDDL.ToList();

            return View(basementList);
        }

        // GET: Products/Basements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basement basement = await db.Basements.FindAsync(id);
            if (basement == null)
            {
                return HttpNotFound();
            }
            return View(basement);
        }

        // GET: Products/Basements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Basements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ProductNo,UnitNo,BaseNo,UnitArea,TotalContractPrice")] Basement basement)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(basement);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(basement);
        }

        // GET: Products/Basements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basement basement = await db.Basements.FindAsync(id);
            if (basement == null)
            {
                return HttpNotFound();
            }
            return View(basement);
        }

        // POST: Products/Basements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ProductNo,UnitNo,BaseNo,UnitArea,TotalContractPrice")] Basement basement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basement).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(basement);
        }

        // GET: Products/Basements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basement basement = await db.Basements.FindAsync(id);
            if (basement == null)
            {
                return HttpNotFound();
            }
            return View(basement);
        }

        // POST: Products/Basements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Basement basement = await db.Basements.FindAsync(id);
            db.Products.Remove(basement);
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
