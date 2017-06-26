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

        [TestMethod]
        public void GetAccountByExistingNumberGivesAccount()
        {
            //Arrange
            Account expectedResult = new Account() { AccountNumber = 7 };
            ICollection<Account> allAccounts = new List<Account>() { 
                new Account() {AccountNumber = 1},
                new Account() {AccountNumber = 1},
                new Account() {AccountNumber = 2},
                expectedResult
            };
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(allAccounts);
            Bank bank = new Bank(fakeDb);
            //Act
            Account actualResult = bank.GetAccountByNumber(7);
            //Assert

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetAccountByNonExistingNumberGivesNull()
        {
            //Arrange
            Account expectedResult = null;
            ICollection<Account> allAccounts = new List<Account>() { 
                new Account() {AccountNumber = 1},
                new Account() {AccountNumber = 1},
                new Account() {AccountNumber = 2},
                new Account() { AccountNumber = 7}
            };
            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllAccounts()).Returns(allAccounts);
            Bank bank = new Bank(fakeDb);
            //Act
            Account actualResult = bank.GetAccountByNumber(100);
            //Assert

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetAllCustomersGiven2CutomersReturns2Customers()
        {
            List<Customer> cData = new List<Customer>(){
                new Customer() { Id = 1, Name = "First M.I. Customer" },
                new Customer() { Id = 2, Name = "Second Client" }
            };

            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllCustomers()).Returns(cData).MustBeCalled();
            Bank bank = new Bank(fakeDb);
            //Act
            IEnumerable<Customer> actualresult = bank.GetAllCustomers();
            //Assert
            Assert.IsTrue((cData).SequenceEqual(actualresult));
        }

        [TestMethod]
        public void GetCustomerByExistingIdReturnsCustomer()
        {
            List<Customer> cData = new List<Customer>(){
                new Customer() { Id = 900, Name = "First M.I. Customer" },
                new Customer() { Id = 2, Name = "Second Client" }
            };

            Customer expectedResult = cData[0];

            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllCustomers()).Returns(cData).MustBeCalled();
            Bank bank = new Bank(fakeDb);
            //Act
            Customer actualresult = bank.GetCustomerById(900);
            //Assert
            Assert.AreEqual(expectedResult, actualresult);
        }

        [TestMethod]
        public void GetCustomerByNonExistingIdReturnsNull()
        {
            List<Customer> cData = new List<Customer>(){
                new Customer() { Id = 900, Name = "First M.I. Customer" },
                new Customer() { Id = 2, Name = "Second Client" }
            };

            Customer expectedResult = null;

            IBankData fakeDb = Mock.Create<IBankData>();
            Mock.Arrange(() => fakeDb.GetAllCustomers()).Returns(cData).MustBeCalled();
            Bank bank = new Bank(fakeDb);
            //Act
            Customer actualresult = bank.GetCustomerById(501);
            //Assert
            Assert.AreEqual(expectedResult, actualresult);
        }
    }
}
