using LibraryManagmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserByUserNameAndPasswordAsync(string userName, string password);
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
    }
}
