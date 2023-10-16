using System.ComponentModel.DataAnnotations;

namespace Shared;

public class User
{
    [Key]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Domain is required")]
    public string Domain { get; set; }

    //Validation for SecurityLevel 1-5
    
    [Required(ErrorMessage = "SecurityLevel is required")]
    public int SecurityLevel { get; set; }
    
    [Required(ErrorMessage = "Role is required")]
    public Role Role { get; set; }
}