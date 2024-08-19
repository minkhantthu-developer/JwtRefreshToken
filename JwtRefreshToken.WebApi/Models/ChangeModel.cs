using JwtRefreshToken.WebApi.Db;

namespace JwtRefreshToken.WebApi.Models
{
    public static class ChangeModel
    {
        public static UserModel ChangeUser(this Tbl_UserRegister userRegister)
        {
            return new UserModel
            {
                UserId = userRegister.UserId,
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                Password = userRegister.Password,
                RefreshToken = userRegister.RefreshToken
            };
        }

        public static Tbl_UserRegister ToEntity(this UserRegisterModel user)
        {
            return new Tbl_UserRegister
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}
