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
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Fauzan\Documents\Raihan's Folder\Sheffield Hallam University\firstyear\software_projects\LoyaltySoftwareProject\LoyaltySoftware\Data\LoyaltyDB.mdf;Integrated Security=True;Connect Timeout=30";
            return DbString;
        }
    }
}
