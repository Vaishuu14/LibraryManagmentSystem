using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace LibraryManagmentSystem.Infrastructure.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUserNameAsync(username);
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);
            if (user == null )
            {
                return null;
            }

            return user;
        }


        public async Task<User> RegisterUserAsync(Registration model)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetUserByUserNameAsync(model.UserName);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            // Create and save the new user
            var newUser = new User
            {
                UserName = model.UserName,
                Password = model.Password, // Ensure password is hashed
                Email = model.Email,
                Role = model.Role // Set default role or specify role as needed
            };

            await _userRepository.AddUserAsync(newUser); // Save the user to the database

            return newUser;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}
