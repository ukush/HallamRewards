using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoyaltySoftware.Models;
using System;
using LoyaltySoftware.UnitTests;

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
            string roleActual = LoyaltySoftware.UnitTests.UserAccount.checkRole(username);
            Assert.AreEqual(roleExpected, roleActual);
        }

        [TestMethod]
        public void TestStatus()
        {
            string username = "jsmith1";
            string userStatusExpected = "active";
            string userStatusActual;
            userStatusActual = LoyaltySoftware.UnitTests.UserAccount.checkStatus(username);
            Assert.AreEqual(userStatusActual, userStatusExpected);
        }

        [TestMethod]
        public void TestUsername()
        {
            string username = "hsmith3";
            Assert.IsTrue(LoyaltySoftware.UnitTests.UserAccount.checkIfUsernameExists(username));
        }
        [TestMethod]
        public void TestPassword()
        {
            string username = "jsmith1";
            string password = "12345";
            Assert.IsTrue(LoyaltySoftware.UnitTests.UserAccount.checkPassword(username, password));
        }
        [TestMethod]
        public void TestAccountID()
        {
            string username = "jsmith1";
            int accountIDExpected = 1028;
            int accountIDActual = LoyaltySoftware.UnitTests.UserAccount.findAccountID(username);
            Assert.AreEqual(accountIDExpected, accountIDActual);
        }


    }
}
