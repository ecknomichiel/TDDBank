using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDDBanking.Models
{
    public class Customer
    {
        private List<Account> accounts = new List<Account>();
        public int Id { get; set; }
        public string Name { get; set; }

        public void AddAccount(Account account)
        {
            if (!accounts.Contains(account))
            {
                accounts.Add(account);
            }
        }

        public void RemoveAccount(Account account)
        {
            accounts.Remove(account);
        }
        
        public IEnumerable<Account> GetAccounts()
        {
            return accounts;
        }
    }
}