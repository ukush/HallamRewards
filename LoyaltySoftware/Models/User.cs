using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class User
    {

        public int Id { get; set; }
        public int user_id { get; set; }
        
        [Display(Name = "First Name")]
        [Required]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public string dob { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        public string telephone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Total Points")]
        public int total_points { get; set; }
    }
}
