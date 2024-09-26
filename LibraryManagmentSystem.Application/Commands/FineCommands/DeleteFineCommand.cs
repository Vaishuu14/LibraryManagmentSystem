using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.FineCommands
{
    public class DeleteFineCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DeleteFineCommand(int id)
        {
            Id = id;
        }
    }
}
