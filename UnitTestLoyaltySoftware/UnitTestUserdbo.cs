using LoyaltySoftware.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestUserdbo
    {
        public Userdbo Userdbo { get; set; }
        [TestMethod]
        public void TestTotalPoints()
        {
            int accountID = 2029;
            int totalPointsExpected = 200;
            int totalPointsActual = Userdbo.getTotalPoints(accountID);
            Assert.AreEqual(totalPointsExpected, totalPointsActual);
        }
    }
}
