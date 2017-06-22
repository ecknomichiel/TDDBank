﻿using System;
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

        public HomeController(Bank bank)
        {
            this.bank = bank;
        }
        public HomeController()
        {
            //Todo: implement way to make a bank that gets his data from somewhere 
            bank = new Bank(null);
        }
    }
}