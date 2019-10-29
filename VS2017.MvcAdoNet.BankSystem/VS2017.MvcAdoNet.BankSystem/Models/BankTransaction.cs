using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VS2017.MvcAdoNet.BankSystem.Models
{
    public class BankTransaction
    {
        [Display(Name = "Source Bank Account Id")]
        public int SourceBankAccountId { get; set; }
        [Display(Name = "Transaction History Id")]
        public int TransactionHistoryId { get; set; }
        [Display(Name = "Source Account Number")]
        public string SourceAccountNumber { get; set; }
        [Display(Name = "Target Account Number")]
        [Required(ErrorMessage = "Target Account Number is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Numeric characters and period characters only are allowed")]
        public string TargetAccountNumber { get; set; }
        [Display(Name = "Transaction TYpe")]
        public string TransactionType { get; set; }
        [Display(Name = "Transaction Amount")]
        [Required(ErrorMessage = "Transaction Amount is required")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Numeric characters and period characters only are allowed")]
        public double TransactionAmount { get; set; }
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }
    }
}
