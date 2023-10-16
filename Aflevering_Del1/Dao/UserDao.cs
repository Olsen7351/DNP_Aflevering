using System.Net;
using System.Text.Json;
using Aflevering_Del1.Exceptions;
using Shared;

namespace Aflevering_Del1.Dao
{
    public class UserDao : IUserDao
    {
        private readonly string usersFilePath; // Path to the JSON file for storing users
        private List<User>? users; 

        public UserDao()
        {
            usersFilePath = Path.Combine(Environment.CurrentDirectory, "../Resource/users.json");
            // Initialize the users list by reading data from the file, if the file exists.
            if (File.Exists(usersFilePath))
            {
                string jsonData = File.ReadAllText(usersFilePath);
                users = JsonSerializer.Deserialize<List<User>>(jsonData);
            }
            else
            {
                // If the file doesn't exist, create an empty list.
                users = new List<User>();
            }
        }

        public async Task<User> GetUser(string username, string password)
        {
            var user = users.Find(u => u.Username == username);

            // Check if the user is found and return it as a Task
            if (user != null)
            {
                return await Task.FromResult(user);
            }
            else
            {
                // If the user is not found, throw an exception
                throw new UserNotFoundException("User not found");
            }
        }



        public Task RegisterUser(User user)
        {
            if (users.Exists(u => u.Username == user.Username))
            {
                throw new UserAlreadyExistsException("User already exists");
            }
            users.Add(user);
            SaveChanges();
            return Task.CompletedTask;
        }

        private void SaveChanges()
        {
            string jsonData = JsonSerializer.Serialize(users);
            if (!File.Exists(usersFilePath) || new FileInfo(usersFilePath).Length == 0)
            {
                File.WriteAllText(usersFilePath, "{}");
            }
            File.WriteAllText(usersFilePath, jsonData);
        }
    }
}