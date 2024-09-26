using LibraryManagmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.Interfaces
{
    public interface IFineCalculationService
    {

        Task<decimal> CalculateFineAsync(Loan loan, DateTime returnDate, decimal dailyFine);

    }
}
