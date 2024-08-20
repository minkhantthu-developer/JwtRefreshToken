using JwtRefreshToken.WebApi.Models;
using JwtRefreshToken.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtRefreshToken.WebApi.Features.User
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository _jwtManager;
        private readonly IUserService _userService;

        public UserController(IJWTManagerRepository jwtManager, IUserService userService)
        {
            _jwtManager = jwtManager;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate-user")]
        public async Task<IActionResult> AuthenticateAsync(UserLogin usersdata)
        {
            var validUser = await _userService.IsValidUserAsync(usersdata);

            if (!validUser)
            {
                return Unauthorized("Invalid username or password...");
            }

            var token = _jwtManager.GenerateToken(usersdata.Email);

            if (token == null)
            {
                return Unauthorized("Invalid Attempt..");
            }

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = token.RefreshToken,
                UserName = usersdata.Email
            };

            _userService.AddUserRefreshToken(obj);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh-token")]
        public IActionResult Refresh(Tokens token)
        {
            var principal = _jwtManager.GetClaimsPrincipalFromExpireToken(token.AcessToken);
            var username = principal.Identity?.Name;

            var savedRefreshToken = _userService.GetSavedUserRefreshToken(username, token.RefreshToken);

            if (savedRefreshToken.RefreshToken != token.RefreshToken)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = _jwtManager.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserName = username
            };

            _userService.DeleteRefreshToken(username, token.RefreshToken);
            _userService.AddUserRefreshToken(obj);

            return Ok(newJwtToken);
        }
    }
}
