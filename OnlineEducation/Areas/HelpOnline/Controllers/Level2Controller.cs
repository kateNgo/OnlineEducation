using OnlineEducation.Areas.HelpOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class Level2Controller : Controller
    {
        private HelpLevel2DB helpLevel2DB = new HelpLevel2DB();
        int level1Id = 0;
        // GET: HelpOnline/Level2
        public ActionResult Index(int Id)
        {
            level1Id = Id;
            TempData["level1Id"] = level1Id;

            return View(helpLevel2DB.LoadHelpLevel2ByParentId(Id));
        }
    }
}