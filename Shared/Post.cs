using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Post
{
    [Key]
    public int Id { get; set; }
    
    [Required (ErrorMessage = "Header is required")]
    public string Header { get; set; }
    
    [Required (ErrorMessage = "Body is required")]
    public string Body { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }

    public Post(int id, string header, string body, DateTime createdAt)
    {
        Id = id;
        Header = header;
        Body = body;
        CreatedAt = createdAt;
    }
}