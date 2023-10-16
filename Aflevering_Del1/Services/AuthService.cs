using System.ComponentModel.DataAnnotations;
using Aflevering_Del1.Dao;
using Shared;

namespace Aflevering_Del1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserDao UserDao;

        public AuthService(IUserDao userDao)
        {
            UserDao = userDao;
        }
        
        private async Task<User> ValidateUser(string username, string password)
        {
            if (username == null || password == null)
            {
                throw new ArgumentNullException("Username or password is null");
            }
            
            var user = await UserDao.GetUser(username, password);

            if (user == null)
            {
                throw new NullReferenceException("User not found");
            }
            return user;
        }

        public Task<User> GetUser(string username, string password)
        {
            return ValidateUser(username, password);
        }

        public async Task RegisterUser(User user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    throw new ValidationException("Username is required");
                }
                if (string.IsNullOrEmpty(user.Password))
                {
                    throw new ValidationException("Password is required");
                }

                // Add the user to the DAO
                await UserDao.RegisterUser(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}