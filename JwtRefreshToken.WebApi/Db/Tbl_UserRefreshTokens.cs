using System.ComponentModel.DataAnnotations;

namespace JwtRefreshToken.WebApi.Db
{
    public class Tbl_UserRefreshTokens
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        public bool IsActive { get; set; }=true;
    }
}
