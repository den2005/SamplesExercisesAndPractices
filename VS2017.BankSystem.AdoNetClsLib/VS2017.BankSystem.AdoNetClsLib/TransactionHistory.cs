using System;
using System.Collections.Generic;
using System.Text;

namespace VS2017.BankSystem.AdoNetClsLib
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
