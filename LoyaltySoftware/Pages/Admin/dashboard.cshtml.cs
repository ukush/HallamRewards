using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.AdminPages
{
    public class dashboardModel : PageModel
    {
        public string Username;
        public const string SessionKeyName1 = "username";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";

        public IActionResult OnGet()
        {
            Username = HttpContext.Session.GetString(SessionKeyName1);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Login/Login");
            }
            return Page();

        }
    }
}
