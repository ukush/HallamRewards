using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Methods
{
    public class ClaimRewardMethods
    {
        [BindProperty]
        public static Reward RewardRec { get; set; }
        public static Reward getReward(int? id)
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

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

        public static void removePoints(int id, int total_points)
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Userdbo SET points = @Pts WHERE account_id = @AID";

                command.Parameters.AddWithValue("@AID", id);
                command.Parameters.AddWithValue("@Pts", total_points);

                command.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}
