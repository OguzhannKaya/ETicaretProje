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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetCategorybyId(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Category>> GetCustomCategoryListAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<Category>> GetCustomCategoryListAsync(Expression<Func<Category, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().ToListAsync();
        }
    }
}
