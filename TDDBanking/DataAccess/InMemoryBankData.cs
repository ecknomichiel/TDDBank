using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDDBanking.Models;

namespace TDDBanking.DataAccess
{
    public class InMemoryBankData: IBankData
    {
        private List<Account> accounts = new List<Account>() 
        //Sample data
            { new Account(new List<Transaction>{ new Transaction(){ID = 1, Amount = -100, BalanceAccountNumber = 7654321, TransactionDate = DateTime.UtcNow}}) { AccountNumber = 1234567 }, 
                new Account(new List<Transaction>{ new Transaction(){ID = 2, Amount = 100, BalanceAccountNumber = 1234567, TransactionDate = DateTime.UtcNow}}) { AccountNumber = 7654321} };
        public ICollection<Models.Account> GetAllAccounts()
        {
            return accounts;
        }
    }
}