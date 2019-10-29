using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VS2017.BankSystems.AspNetCoreWebApp.Models
{
    public class UserLogin
    {
        public int BankAccountId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
