using System.IO.Compression;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;
        public EfPostRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public void UpdatePost()
        {
            _context.SaveChanges();
        }

        public void EditPost(Post post)
        {

        }

        public void EditPost(Post post, int[] tagIds)
        {
            var entity = _context.Posts.Include(x => x.Tags).FirstOrDefault(i  => i.PostId == post.PostId);

            if(entity != null)
            {
                entity.Title = post.Title;
                entity.Content = post.Content;
                entity.Url = post.Url;
                entity.isActive = post.isActive; 
                entity.Tags =  _context.Tags.Where(x => tagIds.Contains(x.TagId)).ToList();
            }

            _context.SaveChanges();
        }
    }
}