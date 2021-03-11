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

        [BindProperty]
        public Userdbo UserAccountRecord { get; set; }


        public string Message { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(UserRecord.user_id);


            if (Userdbo.CheckUserId(UserRecord.user_id))
            {

                UserRecord.user_id = UserRecord.user_id;
                conn.Close();
                return RedirectToPage("/Member/ResetPassword");
            }
            else
            {
                Message = "That ID is not recognised.";
            }
            conn.Close();
            return Page();
            }
    }

}
