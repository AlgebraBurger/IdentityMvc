using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrmCoreLite.Areas.Customers.Models;
using CrmCoreLite.Infrastructure;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using CrmCoreLite.Hubs;
using Microsoft.AspNet.SignalR;

namespace CrmCoreLite.Areas.Customers.Controllers
{
    [System.Web.Http.Authorize]
    public class CustomersController : Controller
    {
        private CrmCore db = new CrmCore();
        private CrmCoreApplicationHelpers crmHelper = new CrmCoreApplicationHelpers();

        // GET: Customers/Customers
        public ActionResult Index(string SearchName,string AccountNo, string Export)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SignalHub>();
            hubContext.Clients.All.broadMessage("From Controller");

            var customerList = from c in db.Customers
                               select c;

            if (!String.IsNullOrEmpty(SearchName))
            {

                ViewBag.SearchName = SearchName;
                //SearchName
                var SearchNames = SearchName.Split(' ').ToList();
                if (SearchNames.Count>1)
                {
                    if (SearchNames.Count>=3)
                    {
                        var fname = SearchNames[0].ToString();
                        var mname = SearchNames[1].ToString();
                        var lname = SearchNames[2].ToString();
                        customerList = customerList.Where(x => x.Firstname.Contains(fname) && x.Middlename.Contains(mname) && x.Lastname.Contains(lname));
                    }
                    else
                    {
                        var fname = SearchNames[0].ToString();                        
                        var lname = SearchNames[1].ToString();
                        customerList = customerList.Where(x => x.Firstname.Contains(fname) && x.Lastname.Contains(lname));
                    }

                    
                }
                else
                {
                    customerList = customerList.Where(x => x.Firstname.Contains(SearchName) || x.Middlename.Contains(SearchName) || x.Lastname.Contains(SearchName));
                }
                
            }

            if (!String.IsNullOrEmpty(AccountNo))
            {
                ViewBag.AccountNo = AccountNo;
                customerList = customerList.Where(x => x.AccountNo.Contains(AccountNo));
            }

            if (!String.IsNullOrEmpty(Export))
            {
                GridView gv = new GridView();
                gv.DataSource = customerList.ToList();
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=Customers" + DateTime.Now + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

            return View(customerList);
        }


        public JsonResult getCustomer(string SearchName)
        {
            var customerList = from c in db.Customers
                               select c;

            if (!String.IsNullOrEmpty(SearchName))
            {
                customerList = customerList.Where(x => x.Firstname.Contains(SearchName));
            }
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            gv.DataSource = db.Customers.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Customers"+ DateTime.Now +".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            

            return RedirectToAction("Index");
        }

        // GET: Customers/Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var billList = from b in db.Bills
                           select b;

            if (billList!=null)
            {
                billList = billList.Where(x => x.CustomerID==id);
            }
            
            ViewBag.BillList = billList;

            return View(customer);
        }

        // GET: Customers/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Firstname,Middlename,Lastname,EmailAddress,Telephone,Mobile,Birthplace,PermanentAddress,Nationality,CivilStatus,BirthDay,DateCreated,AccountNo,EmploymentStatus,CompanyName,NatureofBusiness,Occupation,CompanyAddress,CompanyTelephoneNumbers,YearsWithCompany,MonthlyIncome,TaxIdentificationNumber,Passport,ValidGovernmentID,PlaceIssue,DateIssue")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();

                var sequence = crmHelper.generateSequenceNumber(crmHelper.getPrefix(SerialType.ACCOUNT), customer.ID);
                customer.AccountNo = sequence;
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,AccountNo,Firstname,Middlename,Lastname,EmailAddress,Telephone,Mobile,Birthplace,PermanentAddress,Nationality,CivilStatus,BirthDay,DateCreated,AccountNo,EmploymentStatus,CompanyName,NatureofBusiness,Occupation,CompanyAddress,CompanyTelephoneNumbers,YearsWithCompany,MonthlyIncome,TaxIdentificationNumber,Passport,ValidGovernmentID,PlaceIssue,DateIssue")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
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
