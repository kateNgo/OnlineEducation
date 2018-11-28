using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineEducation.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]   
        public string Password { get; set; }
        public string Name { get; set; }
        [DisplayName("Retype password")]
        [Compare("NewPassword")]
        public string AgainPassword { get; set; }
        public string NewPassword { get; set; }
    }
}