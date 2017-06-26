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
        private Bank bank;

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

        #region Constructors
        public HomeController(Bank bank)
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