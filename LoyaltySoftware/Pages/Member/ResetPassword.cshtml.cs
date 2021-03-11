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
    public class ResetPasswordModel : PageModel
    {

        [BindProperty]
        public UserAccount UserAccountRecord { get; set; }

        [BindProperty]
        public Userdbo UserRecord { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
           { 
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"UPDATE UserAccount SET password = @newPw WHERE user_id = @UID";
                command.Parameters.AddWithValue("@UID", UserAccountRecord.user_id);
                command.Parameters.AddWithValue("@NewPw", UserAccountRecord.password);
                command.ExecuteReader();
            }
            conn.Close();

            return RedirectToPage("/Login/UserLogin");
        }
    }
}
