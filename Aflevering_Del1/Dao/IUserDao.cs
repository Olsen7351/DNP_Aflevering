using Shared;

namespace Aflevering_Del1.Dao;

public interface IUserDao
{
    Task<User> GetUser(string username, string password);
    Task RegisterUser(User user);
}