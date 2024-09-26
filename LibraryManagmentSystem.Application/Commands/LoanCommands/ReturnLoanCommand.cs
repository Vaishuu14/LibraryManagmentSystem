using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.LoanCommands
{
    public class ReturnLoanCommand : IRequest<Unit>
    {
        public int LoanId { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
