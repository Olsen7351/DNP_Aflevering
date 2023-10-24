using System.Net.Http.Headers;
using System.Net.Http.Json;
using Aflevering_Frontend.Services.Auth;
using Frontend_Blazor.Services;
using Shared;
using Shared.Dto;

namespace Aflevering_Frontend.Services;

public class PostService : IPostService
{
    private readonly HttpClient _httpClient;

    public PostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Posts");
        if (response.IsSuccessStatusCode)
        {
            var posts = await response.Content.ReadFromJsonAsync<IEnumerable<Post>>();
            if (posts != null)
            {
                return posts;
            }
            else
            {
                throw new NullReferenceException("Posts was not found");
            }
        }
        else
        {
            throw new NullReferenceException("Posts was not found");
        }
    }

    public async Task<Post?> GetPost(int id)
    {
        var response = await _httpClient.GetAsync($"api/Posts/{id}");
        if (response.IsSuccessStatusCode)
        {
            var post = response.Content.ReadFromJsonAsync<Post>();
            return Task.FromResult(post).Result.Result;
        }
        else
        {
            throw new NullReferenceException("Post was not found");
        }
    }

    public async Task CreatePost(PostDto post)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("api/Posts", post);
        if (responseMessage.IsSuccessStatusCode)
        {
            var postResponse = responseMessage.Content.ReadFromJsonAsync<Post>();
            return Task.FromResult(postResponse).Result.Result;
        }
        else
        {
            throw new NullReferenceException("Post was not Created");
        }
    }

    public async Task DeletePost(int id)
    {
        await _httpClient.DeleteAsync("api/Posts/" + id);
    }
    public async Task UpdatePost(PostService post)
    {
        var responseMessage = await _httpClient.PutAsJsonAsync("api/Posts/" + post.Id, post);
        if (responseMessage.IsSuccessStatusCode)
        {
            var postResponse = responseMessage.Content.ReadFromJsonAsync<Post>();
            return Task.FromResult(postResponse).Result.Result;
        }
        else
        {
            throw new NullReferenceException("Post was not found");
        }
    }

}