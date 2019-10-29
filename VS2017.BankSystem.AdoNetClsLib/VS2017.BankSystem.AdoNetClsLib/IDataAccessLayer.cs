using System;
using System.Collections.Generic;
using System.Text;

namespace VS2017.BankSystem.AdoNetClsLib
{
    public interface IDataAccessLayer
    {
        List<BankAccount> GetAllBankAccounts();
        BankAccount GetBankAccountByAccountNumber(string accountNumber);
        BankAccount GetBankAccountByLoginName(string loginName);
        void InsertNewBankAccount(BankAccount newAcct);
        void UpdateBankAccount(BankAccount editAcct, string transactionType, double transAmount);
    }
}
