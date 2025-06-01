using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public UserRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
