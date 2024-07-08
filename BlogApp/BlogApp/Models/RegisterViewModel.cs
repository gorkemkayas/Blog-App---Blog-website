using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string? SurName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }




        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }


        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength:30,MinimumLength =6,ErrorMessage ="Please enter at least '6', up to '30' characters.")]
        public string? Password { get; set;}

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [StringLength(maximumLength:30,MinimumLength =6)]
        public string? ConfirmPassword { get; set;}

        public string? Title { get; set; }
    }
}