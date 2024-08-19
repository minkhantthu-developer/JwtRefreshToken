namespace JwtRefreshToken.WebApi.Feature.User
{
    public interface IUserService
    {
        Task<bool> IsValidUserAsync(UserLoginModel userLogin);

        Task<UserModel> AddUserRefreshToken(UserRegisterModel user);

        Task<UserModel> GetSaveRefreshToken(string userName,string refreshToken); 

        Task DeleteRefreshToken(string userName,string refreshToken);
    }
}
