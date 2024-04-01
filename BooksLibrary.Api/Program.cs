using BooksLibrary.Api.Data;
using BooksLibrary.Api.Endpoints;
using BooksLibrary.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BooksLibraryStoreContext");
builder.Services.AddRepositories(builder.Configuration);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
             .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();

await app.Services.InitializeDbAsync();
app.UseCors("AllowAll");
app.UserEndpoints();
app.BookEndpoints();

app.Run();
