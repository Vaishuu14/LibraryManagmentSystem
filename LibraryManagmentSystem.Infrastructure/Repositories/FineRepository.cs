using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using LibraryManagmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Infrastructure.Repositories
{
    public class FineRepository : IFineRepository
    {
        private readonly LibraryDBContext _context;

        public FineRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public async Task<Fine> GetByIdAsync(int id)
        {
            return await _context.Fines.FindAsync(id);
        }

        public async Task AddAsync(Fine fine)
        {
            await _context.Fines.AddAsync(fine);
        }

        public async Task UpdateAsync(Fine fine)
        {
            _context.Fines.Update(fine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var fine = await _context.Fines.FindAsync(id);
            if (fine != null)
            {
                _context.Fines.Remove(fine);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Fine>> GetAllAsync()
        {
            return await _context.Fines.ToListAsync();
        }
       



    }
}
