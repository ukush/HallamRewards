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
    public class ProfilePageModel : PageModel
    {

        public List<User> UserRec { get; set; }

        public List<UserAccount> UserAccountRec { get; set; }


        public void OnGet()
        {
            DBConnection dbstring = new DBConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Userdbo";

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                UserRec = new List<User>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    User record = new User(); 
                    record.Id = reader.GetInt32(0); 
                    record.first_name = reader.GetString(1);
                    record.last_name = reader.GetString(2);
                    record.dob = reader.GetString(3);
                    record.telephone = reader.GetString(4);
                    record.email = reader.GetString(5);
                    record.total_points = reader.GetInt32(6);

                    UserRec.Add(record); //adding the single record into the list
                }

                command.CommandText = @"SELECT * FROM UserAccount";


                UserAccountRec = new List<UserAccount>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    UserAccount record = new UserAccount();
                    record.Id = reader.GetInt32(0);
                    record.Username = reader.GetString(1);
                    record.Password = reader.GetString(2);
                    record.Status = reader.GetString(3);
                    record.UserRole = reader.GetString(4);

                    UserAccountRec.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
    }
}
