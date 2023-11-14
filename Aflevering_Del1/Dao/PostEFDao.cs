using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Dao
{
    public class PostEFDao : IPostDao
    {
        private readonly DNP_Context _context;

        public PostEFDao(DNP_Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            IQueryable<Post> postQueryable = _context.Posts.AsQueryable();
            return await postQueryable.ToListAsync();
        }

        public async Task<Post> GetPost(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task CreatePost(Post post)
        {
            EntityEntry<Post> newPost = await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePost(int id)
        {
            var postToDelete = await _context.Posts.FindAsync(id);
            if (postToDelete != null)
            {
                _context.Posts.Remove(postToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePost(Post post)
        {
            var existingPost = await _context.Posts.FindAsync(post.Id);
            if (existingPost != null)
            {
                existingPost.Header = post.Header;
                existingPost.Body = post.Body;
                existingPost.SubComments = post.SubComments;
                _context.Posts.Add(existingPost);
                // Update other properties as needed

                await _context.SaveChangesAsync();
            }
        }
    }
}
