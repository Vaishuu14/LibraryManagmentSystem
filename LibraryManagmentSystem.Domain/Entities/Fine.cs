using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.Entities
{
    public class Fine
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public bool IsPaid { get; set; }

    }
}
