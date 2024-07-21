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
    public class OrderRepository : Repository<Orders>, IOrderRepository
    {
        public OrderRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Orders>> GetCustomOrderListAsync()
        {
            return await _dbSet.AsNoTracking().Include(x=> x.User).Include(y=> y.Category).ToListAsync();
        }

        public async Task<List<Orders>> GetCustomOrderListAsync(Expression<Func<Orders, bool>> expression)
        {
            return await _dbSet.Where(expression).Include(x=> x.User).ToListAsync();
        }

        public async Task<Orders> GetOrderbyId(int id)
        {
            return await _dbSet.AsNoTracking().Include(x=> x.User).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
