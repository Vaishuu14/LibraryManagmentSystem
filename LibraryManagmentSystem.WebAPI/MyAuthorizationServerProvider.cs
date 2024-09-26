using LibraryManagmentSystem.Domain.Interfaces;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.WebAPI
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserRepository _userRepository;

        //public MyAuthorizationServerProvider(IUserRepository userRepository)
        //{
        //    _userRepository = userRepository;
        //}

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // Allow all clients
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Validate user credentials using the UserRepository
            var user = await _userRepository.GetUserByUserNameAndPasswordAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password are incorrect.");
                return;
            }

            // Create claims identity
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));

            // Validate the identity
            context.Validated(identity);
        }
    }
}
