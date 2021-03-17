using LoyaltySoftware.UnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestProductInfo
    {
        [TestMethod]
        public void TestProduct()
        {
            int? productID = 1;
            string expectedProductName = "Shoe 1";
            string actualProductName = ProductInfo.getProductName(productID);
            Assert.AreEqual(expectedProductName, actualProductName);
        }

    }
}
