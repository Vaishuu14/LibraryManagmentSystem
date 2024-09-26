using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Commands.MemberCommands
{
    public class DeleteMemberCommand : IRequest<Unit>
    {
        public int Id { get; set; }

    }
}
