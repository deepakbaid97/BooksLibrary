using BooksLibrary.Api.Dtos;

namespace BooksLibrary.Api.Entities;

public static class EntityExtensions
{
    public static UserDTO AsDto(this User user)
    {
        return new UserDTO(
            user.ID,
            user.Name,
            user.Email,
            user.Password
        );
    }

    public static BookDTO AsDto(this Book book)
    {
        return new BookDTO(
            book.ID,
            book.Name,
            book.Genre,
            book.AuthorID,
            book.UserID
        );
    }
}