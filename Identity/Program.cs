using INTEXII.Models;
using INTEXII.CustomPolicy;
using INTEXII.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Configuration;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("INTEXIIIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'INTEXIIIdentityDbContextConnection' not found.");

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

//builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor,
    HttpContextAccessor>();

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});


builder.Services.AddDbContext<ProductContext>(options => options.UseSqlite(builder.Configuration["ConnectionStrings:TestingLocalDBConnection"]));
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlite(builder.Configuration["ConnectionStrings:IdentityConnection"]));
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<INTEXIIIdentityDbContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(10));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("AspManager", policy =>
    {
        policy.RequireRole("Manager");
        policy.RequireClaim("Coding-Skill", "ASP.NET Core MVC");
    });
});

builder.Services.AddTransient<IAuthorizationHandler, AllowUsersHandler>();
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("AllowTom", policy =>
    {
        policy.AddRequirements(new AllowUserPolicy("tom"));
    });
});

builder.Services.AddTransient<IAuthorizationHandler, AllowPrivateHandler>();
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("PrivateAccess", policy =>
    {
        policy.AddRequirements(new AllowPrivatePolicy());
    });
});

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Lockout.AllowedForNewUsers = true;
    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    opts.Lockout.MaxFailedAccessAttempts = 3;
});

/*builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.SignIn.RequireConfirmedEmail = true;
});*/

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<ProductContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration["ConnectionStrings:LegoConnection"]);
//});

builder.Services.AddScoped<IProductRepository, EFProductRepository>();

// Build the server for the razor page
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("pagination", "{pageNum}", new { controller = "Home", Action = "Index", pageNum = 1 });
app.MapControllerRoute("pagination", "{pageNum}", new { controller = "Admin", Action = "Home", pageNum = 1 });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
