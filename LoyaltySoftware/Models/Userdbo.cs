using LoyaltySoftware.Pages.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class Userdbo
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public string dob { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        public string telephone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Total Points")]
        public int total_points { get; set; }



        public static bool CheckUserId(int userInputId)
        {

            int user_id = 0;

            using (SqlCommand command = new SqlCommand())
            {

                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT user_id FROM Userdbo WHERE user_id = @UID";

                command.Parameters.AddWithValue("@UID", userInputId);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user_id = reader.GetInt32(0);
                }

                if (user_id.Equals(userInputId))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

 

    }
}
