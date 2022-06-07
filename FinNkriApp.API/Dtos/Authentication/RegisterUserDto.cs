using FinNkriApp.API.Entities;
using FinNkriApp.API.Mapping;
using System.ComponentModel.DataAnnotations;

namespace FinNkriApp.API.Dtos.Authentication
{
    public class RegisterUserDto : IMapFrom<ApplicationUser>
    {
        public string Email { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsHouseOwner { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "'ConfirmPassword' and 'Password' do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
