using LoyaltySoftware.Pages.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    
    public class UserAccount
    {
        public int account_id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [Display(Name = "Status")]
        public static string status { get; set; }
        [Required]
        [Display(Name = "User Role")]
        public static string user_role { get; set; }

        public static string[] UserRoles = new string[] { "member", "admin" };
        public static string[] UserStatuses = new string[] { "active", "suspended", "revoked" };






    }
}


