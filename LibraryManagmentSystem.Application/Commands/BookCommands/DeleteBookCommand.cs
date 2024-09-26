using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.BookCommands
{
    public class DeleteBookCommand : IRequest<Unit>
{
    public int Id { get; set; }
        public DeleteBookCommand(int id)
        {
            Id = id;
        }
    }
}

