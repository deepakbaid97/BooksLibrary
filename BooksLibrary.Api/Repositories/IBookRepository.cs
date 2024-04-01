using BooksLibrary.Api.Dtos;
using BooksLibrary.Api.Entities;

namespace BooksLibrary.Api.Repositories;

public interface IBookRepository
{
    Task CreateBookAsync(CreateBookDTO book, IAuthorRepository authorRepository);
    Task<Book?> GetBookByIdAsync(string id);
    Task<IEnumerable<BookDTO>> GetAllBookAsync();
    Task UpdateBookAsync(Book updatedBook);
}