using System.ComponentModel.DataAnnotations;

namespace Shared.Dto;

public class PostDto
{
    
    [Required (ErrorMessage = "Header is required")]
    public string Header { get; set; }
    
    [Required (ErrorMessage = "Body is required")]
    public string Body { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}