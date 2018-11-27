using OnlineEducation.Areas.HelpOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class Level3Controller : Controller
    {
        private HelpLevel3DB helpLevel3DB = new HelpLevel3DB();
        // GET: HelpOnline/Level3
        public ActionResult Index(int Id)
        {
            int level1Id = Convert.ToInt32(TempData["level1Id"]);
            ViewBag.level1Id = level1Id;
            return View(helpLevel3DB.LoadHelpLevel3ByParentId(Id));
        }
    }
}