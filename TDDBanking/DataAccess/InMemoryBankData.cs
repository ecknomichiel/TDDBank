using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDDBanking.Models;

namespace TDDBanking.DataAccess
{
    public class InMemoryBankData: IBankData
    {
        private List<Account> accounts = new List<Account>();
        public ICollection<Models.Account> GetAllAccounts()
        {
            return accounts;
        }
    }
}