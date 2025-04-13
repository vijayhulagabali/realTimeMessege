using ChatApp.Hubs;
using ChatApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ChatApp.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ChatApp.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

// Add EF Core DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Authentication and Authorization (for login)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login.html";
    });

// Add services for email and SMS
builder.Services.AddSingleton<EmailSender>();
builder.Services.AddSingleton<SmsSender>();

// Build the app
var app = builder.Build();

// Middleware for static files
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

// SignalR Hub route
app.MapHub<ChatHub>("/chathub");

// Basic login route
app.MapPost("/login", async (HttpContext http, string username, string password) =>
{
    if (UserStore.Users.TryGetValue(username, out var pw) && pw == password)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
        var principal = new ClaimsPrincipal(identity);
        await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return Results.Ok("Logged in");
    }
    return Results.Unauthorized();
});

app.MapFallbackToFile("/chat.html");

app.Run();
