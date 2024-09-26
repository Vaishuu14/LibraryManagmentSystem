using AutoMapper;
using LibraryManagmentSystem.Application.Commands.FineCommands;
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
    public class CalculateFineCommandHandler : IRequestHandler<CalculateFineCommand, FineDto>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IFineCalculationService _fineCalculationService;

        public CalculateFineCommandHandler(ILoanRepository loanRepository, IFineCalculationService fineCalculationService)
        {
            _loanRepository = loanRepository;
            _fineCalculationService = fineCalculationService;
        }

        public async Task<FineDto> Handle(CalculateFineCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the loan from the repository
            var loan = await _loanRepository.GetLoanByIdAsync(request.LoanId);
            //if (loan == null)
            //{
                
            //}

            // Calculate the fine using the provided return date and daily fine
            var fine = await _fineCalculationService.CalculateFineAsync(loan, request.ReturnDate, request.DailyFine);

            return new FineDto(fine);
        }

    }
}
