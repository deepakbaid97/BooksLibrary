using System.ComponentModel.DataAnnotations;

namespace BooksLibrary.Api.Dtos;

public record UserDTO
(
    string ID,
    string Name,
    string Email,
    string Password
);

public record CreateUserDTO
(
    [Required(ErrorMessage = "User name is required")]
    string Name,

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    string Password
);

public record UpdateUserDTO
(
    [Required(ErrorMessage = "User name is required")]
    string Name,

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    string Password
);

public record LoginUserDTO
(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    string Email,

    [Required(ErrorMessage = "Password is requierd")]
    string Password
);

public record UserDetailsDTO
(
    string Name,
    string Email,
    string SessionToken,
    bool Role
);

public record UpdatePasswordDTO
(
    string Email,
    string Oldpassword,
    string NewPassword
);