using AutoMapper;
using LibraryManagmentSystem.Application.DTOs;
using LibraryManagmentSystem.Application.Queries.LoanQueries;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.QueryHandler
{
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetLoanByIdQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(request.Id);
            return _mapper.Map<LoanDto>(loan);
        }

    }
}
