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
    public Task<Post> GetPost(int id)
    {
        return PostDao.GetPost(id);
    }
    public Task<Post> UpdatePost(Post post)
    {
        if (post == null)
        {
            throw new NullReferenceException();
        }
        return PostDao.UpdatePost(post);
    }
    public Task DeletePost(int id)
    {
        if (id < 0)
        {
            throw new NullReferenceException();
        }

        PostDao.DeletePost(id);
        return Task.CompletedTask;
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