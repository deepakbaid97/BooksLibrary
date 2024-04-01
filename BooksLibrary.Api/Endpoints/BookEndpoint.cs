using BooksLibrary.Api.Dtos;
using BooksLibrary.Api.Entities;
using BooksLibrary.Api.Helper;
using BooksLibrary.Api.Repositories;

namespace BooksLibrary.Api.Endpoints;

public static class BookEndpoint
{
    public static RouteGroupBuilder BookEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/books")
                        .WithParameterValidation();

        group.MapGet("/", async (HttpContext context, IBookRepository repository) =>
        await repository.GetAllBookAsync());

        group.MapPost("/create", async (HttpContext context, IBookRepository bookRepositoryrepository, IAuthorRepository authorRepository, CreateBookDTO bookDTO) =>
        {
            string? token = context.Request.Headers["Authorization"];
            if (AuthHelper.CanAccess(true, token) == false)
            {
                return Results.BadRequest("Invalid Auth. Sign in as an Admin");
            }

            await bookRepositoryrepository.CreateBookAsync(bookDTO, authorRepository);
            return Results.Ok();
        });

        group.MapGet("/{id}", async (HttpContext context, IBookRepository bookRepositoryrepository, string id) =>
        {
            string? token = context.Request.Headers["Authorization"];
            if (AuthHelper.CanAccess(false, token) == false)
            {
                return Results.BadRequest("Invalid Auth");
            }

            Book? book = await bookRepositoryrepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return Results.NotFound();
            }

            return Results.Json(book);
        });

        group.MapPut("/takeBook/{id}", async (HttpContext context, IBookRepository bookRepositoryrepository, string id) =>
        {
            string? token = context.Request.Headers["Authorization"];
            if (AuthHelper.CanAccess(false, token) == false)
            {
                return Results.BadRequest("Invalid Auth. Sign in as an Admin");
            }

            Book? existingBook = await bookRepositoryrepository.GetBookByIdAsync(id);
            if (existingBook == null) return Results.NotFound();

            existingBook.UserID = AuthHelper.tokenToUser[token].Email;
            await bookRepositoryrepository.UpdateBookAsync(existingBook);

            return Results.Ok();
        });

        group.MapPut("/returnBook/{id}", async (HttpContext context, IBookRepository bookRepositoryrepository, string id) =>
        {
            string? token = context.Request.Headers["Authorization"];
            if (AuthHelper.CanAccess(false, token) == false)
            {
                return Results.BadRequest("Invalid Auth. Sign in as an Admin");
            }

            Book? existingBook = await bookRepositoryrepository.GetBookByIdAsync(id);
            if (existingBook == null) return Results.NotFound();

            existingBook.UserID = null;
            await bookRepositoryrepository.UpdateBookAsync(existingBook);

            return Results.Ok();
        });

        return group;
    }
}