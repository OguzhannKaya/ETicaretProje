using ETicaret.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Abstract
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetCustomUserById(int id);
        Task<List<User>> GetCustomUserList();
        Task<List<User>> GetCustomUserList(Expression<Func<User, bool>> expression);

    }
}
