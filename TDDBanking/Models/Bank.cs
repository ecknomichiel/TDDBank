using System;
using System.Collections.Generic;

namespace TDDBanking.Models
{
    public class Bank
    {
        public IEnumerable<Account> GetAllAccounts()
        {
            return new List<Account>();
        }
    }
}