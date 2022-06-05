using FinNkriApp.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinNkriApp.API.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }


        public async Task TrySeedAsync()
        {
            // Default roles
            var administratorRole = new IdentityRole("Administrator");

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            // Default users
            var administrator = new ApplicationUser
            {
                UserName = "admin@finkri",
                Email = "finKri@finkri.com",
                FirstName = "john",
                LastName = "Smith",
                ImageUrl = "https://avatars.githubusercontent.com/u/69154853?v=4"
            };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "AdminFinkri123!");
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }

            // Default data
            // if it necessary
            if (!_context.Posts.Any())
            {
                _context.Posts.Add(new Post
                {
                    UserId = administrator.Id,
                    Title = "This demo post",
                    Description = "This is demo post's description",
                    Location = "El Houda, Agadir, Morocco",
                    Price = 199.99,
                    TotalBathrooms = 2,
                    TotalBedrooms = 4,
                    TotalKitchens = 1,
                    TotalLivingrooms = 1,
                    TotalFloors = 1,
                    IsAvailable = true,
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
