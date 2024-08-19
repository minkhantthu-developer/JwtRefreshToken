using System.ComponentModel.DataAnnotations;

namespace JwtRefreshToken.WebApi.Db
{
    public class Tbl_UserRegister
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
