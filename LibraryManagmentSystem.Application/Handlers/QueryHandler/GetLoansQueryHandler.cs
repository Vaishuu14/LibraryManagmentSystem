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
    public class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, List<LoanDto>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetLoansQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<List<LoanDto>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync();
            return _mapper.Map<List<LoanDto>>(loans);
        }

    }
}
