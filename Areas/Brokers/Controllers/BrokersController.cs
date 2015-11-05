using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrmCoreLite.Areas.Brokers.Models;
using CrmCoreLite.Infrastructure;

namespace CrmCoreLite.Areas.Brokers.Controllers
{
    [Authorize]
    public class BrokersController : Controller
    {
        private CrmCore db = new CrmCore();

        // GET: Brokers/Brokers
        public ActionResult Index(string SearchName, string AgencyName)
        {
            var brokersList = from b in db.Brokers
                              select b;
            if (!String.IsNullOrEmpty(AgencyName))
            {
                brokersList = brokersList.Where(x => x.AgencyName.Contains(AgencyName));
            }
            if (!String.IsNullOrEmpty(SearchName))
            {
                //SearchName
                var SearchNames = SearchName.Split(' ').ToList();
                if (SearchNames.Count > 1)
                {
                    if (SearchNames.Count >= 3)
                    {
                        var fname = SearchNames[0].ToString();
                        var mname = SearchNames[1].ToString();
                        var lname = SearchNames[2].ToString();
                        brokersList = brokersList.Where(x => x.Firstname.Contains(fname) && x.Middlename.Contains(mname) && x.Lastname.Contains(lname));
                    }
                    else
                    {
                        var fname = SearchNames[0].ToString();
                        var lname = SearchNames[1].ToString();
                        brokersList = brokersList.Where(x => x.Firstname.Contains(fname) && x.Lastname.Contains(lname));
                    }


                }
                else
                {
                    brokersList = brokersList.Where(x => x.Firstname.Contains(SearchName) || x.Middlename.Contains(SearchName) || x.Lastname.Contains(SearchName));
                }
            }
            return View(brokersList);
        }

        // GET: Brokers/Brokers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broker broker = await db.Brokers.FindAsync(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // GET: Brokers/Brokers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brokers/Brokers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Firstname,Middlename,Lastname,EmailAddress,Telephone,Mobile,Birthplace,PermanentAddress,Nationality,CivilStatus,BirthDay,DateCreated,AgencyName")] Broker broker)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(broker);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(broker);
        }

        // GET: Brokers/Brokers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broker broker = await db.Brokers.FindAsync(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // POST: Brokers/Brokers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Firstname,Middlename,Lastname,EmailAddress,Telephone,Mobile,Birthplace,PermanentAddress,Nationality,CivilStatus,BirthDay,DateCreated,AgencyName")] Broker broker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(broker).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(broker);
        }

        // GET: Brokers/Brokers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broker broker = await db.Brokers.FindAsync(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // POST: Brokers/Brokers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Broker broker = await db.Brokers.FindAsync(id);
            db.People.Remove(broker);
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
