using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDBanking.Models;
using TDDBanking.DataAccess;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;

namespace TDDBankingTests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorgivenNullGivesException()
        {
            //Arrange
            Bank actualResult = new Bank(null);
            //Act
            //Assert
        }

        [TestMethod]
        public void ConstructorWithoutArgumentGivesBank()
        {
            //Arrange
            Bank actualResult = new Bank();
            //Act
            actualResult.GetAllAccounts(); // Gives exception if context == null
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(Bank));
        }

        [TestMethod]
        public void ConstructorGivenIBankDataGivesBank()
        {
            //Arrange
            IBankData fakeDb = Mock.Create<IBankData>();
            Bank actualResult = new Bank(fakeDb);
            //Act
            actualResult.GetAllAccounts(); // Gives exception if context == null
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(Bank));
        }

        [TestMethod]
        public void GetAllAccountsGivesListOfAccounts()
        {
            //Arrange
            ICollection<Account> expectedResult = new List<Account>() { 
                new Account(),
                new Account()
            };
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(expectedResult).MustBeCalled();
            Bank bank = new Bank(fakeDb);
            //Act
            IEnumerable<Account> actualResult = bank.GetAllAccounts();
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(IEnumerable<Account>));
        }

        [TestMethod]
        public void GetAllAccountsGivesAll2Accounts()
        {
            //Arrange
            ICollection<Account> expectedResult = new List<Account>() { 
                new Account(),
                new Account()
            };
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(expectedResult).MustBeCalled();
            Bank bank = new Bank(fakeDb);
            //Act
            IEnumerable<Account> actualResult = bank.GetAllAccounts();
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(IEnumerable<Account>));
            Assert.IsTrue((expectedResult as IEnumerable<Account>).SequenceEqual<Account>(actualResult));
        }

        [TestMethod]
        public void GetAllAccountsGivesAll3Accounts()
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
            //Act
            IEnumerable<Account> actualResult = bank.GetAllAccounts();
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(IEnumerable<Account>));
            Assert.IsTrue((expectedResult as IEnumerable<Account>).SequenceEqual<Account>(actualResult));
        }
    }
}
