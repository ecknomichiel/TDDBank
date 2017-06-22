using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDBanking.Models;

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
            Bank actualResult = bank.GetAllAccounts();
            //Assert
            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult, typeof(IEnumerable<Account>));
        }
    }
}
