using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
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
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Uwais\source\repos\LoyaltySoftware2\LoyaltySoftware\LoyaltySoftware\Database\LoyaltySystem.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM User";

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                UserRec = new List<User>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    User record = new User(); //a local var to hold a record temporarily
                    record.Id = reader.GetInt32(0); //getting the first field from the table
                    record.user_id = reader.GetInt32(1); //getting the second field from the table
                    record.first_name = reader.GetString(2); //getting the third field from the table
                    record.last_name = reader.GetString(3);
                    record.dob = reader.GetString(4);
                    record.telephone = reader.GetString(5);
                    record.email = reader.GetString(6);
                    record.total_points = reader.GetInt32(7);

                    UserRec.Add(record); //adding the single record into the list
                }

                command.CommandText = @"SELECT * FROM UserAccount";


                UserAccountRec = new List<UserAccount>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    UserAccount record = new UserAccount(); //a local var to hold a record temporarily
                    record.Id = reader.GetInt32(0); //getting the first field from the table
                    record.username = reader.GetString(1); //getting the second field from the table
                    record.password = reader.GetString(2); //getting the third field from the table
                    record.status = reader.GetString(3);
                    record.userRole = reader.GetString(4);

                    UserAccountRec.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
    }
}
