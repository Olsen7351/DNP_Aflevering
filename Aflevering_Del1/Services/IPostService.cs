using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
    Task<Post> GetPost(int id);
    Task UpdatePost(Post post);
    Task CreatePost(Post post);
    Task DeletePost(int id);
}