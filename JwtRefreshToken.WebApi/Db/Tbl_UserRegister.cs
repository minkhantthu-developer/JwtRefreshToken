using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtRefreshToken.WebApi.Db
{
    [Table("Tbl_UserRegister")]
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

        public string RefreshToken { get; set; }
    }
}
