using System;
using System.Collections.Generic;
using System.Linq;
using TDDBanking.DataAccess;

namespace TDDBanking.Models
{
    public class Bank: IBank
    {
        private IBankData context;

        public IEnumerable<Account> GetAllAccounts()
        {
            return context.GetAllAccounts() as IEnumerable<Account>;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return context.GetAllCustomers();
        }

        public Customer GetCustomerById(int Id)
        {
            return context.GetAllCustomers().SingleOrDefault(cu => cu.Id == Id);
        }

        public Account GetAccountByNumber(int accountNumber)
        {
            return context.GetAllAccounts().SingleOrDefault(ac => ac.AccountNumber == accountNumber);
        }

        public Bank(IBankData context)
        {
            if (context == null)
                throw new ArgumentNullException("Context may not be null when constructing a bank");
            this.context = context;
        }

        public Bank()
        {
            context = InMemoryBankData.Instance;
        }

        
    }
}