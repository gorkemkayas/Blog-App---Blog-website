using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Entity
{
    public class Tag
    {
        public enum TagColors
        {
            primary, danger, warning, success, secondary 
        }

        public int TagId { get; set; }
        public string? Url { get; set; }
        public string? Text { get; set; }

        public TagColors? Color { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

    }
}