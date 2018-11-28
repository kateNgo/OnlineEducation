using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class TableOfContent
    {
        public string SourceFolder { get; set; }
        public string  XMLFileStr { get; set; }
        public HttpPostedFileBase XMLFilePath { get; set; }
    }
}