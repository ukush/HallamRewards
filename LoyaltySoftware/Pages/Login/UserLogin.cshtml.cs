using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Login
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public UserAccount UserAccount { get; set; }
        public string Message { get; set; }
        public string SessionID;


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Uwais\Downloads\LoyaltySoftware-loginValidation\LoyaltySoftware-loginValidation\LoyaltySoftware\Database\LoyaltySystem.mdf;Integrated Security=True";


            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT , , , UserRole FROM User WHERE userID = @UID, username = @UName, password = @Pwd AND userRole = @URole";

                command.Parameters.AddWithValue("@UID", UserAccount.userID);
                command.Parameters.AddWithValue("@UName", UserAccount.username);
                command.Parameters.AddWithValue("@Pwd", UserAccount.password);
                command.Parameters.AddWithValue("@URole", UserAccount.userRole);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserAccount.userID = reader.GetInt32(0);
                    UserAccount.username = reader.GetString(1);
                    UserAccount.password = reader.GetString(2);
                    UserAccount.userRole = reader.GetString(3);
                }

                if (!string.IsNullOrEmpty(UserAccount.userID.ToString()))
                {
                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);
                    HttpContext.Session.SetString("username", UserAccount.username);
                    HttpContext.Session.SetString("fname", UserAccount.password);

                    if (!UserAccount.checkIfUsernameExists(UserAccount.username))
                    {
                        Message = "Username does not exists!";
                        return Page();
                    }
                    else if (!UserAccount.checkPassword(UserAccount.username, UserAccount.password))
                    {
                        Message = "Username does not exists!";
                        return Page();
                    }
                    else
                    {
                        if (UserAccount.checkRole(UserAccount.userRole) == "member")
                        {
                            return RedirectToPage("/MemberPages/Dashboard");
                        }
                        else
                        {
                            return RedirectToPage("/AdminPages/Dashboard");
                        }
                    }
                }





                return RedirectToPage("/Index");

            }
        }
    }

           
}