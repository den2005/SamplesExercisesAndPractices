using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using VS2017.BankSystem.AdoNetClsLib;

namespace VS2017.BankSystem.UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private IDataAccessLayer _dataLib;

        public UnitTest1()
        {
            _dataLib = new DataAccessLayer();
        }


        [TestMethod]
        public void TestGetAllBankAccounts()
        {
            List<BankAccount> accts = _dataLib.GetAllBankAccounts();
            int expectedRecords = 2;
            int actualRecords = accts.Count;

            Assert.AreEqual(expectedRecords, actualRecords);
        }

        [TestMethod]
        public void TestGetBankAccountByAccountNumber()
        {
            string expectedAccountNumber = "";
            string expectedLoginName = "";
            
            BankAccount acct = _dataLib.GetBankAccountByAccountNumber("");            
            string actualAccountNumber = acct.AccountNumber;
            string actualLoginName = acct.LoginName;

            Assert.AreEqual(expectedLoginName, actualLoginName);
            Assert.AreEqual(expectedAccountNumber, actualAccountNumber);
        }

        [TestMethod]
        public void TestGetBankAccountByLoginName()
        {
            string expectedAccountNumber = "";
            string expectedLoginName = "";

            BankAccount acct = _dataLib.GetBankAccountByLoginName("");
            string actualAccountNumber = acct.AccountNumber;
            string actualLoginName = acct.LoginName;

            Assert.AreEqual(expectedLoginName, actualLoginName);
            Assert.AreEqual(expectedAccountNumber, actualAccountNumber);
        }

        [TestMethod]
        public void TestInsertNewBankAccount()
        {
            BankAccount newAcct = GenerateBankAccount();
            _dataLib.InsertNewBankAccount(newAcct);

            BankAccount dbAcct = _dataLib.GetBankAccountByAccountNumber(newAcct.AccountNumber);

            Assert.AreEqual(newAcct.AccountNumber, dbAcct.AccountNumber);
            Assert.AreEqual(newAcct.LoginName, dbAcct.LoginName);
            Assert.AreEqual(newAcct.Password, dbAcct.Password);
            Assert.AreEqual(newAcct.Balance, dbAcct.Balance);
        }

        [TestMethod]
        public void TestUpdateBankAccount()
        {
            BankAccount existAcct = _dataLib.GetBankAccountByAccountNumber("2019101000002");

            existAcct.LoginName = "testme2019";
            existAcct.Balance = 90;
            double transactionAmount = 10;
            string transactionType = "withdrawal";

            _dataLib.UpdateBankAccount(existAcct, transactionType, transactionAmount);

            BankAccount dbAcct = _dataLib.GetBankAccountByAccountNumber("2019101000002");

            Assert.AreEqual(existAcct.LoginName, dbAcct.LoginName);
            Assert.AreEqual(existAcct.AccountNumber, dbAcct.AccountNumber);
            Assert.AreEqual(existAcct.Balance, dbAcct.Balance);
        }

        private BankAccount GenerateBankAccount()
        {
            return new BankAccount() {
                AccountNumber = "2019101000002",
                LoginName = "testuser2019",
                Password = "testuser2019",
                Balance = 100.00,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
