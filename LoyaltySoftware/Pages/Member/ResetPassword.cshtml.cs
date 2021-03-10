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

        public IActionResult OnGet(int? id)
        {

            DBConnection dbstring = new DBConnection();      //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {

                command.Connection = conn;
                command.CommandText = @"SELECT password FROM UserAccount WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserAccountRecord.password = reader.GetString(3);
                }

            }

            conn.Close();

            return Page();
        }



        public IActionResult OnPost()
        {

            DBConnection dbstring = new DBConnection();      //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {

                command.Connection = conn;
                command.CommandText = @"UPDATE UserAccount SET password = @newPw";

                command.Parameters.AddWithValue("@NewPw", UserAccountRecord.password);

                command.ExecuteReader();
            }
            conn.Close();

            return RedirectToPage("/Login/UserLogin");
        }



    }


    
}
