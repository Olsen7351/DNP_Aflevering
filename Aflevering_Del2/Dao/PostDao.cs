using System.Text.Json;
using Aflevering_Del1.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Dao;

public class PostDao : IPostDao
{
    private readonly string PostFilePath; // Path to the JSON file for storing Posts
    private List<Post>? Posts;
    private int PostID;

    public PostDao()
    {
        PostFilePath = Path.Combine(Environment.CurrentDirectory, "../Resource/posts.json");
        if (File.Exists(PostFilePath))
        {
            string jsonData = File.ReadAllText(PostFilePath);
            Posts = JsonSerializer.Deserialize<List<Post>>(jsonData);
            PostID = Posts.Count;
        }
        else
        {
            // If the file doesn't exist, create an empty list.
            Posts = new List<Post>();
        }
    }
    public async Task<IEnumerable<Post>> GetPosts()
    {
        if (Posts != null)
        {
            return await Task.FromResult(Posts);
        }
        else
        {
            throw new PostListNotFound("Post not found");
        }
    }
    public async Task<Post> GetPost(int id)
    {
        var post = Posts.Find(p => p.Id == id);
        if (post != null)
        {
            return await Task.FromResult(post);
        }
        else
        {
            throw new PostNotFoundException("Post not found");
        }
    }

    public Task DeletePost(int id)
    {
        if (id < 0)
        {
            throw new InvalidDataException("Post ID is invalid");
        }
        var post = Posts.Find(p => p.Id == id);
        if (post != null)
        {
            Posts.Remove(post);
            SaveChanges();
        }
        else
        {
            throw new PostNotFoundException("Post not found");
        }
        return Task.CompletedTask;
    }

    public Task CreatePost(PostDto post)
    {
        if (post == null)
        {
            throw new InvalidDataException("Post is null");
        }

        try
        {
            //If post ID already exists +1 to the ID
            while (Posts.Any(p => p.Id == PostID))
            {
                PostID++;
            }

            Post NewPost = new Post(Posts.Count, post.Header, post.Body, post.CreatedAt);
            Posts.Add(NewPost);
            SaveChanges();
        }
        catch (Exception e)
        {
            throw new PostNotSaved("Post already exists");
        }

        return Task.CompletedTask;
    }
    public Task UpdatePost(Post post)
    {
        if (post.Equals(null) || Posts.IsNullOrEmpty())
        {
            throw new InvalidDataException("Post is null");
        }
        int id = post.Id;
        Post postToUpdate = Posts.Find(post => post.Id.Equals(id));
        if (postToUpdate != null)
        {
            postToUpdate.SubComments = post.SubComments;
            SaveChanges();
        }
        else
        {
            throw new PostNotFoundException("Post not found");
        }
        return Task.CompletedTask;
    }

    private void SaveChanges()
    {
        string jsonData = JsonSerializer.Serialize(Posts);
        if (!File.Exists(PostFilePath) || new FileInfo(PostFilePath).Length == 0)
        {
            File.WriteAllText(PostFilePath, "{}");
        }
        File.WriteAllText(PostFilePath, jsonData);
    }
}