using LoyaltySoftware.Pages.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestLoyaltySoftware
{
    [TestClass]
    public class UnitTestAccount
    {

        [TestMethod]
        public void TestCheckRole()
        {
            string username = "jsmith1";
            string roleExpected = "member";
            string roleActual = AccountMethods.checkRole(username);
            Assert.AreEqual(roleExpected, roleActual);
        }

        [TestMethod]
        public void TestCheckStatus()
        {
            string username = "jsmith1";
            string userStatusExpected = "active";
            string userStatusActual;
            userStatusActual = AccountMethods.checkStatus(username);
            Assert.AreEqual(userStatusActual, userStatusExpected);
        }

        [TestMethod]
        public void TestCheckIfUsernameExists()
        {
            string username = "jsmith1";
            Assert.IsTrue(AccountMethods.checkIfUsernameExists(username));
        }
        [TestMethod]
        public void TestCheckPassword()
        {
            string username = "jsmith1";
            string password = "12345";
            Assert.IsTrue(AccountMethods.checkPassword(username, password));
        }
        [TestMethod]
        public void TestFindAccountID()
        {
            string username = "jsmith1";
            int accountIDExpected = 2029;
            int accountIDActual = AccountMethods.findAccountID(username);
            Assert.AreEqual(accountIDExpected, accountIDActual);
        }

    }
}
