using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VS2017.BankSystem.AdoNetLibrary;

namespace VS2017.MvcAdoNet.BankSystem.Models
{
    public class BSRepository : IBSRepository
    {
        private IDataAccessLibrary _lib;

        public BSRepository()
        {
            _lib = new DataAccessLibrary();
        }

        public UserLogin GetUserLoginInfo(string loginName)
        {
            UserLogin login = null;

            BankAccount dbBankAcct = _lib.GetBankAccountByLoginName(loginName);

            if (dbBankAcct != null)
            {
                login = new UserLogin() {
                    BankAccountId = dbBankAcct.Id,
                    LoginName = dbBankAcct.LoginName,
                    Password = dbBankAcct.Password,
                    AccountNumber = dbBankAcct.AccountNumber,
                    CreatedDate = dbBankAcct.CreatedDate
                };
            }

            return login;
        }

        public bool ValidateUserLogin(string loginName, string password)
        {
            bool isValid = false;

            BankAccount dbBankAcct = _lib.GetBankAccountByLoginName(loginName);

            if (dbBankAcct != null && dbBankAcct.Password == password)
            {
                isValid = true;
            }

            return isValid;
        }

        public BankAccount RetrieveBankAccountInfo(string loginName)
        {
            BankAccount acct = _lib.GetBankAccountByLoginName(loginName);

            return acct;
        }

        public BankAccount RetrieveBankAccountInfo(int id)
        {
            BankAccount acct = _lib.GetBankAccountById(id);

            return acct;
        }

        public string ProcessBankTransaction(BankTransaction trans)
        {
            string result = "";

            switch (trans.TransactionType)
            {
                case "Deposit":
                    result = DepositAmount(trans);
                    break;
                case "Withdraw":
                    result = WithrawAmount(trans);
                    break;
                case "Transfer":
                    result = TransferAmount(trans);
                    break;
                default:
                    break;
            }

            return result;
        }

        public List<TransactionHistory> GetTransactionHistoryRecords(int bankAcctId)
        {
            List<TransactionHistory> bankTransactions = _lib.GetTransactionHistoryRecordsByAccountId(bankAcctId);

            return bankTransactions;
        }

        public List<TransactionHistory> GetTransactionHistoryRecords(string accountNumber)
        {
            List<TransactionHistory> bankTransactions = _lib.GetTransactionHistoryRecordsByAccountNumber(accountNumber);

            return bankTransactions;
        }

        public string InsertNewBankAccount(BankAccount newAcct, string transactionType)
        {
            

            _lib.InsertNewBankAccount(newAcct, transactionType);

            string result = "New User Login Record created Successfully";

            return result;
        }

        public string GenerateNewAccountNumber()
        {
            string acctnumber = "";

            int year = DateTime.UtcNow.Year;
            int month = DateTime.UtcNow.Month;
            string prefix = year.ToString() + month.ToString();

            List<BankAccount> accts = _lib.GetAllBankAccounts();

            if (accts != null && accts.Count > 0)
            {
                var varNums = (from a in accts
                               where a.AccountNumber.StartsWith(prefix)
                               select Convert.ToInt64(a.AccountNumber));
                long maxNumber = varNums.Max();
                acctnumber = (maxNumber + 1).ToString();
            }
            else {
                acctnumber = prefix + "00100001";
            }

            return acctnumber;
        }

        private string TransferAmount(BankTransaction trans)
        {
            BankAccount sourceAcct = _lib.GetBankAccountByAccountNumber(trans.SourceAccountNumber);
            BankAccount targetAcct = _lib.GetBankAccountByAccountNumber(trans.TargetAccountNumber);

            if (sourceAcct != null && targetAcct != null && trans.TransactionAmount < 0)
            {
                return "Transaction Amount cannot be negative or less than zero";
            }
            else if (sourceAcct != null && targetAcct != null && sourceAcct.Balance > trans.TransactionAmount)
            {                
                //sourceAcct.Balance = sourceAcct.Balance - trans.TransactionAmount;
                
                //targetAcct.Balance = targetAcct.Balance + trans.TransactionAmount;

                _lib.UpdateBankAccount(sourceAcct, targetAcct, trans.TransactionType, trans.TransactionAmount);

                
            }            
            else if (sourceAcct != null && targetAcct != null && sourceAcct.Balance < trans.TransactionAmount)
            {
                return "Transaction Amount must be less than Balance Amount.";
            }
            else if (sourceAcct != null && targetAcct == null )
            {
                return "Target Account Number does not exist.";
            }
            else if (sourceAcct == null && targetAcct != null)
            {
                return "Source Account Number does not exists.";
            }

            return "Bank Transaction Successfull";
        }

        private string DepositAmount(BankTransaction trans)
        {
            BankAccount acct = _lib.GetBankAccountByAccountNumber(trans.SourceAccountNumber);

            if (acct != null)
            {
                if (trans.TransactionAmount > 0)
                {
                    acct.Balance = acct.Balance + trans.TransactionAmount;

                    _lib.UpdateBankAccount(acct, trans.TransactionType, trans.TransactionAmount);

                    return "Bank Transaction Successfull";
                }            
                else
                {
                    return "Transaction Amount cannot be negative or less than zero";
                }
            }   
            else
            {
                return "Account Number does not exists!";
            }
        }

        private string WithrawAmount(BankTransaction trans)
        {
            BankAccount acct = _lib.GetBankAccountByAccountNumber(trans.SourceAccountNumber);

            if (acct != null && acct.Balance > trans.TransactionAmount)
            {
                if (trans.TransactionAmount > 0)
                {
                    acct.Balance = acct.Balance - trans.TransactionAmount;

                    _lib.UpdateBankAccount(acct, trans.TransactionType, trans.TransactionAmount);

                    return "Bank Transaction Successfull";
                }
                else
                {
                    return "Transaction Amount cannot be negative or less than zero";
                }               
            }
            else
            {
                return "Account Number does not exists!";
            }
        }

        
    }
}
