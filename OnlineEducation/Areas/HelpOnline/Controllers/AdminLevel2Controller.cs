using OnlineEducation.Areas.HelpOnline.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class AdminLevel2Controller : Controller
    {
        private HelpLevel2DB helpLevel2DB = new HelpLevel2DB();
        private HelpLevel1DB helpLevel1DB = new HelpLevel1DB();
        
        // GET: HelpOnline/AdminLevel2/Index/5
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                TempData["parentTitle"] = "All";
                return View(helpLevel2DB.ListAll());
            }
            HelpLevel1 parent = helpLevel1DB.GetHelpLevel1ById(id.Value);
            if (parent == null)
            {
                return HttpNotFound();
            }
            TempData["parentTitle"] = parent.Title;
            TempData["parentId"] = parent.Id;
            return View(helpLevel2DB.LoadHelpLevel2ByParentId(id.Value));
        }
        // GET: HelpOnline/AdminLevel2/Details/5
        public ActionResult Details(int id)
        {
            
            HelpLevel2 helpLevel2 = helpLevel2DB.GetHelpLevel2ById(id);
            if (helpLevel2 == null)
            {
                return HttpNotFound();
            }
            return View(helpLevel2);
        }

        // GET: HelpOnline/AdminLevel2/Create
        public ActionResult Create()
        {
            List<SelectListItem> parentList = new List<SelectListItem>();
            foreach(HelpLevel1 item in helpLevel1DB.ListAll())
            {
                parentList.Add(new SelectListItem { Text = item.Title, Value =  item.Id.ToString() });
            }
            
            ViewBag.ParentList = new SelectList(parentList, "Value", "Text");
            return View();
        }

        // POST: HelpOnline/AdminLevel2/Create
        [HttpPost]
        public ActionResult Create(HelpLevel2 helpLevel2)
        {
            if (ModelState.IsValid)
            {
                helpLevel2DB.InsertLevel2(helpLevel2);
                return RedirectToAction("Index/" + helpLevel2.ParentId);
            }
            return View();
        }

        // GET: HelpOnline/AdminLevel2/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel2 helpLevel2 = helpLevel2DB.GetHelpLevel2ById(id);

            if (helpLevel2 == null)
            {
                return HttpNotFound();
            }

            HelpLevel1 helpLevel1 = helpLevel1DB.GetHelpLevel1ById(helpLevel2.ParentId);
            if (helpLevel1 == null)
            {
                return HttpNotFound();
            }
            helpLevel2.ParentTopic = helpLevel1;
            return View(helpLevel2);
        }

        // POST: HelpOnline/AdminLevel2/Edit/5
        [HttpPost]
        public ActionResult Edit(HelpLevel2 helpLevel2)
        {
            if (ModelState.IsValid)
            {
                helpLevel2DB.UpdateLevel2(helpLevel2);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // GET: HelpOnline/AdminLevel2/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel2 helpLevel2 = helpLevel2DB.GetHelpLevel2ById(id);
            if (helpLevel2 == null)
            {
                return HttpNotFound();
            }
            //helpLevel2DB.DeleleLevel2(id);
            return View(helpLevel2);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HelpLevel2 helpLevel2 = helpLevel2DB.GetHelpLevel2ById(id);
            helpLevel2DB.DeleleLevel2(id);
            return RedirectToAction("Index/" + helpLevel2.ParentId);
        }
    }
}
