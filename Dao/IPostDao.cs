using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Dao;

public interface IPostDao
{
    Task<IEnumerable<Post>> GetPosts();
    Task CreatePost(PostDto post);
}