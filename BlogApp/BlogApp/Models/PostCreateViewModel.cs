using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    public class PostCreateViewModel
    {

        public int PostId { get; set; }

        public bool isActive { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Required]
        [Display(Name = "Content")]
        
        public string? Content { get; set;}

        [Required]
        [Display(Name = "Url")]
        public string? Url { get; set;}

        public List<Tag> Tags { get; set; } = new();
    }
}