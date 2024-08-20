using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtRefreshToken.WebApi.Models
{
    [Table("Tbl_UserRefreshToken")]
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string UserName { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        public bool IsActive { get; set; }
    }
}
