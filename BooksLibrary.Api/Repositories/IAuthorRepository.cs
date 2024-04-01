using BooksLibrary.Api.Dtos;
using BooksLibrary.Api.Entities;

namespace BooksLibrary.Api.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> CreateAuthor(CreateAuthorDTO authorDTO);
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorAsync(string name);
    }
}