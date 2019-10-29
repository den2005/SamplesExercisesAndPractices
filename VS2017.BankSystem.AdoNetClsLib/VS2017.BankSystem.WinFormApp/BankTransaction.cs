using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS2017.BankSystem.WinFormApp
{
    public class BankTransaction
    {
        public int BankAccountId { get; set; }
        public int TransactionHistoryId { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public double TransactionAmount { get; set; }
        public double Balance { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
