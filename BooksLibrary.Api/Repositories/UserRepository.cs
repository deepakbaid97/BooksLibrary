using BooksLibrary.Api.Data;
using BooksLibrary.Api.Entities;
using BooksLibrary.Api.Helper;
using Microsoft.EntityFrameworkCore;

namespace BooksLibrary.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BooksLibraryStoreContext dbContext;

        public UserRepository(BooksLibraryStoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            User? exisitingUser = await GetUserAsync(user.Email);
            if (exisitingUser != null) throw new Exception("Email already exists");
            user.Password = PasswordHasher.HashPassword(user.Password);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserAsync(string email)
        {
            return await dbContext.Users.FindAsync(email);
        }

        public async Task UpdateAsync(User updatedUser)
        {
            dbContext.Users.Update(updatedUser);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdatePassword(string email, string oldPassword, string newPassword)
        {
            User? existingUser = await GetUserAsync(email);
            if (existingUser == null)
            {
                throw new Exception("User doesn't exisit");
            }

            if (PasswordHasher.VerifyPassword(existingUser.Password, oldPassword))
            {
                existingUser.Password = PasswordHasher.HashPassword(newPassword);
                dbContext.Users.Update(existingUser);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Password doesn't match");
            }
        }
    }
}