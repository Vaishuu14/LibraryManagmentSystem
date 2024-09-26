using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using LibraryManagmentSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagmentSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IUserService _userService;
        public AuthController(IJwtTokenManager jwtTokenManager, IUserService userService)
        {
            _jwtTokenManager = jwtTokenManager;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUserAsync(model.UserName, model.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid user credentials.");
                }

                var token = _jwtTokenManager.IssueToken(user);
                var role = user.Role; // Get the user's role

                return Ok(new { Token = token, Role = role }); // Return token and role
            }
            return BadRequest("Invalid Request Body");
        }


    }
}
