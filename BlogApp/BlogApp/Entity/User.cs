using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Entity
{
    public class User
    {
        public int UserId { get; set; }

        public string? Title { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        public string? Image { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}