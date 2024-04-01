namespace BooksLibrary.Api.Dtos;

public record AuthorDTO
(
    string ID,
    string Name

);

public record CreateAuthorDTO
(
    string Name
);