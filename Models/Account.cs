using System;
using System.Collections.Generic;

namespace GenFuTest.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public int CustomerId { get; set; }
        public double Balance { get; set; }
        public string AccountNumber { get; set; }

        public virtual AccountType AccountType { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
