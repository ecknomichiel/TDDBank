using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDDBanking.Models;

namespace TDDBanking.DataAccess
{
    public class InMemoryBankData: IBankData
    {
        private List<Account> accounts = new List<Account>() { new Account() { AccountNumber = 1, Balance = -100 }, new Account() { AccountNumber = 7654321, Balance = 100 } };
        public ICollection<Models.Account> GetAllAccounts()
        {
            return accounts;
        }
    }
}