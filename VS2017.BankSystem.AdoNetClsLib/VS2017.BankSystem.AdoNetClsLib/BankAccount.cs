using System;
using System.Collections.Generic;
using System.Text;

namespace VS2017.BankSystem.AdoNetClsLib
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
