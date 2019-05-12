using System;
using System.Collections.Generic;

namespace GenFuTest.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Account = new HashSet<Account>();
            LoanAmount = new HashSet<LoanAmount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<LoanAmount> LoanAmount { get; set; }
    }
}
