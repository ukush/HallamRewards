using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Shop
{
    public class ProductInfoModel : PageModel
    {
        [BindProperty]
        public Userdbo UserRec { get; set; }
        public Product ProductRec;
        [BindProperty]
        public int pointsEarned { get; set; }
        public string Username;
        public int AccountID;
        public const string SessionKeyName1 = "username";
        public string SessionID;
        public const string SessionKeyName2 = "sessionID";
        public IActionResult OnGet(int? id)
        {

            ProductRec = PurchaseMethods.findProduct(id);
            pointsEarned = PurchaseMethods.calculatePointsEarned(ProductRec.productPrice);

            return Page();

        }

        public IActionResult OnPost()
        {

            Username = HttpContext.Session.GetString(SessionKeyName1);
            SessionID = HttpContext.Session.GetString(SessionKeyName2);

            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Login/UserLogin");
            }
            else
            {

                UserRec = new Userdbo();
                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                Username = HttpContext.Session.GetString(SessionKeyName1);
                AccountID = AccountMethods.findAccountID(Username);

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;

                    UserRec.total_points = Userdbo.getTotalPoints(AccountID);

                    command.CommandText = @"UPDATE Userdbo SET points = @Pts WHERE account_id = @AID";

                    UserRec.total_points += pointsEarned; // add points earned to user's total points

                    command.Parameters.AddWithValue("@AID", AccountID);
                    command.Parameters.AddWithValue("@Pts", UserRec.total_points);

                    command.ExecuteNonQuery();
                }
                conn.Close();

                return RedirectToPage("/Member/MemberDashboard");
            }
        }



    }
}
