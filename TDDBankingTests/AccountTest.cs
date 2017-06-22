using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using TDDBanking.Models;
using TDDBanking.DataAccess;
using System.Collections.Generic;

namespace TDDBankingTests
{
    [TestClass]
    public class AccountTest
    {

        [TestMethod]
        public void AccountHasNumber()
        {
            //Arrange
            Account account = new Account(new List<Transaction> { new Transaction() { ID = 1, Amount = -100, BalanceAccountNumber = 1234567 } }) { AccountNumber = 7654321 };
            //Act
            //Assert
            Assert.AreEqual(7654321, account.AccountNumber);
        }

        [TestMethod]
        public void AccountHasBalance()
        {
            //Arrange
            Account account = new Account(new List<Transaction> { new Transaction() { ID = 1, Amount = -100, BalanceAccountNumber = 7654321 } }) { Balance = 100000000.00 };
            //Act
            //Assert
            Assert.AreEqual(100000000.00, account.Balance);
        }

        [TestMethod]
        public void AccountGetTransactionHistoryEmpty()
        {
            //Arrange
            Account account = new Account() { AccountNumber = 13};
            int expectedResult = 0;


            //Act
            int actualResult = account.GetAllTransactions();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
