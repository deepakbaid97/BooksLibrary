using BooksLibrary.Api.Repositories;
using BooksLibrary.Api.Dtos;
using BooksLibrary.Api.Entities;
using BooksLibrary.Api.Helper;

namespace BooksLibrary.Api.Endpoints;

public static class UserEndpoint
{
    const string GetUserEndpoint = "GetUser";

    public static RouteGroupBuilder UserEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/user")
                        .WithParameterValidation();

        group.MapGet("/", async (IUserRepository repository) =>
            (await repository.GetAllUsers()).Select(user => user.AsDto()));

        group.MapGet("/{id}", async (IUserRepository repository, string id) =>
        {
            User? user = await repository.GetUserAsync(id);
            return user is not null ? Results.Ok(user.AsDto()) : Results.NotFound();
        })
        .WithName(GetUserEndpoint);

        group.MapPost("/create", async (IUserRepository repository, CreateUserDTO userDTO) =>
        {
            string guidString = Guid.NewGuid().ToString();
            User user = new()
            {
                ID = guidString,
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password
            };
            try
            {
                await repository.CreateUserAsync(user);
                return Results.CreatedAtRoute(GetUserEndpoint, new { id = user.ID }, user);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPut("/{email}", async (IUserRepository repository, string email, UpdateUserDTO updatedUserDTO) =>
        {
            User? existingUser = await repository.GetUserAsync(email);
            return existingUser is not null ? Results.Ok(existingUser.AsDto()) : Results.NotFound();
        });

        group.MapPost("/login", async (HttpContext context, IUserRepository repository, LoginUserDTO loginUserDTO) =>
        {
            User? existingUser = await repository.GetUserAsync(loginUserDTO.Email);
            if (existingUser == null)
            {
                return Results.NotFound("Email doesn't exisit");
            }

            bool passwordMatched = PasswordHasher.VerifyPassword(existingUser.Password, loginUserDTO.Password);
            if (!passwordMatched)
            {
                return Results.NotFound("Email or Password doesn't match!");
            }

            string SessionToken = Guid.NewGuid().ToString();
            UserDetailsDTO userDetails = new UserDetailsDTO(
                Name: existingUser.Name,
                Email: existingUser.Email,
                SessionToken,
                Role: existingUser.Email == "deepak1@gmail.com");

            // Store the genereated token for the user
            AuthHelper.tokenToUser.Add(SessionToken, userDetails);
            return Results.Json(userDetails);
        });

        group.MapPut("/updatePassword", async (HttpContext context, IUserRepository repository, UpdatePasswordDTO user) =>
        {
            string? authToken = context.Request.Headers["Authorization"];
            if (AuthHelper.CanAccess(false, authToken) == false)
            {
                return Results.BadRequest("You're not authorised to update Password");
            }
            try
            {
                await repository.UpdatePassword(user.Email, user.Oldpassword, user.NewPassword);
                return Results.Ok("password changed successfully");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        group.MapPost("/auth", (IUserRepository repository, UserDetailsDTO user) =>
        {
            UserDetailsDTO? userDetails = AuthHelper.tokenToUser[user.SessionToken];

            if (userDetails == null) return Results.BadRequest();

            return Results.Json(userDetails);
        });
        return group;
    }
}