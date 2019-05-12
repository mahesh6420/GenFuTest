using System;
using System.Collections.Generic;

namespace GenFuTest.Models
{
    public partial class Loan
    {
        public Loan()
        {
            LoanAmount = new HashSet<LoanAmount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LoanAmount> LoanAmount { get; set; }
    }
}
