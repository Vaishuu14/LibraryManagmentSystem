using LibraryManagmentSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.FineCommands
{
    public class UpdateFineCommand : IRequest<Unit>
    {
        public int FineId { get; set; }
        public decimal NewFineAmount { get; set; }
    }

}

