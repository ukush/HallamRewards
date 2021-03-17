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

namespace LoyaltySoftware.Pages.Shop
{
    public class ProductInfoModel : PageModel
    {
        [BindProperty]
        public Product ProductRec { get; set; }
        [BindProperty]
        public Userdbo UserRec { get; set; }
        [BindProperty]
        public int pointsEarned { get; set; }
        public string Username;
        public int AccountID;
        public const string SessionKeyName1 = "username";
        public IActionResult OnGet(int? id)
        {

            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();


            ProductRec = new Product();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Product WHERE product_id = @PID";

                command.Parameters.AddWithValue("@PID", id);
                Console.WriteLine("The id : " + id);

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

            pointsEarned = (int)Math.Round(ProductRec.productPrice, 0);  // price of the product is converted to points where is it is rounded to the nearest integer

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

                UserRec.total_points = Userdbo.getTotalPoints(AccountID);

                command.CommandText = @"UPDATE Userdbo SET points = @Pts WHERE account_id = @AID";

                UserRec.total_points += pointsEarned; // add points earned to user's total points

                command.Parameters.AddWithValue("@AID", AccountID);
                command.Parameters.AddWithValue("@Pts", UserRec.total_points);

                command.ExecuteNonQuery();
            }
            conn.Close();

            return RedirectToPage("/Shop/BrowseProducts");
        }

    }
}
