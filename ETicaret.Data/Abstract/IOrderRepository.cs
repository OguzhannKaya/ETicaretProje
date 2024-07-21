using ETicaret.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Abstract
{
    public interface IOrderRepository: IRepository<Orders>
    {
        Task<Orders> GetOrderbyId(int id);
        Task<List<Orders>> GetCustomOrderListAsync();
        Task<List<Orders>> GetCustomOrderListAsync(Expression<Func<Orders, bool>> expression);
    }
}
