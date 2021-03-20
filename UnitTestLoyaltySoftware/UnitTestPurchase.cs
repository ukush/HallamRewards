using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shop;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestPurchase
    {
        [TestMethod]
        public void TestFindProduct()
        {
            int? productID = 1;
            string productNameExpected = "Shoe 1";
            string productNameActual = PurchaseMethods.findProduct(productID).productName;
            Assert.AreEqual(productNameExpected, productNameActual);
        }

        [TestMethod]
        public void TestCalculatePointsEarned()
        {
            double price = 100.0;
            int pointsClaimedExpected = 100;
            int pointsClaimedActual = PurchaseMethods.calculatePointsEarned(price);
            Assert.AreEqual(pointsClaimedExpected, pointsClaimedActual);
        }

    }
}
