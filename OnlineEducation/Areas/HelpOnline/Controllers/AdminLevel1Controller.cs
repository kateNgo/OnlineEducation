using OnlineEducation.Areas.HelpOnline.Models;
using System.Net;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class AdminLevel1Controller : Controller
    {
        private HelpLevel1DB helpLevel1DB = new HelpLevel1DB();
        // GET: HelpOnline/AdminLevel1
        public ActionResult Index()
        {
            return View(helpLevel1DB.ListAll());
        }

        // GET: HelpOnline/AdminLevel1/Details/5
        public ActionResult Details(int id)
        {
            HelpLevel1 helpLevel1 = helpLevel1DB.GetHelpLevel1ById(id);
            if (helpLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(helpLevel1);
        }

        // GET: HelpOnline/AdminLevel1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HelpOnline/AdminLevel1/Create
        [HttpPost]
        public ActionResult Create(HelpLevel1 helpLevel1)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(helpLevel1.ImageFileObj.FileName);
                //string fileExtension = Path.GetExtension(helpLevel1.ImageFileObj.FileName);
                //Get Upload path from Web.Config file AppSettings.  
                string uploadPath = ConfigurationManager.AppSettings["HelpOnlineImagePath"].ToString() + "HelpOnlineLevel1/";
                //Its Create complete path to store in server.  
                helpLevel1.ImageFile = fileName;//Server.MapPath(uploadPath + fileName);
                //To copy and save file into server.  
                helpLevel1.ImageFileObj.SaveAs(Server.MapPath(uploadPath + fileName));
                helpLevel1DB.InsertLevel1(helpLevel1);
                return RedirectToAction("Index");
            }
            return View();

        }

        // GET: HelpOnline/AdminLevel1/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel1 helpLevel1 = helpLevel1DB.GetHelpLevel1ById(id);
            if (helpLevel1 == null)
            {
                return HttpNotFound();
            }

            return View(helpLevel1);
        }

        // POST: HelpOnline/AdminLevel1/Edit/5
        [HttpPost]
        public ActionResult Edit(HelpLevel1 helpLevel1)
        {
            if (ModelState.IsValid)
            {
                if (helpLevel1.ImageFileObj != null)
                {
                    string fileName = Path.GetFileName(helpLevel1.ImageFileObj.FileName);
                    //string fileExtension = Path.GetExtension(helpLevel1.ImageFileObj.FileName);
                    //Get Upload path from Web.Config file AppSettings.  
                    string uploadPath = ConfigurationManager.AppSettings["HelpOnlineImagePath"].ToString() + "HelpOnlineLevel1/";
                    //Its Create complete path to store in server.  
                    helpLevel1.ImageFile = fileName;//Server.MapPath(uploadPath + fileName);
                                                    //To copy and save file into server.  
                    helpLevel1.ImageFileObj.SaveAs(Server.MapPath(uploadPath + fileName));
                }

                helpLevel1DB.UpdateLevel1(helpLevel1);
                return RedirectToAction("Index");
            }
            return View(helpLevel1);
        }
        
        // GET: HelpOnline/AdminLevel1/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HelpLevel1 helpLevel1 = helpLevel1DB.GetHelpLevel1ById(id);
            if (helpLevel1 == null)
            {
                return HttpNotFound();
            }
           // helpLevel1DB.DeleleHelpLevel1(id);
            return View(helpLevel1);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            helpLevel1DB.DeleleHelpLevel1(id);
            return RedirectToAction("Index");
        }
    }
}
