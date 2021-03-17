using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoyaltySoftware.Models;
using System;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestUserAccount
    {
        [TestMethod]
        public void TestRole()
        {
            string username = "jsmith1";
            string roleExpected = "member";
            string roleActual = UserAccount.checkRole(username);
            Assert.AreEqual(roleExpected, roleActual);
        }

        [TestMethod]
        public void TestStatus()
        {
            string username = "jsmith1";
            string userStatusExpected = "active";
            string userStatusActual;
            userStatusActual = UserAccount.checkStatus(username);
            Assert.AreEqual(userStatusActual, userStatusExpected);
        }

        [TestMethod]
        public void TestUsername()
        {
            string username = "hsmith3";
            Assert.IsTrue(UserAccount.checkIfUsernameExists(username));
        }
        [TestMethod]
        public void TestPassword()
        {
            string username = "jsmith1";
            string password = "12345";
            Assert.IsTrue(UserAccount.checkPassword(username, password));
        }
        

    }
}
