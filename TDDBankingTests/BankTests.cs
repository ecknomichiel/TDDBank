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
        public void ConstructorGivesBank()
        {
            //Arrange
            
            //Act
            Bank actualResult = new Bank();
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(Bank));
        }

        [TestMethod]
        public void GetAllAccountsGivesListOfAccounts()
        {
            //Arrange
            Bank bank = new Bank();
            IEnumerable<Account> expectedResult = new List<Account>();
            //Act
            IEnumerable<Account> actualResult = bank.GetAllAccounts();
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(IEnumerable<Account>));
        }

        [TestMethod]
        public void GetAllAccountsGivesAllAccounts()
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
    }
}
