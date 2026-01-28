using dietsync.DTOs;
using dietsync.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dietsync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public Auth(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserAuthDto userLogin)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == userLogin.Email);
            if (user == null)
            {
                new AuthErrorDto
                {
                    Message = "Email ou senha inválidos.",
                    Success = false,
                    Data = new LoginResponseDto
                    {
                        Email = user.Email,
                        Name = user.Name
                    }
                };
            }
            bool senhaOk = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);

            if (!senhaOk)
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
            }
            var token = GenerateJwtToken(user);

            return Ok(token);
        }

        private LoginResponseDto GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])
            );

            var creds = new Microsoft.IdentityModel.Tokens.SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name)
            };

            var expiration = DateTime.UtcNow.AddMinutes(
                double.Parse(jwtSettings["ExpirationMinutes"])
                );
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new LoginResponseDto
            {
                UserId = (int)user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // In a stateless JWT authentication system, logout is typically handled on the client side
            // by simply deleting the token. However, if you want to implement server-side logout,
            // you might consider maintaining a token blacklist or changing the user's token secret.
            return Ok(new { message = "Logout successful." });
        }
    }
}