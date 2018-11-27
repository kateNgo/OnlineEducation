using OnlineEducation.Areas.HelpOnline.Models;
using System.Net;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using System.Collections.Generic;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class AdminLevel3Controller : Controller
    {
        private HelpLevel3DB helpLevel3DB = new HelpLevel3DB();
        private HelpLevel2DB helpLevel2DB = new HelpLevel2DB();
        private HelpLevel1DB helpLevel1DB = new HelpLevel1DB();
        // GET: HelpOnline/AdminLevel3/Index/5
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                TempData["parentTitle"] = "All";
                return View(helpLevel3DB.ListAll());
            }
            HelpLevel2 parent = helpLevel2DB.GetHelpLevel2ById(id.Value);
            if (parent == null)
            {
                return HttpNotFound();
            }
            TempData["parentTitle"] = parent.Title;
            TempData["parentId"] = parent.Id;
            return View(helpLevel3DB.LoadHelpLevel3ByParentId(id.Value));
        }

        // GET: HelpOnline/AdminLevel3/Details/5
        public ActionResult Details(int id)
        {
            HelpLevel3 helpLevel3 = helpLevel3DB.GetHelpLevel3ById(id);
            if (helpLevel3 == null)
            {
                return HttpNotFound();
            }
            HelpLevel2 parent = helpLevel2DB.GetHelpLevel2ById(helpLevel3.ParentId);
            helpLevel3.ParentTopic = parent;
            return View(helpLevel3);
        }

        // GET: HelpOnline/AdminLevel3/Create
        public ActionResult Create()
        {
            makeParentList();
            return View();
        }
        private void makeParentList()
        {
            List<SelectListItem> parentList = new List<SelectListItem>();
            foreach (HelpLevel2 item in helpLevel2DB.ListAll())
            {
                parentList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
            }

            ViewBag.ParentList = new SelectList(parentList, "Value", "Text");
        }
        // POST: HelpOnline/AdminLevel3/Create
        [HttpPost]
        public ActionResult Create(HelpLevel3 helpLevel3)
        {
            if (ModelState.IsValid)
            {
                if (helpLevel3.URLObj != null)
                {
                    string fileName = Path.GetFileName(helpLevel3.URLObj.FileName);
                    //Get Upload path from Web.Config file AppSettings.  
                    string uploadPath = ConfigurationManager.AppSettings["HelpOnlineHTMLPath"].ToString();
                    //Its Create complete path to store in server.  
                    helpLevel3.URL = fileName;
                    //To copy and save file into server.  
                    helpLevel3.URLObj.SaveAs(Server.MapPath(uploadPath + fileName));
                }else
                {
                    helpLevel3.URL = "";
                }
                

                helpLevel3DB.InsertLevel3(helpLevel3);
                return RedirectToAction("Index/" + helpLevel3.ParentId);
            }
            makeParentList();
            return View();
        }

        // GET: HelpOnline/AdminLevel3/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel3 helpLevel3 = helpLevel3DB.GetHelpLevel3ById(id);

            if (helpLevel3 == null)
            {
                return HttpNotFound();
            }
            HelpLevel2 helpLevel2 = helpLevel2DB.GetHelpLevel2ById(helpLevel3.ParentId);
            if (helpLevel2 == null)
            {
                return HttpNotFound();
            }
            helpLevel3.ParentTopic = helpLevel2;
            return View(helpLevel3);
        }

        // POST: HelpOnline/AdminLevel3/Edit/5
        [HttpPost]
        public ActionResult Edit(HelpLevel3 helpLevel3)
        {
            if (ModelState.IsValid)
            {
                if (helpLevel3.URLObj != null)
                {
                    string fileName = Path.GetFileName(helpLevel3.URLObj.FileName);
                    //Get Upload path from Web.Config file AppSettings.  
                    string uploadPath = ConfigurationManager.AppSettings["HelpOnlineHTMLPath"].ToString();
                    //Its Create complete path to store in server.  
                    helpLevel3.URL = fileName;
                    //To copy and save file into server.  
                    helpLevel3.URLObj.SaveAs(Server.MapPath(uploadPath + fileName));
                }
                helpLevel3DB.UpdateLevel3(helpLevel3);
                return RedirectToAction("Index/" + helpLevel3.ParentId);
            }
            return RedirectToAction("Index/" + helpLevel3.ParentId);
        }

        // GET: HelpOnline/AdminLevel3/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel3 helpLevel3 = helpLevel3DB.GetHelpLevel3ById(id);
            if (helpLevel3 == null)
            {
                return HttpNotFound();
            }
            //helpLevel3DB.DeleleLevel3(id);
            return View(helpLevel3);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HelpLevel3 helpLevel3 = helpLevel3DB.GetHelpLevel3ById(id);
            helpLevel3DB.DeleleLevel3(id);
            return RedirectToAction("Index/" + helpLevel3.ParentId);
        }
    }
}
