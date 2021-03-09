using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using LoyaltySoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LoyaltySoftware.Pages.Register
{
    public class RegisterUserModel : PageModel
    {

        [BindProperty]
        public User UserRecord { get; set; }

        [BindProperty]
        public UserAccount UserAccountRecord { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Uwais\source\repos\LoyaltySoftware2\LoyaltySoftware\LoyaltySoftware\Database\LoyaltySystem.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;

                command.CommandText = @"SET IDENTITY_INSERT UserAccount ON";

                command.CommandText = @"INSERT INTO Userdbo (first_name, last_name, dob, telephone, email, total_points) VALUES (@fname, @lname, @dob, @tphone, @email, @tpoints)";

                //command.Parameters.AddWithValue("@UID", UserRecord.Id);
                command.Parameters.AddWithValue("@fname", UserRecord.first_name);
                command.Parameters.AddWithValue("@lname", UserRecord.last_name);
                command.Parameters.AddWithValue("@dob", UserRecord.dob);
                command.Parameters.AddWithValue("@tphone", UserRecord.telephone);
                command.Parameters.AddWithValue("@email", UserRecord.email);
                command.Parameters.AddWithValue("@tpoints", 0); // set to 0 by default for new members

                command.CommandText = @"SET IDENTITY_INSERT UserAccount OFF";

                // insert username/password into useraccount table
                // status, role, user_id are not user input --> status is active, role is member by default.

                command.CommandText = @"SET IDENTITY_INSERT UserAccount ON";

                command.CommandText = @"INSERT INTO UserAccount (username, password, status, userRole) VALUES (@uname, @pword, @sts, @urole)";

                command.Parameters.AddWithValue("@uname", UserAccountRecord.username);
                command.Parameters.AddWithValue("@pword", UserAccountRecord.password);
                command.Parameters.AddWithValue("@sts", "active"); // set to "active" by default
                command.Parameters.AddWithValue("@urole", "member"); // set to "member" by default

                command.CommandText = @"SET IDENTITY_INSERT UserAccount OFF";


                command.ExecuteNonQuery();
            }


            return RedirectToPage("/Member/Dashboard");
        }

        }


    }
