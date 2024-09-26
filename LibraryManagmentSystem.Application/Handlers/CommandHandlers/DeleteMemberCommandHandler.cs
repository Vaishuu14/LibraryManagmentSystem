using LibraryManagmentSystem.Application.Commands.MemberCommands;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand,Unit>
    {
        private readonly IMemberRepository _memberRepository;

        public DeleteMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Unit> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            await _memberRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }

    }
}
