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
    public class UserLoginModel : PageModel
    {
        [BindProperty]
        public UserAccount UserAccount { get; set; }

        [BindProperty]
        public Userdbo UserRec { get; set; }



        public string Message1 { get; set; }
        public string Message2 { get; set; }

        public string SessionID;

        public int LoginAttempts { get; set; }

        public int currentattemps;



        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {

                // need to pass on user_id to next page


                DBConnection dbstring = new DBConnection(); //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                Console.WriteLine(DbConnection);
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                Console.WriteLine(UserAccount.username);
                Console.WriteLine(UserAccount.password);


                if (string.IsNullOrEmpty(UserAccount.username))
                {
                    Message1 = "Please enter a username!";
                    return Page();
                }
                else if (string.IsNullOrEmpty(UserAccount.password))
                {
                    Message2 = "Please enter a password!";
                    return Page();
                }
                else
                {

                    if (UserAccount.checkIfUsernameExists(UserAccount.username))
                    {
                        SessionID = HttpContext.Session.Id;
                        HttpContext.Session.SetString("sessionID", SessionID);
                        HttpContext.Session.SetString("username", UserAccount.username);

                    if (!UserAccount.checkPassword(UserAccount.username, UserAccount.password))
                        {
                            Message2 = "Password does not match!";
                            return Page();
                        }
                        else
                        {
                            if (UserAccount.checkRole(UserAccount.username) == "member")
                            {
                                return RedirectToPage("/Member/MemberDashboard");
                            }
                            else
                            {
                                return RedirectToPage("/Admin/AdminDashboard");
                            }
                        }
                    }
                    else
                    {
                        Message1 = "Username does not exist!";
                        return Page();
                    }
                }
            
        }

    }
}


