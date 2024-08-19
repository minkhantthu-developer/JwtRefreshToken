

using Microsoft.Identity.Client;
using System.IdentityModel;
using System.Security.Claims;

namespace JwtRefreshToken.WebApi.Repository
{
    public interface IJWTManagerRepository
    {
        TokenModel GenereateToken(string userName);
        TokenModel GenereateRefreshTokenModel(string userName);
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);   
    }
}
