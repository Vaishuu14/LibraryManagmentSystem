using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Infrastructure.Repositories
{
    public class FineCalculationService : IFineCalculationService
    {
        public Task<decimal> CalculateFineAsync(Loan loan, DateTime returnDate, decimal dailyFine)
        {
            if (returnDate <= loan.DueDate)
            {
                return Task.FromResult(0m);
            }

            var overdueDays = (returnDate - loan.DueDate).TotalDays;
            var fine = (decimal)overdueDays * dailyFine;

            return Task.FromResult(fine);
        }
    }
}
