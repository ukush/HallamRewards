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
    public class DeleteDetailsModel : PageModel
    {

        [BindProperty]
        public Userdbo UserRec { get; set; }

        public UserAccount UserAccountRec { get; set; }
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
                command.CommandText = @"SELECT * FROM Userdbo";

                SqlDataReader reader = command.ExecuteReader();


                reader.Read();


                UserRec.user_id = reader.GetInt32(0);
                UserRec.first_name = reader.GetString(1);
                UserRec.last_name = reader.GetString(2);
                UserRec.dob = reader.GetString(3);
                UserRec.telephone = reader.GetString(4);
                UserRec.email = reader.GetString(5);


                UserAccountRec.username = reader.GetString(1);




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
                command.CommandText = @"DELETE Userdbo WHERE user_id = @UID";
                command.CommandText = @"DELETE UserAccount WHERE username = @Uname";

                command.Parameters.AddWithValue("@UID", UserRec.user_id);
                command.Parameters.AddWithValue("@Uname", UserAccountRec.username);

                command.ExecuteNonQuery();
            }
            conn.Close();
            return RedirectToPage("/Index");
        }
    }
}
