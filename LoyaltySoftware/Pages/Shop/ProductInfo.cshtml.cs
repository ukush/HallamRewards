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
        [BindProperty]
        public Product ProductRec { get; set; }
        [BindProperty]
        public int pointsEarned { get; set; }
        public string Username;
        public int AccountID;
        public const string SessionKeyName1 = "username";
        public string SessionID;
        public const string SessionKeyName2 = "sessionID";
        public IActionResult OnGet(int? id)
        {

            ProductRec = findProduct(id);
            pointsEarned = calculatePointsEarned(ProductRec.productPrice);

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
                AccountID = UserAccount.findAccountID(Username);

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;

                    Userdbo.total_points = Userdbo.getTotalPoints(AccountID);

                    command.CommandText = @"UPDATE Userdbo SET points = @Pts WHERE account_id = @AID";

                    Userdbo.total_points += pointsEarned; // add points earned to user's total points

                    command.Parameters.AddWithValue("@AID", AccountID);
                    command.Parameters.AddWithValue("@Pts", Userdbo.total_points);

                    command.ExecuteNonQuery();
                }
                conn.Close();

                return RedirectToPage("/Member/MemberDashboard");
            }
        }

        public static Product findProduct(int? id)
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();


            Product ProductRec = new Product();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Product WHERE product_id = @PID";

                command.Parameters.AddWithValue("@PID", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductRec.productId = reader.GetInt32(0);
                    ProductRec.productName = reader.GetString(1);
                    ProductRec.productPrice = (double)reader.GetDecimal(2);
                    ProductRec.productImageSrc = reader.GetString(3);
                    ProductRec.productDescription = reader.GetString(4);
                }

            }
            conn.Close();
            return ProductRec;
        }

        public static int calculatePointsEarned(double price)
        {
            return (int)Math.Round(price, 0);  // price of the product is converted to points where is it is rounded to the nearest integer
        }



    }
}
