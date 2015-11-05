using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrmCoreLite.Areas.Orders.Models;
using CrmCoreLite.Infrastructure;
using CrmCoreLite.Areas.Billings.Models;

namespace CrmCoreLite.Areas.Orders.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private CrmCore db = new CrmCore();
        private CrmCoreApplicationHelpers crmHelper = new CrmCoreApplicationHelpers();
        // GET: Orders/Orders
        public ActionResult Index(string OrderNo)
        {
            var orderList = db.Orders.Include(o => o.Agent).Include(o => o.Customer);

            if (!String.IsNullOrEmpty(OrderNo))
            {
                orderList = orderList.Where(x => x.OrderNo.Contains(OrderNo));
            }

            return View(orderList);
        }

        // GET: Orders/Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Orders/Create
        public ActionResult Create()
        {
            ViewBag.AgentID = new SelectList(db.Agents, "ID", "FullName");
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName");
            return View();
        }

        // POST: Orders/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,TotalOrderAmount,PaymentType,ContractStatus,CustomerID,AgentID,ReservationFee,DateCreated")] Order order)
        {
            if (ModelState.IsValid)
            {
                var now = DateTime.Now;
                order.DateCreated = now;
                db.Orders.Add(order);
                var monthlyBill = crmHelper.CalculateMonthlyBill((PaymentType)order.PaymentType, order.TotalOrderAmount);
                var numberOfMonth = crmHelper.GetNumberOFMonths((PaymentType)order.PaymentType);
                await db.SaveChangesAsync();

                order.OrderNo = crmHelper.generateSequenceNumber(crmHelper.getPrefix(SerialType.ORDER),order.OrderID);
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();


                for (var i = 1;i<=numberOfMonth;i++)
                {

                    var billnow = DateTime.Now;
                    var newBill = new Bill
                    {
                        OrderID = order.OrderID,
                        CustomerID = order.CustomerID,
                        BillAmount = monthlyBill,
                        BillNo = i.ToString(),
                        BillDue = now.AddMonths(i),
                        DateCreated = billnow,
                    };
                    db.Bills.Add(newBill);
                    await db.SaveChangesAsync();

                    newBill.BillCode = crmHelper.generateSequenceNumber(crmHelper.getPrefix(SerialType.BILL), newBill.ID);
                    db.Entry(newBill).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }


                return RedirectToAction("Index");
            }

            ViewBag.AgentID = new SelectList(db.Agents, "ID", "FullName", order.AgentID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentID = new SelectList(db.Agents, "ID", "FullName", order.AgentID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", order.CustomerID);
            return View(order);
        }

        // POST: Orders/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,OrderNo,TotalOrderAmount,PaymentType,ContractStatus,CustomerID,AgentID,ReservationFee,DateCreated")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AgentID = new SelectList(db.Agents, "ID", "FullName", order.AgentID);
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "FullName", order.CustomerID);
            return View(order);
        }

        // GET: Orders/Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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
