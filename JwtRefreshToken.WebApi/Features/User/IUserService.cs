namespace JwtRefreshToken.WebApi.Features.User
{
    public interface IUserService
    {
        Task<bool> IsValidUserAsync(UserLogin userLogin);

        UserRefreshToken AddUserRefreshToken(UserRefreshToken user);

        UserRefreshToken GetSavedUserRefreshToken(string userName, string refreshTokn);

        Task DeleteRefreshToken(string userName, string refreshTokn);
    }
}
