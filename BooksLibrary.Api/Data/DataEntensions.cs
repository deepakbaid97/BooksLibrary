using BooksLibrary.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksLibrary.Api.Data;

public static class DataEntensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BooksLibraryStoreContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("BooksLibraryStoreContext");
        services.AddSqlServer<BooksLibraryStoreContext>(connString)
                .AddScoped<IAuthorRepository, AuthorRepository>()
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}