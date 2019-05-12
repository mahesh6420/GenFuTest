using System;
using System.Collections.Generic;

namespace GenFuTest.Models
{
    public partial class LoanAmount
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public string LoanNumber { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Loan Loan { get; set; }
    }
}
