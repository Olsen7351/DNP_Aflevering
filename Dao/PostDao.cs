using System.Text.Json;
using Aflevering_Del1.Exceptions;
using Shared;
using Shared.Dto;

namespace Aflevering_Del1.Dao;

public class PostDao: IPostDao
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

    public Task CreatePost(PostDto post)
    {
        if (post == null)
        {
            throw new InvalidDataException("Post is null");
        }

        try
        {
            //If post ID already exists +1 to the ID
            while (Posts.Any(p=>p.Id == PostID))
            {
                PostID++;
            }

            Post NewPost = new Post(Posts.Count,post.Header, post.Body,post.CreatedAt);
            Posts.Add(NewPost);
            SaveChanges();
        }
        catch (Exception e)
        {
            throw new PostNotSaved("Post already exists");
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