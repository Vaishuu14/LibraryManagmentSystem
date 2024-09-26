using LibraryManagmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.Interfaces
{
    public interface IFineRepository
    {
        Task<Fine> GetByIdAsync(int id);
        Task<IEnumerable<Fine>> GetAllAsync();
        Task AddAsync(Fine fine);
        Task UpdateAsync(Fine fine);
        Task DeleteAsync(int id);
        

    }
}
