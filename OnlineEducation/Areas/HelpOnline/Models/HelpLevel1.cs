using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace OnlineEducation.Areas.HelpOnline.Models
{
    public class HelpLevel1
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [DisplayName("File Image")]
        public string ImageFile { get; set; }
        public int Index { get; set; }
        public List<HelpLevel2> Children { get; set; }
        public HttpPostedFileBase ImageFileObj { get; set; }
    }
}