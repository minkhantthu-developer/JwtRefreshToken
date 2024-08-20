namespace JwtRefreshToken.WebApi.Features.User
{
    public interface IUserService
    {
        Task<bool> IsValidUserAsync(UserLogin userLogin);

        Task<UserRefreshToken> AddUserRefreshToken(UserRefreshToken user);

        Task<UserRefreshToken> GetSavedUserRefreshToken(string userName, string refreshTokn);

        Task DeleteRefreshToken(string userName, string refreshTokn);
    }
}
