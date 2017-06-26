using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDDBanking.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public int BalanceAccountNumber { get; set; }


    }

    public class AmountNegativeOrZeroException : Exception
    { }
    public class OverdrawException: Exception
    { }
}