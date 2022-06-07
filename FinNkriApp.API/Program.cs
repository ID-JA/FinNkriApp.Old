using FinNkriApp.API.Data;
using FinNkriApp.API.Data.Interceptors;
using FinNkriApp.API.Entities;
using FinNkriApp.API.Interfaces;
using FinNkriApp.API.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;
using FinNkriApp.API.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

    builder.Services.AddScoped<AuditableEntitySaveChangesInterceptor>();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

    builder.Services.AddScoped<ApplicationDbContextInitialiser>();

    builder.Services.AddDefaultIdentity<ApplicationUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

    builder.Services.AddIdentityServer()
        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

    builder.Services.AddTransient<IDateTime, DateTimeService>();
    builder.Services.AddTransient<IIdentityService, IdentityService>();
    builder.Services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();
    
    builder.Services.AddAuthentication()
        .AddIdentityServerJwt();

    builder.Services.AddAuthorization(options =>
        options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));


    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
