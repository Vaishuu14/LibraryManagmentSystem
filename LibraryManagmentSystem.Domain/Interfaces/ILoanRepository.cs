using LibraryManagmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.Interfaces
{
    public interface ILoanRepository
    {

        Task<Loan> GetLoanByIdAsync(int loanId);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task DeleteAsync(int id);
        

    }
}
