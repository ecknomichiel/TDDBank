using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDBanking.Models;

namespace TDDBankingTests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void AddAccountAddsAccount()
        {
            //Arrange
            Customer user = new Customer() { Id = 1, Name = "Test User" };
            int expectedResult = 1;
            //Act
            user.AddAccount(new Account());
            int actualResult = user.GetAccounts().Count();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RemoveAccountRemovesAccount()
        {
            //Arrange
            Account acc = new Account();
            Customer user = new Customer() { Id = 1, Name = "Test User" };
            user.AddAccount(acc);
            int expectedResult = 0;
            //Act
            user.RemoveAccount(acc);
            int actualResult = user.GetAccounts().Count();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
