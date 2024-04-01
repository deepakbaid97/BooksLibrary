using BooksLibrary.Api.Dtos;

namespace BooksLibrary.Api.Helper;

public class AuthHelper
{
    public static Dictionary<string, UserDetailsDTO> tokenToUser = new Dictionary<string, UserDetailsDTO>();
    public static bool CanAccess(bool roleNeeded, string? authToken)
    {
        if (authToken == null) return false;
        UserDetailsDTO? user = tokenToUser[authToken];

        if (user == null)
        {
            return false;
        }
        if (user.SessionToken == authToken && (user.Role == roleNeeded || user.Role))
        {
            return true;
        }

        return false;
    }
}