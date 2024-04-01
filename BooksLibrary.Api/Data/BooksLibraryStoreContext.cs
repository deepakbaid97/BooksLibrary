using System.Reflection;
using BooksLibrary.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksLibrary.Api.Data;

public class BooksLibraryStoreContext : DbContext
{
    public BooksLibraryStoreContext(DbContextOptions<BooksLibraryStoreContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<User>()
            .HasKey(e => e.Email);

        modelBuilder.Entity<Book>()
            .HasKey(e => e.ID);

        modelBuilder.Entity<Author>()
            .HasKey(e => e.Name);
    }
}