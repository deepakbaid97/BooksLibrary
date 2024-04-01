using System.ComponentModel.DataAnnotations;

namespace BooksLibrary.Api.Entities;

public class User
{
    public required string ID { get; set; }

    [Required(ErrorMessage = "User name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Key]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public required string Password { get; set; }
}