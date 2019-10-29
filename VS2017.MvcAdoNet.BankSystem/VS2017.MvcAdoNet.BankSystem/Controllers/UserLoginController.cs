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
    public class UserLoginController : Controller
    {
        private IBSRepository rep = new BSRepository();

        // GET: UserLogin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            if (collection != null)
            {
                
                IBSRepository rep = new BSRepository();

                UserLogin login = rep.GetUserLoginInfo(collection["LoginName"].ToString());

                if (login != null && login.Password == collection["Password"].ToString())
                {
                    return RedirectToAction("Index", "BankTransaction", new { loginName = login.LoginName });
                }
                else
                {
                    return View();
                }                
                
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult CreateUserLogin()
        {
            BankAccount newAcct = new BankAccount() {
                AccountNumber=rep.GenerateNewAccountNumber(),
                Balance = 0,
                CreatedDate = DateTime.UtcNow
            };
            return View(newAcct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUserLogin(IFormCollection collection)
        {
            if (collection != null)
            {

                IBSRepository rep = new BSRepository();

                BankAccount acct = new BankAccount()
                {
                    LoginName = collection["LoginName"].ToString(),
                    Password = collection["Password"].ToString(),
                    AccountNumber = collection["AccountNumber"].ToString(),
                    Balance = Convert.ToDouble(collection["Balance"]),
                    CreatedDate = Convert.ToDateTime(collection["CreatedDate"])
                };

                //Check if LoginName exists in database
                UserLogin login = rep.GetUserLoginInfo(acct.LoginName);                

                if (login != null)
                {
                    ViewBag.Message = "Login Name already exists in database. Please modify them.";                    

                    return View(acct);
                }
                else
                {
                    rep.InsertNewBankAccount(acct, "Open New Account");

                    return RedirectToAction("Index", "UserLogin", new { loginName = acct.LoginName });
                }

            }
            else
            {
                return View();
            }
        }

        
    }
}