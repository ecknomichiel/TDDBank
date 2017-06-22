using System;
using System.Collections.Generic;
using TDDBanking.DataAccess;

namespace TDDBanking.Models
{
    public class Bank
    {
        public IEnumerable<Account> GetAllAccounts()
        {
            return new List<Account>();
        }

        public Bank()
        {

        }

        public Bank(IBankData context)
        {

        }
    }
}