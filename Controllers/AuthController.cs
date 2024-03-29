using LibraryFinal.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryFinal.Controllers
{
    public class AuthController : Controller
    {
        private readonly LibraryFinalContext _context;

        public AuthController(LibraryFinalContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // uso request.Email para recibir el userName
            if (IsValidUser(request.Email, request.Password)) //include IsValidUser check
            {
                var cookieOptions = new CookieOptions
                {
                    Path = "/", // Ruta de la cookie
                    Expires = DateTime.UtcNow.AddDays(30), // Fecha de expiración de la cookie
                    HttpOnly = true, // Indica que la cookie solo debe ser accesible por el servidor
                    Secure = true, // Indica que la cookie solo debe ser enviada a través de HTTPS
                };
                var tokenString = GenerateJwtToken(request.Email, IsAdmin(request.Email)); // Include isAdmin check
                Response.Cookies.Append("AuthToken", tokenString, cookieOptions);
                return Ok();
            }
            return BadRequest("Invalid username or password");
        }

        private string GenerateJwtToken(string username, bool isAdmin)
        {

            // **JWT**
            // **Clave alfanumérica para crear tokens**
            // **No es recomendable mantenerla aquí por motivos de seguridad**
            string key = "c0Ntr4T4M3pOrfAv0RqU1eRotr4bAjaRj4JaJA=";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User") // Add role claim based on isAdmin check
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMonths(1), // Set token expiration (e.g., 60 minutes)
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(securityToken);

            return tokenString;
        }

        // Implement methods for IsValidUser and IsAdmin (replace with your authentication logic)
        private bool IsValidUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            return user != null;
        }

        private bool IsAdmin(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            return user?.Role == "Admin";
        }
    }

}
