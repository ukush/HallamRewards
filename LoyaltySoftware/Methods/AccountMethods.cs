using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Pages.Login
{
    public class AccountMethods
    {
        [BindProperty]
        public UserAccount UserAccount { get; set; }

        public static string checkStatus(string username)
        {
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username, status FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserAccount.status = reader.GetString(1);
                }

                return UserAccount.status;
            }
        }


        public static string checkRole(string username)
        {

            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username, user_role FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserAccount.user_role = reader.GetString(1);
                }

                return UserAccount.user_role;
            }

        }

        public static bool checkIfUsernameExists(string inputUsername)
        {
            string username = "";
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username FROM userAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                SqlDataReader reader = command.ExecuteReader();

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
            string password = "";

            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username, password FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    password = reader.GetString(1);
                }

                if (inputPassword != password)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public static int findAccountID(string inputUsername)
        {
            int accountID = -1;

            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT account_id FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    accountID = reader.GetInt32(0);
                }

                return accountID;

            }
        }
    }
}
