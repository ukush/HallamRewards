using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Pages.Shared
{
    public class DBConnection
    {
        public string DatabaseString()
        {
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Uwais\source\repos\LoyaltySoftware2\LoyaltySoftware\LoyaltySoftware\Database\LoyaltySystem.mdf;Integrated Security=True";
            return DbString;
        }
    }
}
