using FinNkriApp.API.Dtos.Authentication;
using FinNkriApp.API.Identity;
using FinNkriApp.API.Models;

namespace FinNkriApp.API.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<(Result Result, AuthenticationResult AuthenticationResult)> CreateUserAsync(RegisterUserDto userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
