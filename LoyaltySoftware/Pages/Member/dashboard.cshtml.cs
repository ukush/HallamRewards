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

namespace LoyaltySoftware.Pages.Login
{
    public class loginModel : PageModel
    {
        [BindProperty]
        public UserAccount UserAccount { get; set; }
        public string UsernameMessage { get; set; }
        public string PasswordMessage { get; set; }
        public string SessionID;


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DBConnection dbstring = new DBConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(UserAccount.Username);
            Console.WriteLine(UserAccount.Password);

            if (string.IsNullOrEmpty(UserAccount.Username))
            {
                UsernameMessage = "Please enter a username!";
                return Page();
            }
            else if (string.IsNullOrEmpty(UserAccount.Password))
            {
                PasswordMessage = "Please enter a password!";
                return Page();
            }
            else
            {

                if (UserAccount.checkIfUsernameExists(UserAccount.Username))
                {
                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);
                    HttpContext.Session.SetString("username", UserAccount.Username);
                    HttpContext.Session.SetString("password", UserAccount.Password);

                    if (!UserAccount.checkPassword(UserAccount.Username, UserAccount.Password))
                    {
                        PasswordMessage = "Password does not match!";
                        return Page();
                    }
                    else
                    {
                        if (UserAccount.checkRole(UserAccount.Username) == "member")
                        {
                            return RedirectToPage("/MemberPages/Dashboard");
                        }
                        else
                        {
                            return RedirectToPage("/AdminPages/Dashboard");
                        }
                    }
                }
                else
                {
                    UsernameMessage = "Username does not exist!";
                    return Page();
                }
            }

            
        }
    }
}

