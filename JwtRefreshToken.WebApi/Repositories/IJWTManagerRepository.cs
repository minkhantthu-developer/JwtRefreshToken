using System.Security.Claims;

namespace JwtRefreshToken.WebApi.Repositories
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateToken(string userName);

        Tokens GenerateRefreshToken(string userName);

        ClaimsPrincipal GetClaimsPrincipalFromExpireToken(string token);
    }
}
