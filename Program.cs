using LibraryFinal.Controllers;
using LibraryFinal.Middlewares;
using LibraryFinal.Models;
using LibraryFinal.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

// **JWT**
// **Clave alfanumérica para crear tokens**
// **No es recomendable mantenerla aca por motivos de seguridad**
// secret key para probar "c0Ntr4T4M3pOrfAv0RqU1eRotr4bAjaRj4JaJA="
var secretKey = "c0Ntr4T4M3pOrfAv0RqU1eRotr4bAjaRj4JaJA=";

builder.Services.AddSingleton<JwtService>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<AddCookieHeaderMiddleware>();

// **Agregar el middleware de autenticación de cookies**
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Opciones de configuración de la cookie
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        // ... (otras opciones)
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
});

builder.Services.AddDbContext<LibraryFinalContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("LibraryFinalConnection")));

var app = builder.Build();

// **Agregar la autenticación al pipeline**



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<AddCookieHeaderMiddleware>();
app.UseAuthentication();
app.UseMiddleware<JwtAuthenticationMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
