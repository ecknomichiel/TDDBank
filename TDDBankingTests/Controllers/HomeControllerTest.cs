using System;
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
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(expectedResult).MustBeCalled();
            Bank bank = new Bank(fakeDb);

            // Arrange
            HomeController controller = new HomeController(bank);

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
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(expectedResult).MustBeCalled();
            Bank bank = new Bank(fakeDb);
            HomeController controller = new HomeController(bank);

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
            ICollection<Account> allAccounts = new List<Account>() { 
                new Account(){AccountNumber = 1},
                new Account(){AccountNumber = 3},
                expectedResult,
                new Account(){AccountNumber = 2}
            };
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(allAccounts);
            Bank bank = new Bank(fakeDb);
            HomeController controller = new HomeController(bank);

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
            ICollection<Account> allAccounts = new List<Account>() { 
                new Account(){AccountNumber = 1},
                new Account(){AccountNumber = 3},
                expectedResult,
                new Account(){AccountNumber = 2}
            };
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(allAccounts);
            Bank bank = new Bank(fakeDb);
            HomeController controller = new HomeController(bank);

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
            Account sampleAccount = new Account(new Transaction[] { 
                new Transaction(){ID = 1, Amount = 100, BalanceAccountNumber = 987654},
                new Transaction(){ID = 2, Amount = -0.05, BalanceAccountNumber = 123456}}) { AccountNumber = 7 };
            ICollection<Account> allAccounts = new List<Account>() { 
                new Account(){AccountNumber = 1},
                new Account(){AccountNumber = 3},
                sampleAccount,
                new Account(){AccountNumber = 2}
            };
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(allAccounts);
            Bank bank = new Bank(fakeDb);
            HomeController controller = new HomeController(bank);

            // Act
            ActionResult actualResult = controller.Transactions(-13);

            // Assert
            Assert.IsInstanceOfType(actualResult, typeof(HttpNotFoundResult));
        }
    }
}
