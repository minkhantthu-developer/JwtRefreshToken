
namespace JwtRefreshToken.WebApi.Features.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public UserRefreshToken AddUserRefreshToken(UserRefreshToken user)
        {
            _db.userRefreshToken.Add(user);
            _db.SaveChanges();
            return user;
        }

        public async Task DeleteRefreshToken(string userName, string refreshToken)
        {
            var item = _db.userRefreshToken.FirstOrDefault(x => x.UserName == userName && x.RefreshToken == refreshToken);
            if(item is null)
            {
                return;
            }
            _db.userRefreshToken.Remove(item);
            await _db.SaveChangesAsync();
        }

        public UserRefreshToken GetSavedUserRefreshToken(string userName, string refreshTokn)
        {
            return _db.userRefreshToken.FirstOrDefault(x=>x.UserName==userName && x.RefreshToken==refreshTokn);
        }

        public async Task<bool> IsValidUserAsync(UserLogin userLogin)
        {
            var user=await _db.userRegisters
                              .FirstOrDefaultAsync(x=>x.Email==userLogin.Email &&
                                                      x.Password==userLogin.Password);
            if(user is null)
            {
                return false;
            }
            return true;
        }
    }
}
