using Microsoft.EntityFrameworkCore;
using Shared;

namespace Aflevering_Del1.Dao
{
    public class USerEFDao : IUserDao
    {
        private readonly DNP_Context _context;

        public USerEFDao(DNP_Context context)
        {
            _context = context;
        }
        public async Task<User> GetUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task RegisterUser(User user)
        {
            // Check if the username is already taken
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                throw new InvalidOperationException("Username is already taken");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
