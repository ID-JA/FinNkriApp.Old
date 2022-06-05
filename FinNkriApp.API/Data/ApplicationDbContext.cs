using Duende.IdentityServer.EntityFramework.Options;
using FinNkriApp.API.Data.Interceptors;
using FinNkriApp.API.Entities;
using FinNkriApp.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace FinNkriApp.API.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
        public ApplicationDbContext(
          DbContextOptions<ApplicationDbContext> options,
           IOptions<OperationalStoreOptions> operationalStoreOptions,
          IMediator mediator,
          AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
      : base(options, operationalStoreOptions)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Favourite> Favourites => Set<Favourite>();
        public DbSet<Image> Images => Set<Image>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
