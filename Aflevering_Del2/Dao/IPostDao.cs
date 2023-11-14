using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Dao;

public interface IPostDao
{
    Task<IEnumerable<Post>> GetPosts();
    Task<Post> GetPost(int id);
    Task CreatePost(PostDto post);
    Task DeletePost(int id);
    Task UpdatePost(Post post);
}