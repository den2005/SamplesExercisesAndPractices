using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VS2017.BankSystem.AdoNetLibrary;
using VS2017.MvcAdoNet.BankSystem.Models;

namespace VS2017.MvcAdoNet.BankSystem.Controllers
{
    public class BankTransactionController : Controller
    {
        //private IDataAccessLibrary lib = new DataAccessLibrary();
        private IBSRepository rep = new BSRepository();
        private string _loginName = "";

        // GET: BankTransaction
        public ActionResult Index(string loginName)
        {
            BankAccount acct = rep.RetrieveBankAccountInfo(loginName);

            //_loginName = loginName;
            if (!string.IsNullOrEmpty(loginName))
            {
                HttpContext.Session.SetString("loginName", loginName);
            }
            

            return View(acct);
        }

        // GET: BankTransaction/ViewTransaction/5
        public ActionResult ViewTransaction(int id)
        {
            List<TransactionHistory> trans = rep.GetTransactionHistoryRecords(id);

            var login = HttpContext.Session.GetString("loginName");
            if (login != null)
            {
                ViewBag.LoginName = login.ToString();
            }

            return View(trans);
        }

        // GET: BankTransaction/Deposit/5
        public ActionResult Deposit(int id)
        {
            BankAccount acct = rep.RetrieveBankAccountInfo(id);

            BankTransaction trans = new BankTransaction()
            {
                SourceBankAccountId = acct.Id,
                SourceAccountNumber = acct.AccountNumber,
                TransactionType = "Deposit",
                TransactionDate = DateTime.UtcNow
            };

            var login = HttpContext.Session.GetString("loginName");
            if (login != null)
            {
                ViewBag.LoginName = login.ToString();
            }

            return View(trans);
        }

        // GET: BankTransaction/Withdraw/5
        public ActionResult Withdraw(int id)
        {
            BankAccount acct = rep.RetrieveBankAccountInfo(id);

            BankTransaction trans = new BankTransaction()
            {
                SourceBankAccountId = acct.Id,
                SourceAccountNumber = acct.AccountNumber,
                TransactionType = "Withdraw",
                TransactionDate = DateTime.UtcNow
            };

            var login = HttpContext.Session.GetString("loginName");
            if (login != null)
            {
                ViewBag.LoginName = login.ToString();
            }

            return View(trans);
        }

        // GET: BankTransaction/Transfer/5
        public ActionResult Transfer(int id)
        {
            BankAccount acct = rep.RetrieveBankAccountInfo(id);

            BankTransaction trans = new BankTransaction() {
                SourceBankAccountId = acct.Id,
                SourceAccountNumber = acct.AccountNumber,
                TransactionType = "Transfer",
                TransactionDate = DateTime.UtcNow
            };

            var login = HttpContext.Session.GetString("loginName");
            if (login != null)
            {
                ViewBag.LoginName = login.ToString();
            }

            return View(trans);
        }
        
        // POST: BankTransaction/Deposit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit(int id, IFormCollection collection)
        {
            BankTransaction trans = null;
            try
            {
                string result = "";
                string acctNumber = collection["SourceAccountNumber"].ToString();
                string transAmt = collection["TransactionAmount"].ToString();
                string transType = collection["TransactionType"].ToString();
                string loginName = "";

                trans = new BankTransaction()
                {
                    SourceBankAccountId = id,
                    SourceAccountNumber = acctNumber,
                    TransactionType = transType,
                    TransactionAmount = Convert.ToDouble(transAmt),
                    TransactionDate = DateTime.UtcNow
                };

                var login = HttpContext.Session.GetString("loginName");
                if (login != null)
                {
                    ViewBag.LoginName = login.ToString();
                }

                if (!string.IsNullOrEmpty(acctNumber) && !string.IsNullOrEmpty(transAmt) && !string.IsNullOrEmpty(transType))
                {                 

                    result = rep.ProcessBankTransaction(trans);
                }
                else if (string.IsNullOrEmpty(acctNumber) || string.IsNullOrEmpty(transAmt) || string.IsNullOrEmpty(transType))
                {
                    result = "Account Number and Transaction Amount must not be empty";
                    
                }
                if (result.Contains("Successfull"))
                {
                    _loginName = HttpContext.Session.GetString("loginName");

                    return RedirectToAction("Index", new { loginName = _loginName });
                }
                else
                {
                    ViewBag.Message = result;

                    return View(trans);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

                return View(trans);
            }
        }

        // POST: BankTransaction/Withdraw/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw(int id, IFormCollection collection)
        {
            BankTransaction trans = null;
            try
            {
                string result = "";
                string acctNumber = collection["SourceAccountNumber"].ToString();
                string transAmt = collection["TransactionAmount"].ToString();
                string transType = collection["TransactionType"].ToString();

                trans = new BankTransaction()
                {
                    SourceBankAccountId = id,
                    SourceAccountNumber = acctNumber,
                    TransactionType = transType,
                    TransactionAmount = Convert.ToDouble(transAmt),
                    TransactionDate = DateTime.UtcNow
                };

                var login = HttpContext.Session.GetString("loginName");
                if (login != null)
                {
                    ViewBag.LoginName = login.ToString();
                }

                if (!string.IsNullOrEmpty(acctNumber) && !string.IsNullOrEmpty(transAmt) && !string.IsNullOrEmpty(transType))
                {     

                    result = rep.ProcessBankTransaction(trans);
                }
                else if (string.IsNullOrEmpty(acctNumber) || string.IsNullOrEmpty(transAmt) || string.IsNullOrEmpty(transType))
                {
                    result = "Account Number and Transaction Amount must not be empty";

                }

                if (result.Contains("Successfull"))
                {
                    _loginName = HttpContext.Session.GetString("loginName");

                    return RedirectToAction("Index", new { loginName = _loginName });
                }
                else
                {
                    ViewBag.Message = result;
                    return View(trans);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(trans);
            }
        }

        // POST: BankTransaction/Transfer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer(int id, IFormCollection collection)
        {
            BankTransaction trans = null;

            try
            {
                string result = "";
                string sourceAcctNumber = collection["SourceAccountNumber"].ToString();
                string targetAcctNumber = collection["TargetAccountNumber"].ToString();
                string transAmt = collection["TransactionAmount"].ToString();
                string transType = collection["TransactionType"].ToString();

                trans = new BankTransaction()
                {
                    SourceBankAccountId = id,
                    SourceAccountNumber = sourceAcctNumber,
                    TargetAccountNumber = targetAcctNumber,
                    TransactionType = transType,
                    TransactionAmount = Convert.ToDouble(transAmt),
                    TransactionDate = DateTime.UtcNow
                };

                var login = HttpContext.Session.GetString("loginName");
                if (login != null)
                {
                    ViewBag.LoginName = login.ToString();
                }

                if (!string.IsNullOrEmpty(sourceAcctNumber) && !string.IsNullOrEmpty(targetAcctNumber) && !string.IsNullOrEmpty(transAmt) && !string.IsNullOrEmpty(transType))
                {                   

                    result = rep.ProcessBankTransaction(trans);
                }
                else if (string.IsNullOrEmpty(sourceAcctNumber) || string.IsNullOrEmpty(targetAcctNumber) || string.IsNullOrEmpty(transAmt) || string.IsNullOrEmpty(transType))
                {
                    result = "Source Account Number and Target Source Account Number and Transaction Amount must not be empty";
                }

                if (result.Contains("Successfull"))
                {
                    _loginName = HttpContext.Session.GetString("loginName");

                    return RedirectToAction("Index", new { loginName = _loginName });
                }
                else
                {
                    ViewBag.Message = result;

                    return View(trans);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;

                return View(trans);
            }
        }

        
    }
}