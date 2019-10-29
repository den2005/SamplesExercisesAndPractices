using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using VS2017.BankSystem.AdoNetLibrary;
using VS2017.MvcAdoNet.BankSystem.Models;

namespace VS2017.MvcAdoNet.BankSytem.MsTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private IDataAccessLibrary _dataLib;
        private IBSRepository _repository;

        public UnitTest1()
        {
            _dataLib = new DataAccessLibrary();
            _repository = new BSRepository();
        }


        [TestMethod]
        public void TestGetAllBankAccounts()
        {
            List<BankAccount> accts = _dataLib.GetAllBankAccounts().FindAll(x=> x.CreatedDate <= Convert.ToDateTime("2019-10-28"));
            int expectedRecords = 3;//this has to change when new login record is added
                        
            Assert.IsNotNull(accts);
            Assert.AreEqual(expectedRecords, accts.Count);
        }

        [TestMethod]
        public void TestGetBankAccountByAccountNumber()
        {
            string expectedAccountNumber = "20191000500201";
            string expectedLoginName = "testme";

            BankAccount acct = _dataLib.GetBankAccountByAccountNumber(expectedAccountNumber);

            Assert.IsNotNull(acct);
            Assert.AreEqual(expectedLoginName, acct.LoginName);
            Assert.AreEqual(expectedAccountNumber, acct.AccountNumber);
        }

        [TestMethod]
        public void TestGetBankAccountByLoginName()
        {
            string expectedAccountNumber = "20191000100001";
            string expectedLoginName = "testuser1";

            BankAccount acct = _dataLib.GetBankAccountByLoginName(expectedLoginName);

            Assert.IsNotNull(acct);
            Assert.AreEqual(expectedLoginName, acct.LoginName);
            Assert.AreEqual(expectedAccountNumber, acct.AccountNumber);
        }

        [TestMethod]
        public void TestInsertNewBankAccount()
        {
            BankAccount newAcct = GenerateBankAccount();
            _dataLib.InsertNewBankAccount(newAcct, "Create New Account");

            BankAccount dbAcct = _dataLib.GetBankAccountByAccountNumber(newAcct.AccountNumber);

            Assert.IsNotNull(dbAcct);
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

            Assert.IsNotNull(dbAcct);
            Assert.AreEqual(existAcct.LoginName, dbAcct.LoginName);
            Assert.AreEqual(existAcct.AccountNumber, dbAcct.AccountNumber);
            Assert.AreEqual(existAcct.Balance, dbAcct.Balance);
        }

        [TestMethod]
        public void TestRepGetUserLoginInfo()
        {
            string expectedLogin = "test1";
            UserLogin login = _repository.GetUserLoginInfo(expectedLogin);

            Assert.IsNotNull(login);
            Assert.AreEqual(expectedLogin, login.LoginName);
        }

        [TestMethod]
        public void TestRepValidateUserLogin()
        {
            string expectedLogin = "test1";
            string expectedValidPassword = "test1";//if change in db, this value must also be changed

            string expectedInvalidPassword = "testno";

            bool actualIsValid = _repository.ValidateUserLogin(expectedLogin, expectedValidPassword);
            bool actualIsInvalid = _repository.ValidateUserLogin(expectedLogin, expectedInvalidPassword);

            Assert.IsTrue(actualIsValid);
            Assert.IsFalse(actualIsInvalid);
        }

        [TestMethod]
        public void TestRepRetrieveBankAccountInfoByLoginName()
        {
            string expectedLoginName = "test1";
            string expectedAccountNumber = "20191000600123";
            BankAccount actualAcct = _repository.RetrieveBankAccountInfo(expectedLoginName);

            Assert.IsNotNull(actualAcct);
            Assert.AreEqual(expectedLoginName, actualAcct.LoginName);
            Assert.AreEqual(expectedAccountNumber, actualAcct.AccountNumber);
        }


        [TestMethod]
        public void TestRepRetrieveBankAccountInfoById()
        {
            int expectedBankAcctId = 3;
            string expectedLoginName = "test1";
            string expectedAccountNumber = "20191000600123";
            BankAccount actualAcct = _repository.RetrieveBankAccountInfo(expectedBankAcctId);

            Assert.IsNotNull(actualAcct);
            Assert.AreEqual(expectedBankAcctId, actualAcct.Id);
            Assert.AreEqual(expectedLoginName, actualAcct.LoginName);
            Assert.AreEqual(expectedAccountNumber, actualAcct.AccountNumber);
        }

        [TestMethod]
        public void TestRepGetTransactionHistoryRecordsByAcctNumber()
        {
            //this test assures passed if values in database was not modified after this test was written
            string expectedAccountNumber = "20191000600123";
            int expectedRecordCount = 8;
            List<TransactionHistory> trans = _repository.GetTransactionHistoryRecords(expectedAccountNumber);

            //Filter by transaction date
            trans = trans.FindAll(x => x.TransactionDate == Convert.ToDateTime("2019-10-28"));

            TransactionHistory actualTran = null;

            if (trans != null && trans.Count > 0)
            {
                actualTran = trans.Find(x => x.AccountNumber == expectedAccountNumber && x.Id == 3);
            }

            Assert.IsNotNull(trans);
            Assert.IsNotNull(actualTran);
            Assert.AreEqual(expectedRecordCount, trans.Count);
            Assert.AreEqual("Deposit", actualTran.TransactionType);
            Assert.AreEqual(2, actualTran.TransactionAmount);
        }

        [TestMethod]
        public void TestRepGetTransactionHistoryRecordsById()
        {
            //this test assures passed if values in database was not modified after this test was written
            int expectedAcctId = 2;
            string expectedAccountNumber = "20191000500201";
            int expectedRecordCount = 1;
            List<TransactionHistory> trans = _repository.GetTransactionHistoryRecords(expectedAcctId);

            Assert.IsNotNull(trans);
            Assert.AreEqual(expectedRecordCount, trans.Count);
            Assert.AreEqual(expectedAccountNumber,trans[0].AccountNumber);
            Assert.AreEqual(20, trans[0].TransactionAmount);
        }

        [TestMethod]
        public void TestRepInsertNewBankAccount()
        {
            // this will change number of records 
            DateTime date = DateTime.UtcNow;
            string expectedAccountNumber = _repository.GenerateNewAccountNumber();
            string expectedLoginName = "test" + date.Month.ToString() + date.Year.ToString() + date.Day.ToString() + date.Hour.ToString();
            BankAccount newAcct = new BankAccount() {
                LoginName = expectedLoginName,
                Password = "test",
                AccountNumber = expectedAccountNumber,
                Balance = 50,
                CreatedDate = date
            };

            _repository.InsertNewBankAccount(newAcct, "Create New Account");

            BankAccount actualAcct = _dataLib.GetBankAccountByAccountNumber(expectedAccountNumber);

            Assert.IsNotNull(actualAcct);
            Assert.AreEqual(expectedAccountNumber, actualAcct.AccountNumber);
            Assert.AreEqual(expectedLoginName, actualAcct.LoginName);
        }

        [TestMethod]
        public void TestRepProcessBankTransactionUsingDeposit()
        {
            string expectedSourceAcctNumber = "20191000600123";
            double transactionAmt = 2;

            //Get values prior to test 
            BankAccount priorBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedSourceAcctNumber);

            double expectedBalance = priorBankAcct.Balance + transactionAmt;

            BankTransaction expectedTrans = GenerateBankTransaction(expectedSourceAcctNumber, "", transactionAmt, "Deposit");

            _repository.ProcessBankTransaction(expectedTrans);

            //Get values after test
            BankAccount currentBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedSourceAcctNumber);

            Assert.IsNotNull(priorBankAcct);
            Assert.IsNotNull(currentBankAcct);
            Assert.AreEqual(expectedBalance, currentBankAcct.Balance);
        }

        [TestMethod]
        public void TestRepProcessBankTransactionUsingWithdraw()
        {
            string expectedSourceAcctNumber = "20191000600123";
            double transactionAmt = 2;

            //Get values prior to test 
            BankAccount priorBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedSourceAcctNumber);

            double expectedBalance = priorBankAcct.Balance - transactionAmt;

            BankTransaction expectedTrans = GenerateBankTransaction(expectedSourceAcctNumber, "", transactionAmt, "Withdraw");

            _repository.ProcessBankTransaction(expectedTrans);

            //Get values after test
            BankAccount currentBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedSourceAcctNumber);

            Assert.IsNotNull(priorBankAcct);
            Assert.IsNotNull(currentBankAcct);
            Assert.AreEqual(expectedBalance, currentBankAcct.Balance);
        }

        [TestMethod]
        public void TestRepProcessBankTransactionUsingTransfer()
        {
            string expectedSourceAcctNumber = "20191000600123";
            string expectedTargetAcctNumber = "20191000600124";
            double transactionAmt = 2;

            //Get values prior to test 
            BankAccount priorSourceBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedSourceAcctNumber);
            BankAccount priorTargetBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedTargetAcctNumber);

            double expectedSourceBalance = priorSourceBankAcct.Balance - transactionAmt;
            double expectedTargetBalance = priorTargetBankAcct.Balance + transactionAmt;

            BankTransaction expectedTrans = GenerateBankTransaction(expectedSourceAcctNumber, expectedTargetAcctNumber, transactionAmt, "Transfer");

            _repository.ProcessBankTransaction(expectedTrans);

            //Get values after test
            BankAccount currentSourceBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedSourceAcctNumber);
            BankAccount currentTargetBankAcct = _dataLib.GetBankAccountByAccountNumber(expectedTargetAcctNumber);

            Assert.IsNotNull(priorSourceBankAcct);
            Assert.IsNotNull(priorTargetBankAcct);
            Assert.IsNotNull(currentSourceBankAcct);
            Assert.IsNotNull(currentTargetBankAcct);
            Assert.AreEqual(expectedSourceBalance, currentSourceBankAcct.Balance);
            Assert.AreEqual(expectedTargetBalance, currentTargetBankAcct.Balance);
        }

        private BankAccount GenerateBankAccount()
        {
            return new BankAccount()
            {
                AccountNumber = "2019101000002",
                LoginName = "testuser2019",
                Password = "testuser2019",
                Balance = 100.00,
                CreatedDate = DateTime.UtcNow
            };
        }

        private BankTransaction GenerateBankTransaction(string sourceAcct, string targetAcct, double transAmt, string transType)
        {
            return new BankTransaction() {
                SourceAccountNumber = sourceAcct,
                TargetAccountNumber = targetAcct,
                TransactionType = transType,
                TransactionAmount = transAmt,
                TransactionDate = DateTime.UtcNow
            };
        } 
    }
}
