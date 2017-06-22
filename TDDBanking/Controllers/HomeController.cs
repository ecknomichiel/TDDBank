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
            ViewBag.Title = "Home";
            ViewBag.CompanyName = "TDD Banken";
            return View(bank.GetAllAccounts());
        }

        public ActionResult Balance()
        {
            return null;
        }

        #region Constructors
        public HomeController(Bank bank)
        {
            this.bank = bank;
        }
        public HomeController()
        {
            bank = new Bank();
        }
        #endregion
    }
}