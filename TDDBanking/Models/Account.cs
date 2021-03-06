﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TDDBanking.Models
{
    public class Account
    {
        private List<Transaction> transactions = new List<Transaction>();
        public int AccountNumber { get; set; }
        public double Balance { get { return GetBalance(); } }

        private double GetBalance()
        {
            return transactions.Sum(tr => tr.Amount);
        }

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

        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new AmountNegativeOrZeroException();
            //Todo add transaction to balancing account (internal cash account)
            Transaction trans = new Transaction() { Amount = amount, TransactionDate = DateTime.UtcNow };
            transactions.Add(trans);
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new AmountNegativeOrZeroException();
            if (amount > Balance)
                throw new OverdrawException();
            //Todo add transaction to balancing account (internal cash account)
            Transaction trans = new Transaction() { Amount = -amount, TransactionDate = DateTime.UtcNow };
            transactions.Add(trans);
        }
    }
}