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
    public class AgentsController : Controller
    {
        private CrmCore db = new CrmCore();
        private CrmCoreApplicationHelpers crmHelper = new CrmCoreApplicationHelpers();
        // GET: Brokers/Agents
        public ActionResult Index(string AgentNo, string Agency, string BrokerName, string SearchName)
        {
            var people = db.Agents.Include(a => a.Broker);

            if (!String.IsNullOrEmpty(AgentNo))
            {
                people = people.Where(x => x.AgentNo.Contains(AgentNo));
            }
            if (!String.IsNullOrEmpty(Agency))
            {
                people = people.Where(x => x.Agency.Contains(Agency));
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
                        people = people.Where(x => x.Firstname.Contains(fname) && x.Middlename.Contains(mname) && x.Lastname.Contains(lname));
                    }
                    else
                    {
                        var fname = SearchNames[0].ToString();
                        var lname = SearchNames[1].ToString();
                        people = people.Where(x => x.Firstname.Contains(fname) && x.Lastname.Contains(lname));
                    }


                }
                else
                {
                    people = people.Where(x => x.Firstname.Contains(SearchName) || x.Middlename.Contains(SearchName) || x.Lastname.Contains(SearchName));
                }
            }
            return View(people);
        }

        // GET: Brokers/Agents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agents.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // GET: Brokers/Agents/Create
        public ActionResult Create()
        {
            ViewBag.BrokerID = new SelectList(db.Brokers, "ID", "FullName");
            return View();
        }

        // POST: Brokers/Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Firstname,Middlename,Lastname,EmailAddress,Telephone,Mobile,Birthplace,PermanentAddress,Nationality,CivilStatus,BirthDay,DateCreated,AgentNo,Agency,BrokerID")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Agents.Add(agent);
                await db.SaveChangesAsync();
                agent.AgentNo = crmHelper.generateSequenceNumber(crmHelper.getPrefix(SerialType.AGENT), agent.ID);
                db.Entry(agent).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.BrokerID = new SelectList(db.Brokers, "ID", "FullName", agent.BrokerID);
            return View(agent);
        }

        // GET: Brokers/Agents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agents.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrokerID = new SelectList(db.Brokers, "ID", "FullName", agent.BrokerID);
            return View(agent);
        }

        // POST: Brokers/Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,AgentNo, Firstname,Middlename,Lastname,EmailAddress,Telephone,Mobile,Birthplace,PermanentAddress,Nationality,CivilStatus,BirthDay,DateCreated,AgentNo,Agency,BrokerID")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrokerID = new SelectList(db.Brokers, "ID", "FullName", agent.BrokerID);
            return View(agent);
        }

        // GET: Brokers/Agents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = await db.Agents.FindAsync(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Brokers/Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Agent agent = await db.Agents.FindAsync(id);
            db.People.Remove(agent);
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
