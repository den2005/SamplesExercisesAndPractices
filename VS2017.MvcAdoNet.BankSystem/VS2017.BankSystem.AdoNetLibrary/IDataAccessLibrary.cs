using System;
using System.Collections.Generic;
using System.Text;

namespace VS2017.BankSystem.AdoNetLibrary
{
    public interface IDataAccessLibrary
    {
        List<BankAccount> GetAllBankAccounts();
        BankAccount GetBankAccountByAccountNumber(string accountNumber);
        BankAccount GetBankAccountByLoginName(string loginName);
        BankAccount GetBankAccountById(int id);
        void InsertNewBankAccount(BankAccount newAcct, string transactionType);
        void UpdateBankAccount(BankAccount editAcct, string transactionType, double transAmount);
        void UpdateBankAccount(BankAccount sourceAcct, BankAccount targetAcct, string transactionType, double transAmount);
        List<TransactionHistory> GetTransactionHistoryRecordsByAccountId(int bankAcctId);
        List<TransactionHistory> GetTransactionHistoryRecordsByAccountNumber(string accountNumber);
    }
}
