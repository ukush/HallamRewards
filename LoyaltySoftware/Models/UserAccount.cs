using LoyaltySoftware.Pages.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    
    public class UserAccount
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Status { get; set; }

        public string UserRole { get; set; }

        static string[] UserRoles = new string[] { "member", "admin" };
        static string[] UserStatuses = new string[] { "active", "suspended", "revoked" };


        public static string checkRole(string username)
        {
            string userRole = "";

            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT Username, UserRole FROM UserAccount WHERE Username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userRole = reader.GetString(1);
                }

                return userRole;
            }

        }
        public static string checkStatus(string username)
        {
            string userStatus = "";

            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT Username, UserStatus FROM UserAccount WHERE Username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userStatus = reader.GetString(1);
                }

                return userStatus;
            }
        }

        

        public static bool checkIfUsernameExists(string inputUsername)
        {
            string username="";
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT Username FROM UserAccount WHERE Username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    username = reader.GetString(0);
                }

                if (!string.IsNullOrEmpty(username))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public static bool checkPassword(string inputUsername, string inputPassword)
        {
            string password="";

            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT Username, Password FROM UserAccount WHERE Username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    password = reader.GetString(1);
                }

                if (inputPassword!=password)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }
        }
    }
}


