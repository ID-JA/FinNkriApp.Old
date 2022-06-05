using FinNkriApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinNkriApp.API.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Post> Posts { get;  }
        DbSet<Rating> Ratings { get;  }
        DbSet<Favourite> Favourites { get;  }
        DbSet<Image> Images { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
