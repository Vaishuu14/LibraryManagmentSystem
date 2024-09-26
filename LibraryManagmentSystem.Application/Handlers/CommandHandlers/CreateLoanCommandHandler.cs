using AutoMapper;
using LibraryManagmentSystem.Application.Commands.LoanCommands;
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
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public CreateLoanCommandHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = _mapper.Map<Loan>(request);
            await _loanRepository.AddAsync(loan);
            return _mapper.Map<LoanDto>(loan);
        }

    }
}
