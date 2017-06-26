using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDDBanking.Models;

namespace TDDBanking.DataAccess
{
    public class InMemoryBankData: IBankData
    {
        private static InMemoryBankData instance;
        private List<Account> accounts = new List<Account>();
        //Sample data
            
        private List<Customer> customers = new List<Customer>();
        
        private void LoadSampleData()
        {
            Account acc1 = new Account(new List<Transaction>{ new Transaction(){ID = 1, Amount = -100, BalanceAccountNumber = 7654321, TransactionDate = DateTime.UtcNow}}) { AccountNumber = 1234567 };
            Account acc2 = new Account(new List<Transaction>{ new Transaction(){ID = 2, Amount = 100, BalanceAccountNumber = 1234567, TransactionDate = DateTime.UtcNow}}) { AccountNumber = 7654321};
            Customer cust1 = new Customer() { Id = 1, Name = "M.I. Customer" };
            cust1.AddAccount(acc1);
        }
        public ICollection<Models.Account> GetAllAccounts()
        {
            return accounts;
        }

        public ICollection<Models.Customer> GetAllCustomers()
        {
            return customers;
        }

        public static IBankData Instance 
        {
            get 
            { 
                if (instance == null)
                {
                    instance = new InMemoryBankData();
                    instance.LoadSampleData();
                }
                return instance;
            }
        }
    }
}