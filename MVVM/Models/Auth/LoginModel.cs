using System.ComponentModel.DataAnnotations;

namespace _4kTiles_Frontend.MVVM.Models.Auth
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
