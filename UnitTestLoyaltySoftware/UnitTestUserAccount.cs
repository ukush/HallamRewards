using LoyaltySoftware.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestUserAccount
    {
        public UserAccount UserAccount { get; set; }

        [TestMethod]
        public void TestCheckRole()
        {
            string username = "jsmith1";
            string roleExpected = "member";
            string roleActual = UserAccount.checkRole(username);
            Assert.AreEqual(roleExpected, roleActual);
        }

        [TestMethod]
        public void TestCheckStatus()
        {
            string username = "jsmith1";
            string userStatusExpected = "active";
            string userStatusActual;
            userStatusActual = UserAccount.checkStatus(username);
            Assert.AreEqual(userStatusActual, userStatusExpected);
        }

        [TestMethod]
        public void TestCheckIfUsernameExists()
        {
            string username = "jsmith1";
            Assert.IsTrue(UserAccount.checkIfUsernameExists(username));
        }
        [TestMethod]
        public void TestCheckPassword()
        {
            string username = "jsmith1";
            string password = "12345";
            Assert.IsTrue(UserAccount.checkPassword(username, password));
        }
        [TestMethod]
        public void TestFindAccountID()
        {
            string username = "jsmith1";
            int accountIDExpected = 2029;
            int accountIDActual = UserAccount.findAccountID(username);
            Assert.AreEqual(accountIDExpected, accountIDActual);
        }

    }
}
