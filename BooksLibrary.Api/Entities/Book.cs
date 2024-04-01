using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksLibrary.Api.Entities;

public class Book
{
    public required string ID { get; set; }

    [Required(ErrorMessage = "Book name is required")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Genre is required")]
    public required string Genre { get; set; }

    [Required(ErrorMessage = "Author ID is required")]
    public required string AuthorID { get; set; }

    public string? UserID { get; set; }
}
