using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoyaltySoftware.Pages.Login;

namespace LoyaltySoftware.Pages.Rewards
{
    public class RewardInfoModel : PageModel
    {
        [BindProperty]
        public Reward RewardRec { get; set; }
        [BindProperty]
        public Userdbo UserRec { get; set; }
        [BindProperty]
        public int pointsNeeded { get; set; }
        public string Username;
        public int AccountID;
        public const string SessionKeyName1 = "username";
        public IActionResult OnGet(int? id)
        {
            RewardRec = new Reward();
            RewardRec = getReward(id);
            pointsNeeded = RewardRec.pointsToClaim;
            return Page();

        }

        public IActionResult OnPost()
        {
            UserRec = new Userdbo();
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Username = HttpContext.Session.GetString(SessionKeyName1);
            if (string.IsNullOrEmpty(Username))  // if user has not signed in yet
            {
                return RedirectToPage("/Login/UserLogin");
            }
            else
            {
                AccountID = UserAccount.findAccountID(Username);
            }

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;

                command.CommandText = @"SELECT points FROM Userdbo WHERE account_id = @AID";

                command.Parameters.AddWithValue("@AID", AccountID);

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table


                while (reader.Read())
                {
                    Userdbo.total_points = reader.GetInt32(0); //getting the first field from the table
                }

                if (Userdbo.total_points < pointsNeeded)
                {
                    return RedirectToPage("/Rewards/NotEnoughPointsToClaim");
                }
                else
                {
                    removePoints(AccountID, pointsNeeded);
                }
            }
            conn.Close();

            return RedirectToPage("/Member/MemberDashboard");
        }

        public static Reward getReward(int? id)
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Reward RewardRec = new Reward();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Reward WHERE reward_id = @RID";

                command.Parameters.AddWithValue("@RID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RewardRec.rewardId = reader.GetInt32(0);
                    RewardRec.rewardName = reader.GetString(1);
                    RewardRec.pointsToClaim = reader.GetInt32(2);
                    RewardRec.rewardImageSrc = reader.GetString(3);
                    RewardRec.rewardDescription = reader.GetString(4);
                }

            }

            return RewardRec;
        }

        public static void removePoints(int id, int pointsRemoved)
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            Userdbo.total_points -= pointsRemoved;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Userdbo SET points = @Pts WHERE account_id = @AID";

                command.Parameters.AddWithValue("@AID", id);
                command.Parameters.AddWithValue("@Pts", Userdbo.total_points);

                command.ExecuteNonQuery();
            }

            conn.Close();
        }


    }



}