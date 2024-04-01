using System.ComponentModel.DataAnnotations;
using BooksLibrary.Api.Entities;

namespace BooksLibrary.Api.Dtos;

public record BookDTO
(
    string ID,
    string Name,
    string Genre,
    string authorID,
    string? UserID
);

public record CreateBookDTO
(
    [Required(ErrorMessage = "Book name is reuired")]
    string Name,

    [Required(ErrorMessage = "Genre is required")]
    string Genre,

    [Required(ErrorMessage = "Auther Name is required")]
    string AuthorName
);

public record UpdateBookDTO
(
        [Required(ErrorMessage = "Book name is reuired")]
    string Name,

    [Required(ErrorMessage = "Genre is required")]
    string Genre,

    [Required(ErrorMessage = "Auther Name is required")]
    string AutherName,

    string? UserID
);