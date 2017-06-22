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
    }
}
