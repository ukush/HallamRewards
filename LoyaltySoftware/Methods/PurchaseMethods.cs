using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Pages.Shop
{
    public class PurchaseMethods
    {
        [BindProperty]
        public static Product ProductRec { get; set; }
        public static Product findProduct(int? id)
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
