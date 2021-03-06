﻿using System;
using System.Linq;
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
            Account account = new Account(new List<Transaction> { new Transaction() { ID = 1, Amount = -100, BalanceAccountNumber = 7654321 } }) { AccountNumber = 1 };
            //Act
            //Assert
            Assert.AreEqual(-100, account.Balance);
        }

        [TestMethod]
        public void AccountGetTransactionHistoryEmpty()
        {
            //Arrange
            Account account = new Account() { AccountNumber = 13};
            int expectedResult = 0;


            //Act
            int actualResult = account.GetAllTransactions().Count();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AccountGetTransactionHistory2Transactions()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };
            int expectedResult = 2;

            //Act
            int actualResult = account.GetAllTransactions().Count();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AccountBalanceEqualsSumTransactions()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };
            int expectedResult = 300;

            //Act
            double actualResult = account.Balance;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AccountDepositAddsTransaction()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };
            int expectedResult = 3;

            //Act
            account.Deposit(300.0);

            int actualResult = account.GetAllTransactions().Count();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AccountDepositIncreasesBalance()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };
            int expectedResult = 600;

            //Act
            account.Deposit(300.0);
            double actualResult = account.Balance;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(AmountNegativeOrZeroException))]
        public void AccountDepositNegativeAmountGivesException()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };

            //Act
            account.Deposit(-300.0);
        }

        [TestMethod]
        public void AccountWithdrawAddsTransaction()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };
            int expectedResult = 3;

            //Act
            account.Withdraw(300.0);

            int actualResult = account.GetAllTransactions().Count();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AccountWithdrawDecreasesBalance()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };
            int expectedResult = 0;

            //Act
            account.Withdraw(300.0);
            double actualResult = account.Balance;
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(AmountNegativeOrZeroException))]
        public void AccountWithdrawNegativeAmountGivesException()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };

            //Act
            account.Withdraw(-300.0);
        }

        [TestMethod]
        [ExpectedException(typeof(OverdrawException))]
        public void AccountWithdrawTooMuchtGivesException()
        {
            //Arrange
            IEnumerable<Transaction> history = new List<Transaction>() 
                {
                    new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 7654321},
                    new Transaction(){ID = 2, Amount = 200, BalanceAccountNumber = 1234567}
                };
            Account account = new Account(history) { AccountNumber = 13 };

            //Act
            account.Withdraw(300.01);
        }
        
    }

}
