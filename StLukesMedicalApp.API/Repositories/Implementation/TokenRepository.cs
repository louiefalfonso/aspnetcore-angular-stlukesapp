using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StLukesMedicalApp.API.Repositories.Interface;

namespace StLukesMedicalApp.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Load .env file
            DotNetEnv.Env.Load();

            // Retrieve JWT configuration from environment variables
            var JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY");
            var JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE");


            // Check if any of the values are null
            if (string.IsNullOrEmpty(JWT_KEY) || string.IsNullOrEmpty(JWT_ISSUER) || string.IsNullOrEmpty(JWT_AUDIENCE))
            {
                throw new InvalidOperationException("JWT configuration values are not set in the environment variables.");
            }

            // JWT Security Token Parameters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_KEY));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JWT_ISSUER,
                audience: JWT_AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            // Return Token
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
