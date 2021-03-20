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
        public static UserAccount UserAccountRec { get; set; }
        [BindProperty]
        public static Userdbo UserdboRec { get; set; }

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
            UserAccountRec = new UserAccount();
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
                    UserAccountRec.username = reader.GetString(0);
                }

                if (!string.IsNullOrEmpty(UserAccountRec.username))
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
            UserAccountRec = new UserAccount();
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
                    UserAccountRec.password = reader.GetString(1);
                }

                if (inputPassword != UserAccountRec.password)
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
            UserAccountRec = new UserAccount();
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
                    UserAccountRec.account_id = reader.GetInt32(0);
                }

                return UserAccountRec.account_id;

            }
        }

        public static int getTotalPoints(int accountID)
        {
            UserdboRec = new Userdbo();
            using (SqlCommand command = new SqlCommand())
            {

                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT points FROM Userdbo WHERE account_id = @AID";

                command.Parameters.AddWithValue("@AID", accountID);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserdboRec.total_points = reader.GetInt32(0);
                }

                return UserdboRec.total_points;
            }
        }
    }
}
