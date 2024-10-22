
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SystemRezerwacjiLotow.Application;
using SystemRezerwacjiLotow.Application.Abstractions;
using SystemRezerwacjiLotow.Domain.Models;
using SystemRezerwacjiLotow.Infrastruktura;
using SystemRezerwacjiLotow.Infrastruktura.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDefaultIdentity<Tenant>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 10;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddRoles<IdentityRole<string>>()
    .AddTokenProvider<DataProtectorTokenProvider<Tenant>>(TokenOptions.DefaultProvider)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(o =>
{
    // zabezpieczenia token w mailowych wysy anych do potwierdzenia konta
    o.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    o.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
});


builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(cookie =>
{
    cookie.LoginPath = "/Account/Login";
    cookie.AccessDeniedPath = "/Account/Login";
    cookie.Cookie.HttpOnly = true;
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();


// wysy³anie tokenów potwierdzaj¹cych na maila
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
// Konfiguracja okresu wa no ci tokena dla DataProtectorTokenProvider
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(30);
});

builder.Services.AddControllersWithViews();


// repozytoria
builder.Services.AddScoped<IFlightsRepository, FlightsRepository>();
builder.Services.AddScoped<IFlightBuysRepository, FlightBuysRepository>();
builder.Services.AddScoped<IDniWylotowRepository, DniWylotowRepository>();
builder.Services.AddScoped<IKryteriaZnizekRepository, KryteriaZnizekRepository>();

// serwisy
builder.Services.AddScoped<ITenantsService, TenantsService>();




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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
