using AutoMapper;
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
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Unit>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            // Fetch the existing member from the repository
            var member = await _memberRepository.GetByIdAsync(request.Id);

            // Check if member exists
            if (member == null)
            {
                // Optionally, you can throw an exception or handle this case as per your requirement
                throw new KeyNotFoundException($"Member with ID {request.Id} not found.");
            }

            // Map the properties from the command to the existing member
            _mapper.Map(request, member);

            // Update the member in the repository
            await _memberRepository.UpdateAsync(member);

            // Return Unit.Value to signify successful completion
            return Unit.Value;
        }

    }
}
