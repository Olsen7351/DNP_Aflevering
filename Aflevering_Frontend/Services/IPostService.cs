using Shared;
using Shared.Dto;

namespace Frontend_Blazor.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
    Task<Post?> GetPost(int id);
    Task CreatePost(PostDto post);
    Task DeletePost(int id);
}