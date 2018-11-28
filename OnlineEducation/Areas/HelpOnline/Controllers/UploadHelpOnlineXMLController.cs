using OnlineEducation.Areas.HelpOnline.Models;
using OnlineEducation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEducation.Areas.HelpOnline.Controllers
{
    public class UploadHelpOnlineXMLController : Controller
    {
        AccountDB accountDb = new AccountDB();
        HelpOnlineDB helpOnlineDB = new HelpOnlineDB();
        private string XMLPath = ConfigurationManager.AppSettings["HelpOnlineXMLPath"].ToString();
        private string HTMLPath = ConfigurationManager.AppSettings["HelpOnlineHTMLPath"].ToString() ;
        List<HelpLevel1> listOfLevel1FromXMLFile = new List<HelpLevel1>();
        // GET: HelpOnline/UploadHelpOnlineXML
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(TableOfContent obj)
        {
            if (ModelState.IsValid)
            {
                string xmlFileName = "";
                if (obj.XMLFilePath != null)
                {
                    // 1. Upload xml file to server
                    xmlFileName = Server.MapPath(XMLPath + obj.XMLFilePath.FileName);
                    obj.XMLFilePath.SaveAs(xmlFileName);
                    //Paser xml file from server file
                    listOfLevel1FromXMLFile = XMLHanlder.ReadXML(xmlFileName, Server.MapPath(HTMLPath), obj.SourceFolder);
                    // synchronize with DB
                    helpOnlineDB.UpdateIndexTopicToZero();
                    helpOnlineDB.UpdateDBWithXML(listOfLevel1FromXMLFile, Server.MapPath(HTMLPath), obj.SourceFolder);
                    XMLHanlder.UploadImageFiles(obj.SourceFolder);
                    return RedirectToAction("UploadSuccess");                    
                }
            }
            return View();
        }

        public ActionResult UploadSuccess()
        {
            return View();
        }
    }
}
