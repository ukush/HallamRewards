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

        public List<Userdbo> UserRec { get; set; }

        public List<UserAccount> UserAccountRec { get; set; }


        public void OnGet()
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Userdbo";

                SqlDataReader reader = command.ExecuteReader();

                UserRec = new List<Userdbo>();


                reader.Read();


                Userdbo record1 = new Userdbo(); 
                record1.user_id = reader.GetInt32(0); 
                record1.first_name = reader.GetString(1);
                record1.last_name = reader.GetString(2);
                record1.dob = reader.GetString(3);
                record1.telephone = reader.GetString(4);
                record1.email = reader.GetString(5);

                UserRec.Add(record1);

                command.CommandText = @"SELECT * FROM UserAccount";

                UserAccountRec = new List<UserAccount>();




                UserAccount record2 = new UserAccount();
                record2.username = reader.GetString(1);     // why is this reading from userdbo and NOT UserAccount Table??
                record2.password = reader.GetString(2);

                UserAccountRec.Add(record2);


                reader.Close();

                }
            }
        }
}
