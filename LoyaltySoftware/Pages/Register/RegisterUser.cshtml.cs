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
        public Userdbo UserRecord { get; set; }

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
                command.CommandText = @"INSERT INTO Userdbo (first_name, last_name, dob, telephone, email, points) VALUES (@fname, @lname, @dob, @tphone, @email, @points)";

                command.Parameters.AddWithValue("@fname", UserRecord.first_name);
                command.Parameters.AddWithValue("@lname", UserRecord.last_name);
                command.Parameters.AddWithValue("@dob", UserRecord.dob);
                command.Parameters.AddWithValue("@tphone", UserRecord.telephone);
                command.Parameters.AddWithValue("@email", UserRecord.email);
                command.Parameters.AddWithValue("@points", 0);  // set points to 0 by default

                command.ExecuteNonQuery();

                // insert username/password into useraccount table
                // status, role, user_id are not user input --> status is active, role is member by default.



                command.CommandText = @"INSERT INTO UserAccount (username, password, status, user_role, user_id) VALUES (@uname, @pword, @sts, @urole, @UID)";

                command.Parameters.AddWithValue("@uname", UserAccountRecord.username);
                command.Parameters.AddWithValue("@pword", UserAccountRecord.password);
                command.Parameters.AddWithValue("@sts", "active"); // set to "active" by default
                command.Parameters.AddWithValue("@urole", "member"); // set to "member" by default
                command.Parameters.AddWithValue("@UID", UserRecord.user_id); // add user id into the useraccount table



                command.ExecuteNonQuery();
            }


            conn.Close();
            return RedirectToPage("/Login/UserLogin");
        }

        }


    }
