using BlogApp.Entity;

namespace BlogApp.Data.Abstract
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }

        void CreatePost(Post post);

        public void UpdatePost();
        public void EditPost(Post post);
        void EditPost(Post entityToUpdate, int[] tagIds);
    }
}