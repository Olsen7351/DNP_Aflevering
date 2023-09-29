using Aflevering_Del1.Dao;
using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Services;

public class PostService : IPostService
{
    private readonly IPostDao PostDao;

    public PostService(IPostDao postDao)
    {
        PostDao = postDao;
    }


    public Task<IEnumerable<Post>> GetPosts()
    {
        return PostDao.GetPosts();
    }

    public Task CreatePost(PostDto post)
    {
        PostDao.CreatePost(post);
        return Task.CompletedTask;
    }
}