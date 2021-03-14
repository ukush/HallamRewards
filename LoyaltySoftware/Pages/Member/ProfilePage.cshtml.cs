using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Member
{
    public class ProfilePageModel : PageModel
    {

        public Userdbo UserRec { get; set; }


        public IActionResult OnGet(int? user_id)
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand()) 
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Userdbo Where user_id = @UID";

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
    }
}