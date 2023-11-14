using Aflevering_Del1.Dao;
using Aflevering_Del1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PostsController
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }
    
    
    [HttpGet]
    public async Task<IEnumerable<Post>> GetPosts()
    {
        return await _postService.GetPosts();
    }
    [HttpGet("{id}")]
    public async Task<Post> GetPost(int id)
    {
        return await _postService.GetPost(id);
    }
    
    [HttpPost]
    public async Task CreatePost(PostDto post)
    {
        await _postService.CreatePost(post);
    }
    
    [HttpDelete("{id}")]
    public async Task DeletePost(int id)
    {
        await _postService.DeletePost(id);
    }

    [HttpPost("Update")]
    public async Task UpdatePost(Post post)
    {
        await _postService.UpdatePost(post);
    }
    
}