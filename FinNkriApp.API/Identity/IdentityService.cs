using FinNkriApp.API.Dtos.Authentication;
using FinNkriApp.API.Entities;
using FinNkriApp.API.Interfaces;
using FinNkriApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinNkriApp.API.Identity
{
    public class IdentityService : IIdentityService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITokenClaimsService _tokenClaimsService;

        public IdentityService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, IAuthorizationService authorizationService, ITokenClaimsService tokenClaimsService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _tokenClaimsService = tokenClaimsService;
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }
        
        public async Task<(Result Result, AuthenticationResult AuthenticationResult)> CreateUserAsync(RegisterUserDto registerUser, string password)
        {
            var user = new ApplicationUser
            {
                UserName = registerUser.Email,
                FirstName = registerUser.FistName,
                LastName = registerUser.LastName,
                ImageUrl = registerUser.ImageUrl,
                IsHouseOwner = registerUser.IsHouseOwner,
                Email = registerUser.Email,
            };

            var result = await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, "Member");

            var token = await _tokenClaimsService.GetTokenAsync(user.Email);
            
            var authRes = new AuthenticationResult
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
            };
            
            return (result.ToApplicationResult(), authRes);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }
        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }
        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
