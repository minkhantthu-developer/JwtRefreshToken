using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtRefreshToken.WebApi.Models
{
    [Table("Tbl_UserRegister")]
    public class UserRegister
    {
        [Key]   
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
