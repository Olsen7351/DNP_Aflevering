using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
    Task CreatePost(PostDto post);
}