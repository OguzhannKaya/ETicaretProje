using ETicaret.Data.Abstract;
using ETicaret.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetCustomUserById(int id)
        {
            return await _dbSet.AsNoTracking().Include(x => x.Role).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<User>> GetCustomUserList()
        {
             return await _dbSet.Include(x=> x.Role).ToListAsync();
        }

        public async Task<List<User>> GetCustomUserList(Expression<Func<User, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().Include(x => x.Role).ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking().Include(x => x.Role).FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
