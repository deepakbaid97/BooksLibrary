using System.ComponentModel.DataAnnotations;

namespace BooksLibrary.Api.Entities;

public class Author
{
    public required string ID { get; set; }

    [Required(ErrorMessage = "Author name is required")]
    public required string Name { get; set; }

}