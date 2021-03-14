using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LoyaltySoftware.Pages.Register
{
    public class RegisterUserModel : PageModel
    {

        [BindProperty]
        public Userdbo UserDBORecord { get; set; }

        [BindProperty]
        public UserAccount UserAccountRecord { get; set; }

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

                command.CommandText = @"INSERT INTO UserAccount (username, password, status, user_role) VALUES (@uname, @pword, @sts, @urole)";

                command.Parameters.AddWithValue("@uname", UserAccountRecord.username);
                command.Parameters.AddWithValue("@pword", UserAccountRecord.password);
                command.Parameters.AddWithValue("@sts", "active"); // set to "active" by default
                command.Parameters.AddWithValue("@urole", "member"); // set to "member" by default

                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO Userdbo (first_name, last_name, dob, telephone, email, account_id) VALUES (@fname, @lname, @dob, @tphone, @email, (SELECT account_id FROM UserAccount WHERE username = @uname))";

                command.Parameters.AddWithValue("@fname", UserDBORecord.first_name);
                command.Parameters.AddWithValue("@lname", UserDBORecord.last_name);
                command.Parameters.AddWithValue("@dob", UserDBORecord.dob);
                command.Parameters.AddWithValue("@tphone", UserDBORecord.telephone);
                command.Parameters.AddWithValue("@email", UserDBORecord.email);

                command.ExecuteNonQuery();

                // insert username/password into useraccount table
                // status, role, user_id are not user input --> status is active, role is member by default.

            }


            conn.Close();
            return RedirectToPage("/Login/UserLogin");
        }

        }


    }
