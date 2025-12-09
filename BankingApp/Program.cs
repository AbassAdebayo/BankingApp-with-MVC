using BankingApp.Contracts.Service;
using BankingApp.Identity;
using BankingApp.Implementation.Repositories;
using BankingApp.Implementation.Services;
using BankingApp.Interface.Repositories;
using BankingApp.Interface.Services;
using BankingApp.Models.DTOs.Auth.Validation;
using BankingApp.Models.DTOs.User;
using BankingApp.Models.Entities;
using BankingApp.Persistence.Context;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBankRepository, BankRepository>()
    .AddScoped<IRoleRepository, RoleRepository>()
    .AddScoped<IBankService, BankService>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserService, UserService>().
    AddScoped<IAuthService, AuthService>();

builder.Services.AddControllersWithViews();

builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidation>();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IUserStore<User>, UserStore>();
builder.Services.AddScoped<IRoleStore<Role>, RoleStore>();
builder.Services.AddIdentity<User, Role>()
    .AddDefaultTokenProviders();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Auth/login";
        config.Cookie.Name = "eBank";
        config.LogoutPath = "/Auth/logout";
        config.AccessDeniedPath = "/User/Login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        config.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();

//Add Database
builder.Services.AddDbContext<BankContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("BankContext"),
        new MySqlServerVersion(new Version(9, 0, 0))
    ));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (builder.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=RegisterCustomer}/{id?}")
    .WithStaticAssets();


app.Run();
