using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class HelpLevel2
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string ImageFile { get; set; }
        public int Index { get; set; }
        [Required]
        [DisplayName("Level 1")]
        public int ParentId { get; set; }
        public IEnumerable<SelectListItem> parents { get; set; }
        public HelpLevel1 ParentTopic { get; set; }
        public string ParentTopicTitle
        {
            get
            {
                if (ParentTopic != null)
                    return ParentTopic.Title;
                return "";
            }
        }
        public List<HelpLevel3> Children { get; set; }
    }
}