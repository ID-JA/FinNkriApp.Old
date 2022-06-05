using Microsoft.AspNetCore.Identity;

namespace FinNkriApp.API.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImageUrl { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; }
    public virtual ICollection<Favourite> Favourites { get; set; }
}
