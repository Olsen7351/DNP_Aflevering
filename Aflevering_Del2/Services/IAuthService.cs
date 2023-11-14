using Shared;

namespace Aflevering_Del1.Services;

public interface IAuthService
{
    Task<User> GetUser(string username, string password);
    Task RegisterUser(User user);
}