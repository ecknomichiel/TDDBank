using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDDBanking.Models;

namespace TDDBanking.Controllers
{
    public class HomeController : Controller
    {
        private IBank bank;

        public ActionResult Index()
        {
            
            return View(bank.GetAllAccounts());
        }

        public ActionResult Balance(int accountNumber = 0)
        {
            IEnumerable<Account> resultList;
            if (accountNumber != 0)
            {
                resultList = new List<Account>() { bank.GetAccountByNumber(accountNumber) };
            }
            else
            {
                resultList = bank.GetAllAccounts();
            }
            
            return View(resultList);
        }

        public ActionResult Transactions(int Id)
        {
            Account account = bank.GetAccountByNumber(Id);
            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        public ActionResult Deposit(int accountNumber, double amount)
        {
            Account account = bank.GetAccountByNumber(accountNumber);
            if (account == null)
            {
                return HttpNotFound();
            }

            try 
	        {
                account.Deposit(amount);
	        }
	        catch (Exception e)
	        {
                if (e.GetType() == typeof(AmountNegativeOrZeroException))
                {
                    ViewBag.ErrorMessage = "Cannot deposit an amount less than or equal 0. Amount to deposit: " + amount.ToString() + ". Use withdraw to withdraw money.";
                }
                else
                {
                    throw;
                }        
	        }
            
            return  View("Transactions", account);
        }

        public ActionResult Withdraw(int accountNumber, double amount)
        {
            Account account = bank.GetAccountByNumber(accountNumber);
            if (account == null)
            {
                return HttpNotFound();
            }

            try
            {
                account.Withdraw(amount);
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(AmountNegativeOrZeroException))
                {
                    ViewBag.ErrorMessage = "Cannot withdraw an amount less than or equal 0. Amount to withdraw: "+ amount.ToString() +". Use deposit to deposit money.";
                }
                else if (e.GetType() == typeof(OverdrawException))
                {
                    ViewBag.ErrorMessage = "Cannot withdraw "+ amount.ToString() +". Insufficient funds.";
                }
                else
                {
                    throw;
                }
            }

            return View("Transactions", account);
        }

        public ActionResult Customer(int Id)
        {
            Customer customer = bank.GetCustomerById(Id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        #region Constructors
        public HomeController(IBank bank)
        {
            this.bank = bank;
            ViewBag.Title = "Home";
            ViewBag.CompanyName = "TDD Banken";
        }
        public HomeController()
        {
            bank = new Bank();
            ViewBag.Title = "Home";
            ViewBag.CompanyName = "TDD Banken";
        }
        #endregion
    }
}