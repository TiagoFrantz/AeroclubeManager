using AeroclubeManager.Core.Entities.Email;
using AeroclubeManager.Core.Entities.User;
using AeroclubeManager.Core.Interfaces.Repos.FlightSchoolRepos;
using AeroclubeManager.Core.Interfaces.Repos.User;
using AeroclubeManager.Core.Interfaces.Services.Airport;
using AeroclubeManager.Core.Interfaces.Services.Email;
using AeroclubeManager.Core.Interfaces.Services.FlightSchoolServices;
using AeroclubeManager.Core.Interfaces.Services.Image;
using AeroclubeManager.Core.Interfaces.Services.User;
using AeroclubeManager.Core.Interfaces.Services.Weather;
using AeroclubeManager.Core.Services.Airport;
using AeroclubeManager.Core.Services.Email;
using AeroclubeManager.Core.Services.FlightSchoolServices;
using AeroclubeManager.Core.Services.Image;
using AeroclubeManager.Core.Services.User;
using AeroclubeManager.Core.Services.Weather;
using AeroclubeManager.Infra.Data;
using AeroclubeManager.Infra.Repositories.FlightSchoolRepos;
using AeroclubeManager.Infra.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSenderService>();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IFlightSchoolService, FlightSchoolService>();
builder.Services.AddScoped<IFlightSchoolRepository, FlightSchoolRepository>();
builder.Services.AddScoped<IAirportDbService, AirportDbService>();
builder.Services.AddScoped<IWeatherService, WeatherService>(); 
builder.Services.AddScoped<IWeatherCacheService, WeatherCacheService>();

builder.Services.AddDbContext<AeroclubeManagerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true).AddEntityFrameworkStores<AeroclubeManagerDbContext>().AddDefaultTokenProviders().AddDefaultUI(); ;
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
