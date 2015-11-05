using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrmCoreLite.Areas.Billings.Models;
using CrmCoreLite.Infrastructure;

namespace CrmCoreLite.Areas.Billings.Controllers
{
    [Authorize]
    public class BillsController : Controller
    {
        private CrmCore db = new CrmCore();

        // GET: Billings/Bills
        public ActionResult Index(string BillCode, string OrderNo, string billStatus, string FromDate, string ToDate)
        {
            var bsDDL = Enum.GetValues(typeof(BillStatus)).Cast<BillStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            });
            ViewBag.billStatus = bsDDL.ToList();

            var bills = db.Bills.Include(b => b.Customer).Include(b => b.Order);

            if (!String.IsNullOrEmpty(BillCode))
            {
                bills = bills.Where(x => x.BillCode.Contains(BillCode));
            }
            if (!String.IsNullOrEmpty(OrderNo))
            {
                bills = bills.Where(x => x.Order.OrderNo.Contains(OrderNo));
            }
           

            if (!String.IsNullOrEmpty(billStatus))
            {
                var u = (BillStatus)Enum.Parse(typeof(BillStatus), billStatus);
                bills = bills.Where(x => x.BillStatus == u);
            }

            
            if (!String.IsNullOrEmpty(FromDate))
            {
                var fDate = DateTime.Parse(FromDate);
                bills = bills.Where(x => x.Order.DateCreated > fDate);
            }
            if (!String.IsNullOrEmpty(ToDate))
            {
                var tDate = DateTime.Parse(ToDate);
                bills = bills.Where(x => x.Order.DateCreated < tDate);
            }


            bills = bills.OrderByDescending(x => x.DateCreated);


            return View(bills);
        }

        // GET: Billings/Bills/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = await db.Bills.FindAsync(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Billings/Bills/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName");
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderNo");
            return View();
        }

        // POST: Billings/Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,BillNo,BillAmount,OrderID,CustomerID,BillRemarks,BillDue,DateCreated,BillStatus,PaymentClass,PaymentAmount,PaymentCreated")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", bill.CustomerID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderNo", bill.OrderID);
            return View(bill);
        }

        // GET: Billings/Bills/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = await db.Bills.FindAsync(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Firstname", bill.CustomerID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderNo", bill.OrderID);
            return View(bill);
        }

        // POST: Billings/Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,BillCode,BillNo,BillAmount,OrderID,CustomerID,BillRemarks,BillDue, BillStatus,PaymentClass,PaymentSource,PaymentAmount,PaymentCreated")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.Entry(bill).Property(x => x.DateCreated).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", bill.CustomerID);
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderNo", bill.OrderID);
            return View(bill);
        }

        // GET: Billings/Bills/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = await db.Bills.FindAsync(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Billings/Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bill bill = await db.Bills.FindAsync(id);
            db.Bills.Remove(bill);
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
