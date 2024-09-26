using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.DTOs
{
    public class FineDto
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }
        

        public FineDto(decimal amount)
        {
            Amount = amount;
        }

    }
}
