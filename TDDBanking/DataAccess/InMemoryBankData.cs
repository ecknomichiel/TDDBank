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
            { new Account(new List<Transaction>{ new Transaction(){ID = 1, Amount = -100, BalanceAccountNumber = 7654321}}) { AccountNumber = 1234567, Balance = -100 }, 
                new Account(new List<Transaction>{ new Transaction(){ID = 2, Amount = 100, BalanceAccountNumber = 1234567}}) { AccountNumber = 7654321, Balance = 100 } };
        public ICollection<Models.Account> GetAllAccounts()
        {
            return accounts;
        }
    }
}