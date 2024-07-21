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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> GetCustomProduct(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<List<Product>> GetCustomProductList()
        {
            return await _dbSet.AsNoTracking().Include(x => x.Category).ToListAsync();
        }

        public async Task<List<Product>> GetCustomProductList(Expression<Func<Product, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().Include(x => x.Category).ToListAsync();
        }
    }
}
