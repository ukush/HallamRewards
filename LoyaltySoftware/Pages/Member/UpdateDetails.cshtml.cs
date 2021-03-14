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
    public class UpdateDetailsModel : PageModel
    {

        [BindProperty]
        public Userdbo UserRec { get; set; }
        public IActionResult OnGet(int? user_id)
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            UserRec = new Userdbo();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Userdbo WHERE user_id = @UID";

                command.Parameters.AddWithValue("@UID", user_id);

                SqlDataReader reader = command.ExecuteReader();

        
                reader.Read();


                UserRec.user_id = reader.GetInt32(0);
                UserRec.first_name = reader.GetString(1);
                UserRec.last_name = reader.GetString(2);
                UserRec.dob = reader.GetString(3);
                UserRec.telephone = reader.GetString(4);
                UserRec.email = reader.GetString(5);




                reader.Close();

                return Page();

            }
        }


        public IActionResult OnPost()
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine("First Name : " + UserRec.first_name);
            Console.WriteLine("Last Name : " + UserRec.last_name);
            Console.WriteLine("Date Of Birth : " + UserRec.dob);
            Console.WriteLine("Telephone : " + UserRec.telephone);
            Console.WriteLine("Email : " + UserRec.email);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"UPDATE Userdbo SET first_name = @fname, last_name = @lname, dob = @dob, telephone = @tphone, email = @email WHERE user_id = @UID";

                command.Parameters.AddWithValue("@UID", UserRec.user_id);
                command.Parameters.AddWithValue("@fname", UserRec.first_name);
                command.Parameters.AddWithValue("@lname", UserRec.last_name);
                command.Parameters.AddWithValue("@dob", UserRec.dob);
                command.Parameters.AddWithValue("@tphone", UserRec.telephone);
                command.Parameters.AddWithValue("@email", UserRec.email);

                command.ExecuteNonQuery();
            }
            conn.Close();
            return RedirectToPage("/Member/ProfilePage");
        }
    }
}
