using AutoMapper;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.MemberQueries;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LibraryManagmentSystem.Application.Handlers.QueryHandler
{
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberDto>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.Id);
            if (member == null)
            {
                throw new KeyNotFoundException($"Member with ID {request.Id} not found.");
            }
            return _mapper.Map<MemberDto>(member);
        }

    }
}
