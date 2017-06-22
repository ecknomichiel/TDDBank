using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDBanking.Models;

namespace TDDBankingTests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AccountHasNumber()
        {
            //Arrange
            Account account = new Account() { AccountNumber = 7654321 };
            //Act
            //Assert
            Assert.AreEqual(7654321, account.AccountNumber);
        }

        [TestMethod]
        public void AccountHasBalance()
        {
            //Arrange
            Account account = new Account() { Balance = 100000000.00 };
            //Act
            //Assert
            Assert.AreEqual(100000000.00, account.Balance);
        }
    }
}
