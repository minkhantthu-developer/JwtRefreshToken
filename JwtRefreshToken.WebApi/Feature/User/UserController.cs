using JwtRefreshToken.WebApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtRefreshToken.WebApi.Feature.User
{
    [Authorize]
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTManagerRepository _jwtManager;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
    }
}
