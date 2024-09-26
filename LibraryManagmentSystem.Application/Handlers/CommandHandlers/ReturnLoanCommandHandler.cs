using AutoMapper;
using LibraryManagmentSystem.Application.Commands.LoanCommands;
using LibraryManagmentSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Application.Handlers.CommandHandlers
{
    public class ReturnLoanCommandHandler : IRequestHandler<ReturnLoanCommand, Unit>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public ReturnLoanCommandHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {

            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            // var member = _mapper.Map<Core.Entities.Member>(request);

            _mapper.Map(request, loan);
            await _loanRepository.UpdateAsync(loan);
            return Unit.Value;
        }

    }
}
