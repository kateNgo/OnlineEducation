using OnlineEducation.Areas.HelpOnline.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class Level1Controller : Controller
    {
        private HelpLevel1DB helpLevel1DB = new HelpLevel1DB();

        // GET: HelpOnline/Home
        public ActionResult Index()
        {
            return View(helpLevel1DB.ListAll());
        }
    }
}