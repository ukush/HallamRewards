using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Member
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        public Userdbo UserRecord { get; set; }

        public string Message { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DBConnection dbstring = new DBConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(UserRecord.user_id);

        
            if (Userdbo.CheckUserId(UserRecord.user_id))
            {
            conn.Close();
            return RedirectToPage("/Member/ResetPassword");
            }
            else
            {
                Message = "That ID is not recognised.";
            conn.Close();
            return Page();
            }

            
        }
    }

}
