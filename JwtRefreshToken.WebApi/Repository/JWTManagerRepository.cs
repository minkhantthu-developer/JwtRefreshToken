using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtRefreshToken.WebApi.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;

        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenModel GenereateToken(string userName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userName)
                    }),
                    Expires = DateTime.Now.AddMinutes(1),
                    SigningCredentials = new SigningCredentials
                    (
                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature
                    )
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();
                return new TokenModel
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TokenModel GenereateRefreshTokenModel(string userName)
        {
            return GenereateToken(userName);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var tokenValidParameter = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principle = tokenHandler.ValidateToken
                (
                     token, tokenValidParameter, out SecurityToken securityToken
                );
            JwtSecurityToken? jwtSecurityToken=securityToken as JwtSecurityToken;
            if(jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals
                (
                    SecurityAlgorithms.HmacSha256Signature,
                    StringComparison.InvariantCultureIgnoreCase
                ))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principle;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
