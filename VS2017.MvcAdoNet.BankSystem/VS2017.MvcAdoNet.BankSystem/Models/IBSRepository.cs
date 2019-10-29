using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VS2017.BankSystem.AdoNetLibrary;

namespace VS2017.MvcAdoNet.BankSystem.Models
{
    public interface IBSRepository
    {
        UserLogin GetUserLoginInfo(string loginName);
        bool ValidateUserLogin(string loginName, string password);
        BankAccount RetrieveBankAccountInfo(string loginName);
        BankAccount RetrieveBankAccountInfo(int id);
        string ProcessBankTransaction(BankTransaction trans);
        List<TransactionHistory> GetTransactionHistoryRecords(int bankAcctId);
        List<TransactionHistory> GetTransactionHistoryRecords(string accountNumber);
        string InsertNewBankAccount(BankAccount newAcct, string transactionType);
        string GenerateNewAccountNumber();
    }
}
