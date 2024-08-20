using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtRefreshToken.WebApi.Repositories
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;

        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tokens GenerateRefreshToken(string userName)
        {
            return GenereateTokens(userName);
        }

        public Tokens GenerateToken(string userName)
        {
            return GenereateTokens(userName);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpireToken(string token)
        {
            return GetPrincipleFromExpiredToken(token);
        }

        public Tokens GenereateTokens(string userName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token=tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new Tokens
                {
                    AcessToken = tokenHandler.WriteToken(token),
                    RefreshToken=refreshToken,
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var tokenValidParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidParameter,out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
