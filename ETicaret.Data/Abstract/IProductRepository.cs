using ETicaret.Data.Concrete;
using ETicaret.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product> GetCustomProduct(int id);
        Task<List<Product>> GetCustomProductList();
        Task<List<Product>> GetCustomProductList(Expression<Func<Product, bool>> expression);
    }
}
