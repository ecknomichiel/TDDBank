using System;
using System.Collections.Generic;
using TDDBanking.DataAccess;

namespace TDDBanking.Models
{
    public class Bank
    {
        private IBankData context;

        public IEnumerable<Account> GetAllAccounts()
        {
            return context.GetAllAccounts() as IEnumerable<Account>;
        }

        public Bank(IBankData context)
        {
            this.context = context;
        }
    }
}