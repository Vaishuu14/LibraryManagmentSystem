using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.FineCommands
{
    public class CalculateFineCommand : IRequest<FineDto>
    {
        public int LoanId { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal DailyFine { get; set; }

        public CalculateFineCommand(int loanId, DateTime returnDate, decimal dailyFine)
        {
            LoanId = loanId;
            ReturnDate = returnDate;
            DailyFine = dailyFine;
        }

    }
}
