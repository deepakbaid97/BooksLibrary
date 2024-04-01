using BooksLibrary.Api.Entities;

namespace BooksLibrary.Api.Repositories;

public interface IUserRepository
{
    Task CreateUserAsync(User user);
    Task<User?> GetUserAsync(string id);
    Task<IEnumerable<User>> GetAllUsers();
    Task UpdateAsync(User updatedUser);
    Task UpdatePassword(string email, string oldPassword, string newPassword);
}