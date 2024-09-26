using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.FineCommands
{
    public class CreateFineCommand : IRequest<FineDto>
    {
       
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime FineDate { get; set; }

    }
}
