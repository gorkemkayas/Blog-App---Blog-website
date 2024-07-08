using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength:30,MinimumLength =6,ErrorMessage ="Please enter at least '6', up to '30' characters.")]
        public string? Password { get; set;}
    }
}