using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using bizpay_api.Data;
using bizpay_api.Repository;
using bizpay_api.Models;
using Microsoft.EntityFrameworkCore;

namespace bizpay_api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly APIDbContext _dbContext;

        public AuthController(IConfiguration configuration, APIDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            if (_dbContext.Employees == null)
            {
                return NotFound(new { message = "Contexto de banco dados inválido!" });
            }

            var userAuth = await _dbContext.Employees.Include(p => p.Permition).FirstOrDefaultAsync(e => e.Email == user.Email && e.Password == user.Password);

            if (userAuth != null)
            {
                var token = GenerateJwtToken(userAuth);

                return Ok(new { Token = token });
            }

            return Unauthorized("Login falhou");
        }

        private string GenerateJwtToken(Employee employee)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, employee.Email),
                new Claim("Permition", employee.Permition.Name),
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
