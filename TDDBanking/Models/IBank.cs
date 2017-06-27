using System;
using System.Collections.Generic;
/*  Public interface used to access a bank
 *  Used to decouple Bank and HomeController classes, 
 * thus making it possible to test the controller without testing the Bank
 */ 
namespace TDDBanking.Models
{
    public interface IBank
    {
        IEnumerable<Account> GetAllAccounts();

        IEnumerable<Customer> GetAllCustomers();

        Customer GetCustomerById(int Id);

        Account GetAccountByNumber(int accountNumber);
    }
}
