
using JwtRefreshToken.WebApi.Db;

namespace JwtRefreshToken.WebApi.Feature.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserModel> AddUserRefreshToken(UserRegisterModel user)
        {
            var userModel = user.ToEntity();
            await _db.Tbl_User.AddAsync(userModel);
            await _db.SaveChangesAsync();
            return userModel.ChangeUser();
        }

        public async Task DeleteRefreshToken(string userName, string refreshToken)
        {
            var user = await _db.Tbl_User
                                .FirstOrDefaultAsync(x => x.UserName == userName && x.RefreshToken == refreshToken);
            if(user is null)
            {
                return; 
            }
            _db.Tbl_User.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async Task<UserModel> GetSaveRefreshToken(string userName, string refreshToken)
        {
            var user = await _db.Tbl_User
                                .FirstOrDefaultAsync(x => x.UserName == userName && x.RefreshToken == refreshToken);
            if(user == null)
            {
                return null;
            }
            return user.ChangeUser();
        }

        public async Task<bool> IsValidUserAsync(UserLoginModel userLogin)
        {
            var user = await _db.Tbl_User.FirstOrDefaultAsync(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
            if (user is null)
            {
                return false;
            }
            return true;
        }
    }
}
