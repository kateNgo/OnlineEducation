using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class HelpLevel3
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Index { get; set; }
        public string URL { get; set; }
        [Required]
        public int ParentId { get; set; }
        [DisplayName("Parent Level 2")]
        public HelpLevel2 ParentTopic { get; set; }
        public string ParentTopicTitle
        {
            get
            {
                if (ParentTopic != null)
                    return ParentTopic.Title;
                return "";
            }
        }
        public HttpPostedFileBase URLObj { get; set; }
    }
}