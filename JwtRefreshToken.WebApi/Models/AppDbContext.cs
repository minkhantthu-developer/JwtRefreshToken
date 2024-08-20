namespace JwtRefreshToken.WebApi.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserRegister> userRegisters { get; set; }

        public DbSet<UserRefreshToken> userRefreshToken { get; set; }
    }
}
