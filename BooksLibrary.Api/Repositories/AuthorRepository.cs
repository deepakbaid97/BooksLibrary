using BooksLibrary.Api.Data;
using BooksLibrary.Api.Dtos;
using BooksLibrary.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksLibrary.Api.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BooksLibraryStoreContext dbContext;
    public AuthorRepository(BooksLibraryStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Author> CreateAuthor(CreateAuthorDTO authorDTO)
    {
        Author? existingAuthor = await GetAuthorAsync(authorDTO.Name);
        if (existingAuthor != null)
        {
            return existingAuthor;
        }
        string guidString = Guid.NewGuid().ToString();
        Author newAuthor = new Author()
        {
            ID = guidString,
            Name = authorDTO.Name
        };
        dbContext.Authors.Add(newAuthor);
        await dbContext.SaveChangesAsync();

        return newAuthor;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await dbContext.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<Author?> GetAuthorAsync(string name)
    {
        return await dbContext.Authors.FindAsync(name);
    }
}