using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VS2017.MvcAdoNet.BankSystem.Models
{
    public class UserLogin
    {
        public int BankAccountId { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name ="Login Name")]
        [Required(ErrorMessage ="Login Name is required")]
        [RegularExpression(@"^[a-zA-Z0-9.]*$", ErrorMessage = "Alphanumeric and period characters only are allowed")]
        public string LoginName { get; set; }
     
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^[a-zA-Z0-9.]*$", ErrorMessage = "Alphanumeric and period characters only are allowed")]
        public string Password { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
