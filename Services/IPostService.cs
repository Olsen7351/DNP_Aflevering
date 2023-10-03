using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
    Task<Post> GetPost(int id);
    Task CreatePost(PostDto post);
    Task DeletePost(int id);
}