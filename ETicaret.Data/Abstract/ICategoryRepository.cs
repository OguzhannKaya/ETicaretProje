using ETicaret.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Abstract
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Task<Category> GetCategorybyId(int id);
        Task<List<Category>> GetCustomCategoryListAsync();
        Task<List<Category>> GetCustomCategoryListAsync(Expression<Func<Category, bool>> expression);
    }
}
