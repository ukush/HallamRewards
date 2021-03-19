using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestProductInfo
    {
        public ProductInfoModel ProductInfo { get; set; }
        [TestMethod]
        public void TestFindProduct()
        {
            int? productID = 1;
            string productNameExpected = "Shoes 1";
            Product productNameActual = ProductInfo.findProduct(productID);
            Assert.AreEqual(productNameExpected, "p");
        }

    }
}
