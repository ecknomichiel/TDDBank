﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using TDDBanking;
using TDDBanking.Controllers;
using TDDBanking.DataAccess;
using TDDBanking.Models;

namespace TDDBankingTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexHasTitleHome()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home", result.ViewBag.Title);
        }

        [TestMethod]
        public void IndexHasCompanynameTDDBanken()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TDD Banken", result.ViewBag.CompanyName);
        }

        [TestMethod]
        public void IndexShowsAll3BankAccounts()
        {
            //Arrange
            ICollection<Account> expectedResult = new List<Account>() { 
                new Account(),
                new Account(),
                new Account()
            };
            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAllAccounts()).Returns(expectedResult).MustBeCalled();

            // Arrange
            HomeController controller = new HomeController(fakeBank);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Account>));
            Assert.IsTrue((expectedResult as IEnumerable<Account>).SequenceEqual<Account>(result.Model as IEnumerable<Account>));
        }

        [TestMethod]
        public void BalanceShowsAllBalances()
        {
            //Arrange
            ICollection<Account> expectedResult = new List<Account>() { 
                new Account(){AccountNumber = 1},
                new Account(){AccountNumber = 3},
                new Account(){AccountNumber = 2}
            };
            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAllAccounts()).Returns(expectedResult).MustBeCalled();

            HomeController controller = new HomeController(fakeBank);

            // Act
            ViewResult result = controller.Balance() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Account>));
            Assert.IsTrue((expectedResult as IEnumerable<Account>).SequenceEqual<Account>(result.Model as IEnumerable<Account>));
        }

        [TestMethod]
        public void BalanceGiven7ShowsBalanceForAccount7()
        {
            //Arrange
            Account expectedResult = new Account() { AccountNumber = 7 };

            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAccountByNumber(7)).Returns(expectedResult).MustBeCalled();

            HomeController controller = new HomeController(fakeBank);

            // Act
            ViewResult result = controller.Balance(7) as ViewResult;
            Account actualResult = (result.Model as IEnumerable<Account>).First();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TransactionsGivenValidAccountNrReturnsAccount()
        {
            //Arrange
            Account expectedResult = new Account(new Transaction[] { 
                new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 987654},
                new Transaction(){ID = 2, Amount = -0.05, BalanceAccountNumber = 123456}}) { AccountNumber = 7 };

            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAccountByNumber(7)).Returns(expectedResult).MustBeCalled();

            HomeController controller = new HomeController(fakeBank);

            // Act
            ViewResult result = controller.Transactions(7) as ViewResult;
            Account actualResult = result.Model as Account;

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TransactionsGivenInValidAccountNrReturnsHttpNotFound()
        {
            //Arrange
            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAccountByNumber(-13)).Returns(null as Account);

            HomeController controller = new HomeController(fakeBank);

            // Act
            ActionResult actualResult = controller.Transactions(-13);

            // Assert
            Assert.IsInstanceOfType(actualResult, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void WithdrawZeroGivesError()
        {
            //Arrange
            Account testAccount = new Account(new Transaction[] { 
                new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 987654},
                new Transaction(){ID = 2, Amount = -0.05, BalanceAccountNumber = 123456}}) { AccountNumber = 7 };

            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAccountByNumber(7)).Returns(testAccount);

            HomeController controller = new HomeController(fakeBank);
            double expectedResult = testAccount.Balance;

            // Act
            ViewResult result = controller.Withdraw(7, 0) as ViewResult;
            Account account = result.Model as Account;
            string actualResult = result.ViewBag.ErrorMessage;

            // Assert
            Assert.IsTrue(actualResult.StartsWith("Cannot withdraw an amount less than or equal 0."));
            Assert.AreEqual(expectedResult, account.Balance); //No change done to the account
        }

        [TestMethod]
        public void WithdrawNegGivesError()
        {
            //Arrange
            Account testAccount = new Account(new Transaction[] { 
                new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 987654},
                new Transaction(){ID = 2, Amount = -0.05, BalanceAccountNumber = 123456}}) { AccountNumber = 7 };

            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAccountByNumber(7)).Returns(testAccount);

            HomeController controller = new HomeController(fakeBank);
            double expectedResult = testAccount.Balance;

            // Act
            ViewResult result = controller.Withdraw(7, -300.1) as ViewResult;
            Account account = result.Model as Account;
            string actualResult = result.ViewBag.ErrorMessage;

            // Assert
            Assert.IsTrue(actualResult.StartsWith("Cannot withdraw an amount less than or equal 0."));
            Assert.AreEqual(expectedResult, account.Balance); //No change done to the account
        }

        [TestMethod]
        public void WithdrawMoreThanBalanceGivesError()
        {
            //Arrange
            Account testAccount = new Account(new Transaction[] { 
                new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 987654},
                new Transaction(){ID = 2, Amount = -0.05, BalanceAccountNumber = 123456}}) { AccountNumber = 7 };
            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetAccountByNumber(7)).Returns(testAccount).MustBeCalled();

            HomeController controller = new HomeController(fakeBank);
            double expectedResult = testAccount.Balance;

            // Act
            ViewResult result = controller.Withdraw(7, 100) as ViewResult;
            Assert.IsNotNull(result);

            Account account = result.Model as Account;
            string actualResult = result.ViewBag.ErrorMessage;

            // Assert
            Assert.IsTrue(actualResult.Contains("Insufficient funds."));
            Assert.AreEqual(expectedResult, account.Balance); //No change done to the account
        }

        [TestMethod]
        public void CustomerWithExistingIdReturnsCustomer()
        {
            //Arrange
            Customer testCustomer = new Customer() { Id = 7, Name = "Hans Andersson"};
            Account testAccount = new Account    
                (new Transaction[] { 
                new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 987654},
                new Transaction(){ID = 2, Amount = -0.05, BalanceAccountNumber = 123456}}) { AccountNumber = 7 };

            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetCustomerById(7)).Returns(testCustomer).MustBeCalled();

            HomeController controller = new HomeController(fakeBank);
            Customer expectedResult = testCustomer;

            // Act
            ViewResult result = controller.Customer(7) as ViewResult;

            Customer actualResult = result.Model as Customer;


            // Assert

            Assert.AreEqual(expectedResult, actualResult); 
        }

        [TestMethod]
        public void CustomerWithNonExistingIdReturnsHttpNotFound()
        {
            //Arrange
            Customer testCustomer = new Customer() { Id = 7, Name = "Hans Andersson" };
            Account testAccount = new Account
                (new Transaction[] { 
                new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 987654},
                new Transaction(){ID = 2, Amount = -0.05, BalanceAccountNumber = 123456}}) { AccountNumber = 7 };
            Customer[] allCustomers = { testCustomer };

            IBank fakeBank = Mock.Create<IBank>();
            Mock.Arrange(() => fakeBank.GetCustomerById(501)).Returns(null as Customer).MustBeCalled();

            HomeController controller = new HomeController(fakeBank);
            // Act
            ActionResult result = controller.Customer(501);


            // Assert

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }
    }
}
