using BooksLibrary.Api.Data;
using BooksLibrary.Api.Dtos;
using BooksLibrary.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksLibrary.Api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BooksLibraryStoreContext dbContext;
    public BookRepository(BooksLibraryStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateBookAsync(CreateBookDTO bookDTO, IAuthorRepository authorRepository)
    {
        Book? existingBook = await GetBookByIdAsync(bookDTO.Name);
        if (existingBook != null) throw new Exception("Book already exisits");

        CreateAuthorDTO createAuthorDTO = new(bookDTO.AuthorName);
        Author author = await authorRepository.CreateAuthor(createAuthorDTO);
        string guidString = Guid.NewGuid().ToString();
        Book newBook = new Book()
        {
            ID = guidString,
            Name = bookDTO.Name,
            AuthorID = author.ID,
            Genre = bookDTO.Genre,
            UserID = null
        };
        dbContext.Books.Add(newBook);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookDTO>> GetAllBookAsync()
    {
        var list = dbContext.Books.AsNoTracking();
        var alist = dbContext.Authors.AsNoTracking();
        return await (from l in list
                      join author in alist on l.AuthorID equals author.ID
                      select new BookDTO(l.ID, l.Name, l.Genre, author.Name, l.UserID)).ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(string id)
    {
        return await dbContext.Books.FindAsync(id);
    }

    public async Task UpdateBookAsync(Book updatedBook)
    {
        dbContext.Books.Update(updatedBook);
        await dbContext.SaveChangesAsync();
    }
}