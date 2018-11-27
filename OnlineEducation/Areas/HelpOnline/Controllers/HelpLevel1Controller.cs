using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineEducation.Areas.HelpOnline.Models;
using OnlineEducation.Models;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class HelpLevel1Controller : Controller
    {
        private OnlineEducationContext db = new OnlineEducationContext();

        // GET: HelpOnline/HelpLevel1
        public ActionResult Index()
        {
            return View(db.HelpLevel1.ToList());
        }

        // GET: HelpOnline/HelpLevel1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel1 helpLevel1 = db.HelpLevel1.Find(id);
            if (helpLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(helpLevel1);
        }

        // GET: HelpOnline/HelpLevel1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HelpOnline/HelpLevel1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ImageFile,Index")] HelpLevel1 helpLevel1)
        {
            if (ModelState.IsValid)
            {
                db.HelpLevel1.Add(helpLevel1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(helpLevel1);
        }

        // GET: HelpOnline/HelpLevel1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel1 helpLevel1 = db.HelpLevel1.Find(id);
            if (helpLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(helpLevel1);
        }

        // POST: HelpOnline/HelpLevel1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ImageFile,Index")] HelpLevel1 helpLevel1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(helpLevel1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(helpLevel1);
        }

        // GET: HelpOnline/HelpLevel1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel1 helpLevel1 = db.HelpLevel1.Find(id);
            if (helpLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(helpLevel1);
        }

        // POST: HelpOnline/HelpLevel1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HelpLevel1 helpLevel1 = db.HelpLevel1.Find(id);
            db.HelpLevel1.Remove(helpLevel1);
            db.SaveChanges();
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
