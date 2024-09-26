using AutoMapper;
using LibraryManagmentSystem.Application.Commands.MemberCommands;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberDto>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public CreateMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request);
            await _memberRepository.AddAsync(member);
            return _mapper.Map<MemberDto>(member);
        }

    }
}
