using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VS2017.BankSystems.AspNetCoreWebApp.Models
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
