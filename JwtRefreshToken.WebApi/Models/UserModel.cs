namespace JwtRefreshToken.WebApi.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }
                     
        public string? Email { get; set; }
                     
        public string? Password { get; set; }

        public string? RefreshToken { get; set; }
    }
}
