using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;

namespace Vehicle_Config_DotNet_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ProjectContext _context;

        public TokenController(IConfiguration configuration, ProjectContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // Login endpoint to authenticate and return a JWT token
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CompanyInfo userData)
        {
            if (userData == null || string.IsNullOrEmpty(userData.Email) || string.IsNullOrEmpty(userData.Password))
            {
                return BadRequest("Invalid request");
            }

            // Find user in database
            var user = await _context.CompanyInfos.FirstOrDefaultAsync(u => u.Email == userData.Email);
            if (user == null || user.Password != userData.Password)  // Make sure to compare hashes for passwords!
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT Token
            var token = GenerateJwtToken(user);

            // Return token
            return Ok(new { token, companyInfo = user });
        }

        // Method to generate the JWT token
        private string GenerateJwtToken(CompanyInfo user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? "JWTSubject"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("CompanyId", user.CompanyId.ToString()),
                new Claim("Email", user.Email ?? string.Empty)
            };

            // Retrieve the key from configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  // Ensure the key is 256 bits
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
