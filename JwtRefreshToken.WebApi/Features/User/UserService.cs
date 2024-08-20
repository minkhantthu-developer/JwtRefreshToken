
namespace JwtRefreshToken.WebApi.Features.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserRefreshToken> AddUserRefreshToken(UserRefreshToken user)
        {
           await  _db.userRefreshToken.AddAsync(user);
           await _db.SaveChangesAsync();
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

        public async Task< UserRefreshToken> GetSavedUserRefreshToken(string userName, string refreshTokn)
        {
            var tokens= await _db.userRefreshToken.FirstOrDefaultAsync(x=>x.UserName==userName && x.RefreshToken==refreshTokn);
            return tokens;
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
