using Microsoft.EntityFrameworkCore;

namespace JwtRefreshToken.WebApi.Db
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tbl_UserRegister> Tbl_UserRegister { get; set; }

        public DbSet<Tbl_UserRefreshTokens> Tbl_RefreshTokens { get; set; }
    }
}
