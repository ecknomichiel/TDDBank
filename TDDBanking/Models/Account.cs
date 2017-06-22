using System;
using System.Collections.Generic;

namespace TDDBanking.Models
{
    public class Account
    {
        private List<Transaction> transactions = new List<Transaction>();
        public int AccountNumber { get; set; }
        public double Balance { get; set; }

        public Account()
        {

        }
        
        public Account(IEnumerable<Transaction> storedTransactions)
        { //Use ow list to prevent adding/removing transactions without going through the proper methods
            transactions.AddRange(storedTransactions);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return transactions;
        }
    }
}